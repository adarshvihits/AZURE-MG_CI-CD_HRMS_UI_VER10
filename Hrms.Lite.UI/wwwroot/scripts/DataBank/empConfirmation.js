function ExportToExcel() {

    var tableLength = $('#EmpConfirmationList_Count').val();
    if (tableLength > 0) {
        url = "/DataBank/EmpConfirmation/ExportToExcel?totRecords=" + tableLength ;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }

}