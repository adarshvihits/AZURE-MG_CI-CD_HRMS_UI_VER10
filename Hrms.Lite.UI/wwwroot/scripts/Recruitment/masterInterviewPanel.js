//----------Master Interview Panel--------------------

var panelRoles = ['Chairperson', 'Department Head', 'Technical Panelist', 'HR Panelist', 'External Expert'];

function loadMasterInterviewPanelList() {
    showSpinner();
    $.ajax({
        type: 'GET',
        url: '/Recruitment/MasterInterviewPanel/GetMasterInterviewPanelList',
        dataType: 'html',
        data: {
            LocationCode: $('#LocationFilter').val(),
            DepartmentCode: $('#DepartmentFilter').val(),
            SectionCode: $('#SectionFilter').val(),
            GradeCode: $('#GradeFilter').val()
        },
        success: function (result) {
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#MasterInterviewPanelList').html(result);
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

//----------Create / Edit slider--------------------

function onAddOrEditMasterInterviewPanel(Mode, GI) {
    var url = '/Recruitment/MasterInterviewPanel/Create';
    var data = {};
    if (Mode == 'MODIFY') {
        url = '/Recruitment/MasterInterviewPanel/Edit';
        data = { PanelGI: GI };
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
                $('.form-overlay').on('click', closeMasterInterviewPanelSlider);
                $('#masterInterviewPanelSlider').html(result);

                // render closed first, then open so the slider slides in from the right
                requestAnimationFrame(function () {
                    requestAnimationFrame(function () {
                        $('#MasterInterviewPanelSlider').addClass('slider-forms-open');
                        moveCursorToEnd('PanelName');
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

function closeMasterInterviewPanelSlider() {
    CloseSlider('MasterInterviewPanel');
    // clear the markup once the slide-out transition has finished
    setTimeout(function () {
        if (!$('#MasterInterviewPanelSlider').hasClass('slider-forms-open'))
            $('#masterInterviewPanelSlider').html("");
    }, 300);
}

function addPanelMember() {
    var options = $.map(panelRoles, function (role) { return '<option value="' + role + '">' + role + '</option>'; }).join('');
    var row = '<div class="mp-member-row">' +
        '<input type="text" class="form-control form-control-sm mp-member-name" maxlength="200" placeholder="Full name" />' +
        '<select class="form-control form-control-sm mp-member-role">' + options + '</select>' +
        '<input type="text" class="form-control form-control-sm mp-member-designation" maxlength="200" placeholder="Designation" />' +
        '<input type="text" class="form-control form-control-sm mp-member-email" maxlength="200" placeholder="name@malabargroup.com" />' +
        '<a class="mp-remove-row" title="Remove member" onclick="removePanelMember(this)"><i class="ki ki-close icon-xs"></i></a>' +
        '</div>';
    $('#PanelMemberRows').append(row);
    $('#PanelMemberRows .mp-member-name').last().focus();
    updatePanelMemberCount();
}

function removePanelMember(btn) {
    // always keep one row to type in
    if ($('#PanelMemberRows .mp-member-row').length > 1)
        $(btn).closest('.mp-member-row').remove();
    else
        $(btn).closest('.mp-member-row').find('input').val('');
    updatePanelMemberCount();
}

function updatePanelMemberCount() {
    $('.mp-member-count').html($('#PanelMemberRows .mp-member-row').length);
}

function setPanelValidationMessage(spanId, message) {
    if (message) {
        $('#' + spanId).addClass('error-message').html(message);
        return false;
    }
    $('#' + spanId).removeClass('error-message').html('');
    return true;
}

function validateMasterInterviewPanelSelect(selector, spanId, label) {
    return setPanelValidationMessage(spanId, parseInt($(selector).val() || 0) == 0 ? 'Select ' + label : '');
}

// rows with a name; returns an error message for the first invalid row, '' if all good
function validatePanelMembers() {
    var emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    var filled = 0;
    var message = '';
    $('#PanelMemberRows .mp-member-row').each(function (index) {
        var name = $.trim($(this).find('.mp-member-name').val());
        var email = $.trim($(this).find('.mp-member-email').val());
        if (name == '')
            return;
        filled++;
        if (email != '' && !emailPattern.test(email) && message == '')
            message = 'Enter a valid email for ' + name;
    });
    if (filled == 0)
        return 'Add at least one interviewer';
    return message;
}

function masterInterviewPanelSave(mode) {
    var i = 0;
    if (!validateById('#PanelName', 'mpName'))
        i++;
    if (!validateMasterInterviewPanelSelect('#PanelLocation', 'mpLocation', 'branch'))
        i++;
    if (!validateMasterInterviewPanelSelect('#PanelDepartment', 'mpDepartment', 'department'))
        i++;
    if (!validateMasterInterviewPanelSelect('#PanelSection', 'mpSection', 'section'))
        i++;
    if (!validateMasterInterviewPanelSelect('#PanelGrade', 'mpGrade', 'grade'))
        i++;
    if (!setPanelValidationMessage('mpMembers', validatePanelMembers()))
        i++;
    if (i > 0)
        return false;

    // build the request in the shape MVC model binding expects (MemberList[0].InterviewerName ...)
    var data = {
        PanelGI: $('#PanelGI').val(),
        Mode: $('#PanelMode').val(),
        Name: $.trim($('#PanelName').val()),
        Active: $('input[name="PanelStatus"]:checked').val(),
        LocationCode: $('#PanelLocation').val(),
        DepartmentCode: $('#PanelDepartment').val(),
        SectionCode: $('#PanelSection').val(),
        GradeCode: $('#PanelGrade').val(),
        Description: $.trim($('#PanelDescription').val())
    };
    var m = 0;
    $('#PanelMemberRows .mp-member-row').each(function () {
        var name = $.trim($(this).find('.mp-member-name').val());
        if (name != '') {
            data['MemberList[' + m + '].InterviewerName'] = name;
            data['MemberList[' + m + '].Role'] = $(this).find('.mp-member-role').val();
            data['MemberList[' + m + '].Designation'] = $.trim($(this).find('.mp-member-designation').val());
            data['MemberList[' + m + '].Email'] = $.trim($(this).find('.mp-member-email').val());
            data['MemberList[' + m + '].DisplayOrder'] = m + 1;
            m++;
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
                url: '/Recruitment/MasterInterviewPanel/MasterInterviewPanelSave',
                dataType: 'json',
                data: data,
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            closeMasterInterviewPanelSlider();
                            loadMasterInterviewPanelList();
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
