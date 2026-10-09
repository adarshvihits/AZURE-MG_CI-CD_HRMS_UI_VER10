function loadJobMasterList() {
    showSpinner();
    $('#dataTableSearch').val('');
    $.ajax({
        type: 'GET',
        url: '/Master/JobMaster/GetJobMasterList',
        dataType: 'html',
        data: {
            DepartmentCode: $('#DepartmentFilter').val(),
            Status: $('#StatusFilter').val()
        },
        success: function (result) {
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#JobMasterList').html("");
                $('#JobMasterList').html(result);
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

function onAddNewOrEditClick(Type, GI, LogGI, index) {

    var url = '/Master/JobMaster/Create';
    var data = {};
    if (Type == 'MODIFY') {
        if ($("#JobMasterList_" + index + "__EditActive").val() == 'True') {
            swal.fire(ApprovalPendingMsg);
            return;
        }
        url = '/Master/JobMaster/Edit';
        data = { JobGI: GI, LogGI: LogGI };
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
                $('body').append('<div class="form-overlay"></div>');
                $('#AddNewOrEditSlider').html("");
                $('#AddNewOrEditSlider').html(result);
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

function onApproveClick(GI, LogGI, index) {

    if ($("#JobMasterList_" + index + "__MakerChecker").val() == 'True')
        swal.fire(MakerCheckerMsg);
    else {
        $.ajax({
            type: 'GET',
            url: '/Master/JobMaster/Approve',
            dataType: 'html',
            data: { JobGI: GI, LogGI: LogGI },
            success: function (result) {
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('#ApproveSlider').html("");
                    $('#ApproveSlider').html(result);
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
}

function SaveJobMaster(mode) {

    if (validateJobMaster()) {
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
                    url: '/Master/JobMaster/Save',
                    dataType: 'json',
                    data: $('#jobMasterSave').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                CloseSlider('AddNewOrEdit');
                                loadJobMasterList();
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
            else {
                return false;
            }
        })
    }
    else {
        return false;
    }
}

function confirmApprove(GI, LogGI) {
    ApproveOrRejectJobMaster('/Master/JobMaster/Approve1', "Do you want to Approve?", GI, LogGI);
}

function confirmReject(GI, LogGI) {
    ApproveOrRejectJobMaster('/Master/JobMaster/Reject', "Do you want to Reject?", GI, LogGI);
}

function ApproveOrRejectJobMaster(url, message, GI, LogGI) {
    swal.fire({
        text: message,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: 'POST',
                url: url,
                dataType: 'json',
                data: $('#approve').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('ApproveDiv');
                            loadJobMasterList();
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
        else {
            return false;
        }
    })
}

function validateJobMaster() {
    var i = 0;

    $('#Name, #SalaryRange, #Skill, #Description').each(function () {
        $(this).val($.trim($(this).val()));
    });

    if (!validateById('#Description', 'JobDescriptionVal')) {
        document.getElementById("Description").focus();
        i++;
    }
    if (!validateById('#Skill', 'JobSkillVal')) {
        document.getElementById("Skill").focus();
        i++;
    }
    if (!validateById('#SalaryRange', 'JobSalaryRangeVal')) {
        document.getElementById("SalaryRange").focus();
        i++;
    }
    if (!validateById('#Industry_Code', 'JobIndustryVal')) {
        document.getElementById("Industry_Code").focus();
        i++;
    }
    if (!validateMultiselect('Qualifications', 'JobQualificationVal', 'JobQualificationDiv')) {
        i++;
    }
    if (!validateById('#ExperienceLevel_Code', 'JobExperienceVal')) {
        document.getElementById("ExperienceLevel_Code").focus();
        i++;
    }
    if (!validateById('#EmploymentType_Code', 'JobEmploymentTypeVal')) {
        document.getElementById("EmploymentType_Code").focus();
        i++;
    }
    if (!validateById('#WorkMode_Code', 'JobWorkModeVal')) {
        document.getElementById("WorkMode_Code").focus();
        i++;
    }
    if (!validateMultiselect('Grades', 'JobGradeVal', 'JobGradeDiv')) {
        i++;
    }
    if (!validateById('#BusinessType_Code', 'JobBusinessTypeVal')) {
        document.getElementById("BusinessType_Code").focus();
        i++;
    }
    if (!validateById('#Section_Code', 'JobSectionVal')) {
        document.getElementById("Section_Code").focus();
        i++;
    }
    if (!validateById('#Department_Code', 'JobDepartmentVal')) {
        document.getElementById("Department_Code").focus();
        i++;
    }
    if (!validateById('#Name', 'JobNameVal')) {
        document.getElementById("Name").focus();
        i++;
    }

    return i == 0;
}