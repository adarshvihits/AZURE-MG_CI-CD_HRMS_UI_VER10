var currentTab = 'ACTIVE';

function GetList() {
    showSpinner();
    var branch = $('#Branch_Code').val();
    var planningPeriod = $('#PlanningPeriod_Code').val();

    $.ajax({

        type: 'GET',
        url: '/Master/HrPlanningMaster/GetHrPlanningList',
        dataType: 'html',
        data: { TabIndex: currentTab, Branch: branch, PlanningPeriod: planningPeriod },

        success: function (result) {
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#activeList').html("");
                $('#activeList').html(result);
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

function OnPlanningPeriodChange(sender) {
    $('#AllBranch').prop('checked', false).data('checked', false);
    LoadBranchDropdown("/Master/HrPlanningMaster/GetPlanningLocation", { PlanningPeriod: $(sender).val() });
}

function OnAllBranchClick(sender) {
    // Single radio: clicking it again unchecks it and restores the period-wise branches
    if ($(sender).data('checked')) {
        $(sender).prop('checked', false).data('checked', false);
        LoadBranchDropdown("/Master/HrPlanningMaster/GetPlanningLocation", { PlanningPeriod: $('#PlanningPeriod_Code').val() });
    }
    else {
        $(sender).data('checked', true);
        LoadBranchDropdown("/Master/HrPlanningMaster/GetAllLocation", {});
    }
}

function LoadBranchDropdown(url, params) {
    $.getJSON(url, params, function (data) {
        $('#Branch_Code option').remove();
        $('#Branch_Code').append('<option value>--Select--</option>');
        for (var i = 0; i < data.length; i++) {
            $('#Branch_Code').append('<option value="' + data[i].code + '">' + data[i].name + '</option>');
        }
        GetList();
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

function onAddNewClick() {
    if (!$('#Branch_Code').val() || !$('#PlanningPeriod_Code').val()) {
        swal.fire('Please select Planning Period and Branch first.');
        return;
    }

    if ($('.plan-row-editable').length > 0) {
        swal.fire('Please save or cancel the row you are currently editing first.');
        return;
    }

    showSpinner();
    $.ajax({

        type: 'GET',
        url: '/Master/HrPlanningMaster/Create',
        dataType: 'html',
        success: function (result) {
            hideSpinner();
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#activeTable tbody').prepend(result);
            }
        },

        error: function (xhr, textStatus, errorThrown) {
            hideSpinner();
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
        }
    });
}

function OnEditClick(Type, Logcode, LogGI, GI, index, el) {
    var branch = $('#Branch_Code').val();
    var planningPeriod = $('#PlanningPeriod_Code').val();

    if ($("#HRPlanningMasterList_" + index + "__EditActive").val() == 'True') {
        swal.fire(ApprovalPendingMsg);
        return;
    }

    if ($('.plan-row-editable').length > 0) {
        swal.fire('Please save or cancel the row you are currently editing first.');
        return;
    }

    var row = $(el).closest('tr');

    showSpinner();
    $.ajax({

        type: 'GET',
        url: '/Master/HrPlanningMaster/Edit',
        dataType: 'html',
        data: { Logcode: Logcode, Branch: branch, PlanningPeriod: planningPeriod },
        success: function (result) {
            hideSpinner();
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                row.replaceWith(result);
            }
        },

        error: function (xhr, textStatus, errorThrown) {
            hideSpinner();
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
        }
    });
}

function CancelPlanRow(el) {
    var row = $(el).closest('tr');
    var mode = row.find('#Mode').val();

    if (mode === 'MODIFY') {
        GetList();
    }
    else {
        row.remove();
    }
}

function PreviewPlanRowTotal() {
    var planCount = parseFloat($('#PlanCount').val());
    var avgSalary = parseFloat($('#AvgSalary').val());

    if (isNaN(planCount) || isNaN(avgSalary) || planCount <= 0 || avgSalary <= 0) {
        $('#planRowTotalPreview').text('auto');
    }
    else {
        $('#planRowTotalPreview').text((planCount * avgSalary).toLocaleString('en-IN'));
    }
}

function validatePlanRow() {
    var isValid = true;

    if (!validateById('#Department_Code', 'combinationhr')) {
        isValid = false;
    }
    if (!validateById('#Section_Code', 'sectionhr')) {
        isValid = false;
    }
    if (!validateById('#Grade_Code', 'gradehr')) {
        isValid = false;
    }

    var planCount = $('#PlanCount').val();
    if (!planCount || isNaN(planCount) || parseInt(planCount) <= 0) {
        $('#PlanCount').addClass('is-invalid');
        $('#plancounthr').addClass('error-message').html('Enter a valid plan count');
        isValid = false;
    }
    else {
        $('#PlanCount').removeClass('is-invalid');
        $('#plancounthr').removeClass('error-message').html('');
    }

    var avgSalary = $('#AvgSalary').val();
    if (!avgSalary || isNaN(avgSalary) || parseFloat(avgSalary) <= 0) {
        $('#AvgSalary').addClass('is-invalid');
        $('#avgsalaryhr').addClass('error-message').html('Enter a valid average salary');
        isValid = false;
    }
    else {
        $('#AvgSalary').removeClass('is-invalid');
        $('#avgsalaryhr').removeClass('error-message').html('');
    }

    return isValid;
}

function SavePlanRow(mode) {
    if (!validatePlanRow()) {
        return false;
    }

    var branch = $('#Branch_Code').val();
    var planningPeriod = $('#PlanningPeriod_Code').val();

    swal.fire({
        text: "Do you want to " + mode + "?",
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
                url: '/Master/HrPlanningMaster/Save',
                dataType: 'json',
                data: {
                    'GI': $('#GI').val(),
                    'LogGI': $('#LogGI').val(),
                    'Mode': $('#Mode').val(),
                    'Logcode': $('#Logcode').val(),
                    'Department.Code': $('#Department_Code').val(),
                    'Section.Code': $('#Section_Code').val(),
                    'Grade.Code': $('#Grade_Code').val(),
                    'PlanCount': $('#PlanCount').val(),
                    'AvgSalary': $('#AvgSalary').val(),
                    'Branch': branch,
                    'PlanningPeriod': planningPeriod
                },
                success: function (result) {
                    hideSpinner();
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
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

function onCarryForwardClick() {
    var branch = $('#Branch_Code').val();

    if (!branch) {
        swal.fire('Please select Branch first.');
        return;
    }

    showSpinner();
    $.ajax({

        type: 'GET',
        url: '/Master/HrPlanningMaster/GetCarryForwardPopup',
        dataType: 'html',
        success: function (result) {
            hideSpinner();
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                var m = $('#CarryForwardPlanningPopup');
                m.find('.modal-body').html('');
                m.find('.modal-body').html(result);
                m.modal('show');
            }
        },

        error: function (xhr, textStatus, errorThrown) {
            hideSpinner();
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
        }
    });
}

function confirmCarryForward() {
    var branch = $('#Branch_Code').val();
    var fromPeriod = $('#FromPlanningPeriod').val();
    var toPeriod = $('#ToPlanningPeriod').val();

    if (!fromPeriod || !toPeriod) {
        swal.fire('Please select both From Period and To Period.');
        return;
    }

    swal.fire({
        text: "Carry forward the approved plan from the selected period into the target period?",
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
                url: '/Master/HrPlanningMaster/CarryForward',
                dataType: 'json',
                data: { Branch: branch, FromPlanningPeriod: fromPeriod, ToPlanningPeriod: toPeriod },
                success: function (result) {
                    hideSpinner();
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            $('#CarryForwardPlanningPopup').modal('hide');
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

function OnApproveClick(GI, LogGI, EntryType, index) {
    if ($("#HRPlanningMasterList_" + index + "__MakerChecker").val() == 'True') {
        swal.fire(MakerCheckerMsg);
        return;
    }

    $.ajax({

        type: 'GET',
        url: '/Master/HrPlanningMaster/ApproveOrReject',
        dataType: 'html',
        data: { GI: GI, LogGI: LogGI, EntryType: EntryType },
        success: function (result) {
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('body').append('<div class="form-overlay"></div>');
                $('#approve').html("");
                $('#approve').html(result);
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
                url: '/Master/HrPlanningMaster/Approve',
                dataType: 'json',
                data: $('#HrPlanningMasterApproveOrReject').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('hrPlanningApproveSlider');
                            currentTab = 'APPROVAL_PENDING';
                            GetList();
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
                url: '/Master/HrPlanningMaster/Reject',
                dataType: 'json',
                data: $('#HrPlanningMasterApproveOrReject').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('hrPlanningApproveSlider');
                            currentTab = 'APPROVAL_PENDING';
                            GetList();
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

function onExportToExcelClick() {
    var activeTableLength = $('#HRPlanningMasterList_Count').val();
    if (activeTableLength > 0) {
        var url = "/Master/HrPlanningMaster/ExportToExcel?TabIndex=" + currentTab;
        window.location.href = url;
    }
    else {
        swal.fire(NoDataFoundMsg);
    }
}

function OnDeleteClick(GI, LogGI, Type, index) {
    if ($("#HRPlanningMasterList_" + index + "__EditActive").val() == 'True') {
        swal.fire(ApprovalPendingMsg);
        return;
    }

    $.ajax({
        type: 'GET',
        url: '/Master/HrPlanningMaster/DeleteHrPlanningDetails',
        dataType: 'html',
        data: { GI: GI },
        success: function (result) {
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('body').append('<div class="form-overlay"></div>');
                $('#EditOrDeleteHrPlanning').html("");
                $('#EditOrDeleteHrPlanning').html(result);
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
                url: '/Master/HrPlanningMaster/DeleteHrPlanning',
                dataType: 'json',
                data: { GI: GI },
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('hrPlanningMasterDeleteSlider');
                            currentTab = 'ACTIVE';
                            GetList();
                        });
                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/HrPlanningMaster/DeleteHrPlanningDetails?GI=" + GI + "&LogGI=" + LogGI;
                            $('#hrPlanningMasterDeleteSlider').load(url, function () { });
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
    })
}
