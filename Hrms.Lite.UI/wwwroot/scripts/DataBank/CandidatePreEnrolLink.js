var prevTab = 'PreEnrollLink';
var currentTab = 'CANDIDATELINK';

function OnEmpNameClick(PreEnrollGI, index) {
    debugger;

    var url = "/DataBank/CandidatePreEnrolLink/Create?PreEnrollGI=" + PreEnrollGI;
        window.location.href = url;
    
}


function PersonalProfileAddressApprove(mode) {
    debugger;
    $('#ApproveMode').val(mode);
    if (mode == "APPROVE")
        modeName = "Approve";
    else
        modeName = "Reject";
    swal.fire({
        text: "Do you want to " + modeName + " ?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({

                type: 'POST',
                url: '/DataBank/CandidatePreEnrolLink/ApprovePersonalProfile?PreEnrollGI=' + $('#enrollment_PreEnrollment_PreEnrollGI').val() + "&Apmode=" + mode,
                dataType: 'json',
                data: $('#AddressSave').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            debugger
                            window.location.href = '/DataBank/CandidatePreEnrolLink/Create?PreEnrollGI=' + $("#enrollment_PreEnrollment_PreEnrollGI").val();
                          

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


function PersonalProfileContactDetailsApprove(mode) {
    debugger
    $('#ApproveMode').val(mode);
    if (mode == "APPROVE")
        modeName = "Approve";
    else
        modeName = "Reject";
    swal.fire({
        text: "Do you want to " + modeName + " ?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({

                type: 'POST',
                url: '/DataBank/CandidatePreEnrolLink/ApprovePersonalProfile?PreEnrollGI=' + $('#enrollment_PreEnrollment_PreEnrollGI').val() + "&Apmode=" + mode,
                dataType: 'json',
                data: $('#ContactDetailsSave').serialize() ,


                success: function (result) {
                    if (result.success == true) {
                        window.location.href = '/DataBank/CandidatePreEnrolLink/Create?PreEnrollGI=' + $("#enrollment_PreEnrollment_PreEnrollGI").val();


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

function PersonalProfileInfoApprove(mode) {
    debugger
    $('#ApproveMode').val(mode);
    if (mode == "APPROVE")
        modeName = "Approve";
    else
        modeName = "Reject";
    swal.fire({
        text: "Do you want to " + modeName + " ?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({

                type: 'POST',
                url: '/DataBank/CandidatePreEnrolLink/ApprovePersonalProfile?PreEnrollGI=' + $('#enrollment_PreEnrollment_PreEnrollGI').val() + "&Apmode=" + mode,
                dataType: 'json',
                data: $('#PersonalInfoSave').serialize() ,
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {

                            window.location.href = '/DataBank/CandidatePreEnrolLink/Create?PreEnrollGI=' + $("#enrollment_PreEnrollment_PreEnrollGI").val();
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

function PersonalProfileEmployeeVsNomineeApprove(mode) {
    debugger;
    $('#ApproveMode').val(mode);
    if (mode == "APPROVE")
        modeName = "Approve";
    else
        modeName = "Reject";
    swal.fire({
        text: "Do you want to " + modeName + " ?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({

                type: 'POST',
                url: '/DataBank/CandidatePreEnrolLink/ApprovePersonalProfile?PreEnrollGI=' + $('#enrollment_PreEnrollment_PreEnrollGI').val() + "&Apmode=" + mode,
                dataType: 'json',
                data: $('#EmployeeVsNomineeSave').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            debugger
                            window.location.href = '/DataBank/CandidatePreEnrolLink/Create?PreEnrollGI=' + $("#enrollment_PreEnrollment_PreEnrollGI").val();


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


function PersonalProfileBankDetailsApprove(mode) {
    debugger;
    $('#ApproveMode').val(mode);
    if (mode == "APPROVE")
        modeName = "Approve";
    else
        modeName = "Reject";
    swal.fire({
        text: "Do you want to " + modeName + " ?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({

                type: 'POST',
                url: '/DataBank/CandidatePreEnrolLink/ApprovePersonalProfile?PreEnrollGI=' + $('#enrollment_PreEnrollment_PreEnrollGI').val() + "&Apmode=" + mode,
                dataType: 'json',
                data: $('#BankDetailsSave').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            debugger
                            window.location.href = '/DataBank/CandidatePreEnrolLink/Create?PreEnrollGI=' + $("#enrollment_PreEnrollment_PreEnrollGI").val();


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

function PersonalProfileSkillApprove(Index, mode) {
    debugger
    $('#Skills_' + Index + '__flag').val(1);
    if (mode == "APPROVE")
        modeName = "Approve";
    else
        modeName = "Reject";
    swal.fire({
        text: "Do you want to " + modeName + " ?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({

                type: 'POST',
                url: '/DataBank/CandidatePreEnrolLink/ApprovePersonalProfileLists?PreEnrollGI=' + $('#enrollment_PreEnrollment_PreEnrollGI').val() + "&Apmode=" + mode,
                dataType: 'json',
                cache: false,
                data: $('#SkillsSave').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {

                            window.location.href = '/DataBank/CandidatePreEnrolLink/Create?PreEnrollGI=' + $('#enrollment_PreEnrollment_PreEnrollGI').val();
                        });

                    }
                    else {
                        $('#Skills_' + Index + '__flag').val(0);
                        swal.fire(result.message);
                    }
                },
                error: function (xhr, textStatus, errorThrown) {
                    $('#Skills_' + Index + '__flag').val(0);
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

function PersonalProfileCertificateApprove(Index, mode) {

    debugger;
    $('#Certifications_' + Index + '__flag').val(1);
    if (mode == "APPROVE")
        modeName = "Approve";
    else
        modeName = "Reject";
    swal.fire({
        text: "Do you want to " + modeName + " ?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({

                type: 'POST',
                url: '/DataBank/CandidatePreEnrolLink/ApprovePersonalProfileLists?PreEnrollGI=' + $('#enrollment_PreEnrollment_PreEnrollGI').val() + "&Apmode=" + mode,
                dataType: 'json',
                data: $('#certificateApprove').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            window.location.href = '/DataBank/CandidatePreEnrolLink/Create?PreEnrollGI=' + $("#enrollment_PreEnrollment_PreEnrollGI").val();
                        });

                    }
                    else {
                        $('#Certifications_' + Index + '__flag').val(0);
                        swal.fire(result.message);
                    }
                },
                error: function (xhr, textStatus, errorThrown) {
                    $('#Certifications_' + Index + '__flag').val(0);
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


function PersonalProfileFamilyApprove(Index, mode) {
    debugger;
    $('#FamilyDetails_' + Index + '__flag').val(1);
    if (mode == "APPROVE")
        modeName = "Approve";
    else
        modeName = "Reject";
    swal.fire({
        text: "Do you want to " + modeName + " ?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({

                type: 'POST',
                url: '/DataBank/CandidatePreEnrolLink/ApprovePersonalProfileLists?PreEnrollGI=' + $('#enrollment_PreEnrollment_PreEnrollGI').val() + "&Apmode=" + mode,
                dataType: 'json',
                data: $('#FamilySave').serialize() ,
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {

                            window.location.href = '/DataBank/CandidatePreEnrolLink/Create?PreEnrollGI=' + $("#enrollment_PreEnrollment_PreEnrollGI").val();
                        });

                    }
                    else {
                        $('#FamilyDetails_' + Index + '__flag').val(0);
                        swal.fire(result.message);
                    }
                },
                error: function (xhr, textStatus, errorThrown) {
                    $('#FamilyDetails_' + Index + '__flag').val(0);
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

function PersonalProfilePreviousEmploymentApprove(Index, mode) {
    debugger;
    $('#PreviousEmployment_' + Index + '__flag').val(1);
    if (mode == "APPROVE")
        modeName = "Approve";
    else
        modeName = "Reject";
    swal.fire({
        text: "Do you want to " + modeName + " ?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({

                type: 'POST',
                url: '/DataBank/CandidatePreEnrolLink/ApprovePersonalProfileLists?PreEnrollGI=' + $('#enrollment_PreEnrollment_PreEnrollGI').val() + "&Apmode=" + mode,
                dataType: 'json',
                data: $('#PrevEmpSave').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {

                            window.location.href = '/DataBank/CandidatePreEnrolLink/Create?PreEnrollGI=' + $("#enrollment_PreEnrollment_PreEnrollGI").val();
                        });

                    }
                    else {
                        $('#PreviousEmployment_' + Index + '__flag').val(0);
                        swal.fire(result.message);
                    }
                },
                error: function (xhr, textStatus, errorThrown) {
                    $('#PreviousEmployment_' + Index + '__flag').val(0);
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

function PersonalProfileDocumentDetailsApprove(Index, mode) {
    debugger;
    $('#CandidateDocumentUpload_' + Index + '__flag').val(1);
    if (mode == "APPROVE")
        modeName = "Approve";
    else
        modeName = "Reject";
    swal.fire({
        text: "Do you want to " + modeName + " ?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({

                type: 'POST',
                url: '/DataBank/CandidatePreEnrolLink/ApprovePersonalProfileLists?PreEnrollGI=' + $('#enrollment_PreEnrollment_PreEnrollGI').val() + "&Apmode=" + mode,
                dataType: 'json',
                data: $('#DocumentsSave').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {

                            window.location.href = '/DataBank/CandidatePreEnrolLink/Create?PreEnrollGI=' + $("#enrollment_PreEnrollment_PreEnrollGI").val();
                        });

                    }
                    else {
                        $('#CandidateDocumentUpload_' + Index + '__flag').val(0);
                        swal.fire(result.message);
                    }
                },
                error: function (xhr, textStatus, errorThrown) {
                    $('#CandidateDocumentUpload_' + Index + '__flag').val(0);
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

function PersonalProfilePoliceVerificationDetailsApprove(mode) {
    debugger;
    $('#ApproveMode').val(mode);
    if (mode == "APPROVE")
        modeName = "Approve";
    else
        modeName = "Reject";
    swal.fire({
        text: "Do you want to " + modeName + " ?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({

                type: 'POST',
                url: '/DataBank/CandidatePreEnrolLink/ApprovePersonalProfile?PreEnrollGI=' + $('#enrollment_PreEnrollment_PreEnrollGI').val() + "&Apmode=" + mode,
                dataType: 'json',
                data: $('#PoliceVerificationSave').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            debugger
                            window.location.href = '/DataBank/CandidatePreEnrolLink/Create?PreEnrollGI=' + $("#enrollment_PreEnrollment_PreEnrollGI").val();


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
function PersonalProfileMedicalCheckupDetailsApprove(mode) {
    debugger;
    $('#ApproveMode').val(mode);
    if (mode == "APPROVE")
        modeName = "Approve";
    else
        modeName = "Reject";
    swal.fire({
        text: "Do you want to " + modeName + " ?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({

                type: 'POST',
                url: '/DataBank/CandidatePreEnrolLink/ApprovePersonalProfile?PreEnrollGI=' + $('#enrollment_PreEnrollment_PreEnrollGI').val() + "&Apmode=" + mode,
                dataType: 'json',
                data: $('#MedicalCheckupSave').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            debugger
                            window.location.href = '/DataBank/CandidatePreEnrolLink/Create?PreEnrollGI=' + $("#enrollment_PreEnrollment_PreEnrollGI").val();


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
//listniv
function ListWisePersonalProfileFamilyApprove(GI, mode, Index, TabIndex) {
    debugger;
    var EmpGI = $('#PreEnrollGI').val();
    $.ajax({

        type: 'POST',
        url: '/DataBank/CandidatePreEnrolLink/ListWiseApproveRejectPersonalProfile',
        dataType: 'json',
        data: { EmpGI: EmpGI, GI: GI, Apmode: mode, TabIndex: TabIndex },
        success: function (result) {
            if (result.success == true) {
                swal.fire(result.message).then(function () {

                    window.location.href = '/DataBank/CandidatePreEnrolLink/Create?PreEnrollGI=' + $("#enrollment_PreEnrollment_PreEnrollGI").val();
                });

            }
            else {
                $('#FamilyDetails_' + Index + '__flag').val(0);
                swal.fire(result.message);
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            $('#FamilyDetails_' + Index + '__flag').val(0);
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
        }
    });
}

function PersonalProfileEducationApprove(Index, mode) {
    debugger
    $('#Education_' + Index + '__flag').val(1);
    if (mode == "APPROVE")
        modeName = "Approve";
    else
        modeName = "Reject";
    swal.fire({
        text: "Do you want to " + modeName + " ?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({

                type: 'POST',
                url: '/DataBank/CandidatePreEnrolLink/ApprovePersonalProfileLists?PreEnrollGI=' + $('#enrollment_PreEnrollment_PreEnrollGI').val() + "&Apmode=" + mode,
                dataType: 'json',
                data: $('#EducationSave').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            window.location.href = '/DataBank/CandidatePreEnrolLink/Create?PreEnrollGI=' + $("#enrollment_PreEnrollment_PreEnrollGI").val();
                        });

                    }
                    else {
                        $('#Education_' + Index + '__flag').val(0);
                        swal.fire(result.message);
                    }
                },
                error: function (xhr, textStatus, errorThrown) {
                    $('#Education_' + Index + '__flag').val(0);
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

function Profile_PicApprove(mode) {
    debugger;
    $('#ApproveMode').val(mode);
    if (mode == "APPROVE")
        modeName = "Approve";
    else
        modeName = "Reject";
    swal.fire({
        text: "Do you want to " + modeName + " ?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({

                type: 'POST',
                url: '/DataBank/CandidatePreEnrolLink/ApprovePersonalProfile?PreEnrollGI=' + $('#enrollment_PreEnrollment_PreEnrollGI').val() + "&Apmode=" + mode,
                dataType: 'json',
                data: $('#per_Photoform').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            debugger
                            /* window.location.href = '/DataBank/CandidatePreEnrolLink/Create?EmployeeGI=' + $("#enrollment_PreEnrollment_PreEnrollGI").val();*/
                            window.location.reload();

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

function OnTabClick(Tab, TabIndex) {
    debugger;
    var radiomode = $("input[name='radiomode']:checked").val();
 
    currentTab = TabIndex;

    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");

    if (TabIndex == "CANDIDATELINK") {

        $("#radio").removeClass("hide");
        var preenrolstatus = 0;
    }
    else {
        $("#radio").addClass("hide");
        //var preenrolstatus = 1;
    }

    filter = $("#BasicFilter").serialize();
    $.ajax({

        type: 'GET',
        url: '/DataBank/CandidatePreEnrolLink/GetList?TabIndex=' + currentTab + "&radiomode=" + radiomode,
        dataType: 'html',
        data: filter,

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#PreEnrollCandidateLinkApprovalList').html("");
                $('#PreEnrollCandidateLinkApprovalList').html(result);
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
function fillData() {
    debugger;
if (currentTab == 'CANDIDATELINK') {

        debugger;
        var filter1 = $('#BasicFilter').serialize();

        var doc = 1;

        var temp = filter1;

        var dataTables = $("#offerLetterList_Table").DataTable({

            "pagingType": 'full_numbers',
            "destroy": 'true',
            "ajax": {
                "url": '/DataBank/CandidatePreEnrolLink/GetList?TabIndex=' + currentTab + "&radiomode=" + radiomode,
                "type": "POST",


                "datatype": "json",


                'data':

                    function (d) {
                        temp = '';
                        var start = d.start;
                        debugger;
                        var searchValue = $('#dataTableSearch').val();
                        temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&DocType=' + doc + '&Tabindex=' + currentTab + '&dummy=dummy';
                        d.filter = temp;

                        return JSON.stringify(d);
                    },

                "dataSrc": function (d) {

                    $('#PersonalDetailsList_Count').val(d.recordsTotal);


                    for (var i = 0, ien = d.data.length; i < ien; i++) {



                        //d.data[i].dob = '<a href="/DataBank/offer/GetOfferLetterPDF?PreEnrollCode=' + d.data[i].preEnrollCode + '">' + "Verify" + '</a>'
                        d.data[i].dob = '<a onclick="getData(' + "'" + d.data[i].preEnrollCode + "'" + ',' + "'" + d.data[i].employeeGI + "'" + ',' + "'" + d.data[i].employeeCode + "'" + ',' + d.data[i].letterFormatCode + ')">' + "Verify" + '</a>'


                        d.data[i].doj = _GetFormattedDate(d.data[i].doj);
                        //d.data[i].lwd = _GetFormattedDate(d.data[i].lwd);


                    }

                    return d.data;
                }

            },

            "columns": [

                { "data": "employeeID", "width": "10%" },
                { "data": "employeeName", "width": "16%" },
                { "data": "designation", "width": "10%" },
                { "data": "department", "width": "10%" },
                { "data": "location", "width": "10%" },
                { "data": "doj", "width": "8%" },
                { "data": "dob", "width": "10%" },



            ],

            /* "dom": '<t><"my-wrapper"<"my-inner2"l><"my-inner1"i><"my-inner3"p>>',*/



            "dom": 'rt<"bottom"><"my-wrapper"<"my-inner2"l><"my-inner1"i><"my-inner3"p>>',
            "serverSide": "true",
            "scrollY": '52vh',
            "sScrollX": "100%",
            "sScrollXInner": "100%",

            "order": [0, "asc"],
            "processing": "true",
            "language": {




                processing: '<img width="100%" src="/images/Loader_img1 (7).gif">',

                emptyTable: '<div id="noDataFound" style="text-align: center;display: flex;align-items: center;justify-content: center;height: 45vh;"><img src="/images/nodata.png" /></div >',

                zeroRecords: '<div id="noDataFound" style="text-align: center;display: flex;align-items: center;justify-content: center;height: 45vh;"><img src="/images/nodata.png" /></div >',

                sLengthMenu: "_MENU_",
                paginate: {


                    next: '<img width="100%" src="/images/icons/Icon awesome-arrow-right-1.png" >',
                    previous: '<img src="/images/icons/Icon awesome-arrow-right.png">',
                    first: '<img src="/images/icons/Icon metro-next-1.png">',
                    last: '<img src="/images/icons/Icon metro-next.png">'

                }
            }

        });

        $('.ihits-table table').removeClass("dataTable no-footer");


        $('.paginate_button.next').attr("data-bs-toggle", "tooltip");
        $('.paginate_button.next').attr("title", "Next");
        $('.paginate_button.previous').attr("data-bs-toggle", "tooltip");
        $('.paginate_button.previous').attr("title", "Previous");
        $('.paginate_button.first').attr("data-bs-toggle", "tooltip");
        $('.paginate_button.first').attr("title", "First");
        $('.paginate_button.last').attr("data-bs-toggle", "tooltip");
        $('.paginate_button.last').attr("title", "Last");


        $("#dataTableSearch").on("keyup search input paste cut", function () {
            debugger
            dataTables.search(this.value).draw();

            $('#noDataFound').css("display", 'none');

        });

        $('.search-slider-inner').click(function () {
            //dataTable.search('').draw();
            $('#dataTableSearch').val('');
            $('#dataTableSearch').focus();
        });

        dataTables.on('draw', function () {
            $('.paginate_button.next').attr("data-bs-toggle", "tooltip");
            $('.paginate_button.next').attr("title", "Next");
            $('.paginate_button.previous').attr("data-bs-toggle", "tooltip");
            $('.paginate_button.previous').attr("title", "Previous");
            $('.paginate_button.first').attr("data-bs-toggle", "tooltip");
            $('.paginate_button.first').attr("title", "First");
            $('.paginate_button.last').attr("data-bs-toggle", "tooltip");
            $('.paginate_button.last').attr("title", "Last");

        });
    }

    else {

    url = "/DataBank/CandidatePreEnrolLink/Index";
    window.location.href = url;


    }
}

function onExportToExcelClick() {
    debugger;
    if (currentTab == 'CANDIDATELINK') {
        var radiomode = $("input[type='radio']:checked").val();
        var pendingTableLength = $('#PersonalDetailsList_Count').val();
        if (pendingTableLength > 0) {
            url = "/DataBank/Offer/ExportToExcel?TabIndex=" + currentTab + " & radiomode=" + radiomode;
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
    }
    else {
        var pendingTableLength = $('#PreEnrollCandidateLinkApprovalList_Count').val();
        if (pendingTableLength > 0) {
            url = "/DataBank/CandidatePreEnrolLink/ExportToExcel";
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
    }
}



function SendCandidateLink() {
    debugger;

    swal.fire({
        text: "Do you want to Send Mail?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            var table = $('#offercandidatelink').DataTable();
            var data = table.$('input,select,textarea').serializeArray()
            $.ajax({

                type: 'POST',
                url: '/DataBank/CandidatePreEnrolLink/SendCandidateMail',
                dataType: 'json',
                data: data,//PreEnrollCode: PreEnrollCode, PreEnrollGI: PreEnrollGI, EmpCode: EmpCode
                success: function (result) {
                    debugger;
                    if (result.success == true) {
                        debugger
                        swal.fire(result.message).then(function () {
                            debugger;
                            //window.location.reload();
                            //var url = "/DataBank/Offer/GetList?TabIndex=" + 'CANDIDATELINK';
                            OnTabClick('PreEnrollLink', 'CANDIDATELINK')
                            //window.location.href = url;
                            //$('#offerverification').load(url, function () { });
                            // window.top.close();



                        });

                    }

                    else {
                        debugger;
                        swal.fire(result.message).then(function () {
                            //var url = "/DataBank/Offer/GetList?TabIndex=" + 'APPROVED';

                            //$('#offerverification').load(url, function () { });
                            //var url = "/Recruitment/OfferProcessing/GertOfferLetter?PreEnrollCode=" + PreEnrollCode + "&PreEnrollGI=" + PreEnrollGI;
                            //window.open(url, "_blank");
                            // window.location.reload();


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
function CandidatelinkRadio() {
    debugger;
    var radiomode = $("input[name='radiomode']:checked").val();

    $.ajax({
       
        type: 'GET',
        url: '/DataBank/CandidatePreEnrolLink/GetList',
        dataType: 'html',
        data: { Tab: "CANDIDATELINK", radiomode: radiomode },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#PreEnrollCandidateLinkApprovalList').html("");
                $('#PreEnrollCandidateLinkApprovalList').html(result);
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



function ApplyFilter(mode) {
    debugger
    var radiomode = $("input[name='radiomode']:checked").val();
    var tab = "CANDIDATELINK";
    if (mode == 'apply') {
        if (filterApplicable >= 1) {

            $.ajax({

                type: 'POST',
                url: '/DataBank/CandidatePreEnrolLink/GetList',
                dataType: 'html',
                data: $('#BasicFilter').serialize() + "&Tab=" + tab + "&radiomode=" + radiomode ,
                success: function (result) {
                    debugger
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();

                    }
                    else {
                        $('#PreEnrollCandidateLinkApprovalList').html("");
                        $('#PreEnrollCandidateLinkApprovalList').html(result);
                    }
                    hideSpinner();
                    CloseSlider('basicFilter');

                },


                error: function (xhr, textStatus, errorThrown) {
                    document.body.id = "kt_body";
                    $('body').html(xhr.responseText);
                }
            });

            $('#filterForm').removeClass('filter-apply');
        }
        else {
            showSpinner();
            $.ajax({

                type: 'POST',
                url: '/DataBank/CandidatePreEnrolLink/GetList',
                dataType: 'html',
                data: $('#BasicFilter').serialize() + "&Tab=" + tab + "&radiomode=" + radiomode,
                success: function (result) {
                    debugger
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }
                    else {
                        $('#PreEnrollCandidateLinkApprovalList').html("");
                        $('#PreEnrollCandidateLinkApprovalList').html(result);
                    }
                    hideSpinner();
                    CloseSlider('basicFilter');
                },


                error: function (xhr, textStatus, errorThrown) {
                    document.body.id = "kt_body";
                    $('body').html(xhr.responseText);
                }
            });

            $('#filterForm').addClass('filter-apply');
        }

    }
    else {

        showSpinner();
        filterApplicable = 1;
        $('.reset').val('');
        $('#Optional_Filter_EmploymentStatus_Code').val(0);
        $('#filterForm').removeClass('filter-apply');
        $.ajax({

            type: 'POST',
            url: '/DataBank/CandidatePreEnrolLink/GetList',
            dataType: 'html',
            data: $('#BasicFilter').serialize() + "&Tab=" + tab + "&radiomode=" + radiomode,
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();

                }
                else {
                    $('#PreEnrollCandidateLinkApprovalList').html("");
                    $('#PreEnrollCandidateLinkApprovalList').html(result);
                }
                hideSpinner();

            },


            error: function (xhr, textStatus, errorThrown) {
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);
            }
        });

    }
}



//function ApplyFilter(mode) {
//    debugger
//    if (mode == 'apply') {
//        if (filterApplicable >= 1) {
//            $.ajax({

//                type: 'POST',
//                url: '/DataBank/CandidatePreEnrolLink/ApplyFilter',
//                dataType: 'html',
//                data: $('#BasicFilter').serialize(),
//                success: function (result) {
//                    debugger
//                    CloseSlider('basicFilter');
//                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
//                        window.location.reload();

//                    }
//                    else {
//                        $('#PreEnrollCandidateLinkApprovalList').html("");
//                        $('#PreEnrollCandidateLinkApprovalList').html(result);
//                        $('#PreEnrollCandidateLinkApprovalList').removeClass('hide');
//                    }

//                },


//                error: function (xhr, textStatus, errorThrown) {
//                    document.body.id = "kt_body";
//                    $('body').html(xhr.responseText);
//                }
//            });
//            $('#filterForm').removeClass('filter-apply');
//        }
//        else {
//            $.ajax({

//                type: 'POST',
//                url: '/DataBank/CandidatePreEnrolLink/ApplyFilter',
//                dataType: 'html',
//                data: $('#BasicFilter').serialize(),
//                success: function (result) {
//                    debugger
//                    CloseSlider('basicFilter');
//                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
//                        window.location.reload();

//                    }
//                    else {
//                        $('#PreEnrollCandidateLinkApprovalList').html("");
//                        $('#PreEnrollCandidateLinkApprovalList').html(result);
//                        $('#PreEnrollCandidateLinkApprovalList').removeClass('hide');
//                    }

//                },


//                error: function (xhr, textStatus, errorThrown) {
//                    document.body.id = "kt_body";
//                    $('body').html(xhr.responseText);
//                }
//            });
//            $('#filterForm').addClass('filter-apply');
//        }

//    }
//    else {
//        filterApplicable = 1;
//        $('.reset').val('');
//        $('#filterForm').removeClass('filter-apply');
//        $.ajax({

//            type: 'POST',
//            url: '/DataBank/CandidatePreEnrolLink/ApplyFilter',
//            dataType: 'html',
//            data: $('#BasicFilter').serialize(),
//            success: function (result) {
//                debugger
//                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
//                    window.location.reload();

//                }
//                else {
//                    $('#PreEnrollCandidateLinkApprovalList').html("");
//                    $('#PreEnrollCandidateLinkApprovalList').html(result);
//                    $('#PreEnrollCandidateLinkApprovalList').removeClass('hide');
//                }


//            },


//            error: function (xhr, textStatus, errorThrown) {
//                document.body.id = "kt_body";
//                $('body').html(xhr.responseText);
//            }
//        });
//    }
//}
function onEditClick(i) {
    debugger

    $('#newEmail_' + i).val($('#EmailLabel_' + i).text());
    $('#EmailLabel_' + i).addClass('hide');
    $('#edit_' + i).addClass('hide');
    $('#EmailTxtbox_' + i).removeClass('hide');
    $('#updatecancel_' + i).removeClass('hide');
}
function onUpdateClick(i, PreEnrollGI) {
    debugger
    $('#PersonalDetailsList' + i + '__Email').val($('#newEmail_' + i).val());
    $('#EmailLabel_' + i).html($('#newEmail_' + i).val());
    $('#EmailLabel_' + i).removeClass('hide');
    $('#edit_' + i).removeClass('hide');
    $('#EmailTxtbox_' + i).addClass('hide');
    $('#updatecancel_' + i).addClass('hide');

    var Email = $('#newEmail_' + i).val();

    if (Email == "" || Email == null) {
        swal.fire("Email should not be null");
        fillData();
        return false;
    }

    debugger
    $.ajax({
        type: 'POST',
        url: '/DataBank/CandidatePreEnrolLink/EditEmail',
        dataType: 'json',
        data: "UpdatedEmail=" + Email + "&PreEnrollGI=" + PreEnrollGI,
        success: function (result) {
            debugger;
            if (result.success == true) {
                swal.fire("Updated Successfully").then(function () {
                    fillData();

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


function onCancelClick(i) {
    $('#EmailLabel_' + i).removeClass('hide');
    $('#edit_' + i).removeClass('hide');
    $('#EmailTxtbox_' + i).addClass('hide');
    $('#updatecancel_' + i).addClass('hide');
    $('#newEmail_' + i).val(0);
}