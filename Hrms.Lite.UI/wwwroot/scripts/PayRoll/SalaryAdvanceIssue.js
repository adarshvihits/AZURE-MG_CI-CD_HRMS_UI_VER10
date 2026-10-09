var trnHistory = 0;


function GetList() {
    const loanType = $('#LoanTypeCode').val();
    const monthCode = $('#MonthCode').val();
    const payrollGroup = $('#PayrollGroupCode').val();
    const listType = $("input[name='ListType']:checked").val();

    let url = '';
    let $targetView;

    $('#ListView, #ListViewApprovalPending').hide();
    $('.salBtns').addClass('hide');

    if (listType === "1") {
        $('#ListView').show();
        url = `/PayRoll/SalaryAdvanceIssue/GetList?MonthCode=${monthCode}&LoanTypeCode=${loanType}&PayrollGroupCode=${payrollGroup}`;
        $targetView = $('#ListView');
    } else {
        $('#ListViewApprovalPending').show();
        url = `/PayRoll/SalaryAdvanceIssue/GetPendingApprovalList?MonthCode=${monthCode}&PayrollGroupCode=${payrollGroup}`;
        $targetView = $('#ListViewApprovalPending');
    }

    showSpinner();

    $.ajax({
        type: 'GET',
        url: url,
        dataType: 'html',
        data: $('#BasicFilter').serialize(),
        success: function (data) {
            destroyDataTable('#SalaryAdvanceIssueTable');
            destroyDataTable('#AdvanceRequestApvPendingTable');

            $targetView.html(data);

            if (listType === "1") {
                const hasRows = $('#SalaryAdvanceIssueTable tbody tr').length > 0;
                $('.salBtns').toggleClass('hide', !hasRows);
            }

            hideSpinner();
        },
        error: function (xhr) {
            const responseHtml = $.parseHTML(xhr.responseText);
            const isSessionExpired = $(responseHtml).filter(".Login_Session").length > 0;

            if (isSessionExpired) {
                window.location.reload();
            } else {
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);
            }
        }
    });
}

function destroyDataTable(selector) {
    if ($.fn.dataTable.isDataTable(selector)) {
        $(selector).DataTable().clear().destroy();
    }
}

function GetTrnList() {
    var LType = $('#LoanTypeCode').val();
    var MCode = $('#MonthCode').val();
    if (LType != '' && MCode != '') {
        $.ajax({
            type: 'GET',
            url: '/PayRoll/SalaryAdvanceIssue/GetTrnList?MonthCode=' + MCode + '&LoanTypeCode=' + LType,
            dataType: 'html',
            data: $('#BasicFilter').serialize(),
            success: function (data) {
                $('#ListView').html('');
                $('#ListView').html(data);
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
            }
        });
    }

}


function GetTracker(RequestGI) {

    $.ajax({
        type: 'GET',
        url: '/PayRoll/SalaryAdvanceIssue/Tracker',
        dataType: 'html',
        data: { RequestGI: RequestGI },
        success: function (data) {
            $('body').append('<div class="form-overlay"></div>');
            $('#sliderView').html('');
            $('#sliderView').html(data);
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



function TransactionHistory() {

    window.location.href = '/PayRoll/SalaryAdvanceIssue/TransactionHistory'
}

function ApplyFilter(mode) {
    debugger

    if (mode == 'apply') {
        if (filterApplicable >= 1) {
            if (trnHistory != 1)
                GetList();
            else
                GetTrnList();

            CloseSlider('basicFilter');
            $('#filterForm').removeClass('filter-apply');
        }
        else {
            showSpinner();
            if (trnHistory != 1)
                GetList();
            else
                GetTrnList();
            CloseSlider('basicFilter');
            $('#filterForm').addClass('filter-apply');
        }

    }
    else {
        showSpinner();
        filterApplicable = 1;
        $('.reset').val('');
        $('#Optional_Filter_EmploymentStatus_Code').val(0);
        $('#filterForm').removeClass('filter-apply');
        if (trnHistory != 1)
            GetList();
        else
            GetTrnList();
    }
}



function onEditClick(i) {
    $('.edit_' + i).addClass('hide');
    $('.updatecancel_' + i).removeClass('hide');
}

function onCancelClick(i) {
    $('.updatecancel_' + i).addClass('hide');
    $('.edit_' + i).removeClass('hide');
}

function onUpdateClick(index) {
    var i = 0;
    if (!validateById('#SalaryAdvanceList_' + index + '__LoanAmount', 'LoanAmt_' + index))
        i++;
    if (!validateById('#SalaryAdvanceList_' + index + '__Remarks', 'Rmks_' + index))
        i++;
    if (i == 0) {
        $('#loanAmtDisplay_' + index).html($('#SalaryAdvanceList_' + index + '__LoanAmount').val());
        $('#rmk_' + index).html($('#SalaryAdvanceList_' + index + '__Remarks').val());
        onCancelClick(index);
    }
}

function onChkBoxClick(sender, allCheckBox, tableId) {
    debugger;
    if ($(sender).is(':checked')) {

        debugger;
        if ($('#' + tableId).find('input[type="checkbox"]:checked').length == $('#' + tableId).find('input[type="checkbox"]').length - 1)
            $('#' + allCheckBox).prop('checked', true);
        else
            $('#' + allCheckBox).prop('checked', false);
    }
    else {
        debugger;
        $('#moduleAll').prop('checked', false);
        if ($('#' + tableId).find('input[type="checkbox"]:checked').length == $('#' + tableId).find('input[type="checkbox"]').length)
            $('#' + allCheckBox).prop('checked', true);
        else
            $('#' + allCheckBox).prop('checked', false);
    }
}



function onExportToExcelClick() {
    debugger;
    var TableLength;
    var rdSelected = $("input[name='ListType']:checked").val();

    if (rdSelected == "1") {
        TableLength = $('#SalaryAdvanceList_Count').val();
        url = "/PayRoll/SalaryAdvanceIssue/ExportToExcel?data=" + $('#BasicFilter').serialize() + "&MonthCode=" + $('#MonthCode').val() + "&LoanTypeCode=" + $('#LoanTypeCode').val() + "&MonthName=" + $('#MonthCode option:selected').text() + "&LoanTypeName=" + $('#LoanTypeCode option:selected').text() + "&PayrollGroupCode=" + $('#PayrollGroupCode').val() + "&PayrollGroupName=" + $('#PayrollGroupCode option:selected').text();
    }
    else if (rdSelected == "2") {
        TableLength = $('#ApprovalPendingList_Count').val();
        url = "/PayRoll/SalaryAdvanceIssue/ExportToExcel_PendingApproval?data=" + $('#BasicFilter').serialize() + "&MonthCode=" + $('#MonthCode').val() + "&MonthName=" + $('#MonthCode option:selected').text() + "&LoanTypeName=" + $('#LoanTypeCode option:selected').text() + "&PayrollGroupCode=" + $('#PayrollGroupCode').val() + "&PayrollGroupName=" + $('#PayrollGroupCode option:selected').text();
    }


    if (TableLength > 0) {
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}

function onTrnExportToExcelClick() {
    debugger;

    var TableLength = $('#SalaryAdvanceList_Count').val();
    if (TableLength > 0) {
        url = "/PayRoll/SalaryAdvanceIssue/ExportToExcelTrn?data=" + $('#BasicFilter').serialize() + "&MonthCode=" + $('#MonthCode').val() + "&LoanTypeCode=" + $('#LoanTypeCode').val() + "&MonthName=" + $('#MonthCode option:selected').text() + "&LoanTypeName=" + $('#LoanTypeCode option:selected').text();
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}


function onPayrollGroupChange() {
    $('#MonthCode').empty();
    $.getJSON("/PayRoll/SalaryAdvanceIssue/FillPayrollMonth", { PayrollGroup: $('#PayrollGroupCode').val() }, function (data) {
        $('#MonthCode option').remove();
        if (data.length >= 1 || data.length == 0)
            $('#MonthCode').append('<option value="">--Month--</option');

        for (var i = 0; i < data.length; i++) {
            $('#MonthCode').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
        }

    });

}


function FillLoanTypeDropDown() {

    $('#LoanTypeCode').empty();
    $.getJSON("/PayRoll/SalaryAdvanceIssue/FillLoanType", { TabIndex: TabIndex }, function (data) {
        $('#LoanTypeCode option').remove();
        $('#LoanTypeCode').append('<option value="">--Loan Type--</option');

        for (var i = 0; i < data.length; i++) {
            $('#LoanTypeCode').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
        }

    });

}

function OnClickSaveLoanIssue() {
    debugger;
    var i = 0;

    if ($('#LoanTypeCode').val() == '') {
        $("#LoanTypeCode").addClass('is-invalid');
        $("#Type").addClass('error-message');
        $("#Type").html("This field is required");
        i++;

    }

    var checkboxes = document.querySelectorAll('input[type="checkbox"]:checked');
    if (checkboxes.length == 0) {
        swal.fire("Please select atleast one Entry");
        $('#btnSalAdvSave').attr('data-target', '');
        return false;
    }

    if (i == 0) {

        $('#btnSalAdvSave').attr('data-target', '.bd-example-modal-lg');

    }
    else {
        $('#btnSalAdvSave').attr('data-target', '');
        return false;
    }

}


function onSubmit(Mode) {
    debugger


    var LType = $('#LoanTypeCode').val();
    var MCode = $('#MonthCode').val();

    if (LType != '' && MCode != '') {

        var table = $('#SalaryAdvanceIssueTable').DataTable();
        var data = table.$('input,select,textarea').serializeArray();
        data.push({ name: 'LoanTypeCode', value: $('#LoanTypeCode').val() });
        data.push({ name: 'MonthCode', value: $('#MonthCode').val() });
        data.push({ name: 'Remarks', value: $('#Remarks').val() });
    } else {
        return;
    }

    if (Mode == "APPROVE")
        var msg = "Do you want to issue all these selected items?"

    swal.fire({
        text: msg,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {

            $.ajax({
                type: 'POST',
                url: '/Payroll/SalaryAdvanceIssue/Save?Mode=' + Mode,
                dataType: 'json',
                data: data,
                success: function (result) {
                    debugger;
                    if (result.success == true) {
                        if (Mode == "APPROVE") {
                            swal.fire("Saved Successfully").then(function () {

                                var m = $('.bd-example-modal-lg');
                                m.modal('hide');
                                $('#Remarks').val('');
                                GetList();
                            });
                        }

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

        else
            return false;

    })

}
