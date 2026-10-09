function onExperiencedOrFresherChange(data) {
    debugger;
    if ($(data).val() == 0) {
        $('.CurrentEmployerDiv').removeClass('hide');
        $('.ExperienceDiv').removeClass('hide');        
    }
    else {
        $('.CurrentEmployerDiv').addClass('hide');
        $('.ExperienceDiv').addClass('hide');
    }
}

function onmappedResumeClick(VacancyGI) {
    debugger;
    $.ajax({
        type: 'GET',
        url: "/DataBank/WalkInInterview/GetMappedResumePopUp?VacancyGI=" + VacancyGI,
        dataType: 'html',

        success: function (Data) {
            if ($($.parseHTML(Data)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {
                var m = $('.resumemodal');
                m.find('.modal-bodyyy').html('');
                m.find('.modal-bodyyy').html(Data);
                /* $("#myDiv").css("display", "block");*/
            }

        },
        error: function (xhr, textStatus, errorThrown) {
            swal.fire(xhr.responseText);

        }
    });
}

function viewResume(GI) {
    debugger;
    var url = "/DataBank/MRFVsResume/ResumeView";
    /* window.location.href = url + '?ResumeGI=' + GI;*/
    window.open(url + '?ResumeGI=' + GI, '_blank');
}

function WalkInInterviewSave() {
    var i = 0;
    if ($("#CandidateDetails_CandidatePhoto_FileData").val() == "" && $('#CandidateDetails_CandidatePhoto_AbsoluteUri').val() === "") {
        swal.fire('Candidate photo is required');
        i++;
    }
    if (!validateById("#CandidateDetails_Salutation_Code", 'salutation')) {
        i++;
    }
    if (!validateById("#CandidateDetails_FullName", 'fullname')) {
        i++;
    }
    if (!validateById("#CandidateDetails_Mobile", 'mob')) {
        i++;
    }
    if (!validateById("#CandidateDetails_Email", 'email')) {
        i++;
    }
    if (!validateById("#CandidateDetails_DOB", 'dob')) {
        i++;
    }
    if (!validateById("#CandidateDetails_Gender_Code", 'gender')) {
        i++;
    }
    if (!validateAadhar("CandidateDetails_Adhar", 'aadhar')) {
        i++;
    }
    //if (!validatePan("CandidateDetails_PAN", 'pan')) {
    //    i++;
    //}
    if (!validateById("#CandidateDetails_DrivingLicenseNo", 'drivingLicense')) {
        i++;
    }
    if (!validateById("#CandidateDetails_Qualification_Code", 'qualification')) {
        i++;
    }
    if ($("input[type='radio']:checked").val() != 1) {
        if (!validateById("#CandidateDetails_CurrentEmployer", 'currEmployer')) {
            i++;
        }
        if (!validateById("#CandidateDetails_Experience", 'exp')) {
            i++;
        }
    }
    if (!validateById("#CandidateDetails_CurrentEmployementComments", 'currEmploymentComments')) {
        i++;
    }
    if (!validateById("#CandidateDetails_InterviewDate", 'interviewDte')) {
        i++;
    }
    if (!validateById("#CandidateDetails_Interviewer_EmployeeCode", 'EmpCode')) {
        i++;
    }
    if (!validateById("#CandidateDetails_InterViewFeedBack", 'feedback')) {
        i++;
    }
    if (!validateById("#CandidateDetails_InterviewSelection_Code", 'interviewSel')) {
        i++;
    }

    if (i == 0) {
        swal.fire({
            text: "Do you want to Save?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                var data = new FormData($('#walkInInterviewSave')[0]);
                $.ajax({

                    type: 'POST',
                    url: '/DataBank/WalkInInterview/Save',
                    contentType: false,
                    processData: false,
                    dataType: 'json',
                    data: data,
                    success: function (result) {
                        console.log(result);
                        if (result.success == true) {
                            swal.fire("Saved Successfully").then(function () {
                                var url = "/DataBank/WalkInInterview/Index";
                                window.location.href = url;
                            });
                        }
                        else {
                            swal.fire(result.message);
                        }
                    },


                    error: function (xhr, textStatus, errorThrown) {
                        $('body').html(xhr.responseText);
                    }
                });
            }
        });

}
}

function Preview(id) {
    debugger
    var path = URL.createObjectURL($('.'+id)[0].files[0]);
    window.open(path, "_blank");
}


function ExportToExcel() {
    debugger;
    var walkininterviewTable = $('#VacancyList_Count').val();
    /* var CancellationTable = document.getElementById("CancellationListTable").rows.length - 1;*/

    if (walkininterviewTable > 0) {
        url = "/DataBank/WalkInInterview/ExportToExcel";
        window.location.href = url;

    }
    else {
        swal.fire("No data found to export");
    }
}

function CheckProfilePic(e) {
    debugger;

    debugger;
    var size = $(e)[0].files[0].size;
    var filename = $(e)[0].files[0].name;
    var fileExtension = ['jpeg', 'jpg'];
    //if (size > 2048 * 1024) {
    //    swal.fire('File size cannot exceeds 2 Mb').then(function () {
    //        $(e).val('');
    //        return false;
    //    });
    //}
    if ($.inArray($(e).val().split('.').pop().toLowerCase(), fileExtension) == -1) {
        swal.fire("Incorrect format. Only jpeg and jpg images are allowed").then(function () {
            $(e).val('');
            return false;
        });
    }
    else {
        return true;
    }

}
function validateProfilePhoto() {
    debugger
    if ($("#CandidateDetails_CandidatePhoto_FileData").val() == null)
        swal.fire("No file Uploaded");

    else {
        if (CheckProfilePic(document.getElementById('CandidateDetails_CandidatePhoto_FileData'))) {
            var photo = new FormData($('#walkInInterviewSave')[0]);
            //console.log(photo);
            $.ajax({

                type: 'POST',
                url: '/DataBank/WalkInInterview/SaveProfileToFolder',
                dataType: 'html',
                data: photo,
                contentType: false,
                processData: false,
                success: function (Data) {
                    var m = $('.bd-example-modal-lg');
                    m.find('.modal-body').html('');
                    m.find('.modal-body').html(Data);
                    m.modal('show');
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


}