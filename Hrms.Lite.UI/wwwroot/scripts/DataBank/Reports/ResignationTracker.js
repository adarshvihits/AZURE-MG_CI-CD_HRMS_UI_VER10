


function onDateChange() {
    debugger;
    var fromDate = $('#FromDate').val();
    var toDate = $('#ToDate').val();
    var status = $('#Status option:selected').text()


    if (fromDate !== '' && toDate !== '') {
        showSpinner();
        $.ajax({

            type: 'GET',
            url: '/DataBank/ResignationTrackingReport/GetList',
            dataType: 'html',
            data: { FromDate: fromDate, ToDate: toDate, status: status },

            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('#ResignationTrackerList').html("");
                    $('#ResignationTrackerList').html(result);
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
    }
    else {

        return false;
    }

}




function GetResignationTracker(ResignationGI, EmployeeGI) {
    debugger;
    var status = $('#Status option:selected').text()
    $.ajax({

        type: 'GET',
        url: '/DataBank/ResignationTrackingReport/GetResignationTracker',
        dataType: 'html',
        data: { ResignationGI: ResignationGI, EmployeeGI: EmployeeGI, status: status },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {
                $('body').append('<div class="form-overlay"></div>');
                $('#ResignationTrackerSlider').html("");
                $('#ResignationTrackerSlider').html(result);
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
    var status = $('#Status option:selected').text()
    if (mode == 'apply') {
        if (filterApplicable >= 1) {
            $.ajax({

                type: 'POST',
                url: '/DataBank/ResignationTrackingReport/ApplyFilter',
                dataType: 'html',
                data: $('#BasicFilter').serialize() + "&F_From=" + $('#FromDate').val() + "&F_To=" + $('#ToDate').val() + '&status=' + status,
                success: function (result) {
                    debugger
                    CloseSlider('basicFilter');
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }
                    else {
                        $('#ResignationTrackerList').html("");
                        $('#ResignationTrackerList').html(result);
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
                url: '/DataBank/ResignationTrackingReport/ApplyFilter',
                dataType: 'html',
                data: $('#BasicFilter').serialize() + "&F_From=" + $('#FromDate').val() + "&F_To=" + $('#ToDate').val() + '&status=' + status,
                success: function (result) {
                    debugger
                    CloseSlider('basicFilter');

                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }
                    else {
                        $('#ResignationTrackerList').html("");
                        $('#ResignationTrackerList').html(result);
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
            url: '/DataBank/ResignationTrackingReport/ApplyFilter',
            dataType: 'html',
            data: $('#BasicFilter').serialize() + "&F_From=" + $('#FromDate').val() + "&F_To=" + $('#ToDate').val() + '&status=' + status,
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('#ResignationTrackerList').html("");
                    $('#ResignationTrackerList').html(result);
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
  
    var tableLength = $('#List_Count').val();
    if (tableLength > 0) {
         var status = $('#Status option:selected').text();
        url = "/DataBank/ResignationTrackingReport/ExportToExcel" + '?status=' + status + '&FromDte=' + $('#FromDate').val() + '&ToDte=' + $('#ToDate').val();
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}