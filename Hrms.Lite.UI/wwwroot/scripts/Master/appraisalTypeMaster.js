var prevTab = 'tab1';
var currentTab = 'ACTIVE';
function tabClick(Tab, TabIndex) {
    debugger;
    showSpinner();
    currentTab = TabIndex;
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");
    $('#tabsDropdownAppraisalType').val(currentTab);
    //var url = "/Master/AppraisalTypeMaster/GetAppraisalTypeMasterList";
    //$('#appraisalTypeList').load(url, { TabIndex: TabIndex }, function () { hideSpinner(); });



    $.ajax({

        type: 'GET',
        url: '/Master/AppraisalTypeMaster/GetAppraisalTypeMasterList',
        dataType: 'html',
        data: { TabIndex: TabIndex },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#appraisalTypeList').html("");
                $('#appraisalTypeList').html(result);
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


function OntabClick(val) {
    debugger;
    showSpinner();
    currentTab = $(val).val();
    $("." + prevTab).removeClass("active");
    if (currentTab == "ACTIVE")
        prevTab = 'tab1'
    else if (currentTab == "INACTIVE")
        prevTab = 'tab2';
    else
        prevTab = 'tab2';

    $("." + prevTab).addClass("active");
    //var url = "/Master/AppraisalTypeMaster/GetAppraisalTypeMasterList";
    //$('#appraisalTypeList').load(url, { TabIndex: currentTab }, function () { hideSpinner(); });


    $.ajax({

        type: 'GET',
        url: '/Master/AppraisalTypeMaster/GetAppraisalTypeMasterList',
        dataType: 'html',
        data: { TabIndex: currentTab },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#appraisalTypeList').html("");
                $('#appraisalTypeList').html(result);
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

//function onAddNewClick() {
//    debugger;
//    var url = "/Master/AppraisalTypeMaster/Create";
//    $('#appraisalTypeCreate').load(url, {}, function () { });
//    $('#appraisalTypeCreate').removeClass('hide');
//}




function onAddNewOrEditClick(Type, GI, LogGI, index) {
    debugger;
    currentTab = 'ACTIVE';

    if (Type == 'MODIFY') {

        if ($("#AppraisalTypeMasterList_" + index + "__EditActive").val() == 'True')
            swal.fire(ApprovalPendingMsg);


        else {
            //var url = "/Master/AppraisalTypeMaster/Edit?AppraisalTypeGI=" + GI + "&LogGI=" + LogGI;

            //$('#appraisalTypeCreate').load(url, function () { });
            //$('body').append('<div class="form-overlay"></div>');

            $.ajax({

                type: 'GET',
                url: '/Master/AppraisalTypeMaster/Edit',
                dataType: 'html',
                data: { AppraisalTypeGI: GI, LogGI: LogGI },
                success: function (result) {
                    debugger
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }
                    else {
                        $('body').append('<div class="form-overlay"></div>');
                        $('#appraisalTypeCreate').html("");
                        $('#appraisalTypeCreate').html(result);
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
        //var url = "/Master/AppraisalTypeMaster/Create";
        //$('#appraisalTypeCreate').load(url, function () { });
        //$('body').append('<div class="form-overlay"></div>');

        $.ajax({

            type: 'GET',
            url: '/Master/AppraisalTypeMaster/Create',
            dataType: 'html',
            //data: { AppraisalTypeGI: GI, LogGI: LogGI },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('#appraisalTypeCreate').html("");
                    $('#appraisalTypeCreate').html(result);
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



//function onEditClick(AppraisalTypeGI) {
//    debugger
//    $('body').append('<div class="form-overlay"></div>');
//    var url = "/Master/AppraisalTypeMaster/Edit";
//    $('#appraisalTypeCreate').load(url, { AppraisalTypeGI: AppraisalTypeGI}, function () { });
//    $('#appraisalTypeCreate').removeClass('hide');
//}


//function onApproveClick(GI, LogGI, Mode) {
//    debugger;
//    $('body').append('<div class="form-overlay"></div>');
//    var url = "/Master/AppraisalTypeMaster/Approve";
//   // url = url + "?GI=" + GI + "&LogGI=" + LogGI +"&Mode=" + Mode;
//    //window.location.href = url;
//    $('#approveAppraisalMaster').load(url, { GI: GI, LogGI: LogGI, Mode: Mode }, function () { });
//}



function onApproveClick(GI, LogGI, Type, index) {

    debugger;

    if ($("#AppraisalTypeMasterList_" + index + "__MakerChecker").val() == 'True')
        swal.fire(MakerCheckerMsg);

    else {

        debugger;
        //var url = "/Master/AppraisalTypeMaster/Approve?AppraisalTypeGI=" + GI + "&LogGI=" + LogGI;
        //$('#ApproveSlider').load(url, function () { });
        //$('body').append('<div class="form-overlay"></div>');

        $.ajax({

            type: 'GET',
            url: '/Master/AppraisalTypeMaster/Approve',
            dataType: 'html',
            data: { GI: GI, LogGI: LogGI },
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




function onDeleteClick(GI, LogGI, Type, index) {

    debugger;

    if ($("#AppraisalTypeMasterList_" + index + "__EditActive").val() == 'True')
        swal.fire(ApprovalPendingMsg);


    else {
        //var url = "/Master/AppraisalTypeMaster/DeleteGetDetails?AppraisalTypeGI=" + GI;
        //$('#deleteSlider').load(url, function () { });
        //$('body').append('<div class="form-overlay"></div>');

        $.ajax({
            type: 'GET',
            url: '/Master/AppraisalTypeMaster/DeleteGetDetails',
            dataType: 'html',
            data: { AppraisalTypeGI: GI},
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('#deleteSlider').html("");
                    $('#deleteSlider').html(result);
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




//function OnDeleteClick(GI) {
//    debugger
//    var url = "/Master/AppraisalTypeMaster/DeleteGetDetails";
//    $('#appraisalTypeCreate').load(url, { AppraisalTypeGI: GI }, function () { });
//    $('#appraisalTypeCreate').removeClass('hide');
//}

//function Delete(GI) {
//    debugger
//    var url = "/Master/AppraisalTypeMaster/Delete" + "?AppraisalTypeGI=" + GI;
//    //window.location.href = url + "?AppraisalTypeGI=" + GI;
//    $.getJSON(url, function (result) {
//        swal.fire(result.message).then(function () {
//            CloseSlider('appraisalDelete');
//            if (result.success) {
//                var url = "/Master/AppraisalTypeMaster/Index";
//                window.location.href = url;
//            }
//        })
//    })
//}

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
                url: '/Master/AppraisalTypeMaster/Delete',
                dataType: 'json',
                data: { AppraisalTypeGI:GI},
                success: function (result) {
                    debugger
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('appraisalDelete');
                            var url = "/Master/AppraisalTypeMaster/GetAppraisalTypeMasterList?TabIndex=" + 'ACTIVE';
                            $('#appraisalTypeList').load(url, function () { });
                            $(".PendingTab").removeClass("active");
                            $(".ActiveTab").addClass("active");
                            $("#appraisalTypeList").removeClass("hide");
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/AppraisalTypeMaster/DeleteGetDetails?AppraisalTypeGI=" + GI + "&LogGI=" + LogGI;
                            $('#deleteSlider').load(url, function () { });
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
                url: '/Master/AppraisalTypeMaster/ApproveAppraisal',
                dataType: 'json',
                data: $('#appraisalApproveOrReject').serialize(),
                success: function (result) {

                    if (result.success == true) {
                        debugger
                        swal.fire(result.message).then(function () {
                            CloseSlider('appraisalTypeMasterApprove');
                            var url = "/Master/AppraisalTypeMaster/GetAppraisalTypeMasterList?TabIndex=" + 'APPROVAL_PENDING';
                            $('#appraisalTypeList').load(url, function () { });
                            $(".PendingTab").removeClass("active");
                            $(".ActiveTab").addClass("active");
                            $("#appraisalTypeList").removeClass("hide");
                        });

                    }

                    else {
                        debugger;
                        swal.fire(result.message).then(function () {
                            var url = "/Master/AppraisalTypeMaster/Approve?AppraisalTypeGI=" + GI + "&LogGI=" + LogGI;
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
                url: '/Master/AppraisalTypeMaster/Reject',
                dataType: 'json',
                data: $('#appraisalApproveOrReject').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('appraisalTypeMasterApprove');
                            var url = "/Master/AppraisalTypeMaster/GetAppraisalTypeMasterList?TabIndex=" + 'APPROVAL_PENDING';
                            $('#appraisalTypeList').load(url, function () { });
                            $(".PendingTab").addClass("active");
                            $(".ActiveTab").removeClass("active");
                            $("#appraisalTypeList").removeClass("hide");
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/AppraisalTypeMaster/Approve?AppraisalTypeGI=" + GI + "&LogGI=" + LogGI;
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









function onExportToExcelClick() {
    debugger;

    if (currentTab != "APPROVAL_PENDING") {

        var appraisaltypetableLength = $('#AppraisalTypeMasterList_Count').val();
        if (appraisaltypetableLength > 0) {
            url = "/Master/AppraisalTypeMaster/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }
    else {
        var appraisalTypePendingTableLength = $('#AppraisalTypeMasterList_Count').val();
        if (appraisalTypePendingTableLength > 0) {
            url = "/Master/AppraisalTypeMaster/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }
}

function appraisalSave(mode) {
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
                    url: '/Master/AppraisalTypeMaster/Save',
                    dataType: 'json',
                    data: $('#appraisal-save-slider').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                CloseSlider('appraisalCreate');
                                var url = "/Master/AppraisalTypeMaster/Index";
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
                                //CloseSlider('appraisalCreate');
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


//function validateAll() {
//    if (!validateById('#Name', 'AName')) {
//        return false;
//    }
//    else if (!validateById('#ShortName', 'AShortName')) {
//        return false;
//    }  
//    else if (!validateById('#AssesmentMode_Code', 'TMode')) {
//        return false;
//    }
//    else {
//        return true;
//    }
//}

function validateAll() {
    var i = 0;
    if (!validateById('#Name', 'AName')) {
        i++;
    }
    if (!validateById('#ShortName', 'AShortName')) {
        i++;
    }
    if (!validateById('#AssesmentMode_Code', 'AMode')) {
        i++;
    }
    if (i == 0)
        return true;
    else
        return false;
}





function ActiveInactiveValidationAppType(name, code) {
    debugger

    $.ajax({

        type: 'POST',
        url: '/Master/AppraisalTypeMaster/GetActiveInactiveValidation?name=' + name + '&code=' + code,
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
                    $("#type").prop("checked", true);
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