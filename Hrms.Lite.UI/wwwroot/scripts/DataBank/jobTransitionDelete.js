function onDeleteClick(EmployeeGI, HistoryGI) {
    var url = "/DataBank/JobTransitionDelete/GetDetails?EmployeeGI=" + EmployeeGI + "&HistoryGI=" + HistoryGI;
    window.location.href = url;
}

function Delete(HistoryGI) {
    //var ApproverComments = $('#JobTransition_Approve_ApproverComments').val();
    debugger;
    var ReasonForDelete = $('#JobTransition_Approve_ReasonForDelete').val();
    if (ReasonForDelete != "") {
        swal.fire({
            text: "Do you want to delete?",
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
                    url: '/DataBank/JobTransitionDelete/Delete',
                    dataType: 'json',
                    data: { HistoryGI: HistoryGI, Remarks: ReasonForDelete },
                    success: function (result) {
                        debugger;
                        if (result.success) {
                            //swal.fire(result.message);
                            swal.fire(result.message).then(() => {
                                var url = "/DataBank/JobTransitionDelete/Index";
                                window.location.href = url;
                            });
                        }
                        else {
                            debugger;
                            swal.fire(result.message);
                        }
                        //$('#rewardsListPartial').addClass('hide');               
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
        });
       
    }
    else {
        $('#JobTransition_Approve_ReasonForDelete').addClass('is-invalid');
        $("#DeleteReason").addClass('error-message');
        $("#DeleteReason").html("This field is required");
        document.getElementById("JobTransition_Approve_ReasonForDelete").focus();
    }
    
}


function ApplyFilter(mode) {
    debugger    
    if (mode == 'apply') {
        if (filterApplicable >= 1) {
            $.ajax({

                type: 'POST',
                url: '/DataBank/JobTransitionDelete/ApplyFilter',
                dataType: 'html',
                data: $('#BasicFilter').serialize(),
                success: function (result) {
                    CloseSlider('basicFilter');
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }
                    else {
                        $('#jobTransitionDeleteList').html("");
                        $('#jobTransitionDeleteList').html(result);
                        $('#jobTransitionDeleteList').removeClass('hide');
                    }
                    hideSpinner();
                    //$('#rewardsListPartial').addClass('hide');               
                },


                error: function (xhr, textStatus, errorThrown) {
                    document.body.id = "kt_body";
                    $('body').html(xhr.responseText);
                }
            });
            $('#filterForm').removeClass('filter-apply');
        }
        else {
            showSpinner();
            $.ajax({

                type: 'POST',
                url: '/DataBank/JobTransitionDelete/ApplyFilter',
                dataType: 'html',
                data: $('#BasicFilter').serialize(),
                success: function (result) {
                    CloseSlider('basicFilter');
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }
                    else {
                        $('#jobTransitionDeleteList').html("");
                        $('#jobTransitionDeleteList').html(result);
                        $('#jobTransitionDeleteList').removeClass('hide');
                    }
                    hideSpinner();
                          
                },


                error: function (xhr, textStatus, errorThrown) {
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
            url: '/DataBank/JobTransitionDelete/ApplyFilter',
            dataType: 'html',
            data: $('#BasicFilter').serialize(),
            success: function (result) {
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('#jobTransitionDeleteList').html("");
                    $('#jobTransitionDeleteList').html(result);
                    $('#jobTransitionDeleteList').removeClass('hide');
                }
              
                hideSpinner();
            },


            error: function (xhr, textStatus, errorThrown) {
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);
            }
        });
    }
}

function ExportToExcel() {
    var TableLength = $('#JobTransitionList_Count').val();
    if (TableLength > 0) {
    var url = "/DataBank/JobTransitionDelete/ExportToExcel";
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}

function CTCCalculation() {
    debugger;
    var TotAmt = parseFloat("0");
    var CurrTotAmt = parseFloat("0");
    var TotGrossAmt = parseFloat("0");
    var CurrTotGrossAmt = parseFloat("0");
    var TotCTCAmt = parseFloat("0");
    var CurrTotCTCAmt = parseFloat("0");
    var TotDedAmt = parseFloat("0");
    var CurrTotDedAmt = parseFloat("0");
    var TakeHome = parseFloat("0");
    var CurrTakeHome = parseFloat("0");
    $('.AllowanceAmount').each(function (i, obj) {
        var Amt = parseFloat($("#JobTransition_FixedAllowance_" + i + "__NewAmount").val());
        var CurrAmt = parseFloat($("#JobTransition_FixedAllowance_" + i + "__CurrentAmount").val());
        debugger;
        if (isNaN(Amt)) {
            Amt = parseFloat("0");
            CurrAmt = parseFloat("0");
        }
        TotAmt = TotAmt + Amt;
        CurrTotAmt = CurrTotAmt + CurrAmt;

        TotGrossAmt = TotGrossAmt + Amt;
        CurrTotGrossAmt = CurrTotGrossAmt + CurrAmt;

        $("#NewGrossSal").html(TotGrossAmt);
        $("#CurrGrossSal").html(CurrTotGrossAmt);
        $("#JobTransition_JobDetail_CurrentGrossSalary").val(CurrTotGrossAmt);
        $("#JobTransition_JobDetail_NewGrossSalary").val(TotGrossAmt);
    });
    $('.ProvisionsAmount').each(function (i, obj) {
        var Amt = parseFloat($("#JobTransition_FixedProvision_" + i + "__NewAmount").val());
        var CurrAmt = parseFloat($("#JobTransition_FixedProvision_" + i + "__CurrentAmount").val());

        debugger;
        if (isNaN(Amt) || Amt == 'undefined') {
            Amt = parseFloat("0");
            CurrAmt = parseFloat("0");
        }
        TotCTCAmt = TotCTCAmt + Amt;
        CurrTotCTCAmt = CurrTotCTCAmt + CurrAmt;

 
    });
    $("#NewCTC").html(TotCTCAmt + TotGrossAmt);
    $("#CurrCTC").html(CurrTotCTCAmt + CurrTotGrossAmt);
    $("#JobTransition_JobDetail_CurrentCTC").val(CurrTotCTCAmt + CurrTotGrossAmt);
    $("#JobTransition_JobDetail_NewCTC").val(TotCTCAmt + TotGrossAmt);
    $('.DeductionsAmount').each(function (i, obj) {//Total Dedcutions
        var Amt = parseFloat($("#JobTransition_FixedDeduction_" + i + "__NewAmount").val());
        var CurrAmt = parseFloat($("#JobTransition_FixedDeduction_" + i + "__CurrentAmount").val());
        debugger;
        if (isNaN(Amt)) {
            Amt = parseFloat("0");
        }
        TotDedAmt = TotDedAmt + Amt;
        CurrTotDedAmt = CurrTotDedAmt + CurrAmt;

        //$("#totDeductions").html(TotDedAmt);
        //$("#Compensation_CTC").val(TotCTCAmt + TotGrossAmt);

    });

    TakeHome = TotGrossAmt - TotDedAmt;//Take Home
    CurrTakeHome = CurrTotGrossAmt - CurrTotDedAmt;//Take Home

    $("#CurrTakeHome").html(CurrTakeHome);
    $("#NewTakeHome").html(TakeHome);
    //$("#Compensation_CTC").val(TotCTCAmt + TotGrossAmt);

}
