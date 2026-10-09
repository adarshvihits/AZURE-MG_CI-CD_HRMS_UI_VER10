var prevTab = 'ActiveTab';
var currentTab = 'ACTIVE';

function OnTabClick(Tab, TabIndex) {
    debugger;
    showSpinner();
    currentTab = TabIndex;
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");
    //var url = "/Master/Location/GetLocationList";

    //$('#LocationList').load(url, { TabIndex: TabIndex }, function () { hideSpinner();});

    $.ajax({

        type: 'GET',
        url: '/Master/Venue/GetVenueList',
        dataType: 'html',
        data: { TabIndex: TabIndex },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#VenueList').html("");
                $('#VenueList').html(result);
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
    //var url = "/Master/Location/GetLocationList";
    //$('#LocationList').load(url, { TabIndex: currentTab }, function () { hideSpinner(); });

    $.ajax({

        type: 'GET',
        url: '/Master/Venue/GetVenueList',
        dataType: 'html',
        data: { TabIndex: TabIndex },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#VenueList').html("");
                $('#VenueList').html(result);
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

    if ($("#VenueList_" + index + "__MakerChecker").val() == 'True')
        swal.fire(MakerCheckerMsg);

    //else {

    //    var url = "/Master/Location/Approve?LocationGI=" + GI + "&LogGI=" + LogGI;
    //    $('#ApproveSlider').load(url, function () { });
    //    $('body').append('<div class="form-overlay"></div>');
    //}
    else {

        $.ajax({
            type: 'GET',
            url: '/Master/Venue/Approve',
            dataType: 'html',
            data: { VenueGI: GI, LogGI: LogGI },
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

        if ($("#VenueList_" + index + "__EditActive").val() == 'True')
            swal.fire(ApprovalPendingMsg);



        else {
            $.ajax({

                type: 'GET',
                url: '/Master/Venue/Edit',
                dataType: 'html',
                data: { VenueGI: GI, LogGI: LogGI },
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
        $.ajax({

            type: 'GET',
            url: '/Master/Venue/Create',
            dataType: 'html',
            data: { VenueGI: GI, LogGI: LogGI },
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

    if ($("#VenueList_" + index + "__EditActive").val() == 'True')
        swal.fire(ApprovalPendingMsg);



    else {

        $.ajax({
            type: 'GET',
            url: '/Master/Venue/Delete',
            dataType: 'html',
            data: { VenueGI: GI, LogGI: LogGI },
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
                url: '/Master/Venue/Approve1',
                dataType: 'json',
                data: $('#approve').serialize(),
                success: function (result) {

                    if (result.success == true) {
                        debugger
                        swal.fire(result.message).then(function () {
                            CloseSlider('ApproveDiv');
                            var url = "/Master/Venue/GetVenueList?TabIndex=" + 'APPROVAL_PENDING';
                            debugger

                            $('#VenueList').load(url, function () { });
                            $(".PendingTab").addClass("active");
                            $(".ActiveTab").removeClass("active");
                            $("#VenueList").removeClass("hide");
                        });

                    }

                    else {
                        debugger;
                        swal.fire(result.message).then(function () {
                            var url = "/Master/Venue/Approve?VenueGI=" + GI + "&LogGI=" + LogGI;
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
                url: '/Master/Venue/Delete',
                dataType: 'json',
                data: $('#VenueDelete').serialize(),
                success: function (result) {
                    debugger
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('Delete');
                            var url = "/Master/Venue/GetVenueList?TabIndex=" + 'ACTIVE';
                            $('#VenueList').load(url, function () { });
                            $(".PendingTab").removeClass("active");
                            $(".ActiveTab").addClass("active");
                            $("#VenueList").removeClass("hide");
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/Venue/Delete?VenueGI=" + GI + "&LogGI=" + LogGI;
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
                url: '/Master/Venue/Reject',
                dataType: 'json',
                data: $('#approve').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('ApproveDiv');
                            var url = "/Master/Venue/GetVenueList?TabIndex=" + 'APPROVAL_PENDING';
                            $('#VenueList').load(url, function () { });
                            $(".PendingTab").addClass("active");
                            $(".ActiveTab").removeClass("active");
                            $("#VenueList").removeClass("hide");
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/Venue/Approve?VenueGI=" + GI + "&LogGI=" + LogGI;
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
function SaveVenue(mode) {
    debugger;

    if (validateVenue()) {
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
                    url: '/Master/Venue/Save',
                    dataType: 'json',
                    data: $('#VenueSave').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                CloseSlider('AddNewOrEdit'); $('.AddNewOrEdit').removeClass('Venue-slidernew')
                                var url = "/Master/Venue/GetVenueList?TabIndex=" + currentTab;
                                $('#VenueList').load(url, function () { });


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
        var pendingTableLength = $('#VenueList_Count').val();
        if (pendingTableLength > 0) {
            url = "/Master/Venue/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }

    else if (currentTab == 'ACTIVE') {
        var activeTableLength = $('#VenueList_Count').val();
        if (activeTableLength > 0) {
            url = "/Master/Venue/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }
    else if (currentTab == 'INACTIVE') {
        var inActiveTableLength = $('#VenueList_Count').val();
        if (inActiveTableLength > 0) {
            url = "/Master/Venue/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }
}


function validateVenue() {
    var i = 0;

    if (!validateById('#Region_Code', 'LocRegion')) {
        document.getElementById("Region_Code").focus();
        i++;
    }
    if (!validateById('#AreaName', 'LocArea')) {
        document.getElementById("AreaName").focus();
        i++;
    }
    if (!validateById('#Street', 'LocSreet')) {
        document.getElementById("Street").focus();
        i++;
    }
    if (!validateById('#DoorNumAndBuilding', 'LocDoorNumBuilding')) {
        document.getElementById("DoorNumAndBuilding").focus();
        i++;
    }
    if (!validateById('#ShortName', 'locShortName')) {
        document.getElementById("ShortName").focus();
        i++;
    }
    if (!validateById('#Name', 'VenueName')) {
        document.getElementById("Name").focus();
        i++;
    }

    // optional fields, checked only when filled
    var email = $('#EmailId').val().trim();
    if (email !== "" && !validateEmail(document.getElementById('EmailId'), 'MailId')) {
        i++;
    }
  
    return i == 0;
}

function validateEmail(val, id) {
    debugger;
    let email = val.value.trim();
    val.value = email;

    // Basic empty check
    if (email === "") {
        $("#" + id).addClass('error-message');
        $(val).addClass('is-invalid');
        $("#" + id).html("This field is required");
        return false;
    }
    else {
        // More robust email regex
        let filter = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;

        if (!filter.test(email)) {
            $("#" + id).addClass('error-message');
            $(val).addClass('is-invalid');
            $("#" + id).html('Invalid Email ID');
            return false;
        } else {
            $("#" + id).removeClass('error-message');
            $(val).removeClass('is-invalid');
            $("#" + id).html("");
            return true;
        }
    }
}




function isValidURL(val, id) {

    debugger
    var web = document.getElementById('WebAdderss').value;
    /*   var res = web.match(/(http(s)?:\/\/.)?(www\.)?[-a-zA-Z0-9@:%._\+~#=]{2,256}\.[a-z]{2,6}\b([-a-zA-Z0-9@:%_\+.~#?&//=]*)/g);*/
    var res = web.match(/(http[s]?:\/\/)(www[\w]?\.)[\w]*\.[\w]{2,3}(\.[\w]{2})?$/);


    //if (web== "") {
    //    $("#" + id + "").addClass('error-message');
    //    $(val).addClass('is-invalid');
    //    $("#" + id + "").html("This field is required");
    //}

    if (res == null) {
        debugger;
        $("#" + id + "").addClass('error-message');
        $(val).addClass('is-invalid');

        $("#" + id + "").html('Invalid Web Address')
        return false;
    }
    else {
        $("#" + id + "").removeClass('error-message');
        $(val).removeClass('is-invalid');
        $("#" + id + "").html('')
        return true;
    }


}


function CopypasteValidation(input) {
    debugger;
    let value = input.value;
    let numbers = value.replace(/[^0-9]/g, "");
    input.value = numbers;
}

function ActiveInactiveValidation(name, code) {
    debugger

    $.ajax({

        type: 'POST',
        url: '/Master/Venue/GetActiveInactiveValidation?name=' + name + '&code=' + code,
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
                    $("#activeInactiveVen").prop("checked", true);
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










