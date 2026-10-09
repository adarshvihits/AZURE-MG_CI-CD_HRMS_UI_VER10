

function CalculateDedAndTakeHome() {
    var TotGrossAmt = $('#Enrollment_PreEnrollment_Compensation_GrossSalary').val();
    var TotDedAmt = parseFloat("0");
    $('.DeductionsAmount').each(function (i, obj) {//Total Dedcutions
        var Amt = parseFloat($("#Enrollment_PreEnrollment_Compensation_FixedDeduction_" + i + "__Amount").val());
        debugger;
        if (isNaN(Amt)) {
            Amt = parseFloat("0");
        }
        TotDedAmt = TotDedAmt + Amt;

        $("#totDeductions").html(TotDedAmt);
        //$("#Compensation_CTC").val(TotCTCAmt + TotGrossAmt);

    });

    TakeHome = TotGrossAmt - TotDedAmt;//Take Home

    $("#takeHome").html(TakeHome);
}

function _GetDOBPassword(date) {
    debugger


    var formattedDate = new Date(date);
    var cDate = formattedDate.getDate();
    var cMonth = formattedDate.getMonth() + 1;
    var cYear = formattedDate.getFullYear();
    if (cDate < 10)
        cDate = '0' + cDate;
    if (cMonth < 10)
        cMonth = '0' + cMonth;
    formattedDate = (cDate.toString() + cMonth.toString() + cYear.toString());
    return formattedDate;

}




function confirmJoinExistValidation(GI, dojj) {
    debugger

    var Mobile = $('#Enrollment_PreEnrollment_PermanentAddress_MobileNo').val();
    var PanNum = $('#Enrollment_PreEnrollment_BasicInfo_PanNo').val();
    var Aadhar = $('#Enrollment_PreEnrollment_BasicInfo_AdharNo').val();
    var Account = $('#Enrollment_StatutoryClassification_AccountNo').val();
    var EmployeeID = $('#Enrollment_EmployeeSettings_EmployeeID').val();
    var i = 0;

    var doj = new Date(dojj);
    if (doj > new Date()) {
        
        swal.fire('Future Date Of joining is not allowed');
        return false;
    }
   
    $.getJSON("/DataBank/EditEmployeeInfo/CheckifAlreadySalaryProcessed", { Date: dojj }, function (data) {
        console.log(data);
        debugger
        if (data.success == false) {
            i++;
            swal.fire(data.message);
           
          
        }
    });
    if (i == 0) {
        swal.fire({
            text: "Do you want to Confirm ?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {



                $.ajax({
                    type: 'GET',
                    async: false,
                    url: '/DataBank/JoiningConfirmation/confirmJoinExistValidation',

                    dataType: 'json',
                    data: { EnrollGI: GI },
                    success: function (data) {
                        duplicationData = data;
                        debugger;
                        if (data.alreadyExists_JoiningConfirmation != null) {
                            var aadhar = 0;

                            for (let i = 0; i < data.alreadyExists_JoiningConfirmation.length; i++) {







                                if (data.alreadyExists_JoiningConfirmation[i].code == 6) {
                                    debugger;
                                    EmpID = 6;
                                    $.ajax({
                                        type: 'GET',
                                        async: false,
                                        url: '/DataBank/JoiningConfirmation/GetDuplicationList',
                                        dataType: 'html',
                                        data: { DocumentNum: EmployeeID, Type: 6 },
                                        success: function (result) {
                                            var m = $('#aadharDuplication');
                                            m.find('.modal-body').html('');
                                            m.find('.modal-body').html(result);
                                            m.modal('show');
                                            $('#popupHeader').html(data.alreadyExists_OnBoarding[i].message);
                                        },
                                        error: function (xhr, textStatus, errorThrown) {
                                            swal.fire(xhr.responseText);

                                        }
                                    });

                                }




                                if (data.alreadyExists_JoiningConfirmation[i].code == 5) {
                                    aadhar = 1;
                                    $.ajax({
                                        type: 'GET',
                                        async: false,
                                        url: '/DataBank/JoiningConfirmation/GetDuplicationList',
                                        dataType: 'html',
                                        data: { DocumentNum: Aadhar, Type: 5 },
                                        success: function (result) {
                                            var m = $('#aadharDuplication');
                                            m.find('.modal-body').html('');
                                            m.find('.modal-body').html(result);
                                            m.modal('show');
                                            $('#popupHeader').html(data.alreadyExists_JoiningConfirmation[i].message);
                                        },
                                        error: function (xhr, textStatus, errorThrown) {
                                            swal.fire(xhr.responseText);

                                        }
                                    });

                                }

                                if (data.alreadyExists_JoiningConfirmation[i].code == 4) {
                                    debugger;
                                    pan = 2;
                                    $.ajax({
                                        type: 'GET',
                                        async: false,
                                        url: '/DataBank/JoiningConfirmation/GetDuplicationList',
                                        dataType: 'html',

                                        data: { DocumentNum: PanNum, Type: 4 },

                                        success: function (result) {
                                            var m = $('#aadharDuplication');
                                            m.find('.modal-body').html('');
                                            m.find('.modal-body').html(result);
                                            m.modal('show');
                                            $('#popupHeader').html(data.alreadyExists_JoiningConfirmation[i].message);
                                        },
                                        error: function (xhr, textStatus, errorThrown) {
                                            swal.fire(xhr.responseText);

                                        }
                                    });

                                }


                                if (data.alreadyExists_JoiningConfirmation[i].code == 3) {
                                    debugger;
                                    ACCOUNT = 4;
                                    $.ajax({
                                        type: 'GET',
                                        async: false,
                                        url: '/DataBank/JoiningConfirmation/GetDuplicationList',
                                        dataType: 'html',

                                        data: { DocumentNum: Account, Type: 3 },

                                        success: function (result) {
                                            var m = $('#aadharDuplication');
                                            m.find('.modal-body').html('');
                                            m.find('.modal-body').html(result);
                                            m.modal('show');
                                            $('#popupHeader').html(data.alreadyExists_JoiningConfirmation[i].message);
                                        },
                                        error: function (xhr, textStatus, errorThrown) {
                                            swal.fire(xhr.responseText);

                                        }
                                    });

                                }





                                if (data.alreadyExists_JoiningConfirmation[i].code == 1) {
                                    mob = 3;
                                    $.ajax({
                                        type: 'GET',
                                        async: false,
                                        url: '/DataBank/JoiningConfirmation/GetDuplicationList',
                                        dataType: 'html',
                                        data: { DocumentNum: Mobile, Type: 1 },
                                        success: function (result) {
                                            var m = $('#aadharDuplication');
                                            m.find('.modal-body').html('');
                                            m.find('.modal-body').html(result);
                                            m.modal('show');
                                            $('#popupHeader').html(data.alreadyExists_OnBoarding[i].message);
                                        },
                                        error: function (xhr, textStatus, errorThrown) {
                                            swal.fire(xhr.responseText);

                                        }
                                    });

                                }
                            }
                            if (aadhar == 1) {
                                var msg = alertMessage(data.alreadyExists_OnBoarding);
                                swal.fire({
                                    text: msg,
                                    icon: 'warning',
                                    showCancelButton: true,
                                    confirmButtonColor: '#3085d6',
                                    cancelButtonColor: '#d33',
                                    confirmButtonText: 'Ok'
                                }).then((result) => {
                                    if (result.isConfirmed) {
                                        DuplicationOkClick();
                                    }
                                })
                            }
                            else if (pan == 2) {
                                var msg = alertMessage(data.alreadyExists_OnBoarding);
                                swal.fire({
                                    text: msg,
                                    icon: 'warning',
                                    showCancelButton: true,
                                    confirmButtonColor: '#3085d6',
                                    cancelButtonColor: '#d33',
                                    confirmButtonText: 'Ok'
                                }).then((result) => {
                                    if (result.isConfirmed) {
                                        DuplicationOkClick();
                                    }
                                })
                            }

                            else if (mob == 3) {
                                var msg = alertMessage(data.alreadyExists_OnBoarding);
                                swal.fire({
                                    text: msg,
                                    icon: 'warning',
                                    showCancelButton: true,
                                    confirmButtonColor: '#3085d6',
                                    cancelButtonColor: '#d33',
                                    confirmButtonText: 'Ok'
                                }).then((result) => {
                                    if (result.isConfirmed) {
                                        DuplicationOkClick();
                                    }
                                })
                            }
                            else if (ACCOUNT == 3) {
                                var msg = alertMessage(data.alreadyExists_OnBoarding);
                                swal.fire({
                                    text: msg,
                                    icon: 'warning',
                                    showCancelButton: true,
                                    confirmButtonColor: '#3085d6',
                                    cancelButtonColor: '#d33',
                                    confirmButtonText: 'Ok'
                                }).then((result) => {
                                    if (result.isConfirmed) {
                                        DuplicationOkClick();
                                    }
                                })
                            }


                        }

                        else {
                            debugger;
                            confirmJoin();

                        }
                    },
                    error: function (xhr, textStatus, errorThrown) {
                        swal.fire(xhr.responseText);

                    }
                });
            }
        });

    }
    else return false;
}

function DuplicationOkClick() {

    debugger;
    var msg = alertMessage(duplicationData.alreadyExists_JoiningConfirmation);
    swal.fire({
        text: msg,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            confirmJoin();

            //var preEnrollId = $('#Enrollment_EnrollGUID').val();
            //$.ajax({

            //    type: 'POST',
            //    url: '/DataBank/JoiningConfirmation/ConfirmJoin',
            //    dataType: 'json',
            //    data: $('#Join').serialize(),
            //    //data:
            //    //{
            //    //    preEnrollGI: preEnrollId
            //    //},
            //    success: function (result) {
            //        debugger;
            //        console.log(result);
            //        if (result.success == true) {
            //            swal.fire(result.message).then(function () {
            //                window.location.href = '/DataBank/OnBoarding/Index';
            //            });

            //        }
            //        else {
            //            swal.fire(result.message).then(function () {
            //                window.location.href = '/DataBank/JoiningConfirmation/Create?EnrollGI=' + preEnrollId;

            //            });
            //        }
            //    },
            //    error: function (xhr, textStatus, errorThrown) {
            //        if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
            //            window.location.reload();
            //        }

            //        else {
            //            document.body.id = "kt_body";
            //            $('body').html(xhr.responseText);
            //        }
            //    }
            //});




        }
    })

    CloseDuplicationPopup()
}


function alertMessage(data) {
    debugger;
    var msg = '';
    for (let i = 0; i < data.length; i++) {
        if (data[i].code == 1)
            msg = msg + "Mobile number";
        if (data[i].code == 4) {
            if (msg != '')
                msg = msg + ", Pan number";
            else
                msg = msg + "Pan number";
        }
        if (data[i].code == 5) {
            if (msg != '')
                msg = msg + ", Aadhar number";
            else
                msg = msg + "Aadhar number";
        }

        if (data[i].code == 3) {
            if (msg != '')
                msg = msg + ", Account number";
            else
                msg = msg + "Account number";
        }
    }


    msg = msg + " already exists. Do you want to continue?";

    return msg;
    //console.log(msg);

    //if (msg == "MOBILE_NUMBER_DUPLICATION_ERROR") {
    //    var msg = "Mobile number already exists. Do you want to continue?";
    //    return msg;
    //}
    //else if (msg == "ADHAAR_NUMBER_DUPLICATION_ERROR") {
    //    var msg = "Aadhar number already exists. Do you want to continue?";
    //    return msg;
    //}
    //else if (msg == "PAN_NUMBER_DUPLICATION_ERROR")
    //{
    //    var msg = "Pan number already exists. Do you want to continue?";
    //    return msg;
    //}
    //else {
    //    var msg = "Something went wrong";
    //    return msg;
    //}
}


function CloseDuplicationPopup() {
    debugger
    var m = $('#aadharDuplication');
    m.find('.modal-body').html('');
    m.modal('hide');;
}



















function confirmReject() {
    debugger;
    $('#DOB_Password').val(_GetDOBPassword(document.getElementById("DOB_Password").value));
    $('#MOBILE_password').val($('#Enrollment_PreEnrollment_PermanentAddress_MobileNo').val());

    swal.fire({
        text: "Do you want to Reject ?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            var preEnrollId = $('#Enrollment_EnrollGUID').val();
            $.ajax({

                type: 'POST',
                url: '/DataBank/JoiningConfirmation/RejectJoin',
                dataType: 'json',
                data: $('#Join').serialize(),

                success: function (result) {
                    debugger;
                    console.log(result);
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            debugger;
                            window.location.href = '/DataBank/OnBoarding/Index';
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            debugger;
                            window.location.href = '/DataBank/JoiningConfirmation/Create?EnrollGI=' + preEnrollId;

                        });
                    }
                },
                error: function (xhr, textStatus, errorThrown) {
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }

                    else {
                        document.body.id = "kt_body";
                        $('body').html(xhr.responseText);
                    }
                }
            });
        }
    });

}











function confirmJoin() {
    debugger;

    $('#DOB_Password').val(_GetDOBPassword(document.getElementById("DOB_Password").value));
    $('#MOBILE_password').val($('#Enrollment_PreEnrollment_PermanentAddress_MobileNo').val());

    $.ajax({

        type: 'POST',
        url: '/DataBank/JoiningConfirmation/ConfirmJoin',
        dataType: 'json',
        data: $('#Join').serialize(),
        //data:
        //{
        //    preEnrollGI: preEnrollId
        //},
        success: function (result) {
            debugger;
            console.log(result);
            if (result.success == true) {
                swal.fire(result.message).then(function () {
                    window.location.href = '/DataBank/OnBoarding/Index';
                });

            }
            else {
                swal.fire(result.message).then(function () {
                    window.location.href = '/DataBank/JoiningConfirmation/Create?EnrollGI=' + preEnrollId;

                });
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);
            }
        }
    });



    //swal.fire({
    //    text: "Do you want to Confirm?",
    //    icon: 'warning',
    //    showCancelButton: true,
    //    confirmButtonColor: '#3085d6',
    //    cancelButtonColor: '#d33',
    //    confirmButtonText: 'Ok'
    //}).then((result) => {
    //    if (result.isConfirmed) {
    //        var preEnrollId = $('#Enrollment_EnrollGUID').val();
    //        $.ajax({

    //            type: 'POST',
    //            url: '/DataBank/JoiningConfirmation/ConfirmJoin',
    //            dataType: 'json',
    //            data: $('#Join').serialize(),
    //            //data:
    //            //{
    //            //    preEnrollGI: preEnrollId
    //            //},
    //            success: function (result) {
    //                debugger;
    //                console.log(result);
    //                if (result.success == true) {
    //                    swal.fire(result.message).then(function () {
    //                        window.location.href = '/DataBank/OnBoarding/Index';
    //                    });

    //                }
    //                else {
    //                    swal.fire(result.message).then(function () {
    //                        window.location.href = '/DataBank/JoiningConfirmation/Create?EnrollGI=' + preEnrollId;

    //                    });
    //                }
    //            },
    //            error: function (xhr, textStatus, errorThrown) {
    //                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
    //                    window.location.reload();
    //                }

    //                else {
    //                    document.body.id = "kt_body";
    //                    $('body').html(xhr.responseText);
    //                }
    //            }
    //        });
    //    }
    //});

}





