
function onAssessmentYearChange(sender, id) {
    debugger;



    $.getJSON("/TDS/TDSProcessing/GetMonthDropDown", { AssessmentYearCode: $(sender).val() }, function (data) {
        $('#PayrollMonth_Code option').remove();


        for (var i = 0; i < data.length; i++) {
            $('#PayrollMonth_Code').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
        }
        getList();
    });

}

function onProcessClick() {
    debugger
   
    var i = 0;
    if (!validateById("#AssessmentYear_Code", 'Assessmnt_year'))
        i++;
    if (!validateById("#PayrollMonth_Code", 'month'))
        i++;
    if (!validateById("#PayrollGroup_Code", 'payrollgp'))
        i++;
    if (i == 0) {
        $.ajax({
            type: 'POST',
            url: '/TDS/TDSProcessing/Processing',
            dataType: 'json',
            data: $('#TDS_PROCESS').serialize(),
            success: function (result) {
              
                debugger;
                if (result.success == true) {
                    
                    swal.fire(result.message).then(function () {
                 
                        window.location.href = '/TDS/TDSProcessing/TDSConfirmDetails?AssessmentYearCode=' + $("#AssessmentYear_Code").val() + '&AssessmentYear=' + $("#AssessmentYear_Code option:selected").text() + '&SalaryPeriodCode=' + $("#PayrollMonth_Code").val()+' &SalaryPeriod=' + $("#PayrollMonth_Code option:selected").text()+'&PayrollGroupCode=' + $("#PayrollGroup_Code").val()+' &PayrollGroup= ' +$("#PayrollGroup_Code option:selected").text()+'';
                    

                    });
                }
              
            },
           
            error: function (xhr, textStatus, errorThrown) {
                debugger
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);

            }
        });
    }

    else
        return false;
}
function onTabNameClick(TabName) {
    if (TabName == "Selection") {

        $("#tds_DetailsList").addClass('hide');
        $("#tds_selection").removeClass('hide');

    } 
}
function TDSDetailRptExportToExcel() {
    var AssessmentYearCode = $("#AssessmentYear_Code").val();
    var SalaryPeriodCode = $("#PayrollMonth_Code").val();
    var MonthName = $("#PayrollMonth_Name").val();
    var AssessmentYear = $("#AssessmentYear_Name").val();
    var PayrollGroupCode = $("#PayrollGroup_Code").val();
    var PayrollGroup = $("#PayrollGroup_Name").val();
    var TableLength = $('#TDSConfirmationDetails_Count').val();
    
    if (TableLength > 0) {
        url = "/TDS/TDSProcessing/TDSDetailRptExportToExcel?AssessmentYearCode=" + AssessmentYearCode + "&AssessmentYear=" + AssessmentYear + "&SalaryPeriodCode=" + SalaryPeriodCode + "&PayrollGroupCode=" + PayrollGroupCode + "&PayrollGroup=" + PayrollGroup + "&SalaryPeriod=" + MonthName;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}
//function TDSSummaryRptExportToExcel() {
//    var AssessmentYearCode = $("#AssessmentYear_Code").val();
//    var SalaryPeriodCode = $("#PayrollMonth_Code").val();
//    var PayrollGroup = $("#PayrollGroup_Code").val();
//    var PayrollGroupName = $("#PayrollGroup_Code option:selected").text();
//    var MonthName = $("#PayrollMonth_Code option:selected").text();
//    var AssessmentYear = $("#AssessmentYear_Code option:selected").text();

//    if (TableLength > 0) {
//        url = "/TDS/TDSProcessing/TDSSummaryRptExportToExcel?AssessmentYearCode=" + AssessmentYearCode + "&Payroll=" + PayrollGroup + "&Month=" + SalaryPeriodCode + "&PayrollGroupName=" + PayrollGroupName + "&MonthName=" + MonthName + "&AssessmentYear=" + AssessmentYear;
//        window.location.href = url;
//    }
//    else {
//        swal.fire("No data found to export");
//    }
//}


function GetEmpPopUpDetails(EmployeeGI, Month) {
   
    $.ajax({
        type: 'GET',
        url: '/TDS/TDSProcessing/GetEmployeeDetails',
        dataType: 'html',
        data: { EmployeeGI: EmployeeGI, SalaryPeriod: Month},
        success: function (Data) {
            var m = $('.bd-example-modal-lg');
            m.find('.modal-body').html('');
            m.find('.modal-body').html(Data);
        },
        error: function (xhr, textStatus, errorThrown) {
            swal.fire(xhr.responseText);

        }
    });
}
function EmpExportToExcelPopUp(EmployeeGI) {

    var AssessmentYearCode = $("#AssessmentYear_Code").val();
    var SalaryPeriodCode = $("#PayrollMonth_Code").val();
    var PayrollGroupName = $("#PayrollGroup_Code option:selected").text();
    var MonthName = $("#PayrollMonth_Code option:selected").text();
    var AssessmentYear = $("#AssessmentYear_Code option:selected").text();
    
  
    var TableLength = $('#TDSComponentSplitUp_PopUp_Count').val();
    if (TableLength > 0) {
        url = "/TDS/TDSProcessing/TDSComponentEmpPopUpEE?EmployeeGI=" + EmployeeGI + " &AssessmentYearCode=" + AssessmentYearCode + " &SalaryPeriodCode=" + SalaryPeriodCode + " &MonthName=" + MonthName ;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}
function TDSSummaryExportToExcel() {
    var AssessmentYearCode = $("#AssessmentYear_Code").val();
    var SalaryPeriodCode = $("#PayrollMonth_Code").val();
  
    var MonthName = $("#PayrollMonth_Name").val();
    var AssessmentYear = $("#AssessmentYear_Name").val();
    var PayrollGroupCode = $("#PayrollGroup_Code").val();
    var PayrollGroup = $("#PayrollGroup_Name").val();

    var TableLength = $('#TDSConfirmationDetails_Count').val();
    if (TableLength > 0) {
        url = "/TDS/TDSProcessing/TDSSummaryExportToExcel?AssessmentYearCode=" + AssessmentYearCode + " &AssessmentYear=" + AssessmentYear + " &SalaryPeriodCode=" + SalaryPeriodCode + "&PayrollGroupCode=" + PayrollGroupCode + "&PayrollGroup=" + PayrollGroup + " &SalaryPeriod=" + MonthName + " &Totalrow=" + TableLength;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}
function TDSConfirmationSave() {
    var table = $('#TDSTable').DataTable();
    var SalaryPeriodCode = $("#PayrollMonth_Code").val();
    var data = table.$('input,select,textarea').serializeArray();
    data.push({ name: 'PayrollMonth.Code', value: SalaryPeriodCode });
    swal.fire({
        text: "Do you want to Apply?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {

            $.ajax({

                type: 'POST',
                url: "/TDS/TDSProcessing/TDSConfirmation",
                dataType: 'json',
                data: data,
                success: function (result) {
                    if (result.success == true) {
                        swal.fire("Confirmed Successfully").then(function () {
                            window.location.href = '/TDS/TDSProcessing/TDSConfirmDetails?AssessmentYearCode=' + $("#AssessmentYear_Code").val() + '&AssessmentYear=' + $("#AssessmentYear_Code option:selected").text() + '&SalaryPeriodCode=' + $("#PayrollMonth_Code").val() + ' &SalaryPeriod=' + $("#PayrollMonth_Code option:selected").text() + '&PayrollGroupCode=' + $("#PayrollGroup_Code").val() + ' &PayrollGroup= ' + $("#PayrollGroup_Code option:selected").text() + '';

                        });

                    }

                    else {
                        swal.fire(result.message).then(function () {

                        });
                    }

                },
                error: function (xhr, textStatus, errorThrown) {
                    document.body.id = "kt_body";
                    $('body').html(xhr.responseText);
                }
            });

        }
        else {
            return false;
        }
    })


}


//-----------------tds report
function gettdsReport() {
    debugger;
    var filter1 = $('#BasicFilter').serialize();
    var temp = filter1;
    var Month = $('#PayrollMonth_Code').val();
 
    var dataTable = $("#TDSReportTable").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/TDS/TDSReport/GetList",
            "type": "POST",


            "datatype": "json",


            'data':

                function (d) {
                    temp = '';




                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();




                  

                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&Month=' + Month + '&dummy=dummy';
                    d.filter = temp;


                    return JSON.stringify(d);
                },




            "dataSrc": function (d) {




                $('#TDSConfirmationDetails_Count').val(d.recordsTotal);
                for (var i = 0, ien = d.data.length; i < ien; i++) {

                    d.data[i].employeeHeader.employeeName = '<a class="" data-bs-toggle="modal" data-target=".bd-example-modal-lg" onclick="GetEmpPopUpDetails(' + "'" + d.data[i].employeeHeader.employeeGuid + "'" + ', ' + Month + ')">' + d.data[i].employeeHeader.employeeName + '' + '' + '</a>'
                    d.data[i].doj = _GetFormattedDate(d.data[i].doj);

                }

                return d.data;
            }

        },

        "columns": [

            { "data": "employeeHeader.employeeId" },
            { "data": "employeeHeader.employeeName" },
            { "data": "regime" },
            { "data": "processedSalary" },
            { "data": "projectedSalary" },
            { "data": "grossSalary" },
            { "data": "totExemption" },
            //{ "data": "professionalTax" },
            //{ "data": "otherIncome" },
            { "data": "eightyC" },
            { "data": "sixA" },
            { "data": "taxableIncome" },
            
            { "data": "tax" },
            { "data": "rebate" },
            { "data": "surcharge" },
            { "data": "eduCess" },
            { "data": "totalTax" },
            { "data": "taxPaid" },
            { "data": "balTax" },
            { "data": "remainingMonths" },
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
function ApplyFilter(mode) {

    var tabIndex = $('#Tabindex').val();
    var search = $('#SearchId').val();
    //$("#Tabindex").val(currentTab);
    $("#SearchId").val($('#FilterList_Code').val())
    if (mode == 'apply') {
        if (filterApplicable >= 1) {
            gettdsReport();
            CloseSlider('basicFilter');
            $('#filterForm').removeClass('filter-apply');
        }
        else {
            /*  showSpinner();*/
            gettdsReport();
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
        gettdsReport();
        //    CloseSlider('basicFilter');
    }
}
function TDSReportExportToExcel() {
    var MonthName = $("#PayrollMonth_Code option:selected").text();
    var SalaryPeriodCode = $("#PayrollMonth_Code").val();


    var TableLength = $('#TDSConfirmationDetails_Count').val();
    if (TableLength > 0) {
        url = "/TDS/TDSReport/TDSReportExportToExcel?SalaryPeriodCode=" + SalaryPeriodCode + " &Totalrow=" + TableLength + " &MonthName=" + MonthName;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}

