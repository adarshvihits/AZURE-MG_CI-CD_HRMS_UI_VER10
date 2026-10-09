var prevTab = 'tab1';
var currentTab = 'ACTIVE';

function OntabClick(Tab, TabIndex) {
    $.ajaxSetup({
        async: false
    });
    debugger;
    showSpinner();
    //currentTab = TabIndex;
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");
    /*  $('#tabsDropdownSkill').val(currentTab);*/

    $.ajax({

        type: 'GET',
        url: '/Master/VDAGroup/GetList',
        dataType: 'html',
        data: { TabIndex: TabIndex },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#VDAList').html("");
                $('#VDAList').html(result);
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

    prevTab = Tab;
}

function AddNewOrEditClick(Type, GI, LogGI, index) {
    debugger;
    //currentTab = 'ACTIVE';

    if (Type == 'MODIFY') {
        debugger
        if ($("#VDAGroupList_" + index + "__EditActive").val() == 'True') {
            swal.fire(ApprovalPendingMsg);

        }
        else {
            //var url = "/Master/AssetMaster/Edit?AssetGI=" + GI + "&LogGI=" + LogGI;

            //$('#newRequestSlider').load(url, function () { });
            //$('body').append('<div class="form-overlay"></div>');

            $.ajax({

                type: 'GET',
                url: '/Master/VDAGroup/Edit',
                dataType: 'html',
                data: { VDAGroupGI: GI, LogGI: LogGI },
                success: function (result) {
                    debugger
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }
                    else {
                        $('body').append('<div class="form-overlay"></div>');
                        $('#AddNewSlider').html("");
                        $('#AddNewSlider').html(result);
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
        // var url = "/Master/AccountsHead/Create";
        $.ajax({

            type: 'GET',
            url: '/Master/VDAGroup/Create',
            dataType: 'html',
            data: { VDAGroupGI: GI, LogGI: LogGI },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('#AddNewSlider').html("");
                    $('#AddNewSlider').html(result);
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


function VDAGroupSave(mode) {
    debugger;

    if (validateVDA()) {
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
                    url: '/Master/VDAGroup/Save',
                    dataType: 'json',
                    data: $('#VDASave').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                CloseSlider('addNew');
                                var url = "/Master/VDAGroup/GetList";
                                $('#VDAList').load(url, { TabIndex: currentTab }, function () { });
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




function validateVDA() {
    var i = 0;
    if (!validateById('#Name', 'VDAName')) {
        i++;
    }
    if (!validateById('#ShortName', 'ShName')) {
        i++;
    }

   
    if (i == 0)
        return true;
    else
        return false;
}






function confirmApprove(GI, LogGI) {
    //$.ajaxSetup({
    //    async: false
    //});
    debugger;
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
                url: '/Master/VDAGroup/Approve',
                dataType: 'json',

                data: $('#approveVDA').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            debugger;
                            CloseSlider('approveDiv');
                            var url = "/Master/VDAGroup/GetList?TabIndex=" + 'APPROVAL_PENDING';
                            debugger;




                            $('#VDAList').load(url, function () { });
                            //$(".PendingTab").addClass("active");
                            //$(".ActiveTab").removeClass("active");
                            //$(".InctiveTab").removeClass("active");
                            /* $("#ActiveList").addClass("hide");*/
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/VDAGroup/Approve?VDAGroupGI=" + GI + "&LogGI=" + LogGI;
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

    if ($("#VDAGroupList_" + index + "__EditActive").val() == 'True')
        swal.fire(ApprovalPendingMsg);





    else {

        $.ajax({
            type: 'GET',
            url: '/Master/VDAGroup/Delete',
            dataType: 'html',
            data: { VDAGroupGI: GI, LogGI: LogGI },
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

function confirmDelete(GI, LogGI) {

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
                url: '/Master/VDAGroup/Delete',
                dataType: 'json',
                data: $('#deleteVDA').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('DeleteDiv');
                            debugger;
                            var url = "/Master/VDAGroup/GetList?TabIndex=" + 'ACTIVE';
                            $('#VDAList').load(url, function () { });
                            $(".PendingTab").removeClass("active");
                            $(".ActiveTab").addClass("active");

                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/VDAGroup/Delete?VDAListGI=" + GI + "&LogGI=" + LogGI;
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

    if ($("#VDAGroupList_" + index + "__MakerChecker").val() == 'True')
        swal.fire(MakerCheckerMsg);

    else {
        //var url = "/Master/AssetMaster/Approve?AssetGI=" + GI + "&LogGI=" + LogGI;

        //$('#approveSlider').load(url, function () { });
        //$('body').append('<div class="form-overlay"></div>');


        $.ajax({

            type: 'GET',
            url: '/Master/VDAGroup/Approve',
            dataType: 'html',
            data: { VDAGroupGI: GI, LogGI: LogGI },
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


function confirmReject(GI, LogGI) {

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
                url: '/Master/VDAGroup/Reject',
                dataType: 'json',
                data: $('#approveVDA').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('approveDiv');
                            var url = "/Master/VDAGroup/GetList?TabIndex=" + 'APPROVAL_PENDING';
                            $('#VDAList').load(url, function () { });
                            //$(".PendingTab").addClass("active");
                            //$(".ActiveTab").removeClass("active");
                            //$(".InctiveTab").removeClass("active");

                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/VDAGroup/Approve?VDAGroupGI=" + GI + "&LogGI=" + LogGI;
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


function validateCostCenter() {
    var i = 0;



    if (!validateById('#Name', 'name')) {
        i++;
    }
    if (!validateById('#ShortName', 'Sname')) {
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
        var pendingTableLength = $('#CostCenterList_Count').val();
        if (pendingTableLength > 0) {
            url = "/Master/CostCenter/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }

    else {
        var activeTableLength = $('#CostCenterList_Count').val();
        if (activeTableLength > 0) {
            url = "/Master/CostCenter/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }


}




function ActiveInactiveValidationVDA(name, code) {
    debugger

    $.ajax({

        type: 'POST',
        url: '/Master/VDAGroup/GetActiveInactiveValidation?name=' + name + '&code=' + code,
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
                    $("#VDAActive").prop("checked", true);
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


function ExportToExcel() {

    debugger;
  
        var pendingTableLength = $('#VDAGroupList_Count').val();
        if (pendingTableLength > 0) {
            url = "/Master/VDAGroup/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    

    //else {
    //    var activeTableLength = $('#VDAGroupList_Count').val();
    //    if (activeTableLength > 0) {
    //        url = "/Master/CostCenter/ExportToExcel?TabIndex=" + currentTab;
    //        window.location.href = url;
    //    }
    //    else {
    //        swal.fire(NoDataFoundMsg);
    //    }
    //}


}