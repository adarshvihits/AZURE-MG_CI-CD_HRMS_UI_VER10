
var prevTab = 'ActiveTab';
var CurrentTab = 'ACTIVE';

//function OnTabClick(Tab, TabIndex) {
//    debugger;
//    $.ajaxSetup({
//        async: false
//    });
//    debugger;

    
//    $("." + prevTab).removeClass("active");
//    $("." + Tab).addClass("active");
//    var url = "/Master/DisciplinaryIncidentsAction/GetDisciplinaryIncidentsActionList";
   
//    $('#DisciplinaryIncidentsActionList').load(url, { TabIndex: TabIndex }, function () { });
//    $('#tabsDropdownDesciplinary').val(currentTab);
//    CurrentTab = TabIndex;
//    prevTab = Tab;
 
//}



function OnTabClick(Tab, TabIndex) {
    debugger;
    showSpinner();
    currentTab = TabIndex;
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");
    if (currentTab != "APPROVAL_PENDING") {

        $(".PendingTab").removeClass("active")
    }
 
    /*var url = "/Master/DisciplinaryIncidentsAction/GetDisciplinaryIncidentsActionList";*/
    $('#tabsDropdownDesciplinary').val(currentTab);
   /* $('#DisciplinaryIncidentsActionList').load(url, { TabIndex: TabIndex }, function () { hideSpinner(); });*/
    $.ajax({

        type: 'GET',
        url: '/Master/DisciplinaryIncidentsAction/GetDisciplinaryIncidentsActionList',
        dataType: 'html',
        data: { TabIndex: TabIndex },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#DisciplinaryIncidentsActionList').html("");
                $('#DisciplinaryIncidentsActionList').html(result);
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
        prevTab = 'InActiveTab';
    else
        prevTab = 'PendingTab';

    $("." + prevTab).addClass("active");
   
    $.ajax({

        type: 'GET',
        url: '/Master/DisciplinaryIncidentsAction/GetDisciplinaryIncidentsActionList',
        dataType: 'html',
        data: { TabIndex: currentTab },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#DisciplinaryIncidentsActionList').html("");
                $('#DisciplinaryIncidentsActionList').html(result);
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
    currentTab = 'ACTIVE';

    if (Type == 'MODIFY') {

        if ($("#DisciplinaryIncidentsActionList_" + index + "__EditActive").val() == 'True')
            swal.fire(ApprovalPendingMsg);


        else {
           
            $.ajax({

                type: 'GET',
                url: '/Master/DisciplinaryIncidentsAction/Edit',
                dataType: 'html',
                data: { IncidentActionGI: GI, LogGI: LogGI },
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
      
        var url = "/Master/DisciplinaryIncidentsAction/Create";
        $.ajax({

            type: 'GET',
            url: '/Master/DisciplinaryIncidentsAction/Create',
            dataType: 'html',
            data: { IncidentActionGI: GI, LogGI: LogGI },
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






function DisciplinaryIncidentsActionSave(mode) {
    debugger;
    if (validateDisciplinaryIncidentsAction()) {
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
                    url: '/Master/DisciplinaryIncidentsAction/Save',
                    dataType: 'json',
                    data: $('#DisciplinaryIncidentsActionSave').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                CloseSlider('AddNewOrEdit');
                                var url = "/Master/DisciplinaryIncidentsAction/GetDisciplinaryIncidentsActionList";
                                $('#DisciplinaryIncidentsActionList').load(url, { TabIndex: currentTab }, function () { });
                                /* window.location.href = url;*/
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
        //console.log($('#DisciplinaryIncidentsActionSave').serialize());
       
    }
    else {
        return false;
    }
}




function onApproveClick(GI, LogGI, Type, index)
{

    debugger;

    if ($("#DisciplinaryIncidentsActionList_" + index + "__MakerChecker").val() == 'True')
        swal.fire(MakerCheckerMsg);

    else {

        //var url = "/Master/DisciplinaryIncidentsAction/Approve?IncidentActionGI=" + GI + "&&LogGI=" + LogGI;
        //$('#ApproveSlider').load(url, function () { });
        //$('body').append('<div class="form-overlay"></div>');
        $.ajax({

            type: 'GET',
            url: '/Master/DisciplinaryIncidentsAction/Approve',
            dataType: 'html',
            data: { IncidentActionGI: GI, LogGI: LogGI },
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
        text: "Do you want to Approve",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({

                type: 'POST',
                url: '/Master/DisciplinaryIncidentsAction/Approve',
                dataType: 'json',
                data: $('#approveDisciplinaryIncidentsAction').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('ApproveDiv');
                            var url = "/Master/DisciplinaryIncidentsAction/GetDisciplinaryIncidentsActionList?TabIndex=" + 'APPROVAL_PENDING';
                          
                            $('#DisciplinaryIncidentsActionList').load(url, function () { });
                            $(".PendingTab").addClass("active");
                            $(".ActiveTab").removeClass("active");
                            $("#DisciplinaryIncidentsActionList").removeClass("hide");

                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            debugger;
                            var url = "/Master/DisciplinaryIncidentsAction/Approve?IncidentActionGI=" + GI + "&LogGI=" + LogGI;
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
                url: '/Master/DisciplinaryIncidentsAction/Reject',
                dataType: 'json',
                data: $('#approveDisciplinaryIncidentsAction').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('ApproveDiv');
                            var url = "/Master/DisciplinaryIncidentsAction/GetDisciplinaryIncidentsActionList?TabIndex=" + 'APPROVAL_PENDING';
                            $('#DisciplinaryIncidentsActionList').load(url, function () { });
                            $(".PendingTab").removeClass("active");
                            $(".ActiveTab").addClass("active");
                            $("#DisciplinaryIncidentsActionList").removeClass("hide");
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/DisciplinaryIncidentsAction/Approve?IncidentActionGI=" + GI + "&LogGI=" + LogGI;
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
                url: '/Master/DisciplinaryIncidentsAction/Delete',
                dataType: 'json',
                data: $('#deleteDisciplinaryIncidentsAction').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('delete');
                            var url = "/Master/DisciplinaryIncidentsAction/GetDisciplinaryIncidentsActionList?TabIndex=" + 'ACTIVE';
                            $('#DisciplinaryIncidentsActionList').load(url, function () { });
                            $(".PendingTab").removeClass("active");
                            $(".ActiveTab").addClass("active");
                            $("#DisciplinaryIncidentsActionList").removeClass("hide");
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/DisciplinaryIncidentsAction/Delete?IncidentActionGI=" + GI + "&LogGI=" + LogGI;
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








function onDeleteClick(GI, LogGI, Type, index) {

    debugger;

    if ($("#DisciplinaryIncidentsActionList_" + index + "__EditActive").val() == 'True')
        swal.fire(ApprovalPendingMsg);


    else {
       

        $.ajax({
            type: 'GET',
            url: '/Master/DisciplinaryIncidentsAction/Delete',
            dataType: 'html',
            data: { IncidentActionGI: GI, LogGI: LogGI },
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








function ExportToExcel() {

    debugger;
    if (currentTab == 'APPROVAL_PENDING') {
        var pendingTableLength = $('#DisciplinaryIncidentsActionList_Count').val();
        if (pendingTableLength > 0) {
            url = "/Master/DisciplinaryIncidentsAction/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }

    else if (currentTab == 'ACTIVE') {
        var activeTableLength = $('#DisciplinaryIncidentsActionList_Count').val();
        if (activeTableLength > 0) {
            url = "/Master/DisciplinaryIncidentsAction/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }
    else if (currentTab == 'INACTIVE') {
        var inActiveTableLength = $('#DisciplinaryIncidentsActionList_Count').val();
        if (inActiveTableLength > 0) {
            url = "/Master/DisciplinaryIncidentsAction/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }
}



function validateDisciplinaryIncidentsAction() {
    var i = 0;
    if (!validateById('#Name', 'DIsIncident')) {
        i++;
    }
    if (!validateById('#ShortName', 'DisShort')) {
        i++;
    }

    if (i == 0)
        return true;
    else
        return false;
}


function ActiveInactiveValidationDisci(name, code) {
    debugger

    $.ajax({

        type: 'POST',
        url: '/Master/DisciplinaryIncidentsAction/GetActiveInactiveValidation?name=' + name + '&code=' + code,
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
                    $("#disci").prop("checked", true);
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






