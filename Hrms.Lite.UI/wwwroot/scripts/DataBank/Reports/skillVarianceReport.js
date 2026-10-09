function exportToexcel() {
    debugger;
    var grid = $("#skillRequisitionGrid").data("kendoGrid");
    var dataSource = grid.dataSource;

    //records on current view / page  
    var recordsOnCurrentView = dataSource.view().length;
    //var CancellationTable = $('#VacancyStatusReport_Count').val();
    ///* var CancellationTable = document.getElementById("CancellationListTable").rows.length - 1;*/

    if (recordsOnCurrentView > 0) {
    url = "/DataBank/SkillVarianceReport/ExportToExcel";
        window.location.href = url;

    }
    else {
        swal.fire("No data found to export");
    }
}