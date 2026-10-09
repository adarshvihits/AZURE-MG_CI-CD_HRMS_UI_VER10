//----------Interview Feedback Settings--------------------

var prevRoundTab = 'FirstRoundTab';
var currentRound = 1;

function loadFeedbackSetList() {
    showSpinner();
    $.ajax({
        type: 'GET',
        url: '/Recruitment/InterviewFeedbackSettings/GetInterviewFeedbackSettingsList',
        dataType: 'html',
        data: { RoundType: currentRound, GradeCode: $('#GradeFilter').val() },
        success: function (result) {
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#InterviewFeedbackSettingsList').html(result);
            }
            hideSpinner();
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

function onRoundTabClick(Tab, RoundType) {
    currentRound = RoundType;
    $("." + prevRoundTab).removeClass("active");
    $("." + Tab).addClass("active");
    prevRoundTab = Tab;
    $('#tabsDropdownFeedbackRound').val(RoundType);
    loadFeedbackSetList();
}

function onRoundDropdownChange(val) {
    var round = parseInt($(val).val());
    onRoundTabClick(round == 1 ? 'FirstRoundTab' : 'SecondRoundTab', round);
}

function onGradeFilterChange() {
    loadFeedbackSetList();
}

//----------Create / Edit slider--------------------

function onAddOrEditFeedbackSet(Mode, GI) {
    var url = '/Recruitment/InterviewFeedbackSettings/Create';
    var data = {};
    if (Mode == 'MODIFY') {
        url = '/Recruitment/InterviewFeedbackSettings/Edit';
        data = { FeedbackSetGI: GI };
    }

    $.ajax({
        type: 'GET',
        url: url,
        dataType: 'html',
        data: data,
        success: function (result) {
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $(".form-overlay").remove();
                $('body').append('<div class="form-overlay"></div>');
                $('.form-overlay').on('click', closeFeedbackSetSlider);
                $('#feedbackSetSlider').html(result);
                initFeedbackSetGrades();

                // render closed first, then open so the slider slides in from the right
                requestAnimationFrame(function () {
                    requestAnimationFrame(function () {
                        $('#FeedbackSetSlider').addClass('slider-forms-open');
                        moveCursorToEnd('FeedbackSetName');
                    });
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

// applicable grades: searchable multi-select dropdown with tick options
function initFeedbackSetGrades() {
    $('#FeedbackSetGrades').multiselect({
        includeSelectAllOption: true,
        selectAllText: 'Select All',
        nonSelectedText: '--Select Grades--',
        enableCaseInsensitiveFiltering: true,
        includeFilterClearBtn: false,
        filterPlaceholder: 'Search grade',
        buttonWidth: '100%',
        maxHeight: 250,
        numberDisplayed: 3,
        onChange: function () { validateFeedbackSetGrades(); },
        onSelectAll: function () { validateFeedbackSetGrades(); },
        onDeselectAll: function () { validateFeedbackSetGrades(); }
    });
}

function getFeedbackSetGrades() {
    return $('#FeedbackSetGrades').val() || [];
}

function validateFeedbackSetGrades() {
    return setValidationMessage('ifsGrades', getFeedbackSetGrades().length == 0 ? 'Select at least one grade' : '');
}

function closeFeedbackSetSlider() {
    CloseSlider('FeedbackSet');
    // clear the markup once the slide-out transition has finished
    setTimeout(function () {
        if (!$('#FeedbackSetSlider').hasClass('slider-forms-open'))
            $('#feedbackSetSlider').html("");
    }, 300);
}

function addFeedbackQuestion() {
    var row = '<div class="ifs-question-row">' +
        '<textarea class="form-control form-control-sm ifs-question" rows="2" maxlength="1000" placeholder="Enter feedback question"></textarea>' +
        '<div class="ifs-active-cell">' +
        '<label class="switch" title="Active"><input type="checkbox" class="ifs-active" checked /><span class="slider round"></span></label>' +
        '</div>' +
        '<a class="ifs-remove-row" title="Remove question" onclick="removeFeedbackQuestion(this)"><i class="ki ki-close icon-xs"></i></a>' +
        '</div>';
    $('#FeedbackQuestionRows').append(row);
    $('#FeedbackQuestionRows .ifs-question').last().focus();
    updateQuestionCount();
}

function removeFeedbackQuestion(btn) {
    // always keep one row to type in
    if ($('#FeedbackQuestionRows .ifs-question-row').length > 1)
        $(btn).closest('.ifs-question-row').remove();
    else
        $(btn).closest('.ifs-question-row').find('.ifs-question').val('');
    updateQuestionCount();
}

function updateQuestionCount() {
    $('.ifs-question-count').html($('#FeedbackQuestionRows .ifs-question-row').length);
}

function setValidationMessage(spanId, message) {
    if (message) {
        $('#' + spanId).addClass('error-message').html(message);
        return false;
    }
    $('#' + spanId).removeClass('error-message').html('');
    return true;
}

function feedbackSetSave(mode) {
    var i = 0;
    if (!validateById('#FeedbackSetName', 'ifsName'))
        i++;
    if (!validateFeedbackSetGrades())
        i++;
    var hasQuestion = $('#FeedbackQuestionRows .ifs-question').filter(function () { return $.trim($(this).val()) != ''; }).length > 0;
    if (!setValidationMessage('ifsQuestions', hasQuestion ? '' : 'Add at least one question'))
        i++;
    if (i > 0)
        return false;

    // build the request in the shape MVC model binding expects (SelectedGrades[0], QuestionList[0].Question ...)
    var data = {
        FeedbackSetGI: $('#FeedbackSetGI').val(),
        Mode: $('#FeedbackSetMode').val(),
        Name: $.trim($('#FeedbackSetName').val()),
        RoundType: $('input[name="FeedbackRound"]:checked').val()
    };
    $.each(getFeedbackSetGrades(), function (index, value) {
        data['SelectedGrades[' + index + ']'] = value;
    });
    var q = 0;
    $('#FeedbackQuestionRows .ifs-question-row').each(function () {
        var text = $.trim($(this).find('.ifs-question').val());
        if (text != '') {
            data['QuestionList[' + q + '].Question'] = text;
            data['QuestionList[' + q + '].Active'] = $(this).find('.ifs-active').is(':checked');
            q++;
        }
    });

    swal.fire({
        text: "Do you want to " + mode + "?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: 'POST',
                url: '/Recruitment/InterviewFeedbackSettings/InterviewFeedbackSettingsSave',
                dataType: 'json',
                data: data,
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            closeFeedbackSetSlider();
                            // show the round the set was saved under
                            var round = parseInt(data.RoundType);
                            onRoundTabClick(round == 1 ? 'FirstRoundTab' : 'SecondRoundTab', round);
                        });
                    }
                    else {
                        swal.fire(result.message);
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
    });
    return true;
}
