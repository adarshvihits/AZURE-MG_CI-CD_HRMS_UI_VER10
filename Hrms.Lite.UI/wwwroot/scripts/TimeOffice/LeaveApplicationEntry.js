

var LeaveType;
var fromDate;
var SessionFrom;
var ToDate;
var SessionTo;
var DateOfApplication;
var LeaveApplicationGIvalue;

var prevTab = 'PendingTab';
var currentTab = "PENDING";
var global_balance;


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

    if (i == 0)
        return true;

    else {
        return false;
    }

}


function EmpNameClick(EmployeeGI, employeeCode,EmployeeName,EmployeeId) {
    debugger;
    var url = "/TimeOffice/LeaveApplication/GetLeaveApplicationDetails?EmployeeGI=" + EmployeeGI + "&employeeCode=" + employeeCode + "&EmployeeId=" + EmployeeId + "&EmployeeName=" + EmployeeName;
    window.location.href = url;

}


function fillData() {

    debugger;
    var filter1 = $('#BasicFilter').serialize();

    var temp = filter1;
 
    var dataTable = $("#LeaveApplnTbl").DataTable({

            "pagingType": 'full_numbers',
            "destroy": 'true',
            "ajax": {
                "url": "/TimeOffice/LeaveApplication/GetList",
                "type": "POST",


                "datatype": "json",


                'data':

                    function (d) {
                        temp = '';




                        var start = d.start;

                        var searchValue = $('#dataTableSearch').val();






                        temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue+ '&dummy=dummy';
                        d.filter = temp;


                        return JSON.stringify(d);
                    },




                "dataSrc": function (d) {





                    $('#LeaveApplicationEmployeeList_Count').val(d.recordsTotal);
                    for (var i = 0, ien = d.data.length; i < ien; i++) {
                        debugger;
                        d.data[i].doj = _GetFormattedDate(d.data[i].doj);

                        d.data[i].employeeName = '<div><a onclick="EmpNameClick(' + "'" + d.data[i].employeeGI + "'" + ',' + d.data[i].employeeCode +','+"'"+ d.data[i].employeeName +"'"+ ',' +"'"+d.data[i].employeeID+"'"+ ')">' + d.data[i].employeeName + ' </a ><input type="hidden" id="empGI_' + i + '" value="' + d.data[i].employeeGI + '"></div>'

                    }

                    return d.data;
                }

            },

            "columns": [

                { "data": "employeeID", "width": "20%" },
                { "data": "employeeName", "width": "20%" },
                { "data": "designation.name", "width": "15%" },
                { "data": "department.name", "width": "15%" },
                { "data": "location.name", "width": "15%" },
                { "data": "doj", "width": "15%" },
             

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

            $('#dataTableSearch').val('');
            $('#dataTableSearch').focus();
           // dataTable.search('').draw();
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
function GetLeaveDayWiseDetails() {
    debugger;
    AttndanceLeaveType = $('#AttndanceLeaveType').val();
    LeaveType = $('#LeaveType_Code').val();
    fromDate = $('#From').val();
    SessionFrom = $('#FromType_Code').val();
    ToDate = $('#To').val();
    SessionTo = $('#ToType_Code').val();
    DateOfApplication = $('#DateOfApplication').val();
    tOtalDays = parseInt($("#TotalDays").val());
    leaveBalance = parseInt(global_balance);
    empGI = $('#EmployeeGI').val();
    if (validateDropdowns()) {
        debugger;
        if (parseInt(AttndanceLeaveType) == 1) {
            $.ajax({

                type: 'POST',
                url: '/TimeOffice/LeaveApplication/CheckIfAlreadyLeaveApplied',
                dataType: 'json',
                data: { DateOfApplication: DateOfApplication, LeaveType: LeaveType, fromDate: fromDate, SessionFrom: SessionFrom, ToDate: ToDate, SessionTo: SessionTo, empGI: empGI},
                success: function (result) {
                    if (result.success == true) {
                        $('#leaveSubmit').prop('disabled', false);
                        debugger
                        fillReasonDropdown();
                        $('#LeaveReason_Code').prop('disabled', false);

                        var url = "/TimeOffice/LeaveApplication/GetLeaveDayWiseDetails";
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
        else {
            debugger;
            if (tOtalDays > leaveBalance) {
                swal.fire("No Leave Balance");
            }
            else {
                $.ajax({

                    type: 'POST',
                    url: '/TimeOffice/LeaveApplication/CheckIfAlreadyLeaveApplied',
                    dataType: 'json',
                    data: { DateOfApplication: DateOfApplication, LeaveType: LeaveType, fromDate: fromDate, SessionFrom: SessionFrom, ToDate: ToDate, SessionTo: SessionTo, empGI: empGI },
                    success: function (result) {
                        if (result.success == true) {
                            $('#leaveSubmit').prop('disabled', false);
                            debugger
                            fillReasonDropdown();
                            $('#LeaveReason_Code').prop('disabled', false);

                            var url = "/TimeOffice/LeaveApplication/GetLeaveDayWiseDetails";
                            $('#DayWiseLeaveHRMSList').load(url, { DateOfApplication: DateOfApplication, LeaveType: LeaveType, fromDate: fromDate, SessionFrom: SessionFrom, ToDate: ToDate, SessionTo: SessionTo, empGI: empGI}, function () { });
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

    }
   
}
function fillReasonDropdown() {
    debugger;
    $.getJSON("/TimeOffice/LeaveApplication/GetLeaveVsReaons", { LeaveCode: $('#LeaveType_Code').val() }, function (data) {
        console.log(data);
        $('#LeaveReason_Code option').remove();
        $('#LeaveReason_Code').append('<option value>--Select--</option');

        for (var i = 0; i < data.length; i++) {

            $('#LeaveReason_Code').append('<option value="' + data[i].code + '">' + data[i].name + '</option');
        }

    });
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
    if (parseInt($('#AttndanceLeaveType').val()) != 1) {
        if ($("#TotalDays").val() > global_balance) {
            swal.fire("No Leave Balance");
            return false;
        }
    }
    if (i == 0) {
        $('.todate').prop('disabled', false);
        return true;
    }

    else {
        return false;
    }
}



function CalculateDays() {
    debugger;
    $('#leaveSubmit').prop('disabled', true);
    $('#LeaveReason_Code').prop('disabled', true);


    $('#DayWiseLeaveListTableHRMS tbody').empty();
    var FROM = new Date($('#From').val());
    var TO = new Date($('#To').val());

    var Days = (TO.getTime() - FROM.getTime());
    var TotalDays = Math.ceil(Days / (1000 * 3600 * 24));

    if (Date.parse(FROM) == Date.parse(TO)) {
        if ($('#FromType_Code').val() != 1) {
            $("#TotalDays").val(0.5);
            $(".days").html(0.5);
        }
        else {
            $("#TotalDays").val(1.0);
            $(".days").html(1.0);
        }
    }
    else {
        if ($('#FromType_Code').val() != 1)
            TotalDays = parseFloat(TotalDays) - 0.5
        if ($('#ToType_Code').val() != 1)
            TotalDays = parseFloat(TotalDays) - 0.5

        $("#TotalDays").val(parseFloat(TotalDays + 1));
        $(".days").html(parseFloat(TotalDays + 1));
    }
}



function showsessionto() {
    debugger
    $('#leaveSubmit').prop('disabled', true);
    $('#LeaveReason_Code').prop('disabled', true);

    $('#DayWiseLeaveListTableHRMS tbody').empty();
    if (!validateById('#To', 'toDate'))
        return false;
    fromDate = $('#From').val();
    ToDate = $('#To').val();


    if (Date.parse(fromDate) > Date.parse(ToDate)) {
        swal.fire("To Date should be greater than From Date");
        $('#To').val('');
    }
    else {
        if (fromDate != ToDate) {
            $("#FromType_Code option[value='2']").remove();
            $('#sessiontoshow').removeClass('hide');


        }
        else {
            $("#FromType_Code").append("<option value='2'> First Half</option>");
            $('#sessiontoshow').addClass('hide');

        }

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
                    url: '/TimeOffice/LeaveApplication/SaveLeave',
                    contentType: false,
                    processData: false,
                    dataType: 'json',
                    data: data,
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire("Applied Successfully").then(function () {
                                debugger
                                url = "/TimeOffice/LeaveApplication/Index";
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
function ApplnHistory(EmployeeGI) {
    debugger;
   
    var url = "/TimeOffice/LeaveApplication/ApplicationHistory?EmployeeGI=" + EmployeeGI;
    window.location.href = url;
}
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

    LeavePeriod = $('#LeavePeriod_Code').val();
    LeaveType = $('#LeaveType_Code').val();
    EmployeeGI = $('#EmployeeGI').val();

    if (LeavePeriod !== '' && LeaveType !== '') {
        showSpinner();
        var url = "/TimeOffice/LeaveApplication/GetLeaveApplicationList";
        $.ajax({
            type: 'GET',
            url: '/TimeOffice/LeaveApplication/GetLeaveApplicationList',
            dataType: 'html',
            data: { leavePeriodCode: LeavePeriod, leaveType: LeaveType, TabIndex: currentTab, EmployeeGI: EmployeeGI },
            success: function (result) {
                debugger;
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    //$('body').append('<div class="form-overlay"></div>');                    
                    $('#alllist').html("");
                    $('#alllist').html(result);
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

    var url = "/TimeOffice/LeaveApplication/GetTrackerDetails";

    //$('#applntracker').load(url, { LeaveApplicationGI: LeaveApplicationGIforTracker }, function () { });
    $.ajax({
        type: 'GET',
        url: '/TimeOffice/LeaveApplication/GetTrackerDetails',
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

function viewFileFromDT(AbsoluteUriname) {
    debugger;
    var AbsoluteUri = $('#Attachment_AbsoluteUri').val();
    
    if (AbsoluteUriname == "") {
        swal.fire("No file Uploaded")
    }
    else {
        window.open(AbsoluteUriname);
    }
}
function ExportToExcel() {
    var period = $("#LeavePeriod_Code option:selected").text();
    var leavetype = $("#LeaveType_Code option:selected").text();
    var EmployeeGI = $("#EmployeeGI").val();
    debugger;
    var pendingTableLength = $('#LeaveApplicationList_Count').val();
    if (pendingTableLength > 0) {
        url = "/TimeOffice/LeaveApplication/ExportToExcel?leavePeriodCode=" + $('#LeavePeriod_Code').val() + "&periodName=" + period + "&leaveType=" + $('#LeaveType_Code').val() + "&leaveTypeName=" + leavetype + "&TabIndex=" + currentTab + "&EmployeeGI=" + EmployeeGI;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}
function ExportToExcelMainList() {
    debugger;
    //var period = $("#LeavePeriod_Code option:selected").text();
    //var leavetype = $("#LeaveType_Code option:selected").text();
    //var EmployeeGI = $("#EmployeeGI").val();
    debugger;
    var TotalRows = $('#LeaveApplicationEmployeeList_Count').val();
    if (TotalRows > 0) {
        url = "/TimeOffice/LeaveApplication/ExportToExcelMainList?TotalRows=" + TotalRows;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}

function ApplyFilter(mode) {

    var search = $('#SearchId').val();

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


function showDecompressedDocument(ReqGI, LeaveType) {

    var url = "/TimeOffice/LeaveApplication/DownloadDoc?RequestGI="
        + ReqGI + "&LeaveTypeName=" + LeaveType;

    var checkUrl = url + "&check=1";

    fetch(checkUrl)
        .then(res => {
            if (!res.ok) {
                swal.fire("No file uploaded...");
                return;
            }

            window.open(url, "_blank");
        })
        .catch(() => swal.fire("No file uploaded..."));
} plication
