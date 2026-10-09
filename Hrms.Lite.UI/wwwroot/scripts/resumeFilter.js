var filterApplicable = 1;
function onDesignationChange() {
    if ($('#Designations').val().length == 0) {
        filterApplicable += 1;
    }
    else {
        filterApplicable -= 1;
    }
}
function onDepartmentChange() {
    if ($('#Qualifications').val().length == 0) {
        filterApplicable += 1;
    }
    else {
        filterApplicable -= 1;
    }
}

function onLocationChange() {
    if ($('#Locations').val().length == 0) {
        filterApplicable += 1;
    }
    else {
        filterApplicable -= 1;
    }
}
function onCertificationChange() {
    if ($('#Certifications').val().length == 0) {
        filterApplicable += 1;
    }
    else {
        filterApplicable -= 1;
    }
}
function onSkillChange() {
    if ($('#Skills').val().length == 0) {
        filterApplicable += 1;
    }
    else {
        filterApplicable -= 1;
    }
}
function onDistrictChange() {
    if ($('#Districts').val().length == 0) {
        filterApplicable += 1;
    }
    else {
        filterApplicable -= 1;
    }
}

function onMaritalstatusChange() {
    if ($('#Maritalstatus').val().length == 0) {
        filterApplicable += 1;
    }
    else {
        filterApplicable -= 1;
    }
}
function onGenderChange() {
    if ($('#Gender').val().length == 0) {
        filterApplicable += 1;
    }
    else {
        filterApplicable -= 1;
    }
}

function onResumeSourceChange() {
    if ($('#ResumeSource').val().length == 0) {
        filterApplicable += 1;
    }
    else {
        filterApplicable -= 1;
    }
}
