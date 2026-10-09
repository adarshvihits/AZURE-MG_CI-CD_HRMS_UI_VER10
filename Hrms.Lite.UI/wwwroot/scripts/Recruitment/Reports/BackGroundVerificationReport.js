function ExportToExcel() {

    debugger;
    
    var tableLength = $('#BackGroundVerificationReport_List_Count').val();

    if (tableLength > 0) {
        url = '/Recruitment/BackgroundVerificationReport/ExportToExcel?totalRecords=' + tableLength;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }

}
