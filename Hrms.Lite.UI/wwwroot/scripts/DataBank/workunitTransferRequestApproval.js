function GetPopup(EmployeeGI) {

    $.ajax({
        type: 'GET',
        url: '/DataBank/WorkUnitTransferRequestApproval/GetEmployeeWorkUnitDetails',
        dataType: 'html',
        data: { EmployeeGI: EmployeeGI },
        success: function (result) {
            debugger;
            var m = $('.addAuthorityPopup');
            m.find('.modal-body').html('');
            m.find('.modal-body').html(result);
            m.modal('show');
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

function onWorkUnitChange(sender) {

    $.ajax({
        type: 'GET',
        url: '/DataBank/WorkUnitTransferRequestApproval/GetWorkUnitAuthorityDetails',
        dataType: 'html',
        data: { WorkUnitCode: $(sender).val() },
        success: function (result) {
            $('#PopUpListView').html('');
            $('#PopUpListView').html(result);
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

function apprRemksChange() {
    $('#ApproverRemarks').removeClass('is-invalid')
    $('#rmks').removeClass('error-message')
    $('#rmks').html('')
}
function ApproveOrReject(mode) {
    var i = 0;
    if (!validateById('#EffectiveDate', 'EfFDate'))
        i++;
    var msg = '';
    if (mode == 'Approved')
        msg = 'Approve';
    else {
        if (!validateById('#ApproverRemarks', 'rmks'))
            i++;
        msg = 'Reject';
    }
    if (i == 0) {
 
        swal.fire({
            text: "Do you want " + msg + "?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    type: 'POST',
                    url: '/DataBank/WorkUnitTransferRequestApproval/ApproveOrReject?Mode=' + mode,
                    dataType: 'json',
                    data: $('#WrkUnitTransferReqAppOrRej').serialize(),
                    success: function (result) {
                        if (result.success) {
                            swal.fire(mode + ' Successfully').then(function () {
                                var m = $('.addAuthorityPopup');
                                m.find('.modal-body').html('');
                                m.modal('hide');
                                window.location.reload();
                            });
                        }
                        else
                            swal.fire(result.message);
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
        });

    }

}