
function GetToMonthDropdown(sender) {
    debugger
    $.getJSON("/PayRoll/PeriodWiseDeductionReport/GetToMonth", { code: $(sender).val() }, function (data) {

        $('#PayrollMonthTO_Code option').remove();
        $('#PayrollMonthTO_Code').append('<option value="">--Select--</option');

        for (var i = 0; i < data.length; i++) {
            $('#PayrollMonthTO_Code').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
        }
    
    });
}
function ExportToExcel() {
    debugger;
    var payrollMonthValue = $('#PayrollMonth_Code').val();
    var MonthFROMName = $('#PayrollMonthFROM_Code option:selected').text();
    var MonthTOName = $('#PayrollMonthTO_Code option:selected').text();

    if (validateById("#PayrollMonthFROM_Code", "pay_month_FROM")) {

        if (validateById("#PayrollMonthTO_Code", "pay_month_TO")) {


            $.ajax({

                type: 'POST',
                url: '/PayRoll/PaySheetPeriodWise/CheckDataCount',
                data: $('#PaySheetExporttoExcel').serialize() + "&PayrollMonthFROM=" + MonthFROMName + "&PayrollMonthTO=" + MonthTOName,
                success: function (result) {
                    debugger;
                    if (result > 0) {
                        $('#PaySheetExporttoExcel').submit();
                    }
                    else {
                        swal.fire("No data found");
                    }
                },
                error: function (xhr, textStatus, errorThrown) {
                    debugger;
                    document.body.id = "kt_body";
                    $('body').html(xhr.responseText);
                }
            });
        }
        else {
            return false;
        }


    }
    else {
        return false;
    }

}