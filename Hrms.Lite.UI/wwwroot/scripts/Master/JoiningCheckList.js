
var prevTab = 'ActiveTab';
var currentTab = 'ACTIVE';




function OnTabClick(Tab, TabIndex) {
   
    debugger;
    showSpinner();
    currentTab = TabIndex;
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");
    //var url = "/Master/JoiningCheckList/GetList";
    //$('#JoiningChecklistID').load(url, { TabIndex: TabIndex }, function () { hideSpinner(); });

    $.ajax({

        type: 'GET',
        url: '/Master/JoiningCheckList/GetList',
        dataType: 'html',
        data: { TabIndex: TabIndex },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#JoiningChecklistID').html("");
                $('#JoiningChecklistID').html(result);
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


function onAddNewOrEditClick(Type, GI, LogGI, index) {
    debugger;
    //currentTab = 'ACTIVE';

    if (Type == 'MODIFY') {

        if ($("#JoiningChecklist_" + index + "__EditActive").val() == 'True') {
            swal.fire(ApprovalPendingMsg);


        }
        else {
            //var url = "/Master/JoiningCheckList/Edit?JoiningCheckListGI=" + GI + "&LogGI=" + LogGI;
            //$('#AddNewOrEditSlider').load(url, function () { });
            //$('body').append('<div class="form-overlay"></div>');

            $.ajax({

                type: 'GET',
                url: '/Master/JoiningCheckList/Edit',
                dataType: 'html',
                data: { JoiningCheckListGI: GI, LogGI: LogGI },
                success: function (result) {
                    debugger
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }
                    else {
                        $('body').append('<div class="form-overlay"></div>');
                        $('#AddNewOrEditSlider').html("");
                        $('#AddNewOrEditSlider').html(result);
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
        //var url = "/Master/JoiningCheckList/Create";
        //$('#AddNewOrEditSlider').load(url, function () { });
        //$('body').append('<div class="form-overlay"></div>');
        $.ajax({

            type: 'GET',
            url: '/Master/JoiningCheckList/Create',
            dataType: 'html',
            data: { JoiningCheckListGI: GI, LogGI: LogGI },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('#AddNewOrEditSlider').html("");
                    $('#AddNewOrEditSlider').html(result);
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


function JoiningCheckListSave(mode) {
    debugger;
    if (validateJoiningCheckList()) {

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
                    url: '/Master/JoiningCheckList/Save',
                    dataType: 'json',
                    data: $('#JoiningCheckList-Save').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                CloseSlider('AddNewOrEdit');
                                var url = "/Master/JoiningCheckList/GetList?TabIndex=" + currentTab;
                                $('#JoiningChecklistID').load(url, function () { });

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




function validateJoiningCheckList()
{

    debugger;
    var i = 0;
    if (!validateById('#Name', 'item')) {
        i++;
    }
    if (!validateById('#CheckListType_Code', 'checklisttype')) {
        i++;
    }
   

    if (i == 0)
        return true;
    else
        return false;
}



function onDeleteClick(GI, LogGI, Type, index) {

    debugger;

    if ($("#JoiningChecklist_" + index + "__EditActive").val() == 'True')
        swal.fire(ApprovalPendingMsg);


    else {
        //var url = "/Master/JoiningChecklist/Delete?JoiningCheckListGI=" + GI + "&LogGI=" + LogGI;
        //$('#DeleteSlider').load(url, function () { });
        //$('body').append('<div class="form-overlay"></div>');
        $.ajax({
            type: 'GET',
            url: '/Master/JoiningChecklist/Delete',
            dataType: 'html',
            data: { JoiningCheckListGI: GI, LogGI: LogGI },
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

function ConfirmDelete(GI, LogGI) {
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
                url: '/Master/JoiningChecklist/Delete',
                dataType: 'json',
                data: $('#deleteJoiningCheckList').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('delete');
                            var url = "/Master/JoiningCheckList/GetList?TabIndex=" + 'ACTIVE';
                            $('#JoiningChecklistID').load(url, function () { });
                            $(".PendingTab").removeClass("active");
                            $(".ActiveTab").addClass("active");
                            $("#JoiningChecklistID").removeClass("hide");
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/JoiningCheckList/Delete?JoiningCheckListGI=" + GI + "&LogGI=" + LogGI;
                            $('#delete').load(url, function () { });
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

    if ($("#JoiningChecklist_" + index + "__MakerChecker").val() == 'True')
        swal.fire(MakerCheckerMsg);

    else {

        //var url = "/Master/JoiningChecklist/Approve?JoiningCheckListGI=" + GI + "&&LogGI=" + LogGI;
        //$('#ApproveSlider').load(url, function () { });
        //$('body').append('<div class="form-overlay"></div>');

        $.ajax({

            type: 'GET',
            url: '/Master/JoiningChecklist/Approve',
            dataType: 'html',
            data: { JoiningCheckListGI: GI, LogGI: LogGI },
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
                url: '/Master/JoiningChecklist/Approve',
                dataType: 'json',
                data: $('#JoiningChecklistapprove').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('ApproveDiv');
                            var url = "/Master/JoiningChecklist/GetList?TabIndex=" + 'APPROVAL_PENDING';
                            $('#JoiningChecklistID').load(url, function () { });
                            //$(".PendingTab").removeClass("active");
                            //$(".ActiveTab").addClass("active");
                            //$("#QualificationList").removeClass("hide");

                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            debugger;
                            var url = "/Master/JoiningChecklist/Approve?JoiningCheckListGI=" + GI + "&LogGI=" + LogGI;
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
                url: '/Master/JoiningChecklist/Reject',
                dataType: 'json',
                data: $('#JoiningChecklistapprove').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('ApproveDiv');
                            var url = "/Master/JoiningChecklist/GetList?TabIndex=" + 'APPROVAL_PENDING';
                            $('#JoiningChecklistID').load(url, function () { });
                            //$(".PendingTab").removeClass("active");
                            //$(".ActiveTab").addClass("active");
                            //$("#QualificationList").removeClass("hide");
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/JoiningChecklist/Approve?JoiningCheckListGI=" + GI + "&LogGI=" + LogGI;
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



function ExportToExcel() {

    debugger;
    if (currentTab == 'APPROVAL_PENDING') {
        var pendingTableLength = $('#JoiningChecklist_Count').val();
        if (pendingTableLength > 0) {
            url = "/Master/JoiningChecklist/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }

    else if (currentTab == 'ACTIVE') {
        var activeTableLength = $('#JoiningChecklist_Count').val();
        if (activeTableLength > 0) {
            url = "/Master/JoiningChecklist/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }
    else if (currentTab == 'INACTIVE') {
        var inActiveTableLength = $('#JoiningChecklist_Count').val();
        if (inActiveTableLength > 0) {
            url = "/Master/JoiningChecklist/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }

    }
}





function ActiveInactiveValidationChecklist(name, code) {
    debugger

    $.ajax({

        type: 'POST',
        url: '/Master/JoiningCheckList/GetActiveInactiveValidation?name=' + name + '&code=' + code,
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
                    $("#checklist").prop("checked", true);
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