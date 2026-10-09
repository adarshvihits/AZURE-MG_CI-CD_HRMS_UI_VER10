function ExportToExcel() {
    debugger;
    if (validateById("#PayrollMonth_Code", "pay_month")) {

        $.ajax({

            type: 'POST',
            url: '/PayRoll/MonthlyCTCReport/CheckDataCount',
            data: $('#CtcRptExporttoExcel').serialize(),
            success: function (result) {
                debugger;
                if (result > 0) {
                    $('#CtcRptExporttoExcel').submit();
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