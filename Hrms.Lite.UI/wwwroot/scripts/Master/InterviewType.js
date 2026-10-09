

var prevTab = 'ActiveTab';
var currentTab = 'ACTIVE';


function OnTabClick(Tab, TabIndex) {
    debugger;
    showSpinner();
    currentTab = TabIndex;
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");
    //var url = "/Master/InterviewType/GetInterviewTypeList";
    /*  $('#tabsDropdownAllowance').val(currentTab);*/
    //$('#InterviewtypeID').load(url, { TabIndex: TabIndex }, function () { hideSpinner(); });
    $.ajax({

        type: 'GET',
        url: '/Master/InterviewType/GetInterviewTypeList',
        dataType: 'html',
        data: { TabIndex: TabIndex },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#InterviewtypeID').html("");
                $('#InterviewtypeID').html(result);
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


function ActiveInactiveValidation(name, code) {
    debugger

    $.ajax({

        type: 'POST',
        url: '/Master/InterviewType/GetActiveInactiveValidation?name=' + name + '&code=' + code,
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
                    $("#Activeinactiveinttype").prop("checked", true);
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

function onAddNewOrEditClick(Type, GI, LogGI, index) {
    debugger;
    //currentTab = 'ACTIVE';

    if (Type == 'MODIFY') {

        if ($("#InterviewTypeList_" + index + "__EditActive").val() == 'True')
            swal.fire(ApprovalPendingMsg);


        else {
            //var url = "/Master/InterviewType/Edit?InterviewTypeGI=" + GI + "&LogGI=" + LogGI;

            //$('#AddNewOrEditSlider').load(url, function () { });
            //$('body').append('<div class="form-overlay"></div>');
            $.ajax({

                type: 'GET',
                url: '/Master/InterviewType/Edit',
                dataType: 'html',
                data: { InterviewTypeGI: GI, LogGI: LogGI },
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
        //var url = "/Master/InterviewType/Create";
        //$("#AddNewOrEditSlider").load(url, function () { });
        //$('body').append('<div class="form-overlay"></div>');
        $.ajax({

            type: 'GET',
            url: '/Master/InterviewType/Create',
            dataType: 'html',
            data: { InterviewTypeGI: GI, LogGI: LogGI },
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


function InterviewTypeSave(mode) {
    debugger;
    
    if (ValidateInterviewType()) {

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
                    url: '/Master/InterviewType/Save',
                    dataType: 'json',
                    data: $('#InterviewType-Save').serialize(),
                    success: function (result) {
                        debugger
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                CloseSlider('AddNewDiv');
                                $.ajaxSetup({
                                    async: false
                                });
                                debugger;
                                var url = "/Master/InterviewType/GetInterviewTypeList?TabIndex=" + currentTab;
                                $('#InterviewtypeID').load(url, { TabIndex: TabIndex }, function () { });

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
   

function ValidateInterviewType() {

    debugger;
    var i = 0;
    if (!validateById('#Name', 'InterName')) {
        i++;
    }
    if (!validateById('#ShortName', 'InterShort')) {
        i++;
    }

    if (i == 0)
        return true;
    else
        return false;
}


function onApproveClick(GI, LogGI, Type, index) {

    debugger;

    if ($("#InterviewTypeList_" + index + "__MakerChecker").val() == 'True')
        swal.fire(MakerCheckerMsg);

    else {

        //var url = "/Master/InterviewType/Approve?InterviewTypeGI=" + GI + "&LogGI=" + LogGI;
        //$('#ApproveSlider').load(url, function () { });
        //$('body').append('<div class="form-overlay"></div>');
        $.ajax({

            type: 'GET',
            url: '/Master/InterviewType/Approve',
            dataType: 'html',
            data: { InterviewTypeGI: GI, LogGI: LogGI },
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
                url: '/Master/InterviewType/Approve',
                dataType: 'json',
                data: $('#InterviewTypeApprove').serialize(),
                success: function (result) {
                    debugger
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('ApproveDiv');
                            debugger;
                            var url = "/Master/InterviewType/GetInterviewTypeList?TabIndex=" + 'APPROVAL_PENDING';
                            $('#InterviewtypeID').load(url, function () { });
                            $(".PendingTab").addClass("active");
                            $(".ActiveTab").removeClass("active");
                            $("#InterviewtypeID").removeClass("hide");


                        });

                    }
                    else
                    {
                        debugger;
                        swal.fire(result.message).then(function () {
                            var url = "/Master/InterviewType/Approve?InterviewTypeGI=" + GI + "&LogGI=" + LogGI;
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
                url: '/Master/InterviewType/Reject',
                dataType: 'json',
                data: $('#InterviewTypeApprove').serialize(),
                success: function (result) {
                    debugger
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('ApproveDiv');
                            debugger;
                            var url = "/Master/InterviewType/GetInterviewTypeList?TabIndex=" + 'APPROVAL_PENDING';
                            $('#InterviewtypeID').load(url, function () { });
                            $(".PendingTab").addClass("active");
                            $(".ActiveTab").removeClass("active");
                            $("#InterviewtypeID").removeClass("hide");
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/InterviewType/Approve?InterviewTypeGI=" + GI + "&LogGI=" + LogGI;
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

    if ($("#InterviewTypeList_" + index + "__EditActive").val() == 'True')
        swal.fire(ApprovalPendingMsg);


    else {
        //var url = "/Master/InterviewType/Delete?InterviewTypeGI=" + GI + "&LogGI=" + LogGI;
        //$('#DeleteSlider').load(url, function () { });
        //$('body').append('<div class="form-overlay"></div>');
        $.ajax({
            type: 'GET',
            url: '/Master/InterviewType/Delete',
            dataType: 'html',
            data: { InterviewTypeGI: GI, LogGI: LogGI },
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
                url: '/Master/InterviewType/Delete',
                dataType: 'json',
                data: $('#InterviewTypeDelete').serialize(),
                success: function (result) {
                    debugger
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('delete');
                            var url = "/Master/InterviewType/GetInterviewTypeList?TabIndex=" + 'ACTIVE';
                            $('#InterviewtypeID').load(url, function () { });
                            $(".PendingTab").addClass("active");
                            $(".ActiveTab").removeClass("active");
                            $("#InterviewtypeID").removeClass("hide");
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/InterviewType/Delete?InterviewTypeGI=" + GI + "&LogGI=" + LogGI;
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


function ExportToExcel() {
    debugger;

    
        if (currentTab == 'APPROVAL_PENDING') {
            var pendingTableLength = $('#InterviewTypeList_Count').val();
            if (pendingTableLength > 0) {
                url = "/Master/InterviewType/ExportToExcel?TabIndex=" + currentTab;
                window.location.href = url;
            }
            else {
                swal.fire(NoDataFoundMsg);
            }
        }

        else if (currentTab == 'ACTIVE') {
            var activeTableLength = $('#InterviewTypeList_Count').val();
            if (activeTableLength > 0) {
                url = "/Master/InterviewType/ExportToExcel?TabIndex=" + currentTab;
                window.location.href = url;
            }
            else {
                swal.fire(NoDataFoundMsg);
            }
        }
        else if (currentTab == 'INACTIVE') {
            var inActiveTableLength = $('#InterviewTypeList_Count').val();
            if (inActiveTableLength > 0) {
                url = "/Master/InterviewType/ExportToExcel?TabIndex=" + currentTab;
                window.location.href = url;
            }
            else {
                swal.fire(NoDataFoundMsg);
            }

        }
    
}