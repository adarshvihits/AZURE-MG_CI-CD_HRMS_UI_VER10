var TypeCode=0;
function onLoanTypeChange(sender) {
    debugger;
    TypeCode = sender.value;
    //var url = "/PayRoll/LoanClosure/GetLoanClosureList";
    //$('#loanClosureList').load(url, { LoanType: TypeCode }, function () { });
    $('#LoanType').val(TypeCode);
    $('.filterDropDown').val(TypeCode);
    //validate(sender, 'loanTypeSpan');
    ApplyFilter('reset');
}

function onClosureDateChange() {
    var closureDate = new Date($('#ClosureDate').val());
    var disbursementDate = new Date($('#DisbursementDate').val());
    if (closureDate < disbursementDate) {
        $('#ClosureDate').addClass('is-invalid');
        $('#CDate').addClass('error-message');
        $('#CDate').html('Closure date should be greater than Disbursement date');
    }
    else {
        $('#ClosureDate').removeClass('is-invalid');
        $('#CDate').removeClass('error-message');
        $('#CDate').html('');
    }
}

function onEditClick(EmployeeGI,LoanIssueGI) {
    var url = "/PayRoll/LoanClosure/GetLoanClosureDetails";
    
    $('#editLoanClosure').load(url, { EmployeeGI: EmployeeGI, LoanIssueGI: LoanIssueGI }, function () { });
    $.ajax({

        type: 'GET',
        url: '/PayRoll/LoanClosure/GetLoanClosureDetails',
        dataType: 'html',
        data: { EmployeeGI: EmployeeGI, LoanIssueGI: LoanIssueGI },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {
                $('body').append('<div class="form-overlay"></div>');
                $('#editLoanClosure').html("");
                $('#editLoanClosure').html(result);
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

function LoanClosureSave() {
    var i = 0;
    if (!validateById('#ClosureDate', 'CDate')) {
        i++;
    }
    if (!validateById('#ClosureAmount', 'CAmt')) {
        i++;
    }
    if (i == 0) {
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
                    url: '/PayRoll/LoanClosure/LoanClosureSave',
                    dataType: 'json',
                    data: $('#loanClosureSave').serialize(),
                    success: function (result) {
                        debugger
                        if (result.success) {

                            swal.fire("Saved Successfully");
                            CloseSlider('closureEdit');
                            ApplyFilter('apply');
                            //var url = "/PayRoll/LoanClosure/GetLoanClosureList";
                            //$('#loanClosureList').load(url, { LoanType: TypeCode }, function () { });
                        }
                        else {
                            swal.fire(result.message);
                        }
                    },


                    error: function (xhr, textStatus, errorThrown) {
                        debugger
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


function ApplyFilter(mode) {
    debugger
   
        if ($('#LoanType').val() != "")
            $("#SearchId").val($('#LoanType').val());
        else
            $("#SearchId").val(0);

    if (mode == 'apply') {
        showSpinner();
            if (filterApplicable >= 1) {
                $.ajax({

                    type: 'POST',
                    url: '/PayRoll/LoanClosure/ApplyFilter',
                    dataType: 'html',
                    data: $('#BasicFilter').serialize(),
                    success: function (result) {
                        debugger
                        CloseSlider('basicFilter');
                        if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                            window.location.reload();
                        }

                        else {
                            $('#loanClosureList').html("");
                            $('#loanClosureList').html(result);

                            $('#loanClosureList').removeClass('hide');
                        }
                        hideSpinner();
                        //$('#rewardsListPartial').addClass('hide');               
                    },


                    error: function (xhr, textStatus, errorThrown) {
                        debugger
                        hideSpinner();
                        document.body.id = "kt_body";
                        $('body').html(xhr.responseText);
                    }
                });
                $('#filterForm').removeClass('filter-apply');
            }
            else {
                $.ajax({

                    type: 'POST',
                    url: '/PayRoll/LoanClosure/ApplyFilter',
                    dataType: 'html',
                    data: $('#BasicFilter').serialize(),
                    success: function (result) {
                        debugger
                        CloseSlider('basicFilter');
                        if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                            window.location.reload();
                        }

                        else {
                            $('#loanClosureList').html("");
                            $('#loanClosureList').html(result);

                            $('#loanClosureList').removeClass('hide');
                        }
                        hideSpinner();
                        //$('#rewardsListPartial').addClass('hide');               
                    },


                    error: function (xhr, textStatus, errorThrown) {
                        debugger
                        hideSpinner();
                        document.body.id = "kt_body";
                        $('body').html(xhr.responseText);
                    }
                });
                $('#filterForm').addClass('filter-apply');
            }

        }
    else {
            showSpinner();
            filterApplicable = 1;
            $('.reset').val('');
            $('#Optional_Filter_EmploymentStatus_Code').val(0);
            $('#filterForm').removeClass('filter-apply');
            $.ajax({

                type: 'POST',
                url: '/PayRoll/LoanClosure/ApplyFilter',
                dataType: 'html',
                data: $('#BasicFilter').serialize(),
                success: function (result) {
                    debugger

                    //CloseSlider('assetAllocationFilter');

                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }

                    else {
                        $('#loanClosureList').html("");
                        $('#loanClosureList').html(result);

                        $('#loanClosureList').removeClass('hide');
                        //$('#rewardsListPartial').addClass('hide');
                    }
                    hideSpinner();
                },


                error: function (xhr, textStatus, errorThrown) {
                    hideSpinner();
                    document.body.id = "kt_body";
                    $('body').html(xhr.responseText);
                }
            });
        }
    
}

function ExportToExcel() {
    debugger;

    //if (!validateById('#LoanType', 'loanTypeSpan')) {
    //    return false;
    //}
    //else
    //{
        var TableLength = $('#LoanIssuesList_Count').val();
        if (TableLength > 0) {
            url = "/PayRoll/LoanClosure/ExportToExcel?loanType=" + $('#LoanType option:selected').text() + "&type=" + $('#LoanType').val();
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
    /*}*/

}