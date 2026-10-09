function GetList() {
    showSpinner();
    $.ajax({
        type: 'GET',
        url: '/PayRoll/BonusReport/GetList?MonthCode=' + $('#ProcessMonth').val() + '&BonusGrp=' + $('#BonusGroup').val() + '&Type=' + $('input[name="AmountType"]:checked').val(),
        dataType: 'html',
        data: $('#BasicFilter').serialize(),
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

function ApplyFilter(mode) {
    debugger;


    if (mode == 'apply') {
        if (filterApplicable >= 1) {
            CloseSlider('basicFilter');
            GetList();
            $('#filterForm').removeClass('filter-apply');
        }
        else {
            CloseSlider('basicFilter');
            GetList();
            $('#filterForm').addClass('filter-apply');
        }

    }
    else {
        filterApplicable = 1;
        $('.reset').val('');
        $('#filterForm').removeClass('filter-apply');
        GetList();


    }
}

function ExportToExcel() {
   
    //var tableLength = $('#JoiningReportList_Count').val();
    //if (tableLength > 0) {
        //url = "/DataBank/JoiningReport/ExportToExcel?fromDate=" + fromDate + "&toDate=" + toDate + "&totRecords=" + tableLength;
    url = '/PayRoll/BonusReport/ExportToExcel?MonthCode=' + $('#ProcessMonth').val() + '&BonusGrp=' + $('#BonusGroup').val() + '&Type=' + $('input[name="AmountType"]:checked').val() + 'data=' + $('#BasicFilter').serialize()
        window.location.href = url;
    //}
    //else {
    //    swal.fire("No data found to export");
    //}

}