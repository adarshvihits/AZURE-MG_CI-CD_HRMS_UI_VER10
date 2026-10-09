
 var global_Month=0;
 var global_CIRCLE=0;
var global_PayrollGroup = 0;
function validateshift(val, id) {
 debugger
    if (val.value == -1) {
        $(val).addClass('is-invalid');
        $("#" + id + "").addClass('error-message');
        $("#" + id + "").html("This field is required");
    }
    else {
        $(val).removeClass('is-invalid');
        $("#" + id + "").removeClass('error-message');
        $("#" + id + "").html("");       
        return val.value;
    }
}
function fillMonth(sender) {
    debugger;


    $.getJSON("/TimeOffice/DutyRoster/GetMonth", { PayrollGrp: $(sender).val() }, function (data) {
        
        $('#Month_Code option').remove();    
        $('#Month_Code').append('<option value>--Select-- </option');
       
        for (var i = 0; i < data.length; i++) {
            debugger
            $('#Month_Code').append('<option value="' + data[i].code + '">' + data[i].name + '</option');
        }      
    });    

}


function fillData() {


    var filter1 = $('#Time_PayRoll_BasicFilter').serialize();

    var temp = filter1;
    var month = $("#Month_Code").val();
    var PayrollGroup = $("#PayrollGroup_Code").val();
    var CIRCLE = $("#Circle_Code").val();
    var shifttype = $('#ShiftType_Code').val();
    if ($('#Month_Code').val() != '' /*&& $('#PayrollGroup_Code').val() != ''*/ && $('#Circle_Code').val() != '') {
        var dataTable = $("#dutyroster-entry-list-table").DataTable({

            "pagingType": 'full_numbers',
            "destroy": 'true',
            "ajax": {
                "url": "/TimeOffice/DutyRoster/GetList",
                "type": "POST",
                "datatype": "json",
                'data':

                    function (d) {
                        temp = '';
                        var start = d.start;

                        var searchValue = $('#dataTableSearch').val();
                        temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&month=' + month + '&PayrollGroup=' + PayrollGroup + '&CIRCLE=' + CIRCLE + '&ShiftType=' + shifttype + '&dummy=dummy';
                        d = temp;
                        return JSON.stringify(d);
                    },
                "dataSrc": function (d) {

                    $('#DutyRosterList_Count').val(d.recordsTotal);

                    $('.dataTables_sizing ').css("display", 'block');
                    for (let i = 0; i < 31; i++) {

                        $('#headerDate_' + i).attr('onclick', "DutyRosterDateWise(" + (i + 1) + "," + d.count.month.code + "," + d.count.payrollGroup.code + "," + d.count.circle.code + ")")
                    }
                    for (var i = 0, ien = d.data.length; i < ien; i++) {

                        d.data[i].employeeName = '<a href = "/TimeOffice/DutyRoster/EmployeeDutyRosterEditDetails?EmployeeGI=' + d.data[i].employeeGI + '&Month=' + month + '&PayrollGroup=' + PayrollGroup + '&CIRCLE=' + CIRCLE + '">' + d.data[i].employeeName + ' </a >'

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

                        $('#DutyRosterList_0__Day1_Date').val(d.data[i].day1_Date)
                        $('#DutyRosterList_0__Day2_Date').val(d.data[i].day2_Date)
                        $('#DutyRosterList_0__Day3_Date').val(d.data[i].day3_Date)
                        $('#DutyRosterList_0__Day4_Date').val(d.data[i].day4_Date)
                        $('#DutyRosterList_0__Day5_Date').val(d.data[i].day5_Date)
                        $('#DutyRosterList_0__Day6_Date').val(d.data[i].day6_Date)
                        $('#DutyRosterList_0__Day7_Date').val(d.data[i].day7_Date)
                        $('#DutyRosterList_0__Day8_Date').val(d.data[i].day8_Date)
                        $('#DutyRosterList_0__Day9_Date').val(d.data[i].day9_Date)
                        $('#DutyRosterList_0__Day10_Date').val(d.data[i].day10_Date)
                        $('#DutyRosterList_0__Day11_Date').val(d.data[i].day11_Date)
                        $('#DutyRosterList_0__Day12_Date').val(d.data[i].day12_Date)
                        $('#DutyRosterList_0__Day13_Date').val(d.data[i].day13_Date)
                        $('#DutyRosterList_0__Day14_Date').val(d.data[i].day14_Date)
                        $('#DutyRosterList_0__Day15_Date').val(d.data[i].day15_Date)
                        $('#DutyRosterList_0__Day16_Date').val(d.data[i].day16_Date)
                        $('#DutyRosterList_0__Day17_Date').val(d.data[i].day17_Date)
                        $('#DutyRosterList_0__Day18_Date').val(d.data[i].day18_Date)
                        $('#DutyRosterList_0__Day19_Date').val(d.data[i].day19_Date)
                        $('#DutyRosterList_0__Day20_Date').val(d.data[i].day20_Date)
                        $('#DutyRosterList_0__Day21_Date').val(d.data[i].day21_Date)
                        $('#DutyRosterList_0__Day22_Date').val(d.data[i].day22_Date)
                        $('#DutyRosterList_0__Day23_Date').val(d.data[i].day23_Date)
                        $('#DutyRosterList_0__Day24_Date').val(d.data[i].day24_Date)
                        $('#DutyRosterList_0__Day25_Date').val(d.data[i].day25_Date)
                        $('#DutyRosterList_0__Day26_Date').val(d.data[i].day26_Date)
                        $('#DutyRosterList_0__Day27_Date').val(d.data[i].day27_Date)
                        $('#DutyRosterList_0__Day28_Date').val(d.data[i].day28_Date)
                        $('#DutyRosterList_0__Day29_Date').val(d.data[i].day29_Date)
                        $('#DutyRosterList_0__Day30_Date').val(d.data[i].day30_Date)
                        $('#DutyRosterList_0__Day31_Date').val(d.data[i].day31_Date)

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
                { "data": "workHours", "width": "3%" },
                { "data": "avgHours", "width": "3%" },
                { "data": "nightShift", "width": "3%" },
                { "data": "morningShift", "width": "3%" },
                { "data": "totalShift", "width": "3%" },
                { "data": "totalOffday", "width": "3%" },
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


            debugger
            var val = $('#DailyAttendanceList_Count').val();

            if (val != 0) {
                debugger
                $('.dataTables_sizing ').css("display", 'block');

            }
            else {
                debugger
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
    }
}
function onProceedClick()
{
    debugger
    if ($('#Month_Code').val() != '' && $('#PayrollGroup_Code').val() != '' && $('#Circle_Code').val() != '')
    {
        showSpinner();

        var Month = $("#Month_Code").val();
        var CIRCLE = $("#Circle_Code").val();
        var PayrollGroup = $("#PayrollGroup_Code").val();

        $("#F_Month_Code").val(global_Month);
        $("#F_PayrollGroup_Code").val(global_PayrollGroup);
        $("#F_Circle_Code").val(global_CIRCLE);

  
        $('.reset').val('');
        $('#Optional_Filter_EmploymentStatus_Code').val(0);
        $('#filterForm').removeClass('filter-apply');
        
        global_Month = Month;
        global_CIRCLE = CIRCLE;
        global_PayrollGroup = PayrollGroup;
      
        $.ajax({

            type: 'GET',
            url: '/TimeOffice/DutyRoster/DutyRosterEntryList?Month=' + Month + '&PayrollGroup=' + PayrollGroup + '&CIRCLE=' + CIRCLE,
            dataType: 'html',
            data: $('#Time_PayRoll_BasicFilter').serialize(),
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    debugger
                    $('#dutyRosterList').html("");
                    $('#dutyRosterList').html(result);
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
function validateEntryDropdowns() {
    var i = 0;
    if (!validateById('#Month_Code', 'dutymonth'))
        i++;
  
    if (!validateById('#Circle_Code', 'dutycircle'))
        i++;
    if (!validateById('#PayrollGroup_Code', 'dutypayRollGrp'))
        i++;
   
    if (i == 0)
        return true;
    else {
        return false;
    }
}

function validateDropdowns() {
    var i = 0;
    if (!validateById('#Month_Code', 'Mdutymonth') && !validateById('.RMonth', 'dutymonth'))
        i++;
    if (!validateById('#From', 'MdutyfromDte') && !validateById('.RFrom', 'dutyfromDte'))
        i++;
    if (!validateById('#To', 'Mdutyto') && !validateById('.RTo', 'dutyto'))
        i++;
    if (!validateById('#Circle_Code', 'Mdutycircle') && !validateById('.RCircle', 'dutycircle'))
        i++;
    //if (!validateById('#PayrollGroup_Code', 'MdutypayRollGrp') && !validateById('.RPayroll', 'dutypayRollGrp'))
    //    i++;

    if (i == 0)
        return true;
    else {
        return false;
    }
}

function onDutyCancelClick(index)
{
    $('.shift_Label_' + index).removeClass('hide');
    $('.edit_Button_' + index).removeClass('hide');
    $('.shift_ID_' + index).addClass('hide');
    $('.save_Cancel_' + index).addClass('hide');
 
}

function onDutyDateWiseCancelClick(index) {
    debugger
    $('.shift_Label_DateWise_' + index).removeClass('hide');
    $('.edit_Button_DateWise_' + index).removeClass('hide');
    $('.shift_ID_DateWise_' + index).addClass('hide');
    $('.save_Cancel_DateWise_' + index).addClass('hide');

}
function onDutyEditClick(index) {
    debugger;

    $('.shift_Label_' + index).addClass('hide');
    $('.edit_Button_' + index).addClass('hide');
    $('.shift_ID_' + index).removeClass('hide');
    $('.save_Cancel_' + index).removeClass('hide');
    $('#Employee_DutyRosterEditDetails_' + index + '__ShiftDetails_Code').focus();

}
function onDutyDateWiseEditClick(index, EmployeeGi) {
    debugger;
    $.getJSON("/TimeOffice/DutyRoster/GetEmpWiseShiftDrpDown", { EmployeeGI: EmployeeGi }, function (data) {
        console.log(data);
        $('#DateWise_DutyRosterEditDetails_' + index + '__ShiftDetails_Code option').remove();
        $('#DateWise_DutyRosterEditDetails_' + index + '__ShiftDetails_Code').append('<option value>Shift Code</option');
        
        for (var i = 0; i < data.length; i++) {
            $('#DateWise_DutyRosterEditDetails_' + index + '__ShiftDetails_Code').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
        }

    });
    $('.shift_Label_DateWise_' + index).addClass('hide');
    $('.edit_Button_DateWise_' + index).addClass('hide');
    $('.shift_ID_DateWise_' + index).removeClass('hide');
    $('.save_Cancel_DateWise_' + index).removeClass('hide');
    $('#DateWise_DutyRosterEditDetails_' + index + '__ShiftDetails_Code').focus();

}
function onShiftChange(sender, index)
{
    debugger
    var Shift = $(sender).val();
    $.getJSON("/TimeOffice/DutyRoster/GetShiftDetails", { ShiftCode: Shift }, function (data) {
        console.log(data);
        $(".dutyShiftName_Row_" + index).html(data.name);
        $(".dutyShiftStart_Row_" + index).html(data.shiftStartTime);
        $(".dutyShiftEnd_Row_" + index).html(data.shiftEndTime);

    });
}

function onDateWiseShiftChange(sender, index) {
    debugger
    var Shift = $(sender).val();
    $.getJSON("/TimeOffice/DutyRoster/GetShiftDetails", { ShiftCode: Shift }, function (data) {
        console.log(data);
        $(".dutyShiftName_DateWise_Row_" + index).html(data.name);
        $(".dutyShiftStart_DateWise_Row_" + index).html(data.shiftStartTime);
        $(".dutyShiftEnd_DateWise_Row_" + index).html(data.shiftEndTime);

    });
}

function OnDutySaveClick(EmployeeGI, index) {
    debugger;
    if (!validateById('#Employee_DutyRosterEditDetails_' + index + '__ShiftDetails_Code', 'dutyDREmpShift_'+index))
        return false;
    else {
        var Shift = $("#Employee_DutyRosterEditDetails_" + index + "__ShiftDetails_Code").val();
        var Date = $("#Employee_DutyRosterEditDetails_" + index + "__Date").val();
         $.ajax({
            type: 'POST',
            url: '/TimeOffice/DutyRoster/DutyRosterSave',
            dataType: 'json',
            data: { EmployeeGI: EmployeeGI, Date: Date, ShiftCode: Shift },
            success: function (result) {
                debugger;
                if (result.success == true) {
                    swal.fire("Updated Successfully").then(function () {
                        var Month = $('#Month_Code').val();
                        var PayrollGroup = $('#PayrollGroup_Code').val();
                        var CIRCLE = $("#Circle_Code").val();
                        var url = "/TimeOffice/DutyRoster/EmployeeDutyRosterEditDetails?Month=" + Month + "&PayrollGroup=" + PayrollGroup + "&CIRCLE=" + CIRCLE + "&EmployeeGI=" + EmployeeGI;
                        window.location.href = url;


                    });

                }
                else {
                    swal.fire(result.message).then(function () {
                        var Month = $('#Month_Code').val();
                        var PayrollGroup = $('#PayrollGroup_Code').val();
                        var CIRCLE = $("#Circle_Code").val();
                        var url = "/TimeOffice/DutyRoster/EmployeeDutyRosterEditDetails?Month=" + Month + "&PayrollGroup=" + PayrollGroup + "&CIRCLE=" + CIRCLE + "&EmployeeGI=" + EmployeeGI;
                        window.location.href = url;

                    });
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
}
function OnDutyDateWiseSaveClick(EmployeeGI, index, date) {
    debugger;
    currentPage = dataTable.page();
    if (!validateById('#DateWise_DutyRosterEditDetails_' + index + '__ShiftDetails_Code', 'dutyRrDateShift'))
        return false;
    else {
        var Shift = $("#DateWise_DutyRosterEditDetails_" + index + "__ShiftDetails_Code").val();

        $.ajax({
            type: 'POST',
            url: '/TimeOffice/DutyRoster/DutyRosterSave',
            dataType: 'json',
            data: { EmployeeGI: EmployeeGI, Date: date, ShiftCode: Shift },
            success: function (result) {
                debugger;

                if (result.success == true) {
                    swal.fire("Updated Successfully").then(function () {
                        var url = "/TimeOffice/DutyRoster/DutyRosterDateWiseEntryList?data=" + $('#Time_PayRoll_BasicFilter').serialize();
                        /* window.location.href = url + "?SelectedDate=" + date;*/
                        debugger
                        var Month = $("#Month_Code").val();
                        var CIRCLE = $("#Circle_Code").val();
                        var PayrollGroup = $("#PayrollGroup_Code").val();

                        $('#dateWiseDutyList').load(url, { SelectedDate: date, Month: Month, CIRCLE: CIRCLE, PayrollGroup: PayrollGroup }, function () { });
                        $('.shift_Label_DateWise_' + index).removeClass('hide');
                        $('.edit_Button_DateWise_' + index).removeClass('hide');
                        $('.shift_ID_DateWise_' + index).addClass('hide');
                        $('.save_Cancel_DateWise_' + index).addClass('hide');


                    });

                }
                else {
                    swal.fire(result.message).then(function () {
                        var url = "/TimeOffice/DutyRoster/DutyRosterDateWise";
                        window.location.href = url + "?SelectedDate=" + date;

                    });
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
}
function onDutyRostrAllClick(colchkid, tableid) {
    debugger
    if ($('#' + colchkid).is(':checked'))
        $('#' + tableid).find('input[type=checkbox]').prop('checked', true);
    else
        $('#' + tableid).find('input[type=checkbox]').prop('checked', false);
}


function onFilterClick(FormName)
{ 
    debugger
    if(validateEntryDropdowns())  
        {           
            $.ajax({

                type: 'GET',
                url: '/TimeOffice/DutyRoster/Filter?FormName=' + FormName,
                dataType: 'html',
                data: $('#Time_PayRoll_BasicFilter').serialize(),

                success: function (result) {
                    debugger
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }
                    else {
                        $('body').append('<div class="form-overlay"></div>');
                        if (FormName == "outerList") {
                            $('#dutyFilter').html("");
                            $('#dutyFilter').html(result);
                        }
                        else {
                            $('#dutyDateFilter').html("");
                            $('#dutyDateFilter').html(result);
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
        }
    }



    

function ApplyFilter(Mode, form)
{
    debugger
    if (form == 'outerList')
    {
        ApplyFilterEntry(Mode);
    }
    else
    {
        ApplyFilterDateWise(Mode);
    }
}
function ApplyFilterEntry(Mode) {
    debugger
   
        if (Mode == "apply") {
            if (filterApplicable >= 1) {
                showSpinner();
                fillData()
                CloseSlider('TP_filter');
                $('#filterForm').removeClass('filter-apply');
            }
            else {
                showSpinner();
                fillData()
                CloseSlider('TP_filter');
                $('#filterForm').addClass('filter-apply');
            }
        }
        else {


            showSpinner();
            filterApplicable = 1;
            $('.reset').val('');
            $('#Optional_Filter_EmploymentStatus_Code').val(0);
            $('#filterForm').removeClass('filter-apply');
            fillData();


        }

    }
   



function ApplyFilterDateWise(Mode) {
    debugger

    var Month = $("#Month_Code").val();
    var CIRCLE = $("#Circle_Code").val();
    var PayrollGroup = $("#PayrollGroup_Code").val();
    var SelectedDate = $("#SelectedDate").val();

    if (Mode == "apply") {
        if (filterApplicable >= 1) {
            showSpinner();
            $.ajax({

                type: 'POST',
                url: '/TimeOffice/DutyRoster/ApplyFilterDateWise?Month=' + Month + '&PayrollGroup=' + PayrollGroup + '&CIRCLE=' + CIRCLE + '&SelectedDate=' + SelectedDate,
                dataType: 'html',
                data: $('#Time_PayRoll_BasicFilter').serialize(),
                success: function (result) {
                    debugger
                    CloseSlider('TP_filter');

                    $('#dateWiseDutyList').html("");
                    $('#dateWiseDutyList').html(result);
                    hideSpinner();


                },


                error: function (xhr, textStatus, errorThrown) {
                    document.body.id = "kt_body";
                    $('body').html(xhr.responseText);
                    hideSpinner();
                }
            });
            $('#filterForm').removeClass('filter-apply');
        }
        else {
            showSpinner();
        $.ajax({

            type: 'POST',
            url: '/TimeOffice/DutyRoster/ApplyFilterDateWise?Month=' + Month + '&PayrollGroup=' + PayrollGroup + '&CIRCLE=' + CIRCLE + '&SelectedDate=' + SelectedDate,
            dataType: 'html',
            data: $('#Time_PayRoll_BasicFilter').serialize(),
            success: function (result) {
                debugger
                CloseSlider('TP_filter');

                $('#dateWiseDutyList').html("");
                $('#dateWiseDutyList').html(result);
                hideSpinner();


            },


            error: function (xhr, textStatus, errorThrown) {
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);
                hideSpinner();
            }
        });
        $('#filterForm').addClass('filter-apply');
    }
    }
    else {

        showSpinner();

        $('.reset').val('');
        $('#Optional_Filter_EmploymentStatus_Code').val(0);
        $('#filterForm').removeClass('filter-apply');
        $.ajax({

            type: 'POST',
            url: '/TimeOffice/DutyRoster/ApplyFilterDateWise?Month=' + Month + '&PayrollGroup=' + PayrollGroup + '&CIRCLE=' + CIRCLE + '&SelectedDate=' + SelectedDate,
            dataType: 'html',
            data: $('#Time_PayRoll_BasicFilter').serialize(),
            success: function (result) {
                debugger

                $('#dateWiseDutyList').html("");
                $('#dateWiseDutyList').html(result);


                hideSpinner();


            },


            error: function (xhr, textStatus, errorThrown) {
                hideSpinner();
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);
            }
        });


    }

}

//BatchUpdate
function onEntryBatchUpdateClick() {
    debugger;
    if (validateDropdowns()) {
        debugger

        var Month = $("#Month_Code").val();
        var CIRCLE = $("#Circle_Code").val();
        var PayrollGroup = $("#PayrollGroup_Code").val();

        $.ajax({

            type: 'GET',
            url: '/TimeOffice/DutyRoster/EntryBatchUpdate?Month=' + Month + '&PayrollGroup=' + PayrollGroup + '&CIRCLE=' + CIRCLE,
            dataType: 'html',
            data: $('#Time_PayRoll_BasicFilter').serialize(),

            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {

                    $('#dutyEntryBatch').html("");
                    $('#dutyEntryBatch').html(result);
                    $('body').append('<div class="form-overlay"></div>');

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
}

function onDutyBatchShiftCHnage(sender, formName, Month, PayrollGroup, CIRCLE, SelectedDate)
{
    debugger
    shiftCode = $(sender).val();
    showSpinner();
    $.ajax({

        type: 'GET',
        url: '/TimeOffice/DutyRoster/GetEntryBatchUpdateEmployeeList?ShiftCode=' + shiftCode + '&FormName=' + formName + '&Month=' + Month + '&PayrollGroup=' + PayrollGroup + '&CIRCLE=' + CIRCLE + '&SelectedDate=' + SelectedDate,
        dataType: 'html',
        data: $('#Time_PayRoll_BasicFilter').serialize(),

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                if (formName == 'entry') {
                    $('#dutyEntryBatchList').html("");
                    $('#dutyEntryBatchList').html(result);
                }
                else {
                    $('#dutyDateWiseBatchList').html("");
                    $('#dutyDateWiseBatchList').html(result);
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




}

function ondutyDateWiseBatchUpdateClick(selecteddate, Month, PayrollGroup, CIRCLE)
{
    debugger

    $.ajax({

        type: 'POST',
        url: '/TimeOffice/DutyRoster/DateWiseBatchUpdate?Month=' + Month + '&PayrollGroup=' + PayrollGroup + '&CIRCLE=' + CIRCLE + "&SelectedDate=" + selecteddate,
        dataType: 'html',
        data: $('#Time_PayRoll_BasicFilter').serialize(),
        success: function (result) {
            debugger            

            $('#dateWiseDutyBatch').html("");
            $('#dateWiseDutyBatch').html(result);
            $('body').append('<div class="form-overlay"></div>');
            

        },


        error: function (xhr, textStatus, errorThrown) {
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
            hideSpinner();
        }
    });


}

function onDateWiseBatchUpdateSaveClick(selecteddate) {
    debugger
   

    if (!validateById('#ShiftDetails_Code', 'dutyDatewiseShift'))
        return false;
    else
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
                    url: '/TimeOffice/DutyRoster/EntryBatchUpdateSave',
                    dataType: 'json',
                    data: $('#dutyDateWise_Batch_Form').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire("Updated Successfully").then(function () {
                                CloseSlider('dutyDateWise_Batch');
                                var Month = $("#Month_Code").val();
                                var CIRCLE = $("#Circle_Code").val();
                                var PayrollGroup = $("#PayrollGroup_Code").val();
                                var url = "/TimeOffice/DutyRoster/DutyRosterDateWise?data=" + $('#Time_PayRoll_BasicFilter').serialize() + "&Month=" + Month + "&CIRCLE=" + CIRCLE + "&PayrollGroup=" + PayrollGroup + "&SelectedDate=" + selecteddate;
                                debugger

                                window.location.href = url;

                                //var url = "/TimeOffice/DutyRoster/DutyRosterEntryList";
                                //$('#dutyRosterList').load(url, { Month: global_Month, PayrollGroup: global_PayrollGroup, FROM: global_FROM, TO: global_TO, CIRCLE: global_CIRCLE }, function () { });


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

            else
                return false;

        })
        
}
function onEmpWiseBatchUpdateClick(employeeGI)
{
    debugger

    var Month = $("#Month_Code").val();

    $.ajax({

        type: 'GET',
        url: '/TimeOffice/DutyRoster/EmpWiseBatchUpdate?Month=' + Month + '&EmployeeGI=' + employeeGI,
        dataType: 'html',
        data: $('#Time_PayRoll_BasicFilter').serialize(),
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {

                $('#employeeWiseDutyBatch').html("");
                $('#employeeWiseDutyBatch').html(result);
                $('body').append('<div class="form-overlay"></div>');

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

function onEmpWiseBatchSAveClick(employeeGI) {
    var Month = $('#Month_Code').val();
    var PayrollGroup = $('#PayrollGroup_Code').val();
    var CIRCLE = $("#Circle_Code").val();

    if (validateBatchUpdate()) { 
    debugger
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
                url: '/TimeOffice/DutyRoster/DutyRoster_Employeewise_BatchUpDate',
                dataType: 'json',
                data: $('#dutyEmp_Batch_Form').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire("Updated Successfully").then(function () {
                            CloseSlider('dutyEmpBatch');
                            //var url = "/TimeOffice/DutyRoster/EmployeeDutyRosterEditDetails";
                            var url = "/TimeOffice/DutyRoster/EmployeeDutyRosterEditDetails?Month=" + Month + "&PayrollGroup=" + PayrollGroup + "&CIRCLE=" + CIRCLE + "&EmployeeGI=" + employeeGI;
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

        else
            return false;

    })
}
}
function onEntryBatchUpdateSaveClick()
{
    //if (!validateById('#ShiftDetails_Code', 'dutyBatchShift'))
    //    return false;
    //else
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
                    url: '/TimeOffice/DutyRoster/EntryBatchUpdateSave',
                    dataType: 'json',
                    data: $('#dutyEntry_Batch_Form').serialize(),
                    success: function (result) {
                        if (result.success == true) {                            
                            swal.fire("Updated Successfully").then(function () {
                                CloseSlider('dutyBatchupdate');
                                var url = "/TimeOffice/DutyRoster/DutyRosterEntryList";
                                debugger
                            });

                            fillData();
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

function GetList() {

    var Month = $("#Month_Code").val();
    var CIRCLE = $("#Circle_Code").val();
    var PayrollGroup = $("#PayrollGroup_Code").val();

    $.ajax({

        type: 'POST',
        url: '/TimeOffice/DutyRoster/ApplyFilterEntry?Month=' + Month + '&PayrollGroup=' + PayrollGroup + '&CIRCLE=' + CIRCLE,
        dataType: 'html',
        data: $('#Time_PayRoll_BasicFilter').serialize(),
        success: function (result) {
            debugger
            CloseSlider('TP_filter');

            $('#dutyRosterList').html("");
            $('#dutyRosterList').html(result);

            hideSpinner();

        },

        error: function (xhr, textStatus, errorThrown) {
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
            hideSpinner();
        }
    });
}

function DutyRosterDateWise(i, Month, PayrollGroup, CIRCLE) {
    SelectedDate = $('#DutyRosterList_0__Day' + i + '_Date').val()
    debugger;
    $.ajax({

        type: 'GET',
        url: '/TimeOffice/DutyRoster/DutyrosterLockCheck',
        dataType: 'json',
        data: { CircleCode: CIRCLE, SelectedDate: SelectedDate },
        success: function (result) {
            if (result.success != true) {

                var url = '/TimeOffice/DutyRoster/DutyRosterDateWise?data=' + $('#Time_PayRoll_BasicFilter').serialize() + '&SelectedDate=' + SelectedDate + '&Month=' + Month + '&PayrollGroup=' + PayrollGroup + '&CIRCLE=' + CIRCLE;
                window.location.href = url;
            }
            else {
                swal.fire(result.message);
            }

        },
        error: function (xhr, textStatus, errorThrown) {
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
        }
    });
    
}


function onEmployeeWiseExportClick(emp)
{
    debugger
    var Month = $('#Month_Code').val();
    var PayrollGroup = $('#PayrollGroup_Code').val();
    var CIRCLE = $("#Circle_Code").val();
    var TableLength = $('#Employee_DutyRosterEditDetails_Count').val();
    if (TableLength > 0) {
        url = '/TimeOffice/DutyRoster/ExportToexcel_DutyRosterEmployeeWiseList?&Month=' + Month + '&PayrollGroup=' + PayrollGroup + '&CIRCLE=' + CIRCLE+'&EmployeeGI=' + emp;

        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}

function onDateWiseExportClick()
{
    debugger
    var MName = $('#Month_Code option:selected').text();
    var Month = $('#Month_Code').val();
    var PayrollGroup = $('#PayrollGroup_Code').val();
    var CIRCLE = $("#Circle_Code").val();


    var TableLength = $('#DateWise_DutyRosterEditDetails_Count').val();
    if (TableLength > 0)
    {
        url = '/TimeOffice/DutyRoster/ExportToexcel_DutyRosterDateWiseList?data=' + $('#Time_PayRoll_BasicFilter').serialize()  + "&Month=" + Month + "&PayrollGroup=" + PayrollGroup + "&CIRCLE=" + CIRCLE + '&MName=' + MName + '&SelectedDate=' + $('#SelectedDate').val();
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}

function onDutyRosterListExportClick() {
    debugger
    var CIRCLEName = $("#Circle_Code option:selected").text();
    var PayrollGroupName = $("#PayrollGroup_Code option:selected").text();
    var MName = $('#Month_Code option:selected').text();

    var Month = $('#Month_Code').val();
    var PayrollGroup = $('#PayrollGroup_Code').val();
    var CIRCLE = $("#Circle_Code").val();
    var Stype = $('#ShiftType_Code option:selected').text();
    var Type = $('#ShiftType_Code').val();
    if (Type == 0) Stype = "ALL";
 
    var TableLength = $('#DutyRosterList_Count').val();
    if (TableLength > 0) {
        url = '/TimeOffice/DutyRoster/ExportToexcel_DutyRosterEntryList?data=' + $('#Time_PayRoll_BasicFilter').serialize() + '&totRecords=' + TableLength + "&CIRCLEName=" + CIRCLEName + "&PayrollGroupName=" + PayrollGroupName + "&MName" + MName + "&Month=" + Month + "&PayrollGroup=" + PayrollGroup + "&CIRCLE=" + CIRCLE + "&Type=" + Type + "&Stype=" + Stype;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}

function validateBatchUpdate() {
    var i = 0;
        if (!validateById('.dtFrom', 'fromdate')) {
        i++;
    }
    if (!validateById('.dtTo', 'todate')) {
        i++;
    }  
    if (!validateById('#ShiftDetails_Code', 'empBatchShift')) {
        i++;
    }
    if (i == 0)
        return true;
    else
        return false;
}
function validateShift() {
    debugger;
    var i = 0;
    debugger;
    if (!validateById('#PayrollGroup_Code', 'ShiftPayroll')) {
        i++;
    }
    if (!validateById('#Month_Code', 'ShiftMonth')) {
        i++;
    }
    if (!validateById('#Circle_Code', 'ShiftCircle')) {
        i++;
    }
    if (i == 0) {
        /*$('#Excel_FileData').trigger('click')*/
        var m = $('#Impexcel');
        m.modal('show');

    }

}


function FromToDateUpdation() {
    debugger;
    var i = 0;
    debugger;
    if (!validateById('#PayrollGroup_Code', 'ShiftPayroll')) {
        i++;
    }
    if (!validateById('#Month_Code', 'ShiftMonth')) {
        i++;
    }
    if (!validateById('#Circle_Code', 'ShiftCircle')) {
        i++;
    }
    if (i == 0) {
        /*$('#Excel_FileData').trigger('click')*/
        var m = $('#Imp_FromToDateUpdation_excel');
        m.modal('show');

    }

}
function onImportExcelClick() {

    swal.fire({
        text: "Do you want to Upload?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            var MPPersonalInfo = new FormData($('#DutyRosterUpload')[0]);
            $("#Excel_FileData").val('');
            $.ajax({

                type: 'POST',
                url: '/TimeOffice/DutyRoster/ImportFromExcel',
                contentType: false,
                processData: false,
                data: MPPersonalInfo,
                success: function (result) {
                    if (result.success == true) {
                        $("#Excel_FileData").val('');
                        swal.fire("Uploaded Successfully").then(function () {
                            //url = '/DataBank/HRPlanningBudgetUpload/GetUploadList';
                            //$('#uploadList').load(url, { HrPlanningCode: $("#HRPlanning_Code").val(), DesignationCode: $("#Designation_Code").val() }, function () {
                            //});
                            fillData();
                            var m = $('.bd-example-modal-lg');
                            m.modal('hide');
                        });
                    }
                    else {
                        $("#Excel_FileData").val('');
                        if (result.message != null) {
                            $('#Unmatchedlist').html('')
                            swal.fire(result.message);
                        }
                        else {
                            $('#Unmatchedlist').html(result)
                        }
                    }

                },
                error: function (xhr, textStatus, errorThrown) {
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
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

function ImportFromExcel_FromToDateUpdation() {
    if (validateUpload()) {
        swal.fire({
            text: "Do you want to Upload?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                var MPPersonalInfo = new FormData($('#DutyRosterUpload')[0]);
                $("#Excel_FileData").val('');
                $.ajax({

                    type: 'POST',
                    url: '/TimeOffice/DutyRoster/ImportFromExcel_FromToDateUpdation',
                    contentType: false,
                    processData: false,
                    data: MPPersonalInfo,
                    success: function (result) {
                        if (result.success == true) {
                            $("#Excel_FileData").val('');
                            swal.fire("Uploaded Successfully").then(function () {
                                //url = '/DataBank/HRPlanningBudgetUpload/GetUploadList';
                                //$('#uploadList').load(url, { HrPlanningCode: $("#HRPlanning_Code").val(), DesignationCode: $("#Designation_Code").val() }, function () {
                                //});
                                fillData();
                                var m = $('.bd-example-modal-lg');
                                m.modal('hide');
                            });
                        }
                        else {
                            $("#Excel_FileData").val('');
                            if (result.message != null) {
                                $('#Unmatchedlist').html('')
                                swal.fire(result.message);
                            }
                            else {
                                $('#Unmatchedlist').html(result)
                            }
                        }

                    },
                    error: function (xhr, textStatus, errorThrown) {
                        if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
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

function validateUpload() {
    debugger;
    var i = 0;
    if (!validateById('#ImportFrom', 'importFrom')) {
        i++;
    }
    if (!validateById('#ImportTo', 'importTo')) {
        i++;
    }
    if (!validateById('.importExcelFile', 'importFile')) {
        i++;
    }
    if (i == 0)
        return true;
    else
        return false;
}