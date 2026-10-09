function ExportToExcel() {
    debugger;
    var payrollMonthValue = $('#PayrollMonth_Code').val();
    var MonthName = $('#PayrollMonth_Code option:selected').text();
    if (validateById("#PayrollMonth_Code", "pay_month")) {
      
        
        $.ajax({

            type: 'POST',
            url: '/PayRoll/PaySheet/CheckDataCount',
            data: $('#PaySheetExporttoExcel').serialize() + "&PayrollMonth=" + MonthName,
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