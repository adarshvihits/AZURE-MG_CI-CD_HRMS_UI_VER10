function GetList() {
    debugger;
    var MonthCode = $('#PayrollMonth_Code').val();
    
    $.ajax({

        type: 'GET',
        url: '/PayRoll/ReferralIncentiveReport/GetList',
        dataType: 'html',
        data: { MonthCode: MonthCode },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#ListView').html("");
                $('#ListView').html(result);
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

    var tableLength = $('#List_Count').val();
    if (tableLength > 0) {
        var MonthCode = $('#PayrollMonth_Code').val();
        url = "/PayRoll/ReferralIncentiveReport/ExportToExcel" + '?MonthCode=' + MonthCode;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}