var prevTab = 'HR_All';
var currentTab = "ALL";  //code for 'all'
var pagenumber = 0;

function ChkBxChecking() {
    debugger    
    var checkedCount = $('#HRAttendanceApprovalTable').find('input[type=checkbox]:checked').length;
    var totalRows = $('#HRAttendanceApprovalTable tbody tr').length;
    //$('#HRAttendanceApprovalTableTwo tbody tr').remove();   
    if (totalRows != checkedCount) {
        //alert('1', totalRows, checkedCount);
        $('.selectAllChkBx').html('')
        $('.selectAllChkBx').html('<label class="checkbox checkbox-outline checkbox-success"><input data-val="true" value="true" id="SelectAll" name="SelectAll" type="checkbox" onchange="onSelectAllClick()"><span cursorshover="true"></span></label><label class="checkbox checkbox-outline checkbox-success"></label>')
    }
    else {
        //alert('2', totalRows, checkedCount);
        //$('.selectAllChkBx').html('')
        //$('.selectAllChkBx').html('<label class="checkbox checkbox-outline checkbox-success"><input data-val="true" value="true" id="SelectAll" name="SelectAll" type="checkbox" onchange="onSelectAllClick()"><span cursorshover="true"></span></label>')
        $('#SelectAll').attr('checked', true)
    }
}

function onCheckboxChange(sender, index) {
   
    debugger;
    checked = $(sender).is(':checked');
    
    if (checked) {
        var row = $(sender).closest('tr').html();
        var empcode = $('#HRAttendanceApprovalList_' + index + '__EmployeeCode').val();
        var statustype = $('#HRAttendanceApprovalList_' + index + '__AttendanceStatusTypeCode').val();
        var GI = $('#HRAttendanceApprovalList_' + index + '__LeaveApplicationGI').val();

        $('#HRAttendanceApprovalTableTwo tbody').append('<tr id="otSave_' + pagenumber+'_' + index + '">' + row + '</tr>');

        $('#HRAttendanceApprovalList_' + index + '__EmployeeCode').val(empcode);
        $('#HRAttendanceApprovalList_' + index + '__LeaveApplicationGI').val(GI);
        $('#HRAttendanceApprovalList_' + index + '__AttendanceStatusTypeCode').val(statustype);
        $('#HRAttendanceApprovalList_' + index + '__Applicable').attr('checked', true);

        var checkedCount = $('#HRAttendanceApprovalTable').find('input[type=checkbox]:checked').length;
        var totalRows = $('#HRAttendanceApprovalTable tbody tr').length;

        if (totalRows == checkedCount) {           
            $('#SelectAll').attr('checked', true)
        }
        
    }
    else {

        $('#otSave_' + pagenumber + '_' + index).remove();
        $('.selectAllChkBx').html('')
        $('.selectAllChkBx').html('<label class="checkbox checkbox-outline checkbox-success"><input data-val="true" value="true" id="SelectAll" name="SelectAll" type="checkbox" onchange="onSelectAllClick()"><span cursorshover="true"></span></label><label class="checkbox checkbox-outline checkbox-success"></label>')

    }

    $('#HRAttendanceApprovalTableTwo tbody').find("tr").each(function (currindex) {
        debugger;
        // if (currindex > 0) {
        $(this).find("td").eq(2).html(currindex);
        debugger
        var tdd = $(this).find("td").eq(2);
        tdd.prevObject[1].getElementsByTagName('input')[0].id = 'HRAttendanceApprovalList_' + currindex + '__EmployeeCode';
        tdd.prevObject[1].getElementsByTagName('input')[0].name = 'HRAttendanceApprovalList[' + currindex + '].EmployeeCode';
        tdd.prevObject[1].getElementsByTagName('input')[1].id = 'HRAttendanceApprovalList_' + currindex + '__LeaveApplicationGI';
        tdd.prevObject[1].getElementsByTagName('input')[1].name = 'HRAttendanceApprovalList[' + currindex + '].LeaveApplicationGI';
        tdd.prevObject[1].getElementsByTagName('input')[2].id = 'HRAttendanceApprovalList_' + currindex + '__AttendanceStatusTypeCode';
        tdd.prevObject[1].getElementsByTagName('input')[2].name = 'HRAttendanceApprovalList[' + currindex + '].AttendanceStatusTypeCode';
        tdd.prevObject[10].getElementsByTagName('input')[0].id = 'HRAttendanceApprovalList_' + currindex + '__Applicable';
        tdd.prevObject[10].getElementsByTagName('input')[0].name = 'HRAttendanceApprovalList[' + currindex + '].Applicable';


        // }
    });
}

function tabClick(tabName, tabIndex) {
    debugger
  
    $("." + prevTab).removeClass("active");
    $("." + tabName).addClass("active");
    currentTab = tabIndex;
    $("#TabCode").val(tabIndex);
    $('#HRAttendanceApprovalTableTwo tbody tr').remove();
    fillData();
    $('.selectAllChkBx').html('')
    $('.selectAllChkBx').html('<label class="checkbox checkbox-outline checkbox-success"><input data-val="true" value="true" id="SelectAll" name="SelectAll" type="checkbox" onchange="onSelectAllClick()"><span cursorshover="true"></span></label><label class="checkbox checkbox-outline checkbox-success"></label>')

    prevTab = tabName;
}
function onLevelChange() {
    $('#HRAttendanceApprovalTableTwo tbody tr').remove();
    $.getJSON("/TimeOffice/HRAttendanceApproval/GetAuthority", { TabIndex: currentTab, LeavePeriodCode: 0, Level: $('#AuthorityLevelCode').val(), Month: $('#Month_Code').val() }, function (data) {

        $('#AuthorityCode option').remove();
        //$('#AuthorityCode').append('<option value="">--Select--</option');

        for (var i = 0; i < data.length; i++) {
            $('#AuthorityCode').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
        }
        $('#AuthorityCode').selectpicker('refresh');
    });
 
}


function fillData() {
    var filter1 = $('#BasicFilter').serialize();
    debugger
    var temp = filter1;
    var TabIndex = currentTab;
    var search = $('#SearchId').val();   
    var level = $("#AuthorityLevelCode").val();
    var monthCode = $('#Month_Code').val();
    var PayrollCode = $('#PayrollGroup_Code').val();
    var authority = $('#AuthorityCode').val();

 

   
    if (currentTab != "PREVIOUS_HISTORY")
    {
        var dataTable = $("#HRAttendanceApprovalTable").DataTable({

            "pagingType": 'full_numbers',
            "destroy": 'true',
            "ajax": {
                "url": "/TimeOffice/HRAttendanceApproval/GetList",
                "type": "POST",


                "datatype": "json",

                'data':

                    function (d) {
                        temp = '';


                        var start = d.start;

                        var searchValue = $('#dataTableSearch').val();
                        //console.log(d)

                        temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&Tabindex=' + TabIndex + '&level=' + level + '&AuthorityCode=' + authority + '&monthCode=' + monthCode + '&PayrollCode=' + PayrollCode +'&SearchId=' + search + '&dummy=dummy';
                        d.filter = temp;
                          d._model = $('#HR_Atten_Approval_Form').serialize();

                        return JSON.stringify(d);
                    },




                "dataSrc": function (d) {
                    $('#HRAttendanceApprovalList_Count').val(d.recordsTotal);                    


                    $('.AllCount').html(d.allCount);
                    $('.LeaveCount').html(d.leaveCount);
                    $('.OnDutyCount').html(d.onDutyCount);
                    $('.COFFCount').html(d.coffCount);
                    $('.WFHCount').html(d.wfhCount);
                    $('.MLCount').html(d.maternityLeaveCount);
                    $('.PermissionHoursCount').html(d.permissionHoursCount);
                    $('.AttendanceRegularizationCount').html(d.attendanceRegularizationCount);                  
                    $('.AuthorityChangeCount').html(d.authorityChangeCount);

                    if (currentTab == 'PermissionHours') {
                        $('#daysORHrs').html('Minutes');
                        $('#daysORHrs').attr('data-bs-toggle', 'tooltip');
                        $('#daysORHrs').attr('title', 'Permission Hour in Minutes');
                        $('[data-bs-toggle="tooltip"]').tooltip();
                    }
                    else {
                        $('#daysORHrs').html('Days');
                        $('[data-bs-toggle="tooltip"]').tooltip('dispose');
                        $('#daysORHrs').removeAttr('data-bs-toggle');
                        $('#daysORHrs').removeAttr('title');
                        $('[data-bs-toggle="tooltip"]').tooltip();
                    }

                    //console.log(d.data);

                    for (var i = 0, ien = d.data.length; i < ien; i++)
                    {
                        d.data[i].department = d.data[i].department + '<input type="hidden" id="HRAttendanceApprovalList_' + i + '__EmployeeCode' + '" name="HRAttendanceApprovalList[' + i + '].EmployeeCode" value="' + d.data[i].employeeCode + '"><input type="hidden" id="HRAttendanceApprovalList_' + i + '__LeaveApplicationGI' + '" name="HRAttendanceApprovalList[' + i + '].LeaveApplicationGI" value="' + d.data[i].leaveApplicationGI + '"><input type="hidden" id="HRAttendanceApprovalList_' + i + '__AttendanceStatusTypeCode' + '" name="HRAttendanceApprovalList[' + i + '].AttendanceStatusTypeCode" value="' + d.data[i].attendanceStatusTypeCode + '">'
                        d.data[i].employeeName = '<div class="d-flex align-items-center"><div class="name-post"><div>' + d.data[i].employeeName+'</div>' + d.data[i].id + '</div ></div>'


                        if (d.data[i].applicable != false)
                        {
                            d.data[i].applicable = '<div class="form-group"><div class="checkbox-inline" ><label class="checkbox checkbox-outline checkbox-success"><input  data-val="true" value="true"  id="HRAttendanceApprovalList_' + i + '__Applicable" name="HRAttendanceApprovalList[' + i + '].Applicable" type="checkbox" class="HRAtt_Applicability" onchange="onCheckboxChange(this,' + i + ')" checked="' + d.data[i].applicable+'"><span></span></label><label class="checkbox checkbox-outline checkbox-success"></label></div></div >'
                        }
                        else
                            d.data[i].applicable = '<div class="form-group"><div class="checkbox-inline" ><label class="checkbox checkbox-outline checkbox-success"><input  data-val="true" value="true"  id="HRAttendanceApprovalList_' + i + '__Applicable" name="HRAttendanceApprovalList[' + i + '].Applicable" type="checkbox" class="HRAtt_Applicability" onchange="onCheckboxChange(this,' + i + ')"><span></span></label><label class="checkbox checkbox-outline checkbox-success"></label></div></div >'

                        d.data[i].from = _GetFormattedDate(d.data[i].from)
                        d.data[i].to = _GetFormattedDate(d.data[i].to)



                    }
                    const myTimeout = setTimeout(ChkBxChecking, 100);
                    console.log(d.data);
                    return d.data;
                }

            },

            "columns": [
             
                { "data": "employeeName", "width": "17%" },              
                { "data": "department", "width": "12%" },
                { "data": "location", "width": "12%" },
                { "data": "type", "width": "10%" },

                { "data": "from", "width": "13%" },
                { "data": "to", "width": "13%" },
                { "data": "leaveReason", "width": "12%" },
                { "data": "noOfDays", "width": "8%" },               
                { "data": "status", "width": "11%" },
                { "data": "authority", "width": "15%" },           

                { "data": "applicable", "width": "6%" }
               


            ],






            "dom": 'rt<"bottom"><"my-wrapper"<"my-inner2"l><"my-inner1"i><"my-inner3"p>>',
            "serverSide": "true",
            "scrollY": '52vh',
            "sScrollX": "100%",
            "sScrollXInner": "100%",

            "ordering": false,
            "order": [0, "asc"],
            "processing": "true",
            "language": {
                processing: '<img width="100%" src="/images/Loader_img1 (7).gif">',
                emptyTable: '<div id="noDataFound" style="text-align: center;display: flex;align-items: center;justify-content: center;height: 45vh;"><img src="/images/nodata.png" /></div >',

                zeroRecords: '<div id="noDataFound" style="text-align: center;display: flex;align-items: center;justify-content: center;height: 45vh;"><img src="/images/nodata.png" /></div >',

                sLengthMenu: "_MENU_",
                paginate: {


                    next: '<img width="100%" src="/images/icons/Icon awesome-arrow-right-1.png" >',
                    previous: '<img src="/images/icons/Icon awesome-arrow-right.png">',
                    first: '<img src="/images/icons/Icon metro-next-1.png">',
                    last: '<img src="/images/icons/Icon metro-next.png">'




                }
            }



        });
        
        $('#HRAttendanceApprovalTable').on('page.dt', function () {
            debugger
            var info = dataTable.page.info();
            pagenumber = info.page + 1;
            
        });        
       
    }
    else {
        debugger
        var dataTable = $("#HRAttendancePrevHistoryTable").DataTable({

            "pagingType": 'full_numbers',
            "destroy": 'true',
            "ajax": {
                "url": "/TimeOffice/HRAttendanceApproval/GetList",
                "type": "POST",


                "datatype": "json",


                'data':

                    function (d) {
                        temp = '';


                        var start = d.start;

                        var searchValue = $('#dataTableSearch').val();

                        temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&Tabindex=' + TabIndex + '&level=' + level + '&SearchId=' + search + '&monthCode=' + monthCode + '&PayrollCode=' + PayrollCode + '&AuthorityCode=' + authority +'&dummy=dummy';
                        d.filter = temp;
                        d._model = $('#HR_Atten_Approval_Form').serialize();
                        return JSON.stringify(d);
                    },




                "dataSrc": function (d) {



                    $('#HRAttendanceApprovalList_Count').val(d.recordsTotal);

                    debugger;


                    $('.AllCount').html(d.allCount);
                    $('.LeaveCount').html(d.leaveCount);
                    $('.OnDutyCount').html(d.onDutyCount);
                    $('.COFFCount').html(d.coffCount);
                    $('.WFHCount').html(d.wfhCount);
                    $('.MLCount').html(d.maternityLeaveCount);
                    $('.PermissionHoursCount').html(d.permissionHoursCount);
                    $('.AttendanceRegularizationCount').html(d.attendanceRegularizationCount);
                  
                    $('.AuthorityChangeCount').html(d.authorityChangeCount);

                    for (var i = 0, ien = d.data.length; i < ien; i++) {
                       
                        d.data[i].from = _GetFormattedDate(d.data[i].from)
                        d.data[i].to = _GetFormattedDate(d.data[i].to)
                        d.data[i].employeeName = '<div class="d-flex align-items-center"><div class="name-post"><div>' + d.data[i].employeeName + '</div>' + d.data[i].id + '</div ></div>'



                    }

                    return d.data;
                }

            },

            "columns": [

              
                { "data": "employeeName", "width": "17%" },
                { "data": "designation", "width": "17%" },
                { "data": "department", "width": "12%" },
                { "data": "location", "width": "12%" },
                { "data": "type", "width": "10%" },

                { "data": "from", "width": "10%" },
                { "data": "to", "width": "10%" },
                { "data": "leaveReason", "width": "15%" },
                { "data": "noOfDays", "width": "8%" },               
                { "data": "status", "width": "10%" },
              
               



            ],






            "dom": 'rt<"bottom"><"my-wrapper"<"my-inner2"l><"my-inner1"i><"my-inner3"p>>',
            "serverSide": "true",
            "scrollY": '52vh',
            "sScrollX": "100%",
            "sScrollXInner": "100%",
            "ordering": false,
            "order": [0, "asc"],
            "processing": "true",
            "language": {
                processing: '<img width="100%" src="/images/Loader_img1 (7).gif">',
                emptyTable: '<div id="noDataFound" style="text-align: center;display: flex;align-items: center;justify-content: center;height: 45vh;"><img src="/images/nodata.png" /></div >',

                zeroRecords: '<div id="noDataFound" style="text-align: center;display: flex;align-items: center;justify-content: center;height: 45vh;"><img src="/images/nodata.png" /></div >',

                sLengthMenu: "_MENU_",
                paginate: {


                    next: '<img width="100%" src="/images/icons/Icon awesome-arrow-right-1.png" >',
                    previous: '<img src="/images/icons/Icon awesome-arrow-right.png">',
                    first: '<img src="/images/icons/Icon metro-next-1.png">',
                    last: '<img src="/images/icons/Icon metro-next.png">'




                }
            }



        });
        }


        $('.ihits-table table').removeClass("dataTable no-footer");


        $('.paginate_button.next').attr("data-bs-toggle", "tooltip");
        $('.paginate_button.next').attr("title", "Next");
        $('.paginate_button.previous').attr("data-bs-toggle", "tooltip");
        $('.paginate_button.previous').attr("title", "Previous");
        $('.paginate_button.first').attr("data-bs-toggle", "tooltip");
        $('.paginate_button.first').attr("title", "First");
        $('.paginate_button.last').attr("data-bs-toggle", "tooltip");
        $('.paginate_button.last').attr("title", "Last");


        $("#dataTableSearch").on("keyup search input paste cut", function () {
            dataTable.search(this.value).draw();
            $('#noDataFound').css("display", 'none');

        });
   
        $('.search-slider-inner').click(function () {
            /*    dataTable.search('').draw();*/
            $('#dataTableSearch').val('');
            $('#dataTableSearch').focus();
        });

        dataTable.on('draw', function () {
            $('.paginate_button.next').attr("data-bs-toggle", "tooltip");
            $('.paginate_button.next').attr("title", "Next");
            $('.paginate_button.previous').attr("data-bs-toggle", "tooltip");
            $('.paginate_button.previous').attr("title", "Previous");
            $('.paginate_button.first').attr("data-bs-toggle", "tooltip");
            $('.paginate_button.first').attr("title", "First");
            $('.paginate_button.last').attr("data-bs-toggle", "tooltip");
            $('.paginate_button.last').attr("title", "Last");

        });
    $.fn.DataTable.ext.pager.numbers_length = 5;
   
}



function ApplyFilter(mode) {
    debugger
    var tabIndex = $('#Tabindex').val();
    var search = $('#SearchId').val();
    //$("#Tabindex").val(currentTab);
    $("#SearchId").val($('#FilterList_Code').val())
    if (mode == 'apply') {
        if (filterApplicable >= 1) {

            fillData();
            CloseSlider('basicFilter');
            $('#filterForm').removeClass('filter-apply');
        }
        else {

            fillData();
            CloseSlider('basicFilter');
            $('#filterForm').addClass('filter-apply');
        }

    }
    else {

        filterApplicable = 1;
        $('.reset').val('');
        $('#Optional_Filter_EmploymentStatus_Code').val(0);
        $('#filterForm').removeClass('filter-apply');
        fillData();

    }
}


function onSelectAllClick() {
    if ($('#SelectAll').is(':checked')) {
        $('#HRAttendanceApprovalTableTwo tbody tr').remove();
        $('#HRAttendanceApprovalTable').find('input[type=checkbox]').prop('checked', true);
        //$('.HRAtt_Applicability').attr('checked', true);
    }
    else {
        //$('.HRAtt_Applicability').attr('checked', false);
        $('#HRAttendanceApprovalTable').find('input[type=checkbox]').prop('checked', false);
        $('#HRAttendanceApprovalTableTwo tbody tr').remove();
    }
    
   
    var totalRows = $('#HRAttendanceApprovalTable tbody tr').length;
    for (let i = 0; i < totalRows; i++) {
        onCheckboxChange(document.getElementById('HRAttendanceApprovalList_'+i+'__Applicable'), i)
    }
}

function onSubmit(Mode)
{
    debugger
    //var remark = $('#Remarks').val();
    //var table = $('#HRAttendanceApprovalTable').DataTable();
    //var data = table.$('input,select,textarea').serializeArray();
    //data.push({ name: 'Remarks', value: remark, name: 'Mode', value: Mode });


    var remark = $('#Remarks').val();

    if ($('#SelectAll').is(':checked')) {
        var table = $('#HRAttendanceApprovalTable').DataTable();
        var data = table.$('input,select,textarea').serializeArray();
        //var data = $('#HR_Atten_Approval_Form').serialize();
        data.push({ name: 'AuthorityLevelCode', value: $('#AuthorityLevelCode').val() });
    }
    else
        var data = $('#HR_Atten_Approval_Form').serialize();
    
    //data.push({ name: 'Remarks', value: remark, name: 'Mode', value: Mode });
    if (Mode == "APPROVE")
        var msg = "On approving all the selected attendance approvals will get approved directly. Do you want to Approve?"
    else
        var msg = "Do you want to Reject ?"
    swal.fire({
        text: msg,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {

            $.ajax({
                type: 'POST',
                url: '/TimeOffice/HRAttendanceApproval/ApproveOrReject?Remarks=' + remark + '&Mode=' + Mode,
                dataType: 'json',
                data: data ,
                success: function (result) {
                    debugger;
                    if (result.success == true) {
                        if (Mode == "APPROVE") {
                            swal.fire("Approved Successfully").then(function () {

                                //window.location.href = "/TimeOffice/HRAttendanceApproval/Index";
                                //fillData();
                                var m = $('.bd-example-modal-lg');                                                      
                                m.modal('hide');
                                $('#Remarks').val('');                               
                                refreshDatatable('#HRAttendanceApprovalTable');

                                $('#HRAttendanceApprovalTableTwo tbody tr').remove();

                            });
                        }
                        else {
                            swal.fire("Rejected Successfully").then(function () {

                                //window.location.href = "/TimeOffice/HRAttendanceApproval/Index";
                                //fillData();
                                var m = $('.bd-example-modal-lg');
                                m.modal('hide');
                                $('#Remarks').val('');
                               
                                refreshDatatable('#HRAttendanceApprovalTable');
                                $('#HRAttendanceApprovalTableTwo tbody tr').remove();

                            });
                        }

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

        else
            return false;

    })
    
}
function onHR_Applicability_AllClick(colchkid, tableid) {
    debugger
    if ($('#' + colchkid).is(':checked'))
        $('#' + tableid).find('input[type=checkbox]').prop('checked', true);
    else
        $('#' + tableid).find('input[type=checkbox]').prop('checked', false);
}
function onProceedClick() {
    debugger
    if ($('.HRAtt_Applicability:checked').length == 0) {
        swal.fire("Please select Atleast one Entry");
        $('#HRProceedButton').attr('data-target', '');
    }

    else {
        $('#HRProceedButton').attr('data-target', '.bd-example-modal-lg');

    }
}


function ExportToExcel() {
    debugger;
    var tableLength = $('#HRAttendanceApprovalList_Count').val();
   
    if (tableLength > 0) {
        var param = $('#BasicFilter').serialize();
        var level = $("#AuthorityLevelCode").val();
        var monthCode = $('#Month_Code').val();
        var PayrollCode = $('#PayrollGroup_Code').val();
        url = "/TimeOffice/HRAttendanceApproval/ExportToExcel_Approval?TabName=" + currentTab + "&totRecords=" + tableLength + "&data=" + param + "&Month=" + monthCode + "&PayrollCode=" + PayrollCode
            + "&Level=" + level;
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
    }
   
   
function fillMonth(sender)
{
    debugger;

    $.getJSON("/TimeOffice/LeaveCancellation/GetMonth", { PayrollGrp: $(sender).val() }, function (data) {

        $('#Month_Code option').remove();
        $('#Month_Code').append('<option value>--Month--</option');
        debugger;
        for (var i = 0; i < data.length; i++) {
            $('#Month_Code').append('<option value="' + data[i].code + '">' + data[i].name + '</option');
        }

    });
    //
    fillData();

}