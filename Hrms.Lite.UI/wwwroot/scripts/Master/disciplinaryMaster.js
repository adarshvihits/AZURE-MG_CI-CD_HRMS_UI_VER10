var prevTab = 'ActiveTab';
var currentTab = 'ACTIVE';

function OnTabClick(Tab, TabIndex) {
    debugger;
    showSpinner();
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");
   /* var url = "/Master/DisciplinaryMaster/GetDisciplinaryList";*/

   /* $('#ActiveList').load(url, { TabIndex: TabIndex }, function () { hideSpinner(); });*/
    currentTab = TabIndex;
    $('#tabsDropdown').val(currentTab);
    $.ajax({

        type: 'GET',
        url: '/Master/DisciplinaryMaster/GetDisciplinaryList',
        dataType: 'html',
        data: { TabIndex: TabIndex },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#ActiveList').html("");
                $('#ActiveList').html(result);
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
    //var url = "/Master/DisciplinaryMaster/GetDisciplinaryList";
    //$('#ActiveList').load(url, { TabIndex: currentTab }, function () { hideSpinner(); });
    $.ajax({

        type: 'GET',
        url: '/Master/DisciplinaryMaster/GetDisciplinaryList',
        dataType: 'html',
        data: { TabIndex: currentTab },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#ActiveList').html("");
                $('#ActiveList').html(result);
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

        if ($("#DisciplinaryIncidentsList_" + index + "__EditActive").val() == 'True')
            swal.fire(ApprovalPendingMsg);


        else {
          
            $.ajax({

                type: 'GET',
                url: '/Master/DisciplinaryMaster/Edit',
                dataType: 'html',
                data: { IncidentsGI: GI, LogGI: LogGI },
                success: function (result) {
                    debugger
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }
                    else {
                        $('body').append('<div class="form-overlay"></div>');
                        $('#newRequestSlider').html("");
                        $('#newRequestSlider').html(result);
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
       
        var url = "/Master/DisciplinaryMaster/Create";
        $.ajax({

            type: 'GET',
            url: '/Master/DisciplinaryMaster/Create',
            dataType: 'html',
            data: { IncidentsGI: GI, LogGI: LogGI },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('#newRequestSlider').html("");
                    $('#newRequestSlider').html(result);
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






function ActiveInactiveValidationIncidents(name, code) {
    debugger

    $.ajax({

        type: 'POST',
        url: '/Master/DisciplinaryMaster/GetActiveInactiveValidation?name=' + name + '&code=' + code,
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
                    $("#incidents").prop("checked", true);
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








function onDeleteClick(GI, LogGI, Type, index) {

    debugger;

    if ($("#DisciplinaryIncidentsList_" + index + "__EditActive").val() == 'True')
        swal.fire(ApprovalPendingMsg);


    else {
        //var url = "/Master/DisciplinaryMaster/Delete?IncidentsGI=" + GI + "&LogGI=" + LogGI;
        //$('#deleteDesSlider').load(url, function () { });
        //$('body').append('<div class="form-overlay"></div>');
        $.ajax({
            type: 'GET',
            url: '/Master/DisciplinaryMaster/Delete',
            dataType: 'html',
            data: { IncidentsGI: GI, LogGI: LogGI },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('#deleteDesSlider').html("");
                    $('#deleteDesSlider').html(result);
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









function confirmDelete(GI, LogGI){

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
                url: '/Master/DisciplinaryMaster/Delete',
                dataType: 'json',
                data: $('#Deletedesciplinary').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('DeleteDiv');
                            OnTabClick(prevTab, currentTab);
                            tabClick(document.getElementById('tabsDropdown'));
                            //var url = "/Master/DisciplinaryMaster/GetDisciplinaryIncidentsList?TabIndex=" + 'ACTIVE';
                            //$('#ActiveList').load(url, function () { });
                            //$(".PendingTab").removeClass("active");
                            //$(".ActiveTab").addClass("active");
                            //$("#ActiveList").removeClass("hide");
                        });

                    }
                    else {
                        debugger
                        swal.fire(result.message).then(function () {
                            var url = "/Master/DisciplinaryMaster/Delete?IncidentsGI=" + GI + "&LogGI=" + LogGI;
                            $('#deleteDesSlider').load(url, function () { });
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












function onApproveClick(GI, LogGI, Type, index) {

    debugger;

    if ($("#DisciplinaryIncidentsList_" + index + "__MakerChecker").val() == 'True')
        swal.fire(MakerCheckerMsg);

    else {
        //var url = "/Master/DisciplinaryMaster/Approve?IncidentsGI=" + GI + "&LogGI=" + LogGI;
        //$('#approveSlider').load(url, function () { });
        //$('body').append('<div class="form-overlay"></div>');
        $.ajax({

            type: 'GET',
            url: '/Master/DisciplinaryMaster/Approve',
            dataType: 'html',
            data: { IncidentsGI: GI, LogGI: LogGI },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('#approveSlider').html("");
                    $('#approveSlider').html(result);
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






function disciplinarySave(mode) {
    debugger;
    var TabIndex;
    if (validateDisciplinary()) {

        //console.log($('#disciplinarySave').serialize());
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
                    url: '/Master/DisciplinaryMaster/Save',
                    dataType: 'json',
                    data: $('#disciplinarySave').serialize(),
                    success: function (result) {
                        if (result.success) {
                            debugger;
                            swal.fire(result.message).then(function () {
                                CloseSlider('disciplinarySaveclose');
                                var url = "/Master/DisciplinaryMaster/GetDisciplinaryList?TabIndex=" + currentTab;
                                $('#ActiveList').load(url, function () { });
                            });

                        }

                        else {
                            debugger;
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








function ConfirmDisciplinaryApprove(GI, LogGI) {

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
                url: '/Master/DisciplinaryMaster/Approve',
                dataType: 'json',
                data: $('#approve').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('PendingApprove');
                            var url = "/Master/DisciplinaryMaster/GetDisciplinaryList?TabIndex=" + 'APPROVAL_PENDING';


                            $('#ActiveList').load(url, function () { });
                            $(".PendingTab").addClass("active");
                            $(".ActiveTab").removeClass("active");
                            $("#ActiveList").removeClass("hide");
                        });

                    }
                    else {
                        debugger;
                        swal.fire(result.message).then(function () {
                            debugger;
                            var url = "/Master/DisciplinaryMaster/Approve?IncidentsGI=" + GI + "&LogGI=" + LogGI;
                            $('#approveSlider').load(url, function () { });
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

function ConfirmDisciplinaryReject() {
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
                url: '/Master/DisciplinaryMaster/Reject',
                dataType: 'json',
                data: $('#approve').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('PendingApprove');
                            var url = "/Master/DisciplinaryMaster/GetDisciplinaryList?TabIndex=" + 'APPROVAL_PENDING';
                            $('#ActiveList').load(url, function () { });
                            $(".PendingTab").addClass("active");
                            $(".ActiveTab").removeClass("active");
                            $("#ActiveList").removeClass("hide");
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/DisciplinaryMaster/Approve?IncidentsGI=" + GI + "&LogGI=" + LogGI;
                            $('#approveSlider').load(url, function () { });
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




function onExportToExcelClick() {
    debugger;

    if (currentTab != "APPROVAL_PENDING") {

        var trainingMasterTableLength = $('#DisciplinaryIncidentsList_Count').val();
        if (trainingMasterTableLength > 0) {
            url = "/Master/DisciplinaryMaster/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }
    else {
        var trainingMasterPendingTableLength = $('#DisciplinaryIncidentsList_Count').val();
        if (trainingMasterPendingTableLength > 0) {
            url = "/Master/DisciplinaryMaster/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }
}


function validateDisciplinary() {
    var i = 0;
    if (!validateById('#Name', 'DisciplinaryName')) {
        i++;
    }
    if (!validateById('#ShortName', 'DisciplinaryShortName')) {
        i++;
    }

    if (i == 0)
        return true;
    else
        return false;
}















