







var com;
var user;
function OnExportTopdfClick(UserCode,CompanyCode) {
    debugger;
    //var viewer = $("#reportViewer3").data("telerik_ReportViewer");
    //viewer.reportSource({
    //    report: viewer.reportSource().report,
    //    parameters: {
    //        //salaryperiod:0,

    //        //  Url: Link+"/api/PaySlipReport/GetPaySlipTestJSON?EmployeeCode=" + parseInt($('#employeeCode_' + index).val()) + "&SalaryPeriod=" + parseInt($('#CurrentSalaryPeriodCode').val())
    //        Url: "http://localhost:5001/api/PaySlipReport/GetPaySlipTestJSON?data=" + $('#BasicFilter').serialize() +"&EmployeeCode=" + parseInt($('#employeeCode_' + index).val()) + "&SalaryPeriod=" + parseInt($('#CurrentSalaryPeriodCode').val())
    //    }

    //});
    //$('#reportViewer3').removeClass('hide');







 
    var PfGroup = parseInt($('#PFGroup').val());
    var Month = parseInt($('#Month').val());
    /*var data = $('#BasicFilter').serialize();*/
    console.log($('#BasicFilter').serialize());
   /* var encodedData = encodeURIComponent(data);*/
    var url = "/PayRoll/PFSummaryReport/GetReportPDF?PFGroup=" + PfGroup + "&Month=" + Month + "&data=" + $('#BasicFilter').serialize() ;
        window.location.href=url;
}










function fillData() {

    debugger;
    var filter1 = $('#BasicFilter').serialize();

    var temp = filter1;
   
    var Month = $('#Month').val();
    var PFGroup = $('#PFGroup').val();

    var dataTable = $("#PFsummaryreportTable").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/PayRoll/PFSummaryReport/GetPFSummaryReportList",
            "type": "POST",


            "datatype": "json",


            'data':

                function (d) {
                    temp = '';




                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();


                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&Month=' + Month + '&PFGroup=' + PFGroup + '&dummy=dummy';
                    d.filter = temp;


                    return JSON.stringify(d);
                },




            "dataSrc": function (d) {
                console.log(d);

                debugger;
                $('#PFSummary_Count').val(d.recordsTotal);
                $('#UserBase_Code_User_Code').val(d.userCode);
                $('#UserBase_Code_Company_Code').val(d.comCode);
               
                for (var i = 0, ien = d.data.length; i < ien; i++) {


                    //d.data[i].employeeid = '<div class="d-flex align-items-center"><div class="img-report"><img src="' + d.data[i].absoluteUri + '">  </div> <div class="name-post"><div>  <a href = "/DataBank/EmployeeProfile/GetEmployeeProfileDetails?EmployeeGI=' + d.data[i].employeeGI + '&Breadcrumb=JoiningReport&BreadcrumbName=Joining Report List" >' + d.data[i].employeeName + ' </a > </div> <div> (' + d.data[i].employeeid + ')' + ' </div></div> </div>'

                    EDLIChargeSUM = $("#EdliCharge").html(d.edliChargeSum);
                    AdminChargeSUM = $("#AdminCharge").html(d.adminChargeSum);
                    EPFWageSUM = $("#EpfWage").html(d.epfWageSum);
                    EDLIWageSUM = $("#EdliWage").html(d.edliWageSum);
                    PFSUM = $("#EmployerContri").html(d.pfSum);
                    PensionFundSUM = $("#Pension").html(d.pensionFundSum);
                    EmployeeContributionSUM = $("#EmpContribution").html(d.employeecontributionSum);
                    PFSalarySUM = $("#PfSal").html(d.pfsalarySum);
                    ActualPFSalarySUM = $("#ActualSal").html(d.actualpfsalarySum);

              






                }

                return d.data;
            }

        },

        "columns": [

           
            { "data": "employeeID", "width": "7%"},
            { "data": "employeeName", "width": "9%"},
            { "data": "location", "width": "8%"},
            { "data": "division", "width": "7%"},
            { "data": "pfNumber", "width": "7%"},
            { "data": "uan", "width": "8%"},
            { "data": "actualPFSalary", "width": "7%"}, 
            { "data": "pfSalary", "width": "5%"},
            { "data": "employeeContribution", "width": "6%"},
            { "data": "pensionFund", "width": "7%"},
            { "data": "pf", "width": "7%"},
            { "data": "edliWage", "width": "6%"},
            { "data": "epfWage", "width": "6%"},
            { "data": "adminCharge", "width": "5%"},
            { "data": "edliCharge", "width": "7"},



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




  

















function GetList() {

    debugger
    var Month = $('#Month').val();
    var PFGroup = $('#PFGroup').val();
    /* showSpinner();*/

    if (Month !== '' && PFGroup !== '') {
        //var url = "/PayRoll/PFSummaryReport/GetList?Month=" + Month + "&PFGroup=" + PFGroup;
        //$('#PFSummaryID').load(url, function () { /*hideSpinner();*/ });
        $.ajax({

            type: 'GET',
            url: '/PayRoll/PFSummaryReport/GetList',
            dataType: 'html',
            data: { Month: Month, PFGroup: PFGroup },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }

                else {

                    $('#PFSummaryID').html("");
                    $('#PFSummaryID').html(result);
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


function ExportToExcel() {
    debugger;
    var Month = $('#Month').val();
    var PFGroup = $('#PFGroup').val();
    var MonthName = $('#Month option:selected').text();
    var PFGroupName = $('#PFGroup option:selected').text();

    var TableLength = $('#PFSummary_Count').val();
    if (TableLength > 0) {
        url = "/PayRoll/PFSummaryReport/ExportToExcel?Month=" + Month + "&PFGroup=" + PFGroup + "&MonthName=" + MonthName + "&PFGroupName=" + PFGroupName + "&totRecords=" + TableLength;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}

function ApplyFilter(mode) {
    var tabIndex = $('#Tabindex').val();
    var search = $('#SearchId').val();
    //$("#Tabindex").val(currentTab);
    $("#SearchId").val($('#FilterList_Code').val())
    if (mode == 'apply') {
        if (filterApplicable >= 1) {
            fillData();
            CloseSlider('basicFilter');
            $('#filterForm').removeClass('filter-apply');
        }
        else {
            /*  showSpinner();*/
            fillData();
            CloseSlider('basicFilter');
            $('#filterForm').addClass('filter-apply');
        }
    }
    else {
        /*showSpinner();*/
        filterApplicable = 1;
        $('.reset').val('');
        $('#Optional_Filter_EmploymentStatus_Code').val(0);
        $('#filterForm').removeClass('filter-apply');
        fillData();
        //    CloseSlider('basicFilter');
    }
}