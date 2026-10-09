var leaveCode_ = 0;
function GetEmployeeList() {
    var type = $('input[name="Type"]:checked').val();
    var leavePeriodCode = $('#LeavePeriod_Code').val();
    $.getJSON("/TimeOffice/EmployeeVsLeaveDetailsReport/GetEmployeeList", { Type: type,LeavePeriodCode:leavePeriodCode }, function (data) {
        debugger
        $('#EmployeeCode option').remove();

        $('#EmployeeCode').append(('<option value>Search by Employee Name/ID</option'));


        for (var i = 0; i < data.length; i++) {
            debugger
            $('#EmployeeCode').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
        }
        $('#EmployeeCode').selectpicker('refresh');
    });
}

function GetList() {
    debugger;
    var leavePeriodCode = $('#LeavePeriod_Code').val();
    var employeeCode = $('#EmployeeCode').val();

    var Type = $('input[name="Type"]:checked').val();

    //if (Type == 'ACTIVE') TypeCode = 0;
    //else TypeCode = 1;


    leaveCode_ = 0;
    if (leavePeriodCode != '' && employeeCode != '') {
       
        $.ajax({

            type: 'GET',
            url: '/TimeOffice/EmployeeVsLeaveDetailsReport/GetList',
            dataType: 'html',
            data: { LeavePeriod: leavePeriodCode, EmployeeCode: employeeCode, Type: Type },
            success: function (result) {
                $('#leaveList').html('');
                $('#leaveList').html(result);
            },
            error: function (xhr, textStatus, errorThrown) {
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);
            }
        });
       
    }
}


function onLeaveTypeClick(leaveCode) {
    var leavePeriodCode = $('#LeavePeriod_Code').val();
    var employeeCode = $('#EmployeeCode').val();
    leaveCode_ = leaveCode;
    if (leavePeriodCode != '' && employeeCode!='') {
        $.ajax({

            type: 'GET',
            url: '/TimeOffice/EmployeeVsLeaveDetailsReport/GetLeaveDetails',
            dataType: 'html',
            data: { LeavePeriod: leavePeriodCode, EmployeeCode: employeeCode, LeaveCode: leaveCode },
            success: function (result) {
                $('#leaveDetailsList').html('');
                $('#leaveDetailsList').html(result);
            },
            error: function (xhr, textStatus, errorThrown) {
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);
            }
        });
    }
}

function ExportToExcel() {

    debugger;
    var leavePeriod = $('#LeavePeriod_Code').val();
    var empCode = $('#EmployeeCode').val();

    var tableLength = $('#LeaveRegisterList_Count').val();
    if (tableLength > 0) {
        //url = "/DataBank/JoiningReport/ExportToExcel?fromDate=" + fromDate + "&toDate=" + toDate + "&totRecords=" + tableLength;
        url = "/TimeOffice/EmployeeVsLeaveDetailsReport/ExportToexcel?LeavePeriod=" + leavePeriod + "&EmployeeCode=" + empCode;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }

}
function ExportToExcelDetails() {

    debugger;
    var leavePeriod = $('#LeavePeriod_Code').val();
    var empCode = $('#EmployeeCode').val();

    var tableLength = $('#LeaveDetails_Count').val();
    if (tableLength > 0) {
        url = "/TimeOffice/EmployeeVsLeaveDetailsReport/ExportToExcelDetails?LeaveCode= "+ leaveCode_ +"&LeavePeriod= " + leavePeriod + "&EmployeeCode=" + empCode;
        //data: $('#empVsleaveDetails').serialize() +"&LeavePeriod = " + leavePeriod + " & EmployeeCode=" + empCode,
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }

}