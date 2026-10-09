const month = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
var global_count = 0;
var global_fh = 0;

var global_sh = 0;
function breadscrumb() {
    debugger
    window.location.href = "/TimeOffice/AdvancedEmpWiseAttendEntry/Index?Month=" + $("#Month_Code").val() + "&PgGroup=" + $("#PayrollGroup_Code").val()
}

function validatefh(sender, index) {
    debugger
    flag = 0;
    $.getJSON("/TimeOffice/DailyAttendanceEntry/CheckIffCOFF", { AttendanceStatusCode: $(sender).val() }, function (data) {


        if (data.attendanceStatusType.code == 3 || data.attendanceStatusType.code == 4) {
            swal.fire('Not Allowed To Save PR and AB Status Manually');
            $('#Employee_DailyAttendanceEditDetails_' + index + '__FirstHalf_Code').val(global_fh);
            return false;

        } 
        if ($('#Employee_DailyAttendanceEditDetails_' + index + '__FHType_Name').val() != "PR")   {

            $('#Employee_DailyAttendanceEditDetails_' + index + '__SecondHalf_Code').val($('#Employee_DailyAttendanceEditDetails_' + index + '__FirstHalf_Code').val());
            $('#Employee_DailyAttendanceEditDetails_' + index + '__FHType_Name').val(data.attendanceStatusType.shortName);
            $('#Employee_DailyAttendanceEditDetails_' + index + '__SHType_Name').val(data.attendanceStatusType.shortName);

        }

        EnableDisableCoff(data.attendanceStatusType.code, index);


    });
}
function validatesh(sender, index) {
    debugger
    flag = 0;
    $.getJSON("/TimeOffice/DailyAttendanceEntry/CheckIffCOFF", { AttendanceStatusCode: $(sender).val() }, function (data) {


        if (data.attendanceStatusType.code == 3 || data.attendanceStatusType.code == 4) {
            swal.fire('Not Allowed To Save PR and AB Status Manually');
            $('#Employee_DailyAttendanceEditDetails_'+index+'__SecondHalf_Code').val(global_sh);
            return false;

        }

    });
}


function EnableDisableCoff(attendanceStatusType, index) {
    
    if ($('#Employee_DailyAttendanceEditDetails_' + index + '__FHType_Name').val() == "COFF" && $('#Employee_DailyAttendanceEditDetails_' + index + '__SHType_Name').val() == "COFF") {
        var length = $('.coffdropdown_'+index).children('option').length;
        if (length == 1) {
            $('#Employee_DailyAttendanceEditDetails_' + index + '__FirstHalf_Code').val(global_fh);
            $('#Employee_DailyAttendanceEditDetails_' + index + '__SecondHalf_Code').val(global_sh);
            $('#Employee_DailyAttendanceEditDetails_' + index + '__EntitleDate').val('');
            $('#COFFTxtbox_' + index).addClass('hide');
            $('#hideCOFF_' + index).removeClass('hide');
            swal.fire("No COFF Balance");
            return false;
        } else {
            $('#COFFTxtbox_' + index).removeClass('hide');
            $('#hideCOFF_' + index).addClass('hide');
        }
    }  

}
function ValidateStatus(Statusvalue) {
    debugger
    var AttnStatus = $(Statusvalue).val();
    $.getJSON("/TimeOffice/DailyAttendanceEntry/ValidateAttendanceStatus", { AttnStatus: AttnStatus }, function (data) {
        console.log(data);
        debugger
        if (data.success == false) {
            swal.fire(data.message).then(function () {
                $(Statusvalue).val("");
            });

        }
        else {
            return false;
            //swal.fire("Changes in DOJ will refelect on Renewal Date").then(function () {
            //    return 1;
            /* });*/

        }

    });

}

function fillMonthInnerList(sender) {
    debugger;
    $.ajaxSetup({
        async: false
    });
    $.getJSON("/TimeOffice/DailyAttendanceEntry/GetMonth", { PayrollGrp: $(sender).val() }, function (data) {

        $('#Month_Code option').remove();
        $('#Month_Code').append('<option value>--Select Month-- </option');
        debugger;
        for (var i = 0; i < data.length; i++) {
            $('#Month_Code').append('<option value="' + data[i].code + '">' + data[i].name + '</option');
        }

    });
    $.getJSON("/TimeOffice/AdvancedEmpWiseAttendEntry/GetCurrentMonth", function (data) {     
        $("#Month_Code").val(data.data);
    });
    
    fillEmplist();
    editAttendance($("#employeeGI_0").val(),$("#Month_Code").val());
}
function fillMonth(sender) {
    debugger;
    $.ajaxSetup({
        async: false
    });
    $.getJSON("/TimeOffice/DailyAttendanceEntry/GetMonth", { PayrollGrp: $(sender).val() }, function (data) {

        $('#Month_Code option').remove();
        $('#Month_Code').append('<option value>--Select Month-- </option');
        debugger;
        for (var i = 0; i < data.length; i++) {
            $('#Month_Code').append('<option value="' + data[i].code + '">' + data[i].name + '</option');
        }

    });
    fillData();
}
function editAttendanceEmpClick(empGi, month) {//ON EMPLOYEE CLICK
    window.location.href = "/TimeOffice/AdvancedEmpWiseAttendEntry/EmployeeWiseAttendanceEdit?EmployeeGI=" + empGi + "&Month=" + month +
        "&Pgroup=" + $("#PayrollGroup_Code").val();
}
function editAttendance(empGi, month) {
    debugger
    $.ajax(
        {
            url: "/TimeOffice/AdvancedEmpWiseAttendEntry/EmployeeWiseAttendanceEdit?EmployeeGI=" + empGi + "&Month=" + month,
            dataType: "html",
            success: function (data) {
                var tempDiv = $('<div>').html(data);
                $('#eaEntry_Edit_List_Partial').html(tempDiv.find('#eaEntry_Edit_List_Partial').html());
                $('.advanced-emp-name').html(tempDiv.find('.advanced-emp-name').html());
                $('.ad-summary').html(tempDiv.find('.ad-summary').html());
        
            },
            error: function (xhr, textStatus, errorThrown) {
                debugger
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);


            }
        });
}
function fillEmplist() {

    debugger;
    var filter1 = $('#BasicFilter').serialize();

    var temp = filter1;

    var Month = $('.RMonth').val();
    var PgGroup = $('#PayrollGroup_Code').val();

    var dataTable = $("#eaEntry_Emp_table").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/TimeOffice/AdvancedEmpWiseAttendEntry/GetEmployeeWiseList",
            "type": "POST",
            "datatype": "json",
            'data':

                function (d) {
                    temp = '';
                    var start = d.start;
                    var searchValue = $('#dataTableSearch').val();
                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length +
                        '&searchValue=' + searchValue + "&Month=" + Month + "&PgGroup=" + PgGroup + '&dummy=dummy';

                    d.filter = temp;
                    return JSON.stringify(d);
                },




            "dataSrc": function (d) {

                $('#EmployeeWiseAttendanceList_Count').val(d.recordsTotal);

                for (var i = 0, ien = d.data.length; i < ien; i++) {
                    d.data[i].employeeID = '<input type="hidden" id="employeeGI_' + i + '" value="'+ d.data[i].employeeGI+'">' + d.data[i].employeeID;
                    d.data[i].fullName = `<a onclick="editAttendanceEmpClick('${d.data[i].employeeGI}', ${Month})">${d.data[i].fullName}</a>`;

                }

                return d.data;
            }

        },

        "columns": [

            { "data": "employeeID", "width": "8%" },
            { "data": "fullName", "width": "12%" }           

        ],


        "dom": 'rt<"bottom"><"my-wrapper"<"my-inner2"l><"my-inner1"i><"my-inner3"p>>',

        "serverSide": "true",
        "scrollY": '52vh',
        "sScrollX": "100%",
        "sScrollXInner": "100%",
        "pagingType": "simple", // Shows only 'next' and 'previous' buttons
        "order": [0, "asc"],
        "processing": "true",
        "language": {

            info: " _START_ to _END_ of _TOTAL_ records", // Customize this message
            processing: '<img width="100%" src="/images/Loader_img1 (7).gif">',
            emptyTable: '<div id="noDataFound" style="text-align: center;display: flex;align-items: center;justify-content: center;height: 45vh;"><img src="/images/nodata.png" /></div >',

            zeroRecords: '<div id="noDataFound" style="text-align: center;display: flex;align-items: center;justify-content: center;height: 45vh;"><img src="/images/nodata.png" /></div >',
      
            sLengthMenu: "_MENU_",
            paginate: {


                next: '<img width="100%" src="/images/icons/Icon awesome-arrow-right-1.png" >',
                previous: '<img src="/images/icons/Icon awesome-arrow-right.png">',
                


            }
        }



    });



    $('.ihits-table table').removeClass("dataTable no-footer");


    $('.paginate_button.next').attr("data-bs-toggle", "tooltip");
    $('.paginate_button.next').attr("title", "Next");
    $('.paginate_button.previous').attr("data-bs-toggle", "tooltip");
    $('.paginate_button.previous').attr("title", "Previous");
    //$('.paginate_button.first').attr("data-bs-toggle", "tooltip");
    //$('.paginate_button.first').attr("title", "First");
    //$('.paginate_button.last').attr("data-bs-toggle", "tooltip");
    //$('.paginate_button.last').attr("title", "Last");


    $("#dataTableSearch").on("keyup search input paste cut", function () {
        dataTable.search(this.value).draw();
        $('#noDataFound').css("display", 'none');
    });

    $('.search-slider-inner').click(function () {
        dataTable.search('').draw();
        $('#dataTableSearch').val('');
        $('#dataTableSearch').focus();
    });

    dataTable.on('draw', function () {
        $('.paginate_button.next').attr("data-bs-toggle", "tooltip");
        $('.paginate_button.next').attr("title", "Next");
        $('.paginate_button.previous').attr("data-bs-toggle", "tooltip");
        $('.paginate_button.previous').attr("title", "Previous");
        //$('.paginate_button.first').attr("data-bs-toggle", "tooltip");
        //$('.paginate_button.first').attr("title", "First");
        //$('.paginate_button.last').attr("data-bs-toggle", "tooltip");
        //$('.paginate_button.last').attr("title", "Last");

    });

   // $.fn.DataTable.ext.pager.numbers_length = 3;

}

function fillData() {

    debugger;
    var filter1 = $('#BasicFilter').serialize();
 
    var temp = filter1;
  
   var Month= $('.RMonth').val();
    var PgGroup = $('#PayrollGroup_Code').val();
  
    var dataTable = $("#eaEntry_Summary_table").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/TimeOffice/AdvancedEmpWiseAttendEntry/GetEmployeeWiseList",
            "type": "POST",
            "datatype": "json",
            'data':
                function (d) {
                    temp = '';
                    var start = d.start;
                    var searchValue = $('#dataTableSearch').val();
                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length +
                        '&searchValue=' + searchValue + "&Month=" + Month + "&PgGroup=" + PgGroup + '&dummy=dummy';

                    d.filter = temp;
                    return JSON.stringify(d);
                },




            "dataSrc": function (d) {

                $('#EmployeeWiseAttendanceList_Count').val(d.recordsTotal);

                for (var i = 0, ien = d.data.length; i < ien; i++) {                 

                    d.data[i].fullName = '<a href = "/TimeOffice/AdvancedEmpWiseAttendEntry/EmployeeWiseAttendanceEdit?EmployeeGI=' + d.data[i].employeeGI + '&Month=' + Month + '&Pgroup=' + PgGroup + '"> ' + d.data[i].fullName + '</a >'
      }

                return d.data;
            }

        },

        "columns": [

            { "data": "employeeID","width":"8%" },
            { "data": "fullName", "width": "12%" },
            { "data": "absent", "width": "5%" },
            { "data": "leave", "width": "5%"  },
            { "data": "lop", "width": "5%"  },
            { "data": "offDay", "width": "4%" },
            { "data": "holiDay", "width": "4%"  },
            { "data": "offdayPresent", "width": "4%"  },
            { "data": "holidayPresent", "width": "4%"  },
            { "data": "coff", "width": "4%"  },
            { "data": "presentDays", "width": "4%"  },
            { "data": "onDuty", "width": "4%"  },
            { "data": "ra", "width": "4%"  },
            { "data": "wfh", "width": "5%"  },
            { "data": "ljp", "width": "4%"  },
            { "data": "ltp", "width": "4%" },
            { "data": "esi", "width": "4%" },
            { "data": "paidDays", "width": "6%" },

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
        dataTable.search('').draw();
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



function onAbsentDaysCheckboxChange(sender,EmpGuid) {
    debugger
    var month = $("#Month_Code").val();
    var checkBox = document.getElementById("ShowAbsentDays");    
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

    $.ajax({

        type: 'GET',
        url: '/TimeOffice/AdvancedEmpWiseAttendEntry/EmployeeWiseAttendanceEditList',
        dataType: 'html',
        data: { EmployeeGI: EmpGuid, Month: month, ABSTATUS: ABSTATUS },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#eaEntry_Edit_List_Partial').html("");
                $('#eaEntry_Edit_List_Partial').html(result);
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


function ApplyFilter(mode) {
    debugger
    var tabIndex = $('#Tabindex').val();
    var search = $('#SearchId').val();
    if (!validateById("#Month_Code", 'EmployeeWiseAttendanceMonth'))
        return false;
    //$("#Tabindex").val(currentTab);
    else {
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
            fillData();
          
            $('#filterForm').removeClass('filter-apply');
        }
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
        endDate: _GetFormattedDate($("#Employee_DailyAttendanceEditDetails_" + (global_count - 1) + "__Date").val()),

        autoclose: true,



        format: "dd-M-yyyy",
        orientation: "auto",
        startDate: _GetFormattedDate($("#Employee_DailyAttendanceEditDetails_0__Date").val()),
        clearBtn: true
    });

}
function onEmpWiseBatchUpdateClick(EmployeeGI, selectedDate,PGName) {
    debugger;
    var HeaderDate = new Date(selectedDate);
    let name = month[HeaderDate.getMonth()];
    $.ajax(
        {
            url: "/TimeOffice/AdvancedEmpWiseAttendEntry/BatchUpdate?EmployeeGI=" + EmployeeGI + "&MonthName=" + name,
            dataType: "html",
            success: function (data) {
                $('body').append('<div class="form-overlay"></div>');
                $('#eaEntry_BatchUpdateSlider').html("");
                $('#eaEntry_BatchUpdateSlider').html(data);
                $('#payGrp').html(PGName);
                setDatePicker();
            },
            error: function (xhr, textStatus, errorThrown) {
                debugger
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);


            }
        });


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
function EmployeedailyAttendanceBatchUpdateSave() {
    debugger;

    if (ValidateBatchUpdate()) {
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
                    url: '/TimeOffice/AdvancedEmpWiseAttendEntry/BatchUpdateSave',
                    dataType: 'json',
                    data: $('#eaEntry_Batch_Form').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire("Updated Successfully").then(function () {
                                CloseSlider('eaEntry_Batch_Slider');
                                debugger
                                editAttendance(EmpGuid, month);


                            });

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

            else
                return false;

        })
     }
    else
        return false;
}

function validateEdit(EmployeeGI, Date, empcode, index) {
    PGCode = $("#PayrollGroup_Code").val();
    $.ajax({

        type: 'GET',
        url: '/TimeOffice/DailyAttendanceEntry/GetDailyAttendanceEntryDateValidation',
        dataType: 'json',
        data: { SelectedDate: Date, PayrollGroup: PGCode, EmployeeCode: empcode },

        success: function (result) {
            debugger
            if (result.success == true) {
                onEditClick(index, EmployeeGI, Date);
            }
            else {
                swal.fire(result.message);
            }

        },
        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);


        }
    });
}
function onUpdateClick(i) {
    if(validateEmpWiseEdit(i))
    onEmployeeDailyAttendSaveClick(i);

}
function onCancelClick(i) {
    debugger

    $('#edit_' + i).removeClass('hide');
    $('#updatecancel_' + i).addClass('hide');


    $('#FHTxtbox_' + i).addClass('hide');
    $('#hideFH_' + i).removeClass('hide');
    $('#SHTxtbox_' + i).addClass('hide');
    $('#hideSH_' + i).removeClass('hide');
    $('#COFFTxtbox_' + i).addClass('hide');
    $('#hideCOFF_' + i).removeClass('hide');

}
function onEditClick(i, empgi, date) {
    debugger
    global_fh =$('#Employee_DailyAttendanceEditDetails_'+i+'__FirstHalf_Code').val();
    global_sh = $('#Employee_DailyAttendanceEditDetails_' + i + '__SecondHalf_Code').val();

        $.getJSON("/TimeOffice/AdvancedEmpWiseAttendEntry/GetCOffDates", { EmployeeGI: empgi, Date: date }, function (data) {

            $('.coffdropdown_' + i+ ' option').remove();
            $('.coffdropdown_' + i).append('<option value>--Select-- </option');
            debugger;
            for (var j = 0; j < data.length; j++) {
                $('.coffdropdown_' + i).append('<option value="' + data[j].name + '">' + data[j].name + '</option');
            }
        });
    if ($('#Employee_DailyAttendanceEditDetails_' + i+ '__FHType_Name').val() == "COFF" && $('#Employee_DailyAttendanceEditDetails_' + i + '__SHType_Name').val() == "COFF") {
  
            $('#COFFTxtbox_' + i).removeClass('hide');
            $('#hideCOFF_' + i).addClass('hide');
        }


    
    $('#FHTxtbox_' + i).removeClass('hide');
    $('#hideFH_' + i).addClass('hide');
   
    $('#SHTxtbox_' + i).removeClass('hide');
    $('#hideSH_' + i).addClass('hide');
    $('#edit_' + i).addClass('hide');
    $('#updatecancel_' + i).removeClass('hide');
}

function validateEmpWiseEdit(i) {
    if ($('#Employee_DailyAttendanceEditDetails_' + i + '__FirstHalf_Code').val()=='') {
        swal.fire("Please Select First Half"); return false;
    }
    if ($('#Employee_DailyAttendanceEditDetails_' + i + '__SEcondHalf_Code').val() == '') {
        swal.fire("Please Select SEcond Half"); return false;
    }
    if ($('#Employee_DailyAttendanceEditDetails_' + i + '__FHType_Name').val() == "COFF" && $('#Employee_DailyAttendanceEditDetails_' + i + '__SHType_Name').val() == "COFF") {

        if ($('#Employee_DailyAttendanceEditDetails_' + i + '__FHType_Name').val() != "COFF" || $('#Employee_DailyAttendanceEditDetails_' + i + '__SHType_Name').val() != "COFF") {
            swal.fire("COFF cant be applited for Half day");
            return false;
        }

        else {

            var length = $('.coffdropdown_'+i).children('option').length;
            if (length > 1) {
                if ($('#Employee_DailyAttendanceEditDetails_' + i + '__EntitleDate').val()=='') {
                    swal.fire("Please Select Entitle Date"); return false;
                }
            }
            else {
                debugger
                swal.fire("No COFF Balance");
                return false;
            }
        }
    }
   
        return true;
    
}
function onEmployeeDailyAttendSaveClick(index) {
    debugger;    
   
    var formData = new FormData();
    formData.append("SelectedDate", $('#Employee_DailyAttendanceEditDetails_' + index + '__Date').val());
    formData.append("CoffEntitleDate", $('#Employee_DailyAttendanceEditDetails_' + index + '__EntitleDate').val());
    formData.append("SecondHalf.Code", $('#Employee_DailyAttendanceEditDetails_' + index + '__SecondHalf_Code').val());
    formData.append("FirstHalf.Code", $('#Employee_DailyAttendanceEditDetails_' + index + '__FirstHalf_Code').val());
    formData.append("EmployeeGI", $('#EmployeeHeader_EmployeeGuid').val());

      
   
      
    if (validateEmpWiseEdit())
    {
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
                    url: '/TimeOffice/AdvancedEmpWiseAttendEntry/EditSave',
                    dataType: 'json',
                    processData: false, // Prevent jQuery from processing the data
                    contentType: false, // Set to false to let the browser set it automatically
                    data: formData, // Send as FormData
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire("Modified Successfully").then(function () {
                                editAttendance($('#EmployeeHeader_EmployeeGuid').val(), $("#Month_Code").val());


                            });

                        }
                        else {
                            swal.fire(result.message).then(function () {                                              


                                $.ajax({

                                    type: 'GET',
                                    url: '/TimeOffice/AdvancedEmpWiseAttendEntry/Edit',
                                    dataType: 'html',
                                    data: { EmployeeGI: EmpGuid, Date: Date },

                                    success: function (result) {
                                        debugger
                                        if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                                            window.location.reload();
                                        }
                                        else {
                                            $('#eaEntry_Edit_Slider').html("");
                                            $('#eaEntry_Edit_Slider').html(result);
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



function ExportToExcel_EmployeeAttendanceList() {

    debugger;
    var Month = $("#Month_Code").val();
    var PgGroup = $('#PayrollGroup_Code').val();
    var tableLength = $('#EmployeeWiseAttendanceList_Count').val();

    if (tableLength > 0) {
        url = "/TimeOffice/EmployeeWiseAttendanceEntry/ExportToExcel_EmployeeAttendanceList?Month=" + Month + "&Mname=" + $('#Month_Code option:selected').text() + "&PgGRoup=" + PgGroup + "&totalRecords=" + tableLength;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }

}



function ExportToExcel_EmployeeAttendanceEDitList(EmployeeGI) {
    debugger;
    if ($('#Employee_DailyAttendanceEditDetails_Count').val()=="")
        swal.fire("No data found to export");
    else {
        var Month = $("#Month_Code").val();


        var checkBox = document.getElementById("ShowAbsentDays");
        if (checkBox.checked == true)
            var ABSTATUS = 1;
        else
            var ABSTATUS = 0;
        url = '/TimeOffice/EmployeeWiseAttendanceEntry/ExportToExcel_EmployeeAttendanceEDitList';
        window.location.href = url + "?EmployeeGI=" + EmployeeGI + "&Month=" + Month + "&ABSTATUS=" + ABSTATUS + "&Mname=" + $('#Month_Code option:selected').text();
    }
}
    

    function ValidateBatchUpdate() {


        var i = 0;
        if (!validateById('#Employee_DailyAttendanceBatchUpdate_From', 'eaBatch_from')) {
            i++;
        }
        if (!validateById('#Employee_DailyAttendanceBatchUpdate_To', 'eaBatch_to')) {
            i++;
        }
        if (!validateById('#Employee_DailyAttendanceBatchUpdate_FirstHalf_Code', 'eaBatch_first')) {
            i++;
        }
        if (!validateById('#Employee_DailyAttendanceBatchUpdate_SecondHalf_Code', 'eaBatch_second')) {
            i++;
        }

        if (i == 0)
            return true;
        else
            return false;
    }
