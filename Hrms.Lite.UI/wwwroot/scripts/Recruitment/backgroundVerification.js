var prevTab = 'tab1';
var currentTab = 'All';
var currTabName = 'All';
var CurrTabNameSalSplitUp = 'Pending';
var CurrTabNameEmpCheck = 'Pending';
var CurrTabNameCredCheck = 'Pending';
function CreditCheckTab(Tab, TabIndex, TabName) {
    debugger;
    /* showSpinner();*/
    currentTab = TabIndex;
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");
    CurrTabNameCredCheck = TabName
    //$('#tabsDropdownDivision').val(currentTab);
    //var url = "/DataBank/VacancyManagement/GetList";

    //$('#VacancyList').load(url, { TabIndex: TabIndex }, function () { });
    $.ajax({

        type: 'GET',
        url: '/Recruitment/BackGroundVerification/GetBackGroundVerificationList',
        dataType: 'html',
        data: { TabName: TabName },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {
                $('#CredCheckList').html("");
                $('#CredCheckList').html(result);
                if (TabName == 'Pending') {
                    $('.background_tracker_list').addClass('background_verificationvertification_newlist');
                    $('.background_verificationvertification_newlist').removeClass('background_tracker_list');
                }
                else {
                    $('.background_verificationvertification_newlist').addClass('background_tracker_list');
                    $('.background_tracker_list').removeClass('background_verificationvertification_newlist');
                }
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


function GetCreditCheck() {
    var url = "/BackGroundVerification/Creditcheck/Index";
    window.location.href = url;
}


function onEmpClick(PreEnrollGI, form) {
    debugger


    $.ajax({

        type: 'GET',
        url: '/Recruitment/BackGroundVerification/GetCheckingDetails',
        dataType: 'html',
        data: { PreEnrollGI: PreEnrollGI, formName: form },
        success: function (result) {
            debugger;
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {
                $('body').append('<div class="form-overlay"></div>');

                $('#creditSlider').html("");
                $('#creditSlider').html(result);

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

    //else
    //    $('#employmntSlider').load(url, { PreEnrollGI: PreEnrollGI, formName: form }, function () { });
}



function Confirm() {
    var i = 0;
    debugger  

    if (!validateById('#BasicDetailsStatusReason', 'BasicDtlsStatus')) {
        i++;
    }


    if ($('#EmployeeDoccumentList_Count').val() > 0) {
        if (!validateById('#DocumentDetailsStatusReason', 'DocumentDetails')) {
            i++;
        }
    }
    if ($('#EducationalDetailsList_Count').val() > 0) {
        if (!validateById('#EducationDetailsStatusReason', 'EducationDetails')) {
            i++;
        }
    }
    if ($('#ExperianceDetailsList_Count').val() > 0) {
        if (!validateById('#EducationDetailsStatusReason', 'EducationDetails')) {
            i++;
        }
    }
    if (i == 0) {
        swal.fire({
            text: "Do you want to Confirm?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {               
                $.ajax({
                    type: 'POST',
                    url: '/Recruitment/BackGroundVerification/SaveBackgroundVerification',
                    dataType: 'json',
                    data: $('#BGVEmpSave').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire("Confirmed Successfully").then(function () {                               
                                url = '/Recruitment/BackGroundVerification/Index';
                                window.location.href = url;
                                //$.ajax({

                                //    type: 'GET',
                                //    url: url,
                                //    dataType: 'html',
                                //    success: function (result) {
                                //        debugger
                                //        console.log(result);
                                //        if (form == "CreditChecking") {
                                //            $('.credit-checking-list').html("");
                                //            $('.credit-checking-list').html(jQuery(result).find('.credit-checking-list').html());
                                //        }
                                //        else {
                                //            $('.employment-checking-list').html("");
                                //            $('.employment-checking-list').html(jQuery(result).find('.employment-checking-list').html());
                                //        }
                                //    }
                                //    });

                            });
                        }
                        else {
                            swal.fire(result.message).then(function () {

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
            else {
                return false;
            }
        })
    }
    else
        return false;
}

function ExportTopExcelClick() {
    debugger;
    var CreditCheckingTable = $('#BackgroundVerificationList_Count').val();
    /* var CancellationTable = document.getElementById("CancellationListTable").rows.length - 1;*/

    if (CreditCheckingTable > 0) {
        url = "/Recruitment/BackGroundVerification/ExportToExcel?TabName=" + CurrTabNameCredCheck;
        window.location.href = url;

    }
    else {
        swal.fire("No data found to export");
    }
}



function onEmpCreditCheckDetailsClick(PreEnrollGI, form) {
    debugger
    url = "/Recruitment/BackGroundVerification/EmpCreditCheckDetailsClick?PreEnrollGI=" + PreEnrollGI + '&form=' + form;
    window.location.href = url;

}

function onBasicDetailsSave(mode) {
    if (validateAddress()) {
        debugger;
        if (mode == 'Verify')
            $('#BasicDetailsStatus').val(1);
        else
            $('#BasicDetailsStatus').val(2);


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
                    url: '/Recruitment/BackGroundVerification/BackGroundVerificationDetailsSave',
                    dataType: 'json',
                    data: $('#BasicDetailsSave').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            var msg = '';
                            if (mode == 'Verify')
                                msg = 'Verified Successfully';
                            else
                                msg = 'Rejected Successfully';
                            swal.fire(msg).then(function () {
                                window.location.reload();
                            });

                        }

                        else {
                            swal.fire("Something went wrong.");
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
            else {
                return false;
            }
        })





       
    }
}

function onEducationDetailsSave(mode) {
    debugger;
    if (validateEducation()) {
        if (mode == 'Verify')
            $('#EducationDetailsStatus').val(1);
        else
            $('#EducationDetailsStatus').val(2);


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
                    url: '/Recruitment/BackGroundVerification/BackGroundVerificationDetailsSave',
                    dataType: 'json',
                    data: $('#EducationDetailsSave').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            var msg = '';
                            if (mode == 'Verify')
                                msg = 'Verified Successfully';
                            else
                                msg = 'Rejected Successfully';
                            swal.fire(msg).then(function () {
                                window.location.reload();
                            });

                        }

                        else {
                            swal.fire("Something went wrong.");
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
            else {
                return false;
            }
        })

       
    }
}

function onDocumentDetailsSave(mode) {
    debugger;
    if (validateDocument()) {
        if (mode == 'Verify')
            $('#DocumentDetailsStatus').val(1);
        else
            $('#DocumentDetailsStatus').val(2);

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
                    url: '/Recruitment/BackGroundVerification/BackGroundVerificationDetailsSave',
                    dataType: 'json',
                    data: $('#DocumentDetailsSave').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            var msg = '';
                            if (mode == 'Verify')
                                msg = 'Verified Successfully';
                            else
                                msg = 'Rejected Successfully';
                            swal.fire(msg).then(function () {
                                window.location.reload();
                            });

                        }

                        else {
                            swal.fire("Something went wrong.");
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
            else {
                return false;
            }
        })

       
    }
}

function onExperianceDetailsSave(mode) {
    debugger;
    if (validateExperience()) {
        if (mode == 'Verify')
            $('#PreEmployerDetailsStatus').val(1);
        else
            $('#PreEmployerDetailsStatus').val(2);

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
                    url: '/Recruitment/BackGroundVerification/BackGroundVerificationDetailsSave',
                    dataType: 'json',
                    data: $('#ExperianceDetailsSave').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            var msg = '';
                            if (mode == 'Verify')
                                msg = 'Verified Successfully';
                            else
                                msg = 'Rejected Successfully';
                            swal.fire(msg).then(function () {
                                window.location.reload();
                            });

                        }

                        else {
                            swal.fire("Something went wrong.");
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
            else {
                return false;
            }
        })
        
    }

}





function validateAddress() {
    var i = 0;
    if (!validateById('#BasicDetailsStatusReason', 'BasicDtlsStatus')) {
        i++;
    }


    if (i == 0)
        return true;
    else
        return false;
}
function validateEducation() {
    var i = 0;
    if (!validateById('#EducationDetailsStatusReason', 'EducationDetails')) {
        i++;
    }


    if (i == 0)
        return true;
    else
        return false;
}

function validateDocument() {
    var i = 0;
    if (!validateById('#DocumentDetailsStatusReason', 'DocumentDetails')) {
        i++;
    }


    if (i == 0)
        return true;
    else
        return false;
}

function validateExperience() {
    var i = 0;
    if (!validateById('#PreEmployerDetailsStatusReason', 'PreEmployerDetails')) {
        i++;
    }


    if (i == 0)
        return true;
    else
        return false;
}

