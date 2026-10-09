

var prevTab = 'ActiveTab';
var currentTab = 'ACTIVE';


function OnTabClick(Tab, TabIndex) {
    debugger;
    /*showSpinner();*/
    currentTab = TabIndex;
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");
    //var url = "/Master/KeyPosition/GetKeyPositionList";
    /*  $('#tabsDropdownAllowance').val(currentTab);*/
    //$('#keyPositionListID').load(url, { TabIndex: TabIndex }, function () { /*hideSpinner();*/ });
    $.ajax({

        type: 'GET',
        url: '/Master/KeyPosition/GetKeyPositionList',
        dataType: 'html',
        data: { TabIndex: TabIndex },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#keyPositionListID').html("");
                $('#keyPositionListID').html(result);
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
function onAddOrEditClick(Type, LogGI, GI, index) {
    debugger;

  
    //currentTab = 'ACTIVE';
    
    if (Type == 'MODIFY') {

        if ($("#KeyPositionList_" + index + "__EditActive").val() == 'True')
            swal.fire(ApprovalPendingMsg);


        else {
            //var url = "/Master/KeyPosition/Edit?LogGI=" + LogGI + "&KeyPositionGI=" + GI;
          
            //$('#AddNewOrEditSlider').load(url, function () { });

            $.ajax({

                type: 'GET',
                url: '/Master/KeyPosition/Edit',
                dataType: 'html',
                data: { LogGI: LogGI, KeyPositionGI: GI },
                success: function (result) {
                    debugger
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }
                    else {
                        $('body').append('<div class="form-overlay"></div>');
                        $('#AddNewOrEditSlider').html("");
                        $('#AddNewOrEditSlider').html(result);
                        const val = $('#YSAboveOrBelow_Code').val();

                        if (val == 0) {

                            $('#YOSFrom').addClass('hide');
                        }
                        if (val == 3) {

                            $('#YOSTo').removeClass('hide');


                        }
                        else {
                            $('#YOSTo').addClass('hide');


                        }

                        const val1 = $('#AgeAboveOrBelow_Code').val();

                        if (val1 == 0) {

                            $('#AgeFrom').addClass('hide');
                        }
                        if (val1 == 3) {

                            $('#AgeTo').removeClass('hide');


                        }
                        else {
                            $('#AgeTo').addClass('hide');


                        }
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
        //var url = "/Master/KeyPosition/Create";
        //$('#AddNewOrEditSlider').load(url, function () { });
        /*$('body').append('<div class="form-overlay"></div>');*/
        $.ajax({

            type: 'GET',
            url: '/Master/KeyPosition/Create',
            dataType: 'html',
            data: { LogGI: GI, LogGI: LogGI },
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
function onGenderChange(id) {
    debugger;
    var rr = [];
    $('#Genders :selected').each(function (i, selected) {
        rr[i] = $(selected).text();
    });
    //alert(rr);
    GenderText = $('#Genders option:selected').text();
    $('#Gender.Name').val(rr);
    if (!validateById('#Genders', id)) {
        false;
    }
    else {
        true;
    }
}
function ValidateKeyPosition() {
    debugger
    var i = 0;
    if (!validateById('#Name', 'KeyName')) {
        i++;
    }
    if (!validateById('#ShortName', 'KeyshtName')) {
        i++;
    }
    if (!validateById('#KeyPositionCodeDesc', 'KeyCod')) {
        i++;
    }
    if (!validateById('#Designation_Code', 'desDD')) {
        i++;
    }


    //if (!validateById('#YSAboveOrBelow_Code', 'yrsSer')) {
    //    i++;
    //}

    //if (!validateById('#AgeAboveOrBelow_Code', 'AgeKey')) {
    //    i++;
    //}

    if (!validateById('#Genders', 'Gend')) {
        i++;
    }

    if (!validateById('#KeyPositionRisk_Code', 'Risk')) {
        i++;
    }

    if (i == 0)
        return true;
    else
        return false;
    return true;
}
function SaveKeyPosition(mode) {
    debugger;
    var TabIndex;
    if (ValidateKeyPosition()) {
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
                    url: '/Master/KeyPosition/Save',
                    dataType: 'json',
                    data: $('#KeyPositionSave').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                CloseSlider('AddNewDiv');
                                debugger;
                                var url = "/Master/KeyPosition/GetKeyPositionList?TabIndex=" + currentTab;
                               
                                $('#keyPositionListID').load(url,  function () { /*hideSpinner();*/ });

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

    if ($("#KeyPositionList_" + index + "__MakerChecker").val() == 'True')
        swal.fire(MakerCheckerMsg);

    else {

        //var url = "/Master/KeyPosition/Approve?KeyPositionGI=" + GI + "&LogGI=" + LogGI;
        //$('#ApproveSlider').load(url, function () { });
        /*$('body').append('<div class="form-overlay"></div>');*/
        $.ajax({

            type: 'GET',
            url: '/Master/KeyPosition/Approve',
            dataType: 'html',
            data: { KeyPositionGI: GI, LogGI: LogGI },
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
                url: '/Master/KeyPosition/Approve',
                dataType: 'json',
                data: $('#approveKeyPosition').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('ApproveDiv');
                            var url ="/Master/KeyPosition/GetKeyPositionList?TabIndex=" + 'APPROVAL_PENDING';
                            $('#keyPositionListID').load(url, function () { });
                            $(".PendingTab").addClass("active");
                            $(".ActiveTab").removeClass("active");
                            $("#keyPositionListID").removeClass("hide");
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/KeyPosition/Approve?KeyPositionGI=" + GI + "&LogGI=" + LogGI;
                            $('#ApproveDiv').load(url, function () { });
                            /*$('body').append('<div class="form-overlay"></div>');*/

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





function ActiveInactiveValidation(name, code) {
    debugger

    $.ajax({

        type: 'POST',
        url: '/Master/KeyPosition/GetActiveInactiveValidation?name=' + name + '&code=' + code,
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
                    $("#Activeinactivekeyposition").prop("checked", true);
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
                url: '/Master/KeyPosition/Reject',
                dataType: 'json',
                data: $('#approveKeyPosition').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('ApproveDiv');
                            var url = "/Master/KeyPosition/GetKeyPositionList?TabIndex=" + 'APPROVAL_PENDING';
                            $('#keyPositionListID').load(url, function () { });
                            $(".PendingTab").addClass("active");
                            $(".ActiveTab").removeClass("active");
                            $("#keyPositionListID").removeClass("hide");
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/KeyPosition/Approve?KeyPositionGI=" + GI + "&LogGI=" + LogGI;
                            $('#ApproveDiv').load(url, function () { });
                          /*  $('body').append('<div class="form-overlay"></div>');*/

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

function onDeleteClick(LogGI, GI, Type, index) {

    debugger;

    if ($("#KeyPositionList_" + index + "__EditActive").val() == 'True')
        swal.fire(ApprovalPendingMsg);


    else {
        //var url = "/Master/KeyPosition/Delete?LogGI=" + LogGI + "&KeyPositionGI=" + GI;
        //$('#DeleteSlider').load(url, function () { });
        /*  $('body').append('<div class="form-overlay"></div>');*/
        $.ajax({
            type: 'GET',
            url: '/Master/KeyPosition/Delete',
            dataType: 'html',
            data: { LogGI: LogGI, KeyPositionGI: GI },
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
                url: '/Master/KeyPosition/Delete',
                dataType: 'json',
                data: $('#KeyPositionDelete').serialize(),
                success: function (result) {
                    debugger
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('delete');
                            var url = "/Master/KeyPosition/GetKeyPositionList?TabIndex=" + 'ACTIVE';
                            $('#keyPositionListID').load(url, function () { });
                            $(".PendingTab").removeClass("active");
                            $(".ActiveTab").addClass("active");
                            $("#keyPositionListID").removeClass("hide");
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/KeyPosition/Delete?KeyPositionGI=" + GI + "&LogGI=" + LogGI;
                            $('#delete').load(url, function () { });
                           /* $('body').append('<div class="form-overlay"></div>');*/

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
        var pendingTableLength = $('#KeyPositionList_Count').val();
        if (pendingTableLength > 0) {
            url = "/Master/KeyPosition/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }

    else if (currentTab == 'ACTIVE') {
        var activeTableLength = $('#KeyPositionList_Count').val();
        if (activeTableLength > 0) {
            url = "/Master/KeyPosition/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }
    else if (currentTab == 'INACTIVE') {
        var inActiveTableLength = $('#KeyPositionList_Count').val();
        if (inActiveTableLength > 0) {
            url = "/Master/KeyPosition/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }
}


function onYOSChange(val, id) {
    debugger
    $('#YOSFrom').val('');
    $('#YOSTo').val('');
    YosText = $('#YSAboveOrBelow_Code option:selected').text();
    $('#YSAboveOrBelow_Name').val(YosText);
    $('#YOSFrom').removeClass('hide');
    if (val.value == 0) {

        $('#YOSFrom').addClass('hide');
    }
    if (val.value == 3) {

        $('#YOSTo').removeClass('hide');

       
    }
    else {
        $('#YOSTo').addClass('hide');

        
    }
    
}




function ActiveInactiveValidationPosition(name, code) {
    debugger

    $.ajax({

        type: 'POST',
        url: '/Master/KeyPosition/GetActiveInactiveValidation?name=' + name + '&code=' + code,
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
                    $("#Activeinactivekeyposition").prop("checked", true);
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





function onAgeChange(val,id) {
    $('#AgeFrom').val('');
    $('#AgeTo').val('');
    AgeText = $('#AgeAboveOrBelow_Code option:selected').text();
    $('#AgeAboveOrBelow_Name').val(AgeText);
    $('#AgeFrom').removeClass('hide');
    if (val.value == 0) {

        $('#AgeFrom').addClass('hide');
    }
    if (val.value == 3) {
        $('#AgeTo').removeClass('hide');

        //$('#ageDiv').removeClass('col-md-1');
        //$('#ageDiv').addClass('col-md-2');
        //$('#ageDiv').addClass('two-small-input');
    }
    else {
        $('#AgeTo').addClass('hide');

        //$('#ageDiv').removeClass('col-md-2');
        //$('#ageDiv').removeClass('two-small-input');
        //$('#ageDiv').addClass('col-md-1');
    }
    //if (!validateById('#AgeAboveOrBelow_Code', id)) {
    //    false;
    //}
    //else {
    //    true;
    //}
}