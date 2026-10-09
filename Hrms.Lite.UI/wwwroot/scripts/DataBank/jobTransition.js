var prevTab = 'all';
var currentTab = 0;  //code for 'all'
var currTabName = 'All';

function onPRChange()
{
    var PR = $("#JobTransition_JobDetail_PRApplicability_Code option:selected").text();
    var flag = false;
    if (PR == "YES" && $("#JobTransition_JobDetail_EffectiveDate").val() != "") {

        $.ajax({

            type: 'GET',
            url: '/DataBank/JobTransition/PR_Appl_Validation',
            dataType: 'json',
            async: false,
            data: { EffDate: $("#JobTransition_JobDetail_EffectiveDate").val(), EmpCode: $("#EmployeeHeader_EmployeeCode").val() },
            success: function (result) {
                debugger
                if (result.success == false) {

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
   
}
function onDeductionChekboxClick(index) {
    debugger;
   
    var msg = '';
    if ($('#JobTransition_JobDetail_NewPTGroup_Code').val() == '') {
        if ($('#JobTransition_VariableDeduction_' + index + '__DeductionType_Code').val() == 6) {
            if ($('#JobTransition_VariableDeduction_' + index + '__Applicable').is(':checked') == true) {
                
                msg = 'PT is ticked but PT group is not selected';
                swal.fire(msg).then(function () { $('#JobTransition_VariableDeduction_' + index + '__Applicable').prop('checked', false) })
            }
        }
    }
    if ($('#JobTransition_JobDetail_NewESIGroup_Code').val() == '') {
        if ($('#JobTransition_VariableDeduction_' + index + '__DeductionType_Code').val() == 4) {
           
            if ($('#JobTransition_VariableDeduction_' + index + '__Applicable').is(':checked') == true) {
                msg = 'ESI is ticked but ESI group is not selected';
                swal.fire(msg).then(function () { $('#JobTransition_VariableDeduction_' + index + '__Applicable').prop('checked', false) })
            }
        }
    }
    if ($('#JobTransition_JobDetail_NewPFGroup_Code').val() == '') {
        if ($('#JobTransition_VariableDeduction_' + index + '__DeductionType_Code').val() == 3) {
            if ($('#JobTransition_VariableDeduction_' + index + '__Applicable').is(':checked') == true) {
                msg = 'PF is ticked but PF group is not selected';
                swal.fire(msg).then(function () { $('#JobTransition_VariableDeduction_' + index + '__Applicable').prop('checked', false) })
            }
        }
    }
}

function onJobTranstionSubmit()
{
    debugger
    let i = 0;
    var histGI = $('#HistoryGI').val();
   
    //if (parseFloat($("#JobTransition_JobDetail_CurrentGrossSalary").val()) != parseFloat($("#JobTransition_JobDetail_NewGrossSalary").val())) {

  
    //    //console.log(parseFloat($("#PreEnrollment_Compensation_OfferedSalary").val()));
    //    //console.log(parseFloat($("#PreEnrollment_Compensation_CTC").val()));
    //    i++;
    //    //$('#OfferedSalary').focus();
    //    swal.fire("Sum of Salary Split Up and New Gross Salary amount Not Matching").then(function () {
    //        return false;
    //    });
    //    return false;
    //}

    var TotAmt = parseFloat("0");
    $('.AllowanceAmount').each(function (i, obj) {
        var Amt = parseFloat($("#JobTransition_FixedAllowance_" + i + "__NewAmount").val());
        var CurrAmt = parseFloat($("#JobTransition_FixedAllowance_" + i + "__CurrentAmount").val());
        debugger;
        if (isNaN(Amt)) {
            Amt = parseFloat("0");
        }
        TotAmt = TotAmt + Amt;

    });

    if (parseFloat(TotAmt) != parseFloat($("#JobTransition_JobDetail_NewGrossSalary").val())) {

        i++;
        swal.fire("Sum of Salary Split Up and New Gross Salary amount Not Matching").then(function () {
            return false;
        });
        return false;
    }

    debugger
    flag = 0; 
    let msg = '';
    debugger;
    //var esivalue = $('#JobTransition_JobDetail_ESI').is(':checked');
    var esivalue=false;

        for (let index = 0; index < $('#JobTransition_VariableDeduction_Count').val(); index++) {
            if ($('#JobTransition_VariableDeduction_' + index + '__DeductionType_Code').val() == 4) {
                debugger;
                 esivalue = $('#JobTransition_VariableDeduction_' + index + '__Applicable').is(':checked');

            }
        }
    
    debugger;
    
    if (!validateById("#JobTransition_JobDetail_Remarks", "JTRemarks"))
        return false;
    if (!validateById('#JobTransition_JobDetail_NewSalaryStructure_Code', 'salaryStruct')) {
        i++;
    }
    else {
        debugger
        if ($("#JobTransition_JobDetail_NewCTC").val() > 21000 && esivalue == true) {
            warningmsg = "Salary greater than 21000.Employee may not be applicable for ESI. Do You want to proceed?"
            flag = 1;
        }
        if ($("#JobTransition_JobDetail_NewCTC").val() < 21000 && esivalue == false) {
            debugger;
            warningmsg = "Salary less than 21000.Employee may be applicable for ESI. Do You want to proceed?"
            flag = 1;
        }
        if ($("#JobTransition_JobDetail_NewCTC").val() <= 0 || $("#JobTransition_JobDetail_NewCTC").val() == null)
        {
            warningmsg = "salary amount is zero. Do you want to continue?"
            flag = 1;
        }
    
       
        
        debugger;
        
        if ($('#JobTransition_JobDetail_NewPTGroup_Code').val() == '') {
            for (let index = 0; index < $('#JobTransition_VariableDeduction_Count').val(); index++) {
                if ($('#JobTransition_VariableDeduction_' + index + '__DeductionType_Code').val() == 6) {
                    if ($('#JobTransition_VariableDeduction_' + index + '__Applicable').is(':checked') == true) {
                        msg = 'PT is ticked but PT group is not selected';
                        i++;
                    }
                }
            }
        }
        debugger;
     
        if ($('#JobTransition_JobDetail_NewESIGroup_Code').val() == '') {
            for (let index = 0; index < $('#JobTransition_VariableDeduction_Count').val(); index++) {
                if ($('#JobTransition_VariableDeduction_' + index + '__DeductionType_Code').val() == 4) {
                    if ($('#JobTransition_VariableDeduction_' + index + '__Applicable').is(':checked') == true) {
                        msg = 'ESI is ticked but ESI group is not selected';
                        i++;
                    }
                }
            }
        }
       
        if ($('#JobTransition_JobDetail_NewPFGroup_Code').val() == '') {
            for (let index = 0; index < $('#JobTransition_VariableDeduction_Count').val(); index++) {
                if ($('#JobTransition_VariableDeduction_' + index + '__DeductionType_Code').val() == 3) {
                    if ($('#JobTransition_VariableDeduction_' + index + '__Applicable').is(':checked') == true) {
                        msg = 'PF is ticked but PF group is not selected';
                        i++;
                    }
                }
            }
        }
      
        
        if (i == 0) {

            if (flag == 1) {
                
                debugger
                swal.fire({
                    text: warningmsg,
                    icon: 'warning',
                    showCancelButton: true,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33',
                    confirmButtonText: 'Ok'
                }).then((result) => {

                    if (result.isConfirmed) {
                        debugger;
                        
                        $('#OfferedSalary').prop('disabled', false)
                        $('#JobTransition_JobDetail_NewSalaryStructure_Code').prop('disabled', false)
                        $('.getapplicable').prop('disabled', false)
                        /*$('#JobTransition_JobDetail_PRApplicability_Code').attr('disabled', false);*/
                        //var serializedata = $('#job-tran-Save-form').serialize();
                        data = new FormData($('#job-tran-Save-form')[0]);
                        $.ajax({

                            type: 'POST',
                            url: '/DataBank/JobTransition/Save',
                            contentType: false,
                            processData: false,
                            dataType: 'json',
                            data: data,
                            success: function (result) {
                                if (result.success == true) {
                                    swal.fire("Saved Successfully").then(function () {
                                        debugger;
                                        if (histGI == undefined || histGI == '')
                                            var url = '/DataBank/JobTransition/Index';
                                        else
                                            var url = '/DataBank/JobTransition/EditList';
                                        window.location.href = url;
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
                    else {
                        return false;
                    }
                })
            }
            else {
               
                $('#OfferedSalary').prop('disabled', false)
                $('#JobTransition_JobDetail_NewSalaryStructure_Code').prop('disabled', false)
                $('.getapplicable').prop('disabled', false)
                /*$('#JobTransition_JobDetail_PRApplicability_Code').attr('disabled', false);*/
                //var serializedata = $('#job-tran-Save-form').serialize();
                data = new FormData($('#job-tran-Save-form')[0]);
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
                            url: '/DataBank/JobTransition/Save',
                            contentType: false,
                            processData: false,
                            dataType: 'json',
                            data: data,
                            success: function (result) {
                                if (result.success == true) {
                                    swal.fire("Saved Successfully").then(function () {
                                        debugger;
                                        if (histGI == undefined || histGI == '')
                                            var url = '/DataBank/JobTransition/Index';
                                        else
                                            var url = '/DataBank/JobTransition/EditList';
                                        window.location.href = url;
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
                    else {
                        return false;
                    }
                })



            }

        }
        else {
            swal.fire(msg);
        }

    }
}
function ontabClick(Tab, TabIndex,tabName) {
    debugger;
    showSpinner();
    currentTab = TabIndex;
    currTabName = tabName;
    $('#TabCode').val(TabIndex);
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");
    //var searchID = $("#Filter_Code").val();;
    //var url = "/DataBank/JobTransition/GetJobTransitionList";
    //$('#jobTransition-Approval-PendingList').load(url, { TabIndex: TabIndex, SearchId: searchID }, function () { });
    ApplyFilter('apply');

    //if (TabIndex != 1001) {
    //    $('#jobTransition-Confirm-Approval-Pending-List').hide();
    //    $('#jobTransition-Approval-PendingList').load(url, { TabIndex: TabIndex, SearchId: searchID }, function () { });
    //}
    //else {
    //    $('#jobTransition-Approval-PendingList').hide();
    //    $('#jobTransition-Confirm-Approval-Pending-List').load(url, { TabIndex: TabIndex, SearchId: searchID }, function () { });
    //}
    prevTab = Tab;
}

function TabsDropdown(TabIndex) {
    debugger;
    currentTab = TabIndex;
    var Tab = GetTab($("#TabCode").prop('selectedIndex'));
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");
    
    ApplyFilter('apply');
    
    prevTab = Tab;
}

function GetTab(TabCode) {
    if (TabCode == 0)
        return 'all'
    else if (TabCode == 1)
        return 'tab1';
    else if (TabCode == 2)
        return 'tab2';
    else if (TabCode == 3)
        return 'tab3';
    else if (TabCode == 4)
        return 'tab4';
    else
        return 'more';
}

function onFilterDropdownChange(sender) {
    debugger;

    //var url = "/DataBank/JobTransition/GetJobTransitionList";
    showSpinner();
    var search = $(sender).val();
    $('#Filter_Code').val(search);
    $('.filterDropDown').val(search);
    ApplyFilter('reset');
   // $('#jobTransition-Approval-PendingList').load(url, { TabIndex: currentTab, SearchId: search }, function () { });
    //if (currentTab!=1001)
    //    $('#jobTransition-Approval-PendingList').load(url, { TabIndex: currentTab, SearchId: search }, function () { });
    //else
    //    $('#jobTransition-Confirm-Approval-Pending-List').load(url, { TabIndex: currentTab, SearchId: search }, function () { });
}

function OnEmployeeChange(sender) {
    debugger;
    var EmpCode = $(sender).val();
    if (EmpCode != 0) {

        $.ajax({

            type: 'GET',
            url: '/DataBank/JobTransition/EmployeeCard',
            dataType: 'html',
            data: { EmpCode: EmpCode },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('.search-profile-details').html("");
                    $('.search-profile-details').html(result);
                }


            },
            error: function (xhr, textStatus, errorThrown) {
                debugger
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);


            }
        });
        $('.search-profile-details').show(300);

    }
}

function onNewRequestClick() {
    debugger;
    $('#newRequestSlider').removeClass("hide");
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
       
    });

    $("#NewGrossSal").html(TotGrossAmt.toFixed(2));
    $("#CurrGrossSal").html(CurrTotGrossAmt.toFixed(2));
    $("#JobTransition_JobDetail_CurrentGrossSalary").val(CurrTotGrossAmt.toFixed(2));
    $("#JobTransition_JobDetail_NewGrossSalary").val(TotGrossAmt.toFixed(2));


    $('.ProvisionsAmount').each(function (i, obj) {
        var Amt = parseFloat($("#JobTransition_FixedProvision_" + i + "__NewAmount").val());
        var CurrAmt = parseFloat($("#JobTransition_FixedProvision_" + i + "__CurrentAmount").val());

        debugger;
        if (isNaN(Amt) || Amt == 'undefined') {
            Amt = parseFloat("0");
            CurrAmt = parseFloat("0");
        }

        var Provosion = parseFloat($("#JobTransition_FixedProvision_" + i + "__AllowanceApplicableFor").val());
        if (Provosion != 3) {
            TotCTCAmt = TotCTCAmt + Amt;
            CurrTotCTCAmt = CurrTotCTCAmt + CurrAmt;
        }
        
    });
    $("#NewCTC").html((TotCTCAmt + TotGrossAmt ).toFixed(2));
    $("#CurrCTC").html((CurrTotCTCAmt + CurrTotGrossAmt).toFixed(2));
    //$("#JobTransition_JobDetail_CurrentCTC").val((CurrTotCTCAmt + CurrTotGrossAmt).toFixed(2));
    //$("#JobTransition_JobDetail_NewCTC").val((TotCTCAmt + TotGrossAmt ).toFixed(2));

    //$("#NewCTC").html((TotCTCAmt + TotGrossAmt).toFixed(2));
    //$("#CurrCTC").html((CurrTotCTCAmt + CurrTotGrossAmt).toFixed(2));
    //$("#JobTransition_JobDetail_CurrentCTC").val((CurrTotCTCAmt + CurrTotGrossAmt).toFixed(2));
    //$("#JobTransition_JobDetail_NewCTC").val((TotCTCAmt ).toFixed(2));
    //$("#JobTransition_JobDetail_NewCTC").val((TotCTCAmt + TotGrossAmt).toFixed(2));


    $('.DeductionsAmount').each(function (i, obj) {//Total Dedcutions
        var Amt = parseFloat($("#JobTransition_FixedDeduction_" + i + "__NewAmount").val());
        var CurrAmt = parseFloat($("#JobTransition_FixedDeduction_" + i + "__CurrentAmount").val());
        debugger;
        if (isNaN(Amt)) {
            Amt = parseFloat("0");
        }
        TotDedAmt = parseFloat(TotDedAmt + Amt).toFixed(2);
        CurrTotDedAmt = parseFloat(CurrTotDedAmt + CurrAmt).toFixed(2);

       

    });

    TakeHome = TotGrossAmt - TotDedAmt;//Take Home
    CurrTakeHome = CurrTotGrossAmt - CurrTotDedAmt;//Take Home

    $("#CurrTakeHome").html(CurrTakeHome.toFixed(2));
    $("#NewTakeHome").html(TakeHome.toFixed(2));
   
}

function onProgressTabClick(TabIndex) {
    debugger;

    // List all tab section IDs
    const tabs = [
        'transition-type',
        'growth-history',
        'job-details',
        'compensation-benefits',
        'add-authority',
        'subordinates',
        'summary'
    ];

    tabs.forEach(tab => $('#' + tab).addClass('hide'));

    $('#' + TabIndex).removeClass('hide');
    if (TabIndex == 'growth-history' || TabIndex == 'transition-type') {
        $('#reason').addClass('hide');
    }
    else
    {
        $('#reason').removeClass('hide');
    }

    switch (TabIndex) {
        case 'transition-type':
            $('#JobTransition_JobDetail_JobTransitionsType_Code').focus();
            break;
        case 'job-details':
            $('#JobTransition_JobDetail_NewLocation_Code').focus();
            break;
        //case 'subordinates':
        //    SubordinatesNew();
        //    break;
            
    }
}

function hideAllTabs() {
    $('.job-transition-tabs').addClass('hide');
}

function onNextClick(TabIndex) {
    debugger;
    //$.ajaxSetup({ async: false });
    // All tab IDs
    const tabs = [
        'growth-history',
        'transition-type',
        'job-details',
        'compensation-benefits',
        'add-authority',
        'subordinates',
        'summary'
    ];

    // Helper: Hide all tabs
    //const hideAllTabs = () => tabs.forEach(tab => $('#' + tab).addClass('hide'));

    // === CASE 1: Growth History ===
    if (TabIndex === 'growth-history') {
        $('#reason').addClass('hide');
        hideAllTabs();
        $('#transition-type').removeClass('hide');
        $('#JobTransition_JobDetail_JobTransitionsType_Code').focus();
        return;
    }

    // === CASE 2: Transition Type ===
    if (TabIndex === 'transition-type') {
        let i = 0;
        $('#reason').removeClass('hide');
        if (!validateById('#JobTransition_JobDetail_JobTransitionsType_Code', 'JTJobTransitionsType')) i++;
        if (!validateById('#JobTransition_JobDetail_JobTransitionReason_Code', 'JTJobTransitionReason')) i++;
        if (!validateById('#JobTransition_JobDetail_EffectiveDate', 'JTEffectiveDate')) i++;
        if (!validateById('#JobTransition_JobDetail_PRApplicability_Code', 'prAppl')) i++;

        const PR = $("#JobTransition_JobDetail_PRApplicability_Code option:selected").text();

        // PR validation
        if (PR === "YES" && $("#JobTransition_JobDetail_EffectiveDate").val() !== "") {
            $.ajax({
                type: 'GET',
                url: '/DataBank/JobTransition/PR_Appl_Validation',
                dataType: 'json',
                async: false,
                data: {
                    EffDate: $("#JobTransition_JobDetail_EffectiveDate").val(),
                    EmpCode: $("#EmployeeHeader_EmployeeCode").val()
                },
                success: function (result) {
                    debugger;
                    if (!result.success) {
                        Swal.fire(result.message);
                        i++;
                    }
                },
                error: function (xhr) {
                    if ($($.parseHTML(xhr.responseText)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    } else {
                        document.body.id = "kt_body";
                        $('body').html(xhr.responseText);
                    }
                }
            });
        }

        // Move to next tab if valid
        if (i === 0) {
            hideAllTabs();
            $('#job-details').removeClass('hide');
            $('#JobTransition_JobDetail_NewLocation_Code').focus();
            onCategoryChange(document.getElementById('JobTransition_JobDetail_NewCategory_Code'));
        }
        return;
    }

    // === CASE 3: Job Details ===
    if (TabIndex === 'job-details') {
        var NewLocation = $('#JobTransition_JobDetail_NewLocation_Code').val();
        var CurrentLocation = $('#JobTransition_JobDetail_CurrentLocation_Code').val();
        var NewFunDesignation = $('#JobTransition_JobDetail_NewDesignation_Code').val();
        var CurrentFunDesignation = $('#JobTransition_JobDetail_CurrentDesignation_Code').val();
        var NewDepartment = $('#JobTransition_JobDetail_NewDepartment_Code').val();
        var CurrentDepartment = $('#JobTransition_JobDetail_CurrentDepartment_Code').val();
        var NewPosition = $('#JobTransition_JobDetail_NewPosition_Code').val();
        let i = 0;
        if ((NewLocation != CurrentLocation) || (NewFunDesignation != CurrentFunDesignation) || (NewDepartment != CurrentDepartment)) {
            if (NewPosition == null || NewPosition == "") {
                swal.fire("A position must be selected if any of the Location, Fun.Designation, or Department details are modified.");
                return;
            }
        }
        $('#reason').removeClass('hide');
        if (NewPosition != null && NewPosition != "") {
            if ((NewLocation != CurrentLocation) || (NewFunDesignation != CurrentFunDesignation) || (NewDepartment != CurrentDepartment)) {
                // Field validations
                if (!validateById('#JobTransition_JobDetail_NewLocation_Code', 'JTLocation')) i++;
                if (!validateById('#JobTransition_JobDetail_NewDepartment_Code', 'JTDepartment')) i++;
                if (!validateById('#JobTransition_JobDetail_NewDesignation_Code', 'JTDesignation')) i++;
                if (!validateById('#JobTransition_JobDetail_NewCategory_Code', 'JTCategory')) i++;
                if (!validateById('#JobTransition_JobDetail_NewGrade_Code', 'JTGrade')) i++;
                if (!validateById('#JobTransition_JobDetail_NewGroupCompany_Code', 'GrpC')) i++;
                if (!validateById('#JobTransition_JobDetail_NewPayrollGroup_Code', 'JTPayroll')) i++;
                if (!validateById('#JobTransition_JobDetail_NewSalaryStructure_Code', 'salaryStruct')) i++;
                if ($("#JobTransition_JobDetail_NewCategory_Code option:selected").text() !== 'Confirmed') {
                    if (!validateById('#JobTransition_JobDetail_NewRenewalDate', 'JTRenewal')) i++;
                }
                if (!validateById('#JobTransition_JobDetail_NewWageGrade_Code', 'WGrade')) i++;
                if (!validateById('#JobTransition_JobDetail_NewNoticePeriod', 'JTNotice')) i++;

                if (i === 0) {
                    const PR = $("#JobTransition_JobDetail_PRApplicability_Code option:selected").text();

                    const readonly = (PR === "NO");
                    $(".DeductionsAmount, .AllowanceAmount, .ProvisionsAmount").prop("readonly", readonly);
                    $('#OfferedSalary, #JobTransition_JobDetail_NewSalaryStructure_Code, .getapplicable').prop("disabled", readonly);

                    hideAllTabs();
                    /*$('#add-authority').removeClass('hide');*/
                    $('#compensation-benefits').removeClass('hide');
                    moveCursorToEnd('JobTransition_Allowance_0__NewAmount');
                }
                return;
            }
            else {
                swal.fire("New position selection is not allowed unless there is a change in Location, Fun.Designation, or Department.");
                return;
            }
        }
        else {
            if (!validateById('#JobTransition_JobDetail_NewLocation_Code', 'JTLocation')) i++;
            if (!validateById('#JobTransition_JobDetail_NewDepartment_Code', 'JTDepartment')) i++;
            if (!validateById('#JobTransition_JobDetail_NewDesignation_Code', 'JTDesignation')) i++;
            if (!validateById('#JobTransition_JobDetail_NewCategory_Code', 'JTCategory')) i++;
            if (!validateById('#JobTransition_JobDetail_NewGrade_Code', 'JTGrade')) i++;
            if (!validateById('#JobTransition_JobDetail_NewGroupCompany_Code', 'GrpC')) i++;
            if (!validateById('#JobTransition_JobDetail_NewPayrollGroup_Code', 'JTPayroll')) i++;
            if (!validateById('#JobTransition_JobDetail_NewSalaryStructure_Code', 'salaryStruct')) i++;
            if ($("#JobTransition_JobDetail_NewCategory_Code option:selected").text() !== 'Confirmed') {
                if (!validateById('#JobTransition_JobDetail_NewRenewalDate', 'JTRenewal')) i++;
            }
            if (!validateById('#JobTransition_JobDetail_NewWageGrade_Code', 'WGrade')) i++;
            if (!validateById('#JobTransition_JobDetail_NewNoticePeriod', 'JTNotice')) i++;

            if (i === 0) {
                const PR = $("#JobTransition_JobDetail_PRApplicability_Code option:selected").text();

                const readonly = (PR === "NO");
                $(".DeductionsAmount, .AllowanceAmount, .ProvisionsAmount").prop("readonly", readonly);
                $('#OfferedSalary, #JobTransition_JobDetail_NewSalaryStructure_Code, .getapplicable').prop("disabled", readonly);

                hideAllTabs();
                /*$('#add-authority').removeClass('hide');*/
                $('#compensation-benefits').removeClass('hide');
                moveCursorToEnd('JobTransition_Allowance_0__NewAmount');
            }
            return;
        }
    }

    // === CASE 4: Add Compensation BenefitsAuthority ===
    if (TabIndex === 'compensation-benefits') {
        $('#reason').removeClass('hide');
        hideAllTabs();
        /*SubordinatesNew();*/
        $('#add-authority').removeClass('hide');
        //$('#subordinates').removeClass('hide');
        return;
    }
    
    // === CASE 5: Authority → Subordinates ===
    if (TabIndex === 'add-authority') {
        let valid = 0;
        $('#reason').removeClass('hide');
        if ($('#EmployeeHeader_EmployeeCode').val() != 4409) {

       
        $('#authorityTable tbody tr').each(function (i) {
            const id = '#JobTransition_Authority_' + i + '__AuthorityCode_New';
            if ($(id).val() === '' || $(id).val() === '0')
            {
                $(id).val('');
                validateByIdForSearchDropdn(id, 'authCode_' + i, '.authCodeDropdn_' + i);
                valid++;
            }
        });
        }
        if (valid === 0) {
            hideAllTabs();
             SubordinatesNew();
            $('#subordinates').removeClass('hide')
            /*$('#compensation-benefits').removeClass('hide');*/
        }
        return;
    }

    // === CASE 6: Subordinates 
    if (TabIndex === 'subordinates') {
        $('#reason').removeClass('hide');
        hideAllTabs();
       
        $('#summary').removeClass('hide');
        jobTransitionSummary();
        return;
    }
      // === CASE 7: summary
    if (TabIndex === 'summary') {
        $('#reason').removeClass('hide');
        hideAllTabs();
        $('#summary').removeClass('hide');
        return;
    }
}

function onJObTrnTypechange(sender) {
    var code = $(sender).val();
    if (!validateById("#JobTransition_JobDetail_JobTransitionsType_Code","JTJobTransitionsType"))
        return false;
    else {
    $.getJSON("/DataBank/JobTransition/GetTransitionReasonDrpDwn", { TypeCode: code }, function (data) {
        $('#JobTransition_JobDetail_JobTransitionReason_Code option').remove();
        $('#JobTransition_JobDetail_JobTransitionReason_Code').append('<option value="">Select Reason</option');
        debugger;
        for (var i = 0; i < data.length; i++) {
            $('#JobTransition_JobDetail_JobTransitionReason_Code').append('<option value="' + data[i].code + '">' + data[i].name + '</option');
        }


    });
        //if (code == 11) {
        //    $('#JobTransition_JobDetail_PRApplicability_Code').val(0);
        //    $('#JobTransition_JobDetail_PRApplicability_Code').attr('disabled', true);
        //    validate($('#JobTransition_JobDetail_PRApplicability_Code'), 'prAppl');
        //}
        //else {
        //    $('#JobTransition_JobDetail_PRApplicability_Code').val('');
        //    $('#JobTransition_JobDetail_PRApplicability_Code').attr('disabled', false);
        //}
    }
}

function onCategoryChange(val) {
    debugger;
    if (val!=1)
        validate(val, 'JTCategory');
    EmployeeGI = $("#EmployeeGI").val();
    var categoryCode = val.value;
   var EffectiveDate =$('#JobTransition_JobDetail_EffectiveDate').val();
  
    $.getJSON("/DataBank/JobTransition/GetRenewalDate", { EmployeeGI: EmployeeGI, CategoryCode: categoryCode, EffectiveDate: EffectiveDate}, function (data) {
        $('#JobTransition_JobDetail_NewRenewalDate').val(data);
        $('#JobTransition_JobDetail_NewRenewalDate').datepicker('setDate', data);
        var MName = $("#JobTransition_JobDetail_NewCategory_Code option:selected").text();
        if (MName == "Confirmed") {

            $(".newreneweltext").addClass('hide');
        }
        else {
      
            $(".newreneweltext").removeClass('hide');
        }
    });
    
}

function Approve()
{
     debugger
    if (!validateById("#JobTransition_Approve_ApproverComments", "JobTransition_Approve_remarks"))
        return false;

    else
    {
        swal.fire({
            text: "Do you want to Approve?",
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
                    url: '/DataBank/JobTransition/Approve',
                    dataType: 'json',
                    data: $('#jobTransitionApprove').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire("Approved Successfully").then(function () {
                                var url = '/DataBank/JobTransition/Index';
                                window.location.href = url;
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
}
function Reject(employeeGI, HistoryGI, REJECTMOde) {

    if (REJECTMOde=="REJECT")
    {
        if (!validateById("#JobTransition_Approve_ApproverComments", "JobTransition_Approve_remarks"))
            return false;

        else {
            swal.fire({
                text: "Do you want to Reject?",
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
                        url: '/DataBank/JobTransition/Reject',
                        dataType: 'json',
                        data: $('#jobTransitionApprove').serialize(),
                        success: function (result) {
                            if (result.success == true) {

                                swal.fire("Rejected Successfully").then(function () {
                                    var url = '/DataBank/JobTransition/Index';
                                    window.location.href = url;
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
    }
    else 
    {
        swal.fire({
            text: "Do you want to Confirm?",
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
                    url: '/DataBank/JobTransition/ConfirmReject',
                    dataType: 'json',
                    data: $('#jobTransitionApprove').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                var url = '/DataBank/JobTransition/Index';
                                window.location.href = url;
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
}

function onExportToExcelClick() {
    debugger;
    var searchid = $('#Filter_Code').val();
   
    var TableLength = $('#JobTransitionList_Count').val();
    if (TableLength > 0) {
        url = "/DataBank/JobTransition/ExportToExcel?TabIndex=" + currentTab + "&SearchId=" + searchid + "&TabName=" + currTabName;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}

    function ApplyFilter(mode) {
        debugger
  
        $("#Tabindex").val(currentTab);
        $("#SearchId").val($('#Filter_Code').val())
        if (mode == 'apply') {
            if (filterApplicable >= 1) {
                $.ajax({

                    type: 'POST',
                    url: '/DataBank/JobTransition/ApplyFilter',
                    dataType: 'html',
                    data: $('#BasicFilter').serialize(),
                    success: function (result) {
                        debugger
                        CloseSlider('basicFilter');
                        if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                            window.location.reload();
                        }
                        else {
                            $('#jobTransition-Approval-PendingList').html("");
                            $('#jobTransition-Approval-PendingList').html(result);
                            $('#jobTransition-Approval-PendingList').removeClass('hide');
                        }
                        hideSpinner();                                     
                    },


                    error: function (xhr, textStatus, errorThrown) {
                        swal.fire(xhr.responseText);
                    }
                });
                $('#filterForm').removeClass('filter-apply');
            }
            else {
                showSpinner();
                $.ajax({

                    type: 'POST',
                    url: '/DataBank/JobTransition/ApplyFilter',
                    dataType: 'html',
                    data: $('#BasicFilter').serialize(),
                    success: function (result) {
                        debugger
                        CloseSlider('basicFilter');
                        if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                            window.location.reload();
                        }
                        else {
                            $('#jobTransition-Approval-PendingList').html("");
                            $('#jobTransition-Approval-PendingList').html(result);
                            $('#jobTransition-Approval-PendingList').removeClass('hide');
                        }
                        hideSpinner();
                                     
                    },


                    error: function (xhr, textStatus, errorThrown) {
                        swal.fire(xhr.responseText);
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
                url: '/DataBank/JobTransition/ApplyFilter',
                dataType: 'html',
                data: $('#BasicFilter').serialize(),
                success: function (result) {
                    debugger
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }
                    else {
                        $('#jobTransition-Approval-PendingList').html("");
                        $('#jobTransition-Approval-PendingList').html(result);
                        $('#jobTransition-Approval-PendingList').removeClass('hide');
                    }
                  
                    hideSpinner();

                },


                error: function (xhr, textStatus, errorThrown) {
                    swal.fire(xhr.responseText);
                }
            });
        }
    }


function onSalaryStructureChange() {
    debugger;
    var code = $("#JobTransition_JobDetail_NewSalaryStructure_Code").val();
    var currentcode = $("#JobTransition_JobDetail_CurrentSalaryStructure_Code").val();
    var VDAGroup = $('#JobTransition_JobDetail_NewVDAGroup_Code').val();

    var GrossSalary = $('#JobTransition_JobDetail_GrossNewValue').val();
    var NewBasic = $('#JobTransition_JobDetail_BasicNewValue').val();
    var ABNewValue = $('#JobTransition_JobDetail_ABNewValue').val();
    var AllowanceCode = $('#JobTransition_JobDetail_Allowance_Code').val();
    var AllowanceAmt = $('#JobTransition_JobDetail_AlwAmtNew').val();
    var EmpCode = $('#EmployeeHeader_EmployeeCode').val();
    var NewDepartment = $('#JobTransition_JobDetail_NewDepartment_Code').val();
    var NewLocation = $('#JobTransition_JobDetail_NewLocation_Code').val();


    let CTC = $('#JobTransition_JobDetail_oldCTCValue').val();
    let CTCNew = $('#JobTransition_JobDetail_NewCTCValue').val();
    //let Gross = $('#JobTransition_JobDetail_GrossOldAmt').val();
    //let GrossNew = $('#JobTransition_JobDetail_GrossNewValue').val();
    let OBasic = $('#JobTransition_JobDetail_BasicOldAmt').val();
    let NBasic = $('#JobTransition_JobDetail_BasicNewValue').val();
    let Oabns = $('#JobTransition_JobDetail_ABOldAmt').val();
    let Nabns = $('#JobTransition_JobDetail_ABNewValue').val();
    let AlwAmtOld = $('#JobTransition_JobDetail_AlwAmtOld').val();

    let ChgGross = $('#JobTransition_JobDetail_GrossChangeValue').val();
    let chgBasic = $('#JobTransition_JobDetail_BasicChangeValue').val();
    let chgAB = $('#JobTransition_JobDetail_ABChangeValue').val();
    let calcVal = $('#JobTransition_JobDetail_GrossCalculator_Code').val();
    if (validateById("#JobTransition_JobDetail_NewSalaryStructure_Code", "salaryStruct") && validateById("#OfferedSalary", 'salary')) {

        $.ajax({

            type: 'GET',
            url: '/DataBank/JobTransition/GetSalaryStructureAlwDedGridGeojit',
            dataType: 'html',
            data: {
                EmpCode: EmpCode, SalaryStructureCode: code, CTC: CTCNew /*GrossSalary: GrossSalary*/, currentsalstr: currentcode, VDAGroup: VDAGroup,
                NewBasic: NewBasic, ABNewValue: ABNewValue, AllowanceCode: AllowanceCode, AllowanceAmt: AllowanceAmt, NewDepartment: NewDepartment, NewLocation: NewLocation,
                CTCOld: CTC, OBasic: OBasic, NBasic: NBasic, Oabns: Oabns, Nabns: Nabns, AlwAmtOld: AlwAmtOld, ChgGross: ChgGross, chgBasic: chgBasic, chgAB: chgAB, CalcVal: calcVal},
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('#compensation-benefits').html("");
                    $('#compensation-benefits').html(result);
                   
                }
                CTCCalculation();
              

            },
            error: function (xhr, textStatus, errorThrown) {
                debugger
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);


            }
        });
    }
    else {
        $('.compensationJobTransitionTables tbody tr').remove();
        $("#NewGrossSal").html('');                
        $("#JobTransition_JobDetail_NewGrossSalary").val(0);
        $("#NewCTC").html('');                
        $("#JobTransition_JobDetail_NewCTC").val(0);        
        $("#NewTakeHome").html('');

    }
    //window.location.href = url + "?SalaryStructureCode=" + code + "&GrossSalary" + GrossSalary;
}

function process(input) {
    debugger;
    let value = input.value;
    let numbers = value.replace(/[^0-9]/g, "");
    input.value = numbers;
}

function validateSalary() {
    debugger;
    if ($('#OfferedSalary').val() == "" && $('#OfferedSalary').is('[readonly]') == false) {
        $('#OfferedSalary').addClass('is-invalid');
        $("#salary").addClass('error-message');
        $("#salary").html("This field is required");
        return false;
    }
    else {
        $('#OfferedSalary').removeClass('is-invalid');
        $("#salary").removeClass('error-message');
        $("#salary").html("");
        //trim(val);        
        return true;
    }
}

function ValidateOrderDate() {
    var currentDate = new Date();
    var orderDate = new Date($('#JobTransition_JobDetail_OrderDate').val());
    if (!validateById('#JobTransition_JobDetail_OrderDate', 'JTOrderDate')) {
        return false;
    }
    else if (orderDate > currentDate) {
        $('#JobTransition_JobDetail_OrderDate').addClass('is-invalid');
        $("#JTOrderDate").addClass('error-message');
        $("#JTOrderDate").html("Order date cannot be greater than Current Date");
        return false;
    }
    else {
        $('#JobTransition_JobDetail_OrderDate').removeClass('is-invalid');
        $("#JTOrderDate").removeClass('error-message');
        $("#JTOrderDate").html("");
        //trim(val);        
        return true;
    }
}

function fillNoticeperiod(sender) {
    debugger
    $.getJSON("/DataBank/JobTransition/GetNoticeperiod", { DesignationCode: $(sender).val(), EmployeeGI: $('#EmployeeGI').val() }, function (data) {

        $("#JobTransition_JobDetail_NewNoticePeriod").val(data);

    });
}


function Preview() {
    debugger
    if ($('#Confidential_FileData').val() == '' && $('#Confidential_AbsoluteUri').val() == '') {
        swal.fire("No file uploaded");
    }

    else if ($('#Confidential_FileData').val() != '') {
        var path = URL.createObjectURL($('.PreviewImage')[0].files[0]);
        window.open(path, "_blank");
    }
    else {
        window.open($('#Confidential_AbsoluteUri').val(), '_blank');
    }
}

function onNewAmountChange() {
    debugger;

    let ChangedValue = 0;
    let CurrAmt = 0;
    let NewAmt = 0;

    // GROSS Calculation
    if ($('#JobTransition_JobDetail_GrossCalculator_Code').val() !== '') {
        CurrAmt = parseFloat($('#JobTransition_JobDetail_oldCTCValue').val()) || 0;
        //CurrAmt = parseFloat($('#JobTransition_JobDetail_GrossOldAmt').val()) || 0;
        ChangedValue = parseFloat($('#JobTransition_JobDetail_GrossChangeValue').val()) || 0;
        const calcCode = $('#JobTransition_JobDetail_GrossCalculator_Code').val();

        switch (calcCode) {
            case "0": NewAmt = CurrAmt + (CurrAmt * (ChangedValue / 100)); break;
            case "1": NewAmt = CurrAmt + ChangedValue; break;
            case "2": NewAmt = CurrAmt - (CurrAmt * (ChangedValue / 100)); break;
            case "3": NewAmt = CurrAmt - ChangedValue; break;
            default: NewAmt = CurrAmt;
        }

        $("#JobTransition_JobDetail_NewCTCValue").val(NewAmt.toFixed(2));
    //    $("#JobTransition_JobDetail_GrossNewValue").val(NewAmt.toFixed(2));
    }

    //// BASIC Calculation
    //if ($('#JobTransition_JobDetail_BasicCalculator_Code').val() !== '') {
    //    CurrAmt = parseFloat($('#JobTransition_JobDetail_BasicOldAmt').val()) || 0;
    //    ChangedValue = parseFloat($('#JobTransition_JobDetail_BasicChangeValue').val()) || 0;
    //    const calcCode = $('#JobTransition_JobDetail_BasicCalculator_Code').val();

    //    switch (calcCode) {
    //        case "0": NewAmt = CurrAmt + (CurrAmt * (ChangedValue / 100)); break;
    //        case "1": NewAmt = CurrAmt + ChangedValue; break;
    //        default: NewAmt = CurrAmt;
    //    }

    //    $("#JobTransition_JobDetail_BasicNewValue").val(NewAmt.toFixed(2));
    //}

    //// AB Calculation
    //if ($('#JobTransition_JobDetail_ABCalculator_Code').val() !== '') {
    //    CurrAmt = parseFloat($('#JobTransition_JobDetail_ABOldAmt').val()) || 0;
    //    ChangedValue = parseFloat($('#JobTransition_JobDetail_ABChangeValue').val()) || 0;
    //    const calcCode = $('#JobTransition_JobDetail_ABCalculator_Code').val();

    //    switch (calcCode) {
    //        case "0": NewAmt = CurrAmt + (CurrAmt * (ChangedValue / 100)); break;
    //        case "1": NewAmt = CurrAmt + ChangedValue; break;
    //        default: NewAmt = CurrAmt;
    //    }

    //    $("#JobTransition_JobDetail_ABNewValue").val(NewAmt.toFixed(2));
    //}
}
function onAllowanceChange() {
    debugger;

    const knownCode = $("#JobTransition_JobDetail_Allowance_Code").val();

    const rows = document.querySelectorAll('.compensationJobTransitionTables tbody tr');

    let foundRow = null;

    rows.forEach(row => {
        const codeInput = row.querySelector('input[name$=".Code"]');
        if (codeInput && codeInput.value === knownCode) {
            foundRow = row;
        }
    });

    if (foundRow) {
        const currentAmountText = foundRow.querySelector('td[data-label="Amount"]:nth-child(2)')?.innerText.trim() || "0";
       
        const newAmountText = foundRow.querySelector('input[name$=".NewAmount"]')?.value || "0";

      
        const currentAmount = parseFloat(currentAmountText.replace(/,/g, "")) || 0;
        const newAmount = parseFloat(newAmountText.replace(/,/g, "")) || 0;

    
        $("#JobTransition_JobDetail_AlwAmtOld").val(currentAmount.toFixed(2));
        $("#JobTransition_JobDetail_AlwAmtNew").val(newAmount.toFixed(2));

    } else {
      
        $("#JobTransition_JobDetail_AlwAmtOld").val("0.00");
        $("#JobTransition_JobDetail_AlwAmtNew").val("0.00");
    }
}
function NewPosition() {
    debugger;
    //var currLoc = $('#JobTransition_JobDetail_CurrentLocation_Code').val();
    //var newLoc = $('#JobTransition_JobDetail_NewLocation_Code').val();

    //var currDept = $('#JobTransition_JobDetail_CurrentDepartment_Code').val();
    //var newDept = $('#JobTransition_JobDetail_NewDepartment_Code').val();

    //var currDesg = $('#JobTransition_JobDetail_CurrentDesignation_Code').val();
    //var newDesg = $('#JobTransition_JobDetail_NewDesignation_Code').val();

    //// If ALL are same → show message
    //if (currLoc === newLoc && currDept === newDept && currDesg === newDesg) {

    //    Swal.fire({
    //        icon: "warning",
    //        text: "Position change not allowed unless there is a change in Location, Department, or Functional Designation."
    //    });

    //    return false;
    //}
    //else {
    var LocationCode = $('#JobTransition_JobDetail_NewLocation_Code').val();
    var DesignationCode = $('#JobTransition_JobDetail_NewDesignation_Code').val();
    var DepartmentCode = $('#JobTransition_JobDetail_NewDepartment_Code').val();

    const PositionReplacementOrnot = document.querySelector('input[name="JobTransition_JobDetail.PositionReplacementOrnot"]:checked');


    const $positionDropdown = $('#JobTransition_JobDetail_NewPosition_Code');
    $positionDropdown.empty().append('<option value="">--Employee--</option>');

  
    if (PositionReplacementOrnot && PositionReplacementOrnot.value === "True") {

        $.getJSON("/DataBank/JobTransition/GetReplacementEmployee", {
            Designation: DesignationCode,
            Location: LocationCode,
            Department: DepartmentCode,
            Type:'Yes'

        })
            .done(function (data) {
                debugger;

                if (Array.isArray(data) && data.length > 0) {
                    for (var i = 0; i < data.length; i++) {
                        $positionDropdown.append('<option value="' + data[i].value + '">' + data[i].text + '</option>');
                    }
                  
                    //$positionDropdown.val(data[0].value);
                }
            });

    } else {

        $.getJSON("/DataBank/JobTransition/GetReplacementEmployee", {
            Designation: DesignationCode,
            Location: LocationCode,
            Department: DepartmentCode,
            Type: 'No'
        })
            .done(function (data) {
                debugger;

                if (Array.isArray(data) && data.length > 0) {
                    for (var i = 0; i < data.length; i++) {
                        $positionDropdown.append('<option value="' + data[i].value + '">' + data[i].text + '</option>');
                    }

                    //$positionDropdown.val(data[0].value);
                }
            });
        }
    //}
}


function SubordinatesNew() {
    debugger;

    const positionCode = $('#JobTransition_JobDetail_NewPosition_Code').val();
    const employeeCode = $('#EmployeeHeader_EmployeeCode').val();


        $.ajax({
            type: 'GET',
            url: '/DataBank/JobTransition/GetNewSubordinates',
            data: { positionCode, employeeCode },
            dataType: 'html',
            success: function (result) {
                debugger;

                if ($(result).find('.Login_Session').length > 0) {
                    window.location.reload();
                    return;
                }

                $('#subordinates').empty().html(result);
                /*$('#subordinates').removeClass('hide');*/
            },
            error: function (xhr, textStatus, errorThrown) {
                swal.fire(xhr.responseText);
            }
        });
    
}


function onWageGradeChange() {
        debugger;
    const WageGradeCode = $('#JobTransition_JobDetail_NewWageGrade_Code').val()

    $.getJSON("/DataBank/JobTransition/onWageGradeChange", { WageGradeCode: WageGradeCode }, function (data) {

        $('#JobTransition_JobDetail_NewSalaryStructure_Code option').remove();
        //$('#JobTransition_JobDetail_NewSalaryStructure_Code').append('<option value>--Select Grade--</option>');
            for (var i = 0; i < data.length; i++) {
                $('#JobTransition_JobDetail_NewSalaryStructure_Code').append('<option value="' + data[i].value + '">' + data[i].text + '</option>');
            }

        });
    }


function jobTransitionReason() {
    debugger
    var Params = "0";


    if ($('#JobTransition_JobDetail_CurrentGroupCompany_Code').val() != $('#JobTransition_JobDetail_NewGroupCompany_Code').val()) { Params = Params + ",1" }
    if ($('#JobTransition_JobDetail_CurrentLocation_Code').val() != $('#JobTransition_JobDetail_NewLocation_Code').val()) { Params = Params + ",2" }
    if ($('#JobTransition_JobDetail_CurrentDepartment_Code').val() != $('#JobTransition_JobDetail_NewDepartment_Code').val()) { Params = Params + ",3" }
    if ($('#JobTransition_JobDetail_CurrentGrade_Code').val() != $('#JobTransition_JobDetail_NewGrade_Code').val()) { Params = Params + ",4" }
    if ($('#JobTransition_JobDetail_CurrentDesignation_Code').val() != $('#JobTransition_JobDetail_NewDesignation_Code').val()) { Params = Params + ",5" }
    if ($('#JobTransition_JobDetail_CurrentCategory_Code').val() != $('#JobTransition_JobDetail_NewCategory_Code').val()) { Params = Params + ",6" }
    if ($('#JobTransition_JobDetail_CurrentEmploymentType_Code').val() != $('#JobTransition_JobDetail_NewEmploymentType_Code').val()) { Params = Params + ",7" }
    if ($('#JobTransition_JobDetail_CurrentWageGrade_Code').val() != $('#JobTransition_JobDetail_NewWageGrade_Code').val()) { Params = Params + ",8" }
    if ($('#JobTransition_JobDetail_CurrentSalaryStructure_Code').val() != $('#JobTransition_JobDetail_NewSalaryStructure_Code').val()) { Params = Params + ",9" }
    if ($('#JobTransition_JobDetail_CurrentDivision_Code').val() != $('#JobTransition_JobDetail_NewDivision_Code').val()) { Params = Params + ",11" }
    if ($('#JobTransition_JobDetail_CurrentSection_Code').val() != $('#JobTransition_JobDetail_NewSection_Code').val()) { Params = Params + ",12" }
    if ($('#JobTransition_JobDetail_CurrentPayrollGroup_Code').val() != $('#JobTransition_JobDetail_NewPayrollGroup_Code').val()) { Params = Params + ",13" }
    if ($('#JobTransition_JobDetail_CurrentCTC').val() != $('#JobTransition_JobDetail_NewCTC').val()) { Params = Params + ",14" }
    if ($('#JobTransition_JobDetail_BasicOldAmt').val() != $('#JobTransition_JobDetail_BasicNewValue').val()) { Params = Params + ",15" }
    if ($('#JobTransition_JobDetail_CurrentRenewalDate').val() != $('#JobTransition_JobDetail_NewRenewalDate').val()) { Params = Params + ",17" }
    if ($('#JobTransition_JobDetail_CurrentIncentiveCostCenter_Code').val() != $('#JobTransition_JobDetail_NewIncentiveCostCenter_Code').val()) { Params = Params + ",18" }
    if ($('#JobTransition_JobDetail_CurrentIncApplicability_Code').val() != $('#JobTransition_JobDetail_NewIncApplicability_Code').val()) { Params = Params + ",19" }
    if ($('#JobTransition_JobDetail_CurrentSubDepartment_Code').val() != $('#JobTransition_JobDetail_NewSubDepartment_Code').val()) { Params = Params + ",21" }
    if ($('#JobTransition_JobDetail_CurrentFunction_Code').val() != $('#JobTransition_JobDetail_NewFunction_Code').val()) { Params = Params + ",22" }



        const CurrentWageGrade = $('#JobTransition_JobDetail_CurrentWageGrade_Code').val() || '';
        const NewWageGrade = $('#JobTransition_JobDetail_NewWageGrade_Code').val() || '';

        $.ajax({
            type: 'GET',
            url: '/DataBank/JobTransition/GetJobTransitionReason',
            data: { ChangingParameters: Params, CurrentWageGrade, NewWageGrade },
            dataType: 'html',
            success: function (result) {
                if ($(result).find('.Login_Session').length > 0) { window.location.reload(); return; }
                //if (Tabindex != 'GrowthHistory' && Tabindex != 'TransitionType')
                //{
                $('#reason').html(result);
                //}


            },
            error: function (xhr) {
                Swal.fire('Error', xhr.responseText || 'Something went wrong', 'error');
                $('#reason').addClass('hide');
            }
        });
    
}
function jobTransitionSummary() {
    debugger
    var Params = "0";


    if ($('#JobTransition_JobDetail_CurrentGroupCompany_Code').val() != $('#JobTransition_JobDetail_NewGroupCompany_Code').val()) { Params = Params + ",1" }
    if ($('#JobTransition_JobDetail_CurrentLocation_Code').val() != $('#JobTransition_JobDetail_NewLocation_Code').val()) { Params = Params + ",2" }
    if ($('#JobTransition_JobDetail_CurrentDepartment_Code').val() != $('#JobTransition_JobDetail_NewDepartment_Code').val()) { Params = Params + ",3" }
    if ($('#JobTransition_JobDetail_CurrentGrade_Code').val() != $('#JobTransition_JobDetail_NewGrade_Code').val()) { Params = Params + ",4" }
    if ($('#JobTransition_JobDetail_CurrentDesignation_Code').val() != $('#JobTransition_JobDetail_NewDesignation_Code').val()) { Params = Params + ",5" }
    if ($('#JobTransition_JobDetail_CurrentCategory_Code').val() != $('#JobTransition_JobDetail_NewCategory_Code').val()) { Params = Params + ",6" }
    if ($('#JobTransition_JobDetail_CurrentEmploymentType_Code').val() != $('#JobTransition_JobDetail_NewEmploymentType_Code').val()) { Params = Params + ",7" }
    if ($('#JobTransition_JobDetail_CurrentWageGrade_Code').val() != $('#JobTransition_JobDetail_NewWageGrade_Code').val()) { Params = Params + ",8" }
    if ($('#JobTransition_JobDetail_CurrentSalaryStructure_Code').val() != $('#JobTransition_JobDetail_NewSalaryStructure_Code').val()) { Params = Params + ",9" }
    if ($('#JobTransition_JobDetail_CurrentDivision_Code').val() != $('#JobTransition_JobDetail_NewDivision_Code').val()) { Params = Params + ",11" }
    if ($('#JobTransition_JobDetail_CurrentSection_Code').val() != $('#JobTransition_JobDetail_NewSection_Code').val()) { Params = Params + ",12" }
    if ($('#JobTransition_JobDetail_CurrentPayrollGroup_Code').val() != $('#JobTransition_JobDetail_NewPayrollGroup_Code').val()) { Params = Params + ",13" }
    if ($('#JobTransition_JobDetail_CurrentCTC').val() != $('#JobTransition_JobDetail_NewCTC').val()) { Params = Params + ",14" }
    if ($('#JobTransition_JobDetail_BasicOldAmt').val() != $('#JobTransition_JobDetail_BasicNewValue').val()) { Params = Params + ",15" }
    if ($('#JobTransition_JobDetail_CurrentRenewalDate').val() != $('#JobTransition_JobDetail_NewRenewalDate').val()) { Params = Params + ",17" }
    if ($('#JobTransition_JobDetail_CurrentIncentiveCostCenter_Code').val() != $('#JobTransition_JobDetail_NewIncentiveCostCenter_Code').val()) { Params = Params + ",18" }
    if ($('#JobTransition_JobDetail_CurrentIncApplicability_Code').val() != $('#JobTransition_JobDetail_NewIncApplicability_Code').val()) { Params = Params + ",19" }
    if ($('#JobTransition_JobDetail_CurrentSubDepartment_Code').val() != $('#JobTransition_JobDetail_NewSubDepartment_Code').val()) { Params = Params + ",21" }
    if ($('#JobTransition_JobDetail_CurrentFunction_Code').val() != $('#JobTransition_JobDetail_NewFunction_Code').val()) { Params = Params + ",22" }

    const CurrentWageGrade = $('#JobTransition_JobDetail_CurrentWageGrade_Code').val() || '';
    const NewWageGrade = $('#JobTransition_JobDetail_NewWageGrade_Code').val() || '';

    $.ajax({
        type: 'GET',
        url: '/DataBank/JobTransition/GetJobTransitionSummary',
        data: { ChangingParameters: Params, CurrentWageGrade, NewWageGrade },
        dataType: 'html',
        success: function (result)
        {
            if ($(result).find('.Login_Session').length > 0)
            { window.location.reload(); return; }
            $('#summary').html(result).removeClass('hide');

        },
        error: function (xhr)
        {
            Swal.fire('Error', xhr.responseText || 'Something went wrong', 'error');
        }
    });
}
function PopUp_Preview() {
    debugger

    debugger
    data = new FormData($('#job-tran-Save-form')[0]);

    $.ajax({
        type: 'POST',
        url: '/DataBank/JobTransition/SalarySummary',
        dataType: 'html',
        contentType: false,
        processData: false,
        data: data,
        success: function (Data) {
            console.log(Data);
            var m = $('.salary_summary');
            m.find('.modal-body').html('');
            m.find('.modal-body').html(Data);
            m.modal('show');
            //$('#edoj').text('EDOJ:    ' + $('#PreEnrollment_BasicInfo_AcceptedDOJ').val());
        },
        error: function (xhr, textStatus, errorThrown) {
            console.log(xhr.responseText);
            swal.fire(xhr.responseText);

        }
    });


}





