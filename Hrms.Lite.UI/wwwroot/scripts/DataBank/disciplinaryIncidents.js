var currentTab;
var prevTab;
var currentTabName = 'All';
var currentTab = 0;
function onNewRequestClick() {
    debugger; 
    $('#newRequestSlider').removeClass('hide');
    $('.add-assets').show();
}

function OnEmpChange(sender) {
    debugger;
    var Code = $(sender).val();
    if (Code != 0) {
        $.ajax({

            type: 'GET',
            url: '/DataBank/DisciplinaryIncident/EmployeeCard',
            dataType: 'html',
            data: { EmpCode: Code },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('.search-profile-details').html("");
                    $('.search-profile-details').html(result);
                }


            },
            error: function (xhr, textStatus, errorThrown) {
                debugger
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);


            }
        });
        $('.search-profile-details').show(300);
    }

}
function onFilterDropdownChange(TabIndex, SearchId) {
    debugger;
    showSpinner();
    TabIndex = currentTab;
    $(".all").addClass("active");
    var search = SearchId.value;     
    $('#DurationwiseFilterSlab_Code').val(search);
    $('.filterDropDown').val(search);
    ApplyFilter('apply');
    //$.ajax({

    //    type: 'GET',
    //    url: '/DataBank/DisciplinaryIncident/DisciplinaryTabs',
    //    dataType: 'html',
    //    data: { TabIndex: TabIndex, SearchId: search },
    //    success: function (result) {
    //        debugger
    //        if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
    //            window.location.reload();
    //        }
    //        else {
    //            $('#DisciplinaryIncList').html("");
    //            $('#DisciplinaryIncList').html(result);
    //        }
    //        hideSpinner();

    //    },
    //    error: function (xhr, textStatus, errorThrown) {
    //        debugger
    //        hideSpinner();
    //        document.body.id = "kt_body";
    //        $('body').html(xhr.responseText);


    //    }
    //});

}


function onDisciplinaryLogClick(DisciplinaryIncidentGI) {
    debugger;   
    var EmployeeGI = $('#EmployeeGI').val();
    $.ajax({
        type: 'GET',
        url: '/DataBank/DisciplinaryIncident/DisciplinaryIncidentsLog',
        dataType: 'html',
        data: { DisciplinaryIncidentGI: DisciplinaryIncidentGI, EmployeeGI: EmployeeGI },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('body').append('<div class="form-overlay"></div>');
                $('#DisciplinaryIncidentsLog').html("");
                $('#DisciplinaryIncidentsLog').html(result);
            }
           

        },
        error: function (xhr, textStatus, errorThrown) {
            debugger          
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);


        }
    });
    $('#DisciplinaryIncidentsLog').removeClass('hide');

}



function pendingOpen() {
    $('#pending').removeClass('hide');
    $('#DisciplinaryIncList').addClass('hide');
}
var currentTab;
var prevTab;
function Tab(Tab, TabId,TabName) {
    debugger;
    showSpinner();
    currentTab = Tab;
    currentTabName = TabName;
    $('#TabCode').val(Tab);
    //alert($('#AssetAllocationFilter_Code').val());
    var searchid = $('#DurationwiseFilterSlab_Code').val();
    $('#pending').addClass('hide');
    $(".pendingreq").removeClass("active");
    $(".all").removeClass("active");
    $("." + prevTab).removeClass("active");
    $("." + TabId).addClass("active");
    $('#DisciplinaryIncList').removeClass('hide');
    ApplyFilter('apply');
    //$.ajax({

    //    type: 'GET',
    //    url: '/DataBank/DisciplinaryIncident/DisciplinaryTabs',
    //    dataType: 'html',
    //    data: { TabIndex: Tab, SearchId: searchid },
    //    success: function (result) {
    //        debugger
    //        if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
    //            window.location.reload();
    //        }
    //        else {
    //            $('#DisciplinaryIncList').html("");
    //            $('#DisciplinaryIncList').html(result);
    //        }
    //        hideSpinner();

    //    },
    //    error: function (xhr, textStatus, errorThrown) {
    //        debugger
    //        hideSpinner();
    //        document.body.id = "kt_body";
    //        $('body').html(xhr.responseText);


    //    }
    //});
    $(".filterList").show();
    prevTab = TabId
}

function TabsDropdown(Tab, TabName) {
    debugger;
    currentTab = Tab;
    currentTabName = TabName;
    var TabId = GetTabId($("#TabCode").prop('selectedIndex'));
    //alert($('#AssetAllocationFilter_Code').val());
    var searchid = $('#DurationwiseFilterSlab_Code').val();
    $('#pending').addClass('hide');
    $(".pendingreq").removeClass("active");
    $(".all").removeClass("active");
    $("." + prevTab).removeClass("active");
    $("." + TabId).addClass("active");
    $('#DisciplinaryIncList').removeClass('hide');
    ApplyFilter('apply');
    //$.ajax({

    //    type: 'GET',
    //    url: '/DataBank/DisciplinaryIncident/DisciplinaryTabs',
    //    dataType: 'html',
    //    data: { TabIndex: TabId, SearchId: searchid },
    //    success: function (result) {
    //        debugger
    //        if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
    //            window.location.reload();
    //        }
    //        else {
    //            $('#DisciplinaryIncList').html("");
    //            $('#DisciplinaryIncList').html(result);
    //        }
          

    //    },
    //    error: function (xhr, textStatus, errorThrown) {
    //        debugger
          
    //        document.body.id = "kt_body";
    //        $('body').html(xhr.responseText);


    //    }
    //});
    $(".filterList").show();
    prevTab = TabId
}

function GetTabId(TabCode) {
    if (TabCode == 0)
        return 'all'
    else if (TabCode == 1)
        return 'tab1';
    else if (TabCode == 2)
        return 'tab2';
    else if (TabCode == 3)
        return 'tab3';
    else if (TabCode == 4)
        return 'more';
    else
        return 'pendingreq';
}

function onEmployeeClick(DisciplinaryIncidentGI, EmployeeGI, Tab) {
    debugger;
    if (Tab == 'All') {
       
        $.ajax({

            type: 'GET',
            url: '/DataBank/DisciplinaryIncident/DisciplinaryIncidentsDetails',
            dataType: 'html',
            data: { DisciplinaryIncidentGI: DisciplinaryIncidentGI, EmployeeGI: EmployeeGI },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('#DisciplinaryInc').html("");
                    $('#DisciplinaryInc').html(result);
                }


            },
            error: function (xhr, textStatus, errorThrown) {
                debugger

                document.body.id = "kt_body";
                $('body').html(xhr.responseText);


            }
        });

        $('#DisciplinaryInc').removeClass('hide');
        $('#DisciplinaryInc').show();
    }
    else if (Tab == 'Pending') {
        $.ajax({

            type: 'GET',
            url: '/DataBank/DisciplinaryIncident/Issue',
            dataType: 'html',
            data: { DisciplinaryIncidentGI: DisciplinaryIncidentGI, EmployeeGI: EmployeeGI },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('.issuepending').html("");
                    $('.issuepending').html(result);
                }


            },
            error: function (xhr, textStatus, errorThrown) {
                debugger

                document.body.id = "kt_body";
                $('body').html(xhr.responseText);


            }
        });
        $('#issue').removeClass('hide');
        $('.issuepending').show();
    }

}

function onExportToExcelClick() {
    debugger;
    var searchid = $('#DurationwiseFilterSlab_Code').val();
    /*var searchid = $('#AssetAllocationFilter_Code').val();*/
    
    if (currentTab == 1001) {
        if (pendingTableLength > 0) {
            var pendingTableLength = $('#DisciplinaryIncidentsList_Count').val();
            url = "/DataBank/DisciplinaryIncident/ExportToExcel?TabIndex=" + currentTab + "&SearchId=" + searchid + "&TabName=" + currentTabName + $('#BasicFilter').serialize();
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
    }
    else {
        var assetAllocationTableLength = $('#DisciplinaryIncidentsList_Count').val();
        if (assetAllocationTableLength > 0) {
            url = "/DataBank/DisciplinaryIncident/ExportToExcel?TabIndex=" + currentTab + "&SearchId=" + searchid + "&TabName=" + currentTabName + "&IncidentCode=" + $('#IncidentCode').val();
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
    }
}

function onDeleteDisciplinaryIncidents(DisciplinaryIncidentGI) {
    swal.fire({
        text: "Do you want to Delete?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            url = "/DataBank/DisciplinaryIncident/DisciplinaryIncidentsDelete?DisciplinaryIncidentGI=" + DisciplinaryIncidentGI;
            window.location.href = url;
        }
        else {
            return false;
        }
    })
}

function viewFileFromDT() {
    debugger;
    var AbsoluteURI = $('#IncidentsAction_Incidents_FileUri').val();
    if (AbsoluteURI == "") {
        swal.fire("No file Uploaded")
    }
    else {
        window.open(AbsoluteURI);
    }
}

function Preview() {
    debugger;
    var path = URL.createObjectURL($('.PreviewImage')[0].files[0]);
    if (path == "") {
        swal.fire("No file Uploaded")
    }
    else {
         window.open(path);
    }
}




function validateAllDisciplinaryIncidents() {
    debugger
    var i = 0;

    if (!validateById('#IncidentsAction_Incident_Code', 'incident')) {
        i++;
    }
    if (!validateById('#IncidentsAction_IncidentType_Code', 'incidenttype')) {
        i++;
    }

    // Incident date validation
    var incidentDateStr = $('#IncidentsAction_Incidentdate').val();
    if (incidentDateStr) {
        var enteredDate = new Date(incidentDateStr);
        var today = new Date();
        today.setHours(0, 0, 0, 0);

        if (enteredDate > today) {
            swal.fire("Incident date cannot be greater than today.");
            i++;
        }
    } else {
        if (!validateById('#IncidentsAction_Incidentdate', 'incidentdate')) {
            i++;
        }
    }

    if (!validateById('#IncidentsAction_ReportedBy', 'reportedby')) {
        i++;
    }
    if (!validateReportedDate('#IncidentsAction_Reportedtdate', 'reporteddate')) {
        i++;
    }
    if (!validateById('#IncidentsAction_ActionTaken_Code', 'actiontaken')) {
        i++;
    }
    if (!validateById('#IncidentsAction_Narration', 'narration')) {
        i++;
    }

    if (i == 0)
        swal.fire({
            text: "Do you want to Save?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                var DPData = new FormData($('#DisplinaryIncSave')[0]);
                $.ajax({
                    type: 'POST',
                    url: '/DataBank/DisciplinaryIncident/Create',
                    contentType: false,
                    processData: false,
                    dataType: 'json',
                    data: DPData,
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire("Saved Successfully").then(function () {
                                url = "/DataBank/DisciplinaryIncident/Index";
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
            } else {
                return false;
            }
        })
    else
        return false;
}


//function validateAllDisciplinaryIncidents() {
//    debugger
//    var i = 0;
//    if (!validateById('#IncidentsAction_Incident_Code', 'incident')) {
//        i++;
//    }
//     if (!validateById('#IncidentsAction_IncidentType_Code', 'incidenttype')) {
//        i++;
//    }
//     if (!validateById('#IncidentsAction_Incidentdate', 'incidentdate')) {
//        i++;
//    }
//     if (!validateById('#IncidentsAction_ReportedBy', 'reportedby')) {
//        i++;
//    }
//     if (!validateReportedDate('#IncidentsAction_Reportedtdate', 'reporteddate')) {
//        i++;
//    }
//     if (!validateById('#IncidentsAction_ActionTaken_Code', 'actiontaken')) {
//        i++;
//    }
//     if (!validateById('#IncidentsAction_Narration', 'narration')) {
//        i++;
//    }
//    if (i == 0)
//        swal.fire({
//            text: "Do you want to Save?",
//            icon: 'warning',
//            showCancelButton: true,
//            confirmButtonColor: '#3085d6',
//            cancelButtonColor: '#d33',
//            confirmButtonText: 'Ok'
//        }).then((result) => {
//            if (result.isConfirmed) {
//                //    // Save it
//                var DPData = new FormData($('#DisplinaryIncSave')[0]);
//                $.ajax({

//                    type: 'POST',
//                    url: '/DataBank/DisciplinaryIncident/Create',
//                    contentType: false,
//                    processData: false,
//                    dataType: 'json',
//                    data: DPData,
//                    success: function (result) {
//                        if (result.success == true) {
//                            swal.fire("Saved Successfully").then(function () {                                
//                                url = "/DataBank/DisciplinaryIncident/Index";
//                                window.location.href = url;

//                            });

//                        }

//                        else {
//                            swal.fire(result.message).then(function () {

//                            });
//                        }

//                    },
//                    error: function (xhr, textStatus, errorThrown) {
//                        $('body').html(xhr.responseText);
//                    }
//                });

//            }
//            else {
//                return false;
//            }
//        })
//    else
//        return false;
//}


function validateReportedDate(val, id) {
    debugger;
    if ($(val).val() == "") {
        $(val).addClass('is-invalid');
        $("#" + id + "").addClass('error-message');
        $("#" + id + "").html("This field is required");
    }
    else {

        var issueDate = new Date($('#IncidentsAction_Incidentdate').val());
        var dueDate = new Date($(val).val().toString());
        //var d1 = '2013-11-02', d2 = '2013-11-01';
        //console.log(d1 < d2);
        console.log(dueDate);
        console.log(issueDate);
        console.log(issueDate > dueDate);
        var iDate = new Date(issueDate.toLocaleDateString());
        var rDate = new Date(dueDate.toLocaleDateString());
        if (iDate > rDate) {
            $(val).addClass('is-invalid');
            $("#" + id + "").addClass('error-message');
            $("#" + id + "").html("Reported date should be greater than Incident date");
            return false;
        }
        else {
            $(val).removeClass('is-invalid');
            $("#" + id + "").removeClass('error-message');
            $("#" + id + "").html("");
            return true;
        }
    }
}
function CheckFIleExtensionSize(e) {
    debugger;
    console.log(e);
    debugger;
    var size = $(e)[0].files[0].size;
    var filename = $(e)[0].files[0].name;
    var fileExtension = ['pdf','jpeg', 'jpg', 'png', 'bmp'];
    if (size > 1024 * 1024) {
        swal.fire('File size should be less than 1 MB');
        $(e).val('');
    }
    else if ($.inArray($(e).val().split('.').pop().toLowerCase(), fileExtension) == -1) {
        swal.fire("Only pdf,jpg, jpeg, png and bmp formats are allowed");
        $(e).val('');
    }
    else {
        $('#disp').hide();
    }
}

function ApplyFilter(mode) {
    debugger
    var tabIndex = $('#Tabindex').val();
    var search = $('#SearchId').val();
    $("#Tabindex").val(currentTab);
    $("#SearchId").val($('#DurationwiseFilterSlab_Code').val())
    if (mode == 'apply') {
        if (filterApplicable>=1) {
            $.ajax({

                type: 'POST',
                url: '/DataBank/DisciplinaryIncident/ApplyFilter',
                dataType: 'html',
                data: $('#BasicFilter').serialize() + "&IncidentCode=" + $('#IncidentCode').val(),
                success: function (result) {
                    debugger
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }
                    else {
                        CloseSlider('basicFilter');
                        $('#DisciplinaryIncList').html("");
                        $('#DisciplinaryIncList').html(result);
                        $('#DisciplinaryIncList').removeClass('hide');
                        hideSpinner();
                    }

                              
                },


                error: function (xhr, textStatus, errorThrown) {
                    $('body').html(xhr.responseText);
                }
            });           
            $('#filterForm').removeClass('filter-apply');
        }
        else {
            $.ajax({

                type: 'POST',
                url: '/DataBank/DisciplinaryIncident/ApplyFilter',
                dataType: 'html',
                data: $('#BasicFilter').serialize() + "&IncidentCode=" + $('#IncidentCode').val(),
                success: function (result) {
                    debugger
                    CloseSlider('basicFilter');
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }
                    else {
                        $('#DisciplinaryIncList').html("");
                        $('#DisciplinaryIncList').html(result);
                        $('#DisciplinaryIncList').removeClass('hide');
                        hideSpinner();
                    }
             
                },


                error: function (xhr, textStatus, errorThrown) {
                    $('body').html(xhr.responseText);
                }
            });
            $('#filterForm').addClass('filter-apply');
        }
        
    }
    else {
        filterApplicable = 1;
        $('.reset').val('');
        $('#Optional_Filter_EmploymentStatus_Code').val(0);
        $('#filterForm').removeClass('filter-apply');
        $.ajax({

            type: 'POST',
            url: '/DataBank/DisciplinaryIncident/ApplyFilter',
            dataType: 'html',
            data: $('#BasicFilter').serialize() + "&IncidentCode=" + $('#IncidentCode').val(),
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('#DisciplinaryIncList').html("");
                    $('#DisciplinaryIncList').html(result);
                    $('#DisciplinaryIncList').removeClass('hide');
                    hideSpinner();
                }
               

            },


            error: function (xhr, textStatus, errorThrown) {
                $('body').html(xhr.responseText);
            }
        });
    }
}


