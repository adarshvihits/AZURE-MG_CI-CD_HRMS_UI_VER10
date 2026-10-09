function ClickOnJobTitle(GI) {
    debugger;
    //showSpinner();
    //var url = "/DataBank/VacancyCancellation/GetCancellationDetails?VacancycancellationGI=" + GI;

    //$('#CancellationSlider').load(url, {}, function () { hideSpinner(); });
    $.ajax({

        type: 'GET',
        url: '/DataBank/VacancyCancellation/GetCancellationDetails',
        dataType: 'html',
        data: { VacancycancellationGI: GI },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {
                /*$('body').append('<div class="form-overlay"></div>');*/
                $('#CancellationSlider').html("");
                $('#CancellationSlider').html(result);
            }

            //hideSpinner();
        },


        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
            hideSpinner();

        }
    });
}



function ClickOnCancelButton() {
    debugger;

    if (ValidateCancellation()) {

        swal.fire({
            text: "Do you want to Cancel?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({

                    type: 'POST',
                    url: '/DataBank/VacancyCancellation/Cancel',
                    dataType: 'json',
                    data: $('#VacancyCancellation').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                CloseSlider('cancellationSlider');
                                debugger;
                                var url = "/DataBank/VacancyCancellation/GetList";
                                $('#CancellationList').load(url, {}, function () { });

                            });

                        }

                        else {
                            swal.fire(result.message).then(function () {

                            });
                        }

                    },
                    error: function (xhr, textStatus, errorThrown) {
                        if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                            window.location.reload();
                        }

                        else {
                            document.body.id = "kt_body";
                            $('body').html(xhr.responseText);
                        }

                    }
                });
            }
            else {
                return false;
            }
        })

    }
    else {
        return false;
    }

}



function ValidateCancellation() {
    var i = 0;
    if (!validateById('#CancellationReason_Code', 'cancellation')) {
        i++;
    }
    if (!validateById('#ApproverComments', 'approver')) {
        i++;
    }

    if (i == 0)
        return true;
    else
        return false;
}




function ExportToExcel() {
    debugger;
    var CancellationTable = $('#VacancyStatusReport_Count').val();
   /* var CancellationTable = document.getElementById("CancellationListTable").rows.length - 1;*/

    if (CancellationTable > 0) {
        url = "/DataBank/VacancyCancellation/ExportToExcel";
        window.location.href = url;

    }
    else {
        swal.fire("No data found to export");
    }
}