var prevTab = 'ActiveTab';
var currentTab = 'ACTIVE';


function OnTabClick(Tab, TabIndex) {
    debugger;
    $.ajaxSetup({
        async: false
    });
    /*   currentTab = TabIndex;*/
    showSpinner();
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");
    //var url = "/Master/WorkLocation/GetWorkLocationList";
    //$('#WorkLocationList').load(url, { TabIndex: TabIndex }, function () { hideSpinner(); });
    currentTab = TabIndex;
    $('#tabsDropdownWorkLocation').val(currentTab);
    $.ajax({

        type: 'GET',
        url: '/Master/WorkLocation/GetWorkLocationList',
        dataType: 'html',
        data: { TabIndex: TabIndex },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#WorkLocationList').html("");
                $('#WorkLocationList').html(result);
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
    //var url = "/Master/WorkLocation/GetWorkLocationList";
    //$('#WorkLocationList').load(url, { TabIndex: currentTab }, function () { hideSpinner(); });
    $.ajax({

        type: 'GET',
        url: '/Master/WorkLocation/GetWorkLocationList',
        dataType: 'html',
        data: { TabIndex: TabIndex },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#WorkLocationList').html("");
                $('#WorkLocationList').html(result);
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

        if ($("#WorkLocationList_" + index + "__EditActive").val() == 'True')
            swal.fire(ApprovalPendingMsg);


        else {
            $.ajax({

                type: 'GET',
                url: '/Master/WorkLocation/Edit',
                dataType: 'html',
                data: { WorkLocationGI: GI, LogGI: LogGI },
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
            //var url = "/Master/WorkLocation/Edit?WorkLocationGI=" + GI + "&LogGI=" + LogGI;

            //$('#AddNewOrEditSlider').load(url, function () { });
            //$('body').append('<div class="form-overlay"></div>');
        }

    }

    else {
        //var url = "/Master/WorkLocation/Create";
        //$('#AddNewOrEditSlider').load(url, function () { });
        //$('body').append('<div class="form-overlay"></div>');
        var url = "/Master/WorkLocation/Create";
        $.ajax({

            type: 'GET',
            url: '/Master/WorkLocation/Create',
            dataType: 'html',
            data: { WorkLocationGI: GI, LogGI: LogGI },
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


//function onAddNewOrEditClick(Type, GI, LogGI, index) {
//    debugger;
    
//    if (Action == 'MODIFY') {
//        var url = "/Master/WorkLocation/Edit?WorkLocationGI=" + GI + "&LogGI=" + LogGI;
//    }
//    else {
//        var url = "/Master/WorkLocation/Create";
//    }
//    $('#AddNewOrEditSlider').load(url, function () { });    
//    $('body').append('<div class="form-overlay"></div>');

//}
//function Approve(GI, LogGI) {
//    debugger;
//    var url = "/Master/WorkLocation/Approve?WorkLocationGI=" + GI + "&&LogGI=" + LogGI;
//    $("#ApproveSlider").load(url, function () { });
//    $('body').append('<div class="form-overlay"></div>');
  
  

//}




function onApproveClick(GI, LogGI, Type, index) {

    debugger;

    if ($("#WorkLocationList_" + index + "__MakerChecker").val() == 'True')
        swal.fire(MakerCheckerMsg);

    else {
        //var url = "/Master/WorkLocation/Approve?WorkLocationGI=" + GI + "&LogGI=" + LogGI;

        //$('#ApproveSlider').load(url, function () { });
        //$('body').append('<div class="form-overlay"></div>');
        $.ajax({

            type: 'GET',
            url: '/Master/WorkLocation/Approve',
            dataType: 'html',
            data: { WorkLocationGI: GI, LogGI: LogGI },
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
                url: '/Master/WorkLocation/Approve',
                dataType: 'json',
                data: $('#approve').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('ApproveDiv');
                            var url = "/Master/WorkLocation/GetWorkLocationList?TabIndex=" + 'APPROVAL_PENDING';
                            $('#WorkLocationList').load(url, function () { });
                            $(".PendingTab").addClass("active");
                            $(".ActiveTab").removeClass("active");
                            $("#WorkLocationList").removeClass("hide");

                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            debugger;
                            //var url = "/Master/WorkLocation/Approve?WorkLocationGI=" + GI + "&LogGI=" + LogGI;
                            //$('#ApproveSlider').load(url, function () { });
                            //$('body').append('<div class="form-overlay"></div>');
                            CloseSlider('ApproveDiv');


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
                url: '/Master/WorkLocation/Reject',
                dataType: 'json',
                data: $('#approve').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('ApproveDiv');
                            var url = "/Master/WorkLocation/GetWorkLocationList?TabIndex=" + 'APPROVAL_PENDING';
                            $('#WorkLocationList').load(url, function () { });
                            $(".PendingTab").addClass("active");
                            $(".ActiveTab").removeClass("active");
                            $("#WorkLocationList").removeClass("hide");

                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/WorkLocation/Approve?WorkLocationGI=" + GI + "&LogGI=" + LogGI;
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

//function Delete(GI, LogGI) {

//    debugger;

//    var url = "/Master/WorkLocation/Delete?WorkLocationGI=" + GI + "&LogGI=" + LogGI;
//    $("#DeleteSlider").load(url, function () { });
//    $('body').append('<div class="form-overlay"></div>');
    
//}

function onDeleteClick(GI, LogGI, Type, index) {

    debugger;

    if ($("#WorkLocationList_" + index + "__EditActive").val() == 'True')
        swal.fire(ApprovalPendingMsg);


    else {
        //var url = "/Master/WorkLocation/Delete?WorkLocationGI=" + GI + "&LogGI=" + LogGI;

        //$('#DeleteSlider').load(url, function () { });
        //$('body').append('<div class="form-overlay"></div>');
        $.ajax({
            type: 'GET',
            url: '/Master/WorkLocation/Delete',
            dataType: 'html',
            data: { WorkLocationGI: GI, LogGI: LogGI },
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
                url: '/Master/WorkLocation/Delete',
                dataType: 'json',
                data: $('#deleteWorkLocation').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('delete');
                            var url = "/Master/WorkLocation/GetWorkLocationList?TabIndex=" + 'ACTIVE';
                            $('#WorkLocationList').load(url, function () { });
                            $(".PendingTab").removeClass("active");
                            $(".ActiveTab").addClass("active");
                            $("#WorkLocationList").removeClass("hide");
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/WorkLocation/Delete?WorkLocationGI=" + GI + "&LogGI=" + LogGI;
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






function WorkLocationSave(mode) {
    debugger;
    if (validateWorkLocation()) {

        //console.log($('#worklocationId').serialize());
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
                    url: '/Master/WorkLocation/Save',
                    dataType: 'json',
                    data: $('#worklocationId').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                CloseSlider('AddNewOrEdit');
                                var url = "/Master/WorkLocation/Index";
                                window.location.href = url;
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
        var pendingTableLength = $('#WorkLocationList_Count').val();
        if (pendingTableLength > 0) {
            url = "/Master/WorkLocation/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }

    else {
        var activeTableLength = $('#WorkLocationList_Count').val();
        if (activeTableLength > 0) {
            url = "/Master/WorkLocation/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }
    //else if (currentTab == 'INACTIVE') {
    //    var inActiveTableLength = document.getElementById("WorkLocationTable").rows.length - 1;
    //    if (inActiveTableLength > 0) {
    //        url = "/Master/WorkLocation/ExportToExcel?TabIndex=" + currentTab;
    //        window.location.href = url;
    //    }
    //    else {
    //        swal.fire("No data found to export");
    //    }

    //}

}









function validateWorkLocation() {
    var i = 0;
    if (!validateById('#Name', 'WLName')) {
        i++;
    }
    if (!validateById('#ShortName', 'WLShortName')) {
        i++;
    }

    if (i == 0)
        return true;
    else
        return false;
}

function ActiveInactiveValidationWorkLocation(name, code) {
    debugger

    $.ajax({

        type: 'POST',
        url: '/Master/WorkLocation/GetActiveInactiveValidation?name=' + name + '&code=' + code,
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
                    $("#worklocation_id").prop("checked", true);
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
