var prevTab = 'ActiveTab';
var currentTab = 'ACTIVE';




function OnTabClick(Tab, TabIndex) {
    debugger;
    showSpinner();
    currentTab = TabIndex;
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");
    $('#tabsDropdownDepartment').val(currentTab);

    $.ajax({

        type: 'GET',
        url: '/Master/Consultancy/GetConsultancyList',
        dataType: 'html',
        data: { TabIndex: TabIndex },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#ConsultancyList').html("");
                $('#ConsultancyList').html(result);
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
        url: '/Master/Consultancy/GetConsultancyList',
        dataType: 'html',
        data: { TabIndex: TabIndex },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#ConsultancyList').html("");
                $('#ConsultancyList').html(result);
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









function AddNewOrEditClick(Type, GI, LogGI, index) {
    debugger;

    if (Type == 'MODIFY') {

        if ($("#ConsultancyList_" + index + "__EditActive").val() == 'True')
            swal.fire(ApprovalPendingMsg);


        else {
            $.ajax({

                type: 'GET',
                url: '/Master/Consultancy/Edit',
                dataType: 'html',
                data: { ConsultancyGI: GI, LogGI: LogGI },
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
        var url = "/Master/Consultancy/Create";
        $.ajax({

            type: 'GET',
            url: '/Master/Consultancy/Create',
            dataType: 'html',
            data: { ConsultancyGI: GI, LogGI: LogGI },
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


function validateConsultancy() {
    var i = 0;
    if (!validateById('#Name', 'ConsultancyName')) {
        i++;
    }
    if (!validateById('#ShortName', 'ConsultancyShortName')) {
        i++;
    }
    if (!validateById('#ValidFrom', 'ValidFromDate')) {
        i++;
    }
    if (!validateById('#ValidTo', 'ValidToDate')) {
        i++;
    }
    if (!validateById('#ContactPerson', 'Person')) {
        i++;
    }
    
    if (!validateById('#MobileNo', 'MobileNumber')) {
        i++;
    }

    if (i == 0)
        return true;
    else
        return false;
}




function ValidateDate() {
    debugger;
    var fromDate = $('#ValidFrom').val();
    var toDate = $('#ValidTo').val();
    var D1 = new Date(fromDate);
    var D2 = new Date(toDate);
    if (D1 > D2) {
        swal.fire("To date should be greater than From date");
        /*  $("#To").val('');*/



        return false;
    }
    else
        return true;
}


//function ValidateDate(fromtodate) {
//    debugger;
//    flag = true;
//    var fromDate = $('#ValidFrom').val();
//    var toDate = $('#ValidTo').val();
//    var D1 = new Date(fromDate);
//    var D2 = new Date(toDate);
//    if (fromtodate == 'fromdate') {
//        if (D1 > D2) {
//            flag = false;
//            swal.fire("From date should be less than To date");
//        }
//    }
//    else {
//        if (D2 < D1) {
//            flag = false;
//            swal.fire("To date should be less than From date");
//        }
//    }
//    return flag;

//}



function ConsultancySave(mode) {
    debugger;
    var i = 0;
    if (!ValidateDate()) {
        i++;
    }
    if (i == 0) {
    if (validateConsultancy()) {
        
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
                    url: '/Master/Consultancy/Save',
                    dataType: 'json',
                    data: $('#Consultancysave').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                CloseSlider('AddnewOrEditDiv');
                                //OnTabClick(prevTab, currentTab);
                                //tabClick(document.getElementById('tabsDropdownDepartment'))
                                var url = "/Master/Consultancy/GetConsultancyList?TabIndex=" + currentTab;
                                $('#ConsultancyList').load(url, function () { });

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

}



function onDeleteClick(GI, LogGI, Type, index) {

    debugger;

    if ($("#ConsultancyList_" + index + "__EditActive").val() == 'True')
        swal.fire(ApprovalPendingMsg);


    else {

        $.ajax({
            type: 'GET',
            url: '/Master/Consultancy/Delete',
            dataType: 'html',
            data: { ConsultancyGI: GI, LogGI: LogGI },
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
                url: '/Master/Consultancy/ConfirmDelete',
                dataType: 'json',
                data: $('#DeleteConsultancy').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('delete');
                            OnTabClick(prevTab, currentTab);
                            tabClick(document.getElementById('tabsDropdownDepartment'));
                            //var url = "/Master/Department/GetDepartmentList?TabIndex=" + 'ACTIVE';
                            //$('#DepartmentList').load(url, function () { });
                            //$(".PendingTab").removeClass("active");
                            //$(".ActiveTab").addClass("active");
                            //$("#DepartmentList").removeClass("hide");
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/Consultancy/Delete?ConsultancyGI=" + GI + "&LogGI=" + LogGI;
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



function onApproveClick(GI, LogGI, Type, index) {

    debugger;

    if ($("#ConsultancyList_" + index + "__MakerChecker").val() == 'True')
        swal.fire(MakerCheckerMsg);

    else {

        $.ajax({

            type: 'GET',
            url: '/Master/Consultancy/Approve',
            dataType: 'html',
            data: { ConsultancyGI: GI, LogGI: LogGI },
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
                url: '/Master/Consultancy/ConfirmApprove',
                dataType: 'json',
                data: $('#approve').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('Approve');
                            OnTabClick(prevTab, currentTab);
                            tabClick(document.getElementById('tabsDropdownDepartment'));
                            //var url = "/Master/Department/GetDepartmentList?TabIndex=" + 'APPROVAL_PENDING';
                            //$('#DepartmentList').load(url, function () { });
                            //$(".PendingTab").addClass("active");
                            //$(".ActiveTab").removeClass("active");
                            //$("#DepartmentList").removeClass("hide");
                        });

                    }
                    else {
                        debugger
                        swal.fire(result.message).then(function () {
                            var url = "/Master/Consultancy/Approve?ConsultancyGI=" + GI + "&LogGI=" + LogGI;
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
                url: '/Master/Consultancy/Reject',
                dataType: 'json',
                data: $('#approve').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('Approve');
                            OnTabClick(prevTab, currentTab);
                            tabClick(document.getElementById('tabsDropdownDepartment'));
                            //var url = "/Master/Department/GetDepartmentList?TabIndex=" + 'APPROVAL_PENDING';
                            //$('#DepartmentList').load(url, function () { });
                            //$(".PendingTab").addClass("active");
                            //$(".ActiveTab").removeClass("active");
                            //$("#DepartmentList").removeClass("hide");
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/Consultancy/Approve?ConsultancyGI=" + GI + "&LogGI=" + LogGI;
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


function clickOnExportToExcel() {

    debugger;
    if (currentTab == 'APPROVAL_PENDING') {
        var pendingTableLength = $('#ConsultancyList_Count').val();
        if (pendingTableLength > 0) {
            url = "/Master/Consultancy/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }

    else if (currentTab == 'ACTIVE') {
        var activeTableLength = $('#ConsultancyList_Count').val();
        if (activeTableLength > 0) {
            url = "/Master/Consultancy/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }
    else if (currentTab == 'INACTIVE') {
        var inActiveTableLength = $('#ConsultancyList_Count').val();
        if (inActiveTableLength > 0) {
            url = "/Master/Consultancy/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }
}



