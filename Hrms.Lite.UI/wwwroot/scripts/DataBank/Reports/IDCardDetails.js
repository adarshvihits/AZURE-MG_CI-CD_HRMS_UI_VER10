////function onDateChange() {
////    debugger;
////    var fromDate = $('#FromDate').val();
////    var toDate = $('#ToDate').val();



////    if (fromDate !== '' && toDate !== '') {
////        showSpinner();
////        $.ajax({

////            type: 'GET',
////            url: '/DataBank/IDCardDetails/GetList',
////            dataType: 'html',
////            data: { FromDate: fromDate, ToDate: toDate },

////            success: function (result) {
////                debugger
////                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
////                    window.location.reload();
////                }
////                else {
////                    $('#idCardDetails_List').html("");
////                    $('#idCardDetails_List').html(result);
////                }
////                hideSpinner();


////            },
////            error: function (xhr, textStatus, errorThrown) {
////                debugger
////                document.body.id = "kt_body";
////                $('body').html(xhr.responseText);
////                hideSpinner();

////            }
////        });
////        return true;
////    }
////    else {

////        return false;
////    }

////}

function onDateChange() {
    debugger;
    var fromDate = $('#FromDate').val();
    var toDate = $('#ToDate').val();

    // Check if both dates are provided
    if (fromDate !== '' && toDate !== '') {
        // Parse dates and compare
        var from = new Date(fromDate);
        var to = new Date(toDate);

        if (from > to) {
            alert('From Date must be earlier than To Date.');
            return false;
        }

        // Show spinner and make the AJAX call if dates are valid
        showSpinner();
        $.ajax({
            type: 'GET',
            url: '/DataBank/IDCardDetails/GetList',
            dataType: 'html',
            data: { FromDate: fromDate, ToDate: toDate },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                } else {
                    $('#showIdCardDetails_List').html("");
                    $('#showIdCardDetails_List').html(result);
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
        return true;
    } else {
        alert('Please provide both From Date and To Date.');
        return false;
    }
}




function ApplyFilter(mode) {

    if (mode == 'apply') {
        if (filterApplicable >= 1) {
            $.ajax({

                type: 'POST',
                url: '/DataBank/IDCardDetails/ApplyFilter',
                dataType: 'html',
                data: $('#BasicFilter').serialize() + "&FromDate=" + $('#FromDate').val() + "&ToDate=" + $('#ToDate').val(),
                success: function (result) {
                    debugger
                    CloseSlider('basicFilter');
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }
                    else {
                        $('#showIdCardDetails_List').html("");
                        $('#showIdCardDetails_List').html(result);
                    }
                    hideSpinner();
                },


                error: function (xhr, textStatus, errorThrown) {
                    document.body.id = "kt_body";
                    $('body').html(xhr.responseText);
                }

            });
            $('#filterForm').removeClass('filter-apply');
        }
        else {
            $.ajax({

                type: 'POST',
                url: '/DataBank/IDCardDetails/ApplyFilter',
                dataType: 'html',
                data: $('#BasicFilter').serialize() + "&FromDate=" + $('#FromDate').val() + "&ToDate=" + $('#ToDate').val(),
                success: function (result) {
                    debugger
                    CloseSlider('basicFilter');

                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }
                    else {
                        $('#showIdCardDetails_List').html("");
                        $('#showIdCardDetails_List').html(result);
                    }
                    hideSpinner();
                },


                error: function (xhr, textStatus, errorThrown) {
                    document.body.id = "kt_body";
                    $('body').html(xhr.responseText);
                }

            });
            $('#filterForm').addClass('filter-apply');
        }
    }
    else {
        filterApplicable = 1;
        $('.reset').val('');
        $('#Optional_Filter_EmploymentStatus_Code').val(0);
        $('#filterForm').removeClass('filter-apply');
        $.ajax({

            type: 'POST',
            url: '/DataBank/IDCardDetails/ApplyFilter',
            dataType: 'html',
            data: $('#BasicFilter').serialize() + "&FromDate=" + $('#FromDate').val() + "&ToDate=" + $('#ToDate').val(),
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('#showIdCardDetails_List').html("");
                    $('#showIdCardDetails_List').html(result);
                }
                hideSpinner();
            },


            error: function (xhr, textStatus, errorThrown) {
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);
            }
        });

    }
}

function ExportToExcel() {
    debugger;

    var filter = $('#BasicFilter').serialize();

    var FromDate = $('#FromDate').val();
    var ToDate = $('#ToDate').val();

    var PARAM = 'data=' + filter + '&FromDate=' + FromDate + '&ToDate=' + ToDate;
    var TableLength = $('#IDCardDetailsList_Count').val();
    if (TableLength > 0) {
        url = '/DataBank/IDCardDetails/ExportToExcel?' + PARAM + "&totRecords=" + TableLength;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}

//function downloadDocument(filename, absoluteuri) {
//    debugger
//    if (filename == "" || absoluteuri == "" || absoluteuri == null) {
//        swal.fire("No file uploaded");
//    } else {
//        $.ajax({
//            type: 'GET',
//            url: '/AccessFile/GetFileStorageType',
//            success: function (data) {
//                if (data == 'BLOB') {
//                    debugger
//                    if (filename != "") {
//                        var url = "/AccessFile/DownloadDocument?filename=" + encodeURIComponent(filename); // Use Download API
//                        // Create a temporary <a> element for downloading
//                        var a = document.createElement('a');
//                        a.href = url;
//                        a.download = filename; // Set the filename for download
//                        document.body.appendChild(a);
//                        a.click();
//                        document.body.removeChild(a); // Remove the temporary element after click
//                    }
//                } else {
//                    // For non-BLOB files, use the absolute URI
//                    var a = document.createElement('a');
//                    a.href = absoluteuri;
//                    a.download = filename; // Set the filename for download
//                    document.body.appendChild(a);
//                    a.click();
//                    document.body.removeChild(a); // Remove the temporary element after click
//                }
//            },
//            error: function (xhr, textStatus, errorThrown) {
//                if ($($.parseHTML(xhr.responseText)).filter(".Login_Session").length > 0) {
//                    window.location.reload();
//                } else {
//                    document.body.id = "kt_body";
//                    $('body').html(xhr.responseText);
//                }
//            }
//        });
//    }
//}
function downloadDocument(filename, absoluteuri) {
    debugger;
    if (!filename || !absoluteuri) { // Added null/undefined/empty check
        swal.fire("No file uploaded or file details missing.");
        return; // Stop execution if inputs are invalid
    }

    $.ajax({
        type: 'GET',
        url: '/AccessFile/GetFileStorageType',
        success: function (data) {
            if (!data) { // Check if data returned from server is null or undefined
                swal.fire("File storage type could not be determined.");
                return;
            }

            if (data === 'BLOB') {
                if (filename) {
                    var url = "/AccessFile/DownloadDocument?filename=" + encodeURIComponent(filename);
                    // Create a temporary <a> element for downloading
                    var a = document.createElement('a');
                    a.href = url;
                    a.download = filename; // Suggest the filename for download
                    document.body.appendChild(a);
                    a.click();
                    document.body.removeChild(a); // Remove the temporary element after click
                }
            } else {
                if (absoluteuri) {
                    // For non-BLOB files, use the absolute URI
                    var a = document.createElement('a');
                    a.href = absoluteuri;
                    a.download = filename; // Suggest the filename for download
                    document.body.appendChild(a);
                    a.click();
                    document.body.removeChild(a); // Remove the temporary element after click
                }
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            if ($($.parseHTML(xhr.responseText)).filter(".Login_Session").length > 0) {
                window.location.reload();
            } else {
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);
            }
        }
    });
}


function showDecompressedDocument(EmployeeId) {
    debugger;

   
$.ajax({
    type: 'GET',
    url: "/DataBank/IDCardDetails/DownloadDoc?EmployeeId=" + EmployeeId,
    xhrFields: {
        responseType: 'blob'
    },
    success: function (data, status, xhr) {

        // Default filename
        var filename = "Document";

        // Get filename from response header if available
        var disposition = xhr.getResponseHeader('Content-Disposition');
        if (disposition && disposition.indexOf('filename=') !== -1) {
            filename = disposition.split('filename=')[1].replace(/"/g, '');
        }

        // Create blob object
        var blob = new Blob([data]);

        // Create temporary download link
        var url = window.URL.createObjectURL(blob);
        var a = document.createElement('a');

        a.href = url;
        a.download = filename;

        document.body.appendChild(a);
        a.click();

        // Cleanup
        window.URL.revokeObjectURL(url);
        document.body.removeChild(a);
    },
    error: function () {
        alert("Error downloading file.");
    }
});


}
