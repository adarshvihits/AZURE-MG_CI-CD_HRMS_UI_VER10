var prevTab = 'ActiveTab';
var currentTab = 'ACTIVE';


function OnTabClick(Tab, TabIndex) {
    debugger;
    showSpinner();
    currentTab = TabIndex;
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");
    $('#tabsDropdownLoan').val(currentTab);


    $.ajax({

        type: 'GET',
        url: '/Master/LoanSettings/GetLoanList',
        dataType: 'html',
        data: { TabIndex: TabIndex },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#LoanList').html("");
                $('#LoanList').html(result);
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


function tabClick(val) {
    debugger;
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
        url: '/Master/LoanSettings/GetLoanList',
        dataType: 'html',
        data: { TabIndex: TabIndex },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#LoanList').html("");
                $('#LoanList').html(result);
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




function onApproveClick(GI, LogGI, Type, index) {

    debugger;

    if ($("#LoanSettingsList_" + index + "__MakerChecker").val() == 'True')
        swal.fire(MakerCheckerMsg);

   
    else {

        $.ajax({

            type: 'GET',
            url: '/Master/LoanSettings/Approve',
            dataType: 'html',
            data: { LoanGI: GI, LogGI: LogGI },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('#ApproveSlider').html("");
                    $('#ApproveSlider').html(result);
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


function onAddNewOrEditClick(Type, GI, LogGI, index) {
    debugger;
   

    if (Type == 'MODIFY') {

        if ($("#LoanSettingsList_" + index + "__EditActive").val() == 'True') {
            swal.fire(ApprovalPendingMsg);


        }


        else {
            $.ajax({

                type: 'GET',
                url: '/Master/LoanSettings/Edit',
                dataType: 'html',
                data: { LoanGI: GI, LogGI: LogGI },
                success: function (result) {
                    debugger
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }
                    else {
                        $('body').append('<div class="form-overlay"></div>');
                        $('#CreateOrEditSlider').html("");
                        $('#CreateOrEditSlider').html(result);
                    }


                },


                error: function (xhr, textStatus, errorThrown) {
                    debugger
                    document.body.id = "kt_body";
                    $('body').html(xhr.responseText);


                }
            });

        }
    }

   
    else {
        var url = "/Master/LoanSettings/Create";
        $.ajax({

            type: 'GET',
            url: '/Master/LoanSettings/Create',
            dataType: 'html',
            data: { LoanGI: GI, LogGI: LogGI },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('#CreateOrEditSlider').html("");
                    $('#CreateOrEditSlider').html(result);
                }


            },


            error: function (xhr, textStatus, errorThrown) {
                debugger
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);

            }
        });
    }

}




function onDeleteClick(GI, LogGI, Type, index) {

    debugger;

    if ($("#LoanSettingsList_" + index + "__EditActive").val() == 'True')
        swal.fire(ApprovalPendingMsg);



    else {

        $.ajax({
            type: 'GET',
            url: '/Master/LoanSettings/Delete',
            dataType: 'html',
            data: { LoanGI: GI, LogGI: LogGI },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('#DeleteSlider').html("");
                    $('#DeleteSlider').html(result);
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
}

function confirmApprove(GI, LogGI) {
    debugger
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
                url: '/Master/LoanSettings/Approve',
                dataType: 'json',
                data: $('#approve').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('ApprovalPending');
                            var url = "/Master/LoanSettings/GetLoanList?TabIndex=" + 'APPROVAL_PENDING';
                            $('#LoanList').load(url, function () { });
                            $(".PendingTab").addClass("active");
                            $(".ActiveTab").removeClass("active");
                            $("#LoanList").removeClass("hide");
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/LoanSettings/Approve?LoanGI=" + GI + "&LogGI=" + LogGI;
                            $('#ApproveSlider').load(url, function () { });
                            $('body').append('<div class="form-overlay"></div>');

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


function confirmReject(GI, LogGI) {
    debugger
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
                url: '/Master/LoanSettings/Reject',
                dataType: 'json',
                data: $('#approve').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('ApprovalPending');
                            var url = "/Master/LoanSettings/GetLoanList?TabIndex=" + 'APPROVAL_PENDING';
                            $('#LoanList').load(url, function () { });
                            $(".PendingTab").addClass("active");
                            $(".ActiveTab").removeClass("active");
                            $("#LoanList").removeClass("hide");
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/LoanSettings/Approve?LoanGI=" + GI + "&LogGI=" + LogGI;
                            $('#ApproveSlider').load(url, function () { });
                            $('body').append('<div class="form-overlay"></div>');

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


function confirmDelete(GI, LogGI) {
    debugger
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
                url: '/Master/LoanSettings/Delete',
                dataType: 'json',
                data: $('#Deleteloan').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('delete');
                            var url = "/Master/LoanSettings/GetLoanList?TabIndex=" + 'ACTIVE';
                            $('#LoanList').load(url, function () { });
                            //$(".PendingTab").removeClass("active");
                            //$(".ActiveTab").addClass("active");
                            //$("#LoanList").removeClass("hide");
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/LoanSettings/Delete?LoanGI=" + GI + "&LogGI=" + LogGI;
                            $('#delete').load(url, function () { });
                            $('body').append('<div class="form-overlay"></div>');

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


function SaveLoanMaster(mode) {
    debugger;
    if (validateLoan()) {
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
                    url: '/Master/LoanSettings/Save',
                    dataType: 'json',
                    data: $('#loanSave').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                CloseSlider('AddNewDiv');
                                var url = "/Master/LoanSettings/GetLoanList?TabIndex=" + currentTab;
                                $('#LoanList').load(url, function () { });


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




function ExportToExcel() {

    debugger;
    if (currentTab == 'APPROVAL_PENDING') {
        var pendingTableLength = $('#LoanSettingsList_Count').val();
        if (pendingTableLength > 0) {
            url = "/Master/LoanSettings/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }

    else if (currentTab == 'ACTIVE') {
        var activeTableLength = $('#LoanSettingsList_Count').val();
        if (activeTableLength > 0) {
            url = "/Master/LoanSettings/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }
    else if (currentTab == 'INACTIVE') {
        var inActiveTableLength = $('#LoanSettingsList_Count').val();
        if (inActiveTableLength > 0) {
            url = "/Master/LoanSettings/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }
}

function validateLoan() {
    var i = 0;
    if (!validateById('#Name', 'LoanName')) {
        i++;
    }
    if (!validateById('#ShortName', 'LoanShortName')) {
        i++;
    }
    if (!validateById('#CompanyLoanAmount', 'LoanPAmt')) {
        i++;
    }
    if (!validateById('#MaximumLimitPerEmployee', 'Limit')) {
        i++;
    }
   
    if (!validateById('#AdvType', 'type')) {
        i++;
    }
    if (!validateById('#RepaymentMethod_Code', 'RPMethod')) {
        i++;
    }
    if (!validateById('#MaximumTenure', 'MaxTen')) {
        i++;
    }
    if (!onPrincipleOrLimitChange()) {
        i++;
    }
    if (i == 0)
        return true;
    else
        return false;
}





function hideorshowmaxten() {

   
    RepaymentMethods = $('#RepaymentMethod_Code').val();

    if (RepaymentMethods=="1") {

        $('.showmaxten').hide();
        $('#MaximumTenure').val(1);
    }
    else {
        debugger
        $('.showmaxten').show();
        if ($("#MaximumTenure").val()==0 )
        $('#MaximumTenure').val('');
    }
}
function onPrincipleOrLimitChange() {
    var principleAmt = $('#CompanyLoanAmount').val();
    var LimitAmt = $('#MaximumLimitPerEmployee').val();
    if (parseInt(LimitAmt) > parseInt(principleAmt)) {
        $('#MaximumLimitPerEmployee').addClass('is-invalid');
        $("#Limit").addClass('error-message');
        $("#Limit").html("Maximum Limit Per Employee should be less than Company Principle Amount.");

        //swal.fire("Maximum Limit Per Employee should be less than Company Principle Amount.");
        return false;
    }
    else
        return true;
}


function ActiveInactiveValidationLoan(name, code) {
    debugger

    $.ajax({

        type: 'POST',
        url: '/Master/LoanSettings/GetActiveInactiveValidation?name=' + name + '&code=' + code,
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
                    $("#ActiveLoan").prop("checked", true);
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

