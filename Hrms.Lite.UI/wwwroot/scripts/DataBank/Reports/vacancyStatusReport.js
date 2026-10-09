//----------Vacancy Tracking - period / branch filters--------------------

// true when the row's created date falls in the selected period
function isInVacancyPeriod(created, period) {
    if (period == 'all')
        return true;
    if (!created)
        return false;

    var date = new Date(created + 'T00:00:00');
    var today = new Date();
    var from;
    if (period == 'month')
        from = new Date(today.getFullYear(), today.getMonth(), 1);
    else if (period == 'year')
        from = new Date(today.getFullYear(), 0, 1);
    else
        from = new Date(today.getFullYear(), today.getMonth() - parseInt(period), today.getDate());
    return date >= from;
}

$.fn.dataTable.ext.search.push(function (settings, data, dataIndex) {
    if (settings.nTable.id != 'JoiningReportTable')
        return true;

    var row = $(settings.aoData[dataIndex].nTr);
    var period = $('#VacancyPeriodFilter').val() || 'all';
    var branch = $('#VacancyBranchFilter').val() || '';

    return isInVacancyPeriod(row.attr('data-created'), period)
        && (branch == '' || row.attr('data-branch') == branch);
});

function filterVacancyTracking() {
    $('#JoiningReportTable').DataTable().draw();
}

function ExportToExcel() {

    debugger

    var tableLength = $('#VacancyStatusReport_Count').val();
    if (tableLength > 0) {        
        url = "/DataBank/VacancyStatusReport/ExportToExcelDataset";
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }

}