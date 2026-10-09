var prevTab = 'AllTabRewards';
var currentTab = 0;
var Tab_Name = 'All';
function Tab(TabId, spanName,TabName) {
    debugger
    showSpinner();
    currentTab = TabId;
    $('#TabCode').val(TabId);
    var searchid = $('#RewardsAndRecognitionFilterCombo_Code').val();    
    $("." + prevTab).removeClass("active");
    $("." + spanName).addClass("active");
   

    
    if (TabId == 1001) {
        ApplyFilter('apply');
        $(".Rewardsfilter").show();
       
    }
    else if (TabId == 1002) {
        ApplyFilter('apply');
        $(".Rewardsfilter").hide();
       
    }
    else
    {
        ApplyFilter('apply');
        $(".Rewardsfilter").show();
      
    }
    prevTab = spanName;
    Tab_Name = TabName;
}
function TabsDropdown(TabId, TabName) {
    debugger
    currentTab = TabId;
    var searchid = $('#RewardsAndRecognitionFilterCombo_Code').val();
    var spanName = GetSpanName($("#TabCode").prop('selectedIndex'));
    $("." + prevTab).removeClass("active");
    $("." + spanName).addClass("active");
   

    
    if (TabId == 1001) {
        ApplyFilter('apply');
        $(".Rewardsfilter").show();
       
    }
    else if (TabId == 1002) {
        ApplyFilter('apply');
        $(".Rewardsfilter").hide();
       
    }
    else
    {
        ApplyFilter('apply');
        $(".Rewardsfilter").show();
      
    }
    prevTab = spanName;
    Tab_Name = TabName;
}

function GetSpanName(TabCode) {
    if (TabCode == 0)
        return 'AllTabRewards'
    else if (TabCode == 1)
        return 'tab1';
    else if (TabCode == 2)
        return 'tab2';
    else if (TabCode == 3)
        return 'more';
    else if (TabCode == 4)
        return 'PointsSummaryTab';
    else
        return 'PendingRecommendationTabRewards';
}

function onFilterDropdownChange(SearchId) {
    debugger;
    showSpinner();
   var  TabIndex = currentTab;

    var url = "/DataBank/RewardsAndRecognition/Tab";
    var search = SearchId.value;
    $('#RewardsAndRecognitionFilterCombo_Code').val(search);
    $('.filterDropDown').val(search);
    if (currentTab != 1001) {
        ApplyFilter('apply');
        $('#PointsSummaryPartial').addClass('hide');
        $('#rewardsListPartial').removeClass('hide');
    }
    else {

        ApplyFilter('apply');
        $('#PointsSummaryPartial').removeClass('hide');
        $('#rewardsListPartial').removeClass('hide');
    }
}


function onEmployeeClick(RewardsAndRecognitionGI, EmployeeGI, Tab) {
    debugger;
    if (Tab == 'All') { 
        $.ajax({

            type: 'GET',
            url: '/DataBank/RewardsAndRecognition/RewardsAndRecognitionDetails',
            dataType: 'html',
            data: { RewardsAndRecognitionGI: RewardsAndRecognitionGI, EmployeeGI: EmployeeGI },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('#RewardDtSlider').html("");
                    $('#RewardDtSlider').html(result);
                }


            },
            error: function (xhr, textStatus, errorThrown) {
                debugger
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);


            }
        });

     
    }
    else if (Tab == 'pending') {
        $.ajax({

            type: 'GET',
            url: '/DataBank/RewardsAndRecognition/RewardsApprove',
            dataType: 'html',
            data: { RewardsAndRecognitionGI: RewardsAndRecognitionGI, EmployeeGI: EmployeeGI },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('#RewardDtSlider').html("");
                    $('#RewardDtSlider').html(result);
                }


            },
            error: function (xhr, textStatus, errorThrown) {
                debugger
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);


            }
        });

   
    }
}

function onEmployeeSummaryClick(EmployeeGI) {
    debugger;
    var searchId = $('#RewardsAndRecognitionFilterCombo_Code').val(); 
    $.ajax({

        type: 'GET',
        url: '/DataBank/RewardsAndRecognition/EmployeeSummary',
        dataType: 'html',
        data: { EmployeeGI: EmployeeGI, SearchId:searchId },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('body').append('<div class="form-overlay"></div>');
                $('#RewardDtSlider').html("");
                $('#RewardDtSlider').html(result);
            }


        },
        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);


        }
    });
     
       

    }


function onPointCriteriaChange(sender, spanId)
{
    debugger;
    var code = $(sender).val();
    if (code == 0) {
        $("#" + spanId + "").addClass('error-message');
        $(sender).addClass('is-invalid');
        $("#" + spanId + "").html("This field is required");
    }
    else {
        $("#" + spanId + "").removeClass('error-message');
        $(sender).removeClass('is-invalid');
        $("#" + spanId + "").html("");

        $.getJSON("/DataBank/RewardsAndRecognition/GetPoints", { PointCriteriaCode: code }, function (data) {
            debugger;
            console.log(data);
            $('.Points').html(data);
        });
    }
}



    function onRewardChange(sender, spanId)
  {

        var code = $(sender).val();
        if (code == 0) {
            $("#" + spanId + "").addClass('error-message');
            $(sender).addClass('is-invalid');
            $("#" + spanId + "").html("This field is required");
        }
        else {
            $("#" + spanId + "").removeClass('error-message');
            $(sender).removeClass('is-invalid');
            $("#" + spanId + "").html("");
            $.getJSON("/DataBank/RewardsAndRecognition/GetPointCriteria", { RewardCode: code }, function (data) {
                console.log(data);
                $('#PointCriteria_Code option').remove();
                $('#PointCriteria_Code').append('<option value>Select Point Criteria</option');
                debugger;
                for (var i = 0; i < data.length; i++) {
                    $('#PointCriteria_Code').append('<option value="' + data[i].code + '">' + data[i].name + '</option');
                }

            });
        }
}



function OnEmployeeChange(sender) {
    debugger;
    var EmpCode = $(sender).val();
    var url = "/DataBank/RewardsAndRecognition/EmployeeCard";  
    $.ajax({

        type: 'GET',
        url: '/DataBank/RewardsAndRecognition/EmployeeCard',
        dataType: 'html',
        data: { EmpCode: EmpCode },
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
function onRewardLogClick(RewardsAndRecognitionGI) {
    debugger;
    $.ajax({

        type: 'GET',
        url: '/DataBank/RewardsAndRecognition/RewardLogDetails',
        dataType: 'html',
        data: { RewardsAndRecognitionGI: RewardsAndRecognitionGI  },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('body').append('<div class="form-overlay"></div>');
                $('#RewardLogDetailsSlider').html("");
                $('#RewardLogDetailsSlider').html(result);
            }


        },
        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);


        }
    });

   
}

function onNewRequestClickRewards() {
    debugger;
    $('body').append('<div class="form-overlay"></div>');
    $('#newRequestRewardsSlider').removeClass('hide');
    $('.newRequestDiv').show();
}



function Delete(RewardsAndRecognitionGI) {
    debugger;
    swal.fire({
        text: "Do you want to Delete?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            //    // Save it
            url = "/DataBank/RewardsAndRecognition/Delete?RewardsAndRecognitionGI=" + RewardsAndRecognitionGI;
            window.location.href = url;

        }
        else {
            return false;
        }
    })
    
}
function  ExportToExcel() {
    debugger;
    var TableLength = $('#RewardsAndRecognitionList_Count').val();

    if (TableLength != "")
    { 
        var searchId = $('#RewardsAndRecognitionFilterCombo_Code').val();
        searchName = $('#RewardsAndRecognitionFilterCombo_Code option:selected').text();
        url = "/DataBank/RewardsAndRecognition/ExportToExcel?TabIndex=" + currentTab + "&SearchId=" + searchId + "&SearchName=" + searchName + "&TabName=" + Tab_Name + "&RewardCode=" + $('#RewardsCode').val() + '&data=' + $('#BasicFilter').serialize();;
        window.location.href = url;
     }
     else {
         swal.fire("No data found to export");
          }
   
}

function CheckExtensionAndSize(e) {
    debugger;


    var size = $(e)[0].files[0].size; //Size is in Bytes
    var filename = $(e)[0].files[0].name;
    var fileExtension = ['jpeg', 'jpg', 'png', 'bmp', 'pdf'];
    if (size > 1024 * 1024)  //1,048,576 Bytes=1 MB
    {
        swal.fire('File size should be less than 1 MB');
        $(e).val('');
        return false;
    }
    if ($.inArray($(e).val().split('.').pop().toLowerCase(), fileExtension) == -1) {
        swal.fire("Only pdf, Jpeg,Jpg,Png and bmp files are allowed");
        $(e).val('');
        return false;
 
    }
    return true;

}

function Preview()
{
    debugger
    var path = URL.createObjectURL($('.PreviewImage')[0].files[0]);
    window.open(path, "_blank");
}



function validateRewardsRecognition() {
    var i = 0;
    if (!validateById('#Rewards_Code', 'reward')) {
        i++;
    }
    if (!validateById('#PointCriteria_Code', 'pointCriteria')) {
        i++;
    }
    if (!validateById('#RewardDate', 'rewardDate')) {
        i++;
    }
    if (!validateById('#RewardRecommendedBy', 'recommendedBy')) {
        i++;
    }
    if (!validateById('#RecommendedFor', 'recommendedFor')) {
        i++;
    }
    //else if (!RemarksLimitCheck('Remarks')) {
    //    return false;
    //}
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
                var RAndRData = new FormData($('#RewardsAndRecogSave')[0]);
                $.ajax({
                    type: 'POST',
                    url: '/DataBank/RewardsAndRecognition/Save',
                    contentType: false,
                    processData: false,
                    dataType: 'json',
                    data: RAndRData,
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire("Saved Successfully").then(function () {
                           
                                var url = "/DataBank/RewardsAndRecognition/Index";
                                window.location.href = url;

                            });

                        }

                        else {
                            swal.fire(result.message).then(function () {

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
    else
        return false;
}






function ApplyFilter(mode)
{
    debugger
    var tabIndex = $('#Tabindex').val();
    var search = $('#SearchId').val();
    $("#Tabindex").val(currentTab);
    $("#SearchId").val($('#RewardsAndRecognitionFilterCombo_Code').val())
    if (mode == 'apply') {
        if (filterApplicable >= 1) {
            $.ajax({

                type: 'POST',
                url: '/DataBank/RewardsAndRecognition/ApplyFilter',
                dataType: 'html',
                data: $('#BasicFilter').serialize() + "&RewardCode=" + $('#RewardsCode').val(),
                success: function (result) {
                    debugger                  
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();

                    }                 
                    else
                    {                        
                        $('#rewardsListPartial').html("");
                        $('#rewardsListPartial').html(result);                       
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
                url: '/DataBank/RewardsAndRecognition/ApplyFilter',
                dataType: 'html',
                data: $('#BasicFilter').serialize() + "&RewardCode=" + $('#RewardsCode').val(),
                success: function (result) {
                    debugger
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }
                    else {
                        $('#rewardsListPartial').html("");
                        $('#rewardsListPartial').html(result);
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
    else
    {
        showSpinner();
        filterApplicable = 1;
        $('.reset').val('');
        $('#Optional_Filter_EmploymentStatus_Code').val(0);
        $('#filterForm').removeClass('filter-apply');
        $.ajax({

            type: 'POST',
            url: '/DataBank/RewardsAndRecognition/ApplyFilter',
            dataType: 'html',
            data: $('#BasicFilter').serialize() + "&RewardCode=" + $('#RewardsCode').val(),
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();

                }              
                else {
                    $('#rewardsListPartial').html("");
                    $('#rewardsListPartial').html(result);
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
   


