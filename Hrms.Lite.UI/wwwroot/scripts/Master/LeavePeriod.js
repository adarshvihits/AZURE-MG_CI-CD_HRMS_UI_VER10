

var prevTab = 'ActiveTab';
var currentTab = 'ACTIVE';


function OnTabClick(Tab, TabIndex) {
    debugger;
    showSpinner();
    currentTab = TabIndex;
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");
   /* var url = "/Master/LeavePeriod/GetLeavePeriodList";*/

    /*$('#LeavePeriodList').load(url, { TabIndex: TabIndex }, function () { hideSpinner(); });*/


    $.ajax({

        type: 'GET',
        url: '/Master/LeavePeriod/GetLeavePeriodList',
        dataType: 'html',
        data: { TabIndex: TabIndex },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#LeavePeriodList').html("");
                $('#LeavePeriodList').html(result);
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
    //var url = "/Master/LeavePeriod/GetLeavePeriodList";
    //$('#LeavePeriodList').load(url, { TabIndex: currentTab }, function () { hideSpinner(); });



    $.ajax({

        type: 'GET',
        url: '/Master/LeavePeriod/GetLeavePeriodList',
        dataType: 'html',
        data: { TabIndex: currentTab },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#LeavePeriodList').html("");
                $('#LeavePeriodList').html(result);
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
   // currentTab = 'ACTIVE';

    if (Type == 'MODIFY') {

        if ($("#LeavePeriodList_" + index + "__EditActive").val() == 'True')
            swal.fire(ApprovalPendingMsg);


        else {
            //var url = "/Master/LeavePeriod/Edit?LeavePeriodGI=" + GI + "&LogGI=" + LogGI;

            //$('#AddNewOrEditSlider').load(url, function () { });
            //$('body').append('<div class="form-overlay"></div>');


            $.ajax({

                type: 'GET',
                url: '/Master/LeavePeriod/Edit',
                dataType: 'html',
                data: { LeavePeriodGI: GI, LogGI: LogGI },
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
        //var url = "/Master/LeavePeriod/Create";
        //$('#AddNewOrEditSlider').load(url, function () { });
        //$('body').append('<div class="form-overlay"></div>');


        $.ajax({

            type: 'GET',
            url: '/Master/LeavePeriod/Create',
            dataType: 'html',
            data: { LeavePeriodGI: GI, LogGI: LogGI },
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

    if ($("#LeavePeriodList_" + index + "__MakerChecker").val() == 'True')
        swal.fire(MakerCheckerMsg);

    else {

        //var url = "/Master/LeavePeriod/Approve?LeavePeriodGI=" + GI + "&&LogGI=" + LogGI;
        //$('#ApproveSlider').load(url, function () { });
        //$('body').append('<div class="form-overlay"></div>');

        $.ajax({

            type: 'GET',
            url: '/Master/LeavePeriod/Approve',
            dataType: 'html',
            data: { LeavePeriodGI: GI, LogGI: LogGI },
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
                url: '/Master/LeavePeriod/Approve',
                dataType: 'json',
                data: $('#ApproveLeavePeriod').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('ApproveDiv');
                            var url = "/Master/LeavePeriod/GetLeavePeriodList?TabIndex=" + 'APPROVAL_PENDING';
                            $('#LeavePeriodList').load(url, function () { });

                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            debugger;
                            var url = "/Master/LeavePeriod/Approve?LeavePeriodGI=" + GI + "&LogGI=" + LogGI;
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
                url: '/Master/LeavePeriod/Reject',
                dataType: 'json',
                data: $('#ApproveLeavePeriod').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('ApproveDiv');
                            var url = "/Master/LeavePeriod/GetLeavePeriodList?TabIndex=" + 'APPROVAL_PENDING';
                            $('#LeavePeriodList').load(url, function () { });
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/LeavePeriod/Approve?LeavePeriodGI=" + GI + "&LogGI=" + LogGI;
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



function onDeleteClick(GI, LogGI, Type, index) {

    debugger;

    if ($("#LeavePeriodList_" + index + "__EditActive").val() == 'True')
        swal.fire(ApprovalPendingMsg);


    else {
        //var url = "/Master/LeavePeriod/Delete?LeavePeriodGI=" + GI + "&LogGI=" + LogGI;
        //$('#DeleteSlider').load(url, function () { });
        //$('body').append('<div class="form-overlay"></div>');


        $.ajax({
            type: 'GET',
            url: '/Master/LeavePeriod/Delete',
            dataType: 'html',
            data: { LeavePeriodGI: GI, LogGI: LogGI },
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
                url: '/Master/LeavePeriod/Delete',
                dataType: 'json',
                data: $('#DeleteLeavePeriod').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('delete');
                            var url = "/Master/LeavePeriod/GetLeavePeriodList?TabIndex=" + 'ACTIVE';
                            $('#LeavePeriodList').load(url, function () { });
                            $(".PendingTab").removeClass("active");
                            $(".ActiveTab").addClass("active");
                            $("#LeavePeriodList").removeClass("hide");
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/LeavePeriod/Delete?LeavePeriodGI=" + GI + "&LogGI=" + LogGI;
                            $('#DeleteSlider').load(url, function () { });
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
        var pendingTableLength = $('#LeavePeriodList_Count').val();
        if (pendingTableLength == "") {
            swal.fire(NoDataFoundMsg);
            
        }
        else {
            url = "/Master/LeavePeriod/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
    }

    else if (currentTab == 'ACTIVE') {
        var activeTableLength = $('#LeavePeriodList_Count').val();
        if (activeTableLength == "") {
            swal.fire(NoDataFoundMsg);
           
        }
        else {
            url = "/Master/LeavePeriod/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
    }
    else if (currentTab == 'INACTIVE') {
        var inActiveTableLength = $('#LeavePeriodList_Count').val();
        if (inActiveTableLength =="") {
            swal.fire(NoDataFoundMsg);
          
        }
        else {
            url = "/Master/LeavePeriod/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }

    }
}

function validateLeavePeriod() {
    debugger;
    var i = 0;
    if (!validateById('#Name', 'LeavePName')) {
        i++;
    }
    if (!validateById('#ShortName', 'LeavePShort')) {
        i++;
    }
    if (!validateById('#From', 'FromDate')) {
        i++;
    }

    if (!validateById('#To', 'ToDate')) {
       
        i++;
     
    }
    if (!validateToDate('#To', 'ToDate'))
        {
        i++;
    }
   

    if (i == 0)
        return true;
    else
        return false;
}


//function validateLeavePeriod() {
//    debugger;
//    if ($('.LPName').val() == '') {

//        $('#Name').addClass('is-invalid');
//        $("#LeavePName").addClass('error-message');
//        $('#LeavePName').html("This field is required");
//        $('#Name').focus();
//        return false;
//    }

//    else if ($('.LPShortName').val() == '') {

//        $('#ShortName').addClass('is-invalid');
//        $("#LeavePShort").addClass('error-message');
//        $('#LeavePShort').html("This field is required");
//        $('#ShortName').focus();
//        return false;
//    }
//    else if ($('.fromdate').val() == '') {

//        $('#From').addClass('is-invalid');
//        $("#FromDate").addClass('error-message');
//        $('#FromDate').html("This field is required");
//        $('#From').focus();
//        return false;
//    }
//    else if ($('.toDate').val() == '') {

//        $('#To').addClass('is-invalid');
//        $("#ToDate").addClass('error-message');
//        $('#ToDate').html("This field is required");
//        $('#To').focus();
//        return false;
//    }
//    else
//        return true;
//}


function validateToDate(val, id) {
    debugger;
    if ($(val).val() == "") {
        $(val).addClass('is-invalid');
        $("#" + id + "").addClass('error-message');
        $("#" + id + "").html("This field is required");
    }
    else {

        var ToDate = new Date($('#To').val());
        var fromDate = new Date($('#From').val());
        var d = new Date();
        var TDate = new Date(ToDate.toLocaleDateString());
        var FDate = new Date(fromDate.toLocaleDateString());

        if (FDate > TDate) {
            $(val).addClass('is-invalid');
            $("#" + id + "").addClass('error-message');
            $("#" + id + "").html("To date should be greater than From date");
            return false;
        }
        else {
            $(val).removeClass('is-invalid');
            $("#" + id + "").removeClass('error-message');
            $("#" + id + "").html("");
            return true;
        }
    }
}


function LeavePeriodSave(mode) {
    debugger;

    if (validateLeavePeriod()) {

        //console.log($('#leavePeriodsave').serialize());
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
                    url: '/Master/LeavePeriod/Save',
                    dataType: 'json',
                    data: $('#leavePeriodsave').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                CloseSlider('AddNewOrEdit');
                                var url = "/Master/LeavePeriod/GetLeavePeriodList?TabIndex=" + currentTab;
                                $('#LeavePeriodList').load(url, function () { });

                            });

                        }

                        else {
                            swal.fire(result.message).then(function () {

                            });
                        }

                    },
                    error: function (xhr, textStatus, errorThrown) {
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
    else {
        return false;
    }
}



function ActiveInactiveValidationLeaveperiod(name, code) {
    debugger

    $.ajax({

        type: 'POST',
        url: '/Master/LeaveGroup/GetActiveInactiveValidation?name=' + name + '&code=' + code,
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
                    $("#Leaveperiod").prop("checked", true);
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