//----------Applicants--------------------

var prevApplicantsTab = 'ActiveApplicantsTab';
var currentApplicantsTab = 1;
var quickViewApplicantGI = '';
var applicantsTable;

// dd MMM yyyy, blank for empty / DateTime.MinValue
function formatApplicantDate(value) {
    if (!value || value.indexOf('0001-01-01') == 0)
        return '';
    var date = new Date(value);
    if (isNaN(date.getTime()))
        return '';
    var months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
    return ('0' + date.getDate()).slice(-2) + ' ' + months[date.getMonth()] + ' ' + date.getFullYear();
}

function applicantInitials(name) {
    return $.trim(name || '').split(/\s+/).slice(0, 2).map(function (part) { return part.charAt(0).toUpperCase(); }).join('');
}

function escapeApplicantText(value) {
    return $('<div>').text(value || '').html();
}

function fillApplicantsList() {
    applicantsTable = $("#ApplicantsTable").DataTable({
        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/Recruitment/Applicants/GetApplicantsList",
            "type": "POST",
            "datatype": "json",
            'data':
                function (d) {
                    var searchValue = encodeURIComponent($('#dataTableSearch').val());
                    // leading '&' so the first field is not absorbed into the JSON prefix key
                    d.filter = '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&TabIndex=' + currentApplicantsTab + '&dummy=dummy';
                    return JSON.stringify(d);
                },
            "dataSrc": function (d) {
                $('.ActiveApplicantsCount').html(d.activeCount);
                $('.ShortlistedApplicantsCount').html(d.shortlistedCount);
                $('.EvaluationApplicantsCount').html(d.evaluationCount);
                return d.data || [];
            },
            "error": function (xhr, textStatus, errorThrown) {
                if ($($.parseHTML(xhr.responseText)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    document.body.id = "kt_body";
                    $('body').html(xhr.responseText);
                }
            }
        },
        "columns": [
            { "data": "applicantCode", "render": function (data) { return '<span class="cell-primary">' + escapeApplicantText(data) + '</span>'; } },
            { "data": "applicationDate", "render": function (data) { return formatApplicantDate(data); } },
            {
                "data": "applicantName", "render": function (data) {
                    return '<div class="apl-name-cell"><div class="apl-avatar">' + applicantInitials(data) + '</div>' +
                        '<span class="cell-primary">' + escapeApplicantText(data) + '</span></div>';
                }
            },
            { "data": "dob", "render": function (data) { return formatApplicantDate(data); } },
            { "data": "gender", "render": escapeApplicantText },
            { "data": "qualification", "render": escapeApplicantText },
            { "data": "experience", "render": escapeApplicantText },
            { "data": "mobile", "render": escapeApplicantText },
            { "data": "email", "render": escapeApplicantText },
            { "data": "nationality", "render": escapeApplicantText },
            { "data": "residence", "render": escapeApplicantText },
            { "data": "languagesKnown", "render": escapeApplicantText },
            {
                "data": "resume", "render": function (data) {
                    if (!data)
                        return '';
                    return '<a title="View Uploaded Resume" class="btn btn-icon btn-edit icon-only apl-resume" data-url="' + escapeApplicantText(data) + '">' +
                        '<i class="icon-xl far fa-file-alt"></i></a>';
                }
            },
            { "data": null, "render": function () { return '<i class="fas fa-chevron-right apl-chevron"></i>'; } }
        ],
        "createdRow": function (row) {
            $(row).addClass('apl-row');
        },
        "dom": 'rt<"bottom"><"my-wrapper"<"my-inner2"l><"my-inner1"i><"my-inner3"p>>',
        "serverSide": "true",
        "scrollY": '58vh',
        "sScrollX": "100%",
        "sScrollXInner": "100%",
        "ordering": false,
        "processing": "true",
        "language": {
            processing: '<img width="100%" src="/images/Loader_img1 (7).gif">',
            emptyTable: '<div id="noDataFound" style="text-align: center;display: flex;align-items: center;justify-content: center;height: 45vh;"><img src="/images/nodata.png" /></div >',
            zeroRecords: '<div id="noDataFound" style="text-align: center;display: flex;align-items: center;justify-content: center;height: 45vh;"><img src="/images/nodata.png" /></div >',
            sLengthMenu: "_MENU_",
            paginate: {
                next: '<img width="100%" src="/images/icons/Icon awesome-arrow-right-1.png" >',
                previous: '<img src="/images/icons/Icon awesome-arrow-right.png">',
                first: '<img src="/images/icons/Icon metro-next-1.png">',
                last: '<img src="/images/icons/Icon metro-next.png">'
            }
        }
    });

    $('.ihits-table table').removeClass("dataTable no-footer");

    $("#dataTableSearch").off(".applicants").on("keyup.applicants search.applicants input.applicants paste.applicants cut.applicants", function () {
        applicantsTable.search(this.value).draw();
        $('#noDataFound').css("display", 'none');
    });

    // namespaced so the layout's search-box expand toggle (custom.js) is not unbound
    $('.search-slider-inner').off('click.applicants').on('click.applicants', function () {
        var hadSearch = $('#dataTableSearch').val() != '';
        $('#dataTableSearch').val('');
        $('#dataTableSearch').focus();
        if (hadSearch)
            applicantsTable.search('').draw();
    });

    // row click opens the quick view, resume icon opens the uploaded resume
    $('#ApplicantsTable tbody').off('click.applicants')
        .on('click.applicants', 'a.apl-resume', function (e) {
            e.stopPropagation();
            window.open($(this).attr('data-url'), '_blank');
        })
        .on('click.applicants', 'tr.apl-row', function () {
            openApplicantQuickView(applicantsTable.row(this).data());
        });

    $.fn.DataTable.ext.pager.numbers_length = 5;
    setTimeout(tooltipHover, 500);
}

function onApplicantsTabClick(Tab, TabIndex) {
    currentApplicantsTab = TabIndex;
    $("." + prevApplicantsTab).removeClass("active");
    $("." + Tab).addClass("active");
    prevApplicantsTab = Tab;
    $('#tabsDropdownApplicants').val(TabIndex);
    // back to page 1 of the selected tab
    if (applicantsTable)
        applicantsTable.page('first').draw('page');
    else
        fillApplicantsList();
}

function onApplicantsDropdownChange(val) {
    var tabs = { 1: 'ActiveApplicantsTab', 2: 'ShortlistedApplicantsTab', 3: 'EvaluationApplicantsTab' };
    var index = parseInt($(val).val());
    onApplicantsTabClick(tabs[index], index);
}

//----------Quick view--------------------

function openApplicantQuickView(a) {
    if (!a)
        return;
    quickViewApplicantGI = a.resumeDataBankGI;
    $('#qvInitials').text(applicantInitials(a.applicantName));
    $('#qvName').text(a.applicantName || '');
    $('#qvCode').text(a.applicantCode || '');
    $('#qvApplied').text(formatApplicantDate(a.applicationDate) || '-');
    $('#qvDob').text(formatApplicantDate(a.dob) || '-');
    $('#qvGender').text(a.gender || '-');
    $('#qvQualification').text(a.qualification || '-');
    $('#qvExperience').text(a.experience || '-');
    $('#qvMobile').text(a.mobile || '-');
    $('#qvEmail').text(a.email || '-');
    $('#qvNationality').text(a.nationality || '-');
    $('#qvResidence').text(a.residence || '-');
    $('#qvLanguages').text(a.languagesKnown || '-');

    $(".form-overlay").remove();
    $('body').append('<div class="form-overlay"></div>');
    $('.form-overlay').on('click', closeApplicantQuickView);
    $('#ApplicantQuickViewSlider').addClass('slider-forms-open');
}

function closeApplicantQuickView() {
    CloseSlider('ApplicantQuickView');
}

// Export to PDF - Telerik report Reports/ApplicantProfile.trdp (same as Employee Profile's PDF icon)
function exportApplicantProfilePdf() {
    if (quickViewApplicantGI && quickViewApplicantGI != '00000000-0000-0000-0000-000000000000')
        window.location.href = '/Recruitment/Applicants/ApplicantProfilePrint?ApplicantGI=' + quickViewApplicantGI;
    else
        swal.fire('Profile is not available for this applicant');
}

// full profile opens in the same window ("Back to Applicants" returns to the list)
function viewApplicantFullProfile() {
    if (quickViewApplicantGI && quickViewApplicantGI != '00000000-0000-0000-0000-000000000000')
        window.location.href = '/Recruitment/Applicants/Profile?ApplicantGI=' + quickViewApplicantGI;
    else
        swal.fire('Full profile is not available for this applicant');
}
