var prevTab = 'ActiveTab';
var currentTab = 'ACTIVE';


function OnTabClick(Tab, TabIndex) {
    showSpinner();
    currentTab = TabIndex;
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");
    ////var url = "/Master/AttendanceRequestReason/GetList";
    //$('#tabsDropdownAllowance').val(currentTab);
    //$.ajaxSetup({
    //    async: false
    //});
    //$('#AttendanceRequestList').load(url, { TabIndex: TabIndex }, function () { hideSpinner(); });


    $.ajax({

        type: 'GET',
        url: '/Master/AttendanceRequestReason/GetList',
        dataType: 'html',
        data: { TabIndex: TabIndex },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#AttendanceRequestList').html("");
                $('#AttendanceRequestList').html(result);
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





function ActiveInactiveValidationAttReason(name, code) {
    debugger

    $.ajax({

        type: 'POST',
        url: '/Master/AttendanceRequestReason/GetActiveInactiveValidation?name=' + name + '&code=' + code,
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
                    $("#Attreason").prop("checked", true);
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
    //var url = "/Master/AttendanceRequestReason/GetList";
    //$('#AttendanceRequestList').load(url, { TabIndex: currentTab }, function () { hideSpinner(); });


    $.ajax({

        type: 'GET',
        url: '/Master/AttendanceRequestReason/GetList',
        dataType: 'html',
        data: { TabIndex: currentTab },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#AttendanceRequestList').html("");
                $('#AttendanceRequestList').html(result);
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
function AddNewOrEditClick(Type, GI, LogGI, index) {
    debugger;
    //currentTab = 'ACTIVE';

    if (Type == 'MODIFY') {

        if ($("#RequestReason_" + index + "__EditActive").val() == 'True')
            swal.fire(ApprovalPendingMsg);


        else {
        //var url = "/Master/AttendanceRequestReason/Edit?ReasonGI=" + GI + "&LogGI=" + LogGI;

        //    $('#AddNewOrEditSlider').load(url, function () { });
        //    $('body').append('<div class="form-overlay"></div>');


            $.ajax({

                type: 'GET',
                url: '/Master/AttendanceRequestReason/Edit',
                dataType: 'html',
                data: { ReasonGI: GI, LogGI: LogGI },
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
        //var url = "/Master/AttendanceRequestReason/Create";
        //$('#AddNewOrEditSlider').load(url, function (data) { console.log(data); });
        //$('body').append('<div class="form-overlay"></div>');

        $.ajax({

            type: 'GET',
            url: '/Master/AttendanceRequestReason/Create',
            dataType: 'html',
            data: { ReasonGI: GI, LogGI: LogGI },
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
function onDeleteClick(GI, LogGI, index) {

    debugger;

    if ($("#RequestReason_" + index + "__EditActive").val() == 'True')
        swal.fire(ApprovalPendingMsg);


    else {
        //var url = "/Master/AttendanceRequestReason/Delete?ReasonGI=" + GI + "&LogGI=" + LogGI;
        //$('#DeleteSlider').load(url, function () { });
        //$('body').append('<div class="form-overlay"></div>');


        $.ajax({
            type: 'GET',
            url: '/Master/AttendanceRequestReason/Delete',
            dataType: 'html',
            data: { ReasonGI: GI, LogGI: LogGI },
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

function validateARR() {
    var i = 0;
    if (!validateById('#Name', 'reasonname')) {
        i++;
    }
    if ($('#ReasonApplicable').find('input[type="checkbox"]:checked').length == 0) {
        i++;
        swal.fire("Please Select atleast one Leave Type");
    }

    if (i == 0)
        return true;
    else
        return false;
}
function AttendaceRequestReasonSave(mode) {
    debugger;
    if (validateARR()) {
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
                    url: '/Master/AttendanceRequestReason/Save',
                    dataType: 'json',
                    data: $('#AttendanceRequestReasonSave').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                CloseSlider('AddNewOrEdit');
                                var url = "/Master/AttendanceRequestReason/GetList?TabIndex=" + currentTab;
                                $('#AttendanceRequestList').load(url, function () { });

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

function onApproveClick(GI, LogGI, Type, index) {

    debugger;

    if ($("#RequestReason_" + index + "__MakerChecker").val() == 'True')
        swal.fire(MakerCheckerMsg);

    else {

    //var url = "/Master/AttendanceRequestReason/Approve?ReasonGI=" + GI + "&LogGI=" + LogGI;
    //    $('#ApproveSlider').load(url, function () { });
    //    $('body').append('<div class="form-overlay"></div>');

        $.ajax({

            type: 'GET',
            url: '/Master/AttendanceRequestReason/Approve',
            dataType: 'html',
            data: { ReasonGI: GI, LogGI: LogGI },
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
                url: '/Master/AttendanceRequestReason/Approve',
                dataType: 'json',
                data: $('#approve').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('Approve');
                            var url = "/Master/AttendanceRequestReason/GetList?TabIndex=" + 'APPROVAL_PENDING';
                            $('#AttendanceRequestList').load(url, function () { });
                        });

                    }
                    else {
                        debugger
                        swal.fire(result.message).then(function () {
                            var url = "/Master/AttendanceRequestReason/Approve?DepartmentGI=" + GI + "&LogGI=" + LogGI;
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
                url: '/Master/AttendanceRequestReason/Reject',
                dataType: 'json',
                data: $('#approve').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('Approve');
                            var url = "/Master/AttendanceRequestReason/GetList?TabIndex=" + 'APPROVAL_PENDING';
                            $('#AttendanceRequestList').load(url, function () { });
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/AttendanceRequestReason/Approve?ReasonGI=" + GI + "&LogGI=" + LogGI;
                            $('#AttendanceRequestList').load(url, function () { });
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
            $.ajax({

                type: 'POST',
                url: '/Master/AttendanceRequestReason/Delete',
                dataType: 'json',
                data: $('#DeleteAttendanceRequestReason').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('delete');
                            var url = "/Master/AttendanceRequestReason/GetList?TabIndex=" + 'ACTIVE';
                            $('#AttendanceRequestList').load(url, function () { });
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/AttendanceRequestReason/Delete?ReasonGI=" + GI + "&LogGI=" + LogGI;
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

function ExportToExcel() {
    debugger;
    if (currentTab == 'APPROVAL_PENDING') {
        var pendingTableLength = $('#RequestReason_Count').val();
        if (pendingTableLength > 0) {
            url = "/Master/AttendanceRequestReason/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }

    else if (currentTab == 'ACTIVE') {
        var activeTableLength = $('#RequestReason_Count').val();
        if (activeTableLength > 0) {
            url = "/Master/AttendanceRequestReason/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }
    else if (currentTab == 'INACTIVE') {
        var inActiveTableLength = $('#RequestReason_Count').val();
        if (inActiveTableLength > 0) {
            url = "/Master/AttendanceRequestReason/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }

    }
}