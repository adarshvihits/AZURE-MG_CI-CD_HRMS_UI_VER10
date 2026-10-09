var prevTab = 'tab1';
let currentTab = 1;


function Tab(Tab, TabIndex) {
    debugger;
    /* showSpinner();*/
    currentTab = TabIndex;
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");
    //var url = "/DataBank/JobPosting/GetList";

    //$('#JobPostingList').load(url, { TabIndex: TabIndex }, function () { });
    $.ajax({

        type: 'GET',
        url: '/DataBank/JobPosting/GetList',
        dataType: 'html',
        data: { TabIndex: TabIndex },
        success: function (result) {
            debugger;
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {
                $('#JobPostingList').html("");
                $('#JobPostingList').html(result);
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
function onAllApplicabilityClick(colchkid, tableid) {
    if ($('#' + colchkid).is(':checked'))
        $('#' + tableid).find('input[type=checkbox]').prop('checked', true);
    else
        $('#' + tableid).find('input[type=checkbox]').prop('checked', false);
}
function loadslider(GI, mode) {
    $.ajax({

        type: 'GET',
        url: '/DataBank/JobPosting/Create',
        dataType: 'html',
        data: { GI: GI, mode: mode },
        success: function (result) {
            debugger;
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {
                $('body').append('<div class="form-overlay"></div>');
                $('#JobPostingSlider').html("");
                $('#JobPostingSlider').html(result);
            }
        },


        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);


        }
    });
}

function getJobPostingDetails(GI, mode, isJdEmpty) {
    debugger;
    if (isJdEmpty == "True") {
        swal.fire({
            text: "Job Description not defined for Selected Vacancy. Do you want to continue ?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                loadslider(GI, mode);
            }
        });
    }
    else
        loadslider(GI, mode);
   
}
        
            
function CloseJobSlider(id) {
    debugger
    $("." + id).removeClass('slider-forms-open');
    $(".form-overlay").remove();
    $(".jpclose").remove();
}

function JobPostingSave() {
    //var i = 0;
    //if (!validateById('#Details_JobTitle', 'jobtitle')) {
    //    i++;
    //}
    //if (!validateById('#Details_NeedByDate', 'needbydate')) {
    //    i++;
    //}
    //if (!validateById('#Details_EmployeeCode', 'employee')) {
    //    i++;
    //}
    //if (!validateById('#Details_Designation_Code', 'designation')) {
    //    i++;
    //}
    //if (!validateById('#Details_Department_Code', 'department')) {
    //    i++;
    //}
    //if (!validateById('#Details_Location_Code', 'location')) {
    //    i++;
    //}
    //if (!validateById('#Details_Gender_Code', 'gender')) {
    //    i++;
    //}
    //if (!validateById('#Experience', 'experience')) {
    //    i++;
    //}
    //if (!validateById('#Details_NoOfRequirements', 'nofreq')) {
    //    i++;
    //}
    //if (!validateById('#Details_ExGrossPay', 'exgrosspay')) {
    //    i++;
    //}
    //if (!validateById('#Details_Description', 'remarks')) {
    //    i++;
    //}
    //if (i > 0) {
    //    return false;
    //}
    //else {
    var i = 0;
    for (let index = 0; index < $('#PostedIn_Count').val(); index++)
    {
        if ($("input[type=checkbox]:checked", "#Posting_Source_Table").length != 0) {
            if ($('#PostedIn_' + index + '__Applicability').is(':checked')) {
                if (!validateById('#PostedIn_' + index + '__From', 'From_' + index)) {
                    i++;
                }
                if (!validateById('#PostedIn_' + index + '__To', 'To_' + index)) {
                    i++;
                }
            }
        }
        else {
            swal.fire("Please select atleast one");
            i++;
        }
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
                $.ajax({

                    type: 'POST',
                    url: '/DataBank/JobPosting/JPCreate',
                    dataType: 'json',
                    data: $('#JPSave').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                CloseJobSlider('jpclose');
                                var url = "/DataBank/JobPosting/GetList";

                                $('#JobPostingList').load(url, { TabIndex: currentTab }, function () { });

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
    //        else {
    //            return false;
    //        }
        })
    }
}

function Delete(VacancyManagementGI) {
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
                url: '/DataBank/JobPosting/JPDelete',
                dataType: 'json',
                data: { VacancyManagementGI: VacancyManagementGI },
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            Tab('tab2', 2);

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
        //        else {
        //            return false;
        //        }
    })
   
}

function ExportToExcel() {
    debugger;
    if (currentTab == 1) {
        var TableLength = $('#JobPostingList_Count').val();
        if (TableLength > 0) {
            url = "/DataBank/JobPosting/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
    }
    else {
        var TableLength = $('#JobPostingList_Count').val();
        if (TableLength > 0) {
            url = "/DataBank/JobPosting/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
    }
}