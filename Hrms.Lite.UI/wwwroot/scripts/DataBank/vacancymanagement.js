var prevTab = 'tab1';
let currentTab = 1;


function Tab(Tab,TabIndex) {
    debugger;
    $('.spinner-outer').show();
    currentTab = TabIndex;
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");
    $('#tabsDropdownDivision').val(currentTab);
    //var url = "/DataBank/VacancyManagement/GetList";

    //$('#VacancyList').load(url, { TabIndex: TabIndex }, function () { });
    $.ajax({

        type: 'GET',
        url: '/DataBank/VacancyManagement/GetList',
        dataType: 'html',
        data: { TabIndex: TabIndex },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {
                $('#VacancyList').html("");
                $('#VacancyList').html(result);
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
function getMRFCreate() {
    debugger;
    //var url = "/DataBank/VacancyManagement/Create";
    //$('#CreateMRFSlider').load(url, function () { });
    $.ajax({

        type: 'GET',
        url: '/DataBank/VacancyManagement/Create',
        dataType: 'html',
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {
                /*$('body').append('<div class="form-overlay"></div>');*/
                $('#CreateMRFSlider').html("");
                $('#CreateMRFSlider').html(result);
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

function getApproveDetails(GI,Sliderno) {
    debugger;
    var url = "/DataBank/VacancyManagement/GetApproveDetails";
    $('body').append('<div class="form-overlay"></div>');
    $('#VacancySlider').load(url, { VacancyManagementGI: GI, SlNo: Sliderno }, function () {
        if (currentTab == 1) {
            $('#sliderHeading').html('Active');
            $('.closeOrCancelDiv').addClass('hide');
        }
        else if (currentTab == 5) {
            $('#sliderHeading').html('Cancelled');
            $('.closeOrCancel').html('MRF Cancelled Date');
            $('.closeOrCancelDiv').removeClass('hide');
        }
        else if (currentTab == 6) {
            $('#sliderHeading').html('Closed');
            $('.closeOrCancel').html('MRF Closed Date');
            $('.closeOrCancelDiv').removeClass('hide');
        }
    });
}
function getActivevacancyDetails() {
    debugger;
    var url = "/DataBank/VacancyManagement/GetActivevacancyDetails";
    $('#VacancySlider').load(url, function () { });
}
function getFilledVacancyDetails() {
    debugger;
    var url = "/DataBank/VacancyManagement/GetFilledDetails";
    $('#VacancySlider').load(url, function () { });
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
    if (!validateById('#Experience', 'experience')) {
        i++;
    }
    if (!validateById('#Details_NoOfRequirements', 'nofreq')) {
        i++;
    }
    if (!validateById('#Details_ExGrossPay', 'exgrosspay')) {
        i++;
    }
    if (!validateById('#Details_Description', 'remarks')) {
        i++;
    }
    if (i > 0) {
        return false;
    }
    else {
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
                    url: '/DataBank/VacancyManagement/Create',
                    dataType: 'json',
                    data: $('#VacancySave').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire("Saved Successfully").then(function () {
                                CloseSlider('MRFCreate');
                                var url = "/DataBank/VacancyManagement/GetList";

                                $('#VacancyList').load(url, { TabIndex: currentTab }, function () { });

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

function VacancyApproveReject(ARStatus) {
    var i = 0;
    if (!validateById('#ApproverComments', 'comments')) {
        i++;
    }
    if (i > 0) {
        return false;
    }
    else {
        $('#Approvestatus').val(ARStatus);
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
                    url: '/DataBank/VacancyManagement/ApproveReject',
                    dataType: 'json',
                    data: $('#VacancyApproveReject').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire("Approved Successfully").then(function () {
                                CloseSlider('MRFAccept');
                                var url = "/DataBank/VacancyManagement/GetList";

                                $('#VacancyList').load(url, { TabIndex: currentTab }, function () { });

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

function FilledVacancySave(ARStatus) {
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
        url: '/DataBank/VacancyManagement/FilledVacancySave',
        dataType: 'json',
        data: $('#FilledVacanySave').serialize(),
        success: function (result) {
            if (result.success == true) {
                swal.fire("Saved Successfully").then(function () {
                    CloseSlider('activevacancy');
                    //var url = "/DataBank/VacancyManagement/Index";
                    //window.location.href = url;
                    var url = "/DataBank/VacancyManagement/Getlist";
                    $('#VacancyList').load(url, { TabIndex: currentTab}, function () { });
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

function VacancyDelete() {
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
                url: '/DataBank/VacancyManagement/DeleteVacancy',
                dataType: 'json',
                data: $('#FilledVacanySave').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire("Deleted Successfully").then(function () {
                            CloseSlider('activevacancy');
                            //var url = "/DataBank/VacancyManagement/Index";
                            //window.location.href = url;
                            var url = "/DataBank/VacancyManagement/Getlist";
                            $('#VacancyList').load(url, { TabIndex: currentTab }, function () { });
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
    if (currentTab == 1) {
        var pendingTableLength = $('#VacancyManagement_List_Count').val();
        if (pendingTableLength > 0) {
            url = "/DataBank/VacancyManagement/ExportToExcel?TabIndex=" + currentTab + "&TabnName=" + " Active Vacancies";
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
    }

    else if (currentTab == 2) {
        var pendingTableLength = $('#VacancyManagement_List_Count').val();
        if (pendingTableLength > 0) {
            url = "/DataBank/VacancyManagement/ExportToExcel?TabIndex=" + currentTab + "&TabnName=" + "Pending for Accept";
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
    }
    else if (currentTab == 3) {
        var pendingTableLength = $('#VacancyManagement_List_Count').val();
        if (pendingTableLength > 0) {
            url = "/DataBank/VacancyManagement/ExportToExcel?TabIndex=" + currentTab + "&TabnName=" + "Overdue Vacancies";
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
    }
    else if (currentTab == 4) {
        var pendingTableLength = $('#EmployeeDtList_Count').val();
        if (pendingTableLength > 0) {
            url = "/DataBank/VacancyManagement/ExportToExcel?TabIndex=" + currentTab + "&TabnName=" + "Resignation Acc. Pending";
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
    }
    else if (currentTab == 5) {
        var pendingTableLength = $('#VacancyManagement_List_Count').val();
        if (pendingTableLength > 0) {
            url = "/DataBank/VacancyManagement/ExportToExcel?TabIndex=" + currentTab + "&TabnName=" + "Cancelled";
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
    }
    else if (currentTab == 6) {
        var pendingTableLength = $('#VacancyManagement_List_Count').val();
        if (pendingTableLength > 0) {
            url = "/DataBank/VacancyManagement/ExportToExcel?TabIndex=" + currentTab + "&TabnName=" + "Closed";
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
    }
    //else {
    //    var pendingTableLength = document.getElementById("ShortlistedAgedMRFTbl").rows.length - 1;
    //    if (pendingTableLength > 0) {
    //        url = "/DataBank/VacancyManagement/ExportToExcel?TabIndex=" + currentTab + "&TabnName=" + "Shortlisted Aged MRF";
    //        window.location.href = url;
    //    }
    //    else {
    //        swal.fire("No data found to export");
    //    }
    //}
}


function getVSJD(sender, id) {
    var url = "/DataBank/VacancyManagement/GetVSJD?DesignationCode=" + $(sender).val();
    $('#MRFRequestSliderSub').load(url, function () {
        $("#Details_GenderArray").multiselect('destroy');
        $("#Details_GenderArray").multiselect();
    });
}


function onVacancyTypeClick(VacancyGI) {
    debugger;
    $.ajax({
        type: 'GET',
        url: "/DataBank/VacancyManagement/GetVacancyTypePopUp?VacancyGI=" + VacancyGI,
        dataType: 'html',

        success: function (Data) {
            if ($($.parseHTML(Data)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {
                var m = $('.bd-example-modal-lg');
                m.find('.modal-body').html('');
                m.find('.modal-body').html(Data);
                /* $("#myDiv").css("display", "block");*/
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            swal.fire(xhr.responseText);

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