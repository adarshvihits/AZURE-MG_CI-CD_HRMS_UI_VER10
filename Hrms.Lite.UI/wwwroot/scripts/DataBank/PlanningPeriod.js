var prevTab = 'ActiveTab';
var currentTab = 'ACTIVE';






function onAddOrEditClick(Type, GI, LogGI, index) {
    debugger;
    //currentTab = 'ACTIVE';

    if (Type == 'MODIFY') {

        if ($("#PlanningPeriodDataBankList_" + index + "__EditActive").val() == 'True')
            swal.fire(ApprovalPendingMsg);


        else {
            //var url = "/DataBank/PlanningPeriod/Edit?PlanningPeriodGI=" + GI + "&LogGI=" + LogGI;

            //$('#newRequestSlider').load(url, function () { });
            //$('body').append('<div class="form-overlay"></div>');

            $.ajax({

                type: 'GET',
                url: '/DataBank/PlanningPeriod/Edit',
                dataType: 'html',
                data: { PlanningPeriodGI: GI, LogGI: LogGI },
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
        //var url = "/DataBank/PlanningPeriod/Create";
        //$('#newRequestSlider').load(url, function () { });
        //$('body').append('<div class="form-overlay"></div>');





        $.ajax({

            type: 'GET',
            url: '/DataBank/PlanningPeriod/Create',
            dataType: 'html',
            //data: { AttendanceStatusGI: GI, LogGI: LogGI },
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





function OnTabClick(Tab, TabIndex) {
    debugger;
    showSpinner();
    currentTab = TabIndex;
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");




    $.ajax({

        type: 'GET',
        url: '/DataBank/PlanningPeriod/GetPlanningPeriodList',
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


    //if (TabIndex != 'PendingTab')
    //    $('#ActiveList').load(url, { TabIndex: TabIndex }, function () { hideSpinner(); });
    //else
    //    $('#PendingList').load(url, { TabIndex: TabIndex }, function () { hideSpinner(); });
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
    //var url = "/DataBank/PlanningPeriod/GetPlanningPeriodList";
    //$('#ActiveList').load(url, { TabIndex: currentTab }, function () { hideSpinner(); });
    $.ajax({

        type: 'GET',
        url: '/DataBank/PlanningPeriod/GetPlanningPeriodList',
        dataType: 'html',
        data: { TabIndex: currentTab },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#tabsDropdownPlanningPeriod').html("");
                $('#tabsDropdownPlanningPeriod').html(result);
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






function onDeleteClick(GI, LogGI, Type, index) {

    debugger;

    if ($("#PlanningPeriodDataBankList_" + index + "__EditActive").val() == 'True')
        swal.fire(ApprovalPendingMsg);


    else {
        //var url = "/DataBank/PlanningPeriod/Delete?PlanningPeriodGI=" + GI + "&LogGI=" + LogGI;
        //$('#deleteEvaluationSlider').load(url, function () { });
        //$('body').append('<div class="form-overlay"></div>');


        $.ajax({
            type: 'GET',
            url: '/DataBank/PlanningPeriod/Delete',
            dataType: 'html',
            data: { PlanningPeriodGI: GI, LogGI: LogGI },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('#deleteEvaluationSlider').html("");
                    $('#deleteEvaluationSlider').html(result);
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



function validateAllPlanningPeriodSave(GI, mode) {

    debugger;
    var i = 0;
    if (!validateById('#Name', 'epName')) {
        i++;
    }
    if (!validateById('#ShortName', 'epShortName')) {
        i++;
    }
    if (!validateById('#From', 'epFrom')) {
        i++;
    }
    if (!validateById('#To', 'epTo')) {
        i++;
    }
    else {
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
                    url: '/DataBank/PlanningPeriod/PlanningPeriodSave',
                    dataType: 'json',
                    data: { name: $('#Name').val(), shortname: $('#ShortName').val(), from: $('.fromdp').val(), to: $('.todp').val(), gi: GI, status:  $('#evaluationStatus').prop('checked'), mode: $('#Mode').val(), des: $('#Description').val() },
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                //$('.tabOne').removeClass('active');
                                //$('#tabOne').removeClass('active');
                                //$('#tabOne').css('display', 'none');
                                //$('.tabContactDetails').addClass('active');
                                //$('#tabContactDetails').addClass('active');
                                CloseSlider('evaluationSaveclose');
                                var url = "/DataBank/PlanningPeriod/GetPlanningPeriodList?TabIndex=" + currentTab;
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
    if (i == 0)
        return true;
    else
        return false;
}







//function validateAllPlanningPeriodSave(GI) {
//    debugger;

//    if (!validateById('#Name', 'epName')) {
//        return false;
//    }
//    else if (!validateById('#ShortName', 'epShortName')) {
//        return false;
//    }
//    else if (!validateById('#From', 'epFrom')) {
//        return false;
//    }
//    else if (!validateById('#To', 'epTo')) {
//        return false;
//    }

//    else {
//        $.ajax({

//            type: 'POST',
//            url: '/DataBank/PlanningPeriod/PlanningPeriodSave',
//            dataType: 'json',
//            data: { name: $('#Name').val(), shortname: $('#ShortName').val(), from: $('.fromdp').val(), to: $('.todp').val(), gi: GI, status: $('#Active').val(), mode: $('#Mode').val(), des: $('#Description').val()},
//            success: function (result) {
//                if (result.success == true) {
//                    swal.fire(result.message).then(function () {
//                        //$('.tabOne').removeClass('active');
//                        //$('#tabOne').removeClass('active');
//                        //$('#tabOne').css('display', 'none');
//                        //$('.tabContactDetails').addClass('active');
//                        //$('#tabContactDetails').addClass('active');
//                        CloseSlider('evaluationSaveclose');
//                       var url = "/DataBank/PlanningPeriod/GetPlanningPeriodList?TabIndex=" + currentTab;
//                        $('#ActiveList').load(url, function () { });


//                    });

//                }

//                else {


//                    swal.fire(result.message).then(function () {

//                    });
//                }

//            },

//            error: function (xhr, textStatus, errorThrown) {
//                toastr.error(xhr.responseText);
//            }
//        });
//    }
//}



function onApproveClick(GI, LogGI, Type, index) {

    debugger;

    if ($("#PlanningPeriodDataBankList_" + index + "__MakerChecker").val() == 'True')
        swal.fire(MakerCheckerMsg);

    else {

        //var url = "/DataBank/PlanningPeriod/Approve?PlanningPeriodGI=" + GI + "&LogGI=" + LogGI;
        //$('#approveSlider').load(url, function () { });
        //$('body').append('<div class="form-overlay"></div>');


        $.ajax({

            type: 'GET',
            url: '/DataBank/PlanningPeriod/Approve',
            dataType: 'html',
            data: { PlanningPeriodGI: GI, LogGI: LogGI },
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
                url: '/DataBank/PlanningPeriod/Reject',
                dataType: 'json',
                data: $('#approve').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('PendingApprove');

                            var url = "/DataBank/PlanningPeriod/GetPlanningPeriodList?TabIndex=" + 'APPROVAL_PENDING';
                            $('#ActiveList').load(url, function () { });

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




function confirmDelete(PlanningPeriodGI) {
    debugger
    console.log('PlanningPeriodGI');
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
                url: '/DataBank/PlanningPeriod/PlanningPeriodDelete',
                dataType: 'json',
                /* data: $('#EvaluationDelete').serialize(),*/
                data: { PlanningPeriodGI: PlanningPeriodGI },
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('DeleteDiv');
                            debugger
                            var url = "/DataBank/PlanningPeriod/GetPlanningPeriodList?TabIndex=" + 'ACTIVE';
                            $('#ActiveList').load(url, function () { });
                            $(".PendingTab").removeClass("active");
                            $(".ActiveTab").addClass("active");
                            $("#ActiveList").removeClass("hide");
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            debugger;

                            url = "/DataBank/PlanningPeriod/PlanningPeriodDelete?PlanningPeriodGI=" + PlanningPeriodGI;
                            $('#DeleteDiv').load(url, function () { });
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

        var trainingDataBankTableLength = $('#PlanningPeriodDataBankList_Count').val();
        if (trainingDataBankTableLength > 0) {
            url = "/DataBank/PlanningPeriod/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }
    else {
        var trainingDataBankPendingTableLength = $('#PlanningPeriodDataBankList_Count').val();
        if (trainingDataBankPendingTableLength > 0) {
            url = "/DataBank/PlanningPeriod/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
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
                url: '/DataBank/PlanningPeriod/Approve',
                dataType: 'json',
                data: $('#approve').serialize(),
                success: function (result) {

                    if (result.success == true) {
                        debugger
                        swal.fire(result.message).then(function () {
                            CloseSlider('PendingApprove');
                            var url = "/DataBank/PlanningPeriod/GetPlanningPeriodList?TabIndex=" + 'APPROVAL_PENDING';
                            $('#ActiveList').load(url, function () { });
                            $(".PendingTab").addClass("active");
                            $(".ActiveTab").removeClass("active");
                            $("#ActiveList").removeClass("hide");
                        });

                    }

                    else {
                        debugger;
                        swal.fire(result.message).then(function () {
                            var url = "/DataBank/PlanningPeriod/Approve?PlanningPeriodGI=" + GI + "&LogGI=" + LogGI;
                            $('#approveSlider').load(url, function () { });
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




function ActiveInactiveValidationEvalution(name, code) {
    debugger

    $.ajax({

        type: 'POST',
        url: '/DataBank/PlanningPeriod/GetActiveInactiveValidation?name=' + name + '&code=' + code,
        dataType: 'json',

        success: function (result) {
            if (result.success == true)
            {
                $("#evaluationStatus").prop("checked", false);
                // $("#evaluationStatus").prop("checked", false).val("false");
            }
            else {
                swal.fire(result.message).then(function ()
                {
                    $("#evaluationStatus").prop("checked", true);
                    // $("#evaluationStatus").prop("checked", true).val("true");
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


