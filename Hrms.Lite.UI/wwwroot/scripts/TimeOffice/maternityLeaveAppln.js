



var prevTab = 'PendingTab';
var currentTab = "PENDING";

//region EmployeeList

function fillData() {

    debugger;
    var filter = $('#BasicFilter').serialize(); 

    /*  $('#JoiningReportTable').html('');*/
    var dataTable = $("#maternity_EmployeeList").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/TimeOffice/MaternityLeaveApplication/GetEmployeeList",
            "type": "POST",


            "datatype": "json",


            'data':
                function (d) {
                    temp = '';
                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();
                    temp = filter + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&dummy=dummy';
                    d.filter = temp;

                    return JSON.stringify(d);
                },
            "dataSrc": function (d) {

                $('#LeaveApplication_LeaveApplicationEmployeeList_Count').val(d.recordsTotal);
                for (var i = 0, ien = d.data.length; i < ien; i++) {
                    d.data[i].doj = _GetFormattedDate(d.data[i].doj);
                        d.data[i].employeeName = '<div><a onclick="Create(' + "'" + d.data[i].employeeGI + "'" + ',' + d.data[i].employeeCode + ',' + "'" + d.data[i].employeeName + "'" + ',' + "'" + d.data[i].employeeID + "'" + ')">' + d.data[i].employeeName + ' </a ><input type="hidden" id="empGI_' + i + '" value="' + d.data[i].employeeGI + '"></div>'

                }


                console.log(d.data);
                return d.data;
            }

        },
        "columns": [

            { "data": "employeeID", "width": "10%" },
            { "data": "employeeName", "width": "16%" },
            { "data": "designation.name", "width": "10%" },
            { "data": "department.name", "width": "10%" },
            { "data": "location.name", "width": "10%" },
            { "data": "doj", "width": "8%" }
          



        ],
        "dom": 'rt<"bottom"><"my-wrapper"<"my-inner2"l><"my-inner1"i><"my-inner3"p>>',
        "serverSide": "true",
        "scrollY": '52vh',
        "sScrollX": "100%",
        "sScrollXInner": "100%",

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
        //dataTable.search('').draw();
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
        $('#filterForm').removeClass('filter-apply');
        fillData();

    }
}
//region cReate
function Create(EmployeeGI, employeeCode, EmployeeName, EmployeeId) {
    debugger;
    var url = "/TimeOffice/MaternityLeaveApplication/Create?EmployeeGI=" + EmployeeGI + "&employeeCode=" + employeeCode + "&EmployeeId=" + EmployeeId + "&EmployeeName=" + EmployeeName;
    window.location.href = url;

}



function validateDropdowns() {
    var i = 0;
    if (!validateById('#From', 'fromDate'))
        i++;

    if (!validateById('#FromType_Code', 'sessionFrom'))
        i++;
    if (!validateById('#To', 'toDate'))
        i++;
    if (!validateById('#ToType_Code', 'sessionTo'))
        i++;

    if (!validateById('#LeaveType_Code', 'levType'))
        i++; 
    var from = _GetFormattedDate($("#From").val());
    var to = _GetFormattedDate(($("#To").val()));
    
    var fromDate = new Date(from);
    var toDate = new Date(to);

    if (fromDate > toDate) {
        swal.fire('From date Should Less than To Date');
        i++;
    }
    if (i == 0)
        return true;

    else {
        return false;
    }

}
function validateOnLeaveApply() {
    debugger
    var i = 0;
    if (!validateById('#From', 'fromDate'))
        i++;
    if (!validateById('#LeaveType_Code', 'levType'))
        i++;

    if (!validateById('#FromType_Code', 'sessionFrom'))
        i++;
    if (!validateById('#To', 'toDate'))
        i++;
    if (!validateById('#ToType_Code', 'sessionTo'))
        i++;
    if (!validateById('#LeaveReason_Code', 'Reason'))
        i++;
   
    if (i == 0) {
        $('.todate').prop('disabled', false);
        return true;
    }

    else {
        return false;
    }
}
function ApplyLeave() {
    if (validateOnLeaveApply()) {
        debugger
        data = new FormData($('#levAppl')[0]);


        swal.fire({
            text: "Do you want to apply?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({

                    type: 'POST',
                    url: '/TimeOffice/MaternityLeaveApplication/SaveLeave',
                    contentType: false,
                    processData: false,
                    dataType: 'json',
                    data: data,
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire("Applied Successfully").then(function () {
                                debugger
                                url = "/TimeOffice/MaternityLeaveApplication/ApplicationHistory?EmployeeGI=" + $("#EmployeeGI").val();
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
        });
    }
}
function fillReasonDropdown() {
    debugger;
    $.getJSON("/TimeOffice/MaternityLeaveApplication/GetLeaveVsReaons", { LeaveCode: $('#LeaveType_Code').val() }, function (data) {
        console.log(data);
        $('#LeaveReason_Code option').remove();
        $('#LeaveReason_Code').append('<option value>--Select--</option');

        for (var i = 0; i < data.length; i++) {

            $('#LeaveReason_Code').append('<option value="' + data[i].code + '">' + data[i].name + '</option');
        }

    });
}

function GetLeaveDayWiseDetails() {
    debugger;

    LeaveType = $('#LeaveType_Code').val();
    fromDate = $('#From').val();
    SessionFrom = $('#FromType_Code').val();
    ToDate = $('#To').val();
    SessionTo = $('#ToType_Code').val();
    DateOfApplication = $('#DateOfApplication').val();
 
    empGI = $('#EmployeeGI').val();
    if (validateDropdowns()) {
        debugger;
      
            $.ajax({

                type: 'POST',
                url: '/TimeOffice/MaternityLeaveApplication/CheckIfAlreadyLeaveApplied',
                dataType: 'json',
                data: { DateOfApplication: DateOfApplication, LeaveType: LeaveType, fromDate: fromDate, SessionFrom: SessionFrom, ToDate: ToDate, SessionTo: SessionTo, empGI: empGI },
                success: function (result) {
                    if (result.success == true) {
                        $('#leaveSubmit').prop('disabled', false);
                        debugger
                        fillReasonDropdown();
                        $('#LeaveReason_Code').prop('disabled', false);

                        var url = "/TimeOffice/MaternityLeaveApplication/GetLeaveDayWiseDetails";
                        $('#DayWiseLeaveHRMSList').load(url, { DateOfApplication: DateOfApplication, LeaveType: LeaveType, fromDate: fromDate, SessionFrom: SessionFrom, ToDate: ToDate, SessionTo: SessionTo, empGI: empGI }, function () { });
                    }

                    else {
                        swal.fire(result.message).then(function () {

                        });
                        $('#leaveSubmit').prop('disabled', true);
                        $('#LeaveReason_Code').prop('disabled', true);
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


    }

function fillToDate()
{
    $.getJSON("/TimeOffice/MaternityLeaveApplication/GetMaternityDaysAndToDate?FromDate=" + $("#From").val(), {}, function (data) {

        $("#To").val(_GetFormattedDate(data.date));
    });
}
//region Application History

function tabClick(Tab, TabIndex) {
    debugger;
    currentTab = TabIndex;

    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");
    $('#tabsDropdownLeaveApplication').val(currentTab);
    prevTab = Tab;
    getList();

}


function getList() {
    debugger;

  var  LeavePeriod = $('#LeaveApplication_LeavePeriod_Code').val();
 var   LeaveType = $('#LeaveApplication_LeaveType_Code').val();
  var  EmployeeGI = $('#LeaveApplication_EmployeeGI').val();

    if (LeavePeriod !== '') {
        showSpinner();

        $.ajax({
            type: 'GET',
            url: '/TimeOffice/MaternityLeaveApplication/GetLeaveApplicationList',
            dataType: 'html',
            data: { leavePeriodCode: LeavePeriod, leaveType: LeaveType, TabIndex: currentTab, EmployeeGI: EmployeeGI },
            success: function (result) {
                debugger;
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    //$('body').append('<div class="form-overlay"></div>');                    
                    $('#mat_list').html("");
                    $('#mat_list').html(result);
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
    else {
        return false;
    }


}
function ApplnTracker(LeaveApplicationGIforTracker) {

    debugger;
    $('body').append('<div class="form-overlay"></div>');

    var url = "/TimeOffice/MaternityLeaveApplication/GetTrackerDetails";

    //$('#applntracker').load(url, { LeaveApplicationGI: LeaveApplicationGIforTracker }, function () { });
    $.ajax({
        type: 'GET',
        url: '/TimeOffice/MaternityLeaveApplication/GetTrackerDetails',
        dataType: 'html',
        data: { LeaveApplicationGI: LeaveApplicationGIforTracker },
        success: function (result) {
            debugger;
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                //$('body').append('<div class="form-overlay"></div>');
                $('#applntracker').html("");
                $('#applntracker').html(result);
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
function ExportToExcel() {
    var period = $("#LeaveApplication_LeavePeriod_Code option:selected").text();
    var leavetype = $("#LeaveApplication_LeaveType_Code option:selected").text();
    var EmployeeGI = $("#LeaveApplication_EmployeeGI").val();
    debugger;
    var pendingTableLength = $('#LeaveApplication_LeaveApplicationList_Count').val();
    if (pendingTableLength > 0) {
        url = "/TimeOffice/MaternityLeaveApplication/ExportToExcel?leavePeriodCode=" + $('#LeaveApplication_LeavePeriod_Code').val() + "&periodName=" + period + "&leaveType=" + $('#LeaveApplication_LeaveType_Code').val() + "&leaveTypeName=" + leavetype + "&TabIndex=" + currentTab + "&EmployeeGI=" + EmployeeGI;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}

function ExportToExcelEmployeeList() {
    var filter = $('#BasicFilter').serialize();  

    var PARAM = 'data=' + filter;
    var TableLength = $('#LeaveApplication_LeaveApplicationEmployeeList_Count').val();
    if (TableLength > 0) {
        url = '/TimeOffice/MaternityLeaveApplication/ExportToExcelEmployeeList?' + PARAM + "&TotalRows=" + TableLength;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}

function viewFileFromDT() {
    debugger;
    var AbsoluteUri = $('#LeaveApplication_Attachment_AbsoluteUri').val();

    if (AbsoluteUri == "") {
        swal.fire("No file Uploaded")
    }
    else {
        window.open(AbsoluteUri);
    }
}
