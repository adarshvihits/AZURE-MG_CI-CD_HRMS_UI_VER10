function searchRows() {
    debugger
    var input, filter, table, tr, td, i;
    var img = "<img src='/images/nodata.png' />";

    input = document.getElementById("searchList");
    filter = input.value.toUpperCase();
    table = document.getElementById("salryReconciliationTable");
    tr = table.getElementsByTagName("tr");
    tbody = table.getElementsByTagName("tbody");
    $('#noDataImg').remove();
    var noOfRows = tr.length;
    var show = true;
    var spannedRows = 0;
    for (i = 0; i < tr.length; i++) {
        debugger;
        td = tr[i].getElementsByTagName("td")[0];
        if (td) {
            txtValue = td.textContent || td.innerText;
            if (txtValue.toUpperCase().indexOf(filter) > -1) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
                noOfRows--;
            }
        }       
    }

    
    //alert(noOfRows);
    if (noOfRows == 1) {
        // $(table).find(tbody).append("<div  id='noDataImg' style='text-align:center; display:flex; align-items:center; justify-content:center;height:45vh;'><img src='/images/nodata.png'/></div >");
        $(table).find(tbody).append("<tr id='noDataImg' class='exit-page'><td colspan=8><div  style='text-align:center; display:flex; align-items:center; justify-content:center;height:45vh;'><img src='/images/nodata.png'/></div ></td></tr>");
        // $(table).find(tbody).append("<tr id='noDataImg'><td>" + img + "</td></tr>");
    }
}



function validateMonths() {
    debugger;
    var prevMonth = $('#SelectedMonth_Code').val();
    var currentMonth = $('#CompareWith_Code').val();
    if (prevMonth == currentMonth) {
        swal.fire("Please select another month for comparison").then(function () {
            debugger
            url = '/PayRoll/SalaryReconciliation/Index';
            window.location.href = url;
        });
           
        //$('#SelectedMonth_Code').val("");
        //$('#CompareWith_Code').val("");
       
        
    }
    else
        return true;
}


function Proceed() {
    debugger;
    var selMonth = $('#SelectedMonth_Code').val();
    var compareWith = $('#CompareWith_Code').val();
    var st = $('#Status_Code').val();
    var emp = $('#Employee_Code').val();
    var payrollGrp = $('#PayrollGroup').val();
    if (validateDropdowns())
    {

    
    $.ajax({

        type: 'GET',
        url: '/PayRoll/SalaryReconciliation/GetList',
        dataType: 'html',
        data: { month: selMonth, compareWith: compareWith, Status: st, EmployeeCode: emp, PayrollGroup: payrollGrp },
        success: function (result) {
            debugger;
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

function onStatusChange() {
    var stCode = $('#Status_Code').val();
    $.getJSON("/PayRoll/SalaryReconciliation/GetEmployee", { id: stCode }, function (data) {

        $('#Employee_Code option').remove();
        $('#Employee_Code').append('<option value="">--Select--</option');

        for (var i = 0; i < data.length; i++) {
            $('#Employee_Code').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
        }

    });
}

function onCurrentMonthChange() {
    var SelMonthCode = $('#CompareWith_Code').val();
    $.getJSON("/PayRoll/SalaryReconciliation/GetPrevMonth", { Month: SelMonthCode }, function (data) {

        $('#SelectedMonth_Code option').remove();
        $('#SelectedMonth_Code').append('<option value="">--Select Previous Month--</option');

        for (var i = 0; i < data.length; i++) {
            $('#SelectedMonth_Code').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
        }
        $('#SelectedMonth_Code').val(data[0].value);
    });
}

function onDescriptionClick(typeCode,type) {
    debugger;
    var selMonth = $('#SelectedMonth_Code').val();
    var compareWith = $('#CompareWith_Code').val();
    var st = $('#Status_Code').val();
    var emp = $('#Employee_Code').val();
    var payrollGrp = $('#PayrollGroup').val();
    $.ajax({

        type: 'GET',
        url: '/PayRoll/SalaryReconciliation/GetSalaryReconciliationEmployeeDetails',
        dataType: 'html',
        data: { month: selMonth, compareWith: compareWith, Status: st, EmployeeCode: emp, typeCode: typeCode, type: type, PayrollGroup: payrollGrp },
        success: function (result) {
            debugger;
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {
                debugger;
                var m = $('.sr-example-modal-lg');
                m.find('.modal-body').html('');
                m.find('.modal-body').html(result);
                m.modal('show');
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


function fillData(typeCode, type) {

    debugger;
    var selMonth = $('#SelectedMonth_Code').val();
    var compareWith = $('#CompareWith_Code').val();
    var st = $('#Status_Code').val();
    var emp = $('#Employee_Code').val();
    var payrollGrp = $('#PayrollGroup').val();
    var Filter = $('#BasicFilter').serialize();

    var temp = Filter;


    var dataTable = $("#SalaryReconciliationTable").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/PayRoll/SalaryReconciliation/GetList",
            "type": "POST",


            "datatype": "json",


            'data':

                function (d) {
                    temp = '';

                    var start = d.start;

                    var searchValue = $('#dataTableSearchPopUp').val();
                    temp = Filter + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&month=' + selMonth + '&compareWith=' + compareWith + '&Status=' + st + '&EmployeeCode=' + emp + '&typeCode=' + typeCode + '&type=' + type + '&PayrollGroup='+ payrollGrp+'&dummy=dummy';
                    d.filter = temp;

                    return JSON.stringify(d);
                },


            "dataSrc": function (d) {

                $('#SalaryReconciliationEmployeeDetails_Count').val(d.recordsTotal);
                $('#Code').val(typeCode);
                $('#Type').val(type);
                return d.data;
            }

        },

        "columns": [

            { "data": "employeeID", "width":"10%" },
            { "data": "employeeName", "width":"15%" },
            { "data": "designation", "width":"15%" },
            { "data": "department", "width":"10%" },
            { "data": "location", "width":"12%" },
            { "data": "previousEarning", "width":"15%" },            
            { "data": "currentEarning", "width":"13%" },
            { "data": "variance", "width":"10%" },            


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
    let code = $('#Code').val();
    let typeCode = $('#Type').val();
        if (mode == 'apply') {

            if (filterApplicable >= 1) {
                fillData(code, typeCode);
                CloseSlider('basicFilter');
                $('#filterForm').removeClass('filter-apply');
            }
            else {
                fillData(code, typeCode);
                CloseSlider('basicFilter');
                $('#filterForm').addClass('filter-apply');
            }

        }
        else {

            filterApplicable = 1;
            $('.reset').val('');
            $('#Optional_Filter_EmploymentStatus_Code').val(0);
            fillData(code, typeCode);
            CloseSlider('basicFilter');
            $('#filterForm').removeClass('filter-apply');
        }
    
}


function ExportToExcel() {
    debugger
    var TableLength = $('#SalaryReconciliationDetails_Count').val();
    /* var TableLength = document.getElementById("vacList").rows.length - 1;*/
    var selMonth = $('#SelectedMonth_Code').val();
    var compareWith = $('#CompareWith_Code').val();
    var st = $('#Status_Code').val();
    var emp = $('#Employee_Code').val();
    var payrollGrp = $('#PayrollGroup').val();
    if (TableLength > 0) {
        url = "/PayRoll/SalaryReconciliation/ExportToExcel";
        window.location.href = url + "?month=" + selMonth + "&compareWith=" + compareWith + "&Status=" + st + "&EmployeeCode=" + emp + "&PayrollGroup=" + payrollGrp + "&monthTxt=" + $('#SelectedMonth_Code option:selected').text() + "&compareWithTxt=" + $('#CompareWith_Code option:selected').text() + "&statusTxt=" + $('#Status_Code option:selected').text();
    }
    else {
        swal.fire("No data found to export");
    }
}

function ExportToExcelPopUp() {
    debugger
    var TableLength = $('#SalaryReconciliationEmployeeDetails_Count').val();
    /* var TableLength = document.getElementById("vacList").rows.length - 1;*/
    var selMonth = $('#SelectedMonth_Code').val();
    var compareWith = $('#CompareWith_Code').val();
    var st = $('#Status_Code').val();
    var emp = $('#Employee_Code').val();
    let code = $('#Code').val();
    let typeCode = $('#Type').val();
    if (TableLength > 0) {
        url = "/PayRoll/SalaryReconciliation/ExportToExcelPopUp";
        window.location.href = url + "?month=" + selMonth + "&compareWith=" + compareWith + "&Status=" + st + "&EmployeeCode=" + emp + "&typeCode=" + code + "&type=" + typeCode + "&totRecords=" + TableLength + "&PayrollGroup=" + payrollGrp+ "&monthTxt=" + $('#SelectedMonth_Code option:selected').text() + "&compareWithTxt=" + $('#CompareWith_Code option:selected').text() + "&statusTxt=" + $('#Status_Code option:selected').text();
    }
    else {
        swal.fire("No data found to export");
    }
}

function validateDropdowns() {
    debugger;
    var i = 0;
    if (!validateById('#SelectedMonth_Code', 'month')) {
        i++;
    }
    if (!validateById('#CompareWith_Code', 'compare')) {
        i++;
    }
    if (!validateById('#Status_Code', 'status')) {
        i++;
    }

    if (i == 0)
        return true;
    else
        return false;

}