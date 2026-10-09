var prevTab='all';
var currentTab = 0;
var currentTabName = 'All';
var searchName = 'Allocated';
var searchid = 1;

function Tab(Tab, TabId, TabName, mode,AssetCategoryCode) {
    showSpinner();
    debugger;    
    currentTab = Tab;
    currentTabName = TabName;
    $('#TabCode').val(Tab);
    //alert($('#AssetAllocationFilter_Code').val());
    //var searchid = $('#AssetAllocationFilter_Code').val();
    searchName = $('#AssetAllocationFilter_Code option:selected').text();
    $('#pending').addClass('hide');
    $(".pendingreq").removeClass("active");
    $(".all").removeClass("active");
    $("." + prevTab).removeClass("active");
    $("." + TabId).addClass("active");    
    var url = "/DataBank/AssetAllocation/AssetTabs";
    $('#assetAllocList').removeClass('hide');    
    //$('#assetAllocList').load(url, { TabIndex: Tab, SearchId: searchid}, function () { });
    $("#Tabindex").val(currentTab);
    $("#SearchId").val($('#AssetAllocationFilter_Code').val())
    ApplyFilter(mode, $(AssetCategoryCode).val());
    $(".assetlist").show();
    $(".filterList").show();
    prevTab = TabId
    if (currentTab == 0) {
        
        $('#AssetAllocationFilter_Code').removeClass('hide');
        
    }
    else {
        
        $('#AssetAllocationFilter_Code').addClass('hide');
    }

   
   
}

function tabClick(mode) {
    debugger;
    showSpinner();
    currentTab = $('#TabCode').val();
    currentTabName = $('#TabCode option:selected').text();
    //searchid = $('#AssetAllocationFilter_Code').val();
    searchName = $('#AssetAllocationFilter_Code option:selected').text();
    $("." + prevTab).removeClass("active");
    var TabCode = $("#TabCode").prop('selectedIndex');
    //$("." + Tab).addClass("active");
    //if ($('#FilterList_Code').val() != 0)
    if (TabCode == 0)
        prevTab = 'all'
    else if (TabCode == 1)
        prevTab = 'tab1';
    else if (TabCode == 2)
        prevTab = 'tab2';
    else if (TabCode == 3)
        prevTab = 'tab3';
    else if (TabCode == 1000)
        prevTab = 'more';
    else
        prevTab = 'pendingreq';

    $("." + prevTab).addClass("active");
    
    if (currentTab != 1001) {
        $('#assetFilter').removeClass('hide');
        $('#assetAllocList').removeClass('hide');
        $('#pending').addClass('hide');
        var url = "/DataBank/AssetAllocation/AssetTabs";
      
        //$('#assetAllocList').load(url, { TabIndex: currentTab, SearchId: searchid }, function () { });
        ApplyFilter(mode);
    }
    else {
        $('#assetFilter').addClass('hide');
        $('#assetAllocList').addClass('hide');
        $('#pending').removeClass('hide');
        var url = "/DataBank/AssetAllocation/Pending";
        
        ApplyFilter(mode);
        //$("#pending").load(url, { TabIndex: currentTab }, function () {});
            }
}



function ApplyFilter(mode, AssetCategoryCode) {
    debugger
    var tabIndex = $('#Tabindex').val();
    var search = $('#SearchId').val();
    
    $("#Tabindex").val(currentTab);
    $("#SearchId").val(searchid)
    if (mode == 'apply') {
        if (filterApplicable >= 1) {
            $.ajax({

                type: 'POST',
                url: '/DataBank/AssetAllocation/ApplyFilter',
                dataType: 'html',
                data: $('#BasicFilter').serialize() + "&AssetCategoryCode=" + AssetCategoryCode,
                success: function (result) {
                    debugger
                    CloseSlider('basicFilter');
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }
                    else {
                        $('#assetAllocList').html("");
                        $('#assetAllocList').html(result);
                        $('#assetAllocList').removeClass('hide');
                        $('#pending').addClass('hide');
                        $(".filterList").show();
                    }
                    hideSpinner();
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
                url: '/DataBank/AssetAllocation/ApplyFilter',
                dataType: 'html',
                data: $('#BasicFilter').serialize() + "&AssetCategoryCode=" + AssetCategoryCode,
                success: function (result) {
                    debugger
                    CloseSlider('basicFilter');
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }
                    else {
                        if (tabIndex != 1001) {

                            $('#assetAllocList').html("");
                            $('#assetAllocList').html(result);

                            $('#assetAllocList').removeClass('hide');
                            $('#pending').addClass('hide');
                            $(".filterList").show();
                            //$('#rewardsListPartial').addClass('hide');


                        }
                        else {
                            $(".filterList").hide();
                            $('#pending').html("");
                            $('#pending').html(result);
                            $('#pending').removeClass('hide');
                            $('#assetAllocList').addClass('hide');
                            //$('#PendingRecommendationsPartial').addClass('hide');

                        }
                    }
                    hideSpinner();
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
            url: '/DataBank/AssetAllocation/ApplyFilter',
            dataType: 'html',
            data: $('#BasicFilter').serialize() + "&AssetCategoryCode=" + AssetCategoryCode,
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    if (tabIndex != 1001) {

                        $('#assetAllocList').html("");
                        $('#assetAllocList').html(result);

                        $('#assetAllocList').removeClass('hide');
                        $('#pending').addClass('hide');



                    }
                    else {
                        $('#pending').html("");
                        $('#pending').html(result);
                        $('#pending').removeClass('hide');
                        $('#assetAllocList').addClass('hide');
                    }
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

function onFilterDropdownChange(SearchId) {
    debugger;
    showSpinner();
    var url = "/DataBank/AssetAllocation/AssetTabs";
    //$(".all").addClass("active");
    var search = SearchId.value;
    searchName = SearchId.text;
    searchid = SearchId.value;
    TabIndex = currentTab;
    $('#AssetAllocationFilter_Code').val(search);
    $('.filterDropDown').val(search);
    $('#pending').addClass('hide');
    $("#Tabindex").val(currentTab);
    $("#SearchId").val($('#AssetAllocationFilter_Code').val())
    if (search == 1) //Allocated
    {        
        $('#assetAllocList').removeClass('hide');        
        //$('#assetAllocList').load(url, { TabIndex: TabIndex, SearchId: search }, function () { });    
        ApplyFilter('apply', $('#AssetCategoryCode').val());
        $(".filterList").show();
    }
    
    if (search == 2) //Asset In Stock
    {
        $('#assetAllocList').removeClass('hide');
        //$('#assetAllocList').load(url, { TabIndex: TabIndex, SearchId: search }, function () { });
        ApplyFilter('apply', $('#AssetCategoryCode').val());
        $(".notAllocatedAssets").show();
        $(".filterList").show();
    }
    if (search == 3) //Due List
    {
        $('#assetAllocList').removeClass('hide');
        //$('#assetAllocList').load(url, { TabIndex: TabIndex, SearchId: search }, function () { });
        ApplyFilter('apply', $('#AssetCategoryCode').val());
        $(".filterList").show();
    }
    if (search == 4) //Asset Issue History
    {
        $('#assetAllocList').removeClass('hide');
        //$('#assetAllocList').load(url, { TabIndex: TabIndex, SearchId: search }, function () { });
        ApplyFilter('apply', $('#AssetCategoryCode').val());
        $(".filterList").show();
    }
    if (search == 5) //Damaged
    {
        $('#assetAllocList').removeClass('hide');
        //$('#assetAllocList').load(url, { TabIndex: TabIndex, SearchId: search }, function () { });
        ApplyFilter('apply', $('#AssetCategoryCode').val());
        $(".filterList").show();
    }
}
function OnEmpChange(sender) {
    debugger;
    var Code = $(sender).val();
    if (Code != 0) {
        $.ajax({

            type: 'GET',
            url: '/DataBank/AssetAllocation/EmployeeCard',
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



function onEmployeeClick(AssetAllocationGI,EmployeeGI,Tab) {
    debugger;
    if (Tab == 'All') {      
        $.ajax({

            type: 'GET',
            url: '/DataBank/AssetAllocation/AssetAllocationDetails',
            dataType: 'html',
            data: { AssetAllocationGI: AssetAllocationGI, EmployeeGI: EmployeeGI },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('#assetaloc').html("");
                    $('#assetaloc').html(result);
                }


            },
            error: function (xhr, textStatus, errorThrown) {
                debugger
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);


            }
        });
        $('#assetaloc').removeClass('hide');        
    }
    else if (Tab == 'Pending') {   
        $.ajax({

            type: 'GET',
            url: '/DataBank/AssetAllocation/Issue',
            dataType: 'html',
            data: { AssetAllocationGI: AssetAllocationGI, EmployeeGI: EmployeeGI },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('#issue').html("");
                    $('#issue').html(result);
                }


            },
            error: function (xhr, textStatus, errorThrown) {
                debugger
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);


            }
        });        
    }
    else if (Tab == 'History') {
        $.ajax({

            type: 'GET',
            url: '/DataBank/AssetAllocation/AssetIssueHistoryDetails',
            dataType: 'html',
            data: { AssetAllocationGI: AssetAllocationGI, EmployeeGI: EmployeeGI },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('.issueHistory').html("");
                    $('.issueHistory').html(result);
                }


            },
            error: function (xhr, textStatus, errorThrown) {
                debugger
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);


            }
        });
        $('#assetIssueHistoryDetails').removeClass('hide');
        $('.issueHistory').show();
    }
    
}
function onEmployeeAssetReqClick(EmployeeGI, AssetCategoryCode,TrnCode) {
    debugger;

    $.ajax({

        type: 'GET',
        url: '/DataBank/AssetAllocation/EmployeeAssetReqDetails',
        dataType: 'html',
        data: { EmployeeGI: EmployeeGI, AssetCategoryCode: AssetCategoryCode, TrnCode: TrnCode},
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('body').append('<div class="form-overlay"></div>');
                $('#assetaloc').html("");
                $('#assetaloc').html(result);
            }


        },
        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);


        }
    });
    $('#assetaloc').removeClass('hide');
}

function onAssetClick(AssetCode) { 
    $.ajax({

        type: 'GET',
        url: '/DataBank/AssetAllocation/GetAssetDetails',
        dataType: 'html',
        data: { AssetCode: AssetCode },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('body').append('<div class="form-overlay"></div>');
                $('.assetDetails').html("");
                $('.assetDetails').html(result);
            }


        },
        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);


        }
    });
    $('#assetDetailsSlider').removeClass('hide');
    $('.assetDetails').show();
}
function onNewRequestClick() {
    debugger;  
    $('body').append('<div class="form-overlay"></div>');
    $('#newRequestSlider').removeClass('hide');
    $('.add-assets').show();
}





function validateDueDate(val, id) {
    debugger;
    if ($(val).val() == "") {
        $(val).addClass('is-invalid');
        $("#" + id + "").addClass('error-message');
        $("#" + id + "").html("This field is required");
    }
    else {
        
        var issueDate = new Date($('#AssetRequest_AssetIssueDate').val());
        var dueDate = new Date($(val).val());
        var d = new Date();
        var iDate = new Date(issueDate.toLocaleDateString());
        var dDate = new Date(dueDate.toLocaleDateString());

        if (iDate > dDate) {
            $(val).addClass('is-invalid');
            $("#" + id + "").addClass('error-message');
            $("#" + id + "").html("Due date should be greater than issued date");
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

function validateReturnDate(val, id) {
    debugger;
    if ($(val).val() == "") {
        $(val).addClass('is-invalid');
        $("#" + id + "").addClass('error-message');
        $("#" + id + "").html("This field is required");
    }
    else {        

        var issueDate = new Date($('#AssetRequest_AssetIssueDate').val());
        var dueDate = new Date($(val).val());
        var currDate = new Date();
        if (issueDate > dueDate) {
            $(val).addClass('is-invalid');
            $("#" + id + "").addClass('error-message');
            $("#" + id + "").html("Return date should be greater than issued date");
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

function fillEmployeeDropdown() {
    debugger;
    $.getJSON("/DataBank/AssetAllocation/GetEmployees", {}, function (data) {
        debugger;
        $('#EmpId option').remove();
        $('#EmpId').append('<option value disabled selected hidden>--Select--</option');

        for (var i = 0; i < data.length; i++) {
            $('#EmpId').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
        }

    });
}

function onAssetLogClick(AssetAllocationGI) {
    debugger;
    var Code = 1;
    var EmployeeGI = $('#EmployeeGI').val();
    $.ajax({

        type: 'GET',
        url: '/DataBank/AssetAllocation/AssetLog',
        dataType: 'html',
        data: { AssetAllocationGI: AssetAllocationGI, EmployeeGI: EmployeeGI },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('body').append('<div class="form-overlay"></div>');
                $('.assethistory').html("");
                $('.assethistory').html(result);
            }


        },
        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);


        }
    });
    $('#assetlog').removeClass('hide');
    $('.assethistory').show();
}




function onAssetCategoryChange(sender,id) {

    debugger
    var cid = $(sender).val();
    if (cid == "") {
        $("#" + id + "").addClass('error-message');
        $(sender).addClass('is-invalid');
        $("#" + id + "").html("This field is required");
    }
    else {
        $("#" + id + "").removeClass('error-message');
        $(sender).removeClass('is-invalid');
        $("#" + id + "").html("");
    }
    $.getJSON("/DataBank/AssetAllocation/GetAsset", { AssetCategoryCode: cid }, function (data) {

        $('#AssetRequest_AssetName_Code option').remove();
        $('#AssetRequest_AssetName_Code').append('<option value>--Select Asset Name--</option');

        for (var i = 0; i < data.length; i++) {
            $('#AssetRequest_AssetName_Code').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
        }

    });
}
function onAssetChange(sender,id) {
    debugger;
    var cid = $(sender).val();
    if (cid == 0) {
        $("#" + id + "").addClass('error-message');
        $(sender).addClass('is-invalid');
        $("#" + id + "").html("This field is required");
    }
    else {
        $("#" + id + "").removeClass('error-message');
        $(sender).removeClass('is-invalid');
        $("#" + id + "").html("");

        $.getJSON("/DataBank/AssetAllocation/GetAssetDescription", { AssetCode: cid }, function (data) {
            debugger;
            console.log(data);
            console.log(data.assetRequest.asset);
            $('.assetcode').html(data.assetRequest.asset.assetCode);
            var dop = formatDate(data.assetRequest.asset.dateOfPurchase);
            $('.dop').html(dop);
            $('.brd').html(data.assetRequest.asset.brand);
            $('.modelno').html(data.assetRequest.asset.modelNo);
            $('.cost').html(data.assetRequest.asset.cost);
            $('.issuingdept').html(data.assetRequest.asset.issuingDepartment.name);
            $('.condition').html(data.assetRequest.asset.condition.name);
            $('.spec').html(data.assetRequest.asset.specification);
        });
    }
}
function formatDate(dateObject) {
    debugger;

    const monthNames = ["Jan", "Feb", "Mar", "Apr", "May", "Jun",
        "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"
    ];


    var d = new Date(dateObject);
    var day = d.getDate();
    var month = monthNames[d.getMonth()];  //d.getMonth() + 1;
    var year = d.getFullYear();
    if (day < 10) {
        day = "0" + day;
    }
    if (month < 10) {
        month = "0" + month;
    }
    var date = day + "-" + month + "-" + year;

    return date;
};

function onDeleteAssetAllocation(AssetAllocationGI) {
    swal.fire({
        text: "Do you want to delete?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            url = "/DataBank/AssetAllocation/AssetAllocationDelete?AssetAllocationGI=" + AssetAllocationGI;
            window.location.href = url;
        }
    });
}
function onExportToExcelClick() {
    debugger;
    var searchid = $('#AssetAllocationFilter_Code').val();
    searchName = $('#AssetAllocationFilter_Code option:selected').text();
    var url = "/DataBank/AssetAllocation/ExportToExcel?TabIndex=" + currentTab + "&SearchId=" + searchid + "&TabName=" + currentTabName + "&SearchName=" + searchName + '&data=' + $('#BasicFilter').serialize();

    if (currentTab == 1001) {
        var pendingTableLength = $('#AssetAllocationList_Count').val();
        if (pendingTableLength  !="") {           
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
    }
    else if (searchid == 1) {
        var assetAllocationTableLength = $('#AssetAllocationList_Count').val();
        if (assetAllocationTableLength != "") {            
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
    }
    else if (searchid == 2) {
        var stockTableLength = $('#AssetList_Count').val();
        if (stockTableLength != "") {            
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
    }
    else if (searchid == 3) {
        var dueTableLength = $('#AssetAllocationList_Count').val();
        if (dueTableLength != "") {            
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
    }
    else if (searchid == 4) {
        var historyTableLength = $('#AssetAllocationList_Count').val();
        if (historyTableLength != "") {            
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
    }
    else if (searchid == 5) {
        var damagedTableLength = $('#AssetList_Count').val();
        if (damagedTableLength != "") {            
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
    }    
}


function validateAll() {
   
    var i = 0;
    if (!validateById('#AssetRequest_Asset_AssetCategory_Code', 'assetCategory')) {
        i++;
    }
    if (!validateById('#AssetRequest_AssetName_Code', 'assetName')) {
        i++;
    }
    if (!validateById('#AssetRequest_AssetIssueDate', 'assetIssueDate')) {
        i++;
    }
    if (!validateById('#AssetRequest_AssetDueDate', 'assetDueDate')) {
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
                    url: '/DataBank/AssetAllocation/Create',
                    dataType: 'json',
                    data: $('#assetAllocSave').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire("Saved Successfully").then(function () {
                                CloseSlider('addBenchmarkSalarySlider');
                                var url = "/DataBank/AssetAllocation/Index";
                                window.location.href = url;

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

    //if (!RemarksLimitCheck('AssetRequest_Remarks')) 
    //    {
    //        return false;
    //    }
    //else {
    //        return true;
        
    //}
    //if (!RemarksLimitCheck('AssetRequest_Remarks')) 
    //    {
    //        return false;
    //    }
    //else {
    //        return true;
    //    }
    
}


function IssueAsset() {
    swal.fire({
        text: "Do you want to Issue?",
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
                url: '/DataBank/AssetAllocation/IssueSave',
                dataType: 'json',
                data: $('#AssetAllocIssue').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire("Issued Successfully").then(function () {
                            CloseSlider('issuepending');
                            ApplyFilter('apply');

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
}

function validateAllOnReturn() {
    var i = 0;
    if (!validateReturnDate('#AssetRequest_AssetReturnDate', 'assetReturnDte'))
        i++;
    if (i == 0)
        swal.fire({
            text: "Do you want to Update?",
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
                    url: '/DataBank/AssetAllocation/AssetAllocationDetailsSave',
                    dataType: 'json',
                    data: $('#assetAllocReturn').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire("Updated Successfully").then(function () {
                                CloseSlider('edit-assets');
                                ApplyFilter('apply');

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
}

function confirmApproveClick() {
    debugger
    if (validateAsset()) {
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
                    url: '/DataBank/AssetAllocation/Approve',
                    dataType: 'json',
                    data: $('#assetAllocReq').serialize(),
                    success: function (result) {

                        if (result.success == true) {
                            debugger
                            swal.fire("Approved Successfully").then(function () {
                              
                                var url = "/DataBank/AssetAllocation/Index";
                            window.location.href = url;
                        });

            }

            else {
                debugger;
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
    }
    else {
    return false;
}
}


function confirmRejectClick() {
    debugger
    {
        swal.fire({
            text: "Do you want to Reject?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({

                    type: 'POST',
                    url: '/DataBank/AssetAllocation/Reject',
                    dataType: 'json',
                    data: $('#assetAllocReq').serialize(),
                    success: function (result) {

                        if (result.success == true) {
                            debugger
                            swal.fire("Rejected Successfully").then(function () {

                                var url = "/DataBank/AssetAllocation/Index";
                                window.location.href = url;
                            });

                        }

                        else {
                            debugger;
                            swal.fire(result.message).then(function () {

                            });
                        }

                    },
                    error: function (xhr, textStatus, errorThrown) {
                        if ($($.parseHTML(xhr.responseText)).filter(".ESSP_LoginSession").length > 0) {
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
    
    




function validateAsset() {
    var i = 0;
    if (!validateById('#Asset_Code', 'assetname')) {
        i++;
    }

    if (i == 0)
        return true;
    else
        return false;
}





