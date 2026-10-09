var currentTab=0;
var prevTab='all';
var currentTabName = 'All';
var searchName = 'Allocated';
var flag = 0;
function onNewRequestClick() {
    debugger;
    $('#newRequestSlider').removeClass('hide');
    $('.add-appraisal').show();
}
function OnEmpChange(sender) {
    debugger;
    var Code = $(sender).val();
    if (Code != 0) {
     
        $.ajax({

            type: 'GET',
            url: '/DataBank/Appraisal/EmployeeCard',
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
function CloseSlider(id) {
    $("." + id).removeClass('slider-forms-open');
    $(".form-overlay").remove();
}

function onAppraisalEvaluationPeriodClick(AppraisalTrnGI) {
    debugger;
    var Code = 1;   
    var EmployeeGI = $('#EmployeeGI').val();   

    $.ajax({

        type: 'GET',
        url: '/DataBank/Appraisal/AppraisalLogDetails',
        dataType: 'html',
        data: { AppraisalTrnGI: AppraisalTrnGI, EmployeeGI: EmployeeGI },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {               
                $('#AppraisalLogDetails').html("");
                $('#AppraisalLogDetails').html(result);
            }


        },
        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);


        }
    });
    $('#AppraisalLogDetails').removeClass('hide');

}
function AllTab(Tab,TabName) {
    debugger;
    showSpinner();
    currentTab = Tab;
    currentTabName = TabName;
    var searchid = $('#DurationwiseFilterSlab_Code').val();
    var url = "/DataBank/Appraisal/AppraisalTabs";
   /* $('#pending').addClass('hide');*/
    $(".tab1").removeClass("active");
    $(".tab2").removeClass("active");
    $(".tab3").removeClass("active");
    $(".tab4").removeClass("active");
    $(".more").removeClass("active");
    $("#pendingreq").removeClass("active");
    /*$('#DisciplinaryIncList').removeClass('hide');*/
    $(".all").addClass("active");
    //$('#AppraisalList').load(url, { TabIndex: Tab, SearchId: searchid }, function () { });
    ApplyFilter('apply');
    $(".filterList").show();
}


function Tab(Tab, TabId, TabName) {
    debugger;
    showSpinner();
    currentTab = Tab;
    currentTabName = TabName;
    $('#TabCode').val(Tab);
    //alert($('#AssetAllocationFilter_Code').val());
    var searchid = $('#DurationwiseFilterSlab_Code').val();
    searchName = $('#DurationwiseFilterSlab_Code option:selected').text();
    //$('#pending').addClass('hide');
    //$(".pendingreq").removeClass("active");
    $(".all").removeClass("active");
    $("." + prevTab).removeClass("active");
    $("." + TabId).addClass("active");
    var url = "/DataBank/Appraisal/AppraisalTabs";
    /*$('#AppraisalList').removeClass('hide');*/
    //$('#AppraisalList').load(url, { TabIndex: Tab, SearchId: searchid }, function () { });
    ApplyFilter('apply');
   /* $(".filterList").show();*/
   prevTab = TabId
}
function TabsDropdown(Tab, TabName) {
    debugger;
    currentTab = Tab;
    currentTabName = TabName;
    var TabId = GetTabId($("#TabCode").prop('selectedIndex'));
    //alert($('#AssetAllocationFilter_Code').val());
    var searchid = $('#DurationwiseFilterSlab_Code').val();
    searchName = $('#DurationwiseFilterSlab_Code option:selected').text();
    //$('#pending').addClass('hide');
    //$(".pendingreq").removeClass("active");
    $(".all").removeClass("active");
    $("." + prevTab).removeClass("active");
    $("." + TabId).addClass("active");
    var url = "/DataBank/Appraisal/AppraisalTabs";
    /*$('#AppraisalList').removeClass('hide');*/
    //$('#AppraisalList').load(url, { TabIndex: Tab, SearchId: searchid }, function () { });
    ApplyFilter('apply');
   /* $(".filterList").show();*/
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
        return 'tab4';
    else
        return 'more';
}
function onEmployeeClick(AppraisalGI, EmployeeGI, Tab) {
    debugger;
    if (Tab == 'All') {             
       
        $.ajax({

            type: 'GET',
            url: '/DataBank/Appraisal/AppraisalDetails',
            dataType: 'html',
            data: { AppraisalGI: AppraisalGI, EmployeeGI: EmployeeGI },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('#newDeleteRequestSlider').html("");
                    $('#newDeleteRequestSlider').html(result);
                }


            },
            error: function (xhr, textStatus, errorThrown) {
                debugger
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);


            }
        });

    }
    else if (Tab == 'Pending') {       
        $.ajax({

            type: 'GET',
            url: '/DataBank/Appraisal/Issue',
            dataType: 'html',
            data: { AppraisalGI: AppraisalGI, EmployeeGI: EmployeeGI },
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
function onDeleteAppraisal(AppraisalGI) {

    swal.fire({
        text: "Do you want to delete?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            url = "/DataBank/Appraisal/AppraisalDelete?AppraisalGI=" + AppraisalGI;
            window.location.href = url;
        }
    });   
}
function onStarRatingClick(rating) {
    /*alert(rating);*/
    $('#AppraisalRequest_SelfRating_Code').val(rating);
}
function onStarRatingClick2(rating) {
    /*alert(rating);*/
    $('#AppraisalRequest_AppraiserRating_Code').val(rating);
}
function onStarRatingClick3(rating) {
   /* alert(rating);*/
    $('#AppraisalRequest_ReviewerRating_Code').val(rating);
}

function onFilterDropdownChange(SearchId) {
    debugger;
    var url = "/DataBank/Appraisal/AppraisalTabs";
    $("." + prevTab).removeClass("active");
    $(".all").addClass("active");
    var search = SearchId.value;
    $('#DurationwiseFilterSlab_Code').val(search);
    $('.filterDropDown').val(search);
 
    ApplyFilter('apply');

}

function onAppraisalTypeChange(AppraisalCode,id) {
    debugger;
    validate(AppraisalCode, id);
    var cid = $(AppraisalCode).val();

    $.getJSON("/DataBank/Appraisal/GetAppraisalType", { AppraisalTypeCode: cid }, function (data) {
        debugger;
        console.log(data);
        //$('.trainingType').html(data.trainingRequest.type);
        if (data.assesmentMode == 1) //rating
        {
            $('.text-box').addClass('hide');
            $('.rating').removeClass('hide');
            flag = 1;
        }
        else {
            $('.rating').addClass('hide');
            $('.text-box').removeClass('hide');
            flag = 0;
        }

    });

}

function ExportToExcel() {
    debugger;
    var searchId = $('#DurationwiseFilterSlab_Code').val();
    searchName = $('#DurationwiseFilterSlab_Code option:selected').text();


    var url = "/DataBank/Appraisal/ExportToExcel?TabIndex=" + currentTab + "&SearchId=" + searchId + "&TabName=" + currentTabName + "&SearchName=" + searchName+"&AppraisalTypeCode=" + $('#AppraisalTypeCode').val();
    var TableLength = $('#AppraisalList_Count').val();
    if (TableLength > 0) {

            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
   
}
function ApplyFilter(mode) {
    debugger
    var tabIndex = $('#Tabindex').val();
    var search = $('#SearchId').val();
    $("#Tabindex").val(currentTab);
    $("#SearchId").val($('#DurationwiseFilterSlab_Code').val())
    if (mode == 'apply') {
        if (filterApplicable >= 1) {
            $.ajax({

                type: 'POST',
                url: '/DataBank/Appraisal/ApplyFilter',
                dataType: 'html',
                data: $('#BasicFilter').serialize() + "&AppraisalTypeCode=" + $('#AppraisalTypeCode').val(),
                success: function (result) {
                    debugger
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0)
                    {                      
                        window.location.reload();

                    }
                    else {                   
                        $('#AppraisalList').html("");
                        $('#AppraisalList').html(result);
                        $('#AppraisalList').removeClass('hide');                       
                    }
                    hideSpinner();
                    CloseSlider('basicFilter');
                               
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
                url: '/DataBank/Appraisal/ApplyFilter',
                dataType: 'html',
                data: $('#BasicFilter').serialize() + "&AppraisalTypeCode=" + $('#AppraisalTypeCode').val(),
                success: function (result) {
                    debugger
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }
                    else {
                        $('#AppraisalList').html("");
                        $('#AppraisalList').html(result);
                        $('#AppraisalList').removeClass('hide');
                    }
                    hideSpinner();
                    CloseSlider('basicFilter');
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
            url: '/DataBank/Appraisal/ApplyFilter',
            dataType: 'html',
            data: $('#BasicFilter').serialize() + "&AppraisalTypeCode=" + $('#AppraisalTypeCode').val(),
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('#AppraisalList').html("");
                    $('#AppraisalList').html(result);
                    $('#AppraisalList').removeClass('hide');
                }
                hideSpinner();        

            },


            error: function (xhr, textStatus, errorThrown) {
                $('body').html(xhr.responseText);
            }
        });
    }
}

function process(input) {
    debugger;
    let value = input.value;
    let numbers = value.replace(/[^0-9]/g, "");
    input.value = numbers;
}



function validateAll() {
    debugger;
    var i = 0;
    if (!validateById('#AppraisalRequest_EvaluationPeriod_Code', 'evaluationperiod')) {
        i++;
    }
    if (!validateById('#AppraisalRequest_AppraisalType_Code', 'appraisaltype')) {
        i++;
    }
    if (flag == 0) { //score
        if (!validateById('#AppraisalRequest_AppraiserScore', 'AppraiserScore')) {
            i++;
        }
        if (!validateById('#AppraisalRequest_ReviewerScore', 'reviewerscore')) {
            i++;
        }
        if (!validateById('#AppraisalRequest_SelfScore', 'score')) {
            i++;    
        }
        
    }
    else {
        if ($('#AppraisalRequest_SelfRating_Code').val() == '') {
            swal.fire("Self Rating not selected");
            i++;
        }
        if ($('#AppraisalRequest_AppraiserRating_Code').val() == '') {
            swal.fire("Appraiser Rating not selected");
            i++;
        }
        if ($('#AppraisalRequest_ReviewerRating_Code').val() == '') {
            swal.fire("Reviewer Rating not selected");
            i++;
        }
    }
   
    if (!validateById('#AppraisalRequest_Appraiser_Code', 'appraiser')) {
        i++;
    }
    if (!validateById('#AppraisalRequest_Reviewer_Code', 'reviewer')) {
        i++;
    }
    if (!validateById('#AppraisalRequest_Recommendation_Code', 'Recommendation')) {
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
                //    // Save it
                $.ajax({

                    type: 'POST',
                    url: '/DataBank/Appraisal/Create',
                    dataType: 'json',
                    data: $('#appraisalReqSave').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire("Saved Successfully").then(function () {                                
                                var url = "/DataBank/Appraisal/Index";
                                window.location.href = url;

                            });

                        }
                        else if (result.message =="APPRAISAL_DUPLICATION_ERROR") {
                            swal.fire("Already added Appraisal on this Period").then(function () {

                            });
                        }
                        else {
                            swal.fire(result.message).then(function () {

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
    else
        return false;
}