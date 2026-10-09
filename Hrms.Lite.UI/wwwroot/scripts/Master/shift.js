
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
    "#74bbfb": "Light-Azure", "#7FFFD4": "Organic-Brown", "#c8f3cd": "Creamy-Mint", "#7FFFD4":"Aquamarine"
}


                                    
function OnTabClick(Tab, TabIndex) {
    debugger;
    showSpinner();
    currentTab = TabIndex;
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");
   /* var url = "/Master/Shift/GetShiftList";*/

   /* $('#ShiftList').load(url, { TabIndex: TabIndex }, function () { hideSpinner(); });*/

    $.ajax({

        type: 'GET',
        url: '/Master/Shift/GetShiftList',
        dataType: 'html',
        data: { TabIndex: TabIndex },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#ShiftList').html("");
                $('#ShiftList').html(result);
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
    //var url = "/Master/Shift/GetShiftList";
    //$('#ShiftList').load(url, { TabIndex: currentTab }, function () { hideSpinner(); });

    $.ajax({

        type: 'GET',
        url: '/Master/Shift/GetShiftList',
        dataType: 'html',
        data: { TabIndex: currentTab },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#ShiftList').html("");
                $('#ShiftList').html(result);
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

function onAddNewOrEditClick(Type, GI, LogGI, index) {
    debugger;
    //currentTab = 'ACTIVE';

    if (Type == 'MODIFY') {

        if ($("#ShiftList_" + index + "__EditActive").val() == 'True')
            swal.fire(ApprovalPendingMsg);


        else {
            //var url = "/Master/Shift/Edit?ShiftGI=" + GI + "&LogGI=" + LogGI;

            //$('#AddNewOrEditSlider').load(url, function () { });
            //$('body').append('<div class="form-overlay"></div>');

            $.ajax({

                type: 'GET',
                url: '/Master/Shift/Edit',
                dataType: 'html',
                data: { ShiftGI: GI, LogGI: LogGI },
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
        //var url = "/Master/Shift/Create";
        //$('#AddNewOrEditSlider').load(url, function () { });
        //$('body').append('<div class="form-overlay"></div>');



        $.ajax({

            type: 'GET',
            url: '/Master/Shift/Create',
            dataType: 'html',
            data: { ShiftGI: GI, LogGI: LogGI },
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


function onDeleteClick(GI, LogGI, Type, index) {

    debugger;

    if ($("#ShiftList_" + index + "__EditActive").val() == 'True')
        swal.fire(ApprovalPendingMsg);


    else {
        //var url = "/Master/Shift/Delete?ShiftGI=" + GI + "&LogGI=" + LogGI;
        //$('#DeleteSlider').load(url, function () { });
        //$('body').append('<div class="form-overlay"></div>');



        $.ajax({
            type: 'GET',
            url: '/Master/Shift/Delete',
            dataType: 'html',
            data: { ShiftGI: GI, LogGI: LogGI },
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




function onApproveClick(GI, LogGI, Type, index) {

    debugger;

    if ($("#ShiftList_" + index + "__MakerChecker").val() == 'True')
        swal.fire(MakerCheckerMsg);

    else {

        debugger;
        //var url = "/Master/Shift/Approve?ShiftGI=" + GI + "&LogGI=" + LogGI;
        //$('#ApproveSlider').load(url, function () { });
        //$('body').append('<div class="form-overlay"></div>');

        $.ajax({

            type: 'GET',
            url: '/Master/Shift/Approve',
            dataType: 'html',
            data: { ShiftGI: GI, LogGI: LogGI },
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

function getBreakHour(thisid, spanId) {
    debugger;
    var startTime = $('#BreakHourStartTime').val();
    var endTime = $('#BreakHourEndTime').val();
    var shiftStartTime = $('#ShiftStartTime').val();
    var shiftendTime = $('#ShiftEndTime').val();
    /* var breakTime = $('#BreakHour').val();*/

    var todayDate = moment(new Date(), 'YYYY-MMM-DD'); //Instead of today date, We can pass whatever date
    todayDate = _GetFormattedDate(todayDate);
    var startDate = new Date(`${todayDate} ${startTime}`);
    var endDate = new Date(`${todayDate} ${endTime}`);
    var shiftstartDate = new Date(`${todayDate} ${shiftStartTime}`);
    var shiftendDate = new Date(`${todayDate} ${shiftendTime}`);
    var timeDiff = Math.abs(startDate.getTime() - endDate.getTime());
    //if (shiftstartDate <= shiftendDate) {
    //    $('#ShiftStartTime').removeClass('is-invalid');
    //    $("#SStartTime").removeClass('error-message');
    //    $("#SStartTime").html("");
    //    $('#ShiftEndTime').removeClass('is-invalid');
    //    $("#SEndTime").removeClass('error-message');
    //    $("#SEndTime").html("");
    //}
    //else {
    //    $(thisid).addClass('is-invalid');
    //    if (spanId == "SStartTime") {
    //        $("#" + spanId + "").addClass('error-message');
    //        $("#" + spanId + "").html("Shift start Time must be less than Shift End Time");
    //    }
    //    else if (spanId == "SEndTime") {
    //        $("#" + spanId + "").addClass('error-message');
    //        $("#" + spanId + "").html("Shift End Time must be greater than or equal to Shift Start Time");
    //    }
    //    return false;
    //}
    if (startDate <= endDate) {
        var hh = Math.floor(timeDiff / 1000 / 60 / 60);
        hh = ('0' + hh).slice(-2)
        timeDiff -= hh * 1000 * 60 * 60;
        var mm = Math.floor(timeDiff / 1000 / 60);
        mm = ('0' + mm).slice(-2)
        //var brkHr = hh + ": " + mm;

        $('#BreakHour').val(hh + ": " + mm);
        $('#BreakHourStartTime').removeClass('is-invalid');
        $("#BreakstartTime").removeClass('error-message');
        $("#BreakstartTime").html("");
        $('#BreakHourEndTime').removeClass('is-invalid');
        $("#BreakEndTime").removeClass('error-message');
        $("#BreakEndTime").html("");
        getScheduledHour();        
    }
    else {
        $(thisid).addClass('is-invalid');
        if (spanId == "BreakstartTime") {
            $("#" + spanId + "").addClass('error-message');
            $("#" + spanId + "").html("Break start Time must be less than Break End Time");
        }
        else if (spanId == "BreakEndTime") {
            $("#" + spanId + "").addClass('error-message');
            $("#" + spanId + "").html("Break End Time must be greater than or equal to Break Start Time");
        }
        return false;
    }
}

//function getScheduledHour() {
//    debugger;
//   var startTime = $('#ShiftStartTime').val();
//    var endTime = $('#ShiftEndTime').val();    
//    var totalHrs = 0;
//    if ((timeStringToFloat(startTime) >= 12.00 && timeStringToFloat(endTime) < 12.00)) {
        
//        var diff = timeStringToFloat("24:00") - timeStringToFloat(startTime);
//        var totTime = diff + timeStringToFloat(endTime);
//        totalHrs = totTime;
//        var MEnd = 0;
//        var MStart = 0;
//        if (parseFloat(endTime.split(':')[1]) != 0)
//            MEnd = 60 - parseFloat(endTime.split(':')[1]);
//        //if (parseFloat(startTime.split(':')[1]) != 0)
//        //    MStart = 60 - parseFloat(startTime.split(':')[1]);
//        var FMin = Math.abs(parseFloat(endTime.split(':')[1]) + parseFloat(startTime.split(':')[1])) / 100;
//        if (FMin >= 0.60)
//            FMin = 1 + (FMin - 0.60);
//        totalHrs = parseFloat(totalHrs.toString().split('.')[0]) + FMin
//        if (FMin >= 0.60)
//            totalHrs = totalHrs - 1;
//    }
//    else {
//        var shiftDiff = timeStringToFloat(endTime) - timeStringToFloat(startTime);
//        var MEnd = 0;
//        var MStart = 0;
//        if (parseFloat(endTime.split(':')[1]) != 0)
//            MEnd = 60 - parseFloat(endTime.split(':')[1]);      
//        if (parseFloat(startTime.split(':')[1]) != 0)
//            MStart = 60 - parseFloat(startTime.split(':')[1]);
//        if (MEnd == 0 || (MEnd != 0 && MStart !=0))
//            var FMin = Math.abs(MEnd - MStart) / 100;
//        else
//            var FMin = Math.abs(parseFloat(endTime.split(':')[1]) + parseFloat(startTime.split(':')[1])) / 100;
//        if (FMin >= 0.60)
//            FMin = 1 + (FMin - 0.60);
//        totalHrs = parseFloat(shiftDiff.toString().split('.')[0]) + FMin;
//        if (FMin >= 0.60)
//            totalHrs = totalHrs - 1;

//        totalHrs = Math.abs(totalHrs);
       
//    }
//    var t = 0.0;
//    t = totalHrs;
//    if (t.toString().split('.')[0].length == 1)
//        totalHrs = "0" + totalHrs.toString();
//    if (t.toString().split('.').length > 1) {
//        if (t.toString().split('.')[1].length == 1)
//            totalHrs = totalHrs.toString() + "0";
//    }
//    if (isNaN(totalHrs))
//        totalHrs = 0;
//    $('#ScheduledHour').val(totalHrs);  
//}


function getScheduledHour() {
    debugger;
    var startTime = $('#ShiftStartTime').val();
    var endTime = $('#ShiftEndTime').val();

    if (!startTime || !endTime) {
        $('#ScheduledHour').val("00:00");
        return;
    }

    function timeStringToFloat(time) {
        var parts = time.split(':');
        return parseInt(parts[0], 10) + parseInt(parts[1], 10) / 60;
    }

    var startFloat = timeStringToFloat(startTime);
    var endFloat = timeStringToFloat(endTime);
    var totalHrs = 0;

    if (startFloat >= 12.00 && endFloat < 12.00) {
        totalHrs = (24 - startFloat) + endFloat;
    } else {
        totalHrs = endFloat - startFloat;
    }

    function getMinutes(time) {
        return parseInt(time.split(':')[1], 10);
    }

    var startMinutes = getMinutes(startTime);
    var endMinutes = getMinutes(endTime);

    var totalMinutes = (totalHrs * 60) + (endMinutes - startMinutes);
    if (totalMinutes < 0) totalMinutes += 1440; // Handle negative values (e.g., when crossing midnight)

    var hours = Math.floor(totalMinutes / 60);
    var minutes = Math.abs(totalMinutes % 60);

    var formattedHours = hours.toString().padStart(2, '0');
    var formattedMinutes = minutes.toString().padStart(2, '0');

    var formattedTime = `${formattedHours}:${formattedMinutes}`;

    $('#ScheduledHour').val(formattedTime);
}

function timeStringToFloat(time) {
    var hoursMinutes = time.split(/[.:]/);
    var hours = parseInt(hoursMinutes[0], 10);
    var minutes = hoursMinutes[1] ? parseInt(hoursMinutes[1], 10) : 0;
    return hours + minutes  / 60;
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
                url: '/Master/Shift/Reject',
                dataType: 'json',
                data: $('#ApproveShift').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('ApproveDiv');
                            var url = "/Master/Shift/GetShiftList?TabIndex=" + 'APPROVAL_PENDING';
                            $('#ShiftList').load(url, function () { });
                            $(".PendingTab").addClass("active");
                            $(".ActiveTab").removeClass("active");
                            $("#ShiftList").removeClass("hide");
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/Shift/Approve?ShiftGI=" + GI + "&LogGI=" + LogGI;
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
                url: '/Master/Shift/Delete',
                dataType: 'json',
                data: $('#deleteShift').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('delete');
                            var url = "/Master/Shift/GetShiftList?TabIndex=" + 'ACTIVE';
                            $('#ShiftList').load(url, function () { });
                            $(".PendingTab").removeClass("active");
                            $(".ActiveTab").addClass("active");
                            $("#ShiftList").removeClass("hide");
                        });

                    }
                    else {
                        debugger
                        swal.fire(result.message).then(function () {
                            var url = "/Master/Shift/Delete?ShiftGI=" + GI + "&LogGI=" + LogGI;
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
                url: '/Master/Shift/Approve',
                dataType: 'json',
                data: $('#ApproveShift').serialize(),
                success: function (result) {

                    if (result.success == true) {
                        debugger
                        swal.fire(result.message).then(function () {
                            CloseSlider('ApproveDiv');
                            var url = "/Master/Shift/GetShiftList?TabIndex=" + 'APPROVAL_PENDING';
                            debugger
              
                            $('#ShiftList').load(url, function () { });
                            $(".PendingTab").addClass("active");
                            $(".ActiveTab").removeClass("active");
                            $("#ShiftList").removeClass("hide");
                        });

                    }

                    else {
                        debugger;
                        swal.fire(result.message).then(function () {
                            var url = "/Master/Shift/Approve?ShiftGI=" + GI + "&LogGI=" + LogGI;
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






function SaveShiftMaster(mode) {
    debugger;
    if (validateShift()) {
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
                    url: '/Master/Shift/Save',
                    dataType: 'json',
                    data: $('#shiftsave').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                CloseSlider('AddNewDiv');
                                var url = "/Master/Shift/GetShiftList?TabIndex=" + currentTab;
                                $('#ShiftList').load(url, function () { });

                                //$(".PendingTab").removeClass("active");
                                //$(".ActiveTab").addClass("active");
                                //$("#ShiftList").removeClass("hide");

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
        var pendingTableLength = $('#ShiftList_Count').val();
        if (pendingTableLength > 0) {
            url = "/Master/Shift/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }

    else if (currentTab == 'ACTIVE') {
        var activeTableLength = $('#ShiftList_Count').val();
        if (activeTableLength > 0) {
            url = "/Master/Shift/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }

        else {
            swal.fire(NoDataFoundMsg);
        }
    }
    else if (currentTab == 'INACTIVE') {
        var inActiveTableLength = $('#ShiftList_Count').val();
        if (inActiveTableLength > 0) {
            url = "/Master/Shift/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }

    }

}


function validateShift() {
    debugger;
    var i = 0;

    if (!validateById('#Name', 'ShiftName')) {
        i++;
    }

    if (!validateById('#ShortName', 'ShiftShtName')) {
        i++;
    }
    if (!validateById('#ShiftCode', 'ShCode')) {
        i++;
    }
    if (!validateById('#ShiftCategory_Code', 'ShiftType')) {
        i++;
    }
    if (!validateById('#FullDayHour', 'FullDayHr')) {
        i++;
    }

    if (!validateById('#HalfDayHour', 'HalfDayHr')) {
        i++;
    }
    if (!validateById('#HPOPFullDayHour', 'HPOPFullDayHr')) {
        i++;
    }

    if (!validateById('#HPOPHalfDayHour', 'HPOPHalfDayHr')) {
        i++;
    }
    if (!validateById('#ShiftStartTime', 'SStartTime')) {
        i++;
    }
    if (!validateById('#ShiftEndTime', 'SEndTime')) {
        i++;
    }
    if (!validateById('#BreakHourStartTime', 'BreakstartTime')) {
        i++;
    }
    if (!validateById('#BreakHourEndTime', 'BreakEndTime')) {
        i++;
    }
    //if (!validateById('#BreakHour', 'BreakTime')) {
    //    i++;
    //}
    //if (!validateById('#ScheduledHour', 'Scheduledtime')) {
    //    i++;
    //}
    if (!validateById('#FullDayHour', 'FullDayHr')) {
        i++;
    }
    if (!validateById('#HalfDayHour', 'HalfDayHr')) {
        i++;
    }
    if (!validateById('#HalfDayHour', 'HalfDayHr')) {
        i++;
    };;;;;
    if (!validateById('#ShiftCategory_Code', 'ShiftType')) {
        i++;
    }
    if (!validateById('#DutyRosterBackgroundColor_Name', 'BgName')) {
        i++;
    }
    if (!validateById('#DutyRosterFontColor_Name', 'FName')) {
        i++;
    }
    if (!validateById('#BreakHourStartTime', 'BreakstartTime')) {
        i++;
    }
    if (!validateById('#BreakHourEndTime', 'BreakEndTime')) {
        i++;
    }
    if (!validateById('#ShiftStartTime', 'SStartTime')) {
        i++;
    }
    if (!validateById('#ShiftEndTime', 'SEndTime')) {
        i++;
    }
    if (i == 0)
        return true;
    else
        return false;
}

function ActiveInactiveValidationShift(name, code) {
    debugger

    $.ajax({

        type: 'POST',
        url: '/Master/Shift/GetActiveInactiveValidation?name=' + name + '&code=' + code,
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
                    $("#shiftvalidation").prop("checked", true);
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




