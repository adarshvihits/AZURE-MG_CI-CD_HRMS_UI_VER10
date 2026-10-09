function onAlwDataBound(e) {
    var grid = this;

    if ($('#alwCodes').val() != '0') {
        var selectedSkillCodes = $('#alwCodes').val().split(',');


        grid.tbody.find('tr').each(function myfunction() {
            var row = $(this);
            var dataItem = grid.dataItem(row);

            for (var i = 0; i < selectedSkillCodes.length; i++) {
                if (selectedSkillCodes[i] == dataItem.AllowanceCode) {

                    row.addClass("k-selected")
                    row.find("input").attr("aria-label", "Deselect row");
                    row.find("input").attr("aria-checked", true);
                    row.find(".k-checkbox").prop("checked", true);

                    break;
                }
            }


        });
    }


}

function onDedDataBound(e) {
    var grid = this;

    if ($('#dedCodes').val() != '0') {
        var selectedSkillCodes = $('#dedCodes').val().split(',');

      

        grid.tbody.find('tr').each(function myfunction() {
            var row = $(this);
            var dataItem = grid.dataItem(row);

            for (var i = 0; i < selectedSkillCodes.length; i++) {
                if (selectedSkillCodes[i] == dataItem.DeductionCode) {

                    row.addClass("k-selected")
                    row.find("input").attr("aria-label", "Deselect row");
                    row.find("input").attr("aria-checked", true);
                    row.find(".k-checkbox").prop("checked", true);

                    break;
                }
            }


        });
    }


}


function OnSalaryPeriodClick() {

    debugger
    var salCode = $('#PayrollMonth_Code').val();
    showSpinner();

    $.ajax({

        type: 'GET',
        url: '/PayRoll/SalaryDetailedReport/GetList',
        dataType: 'html',
        data: { salCode: salCode },
        success: function (result) {
            debugger;
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {
                $('#salaryReportList').html("");
                $('#salaryReportList').html(result);
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

function OnGridSettingsClick(SalaryPeriodCode) {
    debugger;
    var SalPeriod = $('#PayrollMonth_Code').val();
    if (SalPeriod == '') {
        debugger;
        swal.fire("Please select Salary Period");
        var element = document.getElementById("myDiv");
        element.style.display = "none";

    }
    else {
        $.ajax({
            type: 'GET',
            url: "/PayRoll/SalaryDetailedReport/GetPopUp",
            dataType: 'html',

            success: function (Data) {
               
                    var m = $('.bd-example-modal-lg');
                    m.find('.modal-body').html('');
                    m.find('.modal-body').html(Data);
                    m.modal('show');
               
            },
            error: function (xhr, textStatus, errorThrown) {
                swal.fire(xhr.responseText);

            }
        });
    }
}

function ExportToExcel() {

    debugger;
    var SalPeriod = $('#Item1_PayrollMonth_Code').val();
    var SalPeriodName = $('#Item1_PayrollMonth_Code option:selected').text();
    var alw = $('#alwCodes').val();
    var ded = $('#dedCodes').val();
    //var tableLength = $('#Data_Count').val();
    var tableLength = $('#Report').data('kendoGrid').dataSource.total();
    
    if (tableLength > 0) {
        url = "/PayRoll/SalaryDetailedReport/ExportToExcel?SalPeriod=" + SalPeriod + "&SalPeriodName=" + SalPeriodName + "&allowance=" + alw + "&deduction=" + ded + "&Total=" + tableLength;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }

}
function OnCheckBoxAllClick(colchkid, tableid) {
    debugger
    if ($('#' + colchkid).is(':checked'))
        $('#' + tableid).find('input[type=checkbox]').prop('checked', true);
    else
        $('#' + tableid).find('input[type=checkbox]').prop('checked', false);
}


function OnApplyClick() {
    debugger;
    var salCode = $('#PayrollMonth_Code').val();
   /* showSpinner();*/
    console.log($('#SalaryDetailedpopup').serialize());
    $.ajax({

        type: 'POST',
        
        /* showSpinner();*/
        url:  "/PayRoll/SalaryDetailedReport/FilterColumn?salCode=" + salCode,
     

        dataType: 'html',
        data: $('#SalaryDetailedpopup').serialize(),
        success: function (result) {


            debugger;
            $('.bd-example-modal-lg').modal('hide');


            $('#salaryReportList').html(result);

            hideSpinner();





        },
        error: function (xhr, textStatus, errorThrown) {

            if ($($.parseHTML(xhr.responseText)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);
            }
        }
    });
}



function ApplyFilter(mode) {

    $('#Report').data('kendoGrid').dataSource.read().then(function () {
             
        $('#Report').data('kendoGrid').dataSource.page(1);
    });
    $('#Report').data('kendoGrid').refresh();




    if (mode == 'apply') {

        if (filterApplicable >= 1) {

            CloseSlider('basicFilter');
            $('#filterForm').removeClass('filter-apply');
        }
        else {


            CloseSlider('basicFilter');
            $('#filterForm').addClass('filter-apply');
        }


    }

    else {
        filterApplicable = 1;
        $('.reset').val('');
        $('#Optional_Filter_EmploymentStatus_Code').val(0);
        $('#filterForm').removeClass('filter-apply');

    }


}

function SalaryDetailedRptPrint() {


    var SalPeriod = $('#Item1_PayrollMonth_Code').val();
  
    var alw = $('#alwCodes').val();
    var ded = $('#dedCodes').val();
   
    var tableLength = $('#Report').data('kendoGrid').dataSource.total();

    if (tableLength > 0) {
        url = "/PayRoll/SalaryDetailedReport/GetSalaryDetailedRptPrint?SalaryPeriod=" + SalPeriod + "&Allowance=" + alw + "&Deduction=" + ded;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }

}

//function ApplyFilter(mode) {
//    debugger
//    var SalPeriod = $('#PayrollMonth_Code').val();
 
//    var search = $('#SearchId').val();
  
//    $("#SearchId").val($('#FilterCombo_Code').val())
//    if (mode == 'apply') {
//        if (filterApplicable >= 1) {
//            $.ajax({

//                type: 'POST',
//                url: '/PayRoll/SalaryDetailedReport/ApplyFilter',
//                dataType: 'html',
//                data: $('#BasicFilter').serialize() + "&SalPeriod=" + SalPeriod,
//                success: function (result) {
//                    debugger
//                    CloseSlider('basicFilter');
//                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
//                        window.location.reload();
//                    }
//                    else {
//                        $('#salaryReportList').html("");
//                        $('#salaryReportList').html(result);
//                        $('#salaryReportList').removeClass('hide');
//                    }
//                    hideSpinner();

//                },


//                error: function (xhr, textStatus, errorThrown) {
//                    document.body.id = "kt_body";
//                    $('body').html(xhr.responseText);
//                }
//            });
//            $('#filterForm').removeClass('filter-apply');
//        }
//        else {
//            showSpinner();
//            $.ajax({

//                type: 'POST',
//                url: '/PayRoll/SalaryDetailedReport/ApplyFilter',
//                dataType: 'html',
//                data: $('#BasicFilter').serialize() + "&SalPeriod=" + SalPeriod,
//                success: function (result) {
//                    debugger
//                    CloseSlider('basicFilter');
//                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
//                        window.location.reload();
//                    }
//                    else {
//                        $('#salaryReportList').html("");
//                        $('#salaryReportList').html(result);
//                        $('#salaryReportList').removeClass('hide');
//                    }
//                    hideSpinner();
//                },


//                error: function (xhr, textStatus, errorThrown) {
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
//        $('.reset').val('');
//        $('#Optional_Filter_EmploymentStatus_Code').val(0);
//        $('#filterForm').removeClass('filter-apply');
//        $.ajax({

//            type: 'POST',
//            url: '/PayRoll/SalaryDetailedReport/ApplyFilter',
//            dataType: 'html',
//            data: $('#BasicFilter').serialize() + "&SalPeriod=" + SalPeriod,
//            success: function (result) {
//                debugger
//                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
//                    window.location.reload();
//                }
//                else {
//                    $('#salaryReportList').html("");
//                    $('#salaryReportList').html(result);
//                    $('#salaryReportList').removeClass('hide');
//                }
//                hideSpinner();
//            },


//            error: function (xhr, textStatus, errorThrown) {
//                document.body.id = "kt_body";
//                $('body').html(xhr.responseText);
//            }
//        });
//    }
//}