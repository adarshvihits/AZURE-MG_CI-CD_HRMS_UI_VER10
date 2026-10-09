var currentTab = 'ACTIVE';

// Shows the selected planning period under "Approved Plan Lines"
function updatePeriodLabel() {
    var $sel = $('#PlanningPeriod_Code');
    var text = $sel.val() ? $sel.find('option:selected').text().trim() : 'All periods';
    $('#approvedPlanPeriod').text(text);
}

document.addEventListener('DOMContentLoaded', updatePeriodLabel);

function GetList() {
    showSpinner();
    //var branch = $('#Branch_Code').val();
    var planningPeriod = $('#PlanningPeriod_Code').val();

    $.ajax({

        type: 'GET',
        url: '/Master/NewBranchPositionCreation/GetNewBranchPostionCreationList',
        dataType: 'html',
        data: { TabIndex: currentTab,   PlanningPeriod: planningPeriod },

        success: function (result) {
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#activeList').html("");
                $('#activeList').html(result);
                updatePeriodLabel();
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

function ShowActiveList() {
    currentTab = 'ACTIVE';
    GetList();
}

function ShowPendingList() {
    currentTab = 'APPROVAL_PENDING';
    GetList();
}

function onExportToExcelClick() {
    var rowCount = $('#activeTable tbody tr td[data-label]').closest('tr').length;
    if (rowCount > 0) {
        var url = "/Master/NewBranchPositionCreation/ExportToExcel?TabIndex=" + currentTab +
            "&PlanningPeriod=" + ($('#PlanningPeriod_Code').val() || "");
        window.location.href = url;
    }
    else {
        swal.fire(NoDataFoundMsg);
    }
}



function renderJdContext(branch, result) {
    var items = [
        ['Branch', branch],
        ['Department', result && result.department && result.department.name],
        ['Section', result && result.section && result.section.name],
        ['Grade', result && result.grade && result.grade.name],
        ['Business Type', result && result.businessType && result.businessType.name]
    ];

    var html = [];
    items.forEach(function (it) {
        if (it[1]) {
            html.push('<span class="jd-ctx-item"><b>' + it[0] + ':</b>' + $('<div>').text(it[1]).html() + '</span>');
        }
    });

    $('#jdContext').html(html.join('<span class="jd-ctx-sep">|</span>'));
}

function loadPopupJobDescription(selectedCode, branchName) {

    var $editor = $('#jdEditor');

    if (!$editor.length) return;

    renderJdContext(branchName, null);
    $editor.removeClass('jd-none').html('Loading...');

    $.ajax({
        type: 'GET',
        url: '/Master/NewBranchPositionCreation/GetMasterJobDetails',
        dataType: 'json',
        data: {
            PlanCode: selectedCode
        },

        success: function (result) {

            renderJdContext(branchName, result);

            var $reportsTo = $('#prReportsTo');
            $reportsTo.empty().append($('<option>').val('').text('-- Select --'));
            if (result && result.parentPositions) {
                $.each(result.parentPositions, function (i, p) {
                    $reportsTo.append($('<option>').val(p.code).text(p.name));
                });
            }

            if (result.openingDateText) {

                $("#prTargetDate")
                    .val(result.openingDateText);

            } else {

                $("#prTargetDate")
                    .val("");
            }



            if (!result ||
                !result.jDescription ||
                !result.jDescription.trim()) {

                var parts = [];
                if (result) {
                    [result.department, result.section, result.grade].forEach(function (x) {
                        if (x && x.name) parts.push($('<div>').text(x.name).html());
                    });
                }
                var label = parts.length ? ' for ' + parts.join(' &middot; ') : '';

                $editor.addClass('jd-none').html(`
                    <div class="jd-empty">
                        <div class="jd-empty-icon">&#129302;</div>
                        <div class="jd-empty-title">No job description yet</div>
                        <div class="jd-empty-text">
                            Generate one on the Job Description master page${label}.
                        </div>
                        <a href="/Master/JobDescription/Create" class="btn btn-default jd-empty-btn">
                            &#10024; Generate JD
                        </a>
                    </div>
                `);

                $('#jdPopupWordCount').text('0 words');

                return;
            }


            $editor.html(result.jDescription);

            // updatePopupJdWordCount();
        },

        error: function () {

            $editor.removeClass('jd-none').html(
                '<span class="text-danger">Unable to load Job Description.</span>'
            );

            $('#jdPopupWordCount').text('0 words');
        }
    });
}
function updatePopupJdWordCount() {

    var text = $('#jdPopupEditor')
        .text()
        .trim();

    var count = text
        ? text.split(/\s+/).length
        : 0;

    $('#jdPopupWordCount').text(count + ' words');
}

function openPositionPopup(
    PlanCode,
    PlanCount,
    AvgSalary,
    TotalSalary,
    BranchName
) {
    $("#prPlancode").val(PlanCode);

    $("#prCount").val(PlanCount || 1);

    // Reset priority on every open so a previous selection doesn't carry over
    $("#prPriority").val("");

    $("#prAvgSalary").text(
        "₹ " + Number(AvgSalary || 0).toLocaleString("en-IN")
    );

    $("#prTotalSalary").text(
        "₹ " + Number(TotalSalary || 0).toLocaleString("en-IN")
    );

    loadPopupJobDescription(PlanCode, BranchName);

    $("#positionRequestModal").modal("show");
}

 

function validatePositionRequest() {
    var errors = [];

    if (!$('#prPlancode').val()) {
        errors.push('Plan code is missing. Please reopen the request.');
    }

    var count = $('#prCount').val();
    if (!count || isNaN(count) || parseInt(count) <= 0) {
        errors.push('Positions requested must be greater than zero.');
    }

    if (!$('#prPriority').val()) {
        errors.push('Please select a priority.');
    }

    if (!$('#prTargetDate').val()) {
        errors.push('Please select a target fill date.');
    }

    // No JD (empty-state shown, still loading, or failed to load)
    var $jd = $('#jdEditor');
    if ($jd.hasClass('jd-none') || !$jd.text().trim() || $jd.find('.text-danger').length || $jd.text().trim() === 'Loading...') {
        errors.push('Job description is not available. Please create the Job Description first.');
    }

    return errors;
}

function savePositionRequest() {
    var errors = validatePositionRequest();
    
    if (errors.length > 0) {
        swal.fire({
            icon: 'warning',
            html: errors.join('<br/>')
        });
        return false;
    }

    swal.fire({
        text: "Do you want to Save?",
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
                url: '/Master/NewBranchPositionCreation/SavePositionRequest',
                dataType: 'json',
                data: {
                    'PlanCode': $('#prPlancode').val(),
                    'PositionsRequested': $('#prCount').val(),
                    'Priority': $('#prPriority').val(),
                    'TargetFillDate': $('#prTargetDate').val(),
                    'ReportsTo': $('#prReportsTo').val()
                },
                success: function (result) {
                    hideSpinner();
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            $('#positionRequestModal').modal('hide');
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
    })
}
