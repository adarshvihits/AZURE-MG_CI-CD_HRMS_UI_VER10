function ExportToExcel() {

    var tableLength = $('#EmployeeTransferList_Count').val();
    if (tableLength > 0) {
        url = "/DataBank/EmployeeTransfer/ExportToExcel?totRecords=" + tableLength;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }

}