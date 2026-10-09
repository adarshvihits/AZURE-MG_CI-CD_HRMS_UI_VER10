

var prevTab = 'ActiveTab';
var currentTab = 'ACTIVE';

function OnTabClick(Tab, TabIndex) {
    debugger;
    showSpinner();
    currentTab = TabIndex;
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");
    $('#tabsDropdownDesignation').val(currentTab);
    //var url = "/Master/Designation/GetDesignationList";

    //$('#designationList').load(url, { TabIndex: TabIndex }, function () { hideSpinner(); });



    $.ajax({

        type: 'GET',
        url: '/Master/Designation/GetDesignationList',
        dataType: 'html',
        data: { TabIndex: TabIndex },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#designationList').html("");
                $('#designationList').html(result);
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
    //var url = "/Master/Designation/GetDesignationList";
    //$('#designationList').load(url, { TabIndex: currentTab }, function () { hideSpinner(); });

    $.ajax({

        type: 'GET',
        url: '/Master/Designation/GetDesignationList',
        dataType: 'html',
        data: { TabIndex: TabIndex },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#designationList').html("");
                $('#designationList').html(result);
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







function onDeleteClick(GI, LogGI, Type, index) {

    debugger;

    if ($("#DesignationList_" + index + "__EditActive").val() == 'True')
        swal.fire(ApprovalPendingMsg);


    else {

        $.ajax({
            type: 'GET',
            url: '/Master/Designation/Delete',
            dataType: 'html',
            data: { DesignationGI: GI, LogGI: LogGI },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('#deleteDesSlider').html("");
                    $('#deleteDesSlider').html(result);
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






function onAddNewOrEditClick(Type, GI, LogGI, index) {
    debugger;
    //currentTab = 'ACTIVE';
    
    if (Type == 'MODIFY') {

        if ($("#DesignationList_" + index + "__EditActive").val() == 'True')
            swal.fire(ApprovalPendingMsg);


        else {
            $.ajax({

                type: 'GET',
                url: '/Master/Designation/Edit',
                dataType: 'html',
                data: { DesignationGI: GI, LogGI: LogGI },
                success: function (result) {
                    debugger
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }
                    else {
                        $('body').append('<div class="form-overlay"></div>');
                        $('#addOrEditSlider').html("");
                        $('#addOrEditSlider').html(result);
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
        var url = "/Master/Designation/Create";
        $.ajax({

            type: 'GET',
            url: '/Master/Designation/Create',
            dataType: 'html',
            data: { DesignationGI: GI, LogGI: LogGI },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('#addOrEditSlider').html("");
                    $('#addOrEditSlider').html(result);
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





function onDesignationSaveClick(mode) {
    debugger;
    if (validateDesigantion()) {        
        //console.log($('#DesignationSave').serialize());
        swal.fire({
            text: "Do you want to " + mode+"?",
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
                    url: '/Master/Designation/Save',
                    dataType: 'json',
                    data: $('#DesignationSave').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                CloseSlider('AddNewOrEdit');
                                var url = "/Master/Designation/GetDesignationList?TabIndex=" + currentTab;
                                $('#designationList').load(url, function () { hideSpinner(); });

                            });

                        }

                        else {
                            swal.fire(result.message).then(function () {
                                hideSpinner();
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








function validateDesigantion() {
    var i = 0;
    if (!validateById('#Name', 'DesignationName')) {
        i++;
    }
    if (!validateById('#ShortName', 'DesignationShortName')) {
        i++;
    }
    if (!validateById('#NoticePeriod', 'DesignationNoticePeriod')) {
        i++;
    }
    if (i == 0)
        return true;
    else
        return false;
}








function ConfirmDesignationApprove(GI, LogGI) {
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
            showSpinner();
            $.ajax({

                type: 'POST',
                url: '/Master/Designation/Approve',
                dataType: 'json',
                data: $('#approve').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('PendingApprove');
                            var url = "/Master/Designation/GetDesignationList?TabIndex=" + 'APPROVAL_PENDING';
                            $('#designationList').load(url, function () { hideSpinner(); });
                            $(".PendingTab").addClass("active");
                            $(".ActiveTab").removeClass("active");
                            $("#designationList").removeClass("hide");
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/Designation/Approve?DesignationGI=" + GI + "&LogGI=" + LogGI;
                            $('#approveSlider').load(url, function () { hideSpinner(); });
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



function ConfirmDesignationReject(GI, LogGI) {
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
            showSpinner();
            $.ajax({

                type: 'POST',
                url: '/Master/Designation/Reject',
                dataType: 'json',
                data: $('#approve').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('PendingApprove');
                            var url = "/Master/Designation/GetDesignationList?TabIndex=" + 'APPROVAL_PENDING';
                            $('#designationList').load(url, function () { hideSpinner(); });
                            $(".PendingTab").addClass("active");
                            $(".ActiveTab").removeClass("active");
                            $("#designationList").removeClass("hide");
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/Designation/Approve?DesignationGI=" + GI + "&LogGI=" + LogGI;
                            $('#approveSlider').load(url, function () { hideSpinner(); });
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
            showSpinner();
            $.ajax({

                type: 'POST',
                url: '/Master/Designation/Delete',
                dataType: 'json',
                data: $('#DeleteDesignation').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('DeleteDiv');
                            var url = "/Master/Designation/GetDesignationList?TabIndex=" + currentTab;
                            $('#designationList').load(url, function () { hideSpinner(); });
                            //$(".PendingTab").removeClass("active");
                            //$(".ActiveTab").addClass("active");
                            //$("#designationList").removeClass("hide");
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/Designation/Delete?DesignationGI=" + GI + "&LogGI=" + LogGI;
                            $('#delete').load(url, function () {  });
                            $('body').append('<div class="form-overlay"></div>');
                            hideSpinner();
                        });
                    }

                },
                error: function (xhr, textStatus, errorThrown) {
                    toastr.error(xhr.responseText);
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

    if ($("#DesignationList_" + index + "__MakerChecker").val() == 'True')
        swal.fire(MakerCheckerMsg);

    //else {

    //    var url = "/Master/Designation/Approve?DesignationGI=" + GI + "&LogGI=" + LogGI;
    //    $('#approveSlider').load(url, function () { });
    //    $('body').append('<div class="form-overlay"></div>');
    //}

    else {

        $.ajax({

            type: 'GET',
            url: '/Master/Designation/Approve',
            dataType: 'html',
            data: { DesignationGI: GI, LogGI: LogGI },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('#approveSlider').html("");
                    $('#approveSlider').html(result);
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



function ExportToExcel() {
   
        debugger;
        if (currentTab == 'APPROVAL_PENDING') {
            var pendingTableLength = $('#DesignationList_Count').val();
            if (pendingTableLength > 0) {
                url = "/Master/Designation/ExportToExcel?TabIndex=" + currentTab;
                window.location.href = url;
            }
            else {
                swal.fire(NoDataFoundMsg);
            }
        }

        else if (currentTab == 'ACTIVE') {
            var activeTableLength = $('#DesignationList_Count').val();
            if (activeTableLength > 0) {
                url = "/Master/Designation/ExportToExcel?TabIndex=" + currentTab;
                window.location.href = url;
            }
            else {
                swal.fire(NoDataFoundMsg);
            }
        }
        else if (currentTab == 'INACTIVE') {
            var inActiveTableLength = $('#DesignationList_Count').val();
            if (inActiveTableLength > 0) {
                url = "/Master/Designation/ExportToExcel?TabIndex=" + currentTab;
                window.location.href = url;
            }
            else {
                swal.fire(NoDataFoundMsg);
            }

        }
}
function ActiveInactiveValidation(name, code) {
    debugger

    $.ajax({

        type: 'POST',
        url: '/Master/Designation/GetActiveInactiveValidation?name=' + name + '&code=' + code,
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
                    $("#activeInactive").prop("checked", true);
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







