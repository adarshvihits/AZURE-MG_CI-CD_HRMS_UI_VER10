var trnHistory = 0;

function GetList_LoanIssue() {
   
    var LType = $('#LoanTypeCode').val();
    var MCode = $('#MonthCode').val();
    var PCode = $('#PayrollGroupCode').val();
        $.ajax({
            type: 'GET',
            url: '/PayRoll/EmployeeRequestLoanIssue/GetList_LoanIssue?LoanTypeCode=' + LType + '&PayrollGroupCode=' + PCode,
            dataType: 'html',
            data: $('#BasicFilter').serialize(),
            success: function (data) {
                $('#LoanListView').html('');
                $('#LoanListView').html(data);
                debugger;
                       
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

//}



function GetTrnList() {
    var LType = $('#LoanTypeCode').val();
    var MCode = $('#MonthCode').val();
        $.ajax({
            type: 'GET',
            url: '/PayRoll/EmployeeRequestLoanIssue/GetTrnList?MonthCode=' + MCode + '&LoanTypeCode=' + LType,
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

//}


function GetTracker(RequestGI) {

    $.ajax({
        type: 'GET',
        url: '/PayRoll/EmployeeRequestLoanIssue/Tracker',
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

function GetLoanIssueTracker(RequestGI, EmployeeGI) {

    $.ajax({
        type: 'GET',
        url: '/PayRoll/EmployeeRequestLoanIssue/LoanIssueTracker',
        dataType: 'html',
        data: { RequestGI: RequestGI, EmployeeGI: EmployeeGI },
        success: function (data) {
            $('body').append('<div class="form-overlay"></div>');
            $('#LoanTracker').html('');
            $('#LoanTracker').html(data);
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
function GetLoanIssueDetails(RequestGI, EmployeeGI) {

    $.ajax({
        type: 'GET',
        url: '/PayRoll/EmployeeRequestLoanIssue/Create',
        dataType: 'html',
        data: { RequestGI: RequestGI, EmployeeGI: EmployeeGI },
        success: function (data) {
            $('body').append('<div class="form-overlay"></div>');
            $('#LoanTracker').html('');
            $('#LoanTracker').html(data);
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

    window.location.href = '/PayRoll/EmployeeRequestLoanIssue/TransactionHistory'
}



function ApplyFilter(mode) {
    debugger

    if (mode == 'apply') {
        if (filterApplicable >= 1) {
            if (trnHistory != 1)
                GetList_LoanIssue();
            else
                GetTrnList();

            CloseSlider('basicFilter');
            $('#filterForm').removeClass('filter-apply');
        }
        else {
            showSpinner();
            if (trnHistory != 1)
                GetList_LoanIssue();
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
            GetList_LoanIssue();
        else
            GetTrnList();
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




function SaveLoanIssue() {
    debugger;
        var LType = $('#LoanTypeCode').val();
        var MCode = $('#MonthCode').val();

            var table = $('#LoanIssueTable').DataTable();
            var data = table.$('input,select,textarea').serializeArray();
            data.push({ name: 'MonthCode', value: $('#MonthCode').val() });
            data.push({ name: 'Remarks', value: $('#Remarks').val() });

            swal.fire({
                text: "Do you want to issue all these selected items?",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Ok'
            }).then((result) => {

                if (result.isConfirmed) {
                    debugger;

                    $.ajax({
                        type: 'POST',
                        url: '/PayRoll/EmployeeRequestLoanIssue/SaveLoanIssueData',
                        dataType: 'json',
                        data: data,
                        success: function (result) {
                            if (result.success == true) {
                                swal.fire("Saved Successfully").then(function () {

                                    var m = $('.bd-example-modal-lg');
                                    m.modal('hide');
                                    $('#Remarks').val('');
                                    GetList_LoanIssue();
                                });
                            }
                            else {
                                swal.fire(result.message).then(function () {

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


function onExportToExcelClick() {
    debugger;
    var TableLength;
 

        TableLength = $('#LoanIssueList_Count').val();
    url = "/PayRoll/EmployeeRequestLoanIssue/ExportToExcelLoanIssue?data=" + $('#BasicFilter').serialize() + "&MonthCode=" + $('#MonthCode').val() + "&LoanTypeCode=" + $('#LoanTypeCode').val() + "&MonthName=" + $('#MonthCode option:selected').text() + "&LoanTypeName=" + $('#LoanTypeCode option:selected').text() + "&PayrollGroupCode=" + $('#PayrollGroupCode').val() + "&PayrollGroupName=" + $('#PayrollGroupCode option:selected').text();

    if (TableLength > 0) {
         window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}

function onTrnExportToExcelClick() {
    debugger;

    var TableLength = $('#LoanIssueList_Count').val();
    if (TableLength > 0) {
        url = "/PayRoll/EmployeeRequestLoanIssue/ExportToExcelTrn?data=" + $('#BasicFilter').serialize() + "&MonthCode=" + $('#MonthCode').val() + "&LoanTypeCode=" + $('#LoanTypeCode').val() + "&MonthName=" + $('#MonthCode option:selected').text() + "&LoanTypeName=" + $('#LoanTypeCode option:selected').text();
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}


function onPayrollGroupChange() {

    $.getJSON("/PayRoll/EmployeeRequestLoanIssue/FillPayrollMonth", { PayrollGroup: $('#PayrollGroupCode').val() }, function (data) {
        $('#MonthCode option').remove();
        if (data.length > 1 || data.length == 0)
            $('#MonthCode').append('<option value="">--Month--</option');

        for (var i = 0; i < data.length; i++) {
            $('#MonthCode').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
        }

    });

}

function FillLoanTypeDropDown() {

    $('#LoanTypeCode').empty();
    $.getJSON("/PayRoll/EmployeeRequestLoanIssue/FillLoanType", function (data) {
        $('#LoanTypeCode option').remove();
            $('#LoanTypeCode').append('<option value="">--Loan Type--</option');

        for (var i = 0; i < data.length; i++) {
            $('#LoanTypeCode').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
        }

    });

}

function OnClickSaveLoanIssueValidate() {
    debugger;
    var i = 0;

    if ($('#MonthCode').val() == '') {
        $("#MonthCode").addClass('is-invalid');
        $("#Month").addClass('error-message');
        $("#Month").html("This field is required");
        i++;
    }

    var checkboxes = document.querySelectorAll('input[type="checkbox"]:checked');
    if (checkboxes.length === 0) {
        swal.fire("Please select atleast one Entry");
        return false;
    }

    if (i == 0) {

        $('#btnLoanIssueSave').attr('data-target', '.bd-example-modal-lg');

    }
    else {
        $('#btnLoanIssueSave').attr('data-target', '');
        return false;
    }
}


//function ApplyFilter(mode) {
//    debugger
//    var tabIndex = $('#Tabindex').val();
//    $("#Tabindex").val(currentTab);

//    if (mode == 'apply') {
//        showSpinner();
//        if (filterApplicable >= 1) {
//            $.ajax({

//                type: 'POST',
//                url: '/PayRoll/LoanIssue/ApplyFilter',
//                dataType: 'html',
//                data: $('#BasicFilter').serialize(),
//                success: function (result) {
//                    debugger
//                    CloseSlider('basicFilter');
//                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
//                        window.location.reload();
//                    }

//                    else {
//                        $('#allList').html("");
//                        $('#allList').html(result);

//                        $('#allList').removeClass('hide');
//                    }
//                    //$('#rewardsListPartial').addClass('hide');       
//                    hideSpinner();
//                },

//                error: function (xhr, textStatus, errorThrown) {
//                    hideSpinner();
//                    document.body.id = "kt_body";
//                    $('body').html(xhr.responseText);
//                }
//            });
//            $('#filterForm').removeClass('filter-apply');
//        }
//        else {
//            $.ajax({

//                type: 'POST',
//                url: '/PayRoll/LoanIssue/ApplyFilter',
//                dataType: 'html',
//                data: $('#BasicFilter').serialize(),
//                success: function (result) {
//                    debugger
//                    CloseSlider('basicFilter');
//                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
//                        window.location.reload();
//                    }

//                    else {
//                        $('#allList').html("");
//                        $('#allList').html(result);

//                        $('#allList').removeClass('hide');
//                    }
//                    //$('#rewardsListPartial').addClass('hide');     
//                    hideSpinner();
//                },

//                error: function (xhr, textStatus, errorThrown) {
//                    hideSpinner();
//                    document.body.id = "kt_body";
//                    $('body').html(xhr.responseText);
//                }
//            });
//            $('#filterForm').addClass('filter-apply');
//        }

//    }
//    else {
//        showSpinner();
//        filterApplicable = 1;
//        $('#filterForm').removeClass('filter-apply');
//        $('.reset').val('');
//        $('#Optional_Filter_EmploymentStatus_Code').val(0);
//        $.ajax({
//            type: 'POST',
//            url: '/PayRoll/LoanIssue/ApplyFilter',
//            dataType: 'html',
//            data: $('#BasicFilter').serialize(),
//            success: function (result) {
//                debugger
//                //CloseSlider('assetAllocationFilter');
//                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
//                    window.location.reload();
//                }

//                else {
//                    $('#allList').html("");
//                    $('#allList').html(result);

//                    $('#allList').removeClass('hide');
//                    //$('#rewardsListPartial').addClass('hide');
//                }
//                hideSpinner();
//            },
//            error: function (xhr, textStatus, errorThrown) {
//                hideSpinner();
//                document.body.id = "kt_body";
//                $('body').html(xhr.responseText);
//            }
//        });
//    }
//}

function validateLoanIssue() {
    var i = 0;
    if (!validateById('#LoanIssueRequest_LoanType_Code', 'loanType')) {
        i++;
    }
    if (!validateById('#LoanIssueRequest_RepaymentStartMonth_Code', 'repaymentStartMonth')) {
        i++;
    }

    if (!validateById('#LoanIssueRequest_Pourpose', 'purpose')) {
        i++;
    }
    if (!validateByIdForLoan('#LoanIssueRequest_LoanAmount', 'loanAmount', 'Loan amount cannot be 0')) {
        i++;
    }

    if (!validateById('#LoanIssueRequest_DisbursementDate', 'disbursementDate'))
        i++;
    if (!validateByIdForLoan('#LoanIssueRequest_MonthlyEMI', 'monthlyEMI', 'Monthly EMI cannot be 0')) {
        i++;
    }

    if (!validateByIdForLoan('#LoanIssueRequest_Tenure', 'tenure', 'Tenure cannot be 0')) {
        i++;
    }
    compareLoanAmtAndMonthlyEmi();

    if (!checkLoanAmt()) {
        i++;
    }
    if (!DOJnDisDate())
        i++;
    if (i == 0)
        swal.fire({
            text: "Do you want to Save?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({

                    type: 'POST',
                    url: '/PayRoll/LoanIssue/Create',
                    dataType: 'json',
                    data: $('#LoanIssueSave').serialize(),
                    success: function (result) {
                        debugger
                        if (result.success) {
                            swal.fire("Saved Successfully").then(function () {
                                debugger;
                                var url = "/PayRoll/EmployeeRequestLoanIssue/Index";
                                window.location.href = url;
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
    else
        return false;
}
function onLogClick(LoanIssueGI) {
    debugger;
     $.ajax({

        type: 'GET',
        url: '/PayRoll/LoanIssue/LoanIssueLogDetails',
        dataType: 'html',
        data: { LoanIssueGI: LoanIssueGI },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {
                $('body').append('<div class="form-overlay"></div>');
                $('#LoanIssueLogDetailsSlider').html("");
                $('#LoanIssueLogDetailsSlider').html(result);
            }

            hideSpinner();
        },


        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
            hideSpinner();

        }
    });
}

function GetLoanWFReport() {

    var LType = $('#LoanTypeCode').val();
    $.ajax({
        type: 'GET',
        url: '/PayRoll/LoanWFTrackingReport/GetList_LoanIssue?LoanTypeCode=' + LType,
        dataType: 'html',
        data: $('#BasicFilter').serialize(),
        success: function (data) {
            $('#LoanListView').html('');
            $('#LoanListView').html(data);
            debugger;

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
function onExportToExcelClickReport() {
    debugger;
    var TableLength;


    TableLength = $('#LoanIssueList_Count').val();
    url = "/PayRoll/LoanWFTrackingReport/ExportToExcelLoanIssue?data=" + $('#BasicFilter').serialize() + "&LoanTypeCode=" + $('#LoanTypeCode').val() + "&LoanTypeName=" + $('#LoanTypeCode option:selected').text();

    if (TableLength > 0) {
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}