var prevTab = 'tab1';
var currentTab = 'PENDING';

function Tab(Tab, TabIndex) {
    currentTab = TabIndex;
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");
    prevTab = Tab;
    $('#dataTableSearch').val('');
    GetList();
}

function GetList() {
    showSpinner();
    $.ajax({
        type: 'GET',
        url: '/Master/JobPostingConfirmation/GetList',
        dataType: 'html',
        data: { TabIndex: currentTab },
        success: function (result) {
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#JobPostingConfirmationList').html("");
                $('#JobPostingConfirmationList').html(result);
            }
            hideSpinner();
        },
        error: function (xhr, textStatus, errorThrown) {
            if ($($.parseHTML(xhr.responseText)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);
            }
            hideSpinner();
        }
    });
}

function OpenVacancySlider(VacancySourceCode, SourceTranactionCode) {
    $.ajax({
        type: 'GET',
        url: '/Master/JobPostingConfirmation/Proceed',
        dataType: 'html',
        data: { VacancySourceCode: VacancySourceCode, SourceTranactionCode: SourceTranactionCode },
        success: function (result) {
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else if ($($.parseHTML(result)).find('.jpcclose').addBack('.jpcclose').length == 0) {
                // The action failed and redirected to the error page instead of returning the slider
                swal.fire('Unable to load the vacancy details. Please try again.');
            }
            else {
                $('body').append('<div class="form-overlay"></div>');
                $('#JobPostingConfirmationSlider').html("");
                $('#JobPostingConfirmationSlider').html(result);

                // Slide in: the panel is rendered closed, then opened on the next frame
                var $slider = $('.jpcclose');
                $slider[0].offsetWidth; // force reflow so the transition runs
                requestAnimationFrame(function () {
                    $slider.addClass('slider-forms-open');
                });
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
        }
    });
}

function CloseVacancySlider() {
    // Slide out first, then remove
    $(".jpcclose").removeClass('slider-forms-open');
    $(".form-overlay").fadeOut(250, function () { $(this).remove(); });
    setTimeout(function () { $(".jpcclose").remove(); }, 450);
}

function ToggleJobMasterDetails() {
    var $box = $('#jpcJdBox');
    var opening = !$box.is(':visible');
    $box.slideToggle(150);
    $('#jpcViewJd').html('<i class="fas fa-caret-' + (opening ? 'up' : 'down') + ' mr-1"></i> ' + (opening ? 'Hide' : 'View'));
}

var CAREER_PORTAL_CODE = '1';

function OnPostToChange() {
    var isPortal = $('#jpcPostingDestination').val() == CAREER_PORTAL_CODE;
    if (!isPortal) {
        $('#jpcNaukri, #jpcLinkedIn').prop('checked', false);
        $('#jpcPortalError').html('');
    }
    $('#jpcPortalBox').toggle(isPortal);
    $('#jpcSourcesError').html('');
    $('#jpcPostingDestination').removeClass('is-invalid');
}

function ValidateConfirmPosting() {
    var errors = 0;

    $('#jpcSourcesError, #jpcPortalError, #jpcPostedByError').html('');
    if ($('#jpcJdEmpty').length) {
        swal.fire({ icon: 'warning', text: 'Job master details is not available. Please create the Job first.' });
        return false;
    }
    $('#jpcPostingDestination').removeClass('is-invalid');

    if (!$('#jpcPostingDestination').val()) {
        $('#jpcSourcesError').html('<span class="text-danger">Post To is required.</span>');
        $('#jpcPostingDestination').addClass('is-invalid');
        errors++;
    }
    // else if ($('#jpcPostingDestination').val() == CAREER_PORTAL_CODE
    //     && $('#jpcNaukri:checked, #jpcLinkedIn:checked').length == 0) {
    //     $('#jpcPortalError').html('<span class="text-danger">Please select at least one job portal.</span>');
    //     errors++;
    // }

    if (!$.trim($('#jpcPostedBy').val())) {
        $('#jpcPostedByError').html('<span class="text-danger">Posted By is required.</span>');
        errors++;
    }

    return errors == 0;
}

function ConfirmPosting() {
    if (!ValidateConfirmPosting()) {
        return false;
    }

    swal.fire({
        text: "Do you want to confirm posting?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            showSpinner();
            $.ajax({
                type: 'POST',
                url: '/Master/JobPostingConfirmation/ConfirmPosting',
                dataType: 'json',
                data: $('#JPCSave').serialize(),
                success: function (result) {
                    hideSpinner();
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseVacancySlider();
                            GetList();
                        });
                    }
                    else {
                        swal.fire(result.message);
                    }
                },
                error: function (xhr, textStatus, errorThrown) {
                    hideSpinner();
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
    });
}

function ExportToExcel() {
    var count = $('#JobPostingConfirmationList_Count').val();
    if (count > 0) {
        window.location.href = "/Master/JobPostingConfirmation/ExportToExcel?TabIndex=" + currentTab;
    }
    else {
        swal.fire(NoDataFoundMsg);
    }
}
