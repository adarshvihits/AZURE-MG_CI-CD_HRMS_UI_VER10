var prevTab = "all";
var currentTab = 0;
var currentTabName = "All";
function tabClick(Tab, TabIndex, TabName, LoanSettingsCode) {
    debugger;
   
    if (TabIndex == 2) {
        $('.loanbulk').removeClass('hide');
        $('#allList').html("");
        $('.loanexcelex').addClass('hide');
        $('#filterForm').addClass('hide');
        $('.dropoption').addClass('hide');
        $('.addNewAssets').addClass('hide');

    }
    else {
        $('.loanexcelex').removeClass('hide');
        $('.loanbulk').addClass('hide');
        $('#filterForm').removeClass('hide');
        $('.dropoption').removeClass('hide');
        $('.addNewAssets').removeClass('hide');
    }
        showSpinner();
        $.ajax({

            type: 'GET',
            url: '/PayRoll/LoanIssue/GetLoanIssueList',
            dataType: 'html',
            data: { TabIndex: TabIndex, LoanSettingsCode: $(LoanSettingsCode).val() },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }

                else {

                    $('#allList').html("");
                    $('#allList').html(result);
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
 
    currentTab = TabIndex;
    currentTabName = TabName;
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");
    //var url = "/PayRoll/LoanIssue/GetLoanIssueList";
    //$('#allList').load(url, { TabIndex: TabIndex }, function () { hideSpinner(); });
   
    prevTab = Tab;
}

function fillSingleValueDropdown() {
    debugger;
    var Repayment = $('#LoanIssueRequest_RepaymentMethod_Code > option').length;
    if (Repayment == 2) {
        document.getElementById('LoanIssueRequest_RepaymentMethod_Code').selectedIndex = 1;
    }

    var LType = $('#LoanIssueRequest_LoanType_Code > option').length;
    if (LType == 2) {
        document.getElementById('LoanIssueRequest_LoanType_Code').selectedIndex = 1;
    }

    var RepaymentStrtMonth = $('#LoanIssueRequest_RepaymentStartMonth_Code > option').length;
    if (RepaymentStrtMonth == 2) {
        document.getElementById('LoanIssueRequest_RepaymentStartMonth_Code').selectedIndex = 1;
    }
}


function onEmployeeClick(LoanIssueGI, EmployeeGI, Tab) {
    debugger;
    

    if (Tab == "AllList") {
        //var url = "/PayRoll/LoanIssue/GetLoanIssueDetails";

        //$('body').append('<div class="form-overlay"></div>');
        //$('.EditLoanIssueSlider').load(url, { LoanIssueGI: LoanIssueGI, EmployeeGI: EmployeeGI }, function () { });

        //$('#editSlider').removeClass('hide');
        $.ajax({

            type: 'GET',
            url: '/PayRoll/LoanIssue/GetLoanIssueDetails',
            dataType: 'html',
            data: { LoanIssueGI: LoanIssueGI, EmployeeGI: EmployeeGI },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }

                else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('.EditLoanIssueSlider').html("");
                    $('.EditLoanIssueSlider').html(result);
                    $('#editSlider').removeClass('hide');
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
    else if (Tab == "pending") {
        //var url = "/PayRoll/LoanIssue/LoanIssueApproveRejectDetails";
        //$('body').append('<div class="form-overlay"></div>');
        //$('#ApproveSlider').load(url, { LoanIssueGI: LoanIssueGI, EmployeeGI: EmployeeGI }, function () { });
        $.ajax({

            type: 'GET',
            //url: '/PayRoll/LoanIssue/LoanIssueApproveRejectDetails',
            url: '/PayRoll/LoanIssue/NewRequest',
            dataType: 'html',
            data: { LoanIssueGI: LoanIssueGI, EmployeeGI: EmployeeGI },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }

                else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('#addNewLoan').html("");
                    $('#addNewLoan').html(result);
                    $('#addNewLoan').removeClass('hide');
                    //$('#ApproveSlider').html("");
                    //$('#ApproveSlider').html(result);
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
     //   $('.rewrd_Rec_Approve').load(url, { LoanIssueGI: LoanIssueGI, EmployeeGI: EmployeeGI }, function () { });

     //   $('#ApproveSlider').removeClass('hide');


    }
}
function onAddNewClick() {
    debugger;
    //var url = "/PayRoll/LoanIssue/NewRequest";
    //$('#addNewLoan').load(url, {}, function () { });
    //$('#addNewLoan').removeClass('hide');
    $.ajax({

        type: 'GET',
        url: '/PayRoll/LoanIssue/NewRequest',
        dataType: 'html',
        data: { },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {
                
                $('#addNewLoan').html("");
                $('#addNewLoan').html(result);
                $('#addNewLoan').removeClass('hide');
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


function OnEmpChange(sender) {
    debugger;
    var Code = $(sender).val();
    if (Code != 0) {
        var url = "/PayRoll/LoanIssue/EmployeeCard";
        $('.search-profile-details').load(url, { EmpCode: Code }, function () { });
        $('.search-profile-details').show(300);
        $.ajax({

            type: 'GET',
            url: '/PayRoll/LoanIssue/EmployeeCard',
            dataType: 'html',
            data: { deduction: deduction, monthcode: month },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }

                else {
                    $('.search-profile-details').html("");
                    $('.search-profile-details').html(result);
                    $('.search-profile-details').show(300);
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

}
function DOJnDisDate() {
    var Ddate = $('#LoanIssueRequest_DisbursementDate').val();
    var doj = _GetFormattedDate($('#EmployeeHeader_DOJ').val());
    if (new Date(Ddate) < new Date(doj)) {       
        swal.fire('Disbursement Date before Date of Joining is not allowed');
        return false;
    }
    else return true;
}
function onDisbursementDateChange() {
    validateById('#LoanIssueRequest_DisbursementDate', 'disbursementDate');
    if (DOJnDisDate()) {
        $.getJSON("/PayRoll/LoanIssue/GetRepaymentStartMonth", { dte: $('#LoanIssueRequest_DisbursementDate').val() }, function (data) {

            $('#LoanIssueRequest_RepaymentStartMonth_Code option').remove();
            $('#LoanIssueRequest_RepaymentStartMonth_Code').append('<option value="">Select Month</option');

            for (var i = 0; i < data.length; i++) {
                $('#LoanIssueRequest_RepaymentStartMonth_Code').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
            }
        });
    }
}

function onLoanTypeChange() {
    debugger
    validateById('#LoanIssueRequest_LoanType_Code', 'loanType');
    $.getJSON("/PayRoll/LoanIssue/GetRepaymentMethod", { LoanType: $('#LoanIssueRequest_LoanType_Code').val() }, function (data) {
        console.log(data);
        $('#LoanIssueRequest_RepaymentMethod_Code').val(data.code);
        $('#repaymentMethod').html(data.name);
   
        
    
    }); 


    $.getJSON("/PayRoll/LoanIssue/GetLoanAmountDetails", { loanType: $('#LoanIssueRequest_LoanType_Code').val() }, function (data) {
        
        $('#CompanyLoanAmount').val(data.companyLoanAmount);
        $('#BalanceAmount').val(data.balanceAmount);
        $('#MaximumLimitPerEmployee').val(data.maximumLimitPerEmployee);
        $('#principleAmt').html(data.companyLoanAmount);
        $('#balAmt').html(data.balanceAmount);
        $('#maxLimitPerEmp').html(data.maximumLimitPerEmployee);
        $('#Tenure').val(data.tenure);

    });



}

function onLoanTypeTwoChange() {
    debugger
    validateById('#LoanIssueRequest_LoanType_Code', 'loanType');
    $.getJSON("/PayRoll/LoanIssue/GetRepaymentMethod", { LoanType: $('#LoanIssueRequest_LoanType_Code').val() }, function (data) {
        console.log(data);
        //$('#LoanIssueRequest_RepaymentMethod_Code').val(data.code);
        //$('#repaymentMethod').html(data.name);

        $('#LoanIssueRequest_RepaymentMethod_Code option').remove();
        $('#LoanIssueRequest_RepaymentMethod_Code').append('<option value="">Select Repayment Method</option');


        $('#LoanIssueRequest_RepaymentMethod_Code').append('<option value="' + data.code + '">' + data.name + '</option');

    });


    $.getJSON("/PayRoll/LoanIssue/GetLoanAmountDetails", { loanType: $('#LoanIssueRequest_LoanType_Code').val() }, function (data) {

        $('#CompanyLoanAmount').val(data.companyLoanAmount);
        $('#BalanceAmount').val(data.balanceAmount);
        $('#MaximumLimitPerEmployee').val(data.maximumLimitPerEmployee);
        $('#principleAmt').html(data.companyLoanAmount);
        $('#balAmt').html(data.balanceAmount);
        $('#maxLimitPerEmp').html(data.maximumLimitPerEmployee);
        $('#Tenure').val(data.tenure);

    });



}

function approveSubmit(LoanIssueGI,mode) {
    debugger;
         
       /* return true;*/
    /*var prevTab = "Approval Pending";*/

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
                url: '/PayRoll/LoanIssue/ApproveOrReject',
                dataType: 'json',
                data: { LoanIssueGI: LoanIssueGI, Mode: mode },
                success: function (result) {
                    if (result.success == true) {
                        swal.fire("Approved Successfully").then(function () {
                            CloseSlider('LoanIssue_Approve');

                            var url = "/PayRoll/LoanIssue/GetLoanIssueList?Tab=" + 'tab1';
                            $('#allList').load(url, function () { });


                            $("." + prevTab).removeClass("active");
                            $(".all").addClass("active");

                            $("#allList").removeClass("hide");
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
function confirmReject(LoanIssueGI, mode) {
    debugger;
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
                url: '/PayRoll/LoanIssue/ApproveOrReject',
                dataType: 'json',
                data: { LoanIssueGI: LoanIssueGI, Mode: mode },
                success: function (result) {
                    if (result.success == true) {
                        swal.fire("Rejected Successfully").then(function () {
                            CloseSlider('LoanIssue_Approve');
                            //var url = "/PayRoll/LoanIssue/GetLoanIssueList?Tab=" + 'AllList';
                            //var url = "/PayRoll/LoanIssue/LoanIssueApproveRejectDetails";
                            var url = "/PayRoll/LoanIssue/GetLoanIssueList?Tab=" + 'tab1';
                            $('#allList').load(url, function () { });
                            $("." + prevTab).removeClass("active");
                            $(".all").addClass("active");
                            $('#ApproveSlider').load(url, { LoanIssueGI: LoanIssueGI, EmployeeGI: EmployeeGI }, function () { });
                            $("#allList").removeClass("hide");
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


function onLogClick(LoanIssueGI) {
    debugger;
    /*if (Tab == 'All') {*/
    //var url = "/PayRoll/LoanIssue/LoanIssueLogDetails";
    //$('body').append('<div class="form-overlay"></div>');
    //$('#LoanIssueLogDetailsSlider').load(url, { LoanIssueGI: LoanIssueGI}, function () { });
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
  /*  $('#LoanIssueLogDetailsSlider').removeClass('hide');*/
}



function ViewFile(absoluteURI) {
    window.open(absoluteURI, "_blank");
}


function PreviewImage() {
    var path = URL.createObjectURL($('.previewImage')[0].files[0]);
    window.open(path, "_blank");
}



function onExportToExcelClick() {
    debugger;
   /* if (currentTab == 1001) {*/
    var AllTableLength = $('#LoanIssuesList_Count').val();
        if (AllTableLength > 0) {
            url = "/PayRoll/LoanIssue/ExportToExcel?TabIndex=" + currentTab + "&TabName=" + currentTabName;
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
    //}
    //else
    //{
    //    var AllTableLength = document.getElementById("AllTable").rows.length - 1;
    //    if (AllTableLength > 0) {
    //        url = "/PayRoll/LoanIssue/ExportToExcel?TabIndex=" + currentTab + "&TabName=" + currentTabName;
    //        window.location.href = url;
    //    }
    //    else {
    //        swal.fire("No data found to export");
    //    }
    //}
    
}

function onfilterClick() {
    debugger
    var tabIndex = currentTab;
    var searchId = $('#LoanIssueFilterCombo_Code').val();
    //var url = "/PayRoll/LoanIssue/ApplyFilter?TabIndex=" + tabIndex + '&SearChId=' + searchId;
    //$('body').append('<div class="form-overlay"></div>');
    //$('#FilterPartial').load(url, function () { });
    $.ajax({

        type: 'GET',
        url: '/PayRoll/LoanIssue/ApplyFilter',
        dataType: 'html',
        data: { TabIndex: tabIndex, SearChId: searchId },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {
                $('body').append('<div class="form-overlay"></div>');
                $('#FilterPartial').html("");
                $('#FilterPartial').html(result);
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


function ApplyFilter(mode) {
    debugger
    var tabIndex = $('#Tabindex').val();    
    $("#Tabindex").val(currentTab);
    
    if (mode == 'apply') {
        showSpinner();
        if (filterApplicable >= 1) {
            $.ajax({

                type: 'POST',
                url: '/PayRoll/LoanIssue/ApplyFilter',
                dataType: 'html',
                data: $('#BasicFilter').serialize(),
                success: function (result) {
                    debugger
                    CloseSlider('basicFilter');
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }

                    else {
                        $('#allList').html("");
                        $('#allList').html(result);

                        $('#allList').removeClass('hide');
                    }
                    //$('#rewardsListPartial').addClass('hide');       
                    hideSpinner();
                },

                error: function (xhr, textStatus, errorThrown) {
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
                url: '/PayRoll/LoanIssue/ApplyFilter',
                dataType: 'html',
                data: $('#BasicFilter').serialize(),
                success: function (result) {
                    debugger
                    CloseSlider('basicFilter');
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }

                    else {
                        $('#allList').html("");
                        $('#allList').html(result);

                        $('#allList').removeClass('hide');
                    }
                    //$('#rewardsListPartial').addClass('hide');     
                    hideSpinner();
                },

                error: function (xhr, textStatus, errorThrown) {
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
        $('#filterForm').removeClass('filter-apply');
        $('.reset').val('');
        $('#Optional_Filter_EmploymentStatus_Code').val(0);
        $.ajax({
            type: 'POST',
            url: '/PayRoll/LoanIssue/ApplyFilter',
            dataType: 'html',
            data: $('#BasicFilter').serialize(),
            success: function (result) {
                debugger
                //CloseSlider('assetAllocationFilter');
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }

                else {
                    $('#allList').html("");
                    $('#allList').html(result);

                    $('#allList').removeClass('hide');
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

function validateByIdForLoan(elementId, spanId,msg) {
    debugger;
    if ($(elementId).val() == "") {
        $(elementId).addClass('is-invalid');
        $("#" + spanId + "").addClass('error-message');
        $("#" + spanId + "").html("This field is required");
        return false;
    }
    else if ($(elementId).val() == 0)
    {
        $(elementId).addClass('is-invalid');
        $("#" + spanId + "").addClass('error-message');
        $("#" + spanId + "").html(msg);
        return false;
    }
    else {
        $(elementId).removeClass('is-invalid');
        $("#" + spanId + "").removeClass('error-message');
        $("#" + spanId + "").html("");
        //trim(val);        
        return true;
    }
}
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
    if (!validateByIdForLoan('#LoanIssueRequest_MonthlyEMI', 'monthlyEMI','Monthly EMI cannot be 0')) {
        i++;
    }    
  
    if (!validateByIdForLoan('#LoanIssueRequest_Tenure', 'tenure','Tenure cannot be 0')) {
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
                                var url = "/PayRoll/LoanIssue/Index";
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
function checkLoanAmt() {
    companyLoanAmt = $('#CompanyLoanAmount').val();
    balanceAmt = $('#BalanceAmount').val();
    maxLimitPerEmp = $('#MaximumLimitPerEmployee').val();
    maxTenure = $('#Tenure').val();
    newTenure = $('#LoanIssueRequest_Tenure').val();
    loanAmt = $('#LoanIssueRequest_LoanAmount').val();
    debugger;
    if (parseInt(newTenure) > parseInt(maxTenure)) {
        $('#LoanIssueRequest_Tenure').addClass('is-invalid');
        $("#tenure").addClass('error-message');
        $("#tenure").html("Tenure should be less than or equal to " + maxTenure);
        return false;
    }
    if (parseInt(loanAmt) > parseInt(balanceAmt)) {
        $('#LoanIssueRequest_LoanAmount').addClass('is-invalid');
        $("#loanAmount").addClass('error-message');
        $("#loanAmount").html("Loan amount greater than balance principle amount");
        return false;
    }
    else if (parseInt(loanAmt) > parseInt(maxLimitPerEmp))
    {
        $('#LoanIssueRequest_LoanAmount').addClass('is-invalid');
        $("#loanAmount").addClass('error-message');
        $("#loanAmount").html("Loan Amount Should be less than maximum limit alloted per employee");
        return false;
    }
    return true;
    //else {
    //    $('#LoanIssueRequest_LoanAmount').removeClass('is-invalid');
    //    $("#loanAmount").removeClass('error-message');
    //    $("#loanAmount").html("");
    //    return true;
    //}
}
function autopopulateEMITenure() {
    debugger;
    if (validateByIdForLoan('#LoanIssueRequest_LoanAmount', 'loanAmount','Loan amount cannot be 0')) {
        checkLoanAmt();
    }
    
    loanAmount = $('#LoanIssueRequest_LoanAmount').val();
    emi = $('#LoanIssueRequest_MonthlyEMI').val();
    repaymentMethod = $('#LoanIssueRequest_RepaymentMethod_Code').val();
    tenure = $('#LoanIssueRequest_Tenure').val();
    //one time settlement:1
    //EMI:2
    if (repaymentMethod!="") {   
   
    if (repaymentMethod == 1) {
        $('#LoanIssueRequest_Tenure').val(1);
        $('#LoanIssueRequest_Tenure').attr('readonly', 'true');
        $('#LoanIssueRequest_MonthlyEMI').val(loanAmount);
        $('#LoanIssueRequest_MonthlyEMI').attr('readonly', 'true');//sree
    }
    if (repaymentMethod == 2) {
        if (loanAmount != '' && tenure != '' && loanAmount != 0 && tenure != 0) {
            monthlyEmi = loanAmount / tenure;
            $('#LoanIssueRequest_MonthlyEMI').val(Math.ceil(monthlyEmi));
            //roundedMontlyEmi = Math.round(monthlyEmi).toFixed(2);
            //roundedMontlyEmi1 = monthlyEmi.toFixed(2);
            //$('#LoanIssueRequest_MonthlyEMI').val(roundedMontlyEmi);

        }
        else if (loanAmount != '' && emi != '' && loanAmount != 0 && emi != 0) {
            tenure = loanAmount / emi;
            $('#LoanIssueRequest_Tenure').val(tenure);
            $('#LoanIssueRequest_Tenure').attr('readonly', 'true');
        }
            
       
        }
    }
    validateByIdForLoan('#LoanIssueRequest_Tenure', 'tenure','Tenure cannot be 0');
    validateByIdForLoan('#LoanIssueRequest_MonthlyEMI', 'monthlyEMI','Monthly EMI cannot be 0');
}
function repaymentMethodChange() {
    debugger;
    validateById('#LoanIssueRequest_RepaymentMethod_Code', 'repaymentMethod');
    $('#LoanIssueRequest_MonthlyEMI').val('');
    $('#LoanIssueRequest_Tenure').val('');
    $('#LoanIssueRequest_Tenure').removeAttr("readonly");
    $('#LoanIssueRequest_MonthlyEMI').removeAttr("readonly");//sree

    loanAmount = $('#LoanIssueRequest_LoanAmount').val();
    emi = $('#LoanIssueRequest_MonthlyEMI').val();
    repaymentMethod = $('#LoanIssueRequest_RepaymentMethod_Code').val();
    tenure = $('#LoanIssueRequest_Tenure').val();
    if (repaymentMethod == "1") {
        $('#LoanIssueRequest_Tenure').val(1);
        $('#LoanIssueRequest_Tenure').attr('readonly', 'true');
        $('#LoanIssueRequest_MonthlyEMI').val(loanAmount);
        $('#LoanIssueRequest_MonthlyEMI').attr('readonly', 'true');//sree
    }
    if (repaymentMethod == 2) {
        if (loanAmount != '' && tenure != '' && loanAmount != 0 && tenure != 0) {
            monthlyEmi = loanAmount / tenure;
            $('#LoanIssueRequest_MonthlyEMI').val(Math.ceil(monthlyEmi));
        }
        else if (loanAmount != '' && emi != '' && loanAmount != 0 && emi != 0) {
            tenure = loanAmount / emi;
            $('#LoanIssueRequest_Tenure').val(tenure);
            $('#LoanIssueRequest_Tenure').attr('readonly', 'true');
        }


    }
}
function monthlyEMIChange() {
    debugger;
    validateByIdForLoan('#LoanIssueRequest_LoanAmount', 'loanAmount','Loan amount cannot be 0');
    validateByIdForLoan('#LoanIssueRequest_MonthlyEMI', 'monthlyEMI','Monthly EMI cannot be 0');
   

    loanAmount = $('#LoanIssueRequest_LoanAmount').val();
    emi = $('#LoanIssueRequest_MonthlyEMI').val();
    repaymentMethod = $('#LoanIssueRequest_RepaymentMethod_Code').val();
    tenure = $('#LoanIssueRequest_Tenure').val();
    emi = Math.ceil(emi);
    if (repaymentMethod == 1 && loanAmount != "") {
        $('#LoanIssueRequest_Tenure').val(1);
        $('#LoanIssueRequest_Tenure').attr('readonly', 'true');
        $('#LoanIssueRequest_LoanAmount').val(emi);
    }
    if (repaymentMethod == 2) {
        
        if (loanAmount != "" && emi != "" && loanAmount != 0 && emi != 0) {
            
            tenure = loanAmount / emi;
                /*$('#LoanIssueRequest_Tenure').val(tenure);*/
                roundedTenure = Math.round(tenure);
                $('#LoanIssueRequest_Tenure').val(roundedTenure);
        }


    }
    $('#LoanIssueRequest_MonthlyEMI').val(emi);
    validateByIdForLoan('#LoanIssueRequest_Tenure', 'tenure','Tenure cannot be 0');
    checkLoanAmt();
}
function tenureChange() {
    debugger;

    validateByIdForLoan('#LoanIssueRequest_LoanAmount', 'loanAmount', 'Loan amount cannot be 0');
    
    validateByIdForLoan('#LoanIssueRequest_Tenure', 'tenure','Tenure cannot be 0');
    checkLoanAmt();
    loanAmount = $('#LoanIssueRequest_LoanAmount').val();
    emi = $('#LoanIssueRequest_MonthlyEMI').val();
    repaymentMethod = $('#LoanIssueRequest_RepaymentMethod_Code').val();
    tenure = $('#LoanIssueRequest_Tenure').val();
    if (repaymentMethod == 1 && loanAmount != "") {
        $('#LoanIssueRequest_Tenure').val(1);
        $('#LoanIssueRequest_Tenure').attr('readonly', 'true');
        $('#LoanIssueRequest_LoanAmount').val(emi);
    }
    if (repaymentMethod == 2) {
        if (loanAmount != "" && tenure != "" && loanAmount != 0 && tenure !=0) {
            monthlyEmi = loanAmount / tenure;
            /* $('#LoanIssueRequest_MonthlyEMI').val(monthlyEmi);*/

           
            //roundedMontlyEmi = Math.round((monthlyEmi + Number.EPSILON) * 100) / 100;
            roundedMontlyEmi = Math.ceil(monthlyEmi);
            $('#LoanIssueRequest_MonthlyEMI').val(roundedMontlyEmi);

        }
        else if (loanAmount != "" && emi != "" && loanAmount != 0 && tenure != 0) {
            tenure = loanAmount / emi;
            $('#LoanIssueRequest_Tenure').val(tenure);
            /* $('#LoanIssueRequest_Tenure').attr('readonly', 'true');*/
        }


    }
    validateByIdForLoan('#LoanIssueRequest_MonthlyEMI', 'monthlyEMI','Monthly EMI cannot be 0');

}

function compareLoanAmtAndMonthlyEmi() {
    var loanAmt = $('#LoanIssueRequest_LoanAmount').val();
    var monthlyEMI = $('#LoanIssueRequest_MonthlyEMI').val();
    if (parseInt(loanAmt) < parseInt(monthlyEMI)) {
        $('#LoanIssueRequest_MonthlyEMI').addClass('is-invalid');
        $('#monthlyEMI').addClass('error-message');
        $('#monthlyEMI').html('Monthly EMI should be less than Loan Amount');
    }
    else {
        $('#LoanIssueRequest_MonthlyEMI').removeClass('is-invalid');
        $('#monthlyEMI').removeClass('error-message');
        $('#monthlyEMI').html('');
    }

}



function ImportFromExcelPopUp() {

    $('.popupformcontrols').val('');
    //$('#Excel_FileData').trigger('click');
    var m = $('.bd-example-modal-lg');
    $('#fieldName').html($('#LoanBulkUploadingType_Code option:selected').text());
    m.modal('show');


}


function onImportExcelClick() {
    debugger
    /////$('#LoanDataBulkUpload')[0].reset();
    var i = 0;
    if (!validateById('#LoanIssueRequest_LoanType_Code', 'loanType')) {
        i++;
    }
    if (!validateById('#LoanIssueRequest_Pourpose', 'purpose')) {
        i++;
    }
    if (!validateById('#LoanIssueRequest_RepaymentMethod_Code', 'repaymentMethod')) {
        i++;
    }
    if (!validateById('#LoanIssueRequest_DisbursementDate', 'disbursementDate')) {
        i++;
    }
    if (!validateById('#LoanIssueRequest_RepaymentStartMonth_Code', 'repaymentStartMonth')) {
        i++;
    }
    if (i == 0) {
        var Data = new FormData($('#LoanDataBulkUpload')[0]);

        $("#Excel_FileData").val('');
        $.ajax({
            type: 'POST',
            url: '/PayRoll/LoanIssue/ImportFromExcel?LoanType=' + $('#LoanType_Code option:selected').text(),

            contentType: false,
            processData: false,
            data: Data,
            success: function (result) {
                debugger;
                if (result.success == true) {
                    $("#Excel_FileData").val('');
                    swal.fire("Uploaded Successfully").then(function () {
                        bulkloanupload(result.data)
                        var m = $('.bd-example-modal-lg');
                        m.modal('hide');
                    });
                }
                else {
                    $("#Excel_FileData").val('');
                    if (result.message != null) {
                        $('#Unmatched').html('')
                        swal.fire(result.message);
                    }
                    else {
                        $('#Unmatched').html(result)
                    }
                }
            },
            error: function (xhr, textStatus, errorThrown) {
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
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
        $("#Excel_FileData").val('');

    }
}
function GetEmpProfilePrint(RequestCode) {


    var url = "/PayRoll/LoanIssue/GetEmpProfilePrint?RequestCode=" + RequestCode;

    //console.log(url);
    window.location.href = url;
    return true
}


function bulkloanupload(data) {
    debugger
    $.ajax({

        type: 'POST',
        url: '/PayRoll/LoanIssue/BulkUploadpage',
        dataType: 'html',
        data: data,
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {

                $('#allList').html("");
                $('#allList').html(result);
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