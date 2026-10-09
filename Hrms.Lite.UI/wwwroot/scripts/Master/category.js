var prevTab = 'ActiveTab';
var currentTab = 'ACTIVE';

function OnTabClick(Tab, TabIndex) {
    debugger;
    showSpinner();
    currentTab = TabIndex;
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");
    $('#tabsDropdownCategory').val(currentTab);
    //var url = "/Master/Category/GetCategoryList";
  
    //$('#CategoryList').load(url, { TabIndex: TabIndex }, function () { hideSpinner(); });



    $.ajax({

        type: 'GET',
        url: '/Master/Category/GetCategoryList',
        dataType: 'html',
        data: { TabIndex: TabIndex },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#CategoryList').html("");
                $('#CategoryList').html(result);
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
    currentTab = $(val).val();
    showSpinner()
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
        url: '/Master/Category/GetCategoryList',
        dataType: 'html',
        data: { TabIndex: TabIndex },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#CategoryList').html("");
                $('#CategoryList').html(result);
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

//function Approve(GI,LogGI) {
//    debugger;
//    var url = "/Master/Category/Approve?CategoryGI=" + GI + "&LogGI=" + LogGI;
//    $('.ApprovalPending').load(url, function () { });
//    $('body').append('<div class="form-overlay"></div>');
//    $("#ApproveSlider").removeClass("hide");
//    $(".ApprovalPending").show();

//}

function onApproveClick(GI, LogGI, Type, index) {

    debugger;
    if ($("#CategoryList_" + index + "__MakerChecker").val() == 'True')
        swal.fire(MakerCheckerMsg);

    //else {
    //    var url = "/Master/Category/Approve?CategoryGI=" + GI + "&LogGI=" + LogGI;

    //    $('#ApproveSlider').load(url, function () { });
    //    $('body').append('<div class="form-overlay"></div>');
    //}


      else {

        $.ajax({

            type: 'GET',
            url: '/Master/Category/Approve',
            dataType: 'html',
            data: { CategoryGI: GI, LogGI: LogGI },
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

        if ($("#CategoryList_" + index + "__EditActive").val() == 'True')
            swal.fire(ApprovalPendingMsg);

        else {
            $.ajax({

                type: 'GET',
                url: '/Master/Category/Edit',
                dataType: 'html',
                data: { CategoryGI: GI, LogGI: LogGI },
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
        var url = "/Master/Category/Create";
        $.ajax({

            type: 'GET',
            url: '/Master/Category/Create',
            dataType: 'html',
            data: { CategoryGI: GI, LogGI: LogGI },
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







function CategorySave(mode) {
    debugger;
    if (validateCategory()) {

        //console.log($('#Save-category').serialize());
       
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
                        url: '/Master/Category/Save',
                        dataType: 'json',
                        data: $('#Save-category').serialize(),
                        success: function (result) {
                            if (result.success == true) {
                                swal.fire(result.message).then(function () {
                                    CloseSlider('AddNewDiv');
                                    var url = "/Master/Category/GetCategoryList?TabIndex=" + currentTab;
                                    $('#CategoryList').load(url, function () { });

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




function onDeleteClick(GI, LogGI, Type, index) {

    debugger;

    if ($("#CategoryList_" + index + "__EditActive").val() == 'True')
        swal.fire(ApprovalPendingMsg);


    //else {
    //    var url = "/Master/Category/Delete?CategoryGI=" + GI + "&LogGI=" + LogGI;
    //    $('#DeleteSlider').load(url, function () { });
    //    $('body').append('<div class="form-overlay"></div>');
    //}

    else {

        $.ajax({
            type: 'GET',
            url: '/Master/Category/Delete',
            dataType: 'html',
            data: { CategoryGI: GI, LogGI: LogGI },
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



function validateCatType(val, id) {
    debugger;
    if (val.value == "") {
        $(val).addClass('is-invalid');
        $("#" + id + "").addClass('error-message');
        $("#" + id + "").html("This field is required");
    }
    else {
        $(val).removeClass('is-invalid');
        $("#" + id + "").removeClass('error-message');
        $("#" + id + "").html("");
        if ($('#CategoryType_Code').val() == 1) {
            debugger;
            i = 0;
            $('.expiry').addClass('hide');
        }
        else {
            $('.expiry').removeClass('hide');
        }
        /* val.value = val.value.replace(/(^\s*)|(\s*$)/gi, "");*/
        val.value = val.value.replace(/(^\s*)|(\s*$)/gi, "");

        return val.value;
    }
}






function validateCategory() {
    var i = 0;
    if (!validateById('#Name', 'CategoryName')) {
        i++;
    }
    if (!validateById('#ShortName', 'CategoryShortName')) {
        i++;
    }
    if (!validateById('#CategoryType_Code', 'CategoryType')) {
        i++;
    }
    if (!validateById('#ExpiryPeriod', 'Period')) {
        i++;
        if ($('#CategoryType_Code').val() == 1) {
            debugger;
            i = 0;
        } 
    }
   

    if (i == 0)
        return true;
    else
        return false;
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
                url: '/Master/Category/Reject',
                dataType: 'json',
                data: $('#approve').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('ApprovalPending');
                            var url = "/Master/Category/GetCategoryList?TabIndex=" + 'APPROVAL_PENDING';
                            $('#CategoryList').load(url, { TabIndex: currentTab }, function () { });


                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/Category/Approve?CategoryGI=" + GI + "&LogGI=" + LogGI;
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





function confirmApprove(GI, LogGI) {
    //$.ajaxSetup({
    //    async: false
    //});
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
                url: '/Master/Category/Approve',
                dataType: 'json',
                data: $('#approve').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('ApprovalPending');
                            var url = "/Master/Category/GetCategoryList?TabIndex=" + 'APPROVAL_PENDING';
                            $('#CategoryList').load(url, function () { });
                            $(".PendingTab").addClass("active");
                            $(".ActiveTab").removeClass("active");
                            $("#CategoryList").removeClass("hide");
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/Category/Approve?CategoryGI=" + GI + "&LogGI=" + LogGI;
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
                url: '/Master/Category/Delete',
                dataType: 'json',
                data: $('#Deletecategory').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('delete');
                            var url = "/Master/Category/GetCategoryList?TabIndex=" + 'ACTIVE';
                            $('#CategoryList').load(url, function () { });
                            $(".PendingTab").removeClass("active");
                            $(".ActiveTab").addClass("active");
                            $("#CategoryList").removeClass("hide");
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/Category/Delete?CategoryGI=" + GI + "&LogGI=" + LogGI;
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
function ActiveInactiveValidation(name, code) {
    debugger

    $.ajax({

        type: 'POST',
        url: '/Master/Category/GetActiveInactiveValidation?name=' + name + '&code=' + code,
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
                    $("#Activeinactivedcat").prop("checked", true);
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




function ExportToExcel() {

    debugger;
    if (currentTab == 'APPROVAL_PENDING') {
        var pendingTableLength = $('#CategoryList_Count').val();
        if (pendingTableLength > 0) {
            url = "/Master/Category/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }

    else if (currentTab == 'ACTIVE') {
        var activeTableLength = $('#CategoryList_Count').val();
        if (activeTableLength > 0) {
            url = "/Master/Category/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }
    else if (currentTab == 'INACTIVE') {
        var inActiveTableLength = $('#CategoryList_Count').val();
        if (inActiveTableLength > 0) {
            url = "/Master/Category/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }
}


function process(input)
{
    debugger;
    let value = input.value;
    let numbers = value.replace(/[^0-9]/g, "");
    input.value = numbers;
}