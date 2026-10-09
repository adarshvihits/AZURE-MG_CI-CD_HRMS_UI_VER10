var prevTab = 'tab1';
var currentTab = 'ACTIVE';
function onAddNewClick() {
    debugger;
    var url = "/Master/TrainingMaster/Create";
    $('#training-master-create').load(url, {}, function () { });
    $('#training-master-create').removeClass('hide');
}

function onAddNewOrEditClick(Type, GI, LogGI, index) {
    debugger;
    //currentTab = 'ACTIVE';
    console.log($("#TrainingMasterList_" + index + "__EditActive").val());
    if (Type == 'MODIFY') {

        if ($("#TrainingMasterList_" + index + "__EditActive").val() == 'True')
            swal.fire(ApprovalPendingMsg);


        else {
            //var url = "/Master/TrainingMaster/Edit?GI=" + GI + "&LogGI=" + LogGI;

            //$('#training-master-create').load(url, function () { });
            //$('body').append('<div class="form-overlay"></div>');
            $.ajax({

                type: 'GET',
                url: '/Master/TrainingMaster/Edit',
                dataType: 'html',
                data: { GI: GI, LogGI: LogGI },
                success: function (result) {
                    debugger
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }
                    else {
                        $('body').append('<div class="form-overlay"></div>');
                        $('#training-master-create').html("");
                        $('#training-master-create').html(result);
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
        //var url = "/Master/TrainingMaster/Create";
        //$('#training-master-create').load(url, function () { });
        //$('body').append('<div class="form-overlay"></div>');
        var url = "/Master/TrainingMaster/Create";
        $.ajax({

            type: 'GET',
            url: '/Master/TrainingMaster/Create',
            dataType: 'html',
           /* data: { DepartmentGI: GI, LogGI: LogGI },*/
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('#training-master-create').html("");
                    $('#training-master-create').html(result);
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



function firstLetterCaps(val, id) {

    debugger;
    stringval = val.value;
    if (val.value == "") {
        $(val).addClass('is-invalid');
        $("#" + id + "").addClass('error-message');
        $("#" + id + "").html("This field is required");
    }
    else {
        $(val).removeClass('is-invalid');
        $("#" + id + "").removeClass('error-message');
        $("#" + id + "").html("");
        val.value = val.value.toLowerCase();
        val.value = val.value.
            replace(/(^\s*)|(\s*$)/gi, ""). // removes leading and trailing spaces
            replace(/[ ]{2,}/gi, "").       // replaces multiple spaces with one space 
            replace(/\n +/, "\n");
        val.value = val.value.substr(0, 1).toUpperCase() + val.value.substr(1);
        return val.value;
    }
}

function ontabClick(Tab, TabIndex) {
    debugger;
    showSpinner();
    currentTab = TabIndex;
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");    
   /* var url = "/Master/TrainingMaster/GetTrainingMasterList";*/
    $('#tabsDropdownTraining').val(currentTab);
    /*  $('#trainingMasterList').load(url, { TabIndex: TabIndex }, function () { hideSpinner(); });*/


    $.ajax({

        type: 'GET',
        url: '/Master/TrainingMaster/GetTrainingMasterList',
        dataType: 'html',
        data: { TabIndex: TabIndex },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#trainingMasterList').html("");
                $('#trainingMasterList').html(result);
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
    //var url = "/Master/TrainingMaster/GetTrainingMasterList";
    //$('#trainingMasterList').load(url, { TabIndex: currentTab }, function () { hideSpinner(); });
    $.ajax({

        type: 'GET',
        url: '/Master/TrainingMaster/GetTrainingMasterList',
        dataType: 'html',
        data: { TabIndex: TabIndex },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#trainingMasterList').html("");
                $('#trainingMasterList').html(result);
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

function onEditClick(GI) {
    debugger;
    //$('body').append('<div class="form-overlay"></div>');
    //var url = "/Master/TrainingMaster/Edit";
    //$('#editTraining').load(url, { GI: GI}, function () { });

    $.ajax({

        type: 'GET',
        url: '/Master/TrainingMaster/Edit',
        dataType: 'html',
        data: { GI: GI },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('body').append('<div class="form-overlay"></div>');
                $('#editTraining').html("");
                $('#editTraining').html(result);
            }


        },


        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);


        }
    });




}
//function onDeleteClick(GI) {
//    debugger;
//    var url = "/Master/TrainingMaster/Delete";

//    $('#editTraining').load(url, { GI: GI }, function () { });
//}






function onDeleteClick(GI, LogGI, Type, index) {

    debugger;

    if ($("#TrainingMasterList_" + index + "__EditActive").val() == 'True')
        swal.fire(ApprovalPendingMsg);


    else {
        //var url = "/Master/TrainingMaster/Delete?GI=" + GI + "&LogGI=" + LogGI;

        //$('#editTraining').load(url, function () { });
        //$('body').append('<div class="form-overlay"></div>');
        $.ajax({
            type: 'GET',
            url: '/Master/TrainingMaster/Delete',
            dataType: 'html',
            data: { GI: GI, LogGI: LogGI },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('#editTraining').html("");
                    $('#editTraining').html(result);
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












//function Delete(GI) {

//    var url = "/Master/TrainingMaster/DeleteTraining" + "?GI=" + GI;
//    $.getJSON(url, function (result) {
//        swal.fire(result.message).then(function () {
//            CloseSlider('deleteTraining');
//            if (result.success) {
//                var url = "/Master/TrainingMaster/Index";
//                window.location.href = url;
//            }
//        })
//    })
//    //window.location.href = url + "?GI=" + GI;
//}


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
                url: '/Master/TrainingMaster/DeleteTraining',
                dataType: 'json',
                data: $('#delete-Training').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('deleteTraining');
                            var url = "/Master/TrainingMaster/GetTrainingMasterList?TabIndex=" + 'ACTIVE';
                            $('#trainingMasterList').load(url, function () { });
                            $(".PendingTab").removeClass("active");
                            $(".ActiveTab").addClass("active");
                            $("#trainingMasterList").removeClass("hide");
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/TrainingMaster/DeleteTraining?GI=" + GI + "&LogGI=" + LogGI;
                            $('#deleteTraining').load(url, function () { });
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
    if ($("#TrainingMasterList_" + index + "__MakerChecker").val() == 'True')
        swal.fire(MakerCheckerMsg);

    else {
        //var url = "/Master/TrainingMaster/Approve?GI=" + GI + "&LogGI=" + LogGI;

        //$('#approveTraining').load(url, function () { });
        //$('body').append('<div class="form-overlay"></div>');
        $.ajax({

            type: 'GET',
            url: '/Master/TrainingMaster/Approve',
            dataType: 'html',
            data: { GI: GI, LogGI: LogGI },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('#approveTraining').html("");
                    $('#approveTraining').html(result);
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




function trainingSave(mode) {
    if (validateAll()) {
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
                    url: '/Master/TrainingMaster/Save',
                    dataType: 'json',
                    data: $('#training-master-save').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                CloseSlider('training-create-slider');
                                var url = "/Master/TrainingMaster/Index";
                                window.location.href = url;
                            });

                        }
                        else if (result.message == "MAKER_CHECKER") {
                            swal.fire("Maker and checker cannot be same.").then(function () {
                                //var url = "/Master/Country/Approve?CountryGI=" + GI + "&LogGI=" + LogGI;
                                //$('#ApproveSlider').load(url, function () { });
                                //$('body').append('<div class="form-overlay"></div>');

                            });
                        }
                        else {
                            swal.fire(result.message).then(function () {
                                //var url = "/Master/Country/Approve?CountryGI=" + GI + "&LogGI=" + LogGI;
                                //$('#ApproveSlider').load(url, function () { });
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
    else {
        return false;
    }
}


function confirmApprove() {
    debugger;
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
                url: '/Master/TrainingMaster/ApproveTraining',
                dataType: 'json',
                data: $('#ApproveOrRejectTraining').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('training-approve-slider');

                            var url = "/Master/TrainingMaster/GetTrainingMasterList?TabIndex=" + 'APPROVAL_PENDING';
                            $('#trainingMasterList').load(url, function () { });

                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            debugger;
                            var url = "/Master/TrainingMaster/ApproveTraining?GI=" + GI + "&LogGI=" + LogGI;
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

function confirmReject() {

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
                url: '/Master/TrainingMaster/Reject',
                dataType: 'json',
                data: $('#ApproveOrRejectTraining').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('training-approve-slider');
                            var url = "/Master/TrainingMaster/GetTrainingMasterList?TabIndex=" + 'APPROVAL_PENDING';
                           /* window.location.href = url;*/
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/TrainingMaster/Approve?GI=" + GI + "&LogGI=" + LogGI;
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
function validateAll() {
    var i = 0;
    if (!validateById('#Name', 'TName')) {
        i++;
    }
    //if (!validateById('#ShortName', 'TShortName')) {
    //    i++;
    //}
    if (!validateById('#TrainingType_Code', 'TType')) {
        i++;
    }
    if (!validateById('#AssesmentMode_Code', 'TMode')) {
        i++;
    }
    if (!validateById('#TrainingCategory_Code', 'TCategory')) {
        i++;
    }
    if (i == 0)
        return true;
    else
        return false;
}





function onExportToExcelClick() {
    debugger;   

    if (currentTab != "APPROVAL_PENDING") {

        var trainingMasterTableLength = $('#TrainingMasterList_Count').val();
        if (trainingMasterTableLength > 0) {
            url = "/Master/TrainingMaster/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }
    else {
        var trainingMasterPendingTableLength = $('#TrainingMasterList_Count').val();
        if (trainingMasterPendingTableLength > 0) {
            url = "/Master/TrainingMaster/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }
}


function ActiveInactiveValidationTraining(name, code) {
    debugger

    $.ajax({

        type: 'POST',
        url: '/Master/TrainingMaster/GetActiveInactiveValidation?name=' + name + '&code=' + code,
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
                    $("#training").prop("checked", true);
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

