var prevTab = 'all';
var currentTab = 0;  //code for 'all'
var currTabName = 'All';
function ontabClick(Tab, TabIndex,TabName) {
    debugger;
    showSpinner();
    currentTab = TabIndex;
    currTabName = TabName;
    $('#TabCode').val(TabIndex);
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");
    //var searchID = $("#Filter_Code").val();;
    //var url = "/DataBank/JobTransitionConfirmation/GetJobTransitionConfirmList";
    //$('#jobTransition-Confirm-Approval-Pending-List').load(url, { TabIndex: TabIndex, SearchId: searchID }, function () { });
    ApplyFilter('apply');
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

function Approve(Mode) {
    debugger;
    swal.fire({
        text: "Do you want to " + Mode+"?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            //    // Save it
          /*  showSpinner();*/
            $.ajax({

                type: 'POST',
                url: '/DataBank/JobTransitionConfirmation/Confirm?Mode=' + Mode,
                dataType: 'json',
                data: $('#jobTransitionApprove').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        var msg = '';
                        if (Mode == 'Reject') {
                            msg = 'Rejected Successfully';
                        }
                        else {
                            msg = 'Confirmed Successfully';
                        }
                        swal.fire(msg).then(function () {
                            hideSpinner();
                            var url = "/DataBank/JobTransitionConfirmation/Index";
                            window.location.href = url;

                        });

                    }

                    else {
                        swal.fire(result.message).then(function () {
                            hideSpinner();
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
function onFilterDropdownChange(SearchId) {
    debugger;

    //var url = "/DataBank/JobTransitionConfirmation/GetJobTransitionConfirmList";
    showSpinner();
    var search = SearchId.value;
    //$('#jobTransition-Confirm-Approval-Pending-List').load(url, { TabIndex: currentTab, SearchId: search }, function () { });
    $('#Filter_Code').val(search);
    $('.filterDropDown').val(search);
    ApplyFilter('reset');
}

function onExportToExcelClick() {
    debugger;
    var searchid = $('#Filter_Code').val();

    var TableLength = $('#JobTransitionConfirmList_Count').val();
    if (TableLength > 0) {
        url = "/DataBank/JobTransitionConfirmation/ExportToExcel?TabIndex=" + currentTab + "&SearchId=" + searchid + "&TabName=" + currTabName;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}


function ApplyFilter(mode) {
    debugger
    var tabIndex = $('#Tabindex').val();
    var search = $('#SearchId').val();
    $("#Tabindex").val(currentTab);
    $("#SearchId").val($('#Filter_Code').val())
    if (mode == 'apply') {
        if (filterApplicable >= 1) {
            $.ajax({

                type: 'POST',
                url: '/DataBank/JobTransitionConfirmation/ApplyFilter',
                dataType: 'html',
                data: $('#BasicFilter').serialize(),
                success: function (result) {
                    debugger
                    CloseSlider('basicFilter');
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }
                    else {
                        $('#jobTransition-Confirm-Approval-Pending-List').html("");
                        $('#jobTransition-Confirm-Approval-Pending-List').html(result);
                        $('#jobTransition-Confirm-Approval-Pending-List').removeClass('hide');
                    }
                    hideSpinner();
                                
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
                url: '/DataBank/JobTransitionConfirmation/ApplyFilter',
                dataType: 'html',
                data: $('#BasicFilter').serialize(),
                success: function (result) {
                    debugger
                    CloseSlider('basicFilter');
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }
                    else {
                        $('#jobTransition-Confirm-Approval-Pending-List').html("");
                        $('#jobTransition-Confirm-Approval-Pending-List').html(result);
                        $('#jobTransition-Confirm-Approval-Pending-List').removeClass('hide');
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
            url: '/DataBank/JobTransitionConfirmation/ApplyFilter',
            dataType: 'html',
            data: $('#BasicFilter').serialize(),
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('#jobTransition-Confirm-Approval-Pending-List').html("");
                    $('#jobTransition-Confirm-Approval-Pending-List').html(result);
                    $('#jobTransition-Confirm-Approval-Pending-List').removeClass('hide');
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
    debugger;
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
