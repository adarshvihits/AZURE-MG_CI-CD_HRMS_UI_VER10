var prevTab = 'tab1';
var currentTab = 'ACTIVE';

function OntabClick(Tab, TabIndex) {
    $.ajaxSetup({
        async: false
    });
    debugger;
    showSpinner();
    //currentTab = TabIndex;
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");
    /*  $('#tabsDropdownSkill').val(currentTab);*/

    $.ajax({

        type: 'GET',
        url: '/Master/AssessmentYear/GetList',
        dataType: 'html',
        data: { TabIndex: TabIndex },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#assessmentList').html("");
                $('#assessmentList').html(result);
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


    currentTab = TabIndex;

    prevTab = Tab;
}




function AddNewOrEditClick(Type, GI, LogGI, index) {
    debugger;
   

    if (Type == 'MODIFY') {
        debugger
        if ($("#AssessmentYearList_" + index + "__EditActive").val() == 'True') {
            swal.fire(ApprovalPendingMsg);

        }
        else {
           
            $.ajax({

                type: 'GET',
                url: '/Master/AssessmentYear/Edit',
                dataType: 'html',
                data: { AssessmentYearGI: GI, LogGI: LogGI },
                success: function (result) {
                    debugger
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }
                    else {
                        $('body').append('<div class="form-overlay"></div>');
                        $('#AddNewSlider').html("");
                        $('#AddNewSlider').html(result);
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
        debugger
       
        $.ajax({

            type: 'GET',
            url: '/Master/AssessmentYear/Create',
            dataType: 'html',
            data: { AssessmentYearGI: GI, LogGI: LogGI },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('#AddNewSlider').html("");
                    $('#AddNewSlider').html(result);
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

function ValidateAssessmentYear() {
    debugger
    var i = 0;
    if (!validateById('#Name', 'assYear')) {
        i++;
    }
    if (!validateById('#From', 'FDate')) {
        i++;
    }
    if (!validateById('#To', 'TDate')) {
        i++;
    }
    if (!validateById('#FinancialYear_Code', 'Fin_yr')) {
        i++;
    }






    if (i == 0)
        return true;
    else
        return false;
}

function SaveAssessmentYear(mode) {
    debugger;

    if (ValidateAssessmentYear()) {
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
                    url: '/Master/AssessmentYear/Save',
                    dataType: 'json',
                    data: $('#AssessmentYear_Save').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                CloseSlider('addNew');
                                var url = "/Master/AssessmentYear/GetList";
                                $('#assessmentList').load(url, { TabIndex: currentTab }, function () { });
                                /* window.location.href = url;*/
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
    else
        return false
}


function onDeleteClick(GI, LogGI, Type, index) {

    debugger;

    if ($("#AssessmentYearList_" + index + "__EditActive").val() == 'True')
        swal.fire(ApprovalPendingMsg);





    else {

        $.ajax({
            type: 'GET',
            url: '/Master/AssessmentYear/Delete',
            dataType: 'html',
            data: { AssessmentYearGI: GI, LogGI: LogGI },
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
                url: '/Master/AssessmentYear/Delete',
                dataType: 'json',
                data: $('#delete_AssessmentYear').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('delete');
                            debugger;
                            var url = "/Master/AssessmentYear/GetList?TabIndex=" + 'ACTIVE';
                            $('#assessmentList').load(url, function () { });
                            $(".PendingTab").removeClass("active");
                            $(".ActiveTab").addClass("active");

                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/AssessmentYear/Delete?AssessmentYearGI=" + GI + "&LogGI=" + LogGI;
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



function onApproveClick(GI, LogGI, Type, index) {

    debugger;

    if ($("#AssessmentYearList_" + index + "__MakerChecker").val() == 'True')
        swal.fire(MakerCheckerMsg);

    else {
       


        $.ajax({

            type: 'GET',
            url: '/Master/AssessmentYear/Approve',
            dataType: 'html',
            data: { AssessmentYearGI: GI, LogGI: LogGI },
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
function ConfirmApprove(GI, LogGI) {
    //$.ajaxSetup({
    //    async: false
    //});
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
                url: '/Master/AssessmentYear/Approve',
                dataType: 'json',

                data: $('#approve_AssessmentYear').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            debugger;
                            CloseSlider('ApproveDiv');

                            var url = "/Master/AssessmentYear/GetList?TabIndex=" + 'APPROVAL_PENDING';
                            $('#assessmentList').load(url, function () { });
                            $(".PendingTab").removeClass("active");
                            $(".ActiveTab").addClass("active");

                            //var url = "/Master/AssessmentYear/GetList?TabIndex=" + 'APPROVAL_PENDING';
                            //debugger;




                            //$('#assessmentList').load(url, function () { });
                          
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/AssessmentYear/Approve?AssessmentYearGI=" + GI + "&LogGI=" + LogGI;
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
                url: '/Master/AssessmentYear/Reject',
                dataType: 'json',
                data: $('#approve_AssessmentYear').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('ApproveDiv');
                            var url = "/Master/AssessmentYear/GetList?TabIndex=" + 'APPROVAL_PENDING';
                            $('#assessmentList').load(url, function () { });
                          

                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/AssessmentYear/Approve?AssessmentYearGI=" + GI + "&LogGI=" + LogGI;
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
    debugger
    if (currentTab == 'APPROVAL_PENDING') {
        var pendingTableLength = $('#AssessmentYearList_Count').val();
        if (pendingTableLength > 0) {
            url = "/Master/AssessmentYear/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }

    else {
        var activeTableLength = $('#AssessmentYearList_Count').val();
        if (activeTableLength > 0) {
            url = "/Master/AssessmentYear/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }
}

//function ActiveInactiveValidationAssessment(name, code) {
//    debugger

//    $.ajax({

//        type: 'POST',
//        url: '/Master/AssessmentYear/GetActiveInactiveValidation?name=' + name + '&code=' + code,
//        dataType: 'json',

//        success: function (result) {
//            if (result.success == true) {
//                //swal.fire(result.message).then(function () {
//                debugger;
//                // $("#activeInactive").prop("checked", false);

//                //});

//            }
//            else {
//                swal.fire(result.message).then(function () {
//                    $("#AssYearActive").prop("checked", true);
//                });
//            }

//        },
//        error: function (xhr, textStatus, errorThrown) {
//            if ($($.parseHTML(xhr.responseText)).filter(".Login_Session").length > 0) {
//                window.location.reload();
//            }
//            else {
//                document.body.id = "kt_body";
//                $('body').html(xhr.responseText);
//            }
//        }
//    });

//}

function validateFromToDate(val, id) {
    debugger;
    /*alert($(val).val());*/
    var from = $('#From').val();
    var to = $('#To').val();
    date1 = new Date(from);
    date2 = new Date(to);

    if (date1 > date2) {
        $(val).addClass('is-invalid');
        $("#" + id + "").addClass('error-message');
        $("#" + id + "").html("To Date should be greater than From Date");
        return false;
    }
    else {
        $(val).removeClass('is-invalid');
        $("#" + id + "").removeClass('error-message');
        $("#" + id + "").html("");
        return true;
    }
}
