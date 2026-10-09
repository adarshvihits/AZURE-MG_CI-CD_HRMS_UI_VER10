////var prevTab = 'ActiveTab';
////var currentTab = 'ACTIVE';

////function OntabClick(Tab, TabIndex) {
   
////    debugger;
////    showSpinner();
////    currentTab = TabIndex;
////    $("." + prevTab).removeClass("active");
////    $("." + Tab).addClass("active");
////    //$('#tabsDropdownSkill').val(currentTab);
    
////    $.ajax({

////        type: 'GET',
////        url: '/Master/AccountsHead/GetList',
////        dataType: 'html',
////        data: { TabIndex: TabIndex },

////        success: function (result) {
////            debugger
////            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
////                window.location.reload();
////            }
////            else {
////                $('#AccountList').html("");
////                $('#AccountList').html(result);
////            }
////            hideSpinner();


////        },


////        error: function (xhr, textStatus, errorThrown) {
////            debugger
////            document.body.id = "kt_body";
////            $('body').html(xhr.responseText);
////            hideSpinner();

////        }
////    });


////    currentTab = TabIndex;

////    prevTab = Tab;
////}






var prevTab = 'tab1';
var currentTab = 'ACTIVE';

function OnTabClick(Tab, TabIndex) {
    debugger;
    showSpinner();
    currentTab = TabIndex;
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");
    /*$('#tabsDropdownCircle').val(currentTab);*/

    $.ajax({

        type: 'GET',
        url: '/Master/AccountsHead/GetList',
        dataType: 'html',
        data: { TabIndex: TabIndex },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#AccountList').html("");
                $('#AccountList').html(result);
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











function AddNewOrEditClick(Type, GI, LogGI, index) {
    debugger;
    //currentTab = 'ACTIVE';

    if (Type == 'MODIFY') {
        debugger
        if ($("#AccountsHeadList_" + index + "__EditActive").val() == 'True') {
            swal.fire(ApprovalPendingMsg);

        }
        else {
            //var url = "/Master/AssetMaster/Edit?AssetGI=" + GI + "&LogGI=" + LogGI;

            //$('#newRequestSlider').load(url, function () { });
            //$('body').append('<div class="form-overlay"></div>');

            $.ajax({

                type: 'GET',
                url: '/Master/AccountsHead/Edit',
                dataType: 'html',
                data: { AccountsGI: GI, LogGI: LogGI },
                success: function (result) {
                    debugger
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }
                    else {
                        $('body').append('<div class="form-overlay"></div>');
                        $('#NewReqSlider').html("");
                        $('#NewReqSlider').html(result);
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
        debugger
        //var url = "/Master/AssetMaster/Create";
        //$('#newRequestSlider').load(url, function () { });
        //$('body').append('<div class="form-overlay"></div>');
        var url = "/Master/AccountsHead/Create";
        $.ajax({

            type: 'GET',
            url: '/Master/AccountsHead/Create',
            dataType: 'html',
            data: { AccountsGI: GI, LogGI: LogGI },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('#NewReqSlider').html("");
                    $('#NewReqSlider').html(result);
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


function AccountSave(mode) {
    debugger;

    if (validateAccountsHead()) {
        //console.log($('#Skill-create-edit').serialize());
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
                    url: '/Master/AccountsHead/Save',
                    dataType: 'json',
                    data: $('#AccountsSave').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                CloseSlider('AddDiv');
                                var url = "/Master/AccountsHead/GetList";
                                $('#AccountList').load(url, { TabIndex: currentTab }, function () { });
                                /* window.location.href = url;*/
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
    else
        return false;
}


function ConfirmApprove(GI, LogGI) {
    //$.ajaxSetup({
    //    async: false
    //});
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
                url: '/Master/AccountsHead/Approve',
                dataType: 'json',

                data: $('#approve').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('approveAccounts');
                            var url = "/Master/AccountsHead/GetList?TabIndex=" + 'APPROVAL_PENDING';
                            debugger;
                            $('#AccountList').load(url, function () { });
                            //$(".PendingTab").addClass("active");
                            //$(".ActiveTab").removeClass("active");
                            //$(".InctiveTab").removeClass("active");
                            /* $("#ActiveList").addClass("hide");*/
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/AccountsHead/Approve?AccountGI=" + GI + "&LogGI=" + LogGI;
                            $('#ApproveSlider').load(url, function () { });
                            $('body').append('<div class="form-overlay"></div>');

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

function onDeleteClick(GI, LogGI, Type, index) {

    debugger;

    if ($("#AccountsHeadList_" + index + "__EditActive").val() == 'True')
        swal.fire(ApprovalPendingMsg);





    else {

        $.ajax({
            type: 'GET',
            url: '/Master/AccountsHead/Delete',
            dataType: 'html',
            data: { AccountsGI: GI, LogGI: LogGI },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                   $('body').append('<div class="form-overlay"></div>');
                    $('#DeleteAccountSlider').html("");
                    $('#DeleteAccountSlider').html(result);
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

function confirmDelete(GI, LogGI) {
    debugger;
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
                url: '/Master/AccountsHead/Delete',
                dataType: 'json',
                data: $('#AccountsheadDelete').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('deleteAccount');
                            var url = "/Master/AccountsHead/GetList?TabIndex=" + 'ACTIVE';
                            $('#AccountList').load(url, function () { });
                            //$(".tab3").removeClass("active");
                            //$(".tab1").addClass("active");

                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/AccountsHead/Delete?AccountGI=" + GI + "&LogGI=" + LogGI;
                            $('#DeleteSlider').load(url, function () { });
                            $('body').append('<div class="form-overlay"></div>');

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


function onApproveClick(GI, LogGI, Type, index) {

    debugger;

    if ($("#AccountsHeadList_" + index + "__MakerChecker").val() == 'True')
        swal.fire(MakerCheckerMsg);

    else {
        //var url = "/Master/AssetMaster/Approve?AssetGI=" + GI + "&LogGI=" + LogGI;

        //$('#approveSlider').load(url, function () { });
        //$('body').append('<div class="form-overlay"></div>');


        $.ajax({

            type: 'GET',
            url: '/Master/AccountsHead/Approve',
            dataType: 'html',
            data: { AccountsGI: GI, LogGI: LogGI },
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


function ConfirmReject(GI, LogGI) {

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
                url: '/Master/AccountsHead/Reject',
                dataType: 'json',
                data: $('#approve').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('approveAccounts');
                            var url = "/Master/AccountsHead/GetList?TabIndex=" + 'ACTIVE';
                            $('#AccountList').load(url, function () { });
                           // $(".PendingTab").addClass("active");
                            $(".tab1").removeClass("active");
                            $(".tab2").removeClass("active");

                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/AccountsHead/Approve?AccountGI=" + GI + "&LogGI=" + LogGI;
                            $('#ApproveSlider').load(url, function () { });
                            $('body').append('<div class="form-overlay"></div>');

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


function validateAccountsHead() {
    var i = 0;
  
   

    if (!validateById('#AccountsHeadUserCode', 'code')) {
        i++;
    }
    if (!validateById('#Name', 'name')) {
        i++;
    }
    if (!validateById('#ShortName', 'SName')) {
        i++;
    }
    if (!validateById('#AccountsHeadCodeType', 'type')) {
        i++;
    }
   
    if (i == 0)
        return true;
    else
        return false;
}


function ExportToExcel() {

    debugger;
    if (currentTab == 'APPROVAL_PENDING') {
        var pendingTableLength = $('#AccountsHeadList_Count').val();
        if (pendingTableLength > 0) {
            url = "/Master/AccountsHead/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }

    else {
        var activeTableLength = $('#AccountsHeadList_Count').val();
        if (activeTableLength > 0) {
            url = "/Master/AccountsHead/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }
    

}



function ActiveInactiveValidationAccount(name, code) {
    debugger

    $.ajax({

        type: 'POST',
        url: '/Master/AccountsHead/GetActiveInactiveValidation?name=' + name + '&code=' + code,
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
                    $("#accounts").prop("checked", true);
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



