
var prevTab = 'ActiveTab';
var currentTab = 'ACTIVE';
var Type;

function OnTabClick(Tab, TabIndex) {
    debugger;
    showSpinner();
    currentTab = TabIndex;
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");
    $('#tabsDropdownERPAccountHead').val(currentTab);



    $.ajax({

        type: 'GET',
        url: '/Master/ERPAccountHead/GetERPAccountHeadList',
        dataType: 'html',
        data: { TabIndex: TabIndex },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#ERPAccountHeadlist').html("");
                $('#ERPAccountHeadlist').html(result);
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
        url: '/Master/ERPAccountHead/GetERPAccountHeadList',
        dataType: 'html',
        data: { TabIndex: TabIndex },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#ERPAccountHeadlist').html("");
                $('#ERPAccountHeadlist').html(result);
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


function ActiveInactiveValidationDist(name, code) {
    debugger

    $.ajax({

        type: 'POST',
        url: '/Master/ERPAccountHead/GetActiveInactiveValidation?name=' + name + '&code=' + code,
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
                    $("#Activeinactivedist").prop("checked", true);
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

function onAddOrEditClick(Type, LogGI, GI, index) {
    debugger;
    //currentTab = 'ACTIVE';

    if (Type == 'MODIFY') {

        if ($("#ERPAccountHeadList_" + index + "__EditActive").val() == 'True')
            swal.fire(ApprovalPendingMsg);


        else {
            $.ajax({

                type: 'GET',
                url: '/Master/ERPAccountHead/Edit',
                dataType: 'html',
                data: { LogGI: LogGI, ERPAccountHeadGI: GI },
                success: function (result) {
                    debugger
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }
                    else {
                        $('body').append('<div class="form-overlay"></div>');
                        $('#AddNewOrEditERPAccountHeadSlider').html("");
                        $('#AddNewOrEditERPAccountHeadSlider').html(result);
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
            url: '/Master/ERPAccountHead/Create',
            dataType: 'html',
            data: { LogGI: LogGI, ERPAccountHeadGI: GI },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('#AddNewOrEditERPAccountHeadSlider').html("");
                    $('#AddNewOrEditERPAccountHeadSlider').html(result);
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




function onDeleteClick(LogGI, GI, Type, index) {

    debugger;

    if ($("#ERPAccountHeadList_" + index + "__EditActive").val() == 'True')
        swal.fire(ApprovalPendingMsg);




    else {

        $.ajax({
            type: 'GET',
            url: '/Master/ERPAccountHead/Delete',
            dataType: 'html',
            data: { LogGI: LogGI, ERPAccountHeadGI: GI },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('#DeleteERPAccountHeadSlider').html("");
                    $('#DeleteERPAccountHeadSlider').html(result);
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

    if ($("#ERPAccountHeadList_" + index + "__MakerChecker").val() == 'True')
        swal.fire(MakerCheckerMsg);



    else {

        $.ajax({

            type: 'GET',
            url: '/Master/ERPAccountHead/Approve',
            dataType: 'html',
            data: { LogGI: LogGI, ERPAccountHeadGI: GI },
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



function confirmApproveERPAccountHead(GI, LogGI) {
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
                url: '/Master/ERPAccountHead/Approve1',
                dataType: 'json',
                data: $('#approve').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('PendingApprove');
                            var url = "/Master/ERPAccountHead/GetERPAccountHeadList?TabIndex=" + 'APPROVAL_PENDING';
                            $('#ERPAccountHeadlist').load(url, function () { });
                            $(".PendingTab").addClass("active");
                            $(".ActiveTab").removeClass("active");
                            $("#ERPAccountHeadlist").removeClass("hide");
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/ERPAccountHead/Approve?ERPAccountHeadGI=" + GI + "&LogGI=" + LogGI;
                            $('#PendingApprove').load(url, function () { });
                            $('body').append('<div class="form-overlay"></div>');

                        });
                    }

                },
                error: function (xhr, textStatus, errorThrown) {
                    document.body.id = "kt_body";
                    $('body').html(xhr.responseText);

                }
            });
        }
        else {
            return false;
        }
    })

}




function confirmRejectERPAccountHead(GI, LogGI) {
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
                url: '/Master/ERPAccountHead/Reject',
                dataType: 'json',
                data: $('#approve').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('PendingApprove');
                            var url = "/Master/ERPAccountHead/GetERPAccountHeadList?TabIndex=" + 'APPROVAL_PENDING';
                            $('#ERPAccountHeadlist').load(url, function () { });
                            $(".PendingTab").addClass("active");
                            $(".AtiveTab").removeClassaddClass("active");
                            $("#ERPAccountHeadlist").removeClass("hide");
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/ERPAccountHead/Approve?ERPAccountHeadGI=" + GI + "&LogGI=" + LogGI;
                            $('#PendingApprove').load(url, function () { });
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
                url: '/Master/ERPAccountHead/Delete1',
                dataType: 'json',
                data: $('#ERPAccountHeadDelete').serialize() + '&ERPAccountHeadGI=' + GI,
                success: function (result) {
                    debugger
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('delete');
                            var url = "/Master/ERPAccountHead/GetERPAccountHeadList?TabIndex=" + 'ACTIVE';
                            $('#ERPAccountHeadlist').load(url, function () { });
                            $(".PendingTab").removeClass("active");
                            $(".ActiveTab").addClass("active");
                            $("#ERPAccountHeadlist").removeClass("hide");
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            //var url = "/Master/ERPAccountHead/Delete?ERPAccountHeadGI=" + GI + "&LogGI=" + LogGI;
                            //$('#delete').load(url, function () { });
                            //$('body').append('<div class="form-overlay"></div>');

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
        var pendingTableLength = $('#ERPAccountHeadList_Count').val();
        if (pendingTableLength > 0) {
            url = "/Master/ERPAccountHead/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }

    else if (currentTab == 'ACTIVE') {
        var activeTableLength = $('#ERPAccountHeadList_Count').val();
        if (activeTableLength > 0) {
            url = "/Master/ERPAccountHead/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }
    else if (currentTab == 'INACTIVE') {
        var inActiveTableLength = $('#ERPAccountHeadList_Count').val();
        if (inActiveTableLength > 0) {
            url = "/Master/ERPAccountHead/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }
}






function validateByClass(elementId, spanId) {
    debugger;
    if ($(elementId).val() == "") {
        $(elementId).addClass('is-invalid');
        $("#" + spanId + "").addClass('error-message');
        $("#" + spanId + "").html("This field is required");
        return false;
    }
    else {
        $(elementId).removeClass('is-invalid');
        $("#" + spanId + "").removeClass('error-message');
        $("#" + spanId + "").html("");
        //trim(val);        
        return true;
    }
}




function validateERPAccountHead() {
    var i = 0;
    if (!validateById('#Name', 'ERPAccountHeadName')) {
        i++;
    }
    if (!validateById('#ShortName', 'ERPAccountHeadshortName')) {
        i++;
    }
    if (!validateById('#Country_Code', 'CountryName')) {
        i++;
    }
    if (!validateById('.dStCode', 'stateName')) {
        i++;
    }
    if (i == 0)
        return true;
    else
        return false;
}




function onCountryChange(sender, id) {

    debugger
    var cid = $(sender).val();
    if (cid == "") {
        $(sender).addClass('is-invalid');
        $("#" + id + "").addClass('error-message');
        $("#" + id + "").html("This field is required");
    }
    else {
        $(sender).removeClass('is-invalid');
        $("#" + id + "").removeClass('error-message');
        $("#" + id + "").html("");
    }
    debugger;
    $.getJSON("/Master/ERPAccountHead/GetState", { id: cid }, function (data) {

        $('.dStCode option').remove();

        $('.dStCode').append(('<option value>--Select State--</option'));


        for (var i = 0; i < data.length; i++) {
            debugger
            $('.dStCode').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
        }

    });




}

function ERPAccountHeadSaveClick(mode) {
    debugger;
    if (validateERPAccountHead()) {
        //console.log($("#State_Code").val());
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
                    url: '/Master/ERPAccountHead/Save',
                    dataType: 'json',
                    data: $('#ERPAccountHeadSave').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                CloseSlider('AddNewDiv');
                                var url = "/Master/ERPAccountHead/GetERPAccountHeadList?TabIndex=" + currentTab;
                                $('#ERPAccountHeadlist').load(url, function () { });


                            });

                        }

                        else {
                            swal.fire(result.message).then(function () {

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
    else {
        return false;
    }
}

