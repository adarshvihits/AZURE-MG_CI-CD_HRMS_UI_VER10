

function fillData() {
    debugger;

    TabIndex = $('input[name="Type"]:checked').val();
    $.ajax({

        type: 'GET',
        url: '/DataBank/InterviewScheduling/GetInterviewSchedulingList',
        dataType: 'html',
        data: { TabIndex: TabIndex },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {
                $('#ApplicantTrackingList').html("");
                $('#ApplicantTrackingList').html(result);
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


function GetApplicantTracker(ResumeDataBankGI) {
    debugger;
      $.ajax({

        type: 'GET',
        url: '/DataBank/ApplicantTracking/GetApplicantTracker',
        dataType: 'html',
        data: { ResumeDataBankGI: ResumeDataBankGI },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {
                $('body').append('<div class="form-overlay"></div>');
                $('#ApplicantTrackingSlider').html("");
                $('#ApplicantTrackingSlider').html(result);
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

function GetEvaluationDetails(ResumeDataBankGI, ApplicantTrackingCode) {

    TabIndex = $('input[name="Type"]:checked').val();
    if (TabIndex == 0) {
        $.ajax({

            type: 'GET',
            url: '/DataBank/InterviewScheduling/GetEditDetails',
            dataType: 'html',
            data: { ResumeDataBankGI: ResumeDataBankGI, ApplicantTrackingCode: ApplicantTrackingCode },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }

                else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('#ApplicantTrackingSlider').html("");
                    $('#ApplicantTrackingSlider').html(result);
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
}
function timepicker() {

    debugger
    $('.kt_timepicker_2, .kt_timepicker_2_modal').timepicker({
        minuteStep: 1,
        defaultTime: '',
        showSeconds: false,
        showMeridian: false,
        autoclose: true
    });
}
function Schedule(msg) {
    swal.fire({
        text: "Do you want to " + msg + "?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            //    // Save it
            $.ajax({

                type: 'POST',
                url: '/DataBank/InterviewScheduling/Schedule',
                dataType: 'json',
                data: $('#interviewSchedule').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire("Saved Successfully").then(function () {
                            CloseSlider('scheduleSlider');
                            CloseSlider('evaluationSlider');
                            Tab('tab2', 2)
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