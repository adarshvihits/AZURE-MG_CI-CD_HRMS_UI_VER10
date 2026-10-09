var prevTab = 'tab1';
var currentTab = 'All';
var currTabName = 'All';
var CurrTabNameSalSplitUp = 'Pending';
var CurrTabNameEmpCheck = 'Pending';
var CurrTabNameCredCheck = 'Pending';
function Tab(Tab, TabIndex,TabName) {
    debugger;
    /* showSpinner();*/
    currentTab = TabIndex;
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");
    CurrTabNameSalSplitUp = TabName
    //$('#tabsDropdownDivision').val(currentTab);
    //var url = "/DataBank/VacancyManagement/GetList";

    //$('#VacancyList').load(url, { TabIndex: TabIndex }, function () { });
    $.ajax({

        type: 'GET',url: '/Recruitment/OfferProcessing/GetSalarySplitUpList',
        dataType: 'html',
        data: { TabName: TabName },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {
                $('#OfferVerificationLst').html("");
                $('#OfferVerificationLst').html(result);
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
    prevTab = Tab;

}

function CTCCalculation() {
    debugger;
    var TotAmt = parseFloat("0");
    var TotGrossAmt = parseFloat("0");
    var TotCTCAmt = parseFloat("0");
    var TotDedAmt = parseFloat("0");
    var TakeHome = parseFloat("0");
    $('.AllowanceAmount').each(function (i, obj) {//Gross Salary
        var Amt = parseFloat($("#Compensation_FixedAllowance_" + i + "__Amount").val());

        debugger;
        if (isNaN(Amt)) {
            Amt = parseFloat("0");
        }
        TotAmt = TotAmt + Amt;

        TotGrossAmt = TotGrossAmt + Amt;

        $("#GrossSalary").html(TotGrossAmt);
        $("#Compensation_GrossSalary").val(TotGrossAmt);
    });
    $('.ProvisionsAmount').each(function (i, obj) {//CTC
        var Amt = parseFloat($("#Compensation_FixedProvisions_" + i + "__Amount").val());

        debugger;
        if (isNaN(Amt)) {
            Amt = parseFloat("0");
        }
        TotCTCAmt = TotCTCAmt + Amt;



    });

    $("#CTC").html(TotCTCAmt + TotGrossAmt);
    $("#Compensation_CTC").val(TotCTCAmt + TotGrossAmt);

    $('.DeductionsAmount').each(function (i, obj) {//Total Dedcutions
        var Amt = parseFloat($("#Compensation_FixedDeduction_" + i + "__Amount").val());
        debugger;
        if (isNaN(Amt)) {
            Amt = parseFloat("0");
        }
        TotDedAmt = TotDedAmt + Amt;

        $("#totDeductions").html(TotDedAmt);
        //$("#Compensation_CTC").val(TotCTCAmt + TotGrossAmt);

    });

    TakeHome = TotGrossAmt - TotDedAmt;//Take Home

    $("#takeHome").html(TakeHome);
    //$("#Compensation_CTC").val(TotCTCAmt + TotGrossAmt);


}






function OnEmployeeClick(PreEnrollCode, PreEnrollGI,Tab) {
    debugger


    var TotAmt = parseFloat("0");
    var TotGrossAmt = parseFloat("0");

    $('.AllowanceAmount').each(function (i, obj) {//Gross Salary
        var Amt = parseFloat($("#Compensation_FixedAllowance_" + i + "__Amount").val());

        debugger;
        if (isNaN(Amt)) {
            Amt = parseFloat("0");
        }
        TotAmt = TotAmt + Amt;

        TotGrossAmt = TotGrossAmt + Amt;

    });



    var url = "/Recruitment/OfferProcessing/GetOfferLetter?PreEnrollCode=" + PreEnrollCode + "&PreEnrollGI=" + PreEnrollGI + "&Gross=" + TotGrossAmt + "&Tab=" + Tab;
    //window.open(url, "_blank");
    window.location.href = url;

}










function EmpoyementCheckTab(Tab, TabIndex, TabName) {
    debugger;
    /* showSpinner();*/
    currentTab = TabIndex;
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");
    CurrTabNameEmpCheck = TabName
    //$('#tabsDropdownDivision').val(currentTab);
    //var url = "/DataBank/VacancyManagement/GetList";

    //$('#VacancyList').load(url, { TabIndex: TabIndex }, function () { });
    $.ajax({

        type: 'GET',
        url: '/Recruitment/OfferProcessing/GetEmployementCheckList',
        dataType: 'html',
        data: { TabName: TabName },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {
                $('#empCheckList').html("");
                $('#empCheckList').html(result);
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
    prevTab = Tab;

}
function CreditCheckTab(Tab, TabIndex, TabName) {
    debugger;
    /* showSpinner();*/
    currentTab = TabIndex;
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");
    CurrTabNameCredCheck = TabName
    //$('#tabsDropdownDivision').val(currentTab);
    //var url = "/DataBank/VacancyManagement/GetList";

    //$('#VacancyList').load(url, { TabIndex: TabIndex }, function () { });
    $.ajax({

        type: 'GET',
        url: '/Recruitment/OfferProcessing/GetCreditCheckList',
        dataType: 'html',
        data: { TabName: TabName },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {
                $('#CredCheckList').html("");
                $('#CredCheckList').html(result);
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
    prevTab = Tab;

}
function tabClick(Tab, TabIndex, TabName) {
    debugger;
    currentTab = TabIndex;
    currTabName = TabName;
    //$('#TabCode').val(TabId);
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");
    showSpinner();
    //var url = '/Recruitment/OfferProcessing/Getlist'
    //$('#offerInitiationList').load(url, { TabIndex: TabIndex }, function () { hideSpinner(); });
    $.ajax({

        type: 'GET',
        url: '/Recruitment/OfferProcessing/Getlist',
        dataType: 'html',
        data: { TabIndex: TabIndex },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {
                $('#offerInitiationList').html("");
                $('#offerInitiationList').html(result);
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
    prevTab = Tab;

}
function validateDropdowns() {
    debugger;
    var i = 0;
    if (!validateById('#ESIDeduction_Code', 'EsiDed')) {
        i++;
    }
    if (!validateById('#PayrollMonth_Code', 'PayrollM')) {
        i++;
    }    

    if (i == 0)
        return true;
    else
        return false;

}


function onEmpClick(PreEnrollGI,form)
{
    debugger

    //var url = '/Recruitment/OfferProcessing/GetCheckingDetails';
    //    $('body').append('<div class="form-overlay"></div>');
    
/*        window.location.href = '/Recruitment/OfferProcessing/GetCheckingDetails?ResumeDataBankGI=' + ResumeDataBankGI + '&formName=' + form;*/

       /* $('#creditSlider').load(url, { PreEnrollGI: PreEnrollGI, formName: form }, function () {*/
            $.ajax({

                type: 'GET',
                url: '/Recruitment/OfferProcessing/GetCheckingDetails',
                dataType: 'html',
                data: { PreEnrollGI: PreEnrollGI, formName: form },
                success: function (result) {
                    debugger;
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }

                    else {
                        $('body').append('<div class="form-overlay"></div>');
                        if (form == 'CreditChecking') {
                            $('#creditSlider').html("");
                            $('#creditSlider').html(result);
                        }
                        else {
                            $('#employmntSlider').html("");
                            $('#employmntSlider').html(result);
                        }
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
        
    //else
    //    $('#employmntSlider').load(url, { PreEnrollGI: PreEnrollGI, formName: form }, function () { });
}

function validateCreditCheck() {
    var i = 0;
    if (!validateById('#CreditChecking_CreditScore', 'credScore')) {
        i++;
    }

}

function saveCheck(form) {
    var i = 0;
    debugger
    //if (!validateById('#ESISettingsDetails_ESIGroup_Code', 'esiGrp')) {
    //    i++;
    //}
    if (form == "CreditChecking") {
        if (!validateById('#CreditChecking_CreditScore', 'credScore')) {
            return false;
        }
        url = '/Recruitment/OfferProcessing/SaveCreditChecking';
    }
    else {
        url = '/Recruitment/OfferProcessing/SaveEmployeementChecking';
    }

    if (i == 0)
    {
    swal.fire({
        text: "Do you want to Save?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            var MPPersonalInfo = new FormData($('#creditEmpSave')[0]);
            $.ajax({

                type: 'POST',
                url: url,               
                contentType: false,
                processData: false,
                dataType: 'json',
                data: MPPersonalInfo,               
                success: function (result) {              
                    if (result.success == true) {
                        swal.fire("Saved Successfully").then(function () {
                            CloseSlider('op_checking');
                            if (form == "CreditChecking")
                                url = '/Recruitment/OfferProcessing/CreditChecking';
                            else
                                url = '/Recruitment/OfferProcessing/EmployeementChecking';                         
                                  window.location.href = url;
                            //$.ajax({

                            //    type: 'GET',
                            //    url: url,
                            //    dataType: 'html',
                            //    success: function (result) {
                            //        debugger
                            //        console.log(result);
                            //        if (form == "CreditChecking") {
                            //            $('.credit-checking-list').html("");
                            //            $('.credit-checking-list').html(jQuery(result).find('.credit-checking-list').html());
                            //        }
                            //        else {
                            //            $('.employment-checking-list').html("");
                            //            $('.employment-checking-list').html(jQuery(result).find('.employment-checking-list').html());
                            //        }
                            //    }
                            //    });

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
    else
        return false;
}

function ApproveReject(GI, mode) {
    debugger

    


    if (mode == "REJECT") {
        $("#Mode").val('REJECT');
        text1 = "Do you want to Reject?";
    }
    else {
        if (parseInt($('#Compensation_OfferedSalary').val()) > parseInt($('#MaximumSalary').val()))
            text1 = "Offered salary greater than maximum salary. Do you want to continue?";

        else if (parseInt($('#Compensation_OfferedSalary').val()) < parseInt($('#MinimumSalary').val()))
            text1 = "Offered salary less than minimum salary. Do you want to continue?";
        else
            text1 = "Do you want to Approve?";        
        $("#Mode").val('APPROVE');
    }

    swal.fire({
        text: text1,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({

                type: 'POST',
                url: '/Recruitment/OfferProcessing/OfferInitiationApproveorReject',
                dataType: 'json',
                data: $("#offerInitiationApprove").serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            var url = "/Recruitment/OfferProcessing/OfferInitiation";
                            window.location.href = url;
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



function ExportTopExcelClick() {
    debugger;
    var CreditCheckingTable = $('#OnBoardingList_Count').val();
    /* var CancellationTable = document.getElementById("CancellationListTable").rows.length - 1;*/

    if (CreditCheckingTable > 0) {
        url = "/Recruitment/OfferProcessing/ExportToExcel?TabName=" + CurrTabNameCredCheck;
        window.location.href = url;

    }
    else {
        swal.fire("No data found to export");
    }
}


function ClickOnExportToExcel() {
    debugger;
    var EmploymentChecking = $('#OnBoardingList_Count').val();
    /* var CancellationTable = document.getElementById("CancellationListTable").rows.length - 1;*/

    if (EmploymentChecking > 0) {
        url = "/Recruitment/OfferProcessing/EmploymentCheckExportToExcel?TabName=" + CurrTabNameEmpCheck;
        window.location.href = url;

    }
    else {
        swal.fire("No data found to export");
    }
}

function SalarySplitUpExportToExcel() {
    debugger;
    var SalarySplitupTable = $('#OnBoardingList_Count').val();
    /* var CancellationTable = document.getElementById("CancellationListTable").rows.length - 1;*/

    if (SalarySplitupTable > 0) {
        url = "/Recruitment/OfferProcessing/SalarySplitUpexportToExcel?TabName=" + CurrTabNameSalSplitUp;
        window.location.href = url;

    }
    else {
        swal.fire("No data found to export");
    }
}

function ExportToExcelOfferInitiation() {
    debugger
    var TableLength = $('#OnBoardingList_Count').val();
    if (TableLength > 0) {
        url = "/Recruitment/OfferProcessing/ExportToExcelOfferInitiation?TabIndex=" + currentTab;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}

function viewOfferLetter(url) {
    debugger;

    window.open(url, '_blank');


    //var TotAmt = parseFloat("0");
    //var TotGrossAmt = parseFloat("0");

    //$('.AllowanceAmount').each(function (i, obj) {//Gross Salary
    //    var Amt = parseFloat($("#Compensation_FixedAllowance_" + i + "__Amount").val());

    //    debugger;
    //    if (isNaN(Amt)) {
    //        Amt = parseFloat("0");
    //    }
    //    TotAmt = TotAmt + Amt;

    //    TotGrossAmt = TotGrossAmt + Amt;

    //});


    //var url = "/Recruitment/OfferProcessing/OfferLetter?GI=" + GI + "&Tab=" + tab ;
    //window.open(url, "_blank");
}


function Preview() {
    debugger
    var path = URL.createObjectURL($('.PreviewImage')[0].files[0]);
    window.open(path, "_blank");
}


function AcceptOrReject(PreEnrollGI,mode) {
    debugger
    var msg = '';
    if (mode == "Reject") {
        swal.fire({
            title: 'Do you want to Reject?',
            //text: "Reason to unlock",
            inputLabel: 'Remarks',
            input: 'text',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok',

            preConfirm: (value) => {
                if (!value) {
                    Swal.showValidationMessage(
                        'Remarks is mandatory'
                    )
                }
            }

        }).then((result) => {
            if (result.value) {
                
                this.AcceptReject(PreEnrollGI, mode, result.value);
            }

        });
    }
    else {
        swal.fire({
            text: 'Do you want to Accept?',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                this.AcceptReject(PreEnrollGI, mode, '');
            }
            else {
                return false;
            }
        })
    }

}

function AcceptReject(PreEnrollGI, mode,Remarks) {
    $.ajax({

        type: 'POST',
        url: '/Recruitment/OfferProcessing/OfferAcceptance',
        dataType: 'json',
        data: { PreEnrollGI: PreEnrollGI, mode: mode, Remarks: Remarks},
        success: function (result) {
            if (result.success == true) {
                swal.fire(result.message).then(function () {
                    tabClick('tab3', 'Offer_Acceptance_Pending', 'Offer Acceptance pending')
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

function OfferInitiationReject(PreEnrollGI,mode) {
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
                url: '/Recruitment/OfferProcessing/OfferInitiationApproveorReject',
                dataType: 'json',
                data: { PreEnrollGI: PreEnrollGI, Mode: mode },
                success: function (result) {
                    swal.fire(result.message).then(function () {
                        tabClick('tab2', 'Offer_Initiation_Pending', 'Offer Initiation pending');
                    });

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
    })

  
}