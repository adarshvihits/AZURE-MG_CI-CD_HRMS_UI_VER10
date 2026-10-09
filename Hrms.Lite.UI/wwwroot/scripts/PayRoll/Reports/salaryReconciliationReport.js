function GetList() {

    debugger
    var Month = $('#Month').val();
    var PayrollGroup = $('#PayrollGroup').val();
    /* showSpinner();*/

    if (Month !== '' && PayrollGroup !== '') {
        //var url = "/PayRoll/PFSummaryReport/GetList?Month=" + Month + "&PFGroup=" + PFGroup;
        //$('#PFSummaryID').load(url, function () { /*hideSpinner();*/ });
        $.ajax({

            type: 'GET',
            url: '/PayRoll/SalaryReconciliationReport/GetList?Month=' + Month + '&PFGroup=' + PayrollGroup,
            dataType: 'html',
            data: $('#BasicFilter').serialize(),
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }

                else {

                    $('#listView').html("");
                    $('#listView').html(result);
                }

                hideSpinner();
            },


            error: function (xhr, textStatus, errorThrown) {
                debugger
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);
                hideSpinner();

            }
        });
    }
    else {

        return false;
    }
}



function fillData(type,bankCode) {

    debugger;
    var selMonth = $('#Month').val();
    var selPayrollGrp = $('#PayrollGroup').val();
    var Filter = $('#BasicFilter').serialize();

    var temp = Filter;


    var dataTable = $("#SalaryReconciliationEmployeeTable").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/PayRoll/SalaryReconciliationReport/GetEmployeeList",
            "type": "POST",


            "datatype": "json",


            'data':

                function (d) {
                    temp = '';

                    var start = d.start;

                    var searchValue = $('#dataTableSearchPopUp').val();
                    temp = Filter + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&month=' + selMonth + '&PFGroup=' + selPayrollGrp + '&Type=' + type + '&BANKCODE=' + bankCode + '&dummy=dummy';
                    d.filter = temp;

                    return JSON.stringify(d);
                },


            "dataSrc": function (d) {
                debugger;
                console.log(d);
                $('#SalaryReconciliationEmployeeDetails_Count').val(d.recordsTotal);
                $('#Code').val(bankCode);//bank code
                $('#Type').val(type);
                return d.data;
            }

        },

        "columns": [

            { "data": "employeeID", "width": "8%" },
            { "data": "employeeName", "width": "25%" },
            { "data": "designation", "width": "22%" },
            { "data": "department", "width": "15%" },
            { "data": "location", "width": "15%" },
            { "data": "salary", "width": "15%" },          


        ],






        "dom": '<t><"my-wrapper"<"my-inner2"l><"my-inner1"i><"my-inner3"p>>',

        "serverSide": "true",
        "scrollY": '52vh',
        "sScrollX": "100%",
        "sScrollXInner": "100%",

        "order": [0, "asc"],
        "processing": "true",
        "language": {

            emptyTable: '<div style="text-align: center;display: flex;align-items: center;justify-content: center;height: 45vh;"><img src="/images/nodata.png" /></div >',

            zeroRecords: '<div style="text-align: center;display: flex;align-items: center;justify-content: center;height: 45vh;"><img src="/images/nodata.png" /></div >',

            sLengthMenu: "_MENU_",
            paginate: {


                next: '<img width="100%" src="/images/icons/Icon awesome-arrow-right-1.png" >',
                previous: '<img src="/images/icons/Icon awesome-arrow-right.png">',
                first: '<img src="/images/icons/Icon metro-next-1.png">',
                last: '<img src="/images/icons/Icon metro-next.png">'




            }
        }



    });



    $('.ihits-table table').removeClass("dataTable no-footer");


    $('.paginate_button.next').attr("data-bs-toggle", "tooltip");
    $('.paginate_button.next').attr("title", "Next");
    $('.paginate_button.previous').attr("data-bs-toggle", "tooltip");
    $('.paginate_button.previous').attr("title", "Previous");
    $('.paginate_button.first').attr("data-bs-toggle", "tooltip");
    $('.paginate_button.first').attr("title", "First");
    $('.paginate_button.last').attr("data-bs-toggle", "tooltip");
    $('.paginate_button.last').attr("title", "Last");


    $("#dataTableSearchPopUp").on("keyup search input paste cut", function () {
        dataTable.search(this.value).draw();

    });

    $('.search-slider-inner2').click(function () {
        //dataTable.search('').draw();
        $('#dataTableSearchPopUp').val('');
        $('#dataTableSearchPopUp').focus();
    });

    dataTable.on('draw', function () {
        $('.paginate_button.next').attr("data-bs-toggle", "tooltip");
        $('.paginate_button.next').attr("title", "Next");
        $('.paginate_button.previous').attr("data-bs-toggle", "tooltip");
        $('.paginate_button.previous').attr("title", "Previous");
        $('.paginate_button.first').attr("data-bs-toggle", "tooltip");
        $('.paginate_button.first').attr("title", "First");
        $('.paginate_button.last').attr("data-bs-toggle", "tooltip");
        $('.paginate_button.last').attr("title", "Last");

    });

    $.fn.DataTable.ext.pager.numbers_length = 5;
}

function ApplyFilter(mode) {
    debugger
    let code = $('#Code').val();//bankcode
    let type = $('#Type').val();
    if (mode == 'apply') {

        if (filterApplicable >= 1) {
            fillData(type, code);
            CloseSlider('basicFilter');
            $('#filterForm').removeClass('filter-apply');
        }
        else {
            fillData(type, code);
            CloseSlider('basicFilter');
            $('#filterForm').addClass('filter-apply');
        }

    }
    else {

        filterApplicable = 1;
        $('.reset').val('');
        $('#Optional_Filter_EmploymentStatus_Code').val(0);
        fillData(type, code);
        CloseSlider('basicFilter');
        $('#filterForm').removeClass('filter-apply');
    }

}



function ExportToExcel() {
    debugger
    var TableLength = $('#SalaryReconciliationDetails_Count').val();
    /* var TableLength = document.getElementById("vacList").rows.length - 1;*/
    var selMonth = $('#Month').val();
    var selPayrollGrp = $('#PayrollGroup').val();    
    if (TableLength > 0) {
        url = "/PayRoll/SalaryReconciliationReport/ExportToExcel";
        window.location.href = url + "?Month=" + selMonth + "&PayrollGroup=" + selPayrollGrp + "&monthTxt=" + $('#Month option:selected').text() + "&PayrollGrpTxt=" + $('#PayrollGroup option:selected').text();
    }
    else {
        swal.fire("No data found to export");
    }
}

function ExportToExcelPopUp() {
    debugger
    var TableLength = $('#SalaryReconciliationEmployeeDetails_Count').val();
    /* var TableLength = document.getElementById("vacList").rows.length - 1;*/
    var selMonth = $('#Month').val();
    var selPayrollGrp = $('#PayrollGroup').val();        
    let code = $('#Code').val();//bankcode
    let type = $('#Type').val();
    if (TableLength > 0) {
        url = "/PayRoll/SalaryReconciliationReport/ExportToExcelPopUp";
        window.location.href = url + "?Month=" + selMonth + "&PayrollGroup=" + selPayrollGrp + "&type=" + type + "&BankCode=" + code + "&totRecords=" + TableLength + "&monthTxt=" + $('#Month option:selected').text() + "&PayrollGrpTxt=" + $('#PayrollGrp option:selected').text();
    }
    else {
        swal.fire("No data found to export");
    }
}


