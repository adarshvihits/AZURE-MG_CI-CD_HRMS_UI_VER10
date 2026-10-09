var prevTab = 'ActiveTab';
var currentTab = 'ACTIVE';

function OnTabClick(Tab, TabIndex) {
    showSpinner();

    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");
    var url = "/Recruitment/Recruitmentdrive/GetList";
    $.ajaxSetup({
        async: false
    });
    $('#recruitmentdrivelist').load(url, { TabIndex: TabIndex }, function () { hideSpinner(); });
    prevTab = Tab;
    currentTab = TabIndex;
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
        url: '/Recruitment/Recruitmentdrive/GetList',
        dataType: 'html',
        data: { TabIndex: currentTab },
        success: function (result) {
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#recruitmentdrivelist').html("");
                $('#recruitmentdrivelist').html(result);
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

function onAddNewClick() {
    var url = "/Recruitment/Recruitmentdrive/AddNew";
    window.location.href = url;
}

/* ---------- Post tab (expense grid) ---------- */

$(document).ready(function () {
    if ($('#costSplitTable').length) {
        if (typeof savedRows !== 'undefined') {
            savedRows.forEach(function (r) { addCostRow(r.code, r.amount); });
        }
        addCostRow();   // empty row ready for input
    }
});

function addCostRow(typeCode, amount) {
    var sel = $('#costTypeTemplate').clone().removeAttr('id').removeAttr('name').removeAttr('style').removeAttr('disabled')
        .addClass('form-control form-control-sm exp-type');
    sel.val(typeCode ? typeCode : '');
    var amt = $('<input type="text" class="form-control form-control-sm exp-amt" maxlength="10" placeholder="Amount" onkeypress="return onlyAmount(event);" />');
    if (amount != null) amt.val(parseFloat(amount).toFixed(2));

    var row = $('<tr>');
    row.append($('<td data-label="Expense Type">').append(sel));
    row.append($('<td data-label="Amount">').append(amt));
    row.append('<td data-label="Actions" class="row-actions"></td>');
    $('#costSplitTable tbody').append(row);

    sel.on('change', refreshCostRows);
    amt.on('input', refreshCostRows);
    refreshCostRows();
}

function onAddCostClick() {
    var last = $('#costSplitTable tbody tr:last');
    var typeCode = last.find('.exp-type').val();
    var amount = parseFloat(last.find('.exp-amt').val());

    if (!typeCode) {
        swal.fire("Please select an expense type.");
        return;
    }
    if (isNaN(amount) || amount <= 0) {
        swal.fire("Please enter a valid amount.");
        return;
    }

    var duplicate = false;
    $('#costSplitTable tbody tr').not(last).each(function () {
        if ($(this).find('.exp-type').val() == typeCode) duplicate = true;
    });
    if (duplicate) {
        swal.fire("This expense type is already added.");
        return;
    }
    addCostRow();   // next line
}

function onRemoveCostClick(sender) {
    $(sender).closest('tr').remove();
    refreshCostRows();
}

// Names only the complete rows (so they bind to CostSplitUpList[i]), shows Add on the last row, and updates the total
function refreshCostRows() {
    var total = 0, idx = 0;
    var rows = $('#costSplitTable tbody tr');
    rows.each(function (i) {
        var sel = $(this).find('.exp-type');
        var amt = $(this).find('.exp-amt');
        var value = parseFloat(amt.val());

        if (sel.val() && !isNaN(value) && value > 0) {
            sel.attr('name', 'CostSplitUpList[' + idx + '].CostHead.Code');
            amt.attr('name', 'CostSplitUpList[' + idx + '].Amount');
            total += value;
            idx++;
        }
        else {
            sel.removeAttr('name');
            amt.removeAttr('name');
        }

        var isLast = (i == rows.length - 1);
        var actions = $(this).find('.row-actions');
        if (actions.data('last') !== isLast) {
            actions.data('last', isLast);
            actions.html(isLast
                ? '<button type="button" class="btn btn-default" onclick="onAddCostClick()">Add</button>'
                : '<a title="Remove" type="button" class="btn btn-icon icon-only btn-delete" onclick="onRemoveCostClick(this)"><i class="icon-xl far fa-trash-alt"></i></a>');
        }
    });
    $('#costTotal').text(total.toFixed(2));
}

/* ---------- Save ---------- */

function validateform() {
    var flag = true;
    var i = 0;
    if (!validateById('#Name', 'SName')) {
        i++;
    }
    if (!validateById('#LocationName', 'SLocation')) {
        i++;
    }
    if (!validateById('#Venue_Code', 'SVenue')) {
        i++;
    }
    if (!validateById('#DateFrom', 'SFrom')) {
        i++;
    }
    if (!validateById('#DateTo', 'STo')) {
        i++;
    }
    if (i == 0) {
        if ($('#DateTo').val() < $('#DateFrom').val()) {
            swal.fire("To date cannot be before From date");
            flag = false;
        }
        else {
            var incomplete = false, duplicate = false, seen = {};
            $('#costSplitTable tbody tr').each(function () {
                var t = $(this).find('.exp-type').val();
                var a = parseFloat($(this).find('.exp-amt').val());
                if ((t && !(a > 0)) || (!t && a > 0)) incomplete = true;
                if (t && a > 0) {
                    if (seen[t]) duplicate = true;
                    seen[t] = true;
                }
            });
            if (incomplete) {
                swal.fire("Please complete or clear the expense row");
                flag = false;
            }
            else if (duplicate) {
                swal.fire("An expense type can be added only once");
                flag = false;
            }
            else if ($('#Mode').val() == 'CREATE' && $('#costSplitTable .exp-type[name]').length == 0) {
                swal.fire("Please add at least one expense");
                flag = false;
            }
        }
    }
    else {
        flag = false;
    }
    return flag;
}

function SaveRecruitmentdrive(mode) {
    refreshCostRows();

    if (validateform()) {
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
                    url: '/Recruitment/Recruitmentdrive/Save',
                    dataType: 'json',
                    data: $('#recruitment-drive-save').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            if (mode == 'Modify') {
                                swal.fire("Modified Successfully").then(function () {
                                    var url = "/Recruitment/Recruitmentdrive/Index";
                                    window.location.href = url;
                                });
                            }
                            else {
                                swal.fire("Saved Successfully").then(function () {
                                    var url = "/Recruitment/Recruitmentdrive/Index";
                                    window.location.href = url;
                                });
                            }
                        }
                        else if (result.message == "MAKER_CHECKER") {
                            swal.fire("Maker and checker cannot be same.");
                        }
                        else if (result.message == "NAME_DUPLICATION_ERROR") {
                            swal.fire("Name already exist.");
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

/* ---------- List actions ---------- */

function onApproveClick(GI, LogGI, index) {
    if ($("#List_" + index + "__MakerChecker").val() == 'True')
        swal.fire(MakerCheckerMsg);
    else {
        showSpinner();
        $.ajax({
            type: 'GET',
            url: '/Recruitment/Recruitmentdrive/Approve',
            data: { RecruitmentdriveGI: GI, LogGI: LogGI },
            dataType: 'html',
            success: function (result) {
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    if ($('#approveSliderContainer').length == 0)
                        $('body').append('<div id="approveSliderContainer"></div>');
                    $('#approveSliderContainer').html(result);
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
}
function confirmApprove() {
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
                url: '/Recruitment/Recruitmentdrive/ApproveRecruitmentdrive',
                dataType: 'json',
                data: $('#approve-recruitment-drive').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire("Approved Successfully").then(function () {
                            window.location.href = "/Recruitment/Recruitmentdrive/Index";
                        });
                    }
                    else if (result.message == "MAKER_CHECKER") {
                        swal.fire("Maker and checker cannot be same.");
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

function confirmReject() {
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
                url: '/Recruitment/Recruitmentdrive/RejectRecruitmentdrive',
                dataType: 'json',
                data: $('#approve-recruitment-drive').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire("Rejected Successfully").then(function () {
                            window.location.href = "/Recruitment/Recruitmentdrive/Index";
                        });
                    }
                    else if (result.message == "MAKER_CHECKER") {
                        swal.fire("Maker and checker cannot be same.");
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
function onEditClick(GI, LogGI, index) {
    if ($("#List_" + index + "__EditActive").val() == 'True')
        swal.fire(ApprovalPendingMsg);
    else {
        var url = "/Recruitment/Recruitmentdrive/Edit?RecruitmentdriveGI=" + GI + "&LogGI=" + LogGI;
        window.location.href = url;
    }
}

function onDeleteClick(GI, LogGI, index) {
    if ($("#List_" + index + "__EditActive").val() == 'True')
        swal.fire(ApprovalPendingMsg);
    else {
        showSpinner();
        $.ajax({
            type: 'GET',
            url: '/Recruitment/Recruitmentdrive/Delete',
            data: { RecruitmentdriveGI: GI, LogGI: LogGI },
            dataType: 'html',
            success: function (result) {
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    if ($('#deleteSliderContainer').length == 0)
                        $('body').append('<div id="deleteSliderContainer"></div>');
                    $('#deleteSliderContainer').html(result);
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
}

function confirmDelete(GI) {
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
                url: '/Recruitment/Recruitmentdrive/DeleteRecruitmentdrive',
                dataType: 'json',
                data: { RecruitmentdriveGI: GI },
                success: function (result) {
                    if (result.success == true) {
                        var msg = (result.message == "DELETED") ? "Deleted Successfully" : "Sent for approval";
                        swal.fire(msg).then(function () {
                            window.location.href = "/Recruitment/Recruitmentdrive/Index";
                        });
                    }
                    else if (result.message == "MAKER_CHECKER") {
                        swal.fire("Maker and checker cannot be same.");
                    }
                    else if (result.message == "EDIT_ERROR") {
                        swal.fire(ApprovalPendingMsg);
                    }
                    else if (result.message == "ERROR_FOREIGN") {
                        swal.fire("Cannot delete, already in use.");
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

function onExportToExcelClick() {
    var listLength = $('#List_Count').val();
    if (listLength > 0) {
        url = "/Recruitment/Recruitmentdrive/ExportToExcel?TabIndex=" + currentTab;
        window.location.href = url;
    }
    else {
        swal.fire(NoDataFoundMsg);
    }
}