function fillReportList() {
    debugger
    var financialyear = $('#FinancialYear_Code').val();
    var category = $('#Category_Code').val();

    if (validateAll()) {
        $.ajax({
            type: 'POST',
            url: '/TrainingModule/TrnScheduleRpt/GetList',
            cache: true,
            async: false,
            dataType: 'html',
            data: { FinancialYearCode: financialyear, Category: category },
            success: function (Data) {
                $('#LOPListID').html("");
                $('#LOPListID').html(Data);

            },
            error: function (xhr, textStatus, errorThrown) {
                alert(xhr.responseText)
                toastr.error(xhr.responseText);
            }
        });
    }
    else {
        return false;
    }


}



function validateAll() {
    debugger
    var i = 0;

    var financialyear = $('#FinancialYear_Code').val();
    var category = $('#Category_Code').val();

    if (!validateById('#FinancialYear_Code', 'FinancialYear')) {
        i++;
    }
    if (!validateById('#Category_Code', 'Category')) {
        i++;
    }
    debugger

    if (i == 0)
        return true;
    else
        return false;
}

function ExportToExcel() {
    debugger;
    var financialyear = $('#FinancialYear_Code').val();
    var category = 0

    var filter = $("#BasicFilter").serialize();
    var radiomode = $("input[type='radio']:checked").val();
    var TrnParticipantListLength = $('#TrainingCompletionList_Count').val();
    var PARAM = 'data=' + filter;

    if (TrnParticipantListLength > 0) {
        url = '/TrainingModule/TrnScheduleRpt/ExportToExcel?' + "ScheduleCode=" + financialyear + " &radiomode=" + category;
        window.location.href = url;

    }
    else {
        swal.fire("No data found to export");
    }
}