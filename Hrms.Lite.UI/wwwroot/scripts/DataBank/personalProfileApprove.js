
function Profile_PicApprove(mode)
{    
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
                    url: '/DataBank/PersonalDetailsApprove/ApprovePersonalProfile',
                    dataType: 'json',
                    data: $('#per_Photoform').serialize() + "&Apmode=" + mode,
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                debugger                               
                                window.location.href =  '/DataBank/PersonalDetailsApprove/Create?EmployeeGI=' + $("#EmployeeGI").val();
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

function OnEmpNameClick(EmployeeGI, index) {
    debugger;
    if ($("#PersonalDetailsApprovalList_" + index + "__MakerChecker").val() == 'True')
        swal.fire(MakerCheckerMsg);

    else {

        var url = "/DataBank/PersonalDetailsApprove/Create?EmployeeGI=" + EmployeeGI;
        window.location.href = url;
    }
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
        url: '/DataBank/PersonalDetailsApprove/ApprovePersonalProfile',
        dataType: 'json',
        data: $('#AddressSave').serialize() + "&Apmode=" + mode,
        success: function (result) {
            if (result.success == true) {
                swal.fire(result.message).then(function () {
                    debugger
                    window.location.href = '/DataBank/PersonalDetailsApprove/Create?EmployeeGI=' + $("#EmployeeGI").val();
        
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
                url: '/DataBank/PersonalDetailsApprove/ApprovePersonalProfile',
                dataType: 'json',
                data: $('#ContactDetailsSave').serialize() + "&Apmode=" + mode,


                success: function (result) {
                    if (result.success == true) {                       
                       window.location.href =  '/DataBank/PersonalDetailsApprove/Create?EmployeeGI=' + $("#EmployeeGI").val();
                       
                       
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
                url: '/DataBank/PersonalDetailsApprove/ApprovePersonalProfile',
                dataType: 'json',
                data: $('#PersonalInfoSave').serialize() + "&Apmode=" + mode,
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                           
                            window.location.href =  '/DataBank/PersonalDetailsApprove/Create?EmployeeGI=' + $("#EmployeeGI").val();
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
                url: '/DataBank/PersonalDetailsApprove/ApprovePersonalProfileLists',
                dataType: 'json',
                cache: false,
                data: $('#SkillsSave').serialize() + "&Apmode=" + mode,
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                         
                            window.location.href =  '/DataBank/PersonalDetailsApprove/Create?EmployeeGI=' + $("#EmployeeGI").val();
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
function PersonalProfileEmpVsNomApprove(Index, mode) {
    debugger
    $('#EmployeeVsNomineeList_' + Index + '__flag').val(1);
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
                url: '/DataBank/PersonalDetailsApprove/ApprovePersonalProfileLists',
                dataType: 'json',
                cache: false,
                data: $('#EmployeeVsNomineeSave').serialize() + "&Apmode=" + mode,
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                         
                            window.location.href =  '/DataBank/PersonalDetailsApprove/Create?EmployeeGI=' + $("#EmployeeGI").val();
                        });
                       
                    }
                    else {
                        $('#EmployeeVsNomineeList_' + Index + '__flag').val(0);
                        swal.fire(result.message);
                    }
                },
                error: function (xhr, textStatus, errorThrown) {
                    $('#EmployeeVsNomineeList_' + Index + '__flag').val(0);
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
                url: '/DataBank/PersonalDetailsApprove/ApprovePersonalProfileLists',
                dataType: 'json',
                data: $('#certificateApprove').serialize() + "&Apmode=" + mode,
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {                           
                            window.location.href =  '/DataBank/PersonalDetailsApprove/Create?EmployeeGI=' + $("#EmployeeGI").val();
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
                url: '/DataBank/PersonalDetailsApprove/ApprovePersonalProfileLists',
                dataType: 'json',
                data: $('#FamilySave').serialize() + "&Apmode=" + mode,
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            
                            window.location.href =  '/DataBank/PersonalDetailsApprove/Create?EmployeeGI=' + $("#EmployeeGI").val();
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

//listniv
function ListWisePersonalProfileFamilyApprove( GI,mode,Index,TabIndex) {
    debugger;
    var EmpGI = $('#EmployeeGI').val();
    $.ajax({

        type: 'POST',
        url: '/DataBank/PersonalDetailsApprove/ListWiseApproveRejectPersonalProfile',
        dataType: 'json',
        data: { EmpGI: EmpGI, GI: GI, Apmode: mode, TabIndex: TabIndex },
        success: function (result) {
            if (result.success == true) {
                swal.fire(result.message).then(function () {
            
                    window.location.href =  '/DataBank/PersonalDetailsApprove/Create?EmployeeGI=' + $("#EmployeeGI").val();
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
                url: '/DataBank/PersonalDetailsApprove/ApprovePersonalProfileLists',
                dataType: 'json',
                data: $('#EducationSave').serialize() + "&Apmode=" + mode,
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {                           
                            window.location.href =  '/DataBank/PersonalDetailsApprove/Create?EmployeeGI=' + $("#EmployeeGI").val();
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

function PersonalProfilePreviousEmploymentApprove(Index, mode) {
    debugger
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
                url: '/DataBank/PersonalDetailsApprove/ApprovePersonalProfileLists',
                dataType: 'json',
                data: $('#PrevEmpSave').serialize() + "&Index=" + Index + "&Apmode=" + mode,
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {                           
                            window.location.href =  '/DataBank/PersonalDetailsApprove/Create?EmployeeGI=' + $("#EmployeeGI").val();
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

function ApplyFilter(mode) {
    debugger
    if (mode == 'apply') {
        if (filterApplicable >= 1) {
            $.ajax({

                type: 'POST',
                url: '/DataBank/PersonalDetailsApprove/ApplyFilter',
                dataType: 'html',
                data: $('#BasicFilter').serialize(),
                success: function (result) {
                    debugger
                    CloseSlider('basicFilter');
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();

                    }
                    else {
                        $('#PersonalDetailsApproveList').html("");
                        $('#PersonalDetailsApproveList').html(result);
                        $('#PersonalDetailsApproveList').removeClass('hide');
                    }
                               
                },


                error: function (xhr, textStatus, errorThrown) {
                    document.body.id = "kt_body";
                    $('body').html(xhr.responseText);
                }
            });
            $('#filterForm').removeClass('filter-apply');
        }
        else {
            $.ajax({

                type: 'POST',
                url: '/DataBank/PersonalDetailsApprove/ApplyFilter',
                dataType: 'html',
                data: $('#BasicFilter').serialize(),
                success: function (result) {
                    debugger
                    CloseSlider('basicFilter');
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();

                    }
                    else {
                        $('#PersonalDetailsApproveList').html("");
                        $('#PersonalDetailsApproveList').html(result);
                        $('#PersonalDetailsApproveList').removeClass('hide');
                    }
            
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
        filterApplicable = 1;
        $('.reset').val('');
        $('#filterForm').removeClass('filter-apply');
        $.ajax({

            type: 'POST',
            url: '/DataBank/PersonalDetailsApprove/ApplyFilter',
            dataType: 'html',
            data: $('#BasicFilter').serialize(),
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();

                }
                else {
                    $('#PersonalDetailsApproveList').html("");
                    $('#PersonalDetailsApproveList').html(result);
                    $('#PersonalDetailsApproveList').removeClass('hide');
                }
               

            },


            error: function (xhr, textStatus, errorThrown) {
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);
            }
        });
    }
}

function onExportToExcelClick() {
    debugger;
    /*var searchid = $('#Item2_PersonalDetailsFilter_Code').val();*/
    /*var searchid = $('#AssetAllocationFilter_Code').val();*/

    var pendingTableLength = $('#PersonalDetailsApprovalList_Count').val();
    if (pendingTableLength > 0) {
        url = "/DataBank/PersonalDetailsApprove/ExportToExcel";
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }


}
function PersonalProfileEmployeeVsNomineeApprove(mode) {
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
                url: '/DataBank/PersonalDetailsApprove/ApprovePersonalProfile',
                dataType: 'json',
                data: $('#EmployeeVsNomineeSave').serialize() + "&Apmode=" + mode,


                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {                            
                            window.location.href =  '/DataBank/PersonalDetailsApprove/Create?EmployeeGI=' + $("#EmployeeGI").val();
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

function DocumentView(EmployeeGI, DocType) {
   
    var url = "/DataBank/PersonalDetailsApprove/DocumentView?EmployeeGI=" + EmployeeGI + "&DocType=" + DocType;
    window.open(url, "_blank");
}