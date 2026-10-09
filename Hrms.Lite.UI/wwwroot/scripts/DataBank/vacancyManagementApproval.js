function getVacancyManagementDetails(GI) {
    debugger;
    //var url = "/DataBank/VacancyManagementApproval/Create";
    //$('#VMASlider').load(url, { GI: GI }, function () { });
    $.ajax({

        type: 'GET',
        url: '/DataBank/VacancyManagementApproval/Create',
        dataType: 'html',
        data: { GI: GI },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {
                /*$('body').append('<div class="form-overlay"></div>');*/
                $('#VMASlider').html("");
                $('#VMASlider').html(result);
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

function VacancyMASave(status) {
    var mode = "";
    if (status == 1) {
        mode = "Approve";
    }
    else {
        mode = "Reject";
    }
    $('#Approvestatus').val(status);
    var i = 0;
    if (!validateById('#ApproverComments', 'appcomment')) {
        i++;
    }
   
    if (i > 0) {
        return false;
    }
    else {
        swal.fire({
            text: "Do you want to "+ mode+"?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
    $.ajax({
        type: 'POST',
        url: '/DataBank/VacancyManagementApproval/VMASave',
        dataType: 'json',
        data: $('#VacancyMASave').serialize(),
        success: function (result) {
            if (result.success == true) {
                swal.fire(result.message).then(function () {
                    CloseSlider('VMA');
                    var url = "/DataBank/VacancyManagementApproval/GetList";

                    $('#VMAList').load(url, function () { });

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
}


function ExportToExcel() {
    debugger;

    var vacancymanagementTable = $('#VacancyManagementApprovalList_Count').val();

    if (vacancymanagementTable > 0) {
        url = "/DataBank/VacancyManagementApproval/ExportToExcel";
        window.location.href = url;

    }
    else {
        swal.fire("No data found to export");
    }
}