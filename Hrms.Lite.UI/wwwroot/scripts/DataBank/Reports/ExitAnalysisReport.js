function GetList() {
    debugger;
    showSpinner();
    var FromDate = $('#FromDate').val();
    var ToDate = $('#ToDate').val();
    var Type = $('#classification').val();
    debugger;
    $.ajax({
        type: 'GET',
        url: '/Databank/ExitAnalysisReport/GetList?FromDate=' + FromDate + '&ToDate=' + ToDate + '&Type=' + Type,
        dataType: 'html',
        data: $('#BasicFilter').serialize(),
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#ExitAnalysisReport').html("");
                $('#ExitAnalysisReport').html(result);
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


function GetExitAnalysisPopUp(GI) {
    debugger
    var FromDate = $('#FromDate').val();
    var ToDate = $('#ToDate').val();
    var Type = $('#classification').val();
    $.ajax({
        type: 'GET',
        url: "/Databank/ExitAnalysisReport/GetPopUp?GI= " + GI + '&FromDate=' + FromDate + '&ToDate=' + ToDate +'&Type=' + Type,
        dataType: 'html',
        success: function (Data) {
            debugger;
            var m = $('.exitAnalysislistreport');
            m.find('.modal-body').html('');
            m.find('.modal-body').html(Data);
            m.modal('show');
        },
        error: function (xhr, textStatus, errorThrown) {
            swal.fire(xhr.responseText);
        }
    });
}


function ExportToExcelPopUp() {
    debugger;
    //var filter = $('#BasicFilter').serialize();
    //var FromDate = $('#FromDate').val();
    //var ToDate = $('#ToDate').val();
    //var PARAM = 'data=' + filter + '&FromDate=' + FromDate + '&ToDate=' + ToDate;
    var TableLength = $('#ExitAnalysisReportList_Count').val();
    if (TableLength > 0) {
        url = '/DataBank/ExitAnalysisReport/ExportToExcel?' +  "&TotalRows=" + TableLength;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}