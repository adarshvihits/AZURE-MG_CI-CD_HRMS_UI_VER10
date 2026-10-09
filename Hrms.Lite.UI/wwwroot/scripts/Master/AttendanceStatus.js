

var prevTab = 'ActiveTab';
var currentTab = 'ACTIVE';


var colours =
{
    "#ff0000": "Red", "#3d8701": "Green", "#0000ff": "Blue", "#ffff00":
        "Yellow", "#000000": "Black", "#FFA500": "Orange", "#808080": "Gray", "#800080": "Purple"
    , "#FF00FF": "Magenta", "#800000": "Maroon", "#00FFFF": "Cyan",
    "#6F4E37": "Coffee", "#CD7F32": "Bronze", "#E30B5D": "Raspberry", "#36013F": "Deep-Purple",
    "#045F5F": "Medium-Teal", "#614051": "Eggplant", "#3D0C02": "Black-Bean",

    "#F8DAD5": "Pale-Pink", "D8EDE8": "Egg-Shell", "#C8A2C8": "Lilac", "#F8B88B": "Pastel-Orange",
    "#AFDCEC": "Coral-Blue", "#C0C0C0": "Silver", "98AFC7": "Blue-Gray", "#B0BF1A": "Acid-Green",
    "#fdfd96": "Pastel-Yellow", "#FDD39B": "Caramel", "#F8F6F0": "Pearl-White", "#CCFB5D": "Tea-Green",
    "#74bbfb": "Light-Azure", "#7FFFD4": "Organic-Brown", "#c8f3cd": "Creamy-Mint", "#7FFFD4": "Aquamarine"
}
function OnTabClick(Tab, TabIndex) {
    debugger;
    showSpinner();
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");
    currentTab = TabIndex;
    /*var url = "/Master/AttendanceStatus/GetAttendanceStatusList";*/
    $('#tabsDropdownAttendance').val(currentTab);

   /* $('#Attendancestatuslist').load(url, { TabIndex: TabIndex }, function () { hideSpinner(); });*/
    $.ajax({

        type: 'GET',
        url: '/Master/AttendanceStatus/GetAttendanceStatusList',
        dataType: 'html',
        data: { TabIndex: TabIndex },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#Attendancestatuslist').html("");
                $('#Attendancestatuslist').html(result);
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
    //var url = "/Master/AttendanceStatus/GetAttendanceStatusList";
    //$('#Attendancestatuslist').load(url, { TabIndex: currentTab }, function () { hideSpinner(); });


    $.ajax({

        type: 'GET',
        url: '/Master/AttendanceStatus/GetAttendanceStatusList',
        dataType: 'html',
        data: { TabIndex: currentTab },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#Attendancestatuslist').html("");
                $('#Attendancestatuslist').html(result);
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


function onAddNewOrEditClick(Type, GI, LogGI, index)
{
    debugger;
    //currentTab = 'ACTIVE';

    if (Type == 'MODIFY') {

        if ($("#AttendanceStatusList_" + index + "__EditActive").val() == 'True') {

        
            swal.fire(ApprovalPendingMsg);

        }
        else {
            debugger;
            ////var url = "/Master/AttendanceStatus/Edit?AttendanceStatusGI=" + GI + "&LogGI=" + LogGI;
            //$('#AddNewOrEditSlider').load(url, function () { });
            //$('body').append('<div class="form-overlay"></div>');


            $.ajax({

                type: 'GET',
                url: '/Master/AttendanceStatus/Edit',
                dataType: 'html',
                data: { AttendanceStatusGI: GI, LogGI: LogGI },
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
        //var url = "/Master/AttendanceStatus/Create";
        //$('#AddNewOrEditSlider').load(url, function () { });
        //$('body').append('<div class="form-overlay"></div>');


        $.ajax({

            type: 'GET',
            url: '/Master/AttendanceStatus/Create',
            dataType: 'html',
            data: { AttendanceStatusGI: GI, LogGI: LogGI },
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

function SaveAttendanceStatus(mode) {
    debugger;
    if (validateAttendanceStatus()) {
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
                    url: '/Master/AttendanceStatus/Save',
                    dataType: 'json',
                    data: $('#AttendanceStatus-Save').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                CloseSlider('AddNewOrEditDiv');
                                var url = "/Master/AttendanceStatus/GetAttendanceStatusList?TabIndex=" + 'ACTIVE';
                                $('#Attendancestatuslist').load(url, function () { });

                                $(".PendingTab").removeClass("active");
                                $(".ActiveTab").addClass("active");
                                $("#Attendancestatuslist").removeClass("hide");

                            });

                        }

                        else {
                            swal.fire(result.message).then(function () {

                            });
                        }

                    },
                    //error: function (xhr, textStatus, errorThrown) {
                    //    /*    console.log(xhr.responseText);*/
                    //    document.body.id = "kt_body";
                    //    $('body').html(xhr.responseText);
                    //}

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


function validateAttendanceStatus() {

    var i = 0;
    if (!validateById('#Name', 'AttName')) {
        i++;
    }
    if (!validateById('#ShortName', 'AttShtName')) {
        i++;
    }
    if (!validateById('#AttendanceStatusType_Code', 'AttType')) {
        i++;
    }
    //if (!validateById('#ForeColor', 'Color')) {
    //    i++;
    //}

    if (i == 0)
        return true;
    else
        return false;

}

function onDeleteClick(GI, LogGI, Type, index) {

    debugger;

    if ($("#AttendanceStatusList_" + index + "__EditActive").val() == 'True') {


        swal.fire(ApprovalPendingMsg);

    }


    else {
        //var url = "/Master/AttendanceStatus/Delete?AttendanceStatusGI=" + GI + "&LogGI=" + LogGI;
        //$('#DeleteSlider').load(url, function () { });
        //$('body').append('<div class="form-overlay"></div>');


        $.ajax({
            type: 'GET',
            url: '/Master/AttendanceStatus/Delete',
            dataType: 'html',
            data: { AttendanceStatusGI: GI, LogGI: LogGI },
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
                url: '/Master/AttendanceStatus/Delete',
                dataType: 'json',
                data: $('#Attendance-Delete').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('delete');
                            var url = "/Master/AttendanceStatus/GetAttendanceStatusList?TabIndex=" + 'ACTIVE';
                            $('#Attendancestatuslist').load(url, function () { });
                            $(".ActiveTab").addClass("active");
                            $(".PendingTab").removeClass("active");
                            $("#Attendancestatuslist").removeClass("hide");
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/AttendanceStatus/Delete?AttendanceStatusGI=" + GI + "&LogGI=" + LogGI;
                            $('#DeleteSlider').load(url, function () { });
                            $('body').append('<div class="form-overlay"></div>');

                        });
                    }

                },
                //error: function (xhr, textStatus, errorThrown) {
                //    document.body.id = "kt_body";
                //    $('body').html(xhr.responseText);
                //}
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

    if ($("#AttendanceStatusList_" + index + "__MakerChecker").val() == 'True')
        swal.fire(MakerCheckerMsg);

    else {
        //var url = "/Master/AttendanceStatus/Approve?AttendanceStatusGI=" + GI + "&LogGI=" + LogGI;

        //$('#ApproveSlider').load(url, function () { });
        //$('body').append('<div class="form-overlay"></div>');
        $.ajax({

            type: 'GET',
            url: '/Master/AttendanceStatus/Approve',
            dataType: 'html',
            data: { AttendanceStatusGI: GI, LogGI: LogGI },
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
                url: '/Master/AttendanceStatus/Approve',
                dataType: 'json',
                data: $('#Approve-Attendance').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('ApproveDiv');
                            var url = "/Master/AttendanceStatus/GetAttendanceStatusList?TabIndex=" + 'APPROVAL_PENDING';
                            $('#Attendancestatuslist').load(url, function () { });
                            $(".PendingTab").addClass("active");
                            $(".ActiveTab").removeClass("active");
                            $("#Attendancestatuslist").removeClass("hide");
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/AttendanceStatus/Approve?AttendanceStatusGI=" + GI + "&LogGI=" + LogGI;
                            $('#ApproveSlider').load(url, function () { });
                            $('body').append('<div class="form-overlay"></div>');

                        });
                    }

                },
                //error: function (xhr, textStatus, errorThrown) {
                //    document.body.id = "kt_body";
                //    $('body').html(xhr.responseText);
                //}

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
                url: '/Master/AttendanceStatus/Reject',
                dataType: 'json',
                data: $('#Approve-Attendance').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('ApproveDiv');
                            var url = "/Master/AttendanceStatus/GetAttendanceStatusList?TabIndex=" + 'APPROVAL_PENDING';
                            $('#Attendancestatuslist').load(url, function () { });
                            $(".PendingTab").addClass("active");
                            $(".ActiveTab").removeClass("active");
                            $("#Attendancestatuslist").removeClass("hide");
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/AttendanceStatus/Reject?AttendanceStatusGI=" + GI + "&LogGI=" + LogGI;
                            $('#ApproveSlider').load(url, function () { });
                            $('body').append('<div class="form-overlay"></div>');

                        });
                    }

                },
                //error: function (xhr, textStatus, errorThrown) {
                //    document.body.id = "kt_body";
                //    $('body').html(xhr.responseText);
                //}

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
        var pendingTableLength = $('#AttendanceStatusList_Count').val();
        if (pendingTableLength > 0) {
            url = "/Master/AttendanceStatus/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }
    else {

        var activeTableLength = $('#AttendanceStatusList_Count').val();
        if (activeTableLength > 0) {
            url = "/Master/AttendanceStatus/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }


    }
    
    
}


function ActiveInactiveValidationAttendance(name, code) {
    debugger

    $.ajax({

        type: 'POST',
        url: '/Master/AttendanceStatus/GetActiveInactiveValidation?name=' + name + '&code=' + code,
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
                    $("#activeinactiveAtt").prop("checked", true);
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




