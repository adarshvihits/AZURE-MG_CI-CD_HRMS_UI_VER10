
var prevTab = 'all';
var currentTab = 0;  //code for 'all'
const month = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];

var global_count = 0;


function onDailyAttendanceEntryClick() {
    debugger;

}
function ValidateStatus(Statusvalue) {
    debugger
    var AttnStatus = $(Statusvalue).val();
    $.getJSON("/TimeOffice/MonthlyAttendanceReport/ValidateAttendanceStatus", { AttnStatus: AttnStatus }, function (data) {
        console.log(data);
        debugger
        if (data.success == false) {
            swal.fire(data.message).then(function () {
                $(Statusvalue).val("");
            });

        }
        else {
            return false;
           

        }

    });

}


function fillMonth(sender) {
    debugger;
    $.getJSON("/TimeOffice/MonthlyAttendanceReport/GetMonth", { PayrollGrp: $(sender).val() }, function (data) {

        $('#Month_Code option').remove();
        $('#Month_Code').append('<option value>--Select Month-- </option');
        debugger;
        for (var i = 0; i < data.length; i++) {
            $('#Month_Code').append('<option value="' + data[i].code + '">' + data[i].name + '</option');
        }

    });
}
function OnDateClick(i) {
    debugger;
    if ($('#DailyAttendanceList_0__Day' + (i + 1) + '_Date').val() != '' && $('#DailyAttendanceList_Count').val() != '0')
    {
        url = "/TimeOffice/MonthlyAttendanceReport/DailyAttendanceEditList?HeaderDate=";
        window.location.href = url + $('#DailyAttendanceList_0__Day' + (i + 1) + '_Date').val();
       
    }
}

function GetAttendanceDateList() {
    debugger
    //var AttenCode = $(sender).val();
    var AttenCode = $('#AttnDay_Code').val();
    $('#AttnDay_Code').val(AttenCode);
    $('.filterDropdownAS').val(AttenCode);
    var filter1 = $('#Time_PayRoll_BasicFilter').serialize();
    //var filter1 = $('#BasicFilter').serialize();

    var temp = filter1;
  


    
    var dataTable = $("#daily-attendance-edit-list-table").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/TimeOffice/MonthlyAttendanceReport/GetEditList",
            "type": "POST",


            "datatype": "json",


            'data':

                function (d) {
                    temp = '';




                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();






                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&AttendanceStatusCode=' + AttenCode + '&dummy=dummy';
                    d.filter = temp;


                    return JSON.stringify(d);
                },




            "dataSrc": function (d) {
                debugger
               
                $('#DailyAttendanceEditList_Count').val(d.recordsTotal);
                for (var i = 0, ien = d.data.length; i < ien; i++) {
                    
                    d.data[i].fullName = '<a data-bs-toggle="modal" data-target=""onclick="GetEmpLeaveDetails(' + "'" + d.data[i].employeeGI + "'" + ')"</a>'+ d.data[i].fullName 
                    /*d.data[i].department.userCode = '<a id="DailyAttedEdit" type="button" data-bs-toggle="tooltip" title="Edit" class="btn btn-icon btn-edit icon-only slider-btn"><i class="icon-xl fas fa-pencil-alt" onclick="onDailyAttedEditClick(' + "'" + d.data[i].employeeGI + "'" + ', ' + "'" + d.selectedDate + "'" + ',' + "'" + d.data[i].firstHalf.name+"'"+')"></i></a>'*/

                }

                return d.data;
            }

        },

        "columns": [

            { "data": "employeeID", "width": "8%" },
            { "data": "fullName", "width": " 11%" },
            { "data": "department.name", "width": "10%" },
            { "data": "location.name", "width": "10%" },
            { "data": "shift.name", "width": "7%" },
            { "data": "in", "width": "7%" },
            { "data": "out", "width": "7%" },
            { "data": "totalTime", "width": "7%" },
            { "data": "firstHalf.name", "width": " 6%" },
            { "data": "secondHalf.name", "width": "10%" },
            { "data": "remarks", "width": "12%" },
            /*{ "data": "department.userCode", "width": "5%" },*/

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
        //dataTable.search('').draw();
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
function validatedaEntry(val, id) {


    if (val.value == "") {
        $(val).addClass('is-invalid');
        $("#" + id + "").addClass('error-message');
        $("#" + id + "").html("This field is required");
    }
    else {
       
        $(val).removeClass('is-invalid');
        $("#" + id + "").removeClass('error-message');
        $("#" + id + "").html("");       
        $('#DailyAttendanceEditDetails_SecondHalf_Code').val($('#DailyAttendanceEditDetails_FirstHalf_Code').val());
        EnableDisableCoff(val,'FirstHalf');
    }
}

function EnableDisableCoff(sender,FH_SH)
{

  
    $.getJSON("/TimeOffice/MonthlyAttendanceReport/CheckIffCOFF", { AttendanceStatusCode: $(sender).val() }, function (data) {
    
        console.log(data);
        if (data.attendanceStatusType.code == 5)
            $("#entittleDateID").removeClass("hide");
        else {
            $('#DailyAttendanceEditDetails_CoffEntitleDate').val('');
            $("#entittleDateID").addClass("hide");
        }
        debugger
        if (FH_SH == "FirstHalf") {

            $("#DailyAttendanceEditDetails_FH_AttendStatusType_ShortName").val(data.attendanceStatusType.shortName);
            $("#DailyAttendanceEditDetails_SH_AttendStatusType_ShortName").val(data.attendanceStatusType.shortName);
        }
        else
        {
            $("#DailyAttendanceEditDetails_SH_AttendStatusType_ShortName").val(data.attendanceStatusType.shortName);
        }

        if ($("#DailyAttendanceEditDetails_FH_AttendStatusType_ShortName").val() == "COFF" && $("#DailyAttendanceEditDetails_SH_AttendStatusType_ShortName").val() == "COFF") {
            
                var length = $('#DailyAttendanceEditDetails_CoffEntitleDate').children('option').length;               
                 if (length == 1) {
                    debugger
                    swal.fire("No COFF Balance");
                    return false;
                }
            
        }

      
    });
}

function fillData() {

    
    var filter1 = $('#Time_PayRoll_BasicFilter').serialize();

    var temp = filter1;
    var month = $("#Month_Code").val();
    var PayrollGroup = $("#PayrollGroup_Code").val();
    var TabIndex = currentTab;

    /*  $('#JoiningReportTable').html('');*/
    var dataTable = $("#daily-attendance-entry-list-table").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',





        "ajax": {
            "url": "/TimeOffice/MonthlyAttendanceReport/GetList",
            "type": "POST",


            "datatype": "json",


            'data':

                function (d) {
                    temp = '';
                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();






                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&month=' + month + '&PayrollGroup=' + PayrollGroup + '&TabIndex=' + TabIndex + '&dummy=dummy';
                    d = temp;

                    /* showSpinner();*/
                    return JSON.stringify(d);
                },




            "dataSrc": function (d) {

                
                $('.AllCount').html(d.count.allCount);
                $('.AbsenteesamCount').html(d.count.absenteesamCount);
                $('.LeaveCount').html(d.count.leaveCount);

                $('#DailyAttendanceList_Count').val(d.recordsTotal);

                //$('#atte ').css("display", 'block');
                $('.dataTables_sizing ').css("display", 'block');

                for (var i = 0, ien = d.data.length; i < ien; i++) {



                    d.data[i].employeeName = '<a href = "/TimeOffice/MonthlyAttendanceReport/EmployeeWiseAttendance?EmployeeGI=' + d.data[i].employeeGI + '&Month=' + month + '">' + d.data[i].employeeName + ' </a >'

                    $('.d1').html(d.var1.day1_Footer);
                    $('.d2').html(d.var1.day2_Footer);
                    $('.d3').html(d.var1.day3_Footer);
                    $('.d4').html(d.var1.day4_Footer);
                    $('.d5').html(d.var1.day5_Footer);
                    $('.d6').html(d.var1.day6_Footer);
                    $('.d7').html(d.var1.day7_Footer);
                    $('.d8').html(d.var1.day8_Footer);
                    $('.d9').html(d.var1.day9_Footer);
                    $('.d10').html(d.var1.day10_Footer);
                    $('.d11').html(d.var1.day11_Footer);
                    $('.d12').html(d.var1.day12_Footer);
                    $('.d13').html(d.var1.day13_Footer);
                    $('.d14').html(d.var1.day14_Footer);
                    $('.d15').html(d.var1.day15_Footer);
                    $('.d16').html(d.var1.day16_Footer);
                    $('.d17').html(d.var1.day17_Footer);
                    $('.d18').html(d.var1.day18_Footer);
                    $('.d19').html(d.var1.day19_Footer);
                    $('.d20').html(d.var1.day20_Footer);
                    $('.d21').html(d.var1.day21_Footer);
                    $('.d22').html(d.var1.day22_Footer);
                    $('.d23').html(d.var1.day23_Footer);
                    $('.d24').html(d.var1.day24_Footer);
                    $('.d25').html(d.var1.day25_Footer);
                    $('.d26').html(d.var1.day26_Footer);
                    $('.d27').html(d.var1.day27_Footer);
                    $('.d28').html(d.var1.day28_Footer);
                    $('.d29').html(d.var1.day29_Footer);
                    $('.d30').html(d.var1.day30_Footer);
                    $('.d31').html(d.var1.day31_Footer);

                    $('#DailyAttendanceList_0__Day1_Date').val(d.data[i].day1_Date)
                    $('#DailyAttendanceList_0__Day2_Date').val(d.data[i].day2_Date)
                    $('#DailyAttendanceList_0__Day3_Date').val(d.data[i].day3_Date)
                    $('#DailyAttendanceList_0__Day4_Date').val(d.data[i].day4_Date)
                    $('#DailyAttendanceList_0__Day5_Date').val(d.data[i].day5_Date)
                    $('#DailyAttendanceList_0__Day6_Date').val(d.data[i].day6_Date)
                    $('#DailyAttendanceList_0__Day7_Date').val(d.data[i].day7_Date)
                    $('#DailyAttendanceList_0__Day8_Date').val(d.data[i].day8_Date)
                    $('#DailyAttendanceList_0__Day9_Date').val(d.data[i].day9_Date)
                    $('#DailyAttendanceList_0__Day10_Date').val(d.data[i].day10_Date)
                    $('#DailyAttendanceList_0__Day11_Date').val(d.data[i].day11_Date)
                    $('#DailyAttendanceList_0__Day12_Date').val(d.data[i].day12_Date)
                    $('#DailyAttendanceList_0__Day13_Date').val(d.data[i].day13_Date)
                    $('#DailyAttendanceList_0__Day14_Date').val(d.data[i].day14_Date)
                    $('#DailyAttendanceList_0__Day15_Date').val(d.data[i].day15_Date)
                    $('#DailyAttendanceList_0__Day16_Date').val(d.data[i].day16_Date)
                    $('#DailyAttendanceList_0__Day17_Date').val(d.data[i].day17_Date)
                    $('#DailyAttendanceList_0__Day18_Date').val(d.data[i].day18_Date)
                    $('#DailyAttendanceList_0__Day19_Date').val(d.data[i].day19_Date)
                    $('#DailyAttendanceList_0__Day20_Date').val(d.data[i].day20_Date)
                    $('#DailyAttendanceList_0__Day21_Date').val(d.data[i].day21_Date)
                    $('#DailyAttendanceList_0__Day22_Date').val(d.data[i].day22_Date)
                    $('#DailyAttendanceList_0__Day23_Date').val(d.data[i].day23_Date)
                    $('#DailyAttendanceList_0__Day24_Date').val(d.data[i].day24_Date)
                    $('#DailyAttendanceList_0__Day25_Date').val(d.data[i].day25_Date)
                    $('#DailyAttendanceList_0__Day26_Date').val(d.data[i].day26_Date)
                    $('#DailyAttendanceList_0__Day27_Date').val(d.data[i].day27_Date)
                    $('#DailyAttendanceList_0__Day28_Date').val(d.data[i].day28_Date)
                    $('#DailyAttendanceList_0__Day29_Date').val(d.data[i].day29_Date)
                    $('#DailyAttendanceList_0__Day30_Date').val(d.data[i].day30_Date)
                    $('#DailyAttendanceList_0__Day31_Date').val(d.data[i].day31_Date)




                    d.data[i].day1 = '<span style=color:' + d.data[i].day1_Color + '>' + d.data[i].day1 + '</span>'
                    d.data[i].day2 = '<span style=color:' + d.data[i].day2_Color + '>' + d.data[i].day2 + '</span>'
                    d.data[i].day3 = '<span style=color:' + d.data[i].day3_Color + '>' + d.data[i].day3 + '</span>'
                    d.data[i].day4 = '<span style=color:' + d.data[i].day4_Color + '>' + d.data[i].day4 + '</span>'
                    d.data[i].day5 = '<span style=color:' + d.data[i].day5_Color + '>' + d.data[i].day5 + '</span>'
                    d.data[i].day6 = '<span style=color:' + d.data[i].day6_Color + '>' + d.data[i].day6 + '</span>'
                    d.data[i].day7 = '<span style=color:' + d.data[i].day7_Color + '>' + d.data[i].day7 + '</span>'
                    d.data[i].day8 = '<span style=color:' + d.data[i].day8_Color + '>' + d.data[i].day8 + '</span>'
                    d.data[i].day9 = '<span style=color:' + d.data[i].day9_Color + '>' + d.data[i].day9 + '</span>'
                    d.data[i].day10 = '<span style=color:' + d.data[i].day10_Color + '>' + d.data[i].day10 + '</span>'
                    d.data[i].day11 = '<span style=color:' + d.data[i].day11_Color + '>' + d.data[i].day11 + '</span>'
                    d.data[i].day12 = '<span style=color:' + d.data[i].day12_Color + '>' + d.data[i].day12 + '</span>'
                    d.data[i].day13 = '<span style=color:' + d.data[i].day13_Color + '>' + d.data[i].day13 + '</span>'
                    d.data[i].day14 = '<span style=color:' + d.data[i].day14_Color + '>' + d.data[i].day14 + '</span>'
                    d.data[i].day15 = '<span style=color:' + d.data[i].day15_Color + '>' + d.data[i].day15 + '</span>'
                    d.data[i].day16 = '<span style=color:' + d.data[i].day16_Color + '>' + d.data[i].day16 + '</span>'
                    d.data[i].day17 = '<span style=color:' + d.data[i].day17_Color + '>' + d.data[i].day17 + '</span>'
                    d.data[i].day18 = '<span style=color:' + d.data[i].day18_Color + '>' + d.data[i].day18 + '</span>'
                    d.data[i].day19 = '<span style=color:' + d.data[i].day19_Color + '>' + d.data[i].day19 + '</span>'
                    d.data[i].day20 = '<span style=color:' + d.data[i].day20_Color + '>' + d.data[i].day20 + '</span>'
                    d.data[i].day21 = '<span style=color:' + d.data[i].day21_Color + '>' + d.data[i].day21 + '</span>'
                    d.data[i].day22 = '<span style=color:' + d.data[i].day22_Color + '>' + d.data[i].day22 + '</span>'
                    d.data[i].day23 = '<span style=color:' + d.data[i].day23_Color + '>' + d.data[i].day23 + '</span>'
                    d.data[i].day24 = '<span style=color:' + d.data[i].day24_Color + '>' + d.data[i].day24 + '</span>'
                    d.data[i].day25 = '<span style=color:' + d.data[i].day25_Color + '>' + d.data[i].day25 + '</span>'
                    d.data[i].day26 = '<span style=color:' + d.data[i].day26_Color + '>' + d.data[i].day26 + '</span>'
                    d.data[i].day27 = '<span style=color:' + d.data[i].day27_Color + '>' + d.data[i].day27 + '</span>'
                    d.data[i].day28 = '<span style=color:' + d.data[i].day28_Color + '>' + d.data[i].day28 + '</span>'
                    d.data[i].day29 = '<span style=color:' + d.data[i].day29_Color + '>' + d.data[i].day29 + '</span>'
                    d.data[i].day30 = '<span style=color:' + d.data[i].day30_Color + '>' + d.data[i].day30 + '</span>'
                    d.data[i].day31 = '<span style=color:' + d.data[i].day31_Color + '>' + d.data[i].day31 + '</span>'





                }

                hideSpinner();
                return d.data;
            }

        },

        "columns": [
            { "data": "employeeID", "width": "9%" },
            { "data": "employeeName", "width": "15%" },
            { "data": "day1", "width": "3%" },
            { "data": "day2", "width": "3%" },
            { "data": "day3", "width": "3%" },
            { "data": "day4", "width": "3%" },
            { "data": "day5", "width": "3%" },
            { "data": "day6", "width": "3%" },
            { "data": "day7", "width": "3%" },
            { "data": "day8", "width": "3%" },
            { "data": "day9", "width": "3%" },
            { "data": "day10", "width": "3%" },
            { "data": "day11", "width": "3%" },
            { "data": "day12", "width": "3%" },
            { "data": "day13", "width": "3%" },
            { "data": "day14", "width": "3%" },
            { "data": "day15", "width": "3%" },
            { "data": "day16", "width": "3%" },
            { "data": "day17", "width": "3%" },
            { "data": "day18", "width": "3%" },
            { "data": "day19", "width": "3%" },
            { "data": "day20", "width": "3%" },
            { "data": "day21", "width": "3%" },
            { "data": "day22", "width": "3%" },
            { "data": "day23", "width": "3%" },
            { "data": "day24", "width": "3%" },
            { "data": "day25", "width": "3%" },
            { "data": "day26", "width": "3%" },
            { "data": "day27", "width": "3%" },
            { "data": "day28", "width": "3%" },
            { "data": "day29", "width": "3%" },
            { "data": "day30", "width": "3%" },
            { "data": "day31", "width": "3%" },
        ],



        
       //"dom": '<t><"my-wrapper"<"my-inner2"l><"my-inner1"i><"my-inner3"p>>',
       //"": '<t><"my-wrapper"<"my-inner2"l><"my-inner1"i><"my-inner3"p>>',
        //"dom": 'rt<"bottom"><"my-wrapper"<"my-inner2"l><"my-inner1"i><"my-inner3"p>>',
        "dom":'rt<"bottom"><"my-wrapper"<"my-inner2"l><"my-inner1"i><"my-inner3"p>>',
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


        debugger
        var val = $('#DailyAttendanceList_Count').val();

        if (val !=0) {
            debugger
            //$('#attendance-test ').css("display", 'block');
            $('.dataTables_sizing ').css("display", 'block');
        
        }
        else {
            debugger
            //$('#attendance-test ').css("display", 'none');
            $('.dataTables_sizing ').css("display", 'none');
        }

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


    /* getTabCount();*/




}










function validateToDate(val, id) {
    debugger;
    if ($(val).val() == "") {
        $(val).addClass('is-invalid');
        $("#" + id + "").addClass('error-message');
        $("#" + id + "").html("This field is required");
    }
    else {

        var ToDate = new Date($('#Employee_DailyAttendanceBatchUpdate_To').val());
        var fromDate = new Date($('#Employee_DailyAttendanceBatchUpdate_From').val());
        var d = new Date();
        var TDate = new Date(ToDate.toLocaleDateString());
        var FDate = new Date(fromDate.toLocaleDateString());

        if (FDate > TDate) {
            $(val).addClass('is-invalid');
            $("#" + id + "").addClass('error-message');
            $("#" + id + "").html("To date should be greater than From date");
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


function onFilterClick(FormName) {
//    debugger
///*    if (validateEntryDropdowns()) {*/
  
        $.ajax({

            type: 'GET',
            url: '/TimeOffice/MonthlyAttendanceReport/Filter',
            dataType: 'html',
            data: { FormName: FormName },

            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('body').append('<div class="form-overlay"></div>');
                    if (FormName == "outerList") {
                        $('#DailyEntryFilter').html("");
                        $('#DailyEntryFilter').html(result);
                    }
                    else {
                        $('#DailyDateWiseFilter').html("");
                        $('#DailyDateWiseFilter').html(result);
                    }
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
  
       
  /*  *//*}*/
}
function onTabClick(tabName,tabIndex)
{
    debugger
    var month = $("#Month_Code").val();
    var PayrollGroup = $("#PayrollGroup_Code").val();
    $("." + prevTab).removeClass("active");
    $("." + tabName).addClass("active");
    currentTab = tabIndex;
    $('#TabCode').val(tabIndex);
    if (tabIndex == 1) {


        $(".LeavePercentageDiv").hide();
        $(".AbsentismPerceantageDiv").show();
    }
    else if (tabIndex == 2) {
        $(".LeavePercentageDiv").show();
        $(".AbsentismPerceantageDiv").hide();
    }
    if (month == 0 || PayrollGroup == 0)
        $('#dailyAttendanceEntryList').hide();
    else
    {
        /*showSpinner();*/
    //    var url = "/TimeOffice/DailyAttendanceEntry/DailyAttendanceEntryList";        
    //    $('.AttendanceEntryList').load(url, { month: month, PayRollGrp: PayrollGroup, TabIndex: tabIndex, MonthName: $("#Month_Code option:selected").text(), payroll: $("#PayrollGroup_Code option:selected").text() }, function () { hideSpinner(); });

        //$.ajax({

        //    type: 'GET',
        //    url: '/TimeOffice/DailyAttendanceEntry/DailyAttendanceEntryList',
        //    dataType: 'html',
        //    data: { month: month, PayRollGrp: PayrollGroup, TabIndex: tabIndex, MonthName: $("#Month_Code option:selected").text(), payroll: $("#PayrollGroup_Code option:selected").text() },

        //    success: function (result) {
        //        debugger
        //        if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
        //            window.location.reload();
        //        }
        //        else {
        //            $('.AttendanceEntryList').html("");
        //            $('.AttendanceEntryList').html(result);
        //        }
        //        hideSpinner();


        //    },
        //    error: function (xhr, textStatus, errorThrown) {
        //        debugger
        //        document.body.id = "kt_body";
        //        $('body').html(xhr.responseText);
        //        hideSpinner();

        //    }
        //});

        fillData();


    }
    prevTab = tabName;
}
function TabsDropdown(TabIndex)
{
    debugger
    var tabName = GetTab($("#TabCode").prop('selectedIndex'))
    var month = $("#Month_Code").val();
    var PayrollGroup = $("#PayrollGroup_Code").val();
    $("." + prevTab).removeClass("active");
    $("." + tabName).addClass("active");
    currentTab = TabIndex;   
    if (month == 0 || PayrollGroup == 0)
        $('#dailyAttendanceEntryList').hide();
    else
    {
        showSpinner();
        //var url = "/TimeOffice/DailyAttendanceEntry/DailyAttendanceEntryList";
        //$('.AttendanceEntryList').load(url, { month: month, PayRollGrp: PayrollGroup, TabIndex: TabIndex }, function () { hideSpinner(); });

        $.ajax({

            type: 'GET',
            url: '/TimeOffice/MonthlyAttendanceReport/DailyAttendanceEntryList',
            dataType: 'html',
            data: { month: month, PayRollGrp: PayrollGroup, TabIndex: TabIndex },

            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('.AttendanceEntryList').html("");
                    $('.AttendanceEntryList').html(result);
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
    prevTab = tabName;
}



function GetTab(TabCode) {
    if (TabCode == 0)
        return 'all'
    else if (TabCode == 1)
        return 'Absenteeism';   
    else
        return 'Leave';

}


function onAttendanceStatusChange(Date, sender) {
    debugger
    showSpinner();
    var AttenCode = $(sender).val();
    $('#AttnDay_Code').val(AttenCode);
    $('.filterDropdownAS').val(AttenCode);
    //var url = "/TimeOffice/DailyAttendanceEntry/DailyAttendanceEditListPartial";
    //$('#dailyAttenEditListPartial').load(url, { HeaderDate: Date, AttendanceStatusCode: AttenCode }, function () { hideSpinner(); });

    $.ajax({

        type: 'GET',
        url: '/TimeOffice/MonthlyAttendanceReport/DailyAttendanceEditListPartial',
        dataType: 'html',
        data: { HeaderDate: Date, AttendanceStatusCode: AttenCode },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#dailyAttenEditListPartial').html("");
                $('#dailyAttenEditListPartial').html(result);
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
   

function onAbsentDaysCheckboxChange(sender,EmpGuid )
{
    debugger
    var month = $("#Month_Code").val();
    //var checkBox = document.getElementById("ShowAbsentDays");
    if ($(sender).is(':checked') == true) {
        $('#ShowAbsentDays').prop('checked', true);
        $('.absentDays').prop('checked', true);
        var ABSTATUS = 1;
    }
    else {
        $('#ShowAbsentDays').prop('checked', false);
        $('.absentDays').prop('checked', false);
        var ABSTATUS = 0;
    }
    //var url = '/TimeOffice/DailyAttendanceEntry/EmployeeWiseAttendanceList' + "?EmployeeGI=" + EmpGuid + "&Month=" + month + "&ABSTATUS=" + ABSTATUS;
    //   $('#emp-Attend-List').load(url, function () { });
    $.ajax({

        type: 'GET',
        url: '/TimeOffice/MonthlyAttendanceReport/EmployeeWiseAttendanceList',
        dataType: 'html',
        data: { EmployeeGI: EmpGuid, Month: month, ABSTATUS: ABSTATUS},

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#emp-Attend-List').html("");
                $('#emp-Attend-List').html(result);
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

function onfilterClick(FormName) {
    debugger

    var url = "/TimeOffice/MonthlyAttendanceReport/ApplyFilter?FormName="+FormName;
    $('body').append('<div class="form-overlay"></div>');
    if (FormName =='entryList')
        $('#FilterPartialEntry').load(url, function () { });
    else
        $('#FilterPartialEdit').load(url, function () { });
}

function ApplyFilter(Mode, form) {
    debugger
    if (form == 'outerList') {
        ApplyFilterEntry(Mode);
    }
    else {
        ApplyFilterEmpWiseAttendance(Mode);
    }
}

function ApplyFilterEmpWiseAttendance(Mode)
{
    debugger

    if (Mode == "apply") {
        //showSpinner();
        if (filterApplicable >= 1) {

            GetAttendanceDateList();
            CloseSlider('TP_filter');
            $('#filterForm').removeClass('filter-apply');

        }
        else {
            GetAttendanceDateList();
            CloseSlider('TP_filter');

            $('#filterForm').addClass('filter-apply');
        }


    }
    else {
        //showSpinner();
        filterApplicable = 1;

        $('.reset').val('');
        $('#Optional_Filter_EmploymentStatus_Code').val(0);
        $('#filterForm').removeClass('filter-apply');
        GetAttendanceDateList();


    }

}


function ApplyFilterEntry(Mode) {
    debugger
    if (Mode == "apply") {
        //showSpinner();
        if (filterApplicable >= 1) {

            fillData();
            CloseSlider('TP_filter');
              
        }
        else {
            fillData();
            CloseSlider('TP_filter');
          
            $('#filterForm').addClass('filter-apply');
        }

       
    }
    else {
        //showSpinner();
        filterApplicable = 1;

        $('.reset').val('');
        $('#Optional_Filter_EmploymentStatus_Code').val(0);
        $('#filterForm').removeClass('filter-apply');
        fillData();
      

    }

}

function onBatchUpdateClick() {
    debugger;
   
    $.ajax(
        {
            url: "/TimeOffice/MonthlyAttendanceReport/DailyAttendanceBatchUpdate",
            dataType: "html",
            success: function (data)
            {
                $('body').append('<div class="form-overlay"></div>');
                $('#batchUpdateDailySlider').html("");
                $('#batchUpdateDailySlider').html(data);
            },
            error: function (e) {
                swal.fire('Error: ' + e);
            }
        });
}




function validateFields() {
    debugger;
    var i = 0;
    if (!validateById('#BatchUpDate_FirstHalfOld_Code', 'fOld')) {
        i++;
    }
    if (!validateById('#BatchUpDate_FirstHalfNew_Code', 'fNew')) {
        i++;
    }
    if (!validateById('#BatchUpDate_SecondHalfOld_Code', 'sOld')) {
        i++;
    }
    if (!validateById('#BatchUpDate_SecondHalfNew_Code', 'sNew')) {
        i++;
    }
    if (i == 0)
        return true;
    else
        return false;
    
}
function dailyAttendanceBatchUpdateSave(HeaderDate) {

    debugger;
    if (validateFields()) {
        swal.fire({
            text: "Do you want to Update?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({

                    type: 'POST',
                    url: '/TimeOffice/MonthlyAttendanceReport/DailyattendanceEntryBatchUpdate',
                    dataType: 'json',
                    data: $('#dailyAttendanceBatch').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire("Updated Successfully").then(function () {
                                CloseSlider('BatchUpdateDaily');
                                var url = '/TimeOffice/MonthlyAttendanceReport/DailyAttendanceEditList?HeaderDate=' + HeaderDate;
                                window.location.href = url;


                            });

                        }
                        else {
                            swal.fire(result.message).then(function () {
                                var url = '/TimeOffice/MonthlyAttendanceReport/DailyAttendanceBatchUpdate';
                                $('#batchUpdateDailySlider').load(url, function () { });
                                $('body').append('<div class="form-overlay"></div>');

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
    }
   
        

      
   
}
function setDatePicker() {
    debugger
    var arrows;
    if (KTUtil.isRTL()) {
        arrows = {
            leftArrow: '<i class="la la-angle-right"></i>',
            rightArrow: '<i class="la la-angle-left"></i>'
        }
    } else {
        arrows = {
            leftArrow: '<i class="la la-angle-left"></i>',
            rightArrow: '<i class="la la-angle-right"></i>'
        }
    }

    $('.kt_datepicker_11, .kt_datepicker_11_validate').datepicker({
        rtl: KTUtil.isRTL(),
        todayHighlight: true,
        orientation: "bottom left",
        templates: arrows,
        endDate: _GetFormattedDate($("#Employee_DailyAttendanceEditDetails_" + (global_count- 1) + "__Date").val()),
          
        autoclose: true,



        format: "dd-M-yyyy",
        orientation: "auto",
        startDate: _GetFormattedDate($("#Employee_DailyAttendanceEditDetails_0__Date").val()),
        clearBtn: true
    });

}
function onEmpWiseBatchUpdateClick(EmployeeGI, selectedDate)
{
 
    debugger;
    var HeaderDate = new Date(selectedDate);
    let name = month[HeaderDate.getMonth()];
    $.ajax(
        {
            url: "/TimeOffice/MonthlyAttendanceReport/EmployeeWiseDailyAttendanceBatchUpdate?EmployeeGI=" + EmployeeGI + "&MonthName=" + name,
            dataType: "html",
            success: function (data) {
                //if ($($.parseHTML(Data)).filter(".Login_Session").length > 0) {
                //    window.location.reload();
                //}
                //else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('#empAttedBatchUpdateSlider').html("");
                   $('#empAttedBatchUpdateSlider').html(data);
                debugger
                setDatePicker();

              /*  }*/
            },
            error: function (xhr, textStatus, errorThrown) {
                debugger
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);
              

            }
        });

 
}

function EmployeedailyAttendanceBatchUpdateSave() {
    debugger;
    if (validateBatchUpdate()) {

        var month = $('#Month_Code').val();
        var EmpGuid = $('#Employee_DailyAttendanceBatchUpdate_EmployeeGI').val();
        var MonthName = $('#Employee_DailyAttendanceBatchUpdate_Month_Name').val();

        swal.fire({
            text: "Do you want to Update?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({

                    type: 'POST',
                    url: '/TimeOffice/MonthlyAttendanceReport/EmployeeWiseAttendanceEntryBatchUpdate',
                    dataType: 'json',
                    data: $('#empAttendanceBatch').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire("Updated Successfully").then(function () {
                                CloseSlider('BatchUpdateEmp');
                                var url = '/TimeOffice/DailyAttendanceEntry/EmployeeWiseAttendance';
                                window.location.href = url + "?EmployeeGI=" + EmpGuid + "&Month=" + month;


                            });

                        }
                        else
                            swal.fire(result.message);
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

    }
    else
        return false;

        
   
}

function onDailyAttedEditClick(EmployeeGI, HeaderDate, mode) {
    debugger   
    if (mode == "")
        Mode = "CREATE";
    else
        Mode = "EDIT";
    $.ajax({

        type: 'GET',
        url: '/TimeOffice/MonthlyAttendanceReport/DailyAttendanceEdit',
        dataType: 'html',
        data: { EmployeeGI: EmployeeGI, HeaderDate: HeaderDate, Mode: Mode },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('body').append('<div class="form-overlay"></div>');
                $('#DailyAttedEditSlider').html("");
                $('#DailyAttedEditSlider').html(result);
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
function dailyAttendanceSave(date) {
    debugger;  
    if (validateEmpWiseEdit()) {
        date_string = date

        var EmpGuid = $('#DailyAttendanceEditDetails_EmployeeGI').val();

        debugger;
        swal.fire({
            text: "Do you want to Modify?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({

                    type: 'POST',
                    url: '/TimeOffice/MonthlyAttendanceReport/DailyAttendanceEditSave',
                    dataType: 'json',
                    data: $('#daily_Edit').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire("Modified Successfully").then(function () {
                                debugger
                                CloseSlider('dailyAttenEdit');
                                url = '/TimeOffice/DailyAttendanceEntry/DailyAttendanceEditList';
                                window.location.href = url + "?HeaderDate=" + date_string;


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
    }
    else
        return false;
    
}


function onEmployeeDailyAttendEditClick(EmployeeGI,Date,mode) {
    debugger
    if (mode == "")
        Mode = "CREATE";
    else
        Mode = "EDIT";
    $.ajax({

        type: 'GET',
        url: '/TimeOffice/MonthlyAttendanceReport/EmployeeDailyAttendanceEdit',
        dataType: 'html',
        data: { EmployeeGI: EmployeeGI, Date: Date, Mode:Mode },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('body').append('<div class="form-overlay"></div>');
                $('#empAttedEditSlider').html("");
                $('#empAttedEditSlider').html(result);
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


function onEmployeeDailyAttendSaveClick() {
    debugger;
    if (validateEmpWiseEdit()) {
        var Date = $('#DailyAttendanceEditDetails_SelectedDate').val();
        var month = $('#Month_Code').val();
        var EmpGuid = $('#DailyAttendanceEditDetails_EmployeeGI').val();

        debugger;
        swal.fire({
            text: "Do you want to Modify?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({

                    type: 'POST',
                    url: '/TimeOffice/MonthlyAttendanceReport/DailyAttendanceEditSave',
                    dataType: 'json',
                    data: $('#employee_Edit').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire("Modified Successfully").then(function () {
                                debugger
                                CloseSlider('employee-wise-edit');
                                url = '/TimeOffice/DailyAttendanceEntry/EmployeeWiseAttendance';
                                window.location.href = url + "?EmployeeGI=" + EmpGuid + "&Month=" + month;


                            });

                        }
                        else {
                            swal.fire(result.message).then(function () {
                                debugger                         

                                $.ajax({

                                    type: 'GET',
                                    url: '/TimeOffice/MonthlyAttendanceReport/EmployeeDailyAttendanceEdit',
                                    dataType: 'html',
                                    data: { EmployeeGI: EmpGuid, Date: Date },

                                    success: function (result) {
                                        debugger
                                        if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                                            window.location.reload();
                                        }
                                        else {
                                            $('#DailyAttedEditSlider').html("");
                                            $('#DailyAttedEditSlider').html(result);
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
    }
    else
        return false;
}




function onProceedClick()
{
    debugger
    var month = $("#Month_Code").val();
    var PayrollGroup = $("#PayrollGroup_Code").val();
    if ($('#Month_Code').val() != "" && $('#PayrollGroup_Code').val() != "")
    {
        showSpinner();
        //var url = "/TimeOffice/DailyAttendanceEntry/DailyAttendanceEntryList";
        //$('.AttendanceEntryList').load(url, { month: month, PayRollGrp: PayrollGroup, TabIndex: currentTab }, function () { hideSpinner(); });


        $.ajax({

            type: 'GET',
            url: '/TimeOffice/MonthlyAttendanceReport/DailyAttendanceEntryList',
            dataType: 'html',
            data: { month: month, PayRollGrp: PayrollGroup, TabIndex: currentTab},

            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('.AttendanceEntryList').html("");
                    $('.AttendanceEntryList').html(result);
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
     
}

function onEmpWiseProceedClick(sender,EmployeeGI) {
    debugger;
    var checkBox = document.getElementById("ShowAbsentDays");
    if (checkBox.checked == true)
        var ABSTATUS = 1;
    else
        var ABSTATUS = 0;
    var month = sender.value;
    $('#Month_Code').val(month);
    $('.filterDropdownMonth').val(month)
    if (validateById('#Month_Code', 'employeeMonth')) {
        showSpinner();


        $.ajax({

            type: 'GET',
            url: '/TimeOffice/MonthlyAttendanceReport/EmployeeWiseAttendanceList',
            dataType: 'html',
            data: { EmployeeGI: EmployeeGI, Month: month, ABSTATUS: ABSTATUS},

            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('#emp-Attend-List').html("");
                    $('#emp-Attend-List').html(result);
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
}




function GetEmpLeaveDetails(EmployeeGI) {
    debugger

    $.ajax({

        type: 'GET',
        url: '/TimeOffice/MonthlyAttendanceReport/EmployeeLeaveDetails',
        dataType: 'html',
        data: { EmployeeGI: EmployeeGI },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                debugger
                var m = $('.daEntry_LeaveDetails');
                m.find('.modal-body').html('');
                m.find('.modal-body').html(result);
                debugger
                var emp = $("#DailyAttendanceEditDetails_FullName").val();
                $('.modal-title').html('Leave Details( '+emp+' )');
                m.modal('show');
            }
         


        },
        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
            hideSpinner();

        }
    });




}


function exportToexcel_DailyAttendanceEntry()
{
    debugger;
    var MName = $("#Month_Code option:selected").text();
    var PGroup = $("#PayrollGroup_Code option:selected").text();

    if (!($('#daily-attendance-entry-list-table').length))
        swal.fire("No data found to export");
    var TableLength = $('#DailyAttendanceList_Count').val();
    if (TableLength > 0) {
        url = '/TimeOffice/MonthlyAttendanceReport/ExportToexcel_DailyAttendanceEntryList?MName=' + MName + '&PGroup=' + PGroup + '&totRecords=' + TableLength;
            window.location.href = url;
            }
            else {
                swal.fire("No data found to export");
            }
}






function ExportToExcel_EmployeeAttendanceEDitList(EmployeeGI) {
    debugger;
    var Month = $("#Month_Code").val();
    var MonthName = $("#Month_Code option:selected").text();
    var checkBox = document.getElementById("ShowAbsentDays");
    if (checkBox.checked == true)
        var ABSTATUS = 1;
    else
        var ABSTATUS = 0;

    var TableLength = $('#Employee_DailyAttendanceEditDetails_Count').val();

    if (TableLength > 0) {
        url = '/TimeOffice/MonthlyAttendanceReport/ExportToExcel_EmployeeAttendanceEDitList';
        window.location.href = url + "?EmployeeGI=" + EmployeeGI + "&Month=" + Month + "&ABSTATUS=" + ABSTATUS + "&MonthName=" + MonthName;
    }
    else {
        swal.fire("No data found to export");
    }
}



function exportToExcel_DailyAttendanceEDitList(HeaderDate)
{
    debugger
    var AttenCode = $("#AttnDay_Code").val();
    var AttenName = $("#AttnDay_Code option:selected").text();
    var TableLength = $('#DailyAttendanceEditList_Count').val();
    if (TableLength > 0) {
        url = '/TimeOffice/MonthlyAttendanceReport/ExportToExcel_DailyAttendanceEDitList';
        window.location.href = url + "?HeaderDate=" + HeaderDate + "&AttendanceStatusCode=" + AttenCode + "&AttendanceStatusName=" + AttenName + "&totalRows=" + TableLength;
    }
    else {
        swal.fire("No data found to export");
    }
}

function validateEmpWiseEdit() {

    var i = 0;
    if (!validateById('#DailyAttendanceEditDetails_FirstHalf_Code', 'da_emp_edit_from')) {
        i++;
    }
    if (!validateById('#DailyAttendanceEditDetails_SecondHalf_Code', 'da_emp_edit_to')) {
        i++;
    }
   
    if ($("#DailyAttendanceEditDetails_FH_AttendStatusType_ShortName").val() == "COFF" || $("#DailyAttendanceEditDetails_SH_AttendStatusType_ShortName").val() == "COFF") {
       
        if ($("#DailyAttendanceEditDetails_SH_AttendStatusType_ShortName").val() != "COFF" || $("#DailyAttendanceEditDetails_FH_AttendStatusType_ShortName").val() != "COFF") {
            swal.fire("COFF cant be applited for Half day");
            return false;
        }

        else {

            var length = $('#DailyAttendanceEditDetails_CoffEntitleDate').children('option').length;
            if (length > 1)
            { 
                if (!validateById('#DailyAttendanceEditDetails_CoffEntitleDate', 'da_emp_edit_coff')) 
                    i++;
             }
                else {
                    debugger
                    swal.fire("No COFF Balance");
                    return false;
                }
        }
    }
    if (i == 0)
        return true;
    else
        return false;
}
function validateBatchUpdate() {
    debugger
    var i = 0;
    if (!validateById('#Employee_DailyAttendanceBatchUpdate_From', 'da_emp_from')) {   
        i++;
    }
    if (!validateById('#Employee_DailyAttendanceBatchUpdate_To', 'da_emp_to')) {
        i++;
    }
    if (!validateById('#Employee_DailyAttendanceBatchUpdate_FirstHalf_Code', 'da_emp_first')) {
        i++;
    }
    if (!validateById('#Employee_DailyAttendanceBatchUpdate_SecondHalf_Code', 'da_emp_second')) {
        i++;
    }

    if (i == 0)
        return true;
    else
        return false;
}