



function ExportToExcel() {
    debugger;
    var salaryperiod = $('#PayrollMonth_Code').val();
    $('#PayrollMonth_Name').val($('#PayrollMonth_Code option:selected').text())
    if (validateById("#PayrollMonth_Code", "paysheet_month")) {
       
        $.ajax({

            type: 'POST',
            url: '/PayRoll/PaySheetDraft/CheckDataCount',
            data: $('#PaySheetDraftExporttoExcel').serialize(),
            success: function (result) {
                debugger;
                if (result > 0) {
                    $('#PaySheetDraftExporttoExcel').submit();
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