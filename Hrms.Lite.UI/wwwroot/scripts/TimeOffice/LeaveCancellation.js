var trnHistory = 0;
function fillData() {
    debugger

    var filter1 = $('#BasicFilter').serialize();

    var temp = filter1;
    var PayrollCode = $("#Payroll_Code").val();
    var MonthCode = $("#Month_Code").val();
    var LeaveCode = $("#Leave_Code").val();
    debugger;

   /* if (MonthCode != '') {*/
        var dataTable = $("#leaveCancellationTable").DataTable({

            "pagingType": 'full_numbers',
            "destroy": 'true',





            "ajax": {
                "url": "/TimeOffice/LeaveCancellation/GetList",
                "type": "POST",


                "datatype": "json",


                'data':

                    function (d) {
                        temp = '';
                        var start = d.start;

                        var searchValue = $('#dataTableSearch').val();






                        temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&MonthCode=' + MonthCode + '&LeaveCode=' + LeaveCode + '&PayrollCode=' + PayrollCode + '&dummy=dummy';
                        d = temp;

                        /* showSpinner();*/
                        return JSON.stringify(d);
                    },




                "dataSrc": function (d) {


                    //$('.AllCount').html(d.count.allCount);
                    //$('.AbsenteesamCount').html(d.count.absenteesamCount);
                    //$('.LeaveCount').html(d.count.leaveCount);
                    console.log(d)
                    $('#LeaveCancellationList_Count').val(d.recordsTotal);

                    //$('#atte ').css("display", 'block');
                    //$('.dataTables_sizing ').css("display", 'block');

                    for (var i = 0, ien = d.data.length; i < ien; i++) {


                        d.data[i].leaveDate = _GetFormattedDate(d.data[i].leaveDate);
                        //d.data[i].employeeName = '<a href = "/TimeOffice/DutyRoster/EmployeeDutyRosterEditDetails?EmployeeGI=' + d.data[i].employeeGI + '&Month=' + month + '&PayrollGroup=' + PayrollGroup + '&CIRCLE=' + CIRCLE + '">' + d.data[i].employeeName + ' </a >'

                        //d.data[i].dayStatus.code = '<button type="button" class="btn btn-default btn-sm" onclick="LeaveCancellationSave(' + d.data[i].employeeCode + ',' + d.data[i].leave.code + ',' + d.data[i].dayStatus.code + ',' + "'" + d.data[i].leaveDate+"'" + ')">Save</button>'
                        d.data[i].dayStatus.code = '<a id = "Cancelbtn" onclick = "LeaveCancellationSave(' + d.data[i].employeeCode + ',' + d.data[i].leave.code + ',' + d.data[i].dayStatus.code + ',' + "'" + d.data[i].leaveDate+"'" + ')" ><i class="fa fa-times-circle" style="color:red ;" data-bs-toggle="tooltip" title="" aria-hidden="true" data-bs-original-title="Cancel"></i> </a >'


                    

                    }
                   
                    //hideSpinner();
                    return d.data;
                }

            },

            "columns": [
                { "data": "employeeID", "width": "9%" },
                { "data": "employeeName", "width": "15%" },
                { "data": "designation.name", "width": "15%" },
                { "data": "department.name", "width": "15%" },
                { "data": "location.name", "width": "15%" },
                { "data": "leave.name", "width": "15%" },
                { "data": "dayStatus.name", "width": "15%" },
                { "data": "leaveDate", "width": "15%" },                
                { "data": "dayStatus.code", "width": "15%" },
                
            ],




            //"dom": '<t><"my-wrapper"<"my-inner2"l><"my-inner1"i><"my-inner3"p>>',
            //"": '<t><"my-wrapper"<"my-inner2"l><"my-inner1"i><"my-inner3"p>>',
            //"dom": 'rt<"bottom"><"my-wrapper"<"my-inner2"l><"my-inner1"i><"my-inner3"p>>',
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
            /*dataTable.search('').draw();*/
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

//}



function ApplyFilter(mode) {
   
    if (mode == 'apply') {
        if (filterApplicable >= 1) {
            if (trnHistory != 1)
                fillData();
            else
                fillDataTrnHistory();
            CloseSlider('basicFilter');
            $('#filterForm').removeClass('filter-apply');
        }
        else {            
            if (trnHistory != 1)
                fillData();
            else
                fillDataTrnHistory();
            CloseSlider('basicFilter');
            $('#filterForm').addClass('filter-apply');
        }
    }
    else {        
        filterApplicable = 1;
        $('.reset').val('');
        $('#Optional_Filter_EmploymentStatus_Code').val(0);
        $('#filterForm').removeClass('filter-apply');
        if (trnHistory != 1)
            fillData();
        else
            fillDataTrnHistory();
    }
}

function TransactionHistory() {
    window.location.href = '/TimeOffice/LeaveCancellation/TransactionHistory';
}

function fillDataTrnHistory() {
    debugger

    var filter1 = $('#BasicFilter').serialize();

    var temp = filter1;
    var MonthCode = $("#Month_Code").val();
    var LeaveCode = $("#Leave_Code").val();
    var PayrollCode = $("#Payroll_Code").val();

    if (MonthCode != '') {
        var dataTable = $("#leaveCancellationTrnHistoryTable").DataTable({

            "pagingType": 'full_numbers',
            "destroy": 'true',





            "ajax": {
                "url": "/TimeOffice/LeaveCancellation/GetTrnHistoryList",
                "type": "POST",


                "datatype": "json",


                'data':

                    function (d) {
                        temp = '';
                        var start = d.start;

                        var searchValue = $('#dataTableSearch').val();






                        temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&MonthCode=' + MonthCode + '&LeaveCode=' + LeaveCode + "&PayrollCode=" + PayrollCode + '&dummy=dummy';
                        d = temp;

                        /* showSpinner();*/
                        return JSON.stringify(d);
                    },




                "dataSrc": function (d) {


                    //$('.AllCount').html(d.count.allCount);
                    //$('.AbsenteesamCount').html(d.count.absenteesamCount);
                    //$('.LeaveCount').html(d.count.leaveCount);
                    console.log(d)
                    $('#LeaveCancellationList_Count').val(d.recordsTotal);

                    //$('#atte ').css("display", 'block');
                    //$('.dataTables_sizing ').css("display", 'block');

                    for (var i = 0, ien = d.data.length; i < ien; i++) {


                        d.data[i].leaveDate = _GetFormattedDate(d.data[i].leaveDate);
                        //d.data[i].employeeName = '<a href = "/TimeOffice/DutyRoster/EmployeeDutyRosterEditDetails?EmployeeGI=' + d.data[i].employeeGI + '&Month=' + month + '&PayrollGroup=' + PayrollGroup + '&CIRCLE=' + CIRCLE + '">' + d.data[i].employeeName + ' </a >'

                        

                    }

                    //hideSpinner();
                    return d.data;
                }

            },

            "columns": [
                { "data": "employeeID", "width": "10%" },
                { "data": "employeeName", "width": "20%" },
                { "data": "designation.name", "width": "10%" },
                { "data": "department.name", "width": "10%" },
                { "data": "location.name", "width": "10%" },
                { "data": "leave.name", "width": "10%" },
                { "data": "dayStatus.name", "width": "9%" },
                { "data": "leaveDate", "width": "9%" },
                
                { "data": "entryUser", "width": "12%" },
            ],




            //"dom": '<t><"my-wrapper"<"my-inner2"l><"my-inner1"i><"my-inner3"p>>',
            //"": '<t><"my-wrapper"<"my-inner2"l><"my-inner1"i><"my-inner3"p>>',
            //"dom": 'rt<"bottom"><"my-wrapper"<"my-inner2"l><"my-inner1"i><"my-inner3"p>>',
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
            /*dataTable.search('').draw();*/
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

}


function LeaveCancellationSave(EmployeeCode, LeaveCode, DayStatusCode, LeaveDate) {



    swal.fire({
        title: 'Do you want to Cancel?',
        //text: "Reason to unlock",
        inputLabel: 'Remarks',
        input: 'text',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok',

        preConfirm: (value) => {
            if (!value) {
                Swal.showValidationMessage(
                    'Reason for unlock is mandatory'
                )
            }
        }

    }).then((result) => {
        if (result.value) {
            
            $.ajax({
                type: 'POST',
                url: '/TimeOffice/LeaveCancellation/Save',
                datatype: 'json',
                data: { EmployeeCode: EmployeeCode, LeaveCode: LeaveCode, DayStatusCode: DayStatusCode, LeaveDate: LeaveDate, Remarks: result.value },
                success: function (result) {
                    if (result.success) {
                        swal.fire('Saved Successfully');
                        fillData();
                    }
                    else
                        swal.fire(result.message);
                },
                error: function (xhr, textStatus, errorThrown) {
                    debugger
                    document.body.id = "kt_body";
                    $('body').html(xhr.responseText);


                }
            });
        }

    });



   

}

function ExportToExcel() {

    debugger;

    var MonthCode = $('#Month_Code').val();
    var LeaveCode = $('#Leave_Code').val();
    var PayrollCode = $('#Payroll_Code').val();
    var MName = $('#Month_Code option:selected').text();
    var LName = $('#Leave_Code option:selected').text();
    var PayrollName = $('#Payroll_Code option:selected').text();

    var tableLength = $('#LeaveCancellationList_Count').val();
    if (tableLength > 0) {
        url = "/TimeOffice/LeaveCancellation/ExportToExcel?data=" + $('#BasicFilter').serialize() + "&MonthCode=" + MonthCode + "&LeaveCode=" + LeaveCode + "&MName=" + MName + "&LName=" + LName + "&PayrollCode=" + PayrollCode + "&PayrollName=" + PayrollName + "&totRecords=" + tableLength;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
   
}

function fillMonth(sender) {
    debugger;
    $.ajaxSetup({ async: false });
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


function ExportToExcelTrnHistory() {

    debugger;
    var MonthCode = $('#Month_Code').val();
    var LeaveCode = $('#Leave_Code').val();
    var MName = $('#Month_Code option:selected').text();
    var LName = $('#Leave_Code option:selected').text();

    var PayrollCode = $('#Payroll_Code').val();
    var PayrollName = $('#Payroll_Code option:selected').text();1

    var tableLength = $('#LeaveCancellationList_Count').val();
    if (tableLength > 0) {
        url = "/TimeOffice/LeaveCancellation/ExportToExcelTrnHistory?data=" + $('#BasicFilter').serialize() + "&MonthCode=" + MonthCode + "&LeaveCode=" + LeaveCode + "&MName=" + MName + "&LName=" + LName + "&PayrollCode=" + PayrollCode + "&PayrollName=" + PayrollName + "&totRecords=" + tableLength;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }

}


