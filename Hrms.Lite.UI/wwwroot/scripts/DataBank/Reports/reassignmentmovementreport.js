
function ExportToExcel() {
    var i = 0;

    if (!ValidateDate()) return;

    if (!validateById('#DateFrom', 'fromdate')) i++;
    if (!validateById('#DateTo', 'todate')) i++;

    if (i > 0) return;

    $.ajax({
        url: '/DataBank/ReassignmentMovementReport/CheckExcelData',
        type: 'POST',
        data: $('#reassignmentmovementSave').serialize(),
        success: function (response) {
            if (response.hasData === true) {
                $('#reassignmentmovementSave').submit();
            } else {
                swal.fire("No data available to export.");
            }
        },
        error: function () {
            swal.fire("Error checking data.");
        }
    });
}

function ValidateDate() {
    var fromDate = $('#DateFrom').val();
    var toDate = $('#DateTo').val();

    // Convert dd-MMM-yyyy to yyyy-MM-dd for JS
    function parseDate(str) {
        var parts = str.split('-');
        if (parts.length !== 3) return null;
        var day = parseInt(parts[0], 10);
        var month = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"].indexOf(parts[1]);
        var year = parseInt(parts[2], 10);
        return new Date(year, month, day);
    }

    var D1 = parseDate(fromDate);
    var D2 = parseDate(toDate);

    if (!D1 || !D2) {
        swal.fire("Please enter valid From and To dates");
        return false;
    }

    if (D1 > D2) {
        swal.fire("To date should be greater than From date");
        return false;
    }

    return true;
}
