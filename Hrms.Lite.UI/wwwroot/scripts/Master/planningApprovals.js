var currentApprovalTab = 'PENDING';

function GetApprovalList() {
    showSpinner();
    var planningPeriod = $('#PlanningPeriod_Code').val();

    $.ajax({

        type: 'GET',
        url: '/Master/PlanningApprovals/GetPlanningApprovalList',
        dataType: 'html',
        data: { TabIndex: currentApprovalTab, PlanningPeriod: planningPeriod },

        success: function (result) {
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#approvalStats').html("");
                $('#approvalStats').html(result);
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

function ShowApprovalTab(tab) {
    currentApprovalTab = tab;
    $('#approvalSearch').val('');
    GetApprovalList();
}

function FilterApprovalCards() {
    var term = $('#approvalSearch').val().toLowerCase();
    $('#approvalCardList .approval-card').each(function () {
        var haystack = $(this).data('search') || '';
        $(this).toggle(haystack.toString().indexOf(term) > -1);
    });
}

function onApproveClick(Logcode, LogGI, EntryType, index) {
    var remarks = $('#note_' + index).val();

    swal.fire({
        text: "Do you want to Approve?",
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
                url: '/Master/PlanningApprovals/Approve',
                dataType: 'json',
                data: { Logcode: Logcode, LogGI: LogGI, EntryType: EntryType, Remarks: remarks },
                success: function (result) {
                    hideSpinner();
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            GetApprovalList();
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

function onRejectClick(Logcode, LogGI, EntryType, index) {
    var remarks = $('#note_' + index).val();

    if (!remarks) {
        swal.fire('Please add a note before rejecting.');
        return;
    }

    swal.fire({
        text: "Do you want to Reject?",
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
                url: '/Master/PlanningApprovals/Reject',
                dataType: 'json',
                data: { Logcode: Logcode, LogGI: LogGI, EntryType: EntryType, Remarks: remarks },
                success: function (result) {
                    hideSpinner();
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            GetApprovalList();
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
