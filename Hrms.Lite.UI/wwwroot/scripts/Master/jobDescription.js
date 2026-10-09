function onAddClick() {
    var url = "/Master/JobDescription/Create";
    window.location.href = url;
}

var prevTab = 'ActiveTab';
var currentTab = 'ACTIVE';

function OnTabClick(Tab, TabIndex) {
    showSpinner();
    currentTab = TabIndex;
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");
    $('#tabsDropdownJobDescription').val(currentTab);

    $.ajax({

        type: 'GET',
        url: '/Master/JobDescription/GetJobDescriptionList',
        dataType: 'html',
        data: { TabIndex: TabIndex },

        success: function (result) {
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#JobDescriptionList').html("");
                $('#JobDescriptionList').html(result);
            }
            hideSpinner();
        },

        error: function (xhr, textStatus, errorThrown) {
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
            hideSpinner();
        }
    });

    prevTab = Tab;
}

function tabClick(val) {
    showSpinner();
    currentTab = $(val).val();
    $("." + prevTab).removeClass("active");
    if (currentTab == "ACTIVE")
        prevTab = 'ActiveTab';
    else if (currentTab == "INACTIVE")
        prevTab = 'InActiveTab';
    else
        prevTab = 'PendingTab';

    $("." + prevTab).addClass("active");
    $.ajax({

        type: 'GET',
        url: '/Master/JobDescription/GetJobDescriptionList',
        dataType: 'html',
        data: { TabIndex: currentTab },

        success: function (result) {
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#JobDescriptionList').html("");
                $('#JobDescriptionList').html(result);
            }
            hideSpinner();
        },

        error: function (xhr, textStatus, errorThrown) {
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
            hideSpinner();
        }
    });
}


/* ---- Master Job select + rich text editor (Create/Modify view) ---- */

/* Department/Section/Grade/Business type are attributes of the selected
   master job, fetched from the server whenever the selection changes. */
function jdLoadMasterJobSnapshot(code) {
    if (!code || code == '0') {
        $('#jdSnapshot').prop('hidden', true);
        return;
    }
    $.ajax({
        type: 'GET',
        url: '/Master/JobDescription/GetMasterJobDetails',
        dataType: 'json',
        data: { PositionPlanCode: code },
        success: function (result) {
            if (!result) {
                $('#jdSnapshot').prop('hidden', true);
                return;
            }
            $('#jdValDept').text((result.department && result.department.name) || '—');
            $('#jdValSection').text((result.section && result.section.name) || '—');
            $('#jdValGrade').text((result.grade && result.grade.name) || '—');
            $('#jdValBiz').text((result.businessType && result.businessType.name) || '—');
            $('#BusinessTypeCode').val(result.businessType.code) ;
            $('#DepartmentCode').val(result.department.code);
            $('#SectionCode').val(result.section.code);
            $('#GradeCode').val(result.grade.code);
            $('#jdSnapshot').prop('hidden', false);
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

// ----masterjob onchange
function onMasterJobChange(select) {
    clearMjError();
    jdLoadMasterJobSnapshot(select.value);
}

function initJobDescriptionEditor(selectedCode, selectedName) {
   
    var $editor = $('#jdEditor');

    if (!$editor.length) return;

    updateJdWordCount();
    jdCleanMarkdownArtifacts($editor);

    if (selectedCode) {
        jdLoadMasterJobSnapshot(selectedCode);
    }

    $('#jdToolbar .jdform-tb-btn').off('click').on('click', function (e) {
        e.preventDefault();
        var cmd = $(this).data('cmd');
        $editor.focus();
        document.execCommand(cmd, false, null);
        updateJdWordCount();
    });

    $editor.off('input').on('input', function () {
        syncJdDescription();
        updateJdWordCount();
        if ($editor.text().trim().length) clearDescError();
    });

    $editor.off('paste').on('paste', function (e) {
        e.preventDefault();
        var clipboardData = (e.originalEvent || e).clipboardData || window.clipboardData;
        var text = jdNormalizeWordText(clipboardData.getData('text/plain'));
        document.execCommand('insertHTML', false, jdMarkdownTextToHtml(text));
        syncJdDescription();
        updateJdWordCount();
        clearDescError();
    });

    $('#jdClearBtn').off('click').on('click', function () {
        $editor.html('');
        syncJdDescription();
        updateJdWordCount();
        $editor.focus();
    });
}

function jdEscapeHtml(s) {
    return s.replace(/[&<>]/g, function (c) { return { '&': '&amp;', '<': '&lt;', '>': '&gt;' }[c]; });
}

/* AI-generated job descriptions are almost always Markdown ("# Heading",
   "- bullet", "**bold**"). This editor has no Markdown support, so a pasted
   "# Job Description" used to land as literal '#' text (and get saved that
   way). Turn the recognised bits into real formatting on paste instead. */
function jdInlineMarkdown(s) {
    return jdEscapeHtml(s).replace(/\*\*([^*]+)\*\*/g, '<strong>$1</strong>');
}

function jdMarkdownTextToHtml(text) {
    var lines = text.replace(/\r\n/g, '\n').split('\n');
    var htmlParts = [];
    var paraLines = [];
    var bulletItems = null;

    function flushPara() {
        if (paraLines.length) {
            htmlParts.push('<p>' + paraLines.join('<br>') + '</p>');
            paraLines = [];
        }
    }
    function flushBullets() {
        if (bulletItems) {
            htmlParts.push('<ul>' + bulletItems.map(function (i) { return '<li>' + i + '</li>'; }).join('') + '</ul>');
            bulletItems = null;
        }
    }

    lines.forEach(function (line) {
        if (!line.trim()) { flushPara(); flushBullets(); return; }

        var heading = line.match(/^#{1,6}\s+(.*)$/);
        if (heading) {
            flushPara(); flushBullets();
            htmlParts.push('<p><strong>' + jdInlineMarkdown(heading[1]) + '</strong></p>');
            return;
        }

        var bullet = line.match(/^[-*]\s+(.*)$/);
        if (bullet) {
            flushPara();
            if (!bulletItems) bulletItems = [];
            bulletItems.push(jdInlineMarkdown(bullet[1]));
            return;
        }

        flushBullets();
        paraLines.push(jdInlineMarkdown(line));
    });
    flushPara();
    flushBullets();
    return htmlParts.join('') || '<p></p>';
}

/* One-time repair for content saved before Markdown handling existed: strips
   a leading '#'/'##'/'- ' marker off each top-level block loaded into the
   editor (e.g. in Modify mode) and turns headings bold, without touching
   any other formatting already in the block. */
function jdCleanMarkdownArtifacts($editor) {
    var changed = false;
    $editor.children().each(function () {
        var $el = $(this);
        var text = $el.text();

        var heading = text.match(/^#{1,6}\s+/);
        if (heading) {
            $el.text(text.slice(heading[0].length)).wrapInner('<strong></strong>');
            changed = true;
            return;
        }

        var bullet = text.match(/^[-*]\s+/);
        if (bullet) {
            $el.text(text.slice(bullet[0].length));
            changed = true;
        }
    });
    if (changed) syncJdDescription();
}

/* Word paste brings in curly quotes/dashes/nbsp that the backend's invalid-
   character check rejects even though the text itself is clean. Flatten
   them to plain ASCII so a Word-sourced description saves normally. */
function jdNormalizeWordText(s) {
    return s
        .replace(/[‘’‚‛]/g, "'")
        .replace(/[“”„‟]/g, '"')
        .replace(/[–—]/g, '-')
        .replace(/…/g, '...')
        .replace(/[  -​]/g, ' ')
        .replace(/[•●▪]/g, '-');
}



function updateJdWordCount() {
    var text = $('#jdEditor').text().trim();
    var n = text.length ? text.split(/\s+/).length : 0;
    $('#jdWordCount').text(n + (n === 1 ? ' word' : ' words'));
}

function clearMjError() {
    $('#MasterJob_Code').removeClass('is-invalid');
    $('#mjError').removeClass('error-message').html('');
}

function clearDescError() {
    $('#jdEditorShell').removeClass('error-message');
    $('#descError').removeClass('error-message').html('');
}
function syncJdDescription() {
    $('#JDescription').val($('#jdEditor').html());
}
function validateJD() {

    var ok = true;

    if (!$('#MasterJob_Code').val()) {
        ok = false;
    }
    if (!$('#jdEditor').text().trim().length) {
        $('#jdEditorShell').addClass('error-message');
        $('#descError').addClass('error-message').html('Add a job description before saving.');
        ok = false;
    }

    return ok;
}



function saveJobDescription(mode) {

    if (validateJD()) {
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
                    url: '/Master/JobDescription/Create',
                    dataType: 'json',
                    data: $('#JobDescriptionSave').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {

                                window.location.href = "/Master/JobDescription/Index";

                            });

                        }
                        else {
                            swal.fire(result.message).then(function () {

                            });
                        }

                    },
                    error: function (xhr, textStatus, errorThrown) {
                        document.body.id = "kt_body";
                        $('body').html(xhr.responseText);
                    }
                });
            }
            else {
                return false;
            }
        });
    }
    else {
        return false;
    }
}

function onEditClick(GI, LogGI, index) {

    if ($("#JobDescriptionList_" + index + "__EditActive").val() == 'True') {
        swal.fire(ApprovalPendingMsg);
    }
    else {
        var url = "/Master/JobDescription/Modify?JobDescriptionGI=" + GI + "&LogGI=" + LogGI;
        window.location.href = url;
    }
}

function onDeleteClick(GI, LogGI, index) {

    if ($("#JobDescriptionList_" + index + "__EditActive").val() == 'True') {
        swal.fire(ApprovalPendingMsg);
    }
    else {
        $.ajax({
            type: 'GET',
            url: '/Master/JobDescription/Delete',
            dataType: 'html',
            data: { JobDescriptionGI: GI, LogGI: LogGI },
            success: function (result) {
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('#DeleteSlider').html("");
                    $('#DeleteSlider').html(result);
                }
            },
            error: function (xhr, textStatus, errorThrown) {
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);
            }
        });
    }
}

function confirmDelete(GI, LogGI) {
    swal.fire({
        text: "Do you want to Delete?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({

                type: 'POST',
                url: '/Master/JobDescription/Delete',
                dataType: 'json',
                data: $('#DeleteJobDescription').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('delete');
                            OnTabClick(prevTab, currentTab);
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/JobDescription/Delete?JobDescriptionGI=" + GI + "&LogGI=" + LogGI;
                            $('#DeleteSlider').load(url, function () { });
                            $('body').append('<div class="form-overlay"></div>');
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
        else {
            return false;
        }
    });
}

function onApproveClick(GI, LogGI, index) {

    if ($("#JobDescriptionList_" + index + "__MakerChecker").val() == 'True') {
        swal.fire(MakerCheckerMsg);
    }
    else {
        $.ajax({
            type: 'GET',
            url: '/Master/JobDescription/Approve',
            dataType: 'html',
            data: { JobDescriptionGI: GI, LogGI: LogGI },
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

function confirmApprove(GI, LogGI) {
    swal.fire({
        text: "Do you want to Approve?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({

                type: 'POST',
                url: '/Master/JobDescription/Approve',
                dataType: 'json',
                data: $('#approve').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('Approve');
                            OnTabClick(prevTab, currentTab);
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/JobDescription/Approve?JobDescriptionGI=" + GI + "&LogGI=" + LogGI;
                            $('#ApproveSlider').load(url, function () { });
                            $('body').append('<div class="form-overlay"></div>');
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
        else {
            return false;
        }
    });
}

function confirmReject(GI, LogGI) {
    swal.fire({
        text: "Do you want to Reject?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({

                type: 'POST',
                url: '/Master/JobDescription/Reject',
                dataType: 'json',
                data: $('#approve').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('Approve');
                            OnTabClick(prevTab, currentTab);
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/JobDescription/Approve?JobDescriptionGI=" + GI + "&LogGI=" + LogGI;
                            $('#ApproveSlider').load(url, function () { });
                            $('body').append('<div class="form-overlay"></div>');
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
        else {
            return false;
        }
    });
}

function ActiveInactiveValidation(name, code) {
    $.ajax({

        type: 'POST',
        url: '/Master/JobDescription/GetActiveInactiveValidation?name=' + name + '&code=' + code,
        dataType: 'json',

        success: function (result) {
            if (result.success == true) {
                // nothing to do
            }
            else {
                swal.fire(result.message).then(function () {
                    $("#ActiveInactiveJD").prop("checked", true);
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

function clickOnExportToExcel() {

    var TableLength = $('#JobDescriptionList_Count').val();
    if (TableLength > 0) {
        var url = "/Master/JobDescription/ExportToExcel?TabIndex=" + currentTab;
        window.location.href = url;
    }
    else {
        swal.fire(NoDataFoundMsg);
    }
}
