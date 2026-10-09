function ExportToExcel() {
    debugger;
    if (validateById("#PayrollMonth_Code", "pay_month")) {

        $.ajax({

            type: 'POST',
            url: '/PayRoll/CostPaySheet/CheckDataCount',
            data: $('#CostPaySheetExporttoExcel').serialize(),
            success: function (result) {
                debugger;
                if (result > 0) {
                    $('#CostPaySheetExporttoExcel').submit();
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