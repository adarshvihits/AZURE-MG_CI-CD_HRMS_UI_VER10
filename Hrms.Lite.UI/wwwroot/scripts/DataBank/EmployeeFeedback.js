function GetFeedbacklist() {
    var radiomode = $('input[name="radiomode"]:checked').val();
    $.ajax({

        type: 'GET',
        url: '/DataBank/EmployeeFeedback/GetList',
        dataType: 'html',
        data: { radiomode: radiomode},
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#EmployeeFeedbackList').html("");
                $('#EmployeeFeedbackList').html(result);
            }


        },
        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
            hideSpinner();

        }
    });
}




function onEditClick(EmployeeGI, FeedbackCode) {
    debugger;
    var radiomode = $('input[name="radiomode"]:checked').val();
    $.ajax({
        type: 'GET',
        url: '/DataBank/EmployeeFeedback/GetDetails',
        dataType: 'html',
        data: { EmployeeGI: EmployeeGI, FeedbackCode: FeedbackCode, radiomode: radiomode},
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('body').append('<div class="form-overlay"></div>');
                $('#EmployeeFeedbackSlider').html("");
                $('#EmployeeFeedbackSlider').html(result);
            }


        },
        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);


        }
    });
}


function ActiveInactiveValidation(name, code) {
    debugger

    $.ajax({

        type: 'POST',
        url: '/DataBank/EmployeeFeedback/GetActiveInactiveValidation?name=' + name + '&code=' + code,
        dataType: 'json',

        success: function (result) {
            if (result.success == true) {
                //swal.fire(result.message).then(function () {
                debugger;
                // $("#activeInactive").prop("checked", false);

                //});

            }
            else {
                swal.fire(result.message).then(function () {
                    $("#Activeinactivetrmode").prop("checked", true);
                });
            }

        },
        error: function (xhr, textStatus, errorThrown) {
            if ($($.parseHTML(xhr.responseText)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);
            }
        }
    });

}

function Feedbacksave() {
    var radiomode = $('input[name="radiomode"]:checked').val();

    swal.fire({
        text: "Do you want to save?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {

    $.ajax({
        type: 'POST',
        url: '/DataBank/EmployeeFeedback/Save',
        dataType: 'json',
        data: $('#FeedbackSlider').serialize(),
        success: function (result) {
            if (result.success == true) {
                swal.fire(result.message).then(function () {
                    CloseSlider('Feedback');
                    var url = "/DataBank/EmployeeFeedback/GetList?radiomode=" + radiomode;
                    $('#EmployeeFeedbackList').load(url, function () { });
                });
            } else {
                swal.fire(result.message).then(function () {
                    // Optional: Add handling for unsuccessful cases here
                });
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            debugger;
            if ($($.parseHTML(xhr.responseText)).filter(".Login_Session").length > 0) {
                window.location.reload();
            } else {
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

