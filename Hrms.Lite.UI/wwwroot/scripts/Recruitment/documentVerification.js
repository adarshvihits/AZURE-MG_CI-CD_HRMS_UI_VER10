function EditAadharOrPan(DocName) {
   /* var url = "/Recruitment/DocumentVerification/UploadAadharOrPan";*/
    var ApplicantGI = $('#EmployeeGI').val();
   /* $('#sliderDiv').load(url, { DocumentName: DocName, ApplicantGI: ApplicantGI }, function () { });*/
    $.ajax({

        type: 'GET',
        url: '/Recruitment/DocumentVerification/UploadAadharOrPan',
        dataType: 'html',
        data: { DocumentName: DocName, ApplicantGI: ApplicantGI },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {
                $('#sliderDiv').html("");

                
                $('#sliderDiv').html(result);
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

function ViewAadharOrPan(DocName, path, trackingCode) {
    debugger
    var ApplicantGI = $('#EmployeeGI').val();    

    $.ajax({

        type: 'GET',
        url: '/Recruitment/DocumentVerification/ViewAadharOrPan',
        dataType: 'html',
        data: { DocumentName: DocName, ApplicantGI: ApplicantGI, imagePath: path, TrackingCode: trackingCode },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {
                $('#sliderDiv').html("");

              
                $('#sliderDiv').html(result);
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

function EditDrivingLicenseOrPassport(DocName) {
   /* var url = "/Recruitment/DocumentVerification/UploadDrivingLicenseOrPassport";*/
    var ApplicantGI = $('#EmployeeGI').val();
    /*$('#sliderDiv').load(url, { DocumentName: DocName, ApplicantGI: ApplicantGI }, function () { });*/
    $.ajax({

        type: 'GET',
        url: '/Recruitment/DocumentVerification/UploadDrivingLicenseOrPassport',
        dataType: 'html',
        data: { DocumentName: DocName, ApplicantGI: ApplicantGI },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {
                $('#sliderDiv').html("");
                $('#sliderDiv').html(result);
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


function onAddNewClick(EmployeeGI) {
    debugger;
    var url = "/Recruitment/DocumentVerification/AddDocument";
    $('#sliderDiv').load(url, { EmployeeGI: EmployeeGI }, function () { });
    $.ajax({

        type: 'GET',
        url: '/Recruitment/DocumentVerification/AddDocument',
        dataType: 'html',
        data: { EmployeeGI: EmployeeGI },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {
                $('#sliderDiv').html("");
                $('#sliderDiv').html(result);
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


function EditOtherDocuments(EmployeeGI, docName, trackingCode) {

    $.ajax({

        type: 'GET',
        url: '/Recruitment/DocumentVerification/EditOtherDocuments',
        dataType: 'html',
        data: { OtherDocName: docName, EmployeeGI: EmployeeGI, TrackingCode:trackingCode},
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {
                $('#sliderDiv').html("");
                $('#sliderDiv').html(result);
                $('#documentNm').html(docName);
                $('#SliderDocumentName').val(docName);
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

function previewDocument(absoluteURI) {
    window.open(absoluteURI, "_blank");
}
function validateAadharOrPan() {
    var i = 0;
    //$.ajaxSetup({
    //    async: false
    //});
    //if ($('#SliderTitle').val() != "Pan Card") {
    //    var AadharNum = document.getElementById("AadharNumber");

    //    abcd = validateAadhar(AadharNum, 'aadhar');
    //    debugger;
    //    if (!validateById('#NameInAadhar', 'NameValidation')) {
    //        i++;
    //    }
    //    if (!validateAadhar(AadharNum, 'aadhar')) {
    //        i++;
    //    }
    //    if (!validateById('#AadharIssueDate', 'IssueDateValidation')) {
    //        i++;
    //    }
    //    if ($('#File_FileData').val() == '' && $('#File_AbsoluteUri').val() == '') {
    //        i++;
    //        swal.fire("Please upload the document");
    //    }

    //}
    //else {
    //    var PanNum = document.getElementById("PANNumber");
    //    var i = 0;
    //    if (!validateById('#NameInPAN', 'NameValidation')) {
    //        i++;
    //    }
    //    if (!validatePan(PanNum, 'pan')) {
    //        i++;
    //    }
    //    if (!validateById('#PANIssueDate', 'IssueDateValidation')) {
    //        i++;
    //    }
    //    if ($('#File_FileData').val() == '' && $('#File_AbsoluteUri').val() == '') {
    //        i++;
    //        swal.fire("Please upload the document");
    //    }

    //}
    
    if (i == 0)
        swal.fire({
            text: "Do you want to Modify?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                var data = new FormData($('#AadharOrPanSave')[0]);

                
                $.ajax({

                    type: 'POST',
                    url: '/Recruitment/DocumentVerification/IdentityDocumentSave',
                    contentType: false,
                    processData: false,
                    dataType: 'json',
                    data: data,
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire("Modified Successfully").then(function () {
                                CloseSlider('addnewDoc');
                                window.location.reload();
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
    else
        return false;
}

function approveDocument() {
    var i = 0;
    
    if (i == 0)
        swal.fire({
            text: "Do you want to Verify?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {

              // var data= $('#DocType').val();

              //  console.log($('#AadharOrPanApprove')[0]);

                var data = new FormData($('#AadharOrPanApprove')[0]);

              

                $.ajax({

                    type: 'POST',
                    url: '/Recruitment/DocumentVerification/DocumentApprove',
                    contentType: false,
                    processData: false,
                    dataType: 'json',
                    data: data,

                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                ClosedocVerifnSlider('viewAadharOrPan');
                                window.location.reload();
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
    else
        return false;
}

function ClosedocVerifnSlider(classname) {
    debugger
    $("." + classname ).removeClass('slider-forms-open');
    $(".form-overlay").remove();
    $("." + classname).remove();
}

function validateDrivingLicenseOrPassport() {
    var i = 0;
    //if ($('#SliderTitle').val() != "Passport") {


    //    if (!validateById('#NameInDrivingLicense', 'NameValidation')) {
    //        i++;
    //    }
    //    if (!validateById('#DrivingLicenseNumber', 'DocNumber')) {
    //        i++;
    //    }
    //    if (!validateFromDate($('#DrivingLicenseValidFrom'), 'FromDate', 'DrivingLicenseValidTo')) {
    //        i++;
    //    }
    //    if (!validateToDate($('#DrivingLicenseValidTo'), 'ToDate', 'DrivingLicenseValidFrom')) {
    //        i++;
    //    }
    //    if ($('#File_FileData').val() == '' && $('#File_AbsoluteUri').val() == '') {
    //        i++;
    //        swal.fire("Please upload the document");
    //    }

    //}
    //else {
    //    var i = 0;
    //    if (!validateById('#NameInPassport', 'NameValidation')) {
    //        i++;
    //    }
    //    if (!validateById('#PassportNumber', 'DocNumber')) {
    //        i++;
    //    }
    //    if (!validateFromDate('#PassportValidFrom', 'FromDate', 'PassportValidTo')) {
    //        i++;
    //    }
    //    if (!validateToDate('#PassportValidTo', 'ToDate', 'PassportValidFrom')) {
    //        i++;
    //    }
    //    if ($('#File_FileData').val() == '' && $('#File_AbsoluteUri').val() == '') {
    //        i++;
    //        swal.fire("Please upload the document");
    //    }

    //}

    if (i == 0)
        swal.fire({
            text: "Do you want to Modify?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                var data = new FormData($('#LicenseOrPassportSave')[0]);
                $.ajax({

                    type: 'POST',
                    url: '/Recruitment/DocumentVerification/IdentityDocumentSave',
                    contentType: false,
                    processData: false,
                    dataType: 'json',
                    data: data,
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire("Modified Successfully").then(function () {
                                CloseSlider('addnewDoc');
                                window.location.reload();

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
    else
        return false;

}


function validateAll(mode) {
    var msg;
    var data;
    var i = 0;
    //if (!validateById('#SliderDocumentType_Code', 'DocType')) {
    //    i++;
    //}
    //if (!validateById('#SliderDocumentName', 'DocName')) {
    //    i++;
    //}
    //if (mode != 'Modify') {
    //    if ($('#File_FileData').val() == '') {
    //        i++;
    //        swal.fire("Please upload the document");
    //    }
    //}
    if (mode != 'Modify')
        msg = "Do you want to Save?";
    else
        msg = "Do you want to Modify?";
    if (mode != 'Modify')
        data = new FormData($('#otherDocumentsSave')[0]);
    else
        data = new FormData($('#editOtherDocumentsSave')[0]);
    debugger;
    if (i == 0)
        swal.fire({
            text: msg,
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({

                    type: 'POST',
                    url: '/Recruitment/DocumentVerification/OtherDocumentSave',
                    contentType: false,
                    processData: false,
                    dataType: 'json',
                    data: data,
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                if (mode != 'Modify')
                                    CloseSlider('addnewDoc');
                                else
                                    CloseSlider('editOtherDoc');
                                //GetOtherDocumentsList();
                                window.location.reload();

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
    else
        return false;
}

function DeleteIdentityDocuments() {
    swal.fire({
        text: "Do you want to Delete?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            var EmployeeGI = $('#EmployeeGI').val();
            var sliderTitle = $('#SliderTitle').val();
            //var url = "/Recruitment/DocumentVerification/DeleteIdentityDocuments?EmployeeGI=" + EmployeeGI + "&sliderTitle=" + sliderTitle;
            //window.location.href = url;


            $.ajax({

                type: 'POST',
                url: '/Recruitment/DocumentVerification/DeleteIdentityDocuments',              
                dataType: 'json',
                data: { EmployeeGI: EmployeeGI, sliderTitle: sliderTitle},
                success: function (result) {
                    if (result.success == true) {
                        swal.fire("Deleted Successfully").then(function () {
                            CloseSlider('uploadAadharOrPan');
                            CloseSlider('uploadLicenseOrPassport')
                            window.location.reload();

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

function DeleteOtherDocuments() {
    swal.fire({
        text: "Do you want to Delete?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
           
            var OtherDocGI = $('#SliderDocumentGI').val();   
            var EmployeeGI = $('#EmployeeGI').val();
            //var url = "/Recruitment/DocumentVerification/DeleteOtherDocuments?OtherDocGI=" + OtherDocGI;
            //window.location.href = url;

            $.ajax({

                type: 'POST',
                url: '/Recruitment/DocumentVerification/DeleteOtherDocuments',                
                dataType: 'json',
                data: { OtherDocGI: OtherDocGI, EmployeeGI: EmployeeGI},
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {                            
                                CloseSlider('addnewDoc');                           
                                CloseSlider('editOtherDoc');
                            var url2 = "/Recruitment/DocumentVerification/GetOtherDocList";
                            $('#OtherDocsList').load(url2, { EmployeeGI: EmployeeGI }, function () { })

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

function VerifyDoc(CandidateID, status) {
    swal.fire({
        text: "Do you want to Verify?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            var EmployeeGI = $('#EmployeeGI').val();
            var sliderTitle = $('#SliderTitle').val();
            //var url = "/Recruitment/DocumentVerification/DeleteIdentityDocuments?EmployeeGI=" + EmployeeGI + "&sliderTitle=" + sliderTitle;
            //window.location.href = url;


            $.ajax({

                type: 'POST',
                url: '/Recruitment/DocumentVerification/Verify',
                dataType: 'json',
                data: { CandidateID: CandidateID, status: status},
                success: function (result) {
                    if (result.success == true) {
                        swal.fire("Verified Successfully").then(function () {

                            var url2 = "/Recruitment/DocumentVerification/Index";
                            window.location.href = url2;
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

function ExportToExcel() {
    var cnt = $('#Document_List_Count').val();
    if (cnt > 0) {
        var url = "/Recruitment/DocumentVerification/ExportToExcel"
        window.location.href = url;
    }
}

function viewFile(DocName,url, trackingCode) {

    ViewAadharOrPan(DocName, url, trackingCode);

    //if (url != '')
    //    window.open(url, '_blank');
    //else
    //    swal.fire("No file uploaded");
}

function Preview() {
    if ($('#File_FileData').val() != '') {
        var path = URL.createObjectURL($('.PreviewImage')[0].files[0]);
        window.open(path, "_blank");
    }
    else if ($('#File_AbsoluteUri').val() != '') {
        window.open($('#File_AbsoluteUri').val(), "_blank");
    }
}