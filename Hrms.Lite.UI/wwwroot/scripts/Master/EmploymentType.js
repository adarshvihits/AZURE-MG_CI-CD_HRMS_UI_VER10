

var prevTab = 'ActiveTab';
var currentTab = 'ACTIVE';



function OnTabClick(Tab, TabIndex) {
    debugger;
    $.ajaxSetup({
        async: false
    });
    showSpinner();

    currentTab = TabIndex;
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");
  
  /*  var url = "/Master/EmploymentType/GetEmploymentTypeList";*/
    $('#tabsDropdownEmploymenttype').val(currentTab);
    //if (TabIndex != 'PendingTab')
    //    $('#EmploymentTypeList').load(url, { TabIndex: TabIndex }, function () { hideSpinner(); });
    //else
    //    $('#PendingList').load(url, { TabIndex: TabIndex }, function () { hideSpinner(); });


    $.ajax({

        type: 'GET',
        url: '/Master/EmploymentType/GetEmploymentTypeList',
        dataType: 'html',
        data: { TabIndex: TabIndex },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#EmploymentTypeList').html("");
                $('#EmploymentTypeList').html(result);
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


function ActiveInactiveValidationEmployment(name, code) {
    debugger

    $.ajax({

        type: 'POST',
        url: '/Master/EmploymentType/GetActiveInactiveValidation?name=' + name + '&code=' + code,
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
                    $("#Activeinactiveemptype").prop("checked", true);
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
    //var url = "/Master/EmploymentType/GetEmploymentTypeList";
    //$('#EmploymentTypeList').load(url, { TabIndex: currentTab }, function () { hideSpinner(); });
    $.ajax({

        type: 'GET',
        url: '/Master/EmploymentType/GetEmploymentTypeList',
        dataType: 'html',
        data: { TabIndex: TabIndex },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#EmploymentTypeList').html("");
                $('#EmploymentTypeList').html(result);
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





function AddNewOrEdit(Type, GI, LogGI, index) {
    debugger;
    //currentTab = 'ACTIVE';

    if (Type == 'MODIFY') {

        if ($("#EmploymentTypeList_" + index + "__EditActive").val() == 'True')
            swal.fire(ApprovalPendingMsg);


        else {
            //var url = "/Master/EmploymentType/Edit?EmploymentTypeGI=" + GI + "&LogGI=" + LogGI;

            //$('#AddNewOrEditSlider').load(url, function () { });
            //$('body').append('<div class="form-overlay"></div>');
            $.ajax({

                type: 'GET',
                url: '/Master/EmploymentType/Edit',
                dataType: 'html',
                data: { EmploymentTypeGI: GI, LogGI: LogGI },
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
        //var url = "/Master/EmploymentType/Create";
        //$('#AddNewOrEditSlider').load(url, function () { });
        //$('body').append('<div class="form-overlay"></div>');

        var url = "/Master/EmploymentType/Create";
        $.ajax({

            type: 'GET',
            url: '/Master/EmploymentType/Create',
            dataType: 'html',
            data: { EmploymentTypeGI: GI, LogGI: LogGI },
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





function onApproveClick(GI, LogGI, Type, index) {

    debugger;

    if ($("#EmploymentTypeList_" + index + "__MakerChecker").val() == 'True')
        swal.fire(MakerCheckerMsg);

    else {

        //var url = "/Master/EmploymentType/Approve?EmploymentTypeGI=" + GI + "&&LogGI=" + LogGI
        //$('#ApproveSlider').load(url, function () { });
        //$('body').append('<div class="form-overlay"></div>');
        $.ajax({

            type: 'GET',
            url: '/Master/EmploymentType/Approve',
            dataType: 'html',
            data: { EmploymentTypeGI: GI, LogGI: LogGI },
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
                url: '/Master/EmploymentType/Approve',
                dataType: 'json',
                data: $('#approve').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('ApproveDiv');
                            var url = "/Master/EmploymentType/GetEmploymentTypeList?TabIndex=" + 'APPROVAL_PENDING';

                            $('#EmploymentTypeList').load(url, function () { });

                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            debugger;
                            var url = "/Master/EmploymentType/Approve?EmploymentTypeGI=" + GI + "&LogGI=" + LogGI;
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
                url: '/Master/EmploymentType/Reject',
                dataType: 'json',
                data: $('#approve').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('ApproveDiv');
                            var url = "/Master/EmploymentType/GetEmploymentTypeList?TabIndex=" + 'APPROVAL_PENDING';
                            //$('#Gradelist').load(url, function () { });
                            //$(".PendingTab").addClass("active");
                            //$(".ActiveTab").removeClass("active");
                            //$("#Gradelist").removeClass("hide");

                            $('#EmploymentTypeList').load(url, function () { });
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/EmploymentType/Approve?EmploymentTypeGI=" + GI + "&LogGI=" + LogGI;
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


function ExportToExcel() {

    debugger;
    if (currentTab == 'APPROVAL_PENDING') {
        var pendingTableLength = $('#EmploymentTypeList_Count').val();
        if (pendingTableLength > 0) {
            url = "/Master/EmploymentType/ExportToExcel?TabIndex=" + 'APPROVAL_PENDING';
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }

    else if (currentTab == 'ACTIVE') {
        var activeTableLength = $('#EmploymentTypeList_Count').val();
        if (activeTableLength > 0) {
            url = "/Master/EmploymentType/ExportToExcel?TabIndex=" + 'ACTIVE';
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }
    else if (currentTab == 'INACTIVE') {
        var inActiveTableLength = $('#EmploymentTypeList_Count').val();
        if (inActiveTableLength > 0) {
            url = "/Master/EmploymentType/ExportToExcel?TabIndex=" + 'INACTIVE';
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
//        var pendingTableLength = document.getElementById("EmpTypePendingTable").rows.length - 1;
//        if (pendingTableLength > 0) {
//            url = "/Master/EmploymentType/ExportToExcel?TabIndex=" + currentTab;
//            window.location.href = url;
//        }
//        else {
//            swal.fire("No data found to export");
//        }
//    }

//    else if (currentTab == 'ACTIVE') {
//        var activeTableLength = document.getElementById("TableEmploymentType").rows.length - 1;
//        if (activeTableLength > 0) {
//            url = "/Master/EmploymentType/ExportToExcel?TabIndex=" + currentTab;
//            window.location.href = url;
//        }
//        else {
//            swal.fire("No data found to export");
//        }
//    }
//    else if (currentTab == 'INACTIVE') {
//        var inActiveTableLength = document.getElementById("TableEmploymentType").rows.length - 1;
//        if (inActiveTableLength > 0) {
//            url = "/Master/EmploymentType/ExportToExcel?TabIndex=" + currentTab;
//            window.location.href = url;
//        }
//        else {
//            swal.fire("No data found to export");
//        }
//    }
//}






function validateEmploymentType() {
    var i = 0;
    if (!validateById('#Name', 'EmploymentName')) {
        i++;
    }
    if (!validateById('#ShortName', 'EmpShortName')) {
        i++;
    }

    if (i == 0)
        return true;
    else
        return false;
}



function onDeleteClick(GI, LogGI, Type, index) {

    debugger;

    if ($("#EmploymentTypeList_" + index + "__EditActive").val() == 'True')
        swal.fire(ApprovalPendingMsg);


    else {
        //var url = "/Master/EmploymentType/Delete?EmploymentTypeGI=" + GI + "&LogGI=" + LogGI;
        //$('#DeleteSlider').load(url, function () { });
        //$('body').append('<div class="form-overlay"></div>');
        $.ajax({
            type: 'GET',
            url: '/Master/EmploymentType/Delete',
            dataType: 'html',
            data: { EmploymentTypeGI: GI, LogGI: LogGI },
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




function EmployeeTypeSave(mode) {
    debugger;
    var TabIndex;
    if (validateEmploymentType()) {
        swal.fire({
            text: "Do you want to "+mode+"?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({

                    type: 'POST',
                    url: '/Master/EmploymentType/Save',
                    dataType: 'json',
                    data: $('#employeetypeSave').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                CloseSlider('AddNewOrEdit');
                                var url = "/Master/EmploymentType/GetEmploymentTypeList?TabIndex=" + currentTab;
                                $('#EmploymentTypeList').load(url, function () { });

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
                url: '/Master/EmploymentType/Delete',
                dataType: 'json',
                data: $('#deleteEmployee').serialize(),
                success: function (result) {
                    debugger
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('delete');
                            var url = "/Master/EmploymentType/GetEmploymentTypeList?TabIndex=" + 'ACTIVE';
                            $('#EmploymentTypeList').load(url, function () { });
                            $(".PendingTab").removeClass("active");
                            $(".ActiveTab").addClass("active");
                            $("#EmploymentTypeList").removeClass("hide");
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/EmploymentType/Delete?EmploymentTypeGI=" + GI + "&LogGI=" + LogGI;
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