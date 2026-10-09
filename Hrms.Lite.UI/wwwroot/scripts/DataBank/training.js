
var prevTab = 'all';
var currentTab=0;
var TabName = 'All';
var trainingAssessmentMode = '';
function onNewRequestClick() {
    $('#EmpCode').val("");
 
    $('#newRequestSlider').removeClass('hide');
}

function OnEmpChange(sender) {
    debugger;
    var Code = $(sender).val();
    if (Code != 0) {

        $.ajax({

            type: 'GET',
            url: '/DataBank/Training/EmployeeCard',
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

function validateDuration(evt) {

    var ASCIICode = (evt.which) ? evt.which : evt.keyCode
    if (ASCIICode > 31 && (ASCIICode < 48 || ASCIICode > 57)) {
        swal.fire("Duration should be enter in hours");
        return false;
    }
    return true;
}

function onTrainingNameChange(sender) {
    var cid = $(sender).val();
    debugger;
    validateById('#TrainingRequest_TrainingName_Code', 'Training');
    $.getJSON("/DataBank/Training/GetTrainingType", { TrainingCode: cid }, function (data) {
        console.log(data);
        $('.trainingType').html(data.name);
        trainingAssessmentMode = data.code;
        if (data.code == 1) //rating
        {
            $('.score').addClass('hide');
            $('.rating').removeClass('hide');
        }
        else {
            $('.rating').addClass('hide');
            $('.score').removeClass('hide');
        }
        
    });
    
}
function onEmployeeClick(EmployeeTrainingGI, EmployeeGI, Tab) {
   /* if (Tab == 'All') { */
     
        $.ajax({

            type: 'GET',
            url: '/DataBank/Training/TrainingDetails',
            dataType: 'html',
            data: { EmployeeTrainingGI: EmployeeTrainingGI, EmployeeGI: EmployeeGI },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('#trainingDetails').html("");
                    $('#trainingDetails').html(result);
                }


            },
            error: function (xhr, textStatus, errorThrown) {
                debugger
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);


            }
        });
        $('#trainingDetails').removeClass('hide');        
    /*}*/
}

function tabClick(TabIndex, CurrentTabName, TrainingTabCode) {
    debugger;
    showSpinner();
    currentTab = TabIndex;
    TabName = CurrentTabName;
    $("." + prevTab).removeClass("active");
    //$("." + Tab).addClass("active");    
    $('#TabCode').val(TabIndex);

    var searchid = $('#FilterList_Code').val();
    var url = "/DataBank/Training/GetTrainingList";

    ApplyFilter('apply', TrainingTabCode);
    //prevTab = Tab;
}

function TabsDropdown(TabIndex, CurrentTabName) {
    currentTab = TabIndex;
    TabName = CurrentTabName;
    var Tab = GetTab($("#TabCode").prop('selectedIndex'))
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");
    //if ($('#FilterList_Code').val() != 0)
    var searchid = $('#FilterList_Code').val();
    //else
    //var searchid = 3;
    var url = "/DataBank/Training/GetTrainingList";

    ApplyFilter('apply');
    prevTab = Tab;
}

function GetTab(TabCode) {
    if (TabCode == 0)
        return 'all'
    else if (TabCode == 1)
        return 'tab1';
    else if (TabCode == 2)
        return 'tab2';
    else if (TabCode == 3)
        return 'tab3';
    else if (TabCode == 4)
        return 'tab4';
    else
        return 'tab5';

}

function onFilterDropdownChange(TabIndex, SearchId) {
    showSpinner();
    var url = "/DataBank/Training/GetTrainingList";
    $("." + prevTab).removeClass("active");
    $(".all").addClass("active");
    var search = SearchId.value;
    $('#FilterList_Code').val(search);
    $('.filterDropDown').val(search);
    //$('#trainingList').load(url, { TabIndex: TabIndex, SearchId: search }, function () { });    
    ApplyFilter('apply', $('#TrainingTabCode').val());

}
function onLogFilterDropdownChange(SearchId) {  
    var search = SearchId.value;
    var EmployeeGI = $('#EmployeeGI').val();


    $.ajax({

        type: 'GET',
        url: '/DataBank/Training/GetTrainingLogList',
        dataType: 'html',
        data: { EmployeeGI: EmployeeGI, SearchId: search },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#trainingLog').html("");
                $('#trainingLog').html(result);
            }


        },
        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);


        }
    });

    
}

function onStarRatingClick(rating) {
    $('#TrainingRequest_TrainingAssessmentMode_Code').val(rating);
    //alert(rating);
}


function onDeleteTraining(EmployeeTrainingGI) {
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
                url: '/DataBank/Training/TrainingDelete',
                dataType: 'json',
                data: { EmployeeTrainingGI: EmployeeTrainingGI},
                success: function (result) {
                    console.log(result);
                    if (result.success == true) {
                        swal.fire("Deleted Successfully").then(function () {
                            CloseSlider('trainingDetails');
                            ApplyFilter('apply');
                        });
                    }
                    else {
                        swal.fire(result.message);
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
    });

    //url = "/DataBank/Training/TrainingDelete?EmployeeTrainingGI=" + EmployeeTrainingGI;
    //window.location.href = url;
}
function onExportToExcelClick() {
    if ($('#FilterList_Code').val() != 0)
        var searchid = $('#FilterList_Code').val();
    else
        var searchid = 3;
    
    var pendingTableLength = $('#TrainingList_Count').val();
        if (pendingTableLength > 0) {           

            $.ajax({

                type: 'POST',
                url: '/DataBank/Training/ExportExcel',
                dataType: 'json',
                data: $('#BasicFilter').serialize(),
                success: function (result) {                 
                    url = "/DataBank/Training/ExportToExcel?TabIndex=" + currentTab + "&SearchId=" + searchid + "&TabName=" + TabName + "&TrainingTabCode=" + $('#TrainingTabCode').val();
                    window.location.href = url;                       
                },


                error: function (xhr, textStatus, errorThrown) {
                    $('body').html(xhr.responseText);
                }
            });


        }
        else {
            swal.fire("No data found to export");
        }
}



function ApplyFilter(mode, TrainingTabCode) {
    var tabIndex = $('#Tabindex').val();
    var search = $('#SearchId').val();
    $("#Tabindex").val(currentTab);
    $("#SearchId").val($('#FilterList_Code').val())
    if (mode == 'apply') {
        if (filterApplicable >= 1) {
            $.ajax({

                type: 'POST',
                url: '/DataBank/Training/ApplyFilter',
                dataType: 'html',
                data: $('#BasicFilter').serialize() + "&TrainingTabCode=" + TrainingTabCode,
                success: function (result) {
                    CloseSlider('basicFilter');
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();

                    }
                    else {
                        $('#trainingList').html("");
                        $('#trainingList').html(result);

                        $('#trainingList').removeClass('hide');
                    }
                    hideSpinner();
                              
                },


                error: function (xhr, textStatus, errorThrown) {
                    $('body').html(xhr.responseText);
                }
            });
            $('#filterForm').removeClass('filter-apply');
        }
        else {
            showSpinner();
        $.ajax({

            type: 'POST',
            url: '/DataBank/Training/ApplyFilter',
            dataType: 'html',
            data: $('#BasicFilter').serialize() + "&TrainingTabCode=" + TrainingTabCode,
            success: function (result) {
                CloseSlider('basicFilter');
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();

                }
                else {
                    $('#trainingList').html("");
                    $('#trainingList').html(result);
                    $('#trainingList').removeClass('hide');
                }
                hideSpinner();
                             
            },


            error: function (xhr, textStatus, errorThrown) {
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
            url: '/DataBank/Training/ApplyFilter',
            dataType: 'html',
            data: $('#BasicFilter').serialize() + "&TrainingTabCode=" + TrainingTabCode,
            success: function (result) {
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();

                }
                else {

                    $('#trainingList').html("");
                    $('#trainingList').html(result);
                    $('#trainingList').removeClass('hide');
                }
               
                hideSpinner();               
            },


            error: function (xhr, textStatus, errorThrown) {
                $('body').html(xhr.responseText);
            }
        });
    }
}

function ValidateTraining() {
    var i = 0;
    if (!validateById('#TrainingRequest_TrainingName_Code', 'Training')) {
        i++;
    }
    if (!validateById('#TrainingRequest_Type', 'Request')) {
        i++;
    }
    if (!validateById('#TrainingRequest_AttendedDate', 'Attended')) {
        i++;
    }
    if (!validateById('#TrainingRequest_Trainer', 'Trainer')) {
        i++;
    }
    if (!validateById('#TrainingRequest_Duration', 'duration')) {
        i++;
    }
    if (!validateById('#TrainingRequest_Mode_Code', 'mode')) {
        i++;
    }
    if (trainingAssessmentMode == 1) {
        if ($('#TrainingRequest_TrainingAssessmentMode_Code').val() == '') {
            swal.fire('Rating is not selected');
            i++;
        }
    }
    else {

        if (!validateById('#TrainingRequest_PostAssessmentScore', 'assessmentScore') ) {
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
                    url: '/DataBank/Training/Create',
                    dataType: 'json',
                    data: $('#trainingSave').serialize(),
                    success: function (result) {
                        console.log(result);
                        if (result.success == true) {
                            swal.fire("Saved Successfully").then(function () {
                                var url = "/DataBank/Training/Index";
                                window.location.href = url;
                            });
                        }
                        else {
                            swal.fire(result.message);
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
        });
        
    }
    else
        return false;
}