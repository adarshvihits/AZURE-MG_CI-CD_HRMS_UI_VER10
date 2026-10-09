// Recruiter Screening

var openVacancies = [];
var linkedCandidates = [];      // applicants already mapped to the selected vacancy
var suggestedCandidates = [];   // unattached applicants
var selectedOrder = [];         // codes of ticked linked applicants, in the order they were ticked
var currentCandidateTab = 'LINKED';
var currentVacancy = null;      // vacancy row opened from the list

$(document).ready(function () {
    UseButtonColour();
    LoadJobs();
    LoadOpenVacancies();
});

// ---- Vacancy list ---------------------------------------------------------

// Business Type -> Department and Section (taken from the jobs of that business type) -> Job -> list
function OnBusinessTypeChange() {
    $('#rsDepartment').val('');
    $('#rsSection').val('');
    $('#rsJob').val('');
    LoadDepartments();
    LoadSections();
    LoadJobs();
    LoadOpenVacancies();
}

function OnDepartmentChange() {
    $('#rsSection').val('');
    $('#rsJob').val('');
    LoadSections();
    LoadJobs();
    LoadOpenVacancies();
}

function OnScreeningFilterChange() {
    $('#rsJob').val('');
    LoadJobs();
    LoadOpenVacancies();
}

function FillDropdown($select, allText, items) {
    var previous = $select.val();
    $select.empty().append($('<option>').val('').text(allText));
    $.each(items || [], function (i, x) {
        $select.append($('<option>').val(x.code).text(x.name));
    });
    $select.val(previous);
    if ($select.val() === null) $select.val('');
}

function LoadDepartments() {
    $.ajax({
        type: 'GET',
        cache: false,
        url: '/Recruitment/RecruiterScreening/GetDepartments',
        dataType: 'json',
        data: { BusinessTypeCode: $('#rsBusinessType').val() },
        success: function (result) { FillDropdown($('#rsDepartment'), 'Department: All', result); },
        error: function (xhr) { AjaxError(xhr); }
    });
}

function LoadSections() {
    $.ajax({
        type: 'GET',
        cache: false,
        url: '/Recruitment/RecruiterScreening/GetSections',
        dataType: 'json',
        data: { BusinessTypeCode: $('#rsBusinessType').val(), DepartmentCode: $('#rsDepartment').val() },
        success: function (result) { FillDropdown($('#rsSection'), 'Section: All', result); },
        error: function (xhr) { AjaxError(xhr); }
    });
}
function OnJobChange() {
    LoadOpenVacancies();
}

function AjaxError(xhr) {
    if ($($.parseHTML(xhr.responseText)).filter(".Login_Session").length > 0) {
        window.location.reload();
    }
    else {
        document.body.id = "kt_body";
        $('body').html(xhr.responseText);
    }
}

// "Job" dropdown: jobs matching Business Type / Department / Section
function LoadJobs() {
    $.ajax({
        type: 'GET',
        cache: false,
        url: '/Recruitment/RecruiterScreening/GetJobs',
        dataType: 'json',
        data: {
            BusinessTypeCode: $('#rsBusinessType').val(),
            DepartmentCode: $('#rsDepartment').val(),
            SectionCode: $('#rsSection').val()
        },
        success: function (result) {
            var previous = $('#rsJob').val();
            var $job = $('#rsJob');
            $job.empty().append($('<option>').val('').text('All Jobs matching the filters above'));
            $.each(result || [], function (i, j) {
                $job.append($('<option>').val(j.code).text(j.name));
            });
            $job.val(previous);
            if ($job.val() === null) $job.val('');
        },
        error: function (xhr) { AjaxError(xhr); }
    });
}

var vacancyRequest = 0;

function LoadOpenVacancies(keepPage) {
    var requestId = ++vacancyRequest;
    $.ajax({
        type: 'GET',
        cache: false,
        url: '/Recruitment/RecruiterScreening/GetOpenVacancies',
        dataType: 'json',
        data: {
            BusinessTypeCode: $('#rsBusinessType').val(),
            DepartmentCode: $('#rsDepartment').val(),
            SectionCode: $('#rsSection').val(),
            JobCode: $('#rsJob').val()
        },
        success: function (result) {
            if (requestId !== vacancyRequest) return;   // a newer filter change is already loading
            openVacancies = result || [];
            if (!keepPage) vacancyPage = 1;   // keep the current page when coming back from a vacancy
            RenderVacancyTable();
        },
        error: function (xhr) { AjaxError(xhr); }
    });
}

var vacancyPage = 1;
var vacancyPageSize = 10;

function RenderVacancyTable() {
    var total = openVacancies.length;
    var pages = Math.max(1, Math.ceil(total / vacancyPageSize));
    if (vacancyPage > pages) vacancyPage = pages;

    var start = (vacancyPage - 1) * vacancyPageSize;
    var rows = [];

    $.each(openVacancies.slice(start, start + vacancyPageSize), function (n, v) {
        var i = start + n;   // index in the full list
        var mapped = v.mappedCount > 0
            ? '<span class="rs-mapped rs-mapped-yes">' + v.mappedCount + ' Mapped</span>'
            : '<span class="rs-mapped rs-mapped-no">0 Mapped</span>';

        rows.push('<tr onclick="OpenVacancy(' + i + ')">'
            + '<td class="rs-job">' + Esc(v.jobName) + '</td>'
            + '<td>' + Esc(v.branchName) + '</td>'
            + '<td>' + Esc(v.departmentName) + ' / ' + Esc(v.sectionName) + '</td>'
            + '<td>' + Esc(v.gradeName) + '</td>'
            + '<td>' + v.openings + '</td>'
            + '<td>' + mapped + '</td>'
            + '</tr>');
    });

    $('#rsVacancyRows').html(rows.join(''));
    $('#rsListEmpty').toggle(total === 0);
    RenderPager(total, pages, start);
}

function RenderPager(total, pages, start) {
    $('#rsPager').toggle(total > 0);
    $('#rsPagerInfo').text('Showing ' + (total ? start + 1 : 0) + ' to ' + Math.min(start + vacancyPageSize, total) + ' of ' + total + ' entries');

    var btn = function (label, page, disabled, active) {
        return '<button type="button" class="rs-page-btn' + (active ? ' active' : '') + '"'
            + (disabled ? ' disabled' : ' onclick="GoToVacancyPage(' + page + ')"') + '>' + label + '</button>';
    };

    var html = [btn('&laquo;', 1, vacancyPage === 1), btn('&lsaquo;', vacancyPage - 1, vacancyPage === 1)];
    var from = Math.max(1, vacancyPage - 2), to = Math.min(pages, vacancyPage + 2);
    for (var p = from; p <= to; p++) {
        html.push(btn(p, p, false, p === vacancyPage));
    }
    html.push(btn('&rsaquo;', vacancyPage + 1, vacancyPage === pages), btn('&raquo;', pages, vacancyPage === pages));
    $('#rsPagerButtons').html(html.join(''));
}

function GoToVacancyPage(page) {
    vacancyPage = page;
    RenderVacancyTable();
}

function OnPageSizeChange() {
    vacancyPageSize = parseInt($('#rsPageSize').val(), 10) || 10;
    vacancyPage = 1;
    RenderVacancyTable();
}
// Open one vacancy: show its details and applicants
function OpenVacancy(index) {
    var vacancy = openVacancies[index];
    if (!vacancy) return;

    currentVacancy = vacancy;
    selectedOrder = [];
    currentCandidateTab = 'LINKED';

    var meta = [vacancy.businessTypeName, vacancy.branchName,
                vacancy.departmentName + ' / ' + vacancy.sectionName, 'Grade: ' + vacancy.gradeName]
        .filter(function (x) { return x; }).join(' · ');

    $('#rsVacancyTitle').text(vacancy.jobName);
    $('#rsVacancyMeta').text(meta);

    if (vacancy.openings > 0) {
        $('#rsOpenings').text(vacancy.openings + ' Opening(s)').show();
    } else {
        $('#rsOpenings').hide();
    }

    $('#rsListView').hide();
    $('#rsDetailView').show();
    LoadCandidates();
}

function BackToVacancyList() {
    selectedOrder = [];
    $('#rsDetailView').hide();
    $('#rsListView').show();
    LoadOpenVacancies(true);   // refresh the "No. Mapped" counts
}

// ---- Applicants -----------------------------------------------------------
// Each applicant: { code, applicantID, applicantName, qualificationLevel, experience }

function LoadCandidates() {
  

    $.ajax({
        type: 'GET',
        url: '/Recruitment/RecruiterScreening/GetCandidates',
        dataType: 'json',
        data: {
            LocationCode: currentVacancy.locationCode,
            DepartmentCode: currentVacancy.departmentCode,
            SectionCode: currentVacancy.sectionCode,
            GradeCode: currentVacancy.gradeCode
        },
        success: function (result) {
            linkedCandidates = (result && result.linked) || [];
            suggestedCandidates = (result && result.suggested) || [];
            RenderCandidates();
        },
        error: function (xhr) { AjaxError(xhr); }
    });
}

function SwitchCandidateTab(tab) {
    currentCandidateTab = tab;
    RenderCandidates();
}

function RenderCandidates() {
    var linked = currentCandidateTab === 'LINKED';
    var list = linked ? linkedCandidates : suggestedCandidates;

    $('#rsTabLinked').toggleClass('active', linked);
    $('#rsTabSuggested').toggleClass('active', !linked);
    $('#rsLinkedCount').text(linkedCandidates.length);
    $('#rsSuggestedCount').text(suggestedCandidates.length);

    var html = [];
    $.each(list, function (i, c) {
        var order = selectedOrder.indexOf(c.code) + 1;
        var sub = [c.qualificationLevel, c.experience].filter(function (x) { return x; }).join(' · ');

        html.push('<div class="rs-row">');
        if (linked) {
            html.push('<div class="rs-check' + (order ? ' selected' : '') + '" onclick="ToggleCandidate(' + c.code + ')">' + (order || '') + '</div>');
        }
        html.push('<div class="rs-avatar">' + Initials(c.applicantName) + '</div>');
        html.push('<div class="rs-cand"><div class="rs-cand-name">' + Esc(c.applicantName) + '</div>'
            + '<div class="rs-cand-sub">' + Esc(sub) + '</div></div>');
        html.push('<span class="rs-match ' + (c.matchPercent >= 80 ? 'rs-match-high' : 'rs-match-mid') + '" title="AI-generated match score for this vacancy"><i class="fas fa-magic"></i>' + c.matchPercent + '% AI Match</span>');
        html.push('<button type="button" class="rs-icon-btn" title="View profile" onclick="ViewProfile(' + c.code + ')"><i class="far fa-user"></i></button>');
        html.push('<button type="button" class="rs-icon-btn" title="View resume" onclick="ViewResume(' + c.code + ')"><i class="far fa-file-alt"></i></button>');
        html.push('<button type="button" class="rs-icon-btn" title="Download resume" onclick="DownloadResume(' + c.code + ')"><i class="fas fa-download"></i></button>');
        if (!linked) {
            html.push('<button type="button" class="rs-map-btn" onclick="MapToVacancy(' + c.code + ')">Map to Vacancy</button>');
        }
        html.push('</div>');
    });
    $('#rsCandidateList').html(html.join(''));

    if (list.length === 0) {
        $('#rsEmpty').text(linked
            ? 'No applicants linked to this vacancy yet — map candidates from the Suggested tab.'
            : 'No suggested matches found for this vacancy right now.').show();
    } else {
        $('#rsEmpty').hide();
    }

    UpdateSelectedCount();
}

function ToggleCandidate(code) {
    var at = selectedOrder.indexOf(code);
    if (at >= 0) {
        selectedOrder.splice(at, 1);
    } else {
        selectedOrder.push(code);
    }
    RenderCandidates();
}

function UpdateSelectedCount() {
    $('#rsSelectedCount').text(selectedOrder.length);
}

function Initials(name) {
    var parts = $.trim(name || '').replace(/\./g, '').split(/\s+/);
    var first = parts[0] ? parts[0].charAt(0) : '';
    var last = parts.length > 1 ? parts[parts.length - 1].charAt(0) : '';
    return Esc((first + last).toUpperCase());
}

function Esc(text) {
    return $('<div>').text(text == null ? '' : text).html();
}

// TODO: wire these once the applicant profile / resume source is known.
function ViewProfile(code) { }
function ViewResume(code) { }
function DownloadResume(code) { }

function MapToVacancy(applicantCode) {
    // alert(currentVacancy.positionPlanCode);
    swal.fire({
        text: "Do you want to map this applicant to the vacancy?",
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
                url: '/Recruitment/RecruiterScreening/MapCandidate',
                dataType: 'json',
                data: {
                    ApplicantCode: applicantCode,
                    LocationCode: currentVacancy.locationCode,
                    DepartmentCode: currentVacancy.departmentCode,
                    SectionCode: currentVacancy.sectionCode,
                    GradeCode: currentVacancy.gradeCode,
                    PositionPlanCode: currentVacancy.positionPlanCode
                },
                success: function (result) {
                    hideSpinner();
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            LoadCandidates();
                        });
                    }
                    else {
                        swal.fire(result.message);
                    }
                },
                error: function (xhr) {
                    hideSpinner();
                    AjaxError(xhr);
                }
            });
        }
    });
}

// ---- Schedule interview ---------------------------------------------------
// Master interview panels: { code, name, interviewers: [{ name, role }] }
var interviewPanels = [];
var panelInterviewers = [];

function ScheduleInterview() {
    if (selectedOrder.length === 0) {
        swal.fire('Please select at least one candidate for interview.');
        return;
    }

    var html = [];
    $.each(selectedOrder, function (i, code) {
        var c = $.grep(linkedCandidates, function (x) { return x.code === code; })[0];
        if (c) {
            html.push('<div class="rs-modal-item"><span class="rs-modal-num">' + (i + 1) + '</span>' + Esc(c.applicantName) + '</div>');
        }
    });
    $('#rsScheduleList').html(html.join(''));

    LoadInterviewPanels();
    $('#rsScheduleModal').modal('show');
}

function LoadInterviewPanels() {
    interviewPanels = [];

    // Design preview only: ?sample=1
    // if (/[?&]sample=1/.test(window.location.search)) {
    //     interviewPanels = [{
    //         code: 1,
    //         name: 'Retail Sales \u2013 Senior Executive Panel',
    //         interviewers: [
    //             { name: 'Anjali Nair', role: 'Chairperson' },
    //             { name: 'Vinod Kumar', role: 'Department Head' },
    //             { name: 'Divya Menon', role: 'Technical Panelist' }
    //         ]
    //     }];
    //     FillPanelDropdown();
    //     return;
    // }

    // Master interview panels set up for this vacancy's branch / department / section / grade.
    // TODO: the panel members (interviewers) are not loaded yet - they need the panel members table.
    $.ajax({
        type: 'GET',
        url: '/Recruitment/RecruiterScreening/GetInterviewPanels',
        dataType: 'json',
        data: {
            LocationCode: currentVacancy.locationCode,
            DepartmentCode: currentVacancy.departmentCode,
            SectionCode: currentVacancy.sectionCode,
            GradeCode: currentVacancy.gradeCode
        },
        success: function (result) {
            interviewPanels = $.map(result || [], function (p) {
                return { code: p.code, name: p.name, interviewers: [] };
            });
            FillPanelDropdown();
        },
        error: function (xhr) { AjaxError(xhr); }
    });
}

function FillPanelDropdown() {
    var $panel = $('#rsPanel');
    $panel.empty().append($('<option>').val('').text('Select'));
    $.each(interviewPanels, function (i, p) {
        $panel.append($('<option>').val(p.code).text(p.name));
    });
    OnPanelChange();
}
function OnPanelChange() {
    var code = $('#rsPanel').val();
    var panel = $.grep(interviewPanels, function (p) { return String(p.code) === String(code); })[0];
    panelInterviewers = panel ? panel.interviewers.slice() : [];
    RenderInterviewers();
}

function RenderInterviewers() {
    var html = [];
    $.each(panelInterviewers, function (i, p) {
        html.push('<span class="rs-chip"><b>' + Esc(p.name) + '</b> · <b>' + Esc(p.role) + '</b>'
            + '<span class="rs-chip-x" onclick="RemoveInterviewer(' + i + ')"><i class="fas fa-times"></i></span></span>');
    });
    $('#rsInterviewers').html(html.join(''));
    $('#rsInterviewersBox').toggle(panelInterviewers.length > 0);
}

function RemoveInterviewer(index) {
    panelInterviewers.splice(index, 1);
    RenderInterviewers();
}

function CompleteScheduling() {
    // TODO: save the interview scheduling requests (selectedOrder + panelInterviewers) once the backend is available.
    $('#rsScheduleModal').modal('hide');
}

// Copy the colour of a normal button (btn-default) so selection circles and the active tab match the active theme
function UseButtonColour() {
    var colour = $('#rsColorProbe').css('background-color');
    if (colour && colour !== 'rgba(0, 0, 0, 0)' && colour !== 'transparent') {
        document.documentElement.style.setProperty('--rs-accent', colour);
    }
}
