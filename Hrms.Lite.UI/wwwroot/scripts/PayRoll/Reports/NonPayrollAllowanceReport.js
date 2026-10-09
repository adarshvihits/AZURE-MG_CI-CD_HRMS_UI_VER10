
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


function GetList() {

    debugger
    var monthCode = $('#PayrollMonth_Code').val();
    showSpinner();
    $.ajax({

        type: 'GET',
        url: '/PayRoll/NonPayrollAllowanceDraftReport/GetList',
        dataType: 'html',
        data: { monthCode: monthCode },
        success: function (result) {
            debugger;
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {
                $('#NonPayrollList').html("");
                $('#NonPayrollList').html(result);
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







function OnGridSettingsClick() {
    debugger;
    var monthCode = $('#PayrollMonth_Code').val();
    if (monthCode == '') {
        debugger;
        swal.fire("Please select Month");
        var element = document.getElementById("myDiv");
        element.style.display = "none";

    }
    else {
        $.ajax({
            type: 'GET',
            url: "/PayRoll/NonPayrollAllowanceDraftReport/GetPopUp",
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



function onAllClick(chkid, tableid) {
    debugger
    if ($('#' + chkid).is(':checked'))
        $('#' + tableid).find('input[type=checkbox]').prop('checked', true);
    else
        $('#' + tableid).find('input[type=checkbox]').prop('checked', false);
}

function checkBoxClick(sender, TableId, checkId) {
    debugger;
    if ($(sender).is(':checked')) {
        if ($('#' + TableId).find('input[type="checkbox"]:checked').length == $('#' + TableId).find('input[type="checkbox"]').length - 1) {

            $('#' + checkId).prop('checked', true)
        }


        else
            $('#' + checkId).prop('checked', false)
    }
    else {
        if ($('#' + TableId).find('input[type="checkbox"]:checked').length == $('#' + TableId).find('input[type="checkbox"]').length) {

            $('#' + checkId).prop('checked', true)
        }
        else
            $('#' + checkId).prop('checked', false)
    }
}

function onApplyClick() {
    debugger;
    var monthCode = $('#PayrollMonth_Code').val();
    console.log($('#non_Payroll_Allowance').serialize());
    $.ajax({

        type: 'POST',

        url: "/PayRoll/NonPayrollAllowanceDraftReport/FilterColumn?monthCode=" + monthCode,


        dataType: 'html',
        data: $('#non_Payroll_Allowance').serialize(),
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



function ExportToExcel() {
    debugger;
    var Month = $('#Item1_PayrollMonth_Code').val();

    var MonthName = $('#Item1_PayrollMonth_Code option:selected').text();

    var TableLength = $('#Report').data('kendoGrid').dataSource.total();
    /*var TableLength = $('#Data_Count').val();*/
    if (TableLength > 0) {
        url = "/PayRoll/NonPayrollAllowanceReport/ExportToExcel?Month=" + Month + "&MonthName=" + MonthName + "&TotRec=" + TableLength;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}