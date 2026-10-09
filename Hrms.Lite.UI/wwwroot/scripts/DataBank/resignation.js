var prevTab = "tab1";
var currentTab = 0;
var currentTabName = "In Notice Period";
function tabClick(Tab, TabIndex,TabName) {
    debugger;
    showSpinner();
    currentTab = TabIndex;
    currentTabName = TabName;
    $('#TabCode').val(TabIndex);
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");
    //var url = "/DataBank/Resignation/GetResignationList";
    //$('#resignationList').load(url, { TabIndex: TabIndex }, function () { });
    ApplyFilter('apply');
    prevTab = Tab;
}
function TabsDropdown(TabIndex, TabName) {
    debugger;
    currentTab = TabIndex;
    currentTabName = TabName;
    var Tab = GetTab($("#TabCode").prop('selectedIndex'));
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");
    //var url = "/DataBank/Resignation/GetResignationList";
    //$('#resignationList').load(url, { TabIndex: TabIndex }, function () { });
    ApplyFilter('apply');
    prevTab = Tab;
}

function calculateLWD() {
    debugger;
    var rDate = new Date($('#ResignationDetails_ResignationDate').val());
    var nDays = 0;
    if ($('#NoticePeriodDetails_0__InDays').val() != '')
        nDays = parseInt($('#NoticePeriodDetails_0__InDays').val());
    rDate.setDate((rDate.getDate() + nDays)-1);
    var lDate = _GetFormattedDate(rDate.toString('yyyy-MM-dd'));    
    $('#ResignationDetails_LastWorkingDate').val(lDate);
    $('#ResignationDetails_LastWorkingDate').datepicker('setDate', lDate);
}

function GetTab(TabCode) {
    if (TabCode == 0)
        return 'tab1'
    else if (TabCode == 1)
        return 'tab2';
    else if (TabCode == 2)
        return 'tab3';
    else if (TabCode == 3)
        return 'tab4';
    else if (TabCode == 4)
        return 'tab5';
    else if (TabCode == 5)
        return 'tab6';
    else if (TabCode == 6)
        return 'tab7';
   
}

function onAddNewClick() {
    debugger;
    var url = "/DataBank/Resignation/AddNew";
    $('#addNewResignation').load(url, {}, function () { });
    $('#addNewResignation').removeClass('hide');
}

function OnEmpChange(sender) {
    debugger;
    var Code = $(sender).val();
    if (Code != 0) {
        var url = "/DataBank/Resignation/EmployeeCard";
        $('.search-profile-details').load(url, { EmpCode: Code }, function () { });
        $('.search-profile-details').show(300);
    }

}

function onResignationTypeChange(sender) {

    debugger;
    var cid = $(sender).val();  
    $.getJSON("/DataBank/Resignation/GetResignationReason", { id: cid }, function (data) {

        $('#ResignationDetails_ResignationReason_Code option').remove();
        $('#ResignationDetails_ResignationReason_Code').append('<option value>--Select Reason--</option');

        for (var i = 0; i < data.length; i++) {
            $('#ResignationDetails_ResignationReason_Code').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
        }

    });
}

function CheckImageExtensionSize(e) {
    debugger;
    console.log(e);
    var size = $(e)[0].files[0].size;
    var filename = $(e)[0].files[0].name;
    var fileExtension = ['jpeg', 'jpg', 'png', 'bmp','pdf'];
    console.log(size);
    if (size > 1024 * 1024) {
        swal.fire('File size cannot exceeds 1 Mb');
        $(e).val('');
    }
    else if ($.inArray($(e).val().split('.').pop().toLowerCase(), fileExtension) == -1) {
        swal.fire("Only pdf, jpg, jpeg, png and bmp formats are allowed");
        $(e).val('');
    }

}

function ViewFile(absoluteURI) {
    //window.open(absoluteURI, "_blank");
    
    if (absoluteURI == "") {
        swal.fire("No file Uploaded")
    }
    else {
        window.open(absoluteURI);
    }
}


function PreviewImage() {
    var path = URL.createObjectURL($('.previewImage')[0].files[0]);
    window.open(path, "_blank");
}

function NoticePeriodInDays(val,spanId) {
    debugger;
    //const date1 = new Date($('#ResignationDetails_ResignationDate').val());
    //const date2 = new Date($('#ResignationDetails_LastWorkingDate').val());
    //const diffTime = Math.abs(date2 - date1);
    //const diffDays = Math.ceil(diffTime / (1000 * 60 * 60 * 24));
    //alert(diffDays + " days");
    validate(val, spanId);
    validateLwd();
    var RDate = $('#ResignationDetails_ResignationDate').val();
    var LWD = $('#ResignationDetails_LastWorkingDate').val();
    var EmployeeGI = $('#EmployeeGI').val();

    $.getJSON("/DataBank/Resignation/GetNoticePeriodDetails", { EmployeeGI: EmployeeGI, ResignationDate: RDate, LastWorkingDate: LWD }, function (data) {        
        $('#noticePeriodDescription1').html(data.noticePeriodDetails[0].description);
        $('#noticePeriodDays1').html(data.noticePeriodDetails[0].inDays);
        $('#noticePeriodDescription2').html(data.noticePeriodDetails[1].description);
        $('#noticePeriodDays2').html(data.noticePeriodDetails[1].inDays);
    });
}

function validateLwd() {
    debugger;
    var LastWrkngDate =new Date($('#ResignationDetails_LastWorkingDate').val());
    var resDate = new Date($('#ResignationDetails_ResignationDate').val());
   
    var d = new Date();
    future=new Date(d.getTime() +30*24*60*60*1000); 

    if (lgCmCode.value != 'RHET') {
        if (LastWrkngDate < resDate) {
            swal.fire("Last working date should be greater than Resignation Date");
            return false;
        } else {


            if (LastWrkngDate > future) {
                swal.fire("Last working date is 30 days after the current date."); return false;

            } else {
                return true;
            }
        }
    }
    else {
        return true;
    }
    
}

function validateRelieving() {
     if (!validateById('#ResignationDetails_LastWorkingDate', 'RLwd')) {
        return false;
    }
    else if (!validateLwd()) {
        return false;
     }
     else if (!validateById('#ResignationDetails_WorkHandOverDetails', 'Whd'))
     {
         return false;
     }
    else {
        return true;
    }
}
function onSkipClick(colchkid, tableid) {
    debugger
    if ($('#' + colchkid).is(':checked')) {
        $('.DefaultSettings').prop('checked', false);
        $('.DefaultSettings').attr('disabled', true);        
        $('.ResignResponsible').multiselect('disable');
    }
    else {
        $('.DefaultSettings').prop('checked', true);
        $('.DefaultSettings').attr('disabled', false);        
        $('.ResignResponsible').multiselect('enable');
    }
}
function onEmpRelievingClick(employeeGI, resignationGI,status)
{
    debugger
    if (status == 'pending')
    {
        swal.fire('Please complete the dues clearance to relieve the employee');
       
    }
    else {
        var url = '/DataBank/Resignation/GetEmployeeRelieving?EmployeeGI=' + employeeGI + '&ResignationGI=' + resignationGI;
        window.location.href = url;
    }
}
function GetResignationTracker(ResignationGI, EmployeeGI) {
    debugger;
    $.ajax({

        type: 'GET',
        url: '/DataBank/Resignation/GetRelievingTracker',
        dataType: 'html',
        data: { ResignationGI: ResignationGI, EmployeeGI: EmployeeGI },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {
                $('body').append('<div class="form-overlay"></div>');
                $('#ResignationTrackerSlider').html("");
                $('#ResignationTrackerSlider').html(result);
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
function validateDues()
{
    debugger
    if (!$('.atleastonecheck').is(':checked')) {
        var checked = $('.DefaultSettings:checked').length;
        if (checked == 0) {
            swal.fire("Please Select atleast one parameter");
            flag = 1;
        }
        }
    flag = 0;
    $(".DefaultSettings").each(function (index, object) {
        debugger;
        if ($("#DueClearanceSettings_" + index + "__DefaultSettings").is(':checked'))
        {
          
            var count = $("#DueClearanceSettings_" + index + "__ResponsiblePersons :selected").length;
            if (count == 0)
            {
                  var parm = $("#DueClearanceSettings_" + index + "__ParameterName").val();
                swal.fire("Please Select atleast one Authority for the Parameter : " + parm);
                flag = 1;
            }
        }
            
    });
    if (flag == 1)
        return false;
    else
        return true;
}
function validateApproveAcceptance() {
    debugger
    if (validateDues()) {
        debugger
        if (!validateById('#ResignationDetails_AcceptanceRemarks', 'RAcceptanceRemarks')) {
         
            return false;
        }      
        else {
          
            swal.fire({
                text: "Do you want to Accept?",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Ok'
            }).then((result) => {
                if (result.isConfirmed) {
                    $.ajax({
                        type: 'POST',
                        url: '/DataBank/Resignation/Approve',
                        dataType: 'json',
                        data: $('#resignationAcceptanceform').serialize(),
                        success: function (result) {
                            if (result.success == true) {
                                swal.fire("Accepted Successfully").then(function () {
                                    var url = "/DataBank/Resignation/Index";
                                    window.location.href = url;
                                });
                            }
                            else {
                                swal.fire(result.message).then(function () {

                                });
                            }
                        },
                        error: function (xhr, textStatus, errorThrown) {
                            swal.fire(xhr.responseText);
                        }
                    });
                }
            });
        }
    }
}

function validateRejectAcceptance() {
    if (!validateById('#ResignationDetails_AcceptanceRemarks', 'RAcceptanceRemarks')) {
        return false;
    }
    else {
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
                    url: '/DataBank/Resignation/Reject',
                    dataType: 'json',
                    data: $('#resignationAcceptanceform').serialize(),
                    success: function (result) {
                        if (result.success == true) {                           
                            swal.fire("Rejected Successfully").then(function () {
                                var url = "/DataBank/Resignation/Index";
                                window.location.href = url;
                            });
                        }                       
                        else {
                            swal.fire(result.message).then(function () {

                            });
                        }

                    },
                    error: function (xhr, textStatus, errorThrown) {
                        swal.fire(xhr.responseText);
                    }
                });
            }
        });
        
        
    }
}

/*#region exitchecklist*/
function validatestatus() {
    debugger 
    status = true;
    flag = 0;
    try {
        $(".ChecklistStatus").each(function (index, object) {

            if ($("#ExitCheckList_" + index + "__IsMandatory").val() == "True") {


                if ($("#ExitCheckList_" + index + "__ExitCheckStatus_Code").val() != 1) {                  
                    swal.fire("Please complete the mandatory items in the checklist to proceed further");
                    flag = 1;
                    throw new Error()
                }
            }

        });
    }
    catch (error) {
        debugger
        if (flag == 1) {

            status = false;
            return status;
        }
    }
    return status;
}
function onstatuschange(index, spanId) {
    debugger
    if ($("#ExitCheckList_" + index + "__IsMandatory").val() == 'True') {
        if ($("#ExitCheckList_" + index + "__ExitCheckStatus_Code").val() != 1) {
            $("#ExitCheckList_" + index + "__ExitCheckStatus_Code").addClass('is-invalid');
            $("#" + spanId + index).addClass('error-message');
            $("#" + spanId + index).html("This field is required");
            return false;
        } else {
            $("#ExitCheckList_" + index + "__ExitCheckStatus_Code").removeClass('is-invalid');
            $("#" + spanId + index).removeClass('error-message');
            $("#" + spanId + index).html("");
            //trim(val);        
            return true;
        }
    }
}
function ExitCheckListSave() {
    if (validatestatus() != "false") {
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
                    url: '/DataBank/Resignation/ExiCheckListSave',
                    dataType: 'json',
                    data: $('#resignChecklist').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire("Saved Successfully").then(function () {
                                var url = "/DataBank/Resignation/Index";
                                window.location.href = url;
                            });
                        }
                        else {
                            swal.fire(result.message).then(function () {

                            });
                        }

                    },
                    error: function (xhr, textStatus, errorThrown) {
                        swal.fire(xhr.responseText);
                    }
                });
            }
        });
    }
}
/*#end checklist*/
function Relieve(Mode) {
    var i = 0;
    if (!validateLwd())
        i++;
    if (Mode == "Approve") {
        txt = "If the attendance is updated after the selected last working the same will be deleted. Do you want to continue?";
        message = "Relieved Successfully";
    }
    else {
        txt = "Do you want to Reject ?";
        message = "Rejected Successfully";

    }
    if (i == 0) {    
        swal.fire({
            //text: "Do you want to Relieve?",
        
            text: txt,
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({

                    type: 'POST',
                    url: '/DataBank/Resignation/EmployeeRelieveSave?Mode=' + Mode,
                    dataType: 'json',
                    data: $('#empRelieve').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(message).then(function () {
                                var url = "/DataBank/Resignation/Index";
                                window.location.href = url;
                            });
                        }
                        else {
                            swal.fire(result.message).then(function () {

                            });
                        }

                    },
                    error: function (xhr, textStatus, errorThrown) {
                        swal.fire(xhr.responseText);
                    }
                });
            }
        });
    }
}
























function onLwdClick() {
    $('#ResignationDetails_LastWorkingDate').datepicker('show');
    //$('#ResignationDetails_LastWorkingDate').removeClass('kt_datepicker_2');
    //$('#ResignationDetails_LastWorkingDate').addClass('kt_datepicker_8');

}


//function validateAll() {
//    if (!validateById('#ResignationDetails_ResignationType_Code', 'RType')) {
//        return false;
//    }
//    else if (!validateById('#ResignationDetails_ResignationReason_Code', 'RReason')) {
//        return false;
//    }
//    else if (!validateById('#ResignationDetails_ResignationDate', 'RDate')) {
//        return false;
//    }
//    else if (!validateById('#ResignationDetails_LastWorkingDate', 'RLwd')) {
//        return false;
//    }
//    else if (!validateLwd())
//    {
//        return false;
//    }
//    else if (!validateById('#ResignationDetails_Remarks', 'RRemarks')) {
//        return false;
//    }
//    else {
//        return true;
//    }
//}


function validateAll() {
    var i = 0;
    if (!validateById('#ResignationDetails_ResignationType_Code', 'RType')) {
        i++;
    }
    if (!validateById('#ResignationDetails_ResignationReason_Code', 'RReason')) {
        i++;
    }
    if (!validateById('#ResignationDetails_ResignationDate', 'RDate')) {
        i++;
    }
    if (!validateById('#ResignationDetails_LastWorkingDate', 'RLwd')) {
        i++;
    }
    //if (!validateById('#ResignationDetails_Remarks', 'RRemarks')) {
    //    i++;
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
                var MPPersonalInfo = new FormData($('#ResignationSave')[0]);
                $.ajax({
                    type: 'POST',
                    url: "/DataBank/Resignation/Create",
                    contentType: false,
                    processData: false,
                    dataType: 'json',
                    data: MPPersonalInfo,
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire("Saved Successfully").then(function () {

                                var url = "/DataBank/Resignation/Index";
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
        });
    else
        return false;
}



function onExportToExcelClick() {
    debugger;
    if (currentTab == 0)
    {
        var inNoticePeriodTableLength = $('#ResignationList_Count').val();
        if (inNoticePeriodTableLength != "") {
            url = "/DataBank/Resignation/ExportToExcel?TabIndex=" + currentTab + "&TabName=" + currentTabName + '&data=' + $('#BasicFilter').serialize();;
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
    }
    else if (currentTab == 1)
    {
        var resignationAcceptTableLength = $('#ResignationList_Count').val();
        if (resignationAcceptTableLength != "") {
            url = "/DataBank/Resignation/ExportToExcel?TabIndex=" + currentTab + "&TabName=" + currentTabName + '&data=' + $('#BasicFilter').serialize();;
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
    }
    else if (currentTab == 2)
    {
        var resignationExitChecklistTableLength = $('#ResignationList_Count').val();
        if (resignationExitChecklistTableLength != "") {
            url = "/DataBank/Resignation/ExportToExcel?TabIndex=" + currentTab + "&TabName=" + currentTabName + '&data=' + $('#BasicFilter').serialize();;
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
    }
    else if (currentTab == 3)
    {
        var resignationRelivingTableLength = $('#ResignationList_Count').val();
        if (resignationRelivingTableLength != "") {
            url = "/DataBank/Resignation/ExportToExcel?TabIndex=" + currentTab + "&TabName=" + currentTabName + '&data=' + $('#BasicFilter').serialize();;
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
        }
    else
    {
        var resignationRetirementTableLength = $('#ResignationList_Count').val();
        if (resignationRetirementTableLength != "") {
            url = "/DataBank/Resignation/ExportToExcel?TabIndex=" + currentTab + "&TabName=" + currentTabName + '&data=' + $('#BasicFilter').serialize();;
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
        
    }
}


function ApplyFilter(mode) {
    debugger
    var tabIndex = $('#Tabindex').val();
    var search = $('#SearchId').val();
    $("#Tabindex").val(currentTab);
    $("#SearchId").val($('#FilterList_Code').val())
    if (mode == 'apply') {
        if (filterApplicable >= 1) {
            $.ajax({

                type: 'POST',
                url: '/DataBank/Resignation/ApplyFilter',
                dataType: 'html',
                data: $('#BasicFilter').serialize(),
                success: function (result) {
                    debugger
                    CloseSlider('basicFilter');
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();

                    }
                    else {
                        $('#resignationList').html("");
                        $('#resignationList').html(result);
                        $('#resignationList').removeClass('hide');
                    }
                    hideSpinner();
                           
                },


                error: function (xhr, textStatus, errorThrown) {
                    swal.fire(xhr.responseText);
                }
            });
            $('#filterForm').removeClass('filter-apply');
        }
        else {
            showSpinner();
            $.ajax({

                type: 'POST',
                url: '/DataBank/Resignation/ApplyFilter',
                dataType: 'html',
                data: $('#BasicFilter').serialize(),
                success: function (result) {
                    debugger
                    CloseSlider('basicFilter');
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();

                    }
                    else {
                        $('#resignationList').html("");
                        $('#resignationList').html(result);
                        $('#resignationList').removeClass('hide');
                    }
                    hideSpinner();
                         
                },


                error: function (xhr, textStatus, errorThrown) {
                    swal.fire(xhr.responseText);
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
            url: '/DataBank/Resignation/ApplyFilter',
            dataType: 'html',
            data: $('#BasicFilter').serialize(),
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();

                }
                else {
                    $('#resignationList').html("");
                    $('#resignationList').html(result);
                    $('#resignationList').removeClass('hide');
                }
              
                hideSpinner();
            },
            error: function (xhr, textStatus, errorThrown) {
                swal.fire(xhr.responseText);
            }
        });
    }
}



function GetResignationAmendmentDetails(EmployeeGI, ResignationGI) {
    debugger;
    $.ajax({
        type: 'GET',
        url: '/DataBank/Resignation/GetResignationAmendmentDetails',
        dataType: 'html',
        data: { EmployeeGI: EmployeeGI, ResignationGI: ResignationGI },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('body').append('<div class="form-overlay"></div>');
                $('#ResignationAmendmentSlider').html("");
                $('#ResignationAmendmentSlider').html(result);
            }


        },
        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);


        }
    });
}

function ResignationAmendmentSave(ResignationGI) {
    AmendRemark = $('#ResignationAmendmentDetails_AmendmentRemark').val();
    debugger;
    $.ajaxSetup({
        async: false
    });
    /* var Reason = $('#ResignationDeleteDetails_Reason').val();*/
    swal.fire({
        text: "Do you want to continue to bypass Dues clearance & Exit Checklist & NOC?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            showSpinner();
            $.ajax({

                type: 'POST',
                url: '/DataBank/Resignation/SaveResignationAmendment',
                dataType: 'json',
                data: $('#ResignationAmendment').serialize(),
                success: function (result) {
                    debugger;
                    if (result.success) {
                        swal.fire(result.message).then(() => {
                            CloseSlider('ResAmendSlider');
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
    });

}
