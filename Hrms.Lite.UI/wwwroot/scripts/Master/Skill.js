

var prevTab = 'ActiveTab';
var currentTab = 'ACTIVE';


function OnTabClick(Tab, TabIndex) {
    $.ajaxSetup({
        async: false
    });
    debugger;
    showSpinner();
    //currentTab = TabIndex;
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");
    $('#tabsDropdownSkill').val(currentTab);
    //var url = "/Master/Skill/GetSkillList";
    //$('#SkillList').load(url, { TabIndex: TabIndex }, function () { hideSpinner(); });
    $.ajax({

        type: 'GET',
        url: '/Master/Skill/GetSkillList',
        dataType: 'html',
        data: { TabIndex: TabIndex },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#SkillList').html("");
                $('#SkillList').html(result);
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


    currentTab = TabIndex;

    prevTab = Tab;
}



function ActiveInactiveValidationSkill(name, code) {
    debugger

    $.ajax({

        type: 'POST',
        url: '/Master/Skill/GetActiveInactiveValidation?name=' + name + '&code=' + code,
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
                    $("#Activeinactiveskill").prop("checked", true);
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
        url: '/Master/Skill/currentTab',
        dataType: 'html',
        data: { TabIndex: TabIndex },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#SkillList').html("");
                $('#SkillList').html(result);
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
function confirmDelete(GI, LogGI) {

    if (confirm("Do you want to Delete?")) {
        debugger;

        $.ajax({

            type: 'POST',
            url: '/Master/Skill/Delete',
            dataType: 'json',
            data: $('#deleteSkill').serialize(),
            success: function (result) {
                if (result.success == true) {
                    swal.fire(result.message).then(function () {
                        CloseSlider('delete');
                        var url = "/Master/Skill/GetSkillList?TabIndex=" + 'ACTIVE';
                       
                       
                       

                        $('#SkillList').load(url, function () { });
                        $(".PendingTab").removeClass("active");
                        $(".ActiveTab").addClass("active");
                        $("#SkillList").removeClass("hide");
                    });

                }
                else {
                    debugger
                    swal.fire(result.message).then(function () {
                        var url = "/Master/Skill/Delete?SkillGI=" + GI + "&LogGI=" + LogGI;
                        $('#delete').load(url, function () { });
                        $('body').append('<div class="form-overlay"></div>');

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

}


function onAddNewOrEditClick(Type, GI, LogGI, index) {
    debugger;
    //currentTab = 'ACTIVE';

    if (Type == 'MODIFY') {

        if ($("#SkillList_" + index + "__EditActive").val() == 'True')
            swal.fire(ApprovalPendingMsg);


        else {
            $.ajax({

                type: 'GET',
                url: '/Master/Skill/Edit',
                dataType: 'html',
                data: { SkillGI: GI, LogGI: LogGI },
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
            url: '/Master/Skill/CreateSkill',
            dataType: 'html',
            data: { SkillGI: GI, LogGI: LogGI },
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





function SkillSave(mode) {
    debugger;
    if (validateSkill()) {

        //console.log($('#Skill-create-edit').serialize());
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
                    url: '/Master/Skill/Create',
                    dataType: 'json',
                    data: $('#Skill-create-edit').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                CloseSlider('AddNewOrEdit');
                                var url = "/Master/Skill/GetSkillList";
                                $('#SkillList').load(url, { TabIndex: currentTab }, function () { });
                                /* window.location.href = url;*/
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
    if ($("#SkillList_" + index + "__MakerChecker").val() == 'True')
        swal.fire(MakerCheckerMsg);

 

    else {

        $.ajax({

            type: 'GET',
            url: '/Master/Skill/Approve',
            dataType: 'html',
            data: { SkillGI: GI, LogGI: LogGI },
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
                url: '/Master/Skill/Approve',
                dataType: 'json',
                data: $('#approve').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('ApproveDiv');
                            var url = "/Master/Skill/GetSkillList?TabIndex=" + 'APPROVAL_PENDING';


                            $('#SkillList').load(url, { TabIndex: currentTab }, function () { });


                            /* window.location.href = url;*/


                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            debugger;
                            var url = "/Master/Skill/Approve?SkillGI=" + GI + "&LogGI=" + LogGI;
                            $('#ApproveSlider').load(url, function () { });
                            $('body').append('<div class="form-overlay"></div>');


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
                url: '/Master/Skill/Reject',
                dataType: 'json',
                data: $('#approve').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('ApproveDiv');
                            var url = "/Master/Skill/GetSkillList?TabIndex=" + 'APPROVAL_PENDING';

                            $('#SkillList').load(url, { TabIndex: currentTab }, function () { });
                            /*window.location.href = url;*/
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/Skill/Approve?SkillGI=" + GI + "&LogGI=" + LogGI;
                            $('#ApproveSlider').load(url, function () { });
                            $('body').append('<div class="form-overlay"></div>');

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
                url: '/Master/Skill/Delete',
                dataType: 'json',
                data: $('#deleteSkill').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('delete');
                            var url = "/Master/Skill/GetSkillList?TabIndex=" + 'ACTIVE';
                            $('#SkillList').load(url, { TabIndex: currentTab }, function () { });
                            $(".PendingTab").removeClass("active");
                            $(".ActiveTab").addClass("active");
                            $("#SkillList").removeClass("hide");
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/Skill/Delete?SkillGI=" + GI + "&LogGI=" + LogGI;
                            $('#delete').load(url, function () { });
                            $('body').append('<div class="form-overlay"></div>');

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


function onDeleteClick(GI, LogGI, Type, index) {

    debugger;

    if ($("#SkillList_" + index + "__EditActive").val() == 'True')
        swal.fire(ApprovalPendingMsg);


  


    else {

        $.ajax({
            type: 'GET',
            url: '/Master/Skill/Delete',
            dataType: 'html',
            data: { SkillGI: GI, LogGI: LogGI },
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
        var pendingTableLength = $('#SkillList_Count').val();
        if (pendingTableLength > 0) {
            url = "/Master/Skill/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }

    else
    {
        var activeTableLength = $('#SkillList_Count').val();
        if (activeTableLength > 0) {
            url = "/Master/Skill/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else
        {
            swal.fire(NoDataFoundMsg);
        }
    }
    ////else if (currentTab == 'INACTIVE') {
    ////    var inActiveTableLength = document.getElementById("SkillTable").rows.length - 1;
    ////    if (inActiveTableLength > 0) {
    ////        url = "/Master/Skill/ExportToExcel?TabIndex=" + currentTab;
    ////        window.location.href = url;
    ////    }
    ////    else {
    ////        swal.fire(NoDataFoundMsg);
    ////    }

    ////}

}


function validateSkill() {
    var i = 0;
    if (!validateById('#Name', 'SkillName')) {
        i++;
    }
    if (!validateById('#ShortName', 'SkillShortName')) {
        i++;
    }
    if (!validateById('#SkillType_Code', 'Skill_Type')) {
        i++;
    }
    if (i == 0)
        return true;
    else
        return false;
}

