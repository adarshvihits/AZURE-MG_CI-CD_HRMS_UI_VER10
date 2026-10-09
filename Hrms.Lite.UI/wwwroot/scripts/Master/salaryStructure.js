var prevTab = 'ActiveTab';
var currentTab = 'ACTIVE';


function allow_applivalidn()
{
    debugger
    var rowcount = document.getElementById('allwTable').rows.length;
    var i = 0;
    /* $(".allowance_appl").each(function (index)*/
    while ( i < rowcount) {
        if ($('#FixedAllowance_' + i + '__Applicable').is(':checked')) {
            if ($('#FixedAllowance_' + i + '__FormulaType_Code').val() == "") {

                return false;
            }
        }
        i++;
    }
    return true;
  /*  });*/
}

function OnTabClick(Tab, TabIndex) {
    debugger;
    showSpinner();
    
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");
    var url = "/Master/SalaryStructure/GetList";
    //$('#tabsDropdownAllowance').val(currentTab);
    $.ajaxSetup({
        async: false
    });
    $('#salryStructureList').load(url, { TabIndex: TabIndex }, function () { hideSpinner(); });
    prevTab = Tab;
    currentTab = TabIndex;

    
}
function onSalaryStructureTypeChange(sender) {
    if ($(sender).val() == 1) {
        $('#FAllwAmtOrPer').html('Amount');     
        $('#allwTable').find('input[type=text]').attr('maxlength', 8);
        $('#allwTable').find('input[type=text]').val(0);
    }
    else {
        $('#FAllwAmtOrPer').html('%');
        $('#allwTable').find('input[type=text]').attr('maxlength', 3);
        $('#allwTable').find('input[type=text]').val(0);
    }

    if ($(sender).val() == 3) {
        $('#FAllwAmtOrPer').html('Amt / %');
        $('.formulaTypeCol').removeClass('hide');
        $('.formulaTypeDropdown').removeClass('hide');
    }
    else {
        $('.formulaTypeCol').addClass('hide');
        $('.formulaTypeDropdown').addClass('hide');
    }
}

function onFormulaTypeChange(sender, index) {
    if ($(sender).val() != 1 && $(sender).val() != 4) {
        $('#FixedAllowance_' + index + '__Amount').val('');
        $('#FixedAllowance_' + index + '__Amount').attr('maxlength', 3);
    }
    else {
        $('#FixedAllowance_' + index + '__Amount').val('');
        $('#FixedAllowance_' + index + '__Amount').attr('maxlength', 8);
    }
}

function tabClick(val) {
    showSpinner();

    currentTab = $(val).val();
    $("." + prevTab).removeClass("active");
    if (currentTab == "ACTIVE")
        prevTab = 'ActiveTab'
    else if (currentTab == "INACTIVE")
        prevTab = 'InctiveTab';
    else
        prevTab = 'PendingTab';

    $("." + prevTab).addClass("active");
   
    $.ajax({

        type: 'GET',
        url: '/Master/SalaryStructure/GetList',
        dataType: 'html',
        data: { TabIndex: TabIndex },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#salryStructureList').html("");
                $('#salryStructureList').html(result);
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


function onAddNewClick() {
    var url = "/Master/SalaryStructure/AddNew";
    window.location.href = url;
}

function onCheckBoxClick(tableid) {
    debugger
    if ($('#' + tableid).find('input[type=checkbox]').is(':checked')) {
        return true;
    }
    else {
        return false;
    }
}

function validateform(count)
{
    debugger
    flag = true;
    var i = 0;
    if (!validateById('#Name', 'SName')) {
        i++;
    }
    if (!validateById('#ShortName', 'SShortName')) {
        i++;
    }
    if (!validateById('#SalaryStructureType_Code', 'SType')) {
        i++;
    }
    if (!validateById('#WageType_Code', 'SWageType')) {
        i++;
    }
    if (!onCheckBoxClick('allwTable')) {
        swal.fire("Please Select atleast One Allowance");
        i++;
    }
    if (i == 0) {
        if ($('#SalaryStructureType_Code').val() == 2) {
            debugger;
            var totPercentage = 0;
            for (j = 0; j < count; j++) {
                if ($('#FixedAllowance_' + j + '__Applicable').is(':checked')) {
                    debugger
                    totPercentage = parseFloat(totPercentage) + parseFloat($('#FixedAllowance_' + j + '__Amount').val());
                }

            }
            if (totPercentage < 100) {
                swal.fire("Total percentage should be 100");
                flag = false;
               
            }
            if (totPercentage > 100) {
                swal.fire("Total percentage should not greater than 100");
                flag = false;
             
            }
        }
        if ($('#SalaryStructureType_Code').val() == 3) {
            if (!allow_applivalidn()) {
            
                swal.fire('Please select Formula type for applicable allowances');
                flag = false;
            }
            debugger;

            var BalancePercCount = 0;
            var BalancePercentage = 0;
            var BalancePercentageCount = 0;
            var totalBalance = 0;
            var allowancepercentagecheck = 0;

            for (var k = 0; k < count; k++)
            {
                if ($('#FixedAllowance_' + k + '__Applicable').is(':checked'))
                {
                   
                    totalBalance = parseFloat(totalBalance) + parseFloat($('#FixedAllowance_' + k + '__Amount').val());
                    if ( $('#FixedAllowance_' + k + '__FormulaType_Code').val() == 4)
                    {
                        BalancePercentage = parseFloat(BalancePercentage) + parseFloat($('#FixedAllowance_' + k + '__Amount').val());
                        BalancePercentageCount = BalancePercentageCount + 1;
                        if ($('#FixedAllowance_' + k + '__Amount').val() == '')
                        {
                            allowancepercentagecheck = allowancepercentagecheck + 1;
                        }
                    }
                }
            }
            if (allowancepercentagecheck > 0)
            {
                swal.fire("Percentage Of Balance Cannot Be Empty");
                flag = false;
            }

            if (BalancePercentageCount > 0)
            {

            
            if (BalancePercentage < 100)
            {
                swal.fire("Total percentage of Balance Formula type should be 100");
                flag = false;

            }
            if (BalancePercentage > 100) {
                swal.fire("Total percentage of Balance Formula type should not greater than 100");
                 flag = false;

            }
            }
            //if (BalancePercentageCount = 0)
            //{

           
            //if (totalBalance < 100) {
            //    swal.fire("Total percentage should be 100");
            //    flag = false;

            //}
            //if (totalBalance > 100) {
            //    swal.fire("Total percentage should not greater than 100");
            //    flag = false;

            //}
            //}
            //var totPercentage = 0;
            //for (j = 0; j < count; j++) {
            //    if ($('#FixedAllowance_' + j + '__Applicable').is(':checked')) {
            //        if ($('#FixedAllowance_' + j + '__FormulaType_Code').val() != 1 && $('#FixedAllowance_' + j + '__FormulaType_Code').val() != 4) {
            //            totPercentage = parseFloat(totPercentage) + parseFloat($('#FixedAllowance_' + j + '__Amount').val());
            //        }
            //    }

            //}
            //if (totPercentage < 100) {
            //    swal.fire("Total percentage should be 100");
            //    flag = false;

            //}
            //if (totPercentage > 100) {
            //    swal.fire("Total percentage should not greater than 100");
            //     flag = false;

            //}

        }
    }
    return flag;
}
function SaveSalaryStructure(mode, count)
{
    debugger
   
    if (validateform(count)) {
        swal.fire({
            text: "Do you want to " + mode + "?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({

                    type: 'POST',
                    url: '/Master/SalaryStructure/Save',
                    dataType: 'json',
                    data: $('#salary-structure-save').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            if (mode == 'Modify') {
                                swal.fire("Modified Successfully").then(function () {
                                    var url = "/Master/SalaryStructure/Index";
                                    window.location.href = url;
                                });
                            }
                            else {
                                swal.fire("Saved Successfully").then(function () {
                                    var url = "/Master/SalaryStructure/Index";
                                    window.location.href = url;
                                });
                            }

                        }
                        else if (result.message == "MAKER_CHECKER") {
                            swal.fire("Maker and checker cannot be same.").then(function () {
                            });
                        }
                        else if (result.message == "NAME_DUPLICATION_ERROR") {
                            swal.fire("Name already exist.").then(function () {
                            });
                        }
                        else if (result.message == "SHORTNAME_DUPLICATION_ERROR") {
                            swal.fire("Shortname already exist.").then(function () {
                            });
                        }
                        else {
                            swal.fire(result.message).then(function () {
                            });
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


function onApproveClick(GI, LogGI, index) {

    if ($("#List_" + index + "__MakerChecker").val() == 'True')
        swal.fire(MakerCheckerMsg);

    else {
        var url = "/Master/SalaryStructure/Approve?SalaryStructureGI=" + GI + "&LogGI=" + LogGI;

        window.location.href = url;
    }


}

function onEditClick(GI, LogGI, index) {

    debugger;
        if ($("#List_" + index + "__EditActive").val() == 'True')
            swal.fire(ApprovalPendingMsg);
        else {
            var url = "/Master/SalaryStructure/Edit?SalaryStructureGI=" + GI + "&LogGI=" + LogGI;
            window.location.href = url;
        }
}

function onDeleteClick(GI, LogGI, index) {


    if ($("#List_" + index + "__EditActive").val() == 'True')
        swal.fire(ApprovalPendingMsg);


    else {
        var url = "/Master/SalaryStructure/Delete?SalaryStructureGI=" + GI + "&LogGI=" + LogGI;
        window.location.href = url;
       
    }
}

function confirmDelete(GI) {

    swal.fire({
        text: "Do you want to Delete?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({

                type: 'POST',
                url: '/Master/SalaryStructure/DeleteSalaryStructure',
                dataType: 'json',
                data: { SalaryStructureGI : GI},
                success: function (result) {
                    if (result.success == true) {
                        swal.fire("Deleted Successfully").then(function () {                            
                            var url = "/Master/SalaryStructure/Index";
                            window.location.href = url;
                        });

                    }
                    else if (result.message == "MAKER_CHECKER") {
                        swal.fire("Maker and checker cannot be same.").then(function () {                           
                        });
                    }
                    else {
                        swal.fire(result.message).then(function () {                            

                        });
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

function confirmApprove() {
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
                url: '/Master/SalaryStructure/ApproveSalaryStructure',
                dataType: 'json',
                data: $('#approve-salary-structure').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire("Approved Successfully").then(function () {                            
                            var url = "/Master/SalaryStructure/Index";                            
                            window.location.href = url;
                        });

                    }
                    else if (result.message == "MAKER_CHECKER") {
                        swal.fire("Maker and checker cannot be same.").then(function () {                            

                        });
                    }
                    else {
                        swal.fire(result.message).then(function () {                          


                        });
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

function confirmReject() {

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
                url: '/Master/SalaryStructure/RejectSalaryStructure',
                dataType: 'json',
                data: $('#approve-salary-structure').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire("Rejected Successfully").then(function () {
                            var url = "/Master/SalaryStructure/Index";
                            window.location.href = url;
                        });

                    }
                    else if (result.message == "MAKER_CHECKER") {
                        swal.fire("Maker and checker cannot be same.").then(function () {                          

                        });
                    }
                    else {
                        swal.fire(result.message).then(function () {                           

                        });
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

function onExportToExcelClick() {

    if (currentTab != "APPROVAL_PENDING") {

        var salryStructureListTableLength = $('#List_Count').val();
        if (salryStructureListTableLength > 0) {
            url = "/Master/SalaryStructure/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }
    else {
        var salryStructurePendingTableLength = $('#List_Count').val();
        if (salryStructurePendingTableLength > 0) {
            url = "/Master/SalaryStructure/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }
}




function ActiveInactiveValidationSalary(name, code) {
    debugger

    $.ajax({

        type: 'POST',
        url: '/Master/SalaryStructure/GetActiveInactiveValidation?name=' + name + '&code=' + code,
        dataType: 'json',

        success: function (result) {
            if (result.success == true) {
                //swal.fire(result.message).then(function () {
                debugger;
                // $("#activeInactive").prop("checked", false);

                //});

            }
            else {
                swal.fire(result.message).then(function () {
                    $("#salarystructure").prop("checked", true);
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