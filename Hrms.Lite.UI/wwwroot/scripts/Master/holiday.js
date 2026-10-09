var prevTab = 'ActiveTab';
var currentTab = 'ACTIVE';

function onTabClick(Tab, TabIndex) {
    debugger;
    showSpinner();
    currentTab = TabIndex;
    let yearcode = $("#Year_Code").val();
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");
    if (currentTab == "APPROVAL_PENDING")
    {
        $("#drpdownholiday").addClass("hide");
    }
    else
        $("#drpdownholiday").removeClass("hide");
    $('#tabsDropdownHoliday').val(currentTab);

    $.ajax({

        type: 'GET',
        url: '/Master/Holiday/GetHolidayList',
        dataType: 'html',
        data: { TabIndex: TabIndex, yearcode: yearcode },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#HolidayList').html("");
                $('#HolidayList').html(result);
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
        url: '/Master/Holiday/GetHolidayList',
        dataType: 'html',
        data: { TabIndex: currentTab },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#HolidayList').html("");
                $('#HolidayList').html(result);
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
//function YearClick(val) {
//    debugger;
//    showSpinner();
//    yearcode = $("#Year_Code").val();

//    currentTab = $(val).val();
///*    $("." + prevTab).removeClass("active");*/
//    if (currentTab == "ACTIVE")
//        TabIndex = 'Active'
//    else if (currentTab == "INACTIVE")
//        TabIndex = 'Inactive';
//    else
//        TabIndex = 'Pending';

///*    $("." + TabIndex).addClass("active");*/

//    $.ajax({

//        type: 'GET',
//        url: '/Master/Holiday/GetHolidayList',
//        dataType: 'html',
//        data: { yearcode: yearcode, TabIndex: TabIndex },

//        success: function (result) {
//            debugger
//            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
//                window.location.reload();
//            }
//            else {
//                $('#HolidayList').html("");
//                $('#HolidayList').html(result);
//            }
//            hideSpinner();


//        },


//        error: function (xhr, textStatus, errorThrown) {
//            debugger
//            document.body.id = "kt_body";
//            $('body').html(xhr.responseText);
//            hideSpinner();

//        }
//    });

//}




function YearClick() {
    debugger;
    showSpinner();

    let yearcode = $("#Year_Code").val();// Get the selected year code
    let tabIndex = currentTab; // Use the stored global TabIndex

    $.ajax({
        type: 'GET',
        url: '/Master/Holiday/GetHolidayList',
        dataType: 'html',
        data: { yearcode: yearcode, TabIndex: tabIndex }, // Pass both yearcode and TabIndex
        success: function (result) {
            debugger;
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            } else {
                $('#HolidayList').html("");
                $('#HolidayList').html(result);
            }
            hideSpinner();
        },
        error: function (xhr, textStatus, errorThrown) {
            debugger;
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
            hideSpinner();
        }
    });
}

function validateHolidayGrp(val, id, divId) {
    debugger;
    if (val.value == "") {
        $('.' + divId).addClass('is-invalid');
        $("#" + id + "").addClass('error-message');
        $("#" + id + "").html("This field is required");
        return false;
    }
    else {
        $('.' + divId).removeClass('is-invalid');
        $("#" + id + "").removeClass('error-message');
        $("#" + id + "").html("");
        return true;
    }
}


function onAddNewOrEditClick(Type, GI, LogGI, index) {
    debugger;
    //currentTab = 'ACTIVE';

    if (Type == 'MODIFY') {

        if ($("#HolidayList_" + index + "__EditActive").val() == 'True')
            swal.fire(ApprovalPendingMsg);


        else {


            $.ajax({

                type: 'GET',
                url: '/Master/Holiday/Edit',
                dataType: 'html',
                data: { HolidayGI: GI, LogGI: LogGI },
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

        var url = "/Master/Holiday/Create";
        $.ajax({

            type: 'GET',
            url: '/Master/Holiday/Create',
            dataType: 'html',
            data: { HolidayGI: GI, LogGI: LogGI },
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

    if ($("#HolidayList_" + index + "__EditActive").val() == 'True')
        swal.fire(ApprovalPendingMsg);


    else {

        $.ajax({
            type: 'GET',
            url: '/Master/Holiday/Delete',
            dataType: 'html',
            data: { HolidayGI: GI, LogGI: LogGI },
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

    if ($("#HolidayList_" + index + "__MakerChecker").val() == 'True')
        swal.fire(MakerCheckerMsg);

    else {



        $.ajax({

            type: 'GET',
            url: '/Master/Holiday/Approve',
            dataType: 'html',
            data: { HolidayGI: GI, LogGI: LogGI },
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



function SaveHoliday(mode) {
    debugger;
    var TabIndex;
    let yearcode = $("#Year_Code").val();
    if (validateHoliday()) {
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
                    url: '/Master/Holiday/Save',
                    dataType: 'json',
                    data: $('#hollidaySave').serialize(),
                    success: function (result) {
                        debugger;
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                CloseSlider('AddNewDiv');
                                var url = "/Master/Holiday/GetHolidayList?TabIndex=" + currentTab + "&yearcode=" + yearcode;
                                $('#HolidayList').load(url, function () { });

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



function confirmApprove(GI, LogGI) {
    debugger
    let yearcode = $("#Year_Code").val();
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
                url: '/Master/Holiday/Approve',
                dataType: 'json',
                data: $('#HolidayApprove').serialize(),
                success: function (result) {

                    if (result.success == true) {
                        debugger
                        swal.fire(result.message).then(function () {
                            CloseSlider('ApproveDiv');
                            var url = "/Master/Holiday/GetHolidayList?TabIndex=" + 'APPROVAL_PENDING' + "&yearcode=" + yearcode;
                            debugger

                            $('#HolidayList').load(url, function () { });
                            $(".PendingTab").addClass("active");
                            $(".ActiveTab").removeClass("active");
                            $("#HolidayList").removeClass("hide");
                        });

                    }

                    else {
                        debugger;
                        swal.fire(result.message).then(function () {
                            var url = "/Master/Holiday/Approve?HolidayGI=" + GI + "&LogGI=" + LogGI;
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
    let yearcode = $("#Year_Code").val();
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
                url: '/Master/Holiday/Reject',
                dataType: 'json',
                data: $('#HolidayApprove').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('ApproveDiv');
                            var url = "/Master/Holiday/GetHolidayList?TabIndex=" + 'APPROVAL_PENDING' + "&yearcode=" + yearcode;
                            $('#HolidayList').load(url, function () { });
                            $(".PendingTab").addClass("active");
                            $(".ActiveTab").removeClass("active");
                            $("#HolidayList").removeClass("hide");
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/Holiday/Approve?HolidayGI=" + GI + "&LogGI=" + LogGI;
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





function ConfirmDelete(GI, LogGI) {
    debugger
    let yearcode = $("#Year_Code").val();
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
                url: '/Master/Holiday/Delete',
                dataType: 'json',
                data: $('#DeleteHoliday').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('Delete');
                            var url = "/Master/Holiday/GetHolidayList?TabIndex=" + 'ACTIVE' + "&yearcode=" + yearcode;
                            $('#HolidayList').load(url, function () { });
                            //$(".PendingTab").removeClass("active");
                            //$(".ActiveTab").addClass("active");
                            //$("#HolidayList").removeClass("hide");
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/Holiday/Delete?HolidayGI=" + GI + "&LogGI=" + LogGI;
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
    let yearcode = $("#Year_Code").val();
    if (currentTab == 'APPROVAL_PENDING') {
        var pendingTableLength = $('#HolidayList_Count').val();
        if (pendingTableLength > 0) {
            url = "/Master/Holiday/ExportToExcel?TabIndex=" + currentTab + "&yearcode=" + yearcode;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }

    else if (currentTab == 'ACTIVE') {
        var activeTableLength = $('#HolidayList_Count').val();
        if (activeTableLength > 0) {
            url = "/Master/Holiday/ExportToExcel?TabIndex=" + currentTab + "&yearcode=" + yearcode;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }
    else if (currentTab == 'INACTIVE') {
        var inActiveTableLength = $('#HolidayList_Count').val();
        if (inActiveTableLength > 0) {
            url = "/Master/Holiday/ExportToExcel?TabIndex=" + currentTab + "&yearcode=" + yearcode;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }
}

function validateHoliday() {
    debugger;
    var i = 0;
    if (!validateById('#Year_Code', 'year')) {
        i++;
    }
    if (!validateById('#HolidayType_Code', 'HType')) {
        i++;
    }
    //if (!validateById('#HolidayDate', 'HDate')) {
    //    i++;
    //}
    if (!validateById('.holidaydate', 'HDate')) {

        i++;
    }
    if (!validateById('#HolidaySession_Code', 'HSession')) {
        i++;
    }
    if (!validateHolidayGrp(document.getElementById('HolidayGroups'), 'HGroup', 'HolidayGrp')) {
        i++;
    }
    if (!validateById('#HolidayDescription', 'HDescription')) {
        i++;
    }
    //if (!validateById('#HolidayGroup_Code', 'HGroup')) {
    //    i++;
    //}
    if (i == 0)
        return true;
    else
        return false;
}




//function validateHoliday() {

//    if (!validateById('#Year_Code', 'year')) {
//        return false;
//    }
//    else if (!validateById('#HolidayType_Code', 'HType')) {
//        return false;
//    }

//    else if (!validateById('#HolidayDate', 'HDate')) {
//        return false;
//    }
//    else if (!validateById('#HolidaySession_Code', 'HSession')) {
//        return false;
//    }


//    else {
//        return true;
//    }
//}
