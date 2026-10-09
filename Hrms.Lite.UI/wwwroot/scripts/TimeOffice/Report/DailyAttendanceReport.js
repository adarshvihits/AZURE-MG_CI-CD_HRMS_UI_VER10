
//function calculateDaysBetween(date1, date2) {
//    // Convert both dates to UTC timestamps
//    const startDate = new Date(date1);
//    const endDate = new Date(date2);

//    // Calculate the difference in milliseconds
//    const differenceInTime = endDate - startDate;

//    // Convert milliseconds to days (1000 ms * 60 s * 60 min * 24 hours)
//    const differenceInDays = differenceInTime / (1000 * 60 * 60 * 24);

//    return Math.round(differenceInDays);
//}

//// Example usage:
//const date1 = '2024-10-01';
//const date2 = '2024-10-30';
//console.log(calculateDaysBetween(date1, date2)); // Output: 29



function fillData() {

    debugger;
    var filter1 = $('#BasicFilter').serialize();
    var temp = filter1;
    var search = $('#SearchId').val();
    var filtercode = $("#FilterCombo_Code").val();
    var From = $("#From").val();
    var To = $("#To").val();
    var AttnStatus = $("#Attendancestatus").val();
    var AttnType = $("#AttendanceType").val();

    var attendanceStatus = $('#Attendancestatus').val();
    if (attendanceStatus.length == 0)
        $('.multiselect-selected-text').html("none selected");




    const startDate = new Date(From);
    const endDate = new Date(To);
    const differenceInTime = endDate - startDate;
    const differenceInDays = differenceInTime / (1000 * 60 * 60 * 24);

    if (differenceInDays > 190) {
        swal.fire("Please ensure the date range selected does not exceed 190 days")

    }
    else {
        var dataTable = $("#Daily_Attendance_Report").DataTable({

            "pagingType": 'full_numbers',
            "destroy": 'true',
            "ajax": {
                "url": "/TimeOffice/DailyAttendanceReport/GetDailyAttendanceReportList",

                "type": "POST",
                "datatype": "json",
                'data':

                    function (d) {
                        temp = '';

                        var start = d.start;
                        var searchValue = $('#dataTableSearch').val();
                        temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&From=' + From + '&To=' + To + '&AttnStatus=' + attendanceStatus + '&AttnType=' + AttnType + '&dummy=dummy';



                        d.filter = temp;
                        d.attendancestatus = attendanceStatus;


                        return JSON.stringify(d);
                    },




                "dataSrc": function (d) {
                    debugger
                    $('#DailyAttendanceProcessing_Count').val(d.recordsTotal);
                    for (var i = 0, ien = d.data.length; i < ien; i++) {

                        d.data[i].punchingDate = _GetFormattedDate(d.data[i].punchingDate);

                        //d.data[i].employeeName = '<a class="" data-bs-toggle="modal" data-target=".bd-example-modal-lg" onclick="onEmployeeNameClick('+'"'+ d.data[i].employeeGI+'"'+')">' + d.data[i].employeeName + '</a>'
                        d.data[i].employeeName = '<td data-label="AB"><a href="" data-bs-toggle="modal" data-target=".bd-example-modal-lg"type="" class="" onclick=onEmployeeNameClick(' + '"' + d.data[i].employeeGI + '"' + ')>' + d.data[i].employeeName + '</a></td>'
                    }
                    return d.data;
                }

            },

            "columns": [

                { "data": "employeeID", "width": "7%" },
                { "data": "fullName", "width": "9%" },
                { "data": "punchingDate", "width": "7%" },
                { "data": "shift.name", "width": "10%" },
                { "data": "in", "width": "5%" },
                { "data": "out", "width": "5%" },
                { "data": "totalTime", "width": "7%" },
                { "data": "inShiftTime", "width": "6%" },
                { "data": "earlyIn", "width": "6%" },
                { "data": "lateIn", "width": "6%" },
                { "data": "earlyOut", "width": "7%" },
                { "data": "overTime", "width": "7%" },
                { "data": "lateOutEarlyIn", "width": "7%" },
                { "data": "session", "width": "7%" },
                { "data": "reason", "width": "11%" },



            ],



            "dom": 'rt<"bottom"><"my-wrapper"<"my-inner2"l><"my-inner1"i><"my-inner3"p>>',

            "serverSide": "true",
            "scrollY": '40vh',
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
            /* dataTable.search('').draw();*/
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


function ExportToExcel() {
    debugger;
   
    const selectedValues = Array.from(document.getElementById('Attendancestatus').selectedOptions).map(option => option.text);
    //var statusName = $("#AttendanceType option : selected").text();
    //var typeName = $("#Attendancestatus option : selected").text();
    if (validateDropdowns()) {

        var TableLength = $('#DailyAttendanceProcessing_Count').val();
        if (TableLength > 0) {
            url = "/TimeOffice/DailyAttendanceReport/ExportToExcel?data=" + $('#BasicFilter').serialize() + "&totRecords=" + TableLength + "&From=" + $('#From').val() + "&To=" + $('#To').val() + "&Attendancestatus=" + $('#Attendancestatus').val() + "&AttendanceType=" + $('#AttendanceType').val();

            //url = "/TimeOffice/DailyAttendanceReport/ExportToExcel?statusName=" + statusName + "&typeName=" + typeName + "&totRecords=" + TableLength;
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
    }
}


function validateDropdowns() {
    debugger

    return true;
}
function ApplyFilter(mode) {
    debugger;



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
