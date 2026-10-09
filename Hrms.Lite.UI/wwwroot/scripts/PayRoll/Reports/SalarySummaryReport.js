



function fillData() {

    debugger;
    var filter1 = $('#BasicFilter').serialize();

    var temp = filter1;
    var month = $('#PayrollMonth_Code').val();
   


    /*  $('#JoiningReportTable').html('');*/
    var dataTable = $("#SalarySummaryReportTable").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/PayRoll/SalarySummaryReport/GetSalarySummaryList",
            "type": "POST",


            "datatype": "json",


            'data':

                function (d) {
                    temp = '';




                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();






                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&month=' + month + '&dummy=dummy';
                    d.filter = temp;


                    return JSON.stringify(d);
                },




            "dataSrc": function (d) {


               

                $('#SalarySummary_Count').val(d.recordsTotal);
                for (var i = 0, ien = d.data.length; i < ien; i++) {

                    debugger;
                    //d.data[i].employeeid = '<div class="d-flex align-items-center"><div class="img-report"><img src="' + d.data[i].absoluteUri + '">  </div> <div class="name-post"><div>  <a href = "/DataBank/EmployeeProfile/GetEmployeeProfileDetails?EmployeeGI=' + d.data[i].employeeGI + '&Breadcrumb=JoiningReport&BreadcrumbName=Joining Report List" >' + d.data[i].employeeName + ' </a > </div> <div> (' + d.data[i].employeeid + ')' + ' </div></div> </div>'
                    TotalEmpCount = $("#empcount").html(d.recordsTotal);
                    TotalGrossAllow = $("#grossAlw").html(d.totAllw);
                    TotalGrossDed = $("#grossDed").html(d.toDed);
                    TotalAdvance = $("#toadvnc").html(d.totadvnc);
                    TotalNetPay = $("#totNet").html(d.totnetpay);
                    

                }

                return d.data;
            }

        },

        "columns": [

            { "data": "employeeID", "width": "8%"},
            { "data": "employeeName", "width": "12%" },
            { "data": "designation", "width": "9%"},
            { "data": "department", "width": "8%" },
            { "data": "location", "width": "7%" },
            { "data": "category", "width": "8%"},
            { "data": "payrollGroup", "width": "8%"},
            { "data": "lop", "width": "5%"},
            { "data": "grossAllowance", "width": "8%" },
            { "data": "grossDeduction", "width": "7%" },
            { "data": "advance", "width": "7%"},
            { "data": "netPay", "width": "6%"},
            { "data": "paymentMode", "width": "7%" },


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
        dataTable.search('').draw();
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




function OnMonthClick() {
    debugger;
    var salCode = $('#PayrollMonth_Code').val();
    /* showSpinner();*/
    $.ajax({

        type: 'GET',
        url: '/PayRoll/SalarySummaryReport/GetList',
        dataType: 'html',
        data: { salCode: salCode },
        success: function (result) {
            debugger;
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {
                $('#SalarySummaryList').html("");
                $('#SalarySummaryList').html(result);
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

function onGroupSummaryClick() {
    debugger;
    var SalPeriod = $('#PayrollMonth_Code').val();



    if (SalPeriod == '') {
        debugger;
        swal.fire("Please select Salary Period");
        var element = document.getElementById("myDiv");
        element.style.display = "none";

    }
    else {
        debugger;
        $.ajax({
            type: 'GET',
            url: "/PayRoll/SalarySummaryReport/GetGroupSummary?salCode=" + SalPeriod,
            dataType: 'html',

            success: function (Data) {
                if ($($.parseHTML(Data)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }

                else {
                    var m = $('.bd-example-modal-lg');
                    m.find('.modal-body').html('');
                    m.find('.modal-body').html(Data);
                    $("#myDiv").css("display", "block");
                }
            },
            error: function (xhr, textStatus, errorThrown) {
                swal.fire(xhr.responseText);

            }
        });
    }
}

function groupbyChange(sender) {
    debugger;
    var SalPeriod = $('#PayrollMonth_Code').val();
    var ddid = $(sender).val();
    var gname = $('#GroupByFill_Code option:selected').text();
    //var url = "/PayRoll/SalarySummaryReport/GroupByBasedSummary";
    //$('#SummaryListID').load(url, { id: ddid, groupbyname: gname, salCode: SalPeriod }, function () { });
    $.ajax({

        type: 'GET',
        url: '/PayRoll/SalarySummaryReport/GroupByBasedSummary',
        dataType: 'html',
        data: { id: ddid, groupbyname: gname, salCode: SalPeriod },
        success: function (result) {
            debugger;
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {
                $('#SummaryListID').html("");
                $('#SummaryListID').html(result);
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


function ExportToExcel() {

    debugger;
    var SalPeriod = $('#PayrollMonth_Code').val();
    var SalPeriodName = $('#PayrollMonth_Code option:selected').text();

    var tableLength = $('#SalarySummary_Count').val();
    if (tableLength > 0) {
        url = "/PayRoll/SalarySummaryReport/ExportToExcel?SalPeriod=" + SalPeriod + "&SalPeriodName=" + SalPeriodName + "&totRecords=" + tableLength;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }

}
function GroupSummaryExportToExcel(TotalEmpCount,TotalGrossAllow)
{


    debugger;
    var SalPeriod = $('#PayrollMonth_Code').val();
    var SalPeriodName = $('#PayrollMonth_Code option:selected').text();
    var id = $('#GroupByFill_Code').val();
    var type = $('#GroupByFill_Code option:selected').text();
    var tableLength = document.getElementById("summaryListTableID").rows.length - 1;
    if (tableLength > 0) {
        url = "/PayRoll/SalarySummaryReport/GroupSummaryExportToExcel?id=" + id + "&SalPeriod=" + SalPeriod + "&SalPeriodName=" + SalPeriodName + "&type=" + type + "&TotalEmpCount=" + TotalEmpCount + "&TotalGrossAllow=" + TotalGrossAllow;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }



}



function ApplyFilter(mode) {
    debugger

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

function ValidateGrpSummaryRpt() {

    var i = 0;

    if (!validateById("#PayrollMonth_Code", "pay_month")) {
        i++;
    }
    if (!validateById("#GroupByFill_Code", "group_by")) {
        i++;
    }
   

    if (i == 0)
        return true;
    else
        return false;
}






//function ApplyFilter(mode) {
//    debugger
//    var SalPeriod = $('#PayrollMonth_Code').val();

//    if (mode == 'apply') {
       
//        if (filterApplicable >= 1) {
//            $.ajax({

//                type: 'POST',
//                url: '/PayRoll/SalarySummaryReport/ApplyFilter',
//                dataType: 'html',
//                data: $('#BasicFilter').serialize(),
//                success: function (result) {
//                    debugger
//                    CloseSlider('basicFilter');
//                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
//                        window.location.reload();
//                    }

//                    else {
//                        $('#SalarySummaryList').html("");
//                        $('#SalarySummaryList').html(result);

//                        $('#SalarySummaryList').removeClass('hide');

                        
//                    }
//                    hideSpinner();
//                },

//                error: function (xhr, textStatus, errorThrown) {
//                    hideSpinner();
//                    document.body.id = "kt_body";
//                    $('body').html(xhr.responseText);
//                }
//            });
//            $('#filterForm').removeClass('filter-apply');
//        }
//        else {
//            $.ajax({

//                type: 'POST',
//                url: '/PayRoll/SalarySummaryReport/ApplyFilter',
//                dataType: 'html',
//                data: $('#BasicFilter').serialize(),
//                success: function (result) {
//                    debugger
//                    CloseSlider('basicFilter');
//                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
//                        window.location.reload();
//                    }

//                    else {
//                        $('#SalarySummaryList').html("");
//                        $('#SalarySummaryList').html(result);

//                        $('#SalarySummaryList').removeClass('hide');
//                    }
                  
//                    hideSpinner();
//                },

//                error: function (xhr, textStatus, errorThrown) {
//                    hideSpinner();
//                    document.body.id = "kt_body";
//                    $('body').html(xhr.responseText);
//                }
//            });
//            $('#filterForm').addClass('filter-apply');
//        }

//    }
//    else {
//        showSpinner();
//        filterApplicable = 1;
//        $('#filterForm').removeClass('filter-apply');
//        $('.reset').val('');
//        $('#Optional_Filter_EmploymentStatus_Code').val(0);
//        $.ajax({
//            type: 'POST',
//            url: '/PayRoll/SalarySummaryReport/ApplyFilter',
//            dataType: 'html',
//            data: $('#BasicFilter').serialize(),
//            success: function (result) {
//                debugger
                
//                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
//                    window.location.reload();
//                }

//                else {
//                    $('#SalarySummaryList').html("");
//                    $('#SalarySummaryList').html(result);

//                    $('#SalarySummaryList').removeClass('hide');
                    
//                }
//                hideSpinner();
//            },
//            error: function (xhr, textStatus, errorThrown) {
//                hideSpinner();
//                document.body.id = "kt_body";
//                $('body').html(xhr.responseText);
//            }
//        });
//    }
//}