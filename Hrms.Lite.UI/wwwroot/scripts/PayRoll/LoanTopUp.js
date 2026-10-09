
function getList(sender) {
    debugger;
    showSpinner();
/*   var selValue = $('#LoanTopUpDetails_LoanType_Code').val();*/
    var selValue = sender.value;
    $('#LoanTopUpDetails_LoanType_Code').val(selValue);
    if (selValue!='') {
        $('.filterDropDown').val(selValue);
        //var url = "/PayRoll/LoanTopUp/GetList?loantype=" + selValue;
        //$('#loantopuplist').load(url, function () { hideSpinner(); });
        $.ajax({

            type: 'GET',
            url: '/PayRoll/LoanTopUp/GetList',
            dataType: 'html',
            data: { loantype: selValue },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }

                else {
                    $('#loantopuplist').html("");
                    $('#loantopuplist').html(result);
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
    else {
        var url = "/PayRoll/LoanTopUp/Index";
        window.location.href = url;
    }
}


function CheckDisbursementDate() {
    var DisbursementDate = new Date($('#NewDisbursementDate').val());
    var LoanIssuedDisbursementDate = new Date($('#DisbursementDate').val());
    if (DisbursementDate < LoanIssuedDisbursementDate) {
        $('#NewDisbursementDate').addClass('is-invalid');
        $("#DisDate").addClass('error-message');
        $("#DisDate").html("Top up Disbursement date should be greater than loan issued disbursement date");
    }
    else {
        $('#NewDisbursementDate').removeClass('is-invalid');
        $("#DisDate").removeClass('error-message');
        $("#DisDate").html("");
    }
}

function OnEditLoanTopUpClick(LoanIssueGI, EmployeeGI, LoanBalance) {
    debugger;
    
    //var url = "/PayRoll/LoanTopUp/GetLoanTopUpDetails";
    //$('#editSlider').load(url, { LoanIssueGI: LoanIssueGI, EmployeeGI: EmployeeGI, LoanBalance: LoanBalance }, function () { });
    $.ajax({

        type: 'GET',
        url: '/PayRoll/LoanTopUp/GetLoanTopUpDetails',
        dataType: 'html',
        data: { LoanIssueGI: LoanIssueGI, EmployeeGI: EmployeeGI, LoanBalance: LoanBalance, LoanType: $('#LoanTopUpDetails_LoanType_Code').val() },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {
                $('body').append('<div class="form-overlay"></div>');
                $('#editSlider').html("");
                $('#editSlider').html(result);
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
function checkLoanAmt() {
    companyLoanAmt = $('#CmpLoanAmount').val();
    balanceAmt = $('#BalanceAmt').val();
    maxLimitPerEmp = $('#MaxLimitPerEmployee').val();
    maxTenure = $('#Tenure').val();
    newTenure = $('#RemainingTenure').val();
    loanAmt = $('#NewBalanceAmount').val();    
    var i = 0;
    debugger;
    if (parseInt(newTenure) > parseInt(maxTenure)) {
        $('#RemainingTenure').addClass('is-invalid');
        $("#RemTenure").addClass('error-message');
        $("#RemTenure").html("Tenure should be less than or equal to " + maxTenure);
        i++;
    }
    if (parseInt(loanAmt) > parseInt(balanceAmt)) {
        $('#TopUpAmount').addClass('is-invalid');
        $("#TopupAmt").addClass('error-message');
        $("#TopupAmt").html("Loan amount Should be less than balance principle amount");
        i++;
    }
    else if (parseInt(loanAmt) > parseInt(maxLimitPerEmp)) {
        $('#TopUpAmount').addClass('is-invalid');
        $("#TopupAmt").addClass('error-message');
        $("#TopupAmt").html("Loan Amount Should be less than maximum limit alloted per employee");
        i++;
    }
    if (i == 0)
        return true;
    else
        return false;
    //else {
    //    $('#LoanIssueRequest_LoanAmount').removeClass('is-invalid');
    //    $("#loanAmount").removeClass('error-message');
    //    $("#loanAmount").html("");
    //    return true;
    //}
}

function OnTopUpAmountClick() {


    if ($('#NewEMIAmount').val() != '') {
        /* var princAmount = parseInt($('#LoanTopUpDetails_PrincipleAmount').val());*/
        var BalAmt = $('#BalanceAmount').val() == '' ? 0 : parseInt($('#BalanceAmount').val());
        var topUpAmount = $('#TopUpAmount').val() == '' ? 0 : parseInt($('#TopUpAmount').val());
        var EmiAmount = $('#NewEMIAmount').val() == '' ? 0 : parseInt($('#NewEMIAmount').val());
        var tenureRemaining = $('#TotalTenure').val() == '' ? 0 : parseInt($('#TotalTenure').val());
        var totalAmt = BalAmt + topUpAmount;
        var newTenure = totalAmt / EmiAmount;
        var tenureRemaining = Math.ceil(newTenure)
        //tenureRemaining = newTenure;
        /*    $('#BalanceTenure').val(tenureRemaining.toFixed(2));*/
        $('#RemainingTenure').val(parseInt(tenureRemaining));
        NewBalance();
    }
}



function OnNewEMIclick() {

    /* var princAmount = parseInt($('#LoanTopUpDetails_PrincipleAmount').val());*/
    if ($('#NewEMIAmount').val() != '') {
        var BalAmt = $('#BalanceAmount').val() == '' ? 0 : parseInt($('#BalanceAmount').val());
        var topUpAmount = $('#TopUpAmount').val() == '' ? 0 : parseInt($('#TopUpAmount').val());
        var EmiAmount = $('#NewEMIAmount').val() == '' ? 0 : parseInt($('#NewEMIAmount').val());
        var totalAmt = BalAmt + topUpAmount;
        var newTenure = totalAmt / EmiAmount;
        var newval = Math.ceil(newTenure)    
        $('#RemainingTenure').val(parseInt(newval));
        validateById('#RemainingTenure', 'RemTenure');
    }

}

function OnRemainingTenureClick() {

    if ($('#RemainingTenure').val() != '') {
    /*var princAmount = parseInt($('#LoanTopUpDetails_PrincipleAmount').val());*/
        var BalAmt = $('#BalanceAmount').val() == '' ? 0 : parseInt($('#BalanceAmount').val());
        var topUpAmount = $('#TopUpAmount').val() == '' ? 0 : parseInt($('#TopUpAmount').val());
        var tenureRemaining = $('#RemainingTenure').val() == '' ? 0 : parseInt($('#RemainingTenure').val());
        var totalAmt = BalAmt + topUpAmount; 
        var newEMI = totalAmt / tenureRemaining;
        var newval = Math.round(newEMI);
        $('#NewEMIAmount').val(parseInt(newval));
        validateById('#NewEMIAmount', 'NewEmi');
    }
   /* $('#NewEMIAmount').val(parseFloat(newEMI).toFixed(2));*/
}

function NewBalance() {
    debugger;
    var BalAmt = $('#BalanceAmount').val() == '' ? 0 : parseInt($('#BalanceAmount').val());
    var topUpAmount = $('#TopUpAmount').val() == '' ? 0 : parseInt($('#TopUpAmount').val());
    var totalAmt = BalAmt + topUpAmount;
    $('#NewBalanceAmount').val(parseInt(totalAmt));
    $('.newBalance').html(totalAmt);
}


function LoanTopUpSave() {
    debugger;
    var type = $('#LoanTopUpDetails_LoanType_Code').val();

    //var ddSelected = document.getElementById('loantype');
    //var val = ddSelected.value;
    //var sel = $('#LoanType_Code option:selected').val();
    /*var strUser = ddSelected.value;*/
    if (validateLoanTopUp()) {

        debugger
    //console.log($('#LoanTopUp-Save').serialize())
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
                    url: '/PayRoll/LoanTopUp/Save',
                    dataType: 'json',
                    data: $('#LoanTopUp-Save').serialize(),
                    success: function (result) {
                        debugger;
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                CloseSlider('edit');

                                var url = "/PayRoll/LoanTopUp/GetList?ddSelected=" + type;
                                $('#loantopuplist').load(url, function () { });

                            });

                        }

                        else {
                            swal.fire(result.message).then(function () {

                            });
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
                return false;
            }
        })
       

    }
    else {
        return false;
    }
}



function ExportToExcel() {
    debugger;

    

        var loanType = $('#LoanTopUpDetails_LoanType_Code option:selected').text();
        var type = $('#LoanTopUpDetails_LoanType_Code').val();
    var TableLength = $('#LoanIssuesList_Count').val();
        if (TableLength > 0) {
            /*url = "/PayRoll/LoanTopUp/ExportToExcel?loanType" + $('#LoanTopUpDetails_LoanType_Code option:selected').text() + "&type" + $('#LoanTopUpDetails_LoanType_Code').val();*/


            url = "/PayRoll/LoanTopUp/ExportToExcel?loanType" + loanType + "&type" + type + '&data=' + $('#BasicFilter').serialize();
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
    
}

function ApplyFilter(mode) {
    debugger

    if (mode == 'apply') {
        showSpinner();
        if (filterApplicable >= 1) {
            $.ajax({

                type: 'POST',
                url: '/PayRoll/LoanTopUp/ApplyFilter',
                dataType: 'html',
                data: $('#BasicFilter').serialize(),
                success: function (result) {
                    debugger
                    CloseSlider('basicFilter');
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }

                    else {
                        $('#loantopuplist').html("");
                        $('#loantopuplist').html(result);

                        $('#loantopuplist').removeClass('hide');
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
            $('#filterForm').removeClass('filter-apply');}
        else {
            $.ajax({

                type: 'POST',
                url: '/PayRoll/LoanTopUp/ApplyFilter',
                dataType: 'html',
                data: $('#BasicFilter').serialize(),
                success: function (result) {
                    debugger
                    CloseSlider('basicFilter');
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }

                    else {
                        $('#loantopuplist').html("");
                        $('#loantopuplist').html(result);

                        $('#loantopuplist').removeClass('hide');
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
            url: '/PayRoll/LoanTopUp/ApplyFilter',
            dataType: 'html',
            data: $('#BasicFilter').serialize(),
            success: function (result) {
                debugger

                //CloseSlider('assetAllocationFilter');

                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }

                else {
                    $('#loantopuplist').html("");
                    $('#loantopuplist').html(result);

                    $('#loantopuplist').removeClass('hide');
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



function validateLoanTopUp()
{
    var i = 0;
    if (!validateById('#TopUpAmount', 'TopupAmt')) {
        i++;
    }
    if (!validateById('#NewEMIAmount', 'NewEmi')) {
        i++;
    }
    if (!validateById('#RemainingTenure', 'RemTenure')) {
        i++;
    }
    //if (!validateById('#NewDisbursementDate', 'Ddate')) {
    //    i++;
    //}
    if (!validateById('#NewDisbursementDate', 'DisDate')) {
        i++;
    }
    if (!checkLoanAmt()) {
        i++;
    }
    if (i == 0)
        return true;
    else
        return false;
}