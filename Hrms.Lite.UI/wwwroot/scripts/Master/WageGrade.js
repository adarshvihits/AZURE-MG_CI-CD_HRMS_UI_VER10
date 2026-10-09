var prevTab = 'ActiveTab';
var currentTab='ACTIVE';



function ontabClick(Tab, TabIndex) {
    debugger;
    showSpinner();
    currentTab = TabIndex;
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");
    $('#tabsDropdownWageGrade').val(currentTab);

    $.ajax({

        type: 'GET',
        url: '/Master/WageGrade/GetWageGradeList',
        dataType: 'html',
        data: { TabIndex: TabIndex },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#WageGradelist').html("");
                $('#WageGradelist').html(result);
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





//function ontabClick(Tab, TabIndex) {
//    debugger;
//    showSpinner();
//    currentTab = TabIndex;
//    $("." + prevTab).removeClass("active");
//    $("." + Tab).addClass("active");
//    var url = "/Master/WageGrade/GetWageGradeList";
//    $('#tabsDropdownWageGrade').val(currentTab);
//    $('#WageGradelist').load(url, { TabIndex: TabIndex }, function () { hideSpinner(); });

//    prevTab = Tab;
//}

function tabClick(val) {
    debugger;
    currentTab = $(val).val();
    showSpinner();
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
        url: '/Master/WageGrade/GetWageGradeList',
        dataType: 'html',
        data: { TabIndex: TabIndex },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#WageGradelist').html("");
                $('#WageGradelist').html(result);
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
    //var url = "/Master/WageGrade/GetWageGradeList";
    //$('#WageGradelist').load(url, { TabIndex: currentTab }, function () { hideSpinner(); });

}


function onAddNewOrEditClick(Type, GI, LogGI, index) {
    debugger;
    //currentTab = 'ACTIVE';
    if (Type == 'MODIFY') {

        if ($("#WageGradeList_" + index + "__EditActive").val() == 'True')
            swal.fire("Approval Pending");


        else {
            $.ajax({

                type: 'GET',
                url: '/Master/WageGrade/Edit',
                dataType: 'html',
                data: { WageGradeGI: GI, LogGI: LogGI },
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

    else {
        var url = "/Master/WageGrade/Create";
        $.ajax({

            type: 'GET',
            url: '/Master/WageGrade/Create',
            dataType: 'html',
            data: { WageGradeGI: GI, LogGI: LogGI },
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
    //if (Type == 'MODIFY') {

    //    if ($("#WageGradeList_" + index + "__EditActive").val() == 'True')
    //        swal.fire("APPROVAL PENDING");


    //    else {
    //        var url = "/Master/WageGrade/Edit?WageGradeGI=" + GI + "&LogGI=" + LogGI;

    //        $('#CreateOrEditSlider').load(url, function () { });
    //        $('body').append('<div class="form-overlay"></div>');
    //    }

    //}

    //else {
    //    var url = "/Master/WageGrade/Create";
    //    $('#CreateOrEditSlider').load(url, function () { });
    //    $('body').append('<div class="form-overlay"></div>');
    //}


}




//function AddNewOrEdit(Action, GI, LogGI) {
//    debugger;
//    if (Action == 'Edit') {
//        var url = "/Master/WageGrade/Edit?WageGradeGI=" + GI + "&LogGI=" + LogGI;
//    }
//    else {
//        var url = "/Master/WageGrade/Create";
//    }
//    $('#CreateOrEditSlider').load(url, function () { });  
//    $('body').append('<div class="form-overlay"></div>');

//}



function onApproveClick(GI, LogGI, Type, index) {

    debugger;


    if ($("#WageGradeList_" + index + "__MakerChecker").val() == 'True')
        swal.fire(MakerCheckerMsg);

    else {

        $.ajax({

            type: 'GET',
            url: '/Master/WageGrade/Approve',
            dataType: 'html',
            data: { WageGradeGI: GI, LogGI: LogGI },
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




    //if ($("#WageGradeList_" + index + "__MakerChecker").val() == 'True')
    //    swal.fire("MAKER AND CHECKER SHOULD BE DIFFERENT");

    //else {
    //    var url = "/Master/WageGrade/Approve?WageGradeGI=" + GI + "&LogGI=" + LogGI;

    //    $('#ApproveSlider').load(url, function () { });
    //    $('body').append('<div class="form-overlay"></div>');
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
                url: '/Master/WageGrade/Approve',
                dataType: 'json',
                data: $('#approve').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('ApproveDiv');
                            var url = "/Master/WageGrade/GetWageGradeList?TabIndex=" + 'APPROVAL_PENDING';
                            $('#WageGradelist').load(url, function () { });
                            $(".PendingTab").addClass("active");
                            $(".ActiveTab").removeClass("active");
                            $("#WageGradelist").removeClass("hide");
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
                url: '/Master/WageGrade/Reject',
                dataType: 'json',
                data: $('#approve').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('ApproveDiv');
                            var url = "/Master/WageGrade/GetWageGradeList?TabIndex=" + 'APPROVAL_PENDING';
                            $('#WageGradelist').load(url, function () { });
                            $(".PendingTab").addClass("active");
                            $(".ActiveTab").removeClass("active");
                            $("#WageGradelist").removeClass("hide");
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

//function validateWageGrade() {
//    debugger;
//    if ($('.wName').val() == '') {

//        $('#Name').addClass('is-invalid');
//        $("#WageGradeName").addClass('error-message');
//        $('#WageGradeName').html("This field is required");
//        $('#Name').focus();
//        return false;
//    }

//    else if ($('.wShtName').val() == '') {

//        $('#ShortName').addClass('is-invalid');
//        $("#WageGradeShortName").addClass('error-message');
//        $('#WageGradeShortName').html("This field is required");
//        $('#ShortName').focus();
//        return false;
//    }
   
//    else
//        return true;
//}

function validateWageGrade() {
    var i = 0;
    if (!validateById('#Name', 'WageGradeName')) {
        i++;
    }
    if (!validateById('#ShortName', 'WageGradeShortName')) {
        i++;
    }

    if (!validateById('#NonMetro', 'NonMetroCTC')) {
        i++;
    }
    if (!validateById('#Metro', 'MetroCTC')) {
        i++;
    }

    if (i == 0)
        return true;
    else
        return false;
}



function onDeleteClick(GI, LogGI, Type, index) {

    debugger;

    if ($("#WageGradeList_" + index + "__EditActive").val() == 'True')
        swal.fire(ApprovalPendingMsg);


    else {

        $.ajax({
            type: 'POST',
            url: '/Master/WageGrade/Delete',
            dataType: 'html',
            data: { WageGradeGI: GI, LogGI: LogGI },
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
    //if ($("#WageGradeList_" + index + "__EditActive").val() == 'True')
    //    swal.fire("APPROVAL PENDING");


    //else {
    //    var url = "/Master/WageGrade/Delete?WageGradeGI=" + GI + "&LogGI=" + LogGI;

    //    $('#DeleteSlider').load(url, function () { });
    //    $('body').append('<div class="form-overlay"></div>');
    //}
}

//function Delete(GI, LogGI) {

//    debugger;

//    var url = "/Master/WageGrade/Delete?WageGradeGI=" + GI + "&LogGI=" + LogGI;
//    $('#DeleteSlider'.load(url, function () { });
//    $('body').append('<div class="form-overlay"></div>');
    
//}
//function onDeleteClick(GI) {
//    var url = "/Master/WageGrade/DeleteWageGrade?GI=" + GI;
//    window.location.href = url;
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
                url: '/Master/WageGrade/DeleteWageGrade',
                dataType: 'json',
                data: $('#deleteWageGrade').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('delete');
                            var url = "/Master/WageGrade/GetWageGradeList?TabIndex=" + 'APPROVAL_PENDING';
                            $('#WageGradelist').load(url, function () { });
                            $(".PendingTab").removeClass("active");
                            $(".ActiveTab").addClass("active");
                            $("#WageGradelist").removeClass("hide");
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/WageGrade/Delete?WageGradeGI=" + GI + "&LogGI=" + LogGI;
                            $('#delete').load(url, function () { });
                            $('body').append('<div class="form-overlay"></div>');

                        });
                    }

                },
                error: function (xhr, textStatus, errorThrown) {
                    debugger
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


function ExportToExcel() {
    debugger;
    if (currentTab == 'APPROVAL_PENDING') {
        var pendingTableLength = $('#WageGradeList_Count').val();
        if (pendingTableLength > 0) {
            url = "/Master/WageGrade/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }

    else if (currentTab=='INACTIVE')
    {
        var activeTableLength = $('#WageGradeList_Count').val();
        if (activeTableLength > 0) {
            url = "/Master/WageGrade/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }

    else  {
        var activeTableLength = $('#WageGradeList_Count').val();
        if (activeTableLength > 0) {
            url = "/Master/WageGrade/ExportToExcel?TabIndex=" + 'ACTIVE';
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }
   
}


//function ExportToExcel() {

//    debugger;
//    if (currentTab == 'APPROVAL_PENDING') {
//        var pendingTableLength = document.getElementById("WageGradePendingTable").rows.length - 1;
//        if (pendingTableLength > 0) {
//            url = "/Master/WageGrade/ExportToExcel?TabIndex=" + currentTab;
//            window.location.href = url;
//        }
//        else {
//            swal.fire("No data found to export");
//        }
//    }

//    else if (currentTab == 'ACTIVE') {
//        var activeTableLength = document.getElementById("WageGradetable").rows.length - 1;
//        if (activeTableLength > 0) {
//            url = "/Master/WageGrade/ExportToExcel?TabIndex=" + currentTab;
//            window.location.href = url;
//        }
//        else {
//            swal.fire("No data found to export");
//        }
//    }
//    else if (currentTab == 'INACTIVE') {
//        var inActiveTableLength = document.getElementById("WageGradetable").rows.length - 1;
//        if (inActiveTableLength > 0) {
//            url = "/Master/WageGrade/ExportToExcel?TabIndex=" + currentTab;
//            window.location.href = url;
//        }
//        else {
//            swal.fire("No data found to export");
//        }
//    }
//}




function SaveWageGrade(mode) {
    debugger;
    if (validateWageGrade()) {

        //console.log($('#WageGradeId').serialize());
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
                    url: '/Master/WageGrade/Save',
                    dataType: 'json',
                    data: $('#WageGradeId').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                CloseSlider('AddNewOrEdit');
                                debugger;
                                var url = "/Master/WageGrade/GetWageGradeList?TabIndex=" + currentTab;
                                $('#WageGradelist').load(url, function () { });
                                
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

function ActiveInactiveValidationWage(name, code) {
    debugger

    $.ajax({

        type: 'POST',
        url: '/Master/WageGrade/GetActiveInactiveValidation?name=' + name + '&code=' + code,
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
                    $("#wageActive").prop("checked", true);
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