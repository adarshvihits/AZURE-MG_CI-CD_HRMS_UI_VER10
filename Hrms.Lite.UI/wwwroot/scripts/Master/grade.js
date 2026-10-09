var prevTab = 'ActiveTab';
var currentTab ='ACTIVE';

function OntabClick(Tab, TabIndex) {
    debugger;
    showSpinner();
    currentTab = TabIndex;
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");
    $('#tabsDropdownGrade').val(currentTab);

    $.ajax({

        type: 'GET',
        url: '/Master/Grade/GetGradeList',
        dataType: 'html',
        data: { TabIndex: TabIndex },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#Gradelist').html("");
                $('#Gradelist').html(result);
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
        url: '/Master/Grade/GetGradeList',
        dataType: 'html',
        data: { TabIndex: TabIndex },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#Gradelist').html("");
                $('#Gradelist').html(result);
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

    if ($("#GradeList_" + index + "__MakerChecker").val() == 'True')
        swal.fire(MakerCheckerMsg);
    else {

        $.ajax({

            type: 'GET',
            url: '/Master/Grade/Approve',
            dataType: 'html',
            data: { GradeGI: GI, LogGI: LogGI },
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


function onAddNewOrEditClick(Type, GI, LogGI, index) {
    debugger;
    //currentTab = 'ACTIVE';

    if (Type == 'MODIFY') {

        if ($("#GradeList_" + index + "__EditActive").val() == 'True')
            swal.fire(ApprovalPendingMsg);



        else {
            $.ajax({

                type: 'GET',
                url: '/Master/Grade/Edit',
                dataType: 'html',
                data: { GradeGI: GI, LogGI: LogGI },
                success: function (result) {
                    debugger
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }
                    else {
                        $('body').append('<div class="form-overlay"></div>');
                        $('#CreateOrEditSlider').html("");
                        $('#CreateOrEditSlider').html(result);
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

    //else {
    //    var url = "/Master/Grade/Create";
    //    $('#CreateOrEditSlider').load(url, function () { });
    //    $('body').append('<div class="form-overlay"></div>');
    //}

    else {
        var url = "/Master/Grade/Create";
        $.ajax({

            type: 'GET',
            url: '/Master/Grade/Create',
            dataType: 'html',
            data: { GradeGI: GI, LogGI: LogGI },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('#CreateOrEditSlider').html("");
                    $('#CreateOrEditSlider').html(result);
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




function validateGrade() {
    var i = 0;
    if (!validateById('#Name', 'GradeName')) {
        i++;
    }
    if (!validateById('#ShortName', 'GradeShrtName')) {
        i++;
    }
    if (!validateById('#NoticePeriod', 'GradeNoticePeriod')) {
        i++;
    }
    else if (!/^\d+$/.test($('#NoticePeriod').val().trim())) {
        $('#GradeNoticePeriod').html('Only numbers are allowed');
        i++;
    }

    return i == 0;
}


function onDeleteClick(GI, LogGI, Type, index) {

    debugger;

    if ($("#GradeList_" + index + "__EditActive").val() == 'True')
        swal.fire(ApprovalPendingMsg);


 
     else {

        $.ajax({
            type: 'GET',
            url: '/Master/Grade/Delete',
            dataType: 'html',
            data: { GradeGI: GI, LogGI: LogGI },
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





function GradeSave(mode) {
   
    debugger;
   
    if (validateGrade()) {
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
                    url: '/Master/Grade/Save',
                    dataType: 'json',
                 
                    data: $('#gradeSave').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                CloseSlider('CreateOrEditDiv');
                                debugger;
                                var url = "/Master/Grade/GetGradeList?TabIndex=" + currentTab;
                                //$(".InActiveTab").removeClass("active");
                                //$(".PendingTab").removeClass("active");
                                //$(".ActiveTab").addClass("active");
                                $('#Gradelist').load(url, function () { });
                           

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
                url: '/Master/Grade/Approve',
                dataType: 'json',
                data: $('#approve').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('ApproveDiv');
                            var url = "/Master/Grade/GetGradeList?TabIndex=" + 'APPROVAL_PENDING';
                            $('#Gradelist').load(url, function () { });
                           
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            
                            var url = "/Master/Grade/Approve?GradeGI=" + GI + "&LogGI=" + LogGI;
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
                url: '/Master/Grade/Reject',
                dataType: 'json',
                data: $('#approve').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('ApproveDiv');
                            var url = "/Master/Grade/GetGradeList?TabIndex=" + 'APPROVAL_PENDING';
                            $('#Gradelist').load(url, function () { });
                            $(".PendingTab").addClass("active");
                            $(".ActiveTab").removeClass("active");
                            $("#Gradelist").removeClass("hide");
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/Grade/Approve?GradeGI=" + GI + "&LogGI=" + LogGI;
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





function ActiveInactiveValidation(name, code) {
    debugger

    $.ajax({

        type: 'POST',
        url: '/Master/Grade/GetActiveInactiveValidation?name=' + name + '&code=' + code,
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
                    $("#Activeinactivedgrd").prop("checked", true);
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









function confirmDelete(GI, LogGI) {
    debugger
    $.ajaxSetup({
        async: false
    });
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
                url: '/Master/Grade/Delete',
                dataType: 'json',
                data: $('#deleteGrade').serialize(),
                success: function (result) {
                    debugger
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('delete');
                            debugger
                         
                            var url = "/Master/Grade/GetGradeList?TabIndex=" + currentTab;
                         
                            $('#Gradelist').load(url, function () { });
                            $(".PendingTab").removeClass("active");
                            $(".ActiveTab").addClass("active");
                            $("#Gradelist").removeClass("hide");
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/Grade/Delete/GradeGI=" + GI + "&LogGI=" + LogGI;
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

function CopypasteValidation(input) {
    input.value = input.value.replace(/[^0-9]/g, "");
}



function ExportToExcel() {

    debugger;
    if (currentTab == 'APPROVAL_PENDING') {
        var pendingTableLength = $('#GradeList_Count').val();
        if (pendingTableLength > 0) {
            url = "/Master/Grade/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }

    else if (currentTab == 'ACTIVE') {
        var activeTableLength = $('#GradeList_Count').val();
        if (activeTableLength > 0) {
            url = "/Master/Grade/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }
    else if (currentTab == 'INACTIVE') {
        var inActiveTableLength = $('#GradeList_Count').val();
        if (inActiveTableLength > 0) {
            url = "/Master/Grade/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }
}