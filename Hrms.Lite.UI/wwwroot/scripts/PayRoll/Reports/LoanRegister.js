


function fillData() {

    debugger;
    var filter1 = $('#BasicFilter').serialize();

    var temp = filter1;
    type = $('#Loans_Code').val();

    if (document.getElementById('r1').checked)
    {
        radioVal = document.getElementById('r1').value;
    }
    else
    {
        radioVal = document.getElementById('r2').value;
    }
    var balanceAmount = 0;
    var principleAmount = 0;
    var topUpAmount = 0;
    var adjAmount = 0;
    var alreadyPaidAmount = 0;
    var monthlyEMI = 0;

    var dataTable = $("#LoanRegisterTable").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/PayRoll/LoanRegister/GetLoanRegisterList",
            "type": "POST",


            "datatype": "json",


            'data':

                function (d) {
                    temp = '';




                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();






                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&type=' + type + '&tab=' + radioVal + '&dummy=dummy';
                    d.filter = temp;


                    return JSON.stringify(d);
                },




            "dataSrc": function (d) {





             
                $('#LoanRegister_Count').val(d.recordsTotal);

                for (var i = 0, ien = d.data.length; i < ien; i++) {
                  
                    d.data[i].employeeName = '<td data-label="Employee Name"><a data-bs-toggle="modal"data-target=".bd-example-modal-lg" type="button"class="btn  btn-sm float-left" onclick="OnEmpNameClick(' + "'" + d.data[i].employeeGI + "'" + ', ' + d.data[i].loanIssueCode + ', ' +"'"+ d.data[i].employeeID +"'"+ ', ' +"'"+ d.data[i].employeeName +"'"+')">' + d.data[i].employeeName + '</a><input type="hidden" id="empID_' + i + '" value="' + d.data[i].employeeID + '"></td><input type="hidden" id="empNAME_' + i + '" value="' + d.data[i].employeeName +'"></td>'

                    

                   
                }
                debugger;
                $("#PrincAmount").html(d.principleAmount);
                $("#TopupAmt").html(d.topUpAmount);
                $("#AdjstAmount").html(d.adjAmount);
                $("#AlrdyPaidAmt").html(d.alreadyPaidAmount);
                $("#balanceAmt").html(d.balanceAmount);
                $("#emiAmount").html(d.monthlyEMI);
                return d.data;
            }

        },

        "columns": [

            { "data": "employeeID" },
            { "data": "employeeName" }, { "data": "location" }, { "data": "division" },
            { "data": "loanSettingsName"},
            { "data": "principleAmount"},
            { "data": "topUpAmount" },
            { "data": "adjAmount"},
            { "data": "monthlyEMI" },
            { "data": "totalTenure" },
            { "data": "alreadyPaidAmount." },
            { "data": "balanceAmount"},
            { "data": "remainingTenure" },
            { "data": "employeeStatus" },
            { "data": "loanStatus"},
           


        ],






        "dom": 'rt<"bottom"><"my-wrapper"<"my-inner2"l><"my-inner1"i><"my-inner3"p>>',

        "serverSide": "true",
        "scrollY": '52vh',
        "sScrollX": "100%",
        "sScrollXInner": "100%",

        "order": [0, "asc"],
        "processing": "true",
        "language": {
            processing: '<img width="100%" src="/images/Loader_img1 (7).gif">',
            emptyTable: '<div id="noDataFound" style="text-align: center;display: flex;align-items: center;justify-content: center;height: 45vh;"><img src="/images/nodata.png" /></div >',

            zeroRecords: '<div id="noDataFound" style="text-align: center;display: flex;align-items: center;justify-content: center;height: 45vh;"><img src="/images/nodata.png" /></div >',

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


    $("#dataTableSearch").on("keyup search input paste cut", function () {
        dataTable.search(this.value).draw();
        $('#noDataFound').css("display", 'none');

    });

    $('.search-slider-inner').click(function () {
        //dataTable.search('').draw();
        $('#dataTableSearch').val('');
        $('#dataTableSearch').focus();
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

















function GetList(sender) {
    debugger;
    radioVal = $(sender).val();
    type = $('#Loans_Code').val();

    if (radioVal !== '' && type !== '') {
        /*showSpinner();*/
        //var url = "/PayRoll/LoanRegister/GetList";
        //$('#LoanRegList').load(url, { Tab: radioVal, type: type }, function () { /*hideSpinner();*/ });
        $.ajax({

            type: 'GET',
            url: '/PayRoll/LoanRegister/GetList',
            dataType: 'html',
            data: { Tab: radioVal, type: type },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }

                else {

                    $('#LoanRegList').html("");
                    $('#LoanRegList').html(result);
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
        return true;
    }
    else {

        return false;
    }
}


function OnEmpNameClick(EmployeeGI, LoanIssueCode,empID,empNAME) {

    debugger;
 
    $.ajax({
        type: 'GET',
        url: "/PayRoll/LoanRegister/GetLoanRegisterDetails?EmployeeGI=" + EmployeeGI + "&LoanIssueCode=" + LoanIssueCode,
        dataType: 'html',

        success: function (Data) {
            if ($($.parseHTML(Data)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {
                var m = $('.bd-example-modal-lg');
                m.find('.modal-body').html('');
                m.find('.modal-body').html(Data);
                $("#empDetails").text(" - ("+empID+","+ empNAME+")");
            }

        },
        error: function (xhr, textStatus, errorThrown) {
            swal.fire(xhr.responseText);

        }
    });

}


function ApplyFilter(mode) {
 
    var search = $('#SearchId').val();
   
    $("#SearchId").val($('#FilterList_Code').val())
    if (mode == 'apply') {
        if (filterApplicable >= 1) {
            fillData();
            CloseSlider('basicFilter');
            $('#filterForm').removeClass('filter-apply');
        }
        else {
            
            fillData();
            CloseSlider('basicFilter');
            $('#filterForm').addClass('filter-apply');
        }
    }
    else {
        
        filterApplicable = 1;
        $('.reset').val('');
        $('#Optional_Filter_EmploymentStatus_Code').val(0);
        $('#filterForm').removeClass('filter-apply');
        fillData();
        
    }
}









function ExportToExcel() {
    debugger;
   
   var type = $('#Loans_Code').val();
    var typeName = $('#Loans_Code option:selected').text();
    var radio = document.querySelector('input[type=radio][name=Radio]:checked');
    var radioVal = radio.value;
   

    var TableLength = $('#LoanRegister_Count').val();
    if (TableLength > 0) {
        url = "/PayRoll/LoanRegister/ExportToExcel?Tab=" + radioVal + "&type=" + type + "&typeName=" + typeName + "&totRecords=" + TableLength;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}

function PopUpExportToExcel(EmpGI,LoanIssueCode) {
    debugger;
   

    var TableLength = $('#LoanRegister_Details_info_Count').val();
    if (TableLength > 0) {
        url = "/PayRoll/LoanRegister/PopUpExportToExcel?EmpGI=" + EmpGI + "&LoanIssueCode=" + LoanIssueCode;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}