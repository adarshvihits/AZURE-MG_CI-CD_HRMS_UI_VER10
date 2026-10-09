var prevTab = 'tab1';
var currentTab = 'ACTIVE';
function TabOnClick(Tab, TabIndex) {
    debugger;
    showSpinner();
    currentTab = TabIndex;
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");    
    //$('#tabsDropdownSection').val(currentTab);  
    
    $.ajax({

        type: 'GET',
        url: '/Master/ResumeSource/GetList',
        dataType: 'html',
        data: { TabIndex: TabIndex },

        success: function (result) {
            debugger
            console.log(result);
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                debugger
                $('#resumeList').html("");
                $('#resumeList').html(result);
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

function onAddNewOrEditClick(Type, GI, LogGI, index) {
    debugger;
    //currentTab = 'ACTIVE';

    if (Type == 'MODIFY') {

        if ($("#SourceList_" + index + "__EditActive").val() == 'True')
            swal.fire(ApprovalPendingMsg);



        else {
            $.ajax({

                type: 'GET',
                url: '/Master/ResumeSource/Edit',
                dataType: 'html',
                data: { ResumeSourceGI: GI, LogGI: LogGI },
                success: function (result) {
                    debugger
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }
                    else {
                        $('body').append('<div class="form-overlay"></div>');
                        $('#sliderView').html("");
                        $('#sliderView').html(result);
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
        //window.location.href = '/Master/ResumeSource/Create';
        $.ajax({

            type: 'GET',
            url: '/Master/ResumeSource/Create',
            dataType: 'html',
            data: { CountryGI: GI, LogGI: LogGI },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    //$('body').append('<div class="form-overlay"></div>');
                    $('#sliderView').html("");
                    $('#sliderView').html(result);
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


function SaveResumeSrc(mode) {
    debugger;
    var TabIndex;
    //if (validateSection()) {
    var i = 0;
    if (!validateById('#Name', 'name')) {
        i++;
    }
    if (!validateById('#ShortName', 'shortname')) {
        i++;
    }
    if (i == 0) {
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
                    url: '/Master/ResumeSource/Save',
                    dataType: 'json',
                    data: $('#ResumeSourceSave').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                CloseSlider('addResumeSrc');

                                if (currentTab == 'ACTIVE') {
                                    tabClass = 'tab1';
                                }
                                else if (currentTab == 'INACTIVE') {
                                    tabClass = 'tab2';
                                }
                                else {
                                    tabClass = 'tab3';
                                }
                                TabOnClick(tabClass, currentTab);
                                

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
    else
        return false;
}

function onApproveClick(GI, LogGI, Type, index) {

    debugger;

    if ($("#SourceList_" + index + "__MakerChecker").val() == 'True')
        swal.fire(MakerCheckerMsg);

    else {

        debugger;

        $.ajax({

            type: 'GET',
            url: '/Master/ResumeSource/Approve',
            dataType: 'html',
            data: { ResumeSourceGI: GI, LogGI: LogGI },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('#sliderView').html("");
                    $('#sliderView').html(result);
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
                url: '/Master/ResumeSource/Reject',
                dataType: 'json',
                data: $('#approve').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('apprResumeSrc');
                            if (currentTab == 'ACTIVE') {
                                tabClass = 'tab1';
                            }
                            else if (currentTab == 'INACTIVE') {
                                tabClass = 'tab2';
                            }
                            else {
                                tabClass = 'tab3';
                            }
                            TabOnClick(tabClass, currentTab);
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
                url: '/Master/ResumeSource/Approve',
                dataType: 'json',
                data: $('#approve').serialize(),
                success: function (result) {

                    if (result.success == true) {
                        debugger
                        swal.fire(result.message).then(function () {
                            CloseSlider('apprResumeSrc');
                            if (currentTab == 'ACTIVE') {
                                tabClass = 'tab1';
                            }
                            else if (currentTab == 'INACTIVE') {
                                tabClass = 'tab2';
                            }
                            else {
                                tabClass = 'tab3';
                            }
                            TabOnClick(tabClass, currentTab);
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


function onDeleteClick(GI, LogGI, Type, index) {

    debugger;

    if ($("#SourceList_" + index + "__EditActive").val() == 'True')
        swal.fire(ApprovalPendingMsg);


    else {
        //var url = "/Master/Section/Delete?SectionGI=" + GI + "&LogGI=" + LogGI;
        //$('#DeleteSlider').load(url, function () { });
        //$('body').append('<div class="form-overlay"></div>');
        $.ajax({
            type: 'GET',
            url: '/Master/ResumeSource/Delete',
            dataType: 'html',
            data: { ResumeSourceGI: GI, LogGI: LogGI },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('#sliderView').html("");
                    $('#sliderView').html(result);
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
                url: '/Master/ResumeSource/Delete',
                dataType: 'json',
                data: $('#ResumeSrcDelete').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('deleteResumeSrc');
                            if (currentTab == 'ACTIVE') {
                                tabClass = 'tab1';
                            }
                            else if (currentTab == 'INACTIVE') {
                                tabClass = 'tab2';
                            }
                            else {
                                tabClass = 'tab3';
                            }
                            TabOnClick(tabClass, currentTab);
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

function ExportToExcel() {

    debugger;
    if (currentTab == 'APPROVAL_PENDING') {
        var pendingTableLength = $('#SourceList_Count').val();
        if (pendingTableLength > 0) {
            url = "/Master/ResumeSource/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }

    else if (currentTab == 'ACTIVE') {
        var activeTableLength = $('#SourceList_Count').val();
        if (activeTableLength > 0) {
            url = "/Master/ResumeSource/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }
    else if (currentTab == 'INACTIVE') {
        var inActiveTableLength = $('#SourceList_Count').val();
        if (inActiveTableLength > 0) {
            url = "/Master/ResumeSource/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }
}



function ActiveInactiveValidationResume(name, code) {
    debugger

    $.ajax({

        type: 'POST',
        url: '/Master/ResignationReason/GetActiveInactiveValidation?name=' + name + '&code=' + code,
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
                    $("#resume").prop("checked", true);
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