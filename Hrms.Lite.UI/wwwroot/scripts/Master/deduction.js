var prevTab = 'ActiveTab';
var currentTab = 'ACTIVE';

function OnTabClick(Tab, TabIndex) {
    debugger;
    showSpinner();
    currentTab = TabIndex;
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");
    $('#tabsDropdownDeduction').val(currentTab);
    //var url = "/Master/Deduction/GetDeductionList";

    //$('#DeductionList').load(url, { TabIndex: TabIndex }, function () { hideSpinner(); });



    $.ajax({

        type: 'GET',
        url: '/Master/Deduction/GetDeductionList',
        dataType: 'html',
        data: { TabIndex: TabIndex },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#DeductionList').html("");
                $('#DeductionList').html(result);
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




function ActiveInactiveValidation(name, code) {
    debugger

    $.ajax({

        type: 'POST',
        url: '/Master/Deduction/GetActiveInactiveValidation?name=' + name + '&code=' + code,
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
                    $("#Activeinactivededuction").prop("checked", true);
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
        url: '/Master/Deduction/GetDeductionList',
        dataType: 'html',
        data: { TabIndex: TabIndex },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#DeductionList').html("");
                $('#DeductionList').html(result);
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
   /* currentTab = 'ACTIVE';*/

    if (Type == 'MODIFY') {

        if ($("#DeductionList_" + index + "__EditActive").val() == 'True')
            swal.fire(ApprovalPendingMsg);


       
        else {
            $.ajax({

                type: 'GET',
                url: '/Master/Deduction/Edit',
                dataType: 'html',
                data: { DeductionGI: GI, LogGI: LogGI },
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
        var url = "/Master/Deduction/Create";
        $.ajax({

            type: 'GET',
            url: '/Master/Deduction/Create',
            dataType: 'html',
            data: { DepartmentGI: GI, LogGI: LogGI },
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











function onSaveDeductionClick(mode) {
    debugger;
    if (validateDeduction()) {

        //console.log($('#SaveDeduction').serialize());
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
                    url: '/Master/Deduction/Save',
                    dataType: 'json',
                    data: $('#SaveDeduction').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                CloseSlider('AddNewOrEditDiv');
                                var url = "/Master/Deduction/GetDeductionList?TabIndex=" + currentTab;
                                //$(".InActiveTab").removeClass("active");
                                //$(".PendingTab").removeClass("active");
                                //$(".ActiveTab").addClass("active");
                                $('#DeductionList').load(url, function () { });
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
    else {
        return false;
    }
}









function onDeleteClick(GI, LogGI, Type, index) {

    debugger;

    if ($("#DeductionList_" + index + "__EditActive").val() == 'True')
        swal.fire(ApprovalPendingMsg);


   
    else {

        $.ajax({
            type: 'GET',
            url: '/Master/Deduction/Delete',
            dataType: 'html',
            data: { DeductionGI: GI, LogGI: LogGI },
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



function onApproveClick(GI, LogGI, Type, index) {

    debugger;

    if ($("#DeductionList_" + index + "__MakerChecker").val() == 'True')
        swal.fire(MakerCheckerMsg);

   
    else {

        $.ajax({
            type: 'GET',
            url: '/Master/Deduction/Approve',
            dataType: 'html',
            data: { DeductionGI: GI, LogGI: LogGI },
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
                url: '/Master/Deduction/Approve',
                dataType: 'json',
                data: $('#deduction').serialize(),
                success: function (result) {

                    if (result.success == true) {
                        debugger
                        swal.fire(result.message).then(function () {
                            CloseSlider('ApproveDiv');
                            var url = "/Master/Deduction/GetDeductionList?TabIndex=" + 'APPROVAL_PENDING';
                            debugger


                            $(".PendingTab").addClass("active");
                            $(".ActiveTab").removeClass("active");
                            $("#DeductionList").removeClass("hide");
                            $('#DeductionList').load(url, function () { });

                        });

                    }

                    else {
                        debugger;
                        swal.fire(result.message).then(function () {
                            var url = "/Master/Deduction/Approve?DeductionGI=" + GI + "&LogGI=" + LogGI;
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
                url: '/Master/Deduction/Reject',
                dataType: 'json',
                data: $('#deduction').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('ApproveDiv');
                            var url = "/Master/Deduction/GetDeductionList?TabIndex=" + 'APPROVAL_PENDING';
                            $('#DeductionList').load(url, function () { });
                            $(".PendingTab").addClass("active");
                            $(".ActiveTab").removeClass("active");
                            $("#DeductionList").removeClass("hide");
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/Deduction/Approve?DeductionGI=" + GI + "&LogGI=" + LogGI;
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
                url: '/Master/Deduction/Delete',
                dataType: 'json',
                data: $('#DeleteDeduction').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('Delete');
                            debugger
                            var url = "/Master/Deduction/GetDeductionList?TabIndex=" + currentTab;
                            $('#DeductionList').load(url, function () { });
                            //$(".PendingTab").removeClass("active");
                            //$(".ActiveTab").addClass("active");
                            //$("#DeductionList").removeClass("hide");
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/Deduction/Delete?DeductionGI=" + GI + "&LogGI=" + LogGI;
                            $('#Delete').load(url, function () { });
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
        var pendingTableLength = $('#DeductionList_Count').val();
        if (pendingTableLength > 0) {
            url = "/Master/Deduction/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }

    else if (currentTab == 'ACTIVE') {
        var activeTableLength = $('#DeductionList_Count').val();
        if (activeTableLength > 0) {
            url = "/Master/Deduction/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }
    else if (currentTab == 'INACTIVE') {
        var inActiveTableLength = $('#DeductionList_Count').val();
        if (inActiveTableLength > 0) {
            url = "/Master/Deduction/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }
}
function validateDeduction() {
    var i = 0;
    if (!validateById('#Name', 'DeductionName')) {
        i++;
    }
    if (!validateById('#ShortName', 'DeductionShortName')) {
        i++;
    }
    if (!validateById('#DeductionType_Code', 'Type')) {
        i++;
    }
    if (!validateById('#DisplayOrder', 'Display')) {
        i++;
    }
    if (i == 0)
        return true;
    else
        return false;
}


function process(input) {
    debugger;
    let value = input.value;
    let numbers = value.replace(/[^0-9]/g, "");
    input.value = numbers;
}

