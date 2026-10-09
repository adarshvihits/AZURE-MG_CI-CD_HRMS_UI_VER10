var currentTab;
var currentTab = 'ACTIVE';
var prevTab = 'ActiveTab';


function OnTabClick(Tab, TabIndex) {
    debugger;
    showSpinner();
    currentTab = TabIndex;
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");
    $('#tabsDropdownRegion').val(currentTab);
    $.ajax({

        type: 'GET',
        url: '/TrainingModule/TrainingSubject/GetTrainingList',
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
function SaveSubject(mode) {
    debugger;
    var TabIndex;
    if (validateSubject()) {
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
                    url: '/TrainingModule/TrainingSubject/Save',
                    dataType: 'json',
                    data: $('#subjectSave').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                CloseSlider('subjectSaveclose');
                                var url = "/TrainingModule/TrainingSubject/GetTrainingList?TabIndex=" + currentTab;
                                $('#ActiveList').load(url, function () { });

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
    //var url = "/TrainingModule/Region/GetRegionList";
    //$('#ActiveList').load(url, { TabIndex: currentTab }, function () { hideSpinner(); });
    $.ajax({

        type: 'GET',
        url: '/TrainingModule/TrainingSubject/GetTrainingList',
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

        if ($("#TrainingList_" + index + "__EditActive").val() == 'True')
            swal.fire(ApprovalPendingMsg);


        else {
            $.ajax({

                type: 'GET',
                url: '/TrainingModule/TrainingSubject/Edit',
                dataType: 'html',
                data: { TrainingSubjectGI: GI, LogGI: LogGI },
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
        //var url = "/TrainingModule/Region/Create";
        //$('#newRequestSlider').load(url, function () { });
        //$('body').append('<div class="form-overlay"></div>');
        var url = "/TrainingModule/TrainingSubject/Create";
        $.ajax({

            type: 'GET',
            url: '/TrainingModule/TrainingSubject/Create',
            dataType: 'html',
            data: { TrainingSubjectGI: GI, LogGI: LogGI },
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
function validateSubject() {
    var i = 0;
    if (!validateById('#Name', 'SubjectName')) {
        i++;
    }
    //if (!validateById('#ShortName', 'SubjectShortName')) {
    //    i++;
    //}

    if (i == 0)
        return true;
    else
        return false;
}
function onDeleteClick(GI, LogGI, Type, index) {

    debugger;

    if ($("#TrainingList_" + index + "__EditActive").val() == 'True')
        swal.fire(ApprovalPendingMsg);


    else {
        //var url = "/TrainingModule/Region/Delete?RegionGI=" + GI + "&LogGI=" + LogGI;
        //$('#deleteRegSlider').load(url, function () { });
        //$('body').append('<div class="form-overlay"></div>');

        $.ajax({
            type: 'GET',
            url: '/TrainingModule/TrainingSubject/Delete',
            dataType: 'html',
            data: { TrainingSubjectGI: GI, LogGI: LogGI },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('#deleteRegSlider').html("");
                    $('#deleteRegSlider').html(result);
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

    if ($("#TrainingList_" + index + "__MakerChecker").val() == 'True')
        swal.fire(MakerCheckerMsg);

    else {

        debugger;
        //var url = "/TrainingModule/Region/Approve?RegionGI=" + GI + "&LogGI=" + LogGI;
        //$('#approveSlider').load(url, function () { });
        //$('body').append('<div class="form-overlay"></div>');
        $.ajax({

            type: 'GET',
            url: '/TrainingModule/TrainingSubject/Approve',
            dataType: 'html',
            data: { TrainingSubjectGI: GI, LogGI: LogGI },
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
function ConfirmSubjectApprove(GI, LogGI) {
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
                url: '/TrainingModule/TrainingSubject/Approve',
                dataType: 'json',
                data: $('#approve').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('PendingApprove');
                            var url = "/TrainingModule/TrainingSubject/GetTrainingList?TabIndex=" + 'APPROVAL_PENDING';
                            $('#ActiveList').load(url, function () { });
                            $(".PendingTab").addClass("active");
                            $(".ActiveTab").removeClass("active");
                            $(".InctiveTab").removeClass("active");
                            $("#ActiveList").removeClass("hide");
                        });

                    }
                    else {
                        debugger;
                        if (result.message == "MAKER_CHECKER") {
                            swal.fire("Maker and checker cannot be same.")
                        }
                        else {
                            swal.fire(result.message).then(function () {


                            });
                        }
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
function ConfirmSubjectReject(GI, LogGI) {
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
                url: '/TrainingModule/TrainingSubject/Reject',
                dataType: 'json',
                data: $('#approve').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('PendingApprove');
                            var url = "/TrainingModule/TrainingSubject/GetTrainingList?TabIndex=" + 'APPROVAL_PENDING';
                            $('#ActiveList').load(url, function () { });
                            $(".PendingTab").addClass("active");
                            $(".ActiveTab").removeClass("active");
                            $(".InctiveTab").removeClass("active");
                            $("#ActiveList").removeClass("hide");
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
                url: '/TrainingModule/TrainingSubject/Delete',
                dataType: 'json',
                data: $('#deleteSubjectmaster').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('deleteRegion');
                            var url = "/TrainingModule/TrainingSubject/GetTrainingList?TabIndex=" + 'ACTIVE';
                            $('#ActiveList').load(url, function () { });
                            $(".PendingTab").removeClass("active");
                            $(".ActiveTab").addClass("active");
                            $("#ActiveList").removeClass("hide");
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/TrainingModule/TrainingSubject/Delete?TrainingSubjectGI=" + GI + "&LogGI=" + LogGI;
                            $('#deleteRegion').load(url, function () { });
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
function CloseSlider(id) {
    debugger;
    $("." + id).removeClass('slider-forms-open');
    $(".form-overlay").remove();
}
function onExportToExcelClick() {
    debugger;
    if (currentTab == 'APPROVAL_PENDING') {
        var pendingTableLength = $('#TrainingList_Count').val();
        if (pendingTableLength > 0) {
            url = "/TrainingModule/TrainingSubject/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }

    else {
        var activeTableLength = $('#TrainingList_Count').val();
        if (activeTableLength > 0) {
            url = "/TrainingModule/TrainingSubject/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }

}
