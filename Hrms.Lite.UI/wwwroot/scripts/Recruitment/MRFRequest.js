function getMRFCreate() {
    debugger;
    //var url = "/DataBank/VacancyManagement/Create";
    //$('#CreateMRFSlider').load(url, function () { });
    $.ajax({

        type: 'GET',
        url: '/Recruitment/MRFRequest/Create',
        dataType: 'html',
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {
                $('body').append('<div class="form-overlay"></div>');
                $('#AddNewMRFSlider').html("");
                $('#AddNewMRFSlider').html(result);
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
function getApproveDetails(GI) {
    debugger;
    var url = "/Recruitment/MRFRequest/GetApproveDetails";
    $('body').append('<div class="form-overlay"></div>');
    $('#MRFViewSlider').load(url, { VacancyManagementGI: GI }, function () { });
}
function VacancySave() {
    var i = 0;
    if (!validateById('#Details_JobTitle', 'jobtitle')) {
        i++;
    }
    if (!validateById('#Details_NeedByDate', 'needbydate')) {
        i++;
    }
    if (!validateById('#Details_EmployeeCode', 'employee')) {
        i++;
    }
    if (!validateById('#Details_Designation_Code', 'designation')) {
        i++;
    }
    if (!validateById('#Details_Department_Code', 'department')) {
        i++;
    }
    if (!validateById('#Details_Location_Code', 'location')) {
        i++;
    }
    if (!validateById('#Details_Gender_Code', 'gender')) {
        i++;
    }
    //if (!validateById('#Details_YOS', 'experience')) {
    //    i++;
    //}
    //if (!validateById('#AgeGroup', 'age')) {
    //    i++;
    //}
    if (!validateById('#Details_NoOfRequirements', 'nofreq')) {
        i++;
    }
    //if (!validateById('#Details_ExGrossPay', 'exgrosspay')) {
    //    i++;
    //}
    if (!validateById('#Details_Description', 'remarks')) {
        i++;
    }
    if (i > 0) {
        return false;
    }
    else {
        debugger

        //var InitiatedEmpCode = ('#Details_EmployeeCode').val();
        swal.fire({
            text: "Do you want to Save?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({

                    type: 'POST',
                    url: '/Recruitment/MRFRequest/Create',
                    dataType: 'json',
                    data: $('#VacancySave').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire("Saved Successfully").then(function () {
                                CloseSlider('MRFCreate');
                                var url = "/Recruitment/MRFRequest/GetList";
                                $('#MRFRequestList').load(url, { TabIndex: 1 }, function () { });
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
}
function loadDesgnVsJDSlider(result) {
    debugger

    $('#MRFRequestSliderSub').html("");
    $('#MRFRequestSliderSub').html(result);
    $("#Details_GenderArray").multiselect('destroy');
    $("#Details_GenderArray").multiselect();
}
function getVSJD(sender, id) {
    //$('#MRFRequestSliderSub').load(url, function () {
    //    $("#Details_GenderArray").multiselect('destroy');
    //    $("#Details_GenderArray").multiselect();
    //});
    $.ajax({

        type: 'GET',
        url: '/Recruitment/MRFRequest/GetVSJD',
        dataType: 'html',
        data: { DesignationCode: $(sender).val()},
        success: function (response) {
            debugger
        
            if ($($.parseHTML(response)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                debugger              
       
                if (jQuery(response).find("#JDCheck").val() == "True") {
                    swal.fire("Job Description not defined for Selected Vacancy").then(function () {
                        loadDesgnVsJDSlider(response);
                    });
                }
                 else
                    loadDesgnVsJDSlider(response);

                //    swal.fire({
                //        text: "Job Description not defined for Selected Vacancy. Do you want to continue ?",
                //        icon: 'warning',
                //        showCancelButton: true,
                //        confirmButtonColor: '#3085d6',
                //        cancelButtonColor: '#d33',
                //        confirmButtonText: 'Ok'
                //    }).then((result) => {
                //        if (result.isConfirmed) {
                //            loadDesgnVsJDSlider(response);
                //        }
                //    });
                //}
                //else
                //    loadDesgnVsJDSlider(response);
            }
            },     
        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);

        }
    });
}

function onmappedResumeClick(VacancyGI) {
    debugger;
    $.ajax({
        type: 'GET',
        url: "/DataBank/MRFVsResume/GetMappedResumePopUp?VacancyGI=" + VacancyGI,
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

function CancelMRF(VacancyManagementGI) {

    swal.fire({
        text: "Do you want to Cancel?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({

                type: 'POST',
                url: '/Recruitment/MRFRequest/CancelMRF',
                dataType: 'json',
                data: { VacancyManagementGI: VacancyManagementGI},
                success: function (result) {
                    if (result.success == true) {
                        swal.fire("Cancelled Successfully").then(function () {                            
                            var url = "/Recruitment/MRFRequest/GetList";
                            $('#MRFRequestList').load(url, { TabIndex: 1 }, function () { });
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
    debugger;
    var TableLength = $('#VacancyManagement_List_Count').val();
    if (TableLength > 0) {
        url = "/Recruitment/MRFRequest/ExportToExcel?TabIndex=" + 1;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}