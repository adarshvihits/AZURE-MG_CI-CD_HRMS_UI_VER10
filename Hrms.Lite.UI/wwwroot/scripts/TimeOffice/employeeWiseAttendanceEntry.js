const month = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
var global_count = 0;
var global_fh = 0;

var global_sh = 0;
var global_FH_TYPE = 0;
var global_SH_TYPE = 0;
GetAttendanceStatusTypeFH();
GetAttendanceStatusTypesh();
function validatefh(sender) {
    debugger
    flag = 0;
    $.getJSON("/TimeOffice/DailyAttendanceEntry/CheckIffCOFF", { AttendanceStatusCode: $(sender).val() }, function (data) {


        if (data.attendanceStatusType.code == 3 || data.attendanceStatusType.code == 4) {
            swal.fire('Not Allowed To Save PR and AB Status Manually');
            $('#DailyAttendanceEditDetails_FirstHalf_Code').val(global_fh);
            return false;

        }
        if ($("#DailyAttendanceEditDetails_SH_AttendStatusType_ShortName").val() != "PR") {

            $('#DailyAttendanceEditDetails_SecondHalf_Code').val($('#DailyAttendanceEditDetails_FirstHalf_Code').val());
            $("#DailyAttendanceEditDetails_FH_AttendStatusType_ShortName").val(data.attendanceStatusType.shortName);
            $("#DailyAttendanceEditDetails_SH_AttendStatusType_ShortName").val(data.attendanceStatusType.shortName);

        }

        GetAttendanceStatusTypeFH();
        GetAttendanceStatusTypesh();
      


    });
}
function validatesh(sender) {
    debugger
    flag = 0;
    $.getJSON("/TimeOffice/DailyAttendanceEntry/CheckIffCOFF", { AttendanceStatusCode: $(sender).val() }, function (data) {


        if (data.attendanceStatusType.code == 3 || data.attendanceStatusType.code == 4) {
            swal.fire('Not Allowed To Save PR and AB Status Manually');
            $('#DailyAttendanceEditDetails_SecondHalf_Code').val(global_sh);
            return false;

        }
     
        GetAttendanceStatusTypeFH();
        GetAttendanceStatusTypesh();
       
    });
}

function GetAttendanceStatusTypeFH() {
    debugger
    $.getJSON("/TimeOffice/DailyAttendanceEntry/CheckIffCOFF", { AttendanceStatusCode: $('#DailyAttendanceEditDetails_FirstHalf_Code').val() }, function (data) {
        global_FH_TYPE = data.attendanceStatusType.code;
        return false;
    });
}
function GetAttendanceStatusTypesh() {
    debugger
    $.getJSON("/TimeOffice/DailyAttendanceEntry/CheckIffCOFF", { AttendanceStatusCode: $('#DailyAttendanceEditDetails_SecondHalf_Code').val() }, function (data) {
        global_SH_TYPE = data.attendanceStatusType.code;
        EnableDisableCoff(data.attendanceStatusType.code);
        return false;
    });
   
}
function EnableDisableCoff(attendanceStatusType) {


    //if (attendanceStatusType == 5)
    if (global_FH_TYPE == 5 || global_SH_TYPE == 5) {

        $("#entittleDateID").removeClass("hide");
        fillCoffDates();
    }
    else {
        $('#DailyAttendanceEditDetails_CoffEntitleDate').val('');
        $("#entittleDateID").addClass("hide");
    }

    if ($("#DailyAttendanceEditDetails_FH_AttendStatusType_ShortName").val() == "COFF" && $("#DailyAttendanceEditDetails_SH_AttendStatusType_ShortName").val() == "COFF") {

        //var length = $('#DailyAttendanceEditDetails_CoffEntitleDate').children('option').length;
        //if (length == 1) {
        //    swal.fire("No COFF Balance");
        //    return false;
        //}

    }
}
function fillCoffDates(mode) {
    if (mode == "")
        Mode = "CREATE";
    else
        Mode = "EDIT";

    $.getJSON("/TimeOffice/DailyAttendanceEntry/fillCoffDates", { EmployeeGI: $('#DailyAttendanceEditDetails_EmployeeGI').val(), HeaderDate: $('#DailyAttendanceEditDetails_SelectedDate').val(), FH: global_FH_TYPE, SH: global_SH_TYPE, Mode: Mode }, function (data) {
        debugger;
        $('#DailyAttendanceEditDetails_CoffEntitleDate option').remove();
        $('#DailyAttendanceEditDetails_CoffEntitleDate').append('<option value>--Select--</option');
        if (data.length < 1) {
            swal.fire("No COFF Balance");
            return false;
        }
        for (var i = 0; i < data.length; i++) {
            $('#DailyAttendanceEditDetails_CoffEntitleDate').append('<option value="' + data[i].name + '">' + data[i].name + '</option');

        }




    });
}
function checkbalance(sender) {
    debugger;

    $.getJSON("/TimeOffice/DailyAttendanceEntry/CheckCoffBalance", { EmployeeGI: $('#DailyAttendanceEditDetails_EmployeeGI').val(), CoffEntitleDate: $(sender).val() }, function (data) {
        debugger;
        if (global_FH_TYPE != global_SH_TYPE) {

            if (data.coffEntitleDateBalance == 1) {
                swal.fire("The remaining coff will not be added back to your coff balance.");
                return false;

            }

        }
    });
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


function fillMonth(sender) {
    debugger;
    //$.ajaxSetup({
    //    async: false
    //});
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
            "url": "/TimeOffice/EmployeeWiseAttendanceEntry/GetEmployeeWiseList",
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

                  

                    //d.data[i].employeeID = '<div class="d-flex align-items-center" ><div class="img-report"><img src="' + d.data[i].absoluteUri + '"></div> <div> <a href="/DataBank/EmployeeProfile/GetEmployeeProfileDetails?EmployeeGI=' + d.data[i].employeeGI + '&Breadcrumb=SeparationReport&BreadcrumbName=Separation Report List">' + d.data[i].employeeName + '</a> <div>(' + d.data[i].employeeID + ')' + ' </div> </div></div>'
                    //d.data[i].employeeName = '<div class="d-flex align-items-center" ><div class="img-report"><img src="' + d.data[i].absoluteUrl + '"></div><div class="name-post" ><div><a href="/DataBank/EmployeeProfile/GetEmployeeProfileDetails?EmployeeGI=' + d.data[i].employeeGI + '&Breadcrumb=EmployeeProfile&BreadcrumbName=Employee Profile List">' + d.data[i].employeeName + '</a></div>' + d.data[i].employeeid + '</div></div>'
                    d.data[i].fullName = '<a href = "/TimeOffice/EmployeeWiseAttendanceEntry/EmployeeWiseAttendanceEdit?EmployeeGI=' +d.data[i].employeeGI + '&Month=' + Month + '"> ' + d.data[i].fullName+'</a >'

                   
                    
                      


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




function onProceedClick(sender) {
    debugger
    var month = sender.value; 
    
        showSpinner();
        $("#Month_Code").val(month);
        $('.RMonth').val(month);

        $('.reset').val('');
        $('#Optional_Filter_EmploymentStatus_Code').val(0);
        $('#filterForm').removeClass('filter-apply');
        $.ajax({

            type: 'GET',
            url: '/TimeOffice/EmployeeWiseAttendanceEntry/EmployeeWiseAttendanceList',
            dataType: 'html',
            data: { month: month  },

            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('#eaEntry_List_Partial').html("");
                    $('#eaEntry_List_Partial').html(result);
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
        url: '/TimeOffice/EmployeeWiseAttendanceEntry/EmployeeWiseAttendanceEditList',
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


function onEmpWiseProceedClick(EmployeeGI, sender) {
    debugger;
    var month = sender.value;
    $("#Month_Code").val(month);
    $('.RMonth').val(month);
    if (validateById('#Month_Code', 'eaEntry_EditMonth')) {

        url = '/TimeOffice/EmployeeWiseAttendanceEntry/EmployeeWiseAttendanceEdit';
        window.location.href = url + "?EmployeeGI=" + EmployeeGI + "&Month=" + month;
    }
}

function onfilterClick() {
    debugger
   
    $.ajax({

        type: 'GET',
        url: '/TimeOffice/EmployeeWiseAttendanceEntry/ApplyFilter',
        dataType: 'html',    

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#eaEntry_Filter_Partial').html("");
                $('#eaEntry_Filter_Partial').html(result);
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
            url: "/TimeOffice/EmployeeWiseAttendanceEntry/BatchUpdate?EmployeeGI=" + EmployeeGI + "&MonthName=" + name,
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
                    url: '/TimeOffice/EmployeeWiseAttendanceEntry/BatchUpdateSave',
                    dataType: 'json',
                    data: $('#eaEntry_Batch_Form').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire("Updated Successfully").then(function () {
                                CloseSlider('eaEntry_Batch_Slider');
                                debugger
                                var url = '/TimeOffice/EmployeeWiseAttendanceEntry/EmployeeWiseAttendanceEdit';
                                window.location.href = url + "?EmployeeGI=" + EmpGuid + "&Month=" + month;


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
function validateEdit(EmployeeGI, Date, mode, empcode, PGCode) {
    $.ajax({

        type: 'GET',
        url: '/TimeOffice/DailyAttendanceEntry/GetDailyAttendanceEntryDateValidation',
        dataType: 'json',
        data: { SelectedDate: Date, PayrollGroup: PGCode, EmployeeCode: empcode },

        success: function (result) {
            debugger
            if (result.success == true) {
                onEmployeeDailyAttendEditClick(EmployeeGI, Date, mode);
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
function onEmployeeDailyAttendEditClick(EmployeeGI, Date, mode) {
    debugger
    if (mode == "")
        Mode = "CREATE";
    else
        Mode = "EDIT";

    $.ajax({

        type: 'GET',
        url: '/TimeOffice/EmployeeWiseAttendanceEntry/Edit',
        dataType: 'html',
        data: { EmployeeGI: EmployeeGI, Date: Date, Mode: Mode},

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#eaEntry_Edit_Slider').html("");
                $('#eaEntry_Edit_Slider').html(result);
                $('body').append('<div class="form-overlay"></div>');

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

function validateEmpWiseEdit() {

    var i = 0;
    if (!validateById('#DailyAttendanceEditDetails_FirstHalf_Code', 'ea_fisrtHalf')) {
        i++;
    }
    if (!validateById('#DailyAttendanceEditDetails_SecondHalf_Code', 'ea_secondHalf')) {
        i++;
    }
    debugger
    if ($("#DailyAttendanceEditDetails_FH_AttendStatusType_ShortName").val() == "COFF" || $("#DailyAttendanceEditDetails_SH_AttendStatusType_ShortName").val() == "COFF") {

        if ($("#DailyAttendanceEditDetails_SH_AttendStatusType_ShortName").val() != "COFF" || $("#DailyAttendanceEditDetails_FH_AttendStatusType_ShortName").val() != "COFF") {
            swal.fire("COFF cant be applited for Half day");
            return false;
        }

        else {

            var length = $('#DailyAttendanceEditDetails_CoffEntitleDate').children('option').length;
            if (length > 1) {
                if (!validateById('#DailyAttendanceEditDetails_CoffEntitleDate', 'ea_CoffDate'))
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
function onEmployeeDailyAttendSaveClick() {
    debugger;
    var Date = $('#DailyAttendanceEditDetails_SelectedDate').val();
    var month = $('#Month_Code').val();
    var EmpGuid = $('#DailyAttendanceEditDetails_EmployeeGI').val();

      
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
                    url: '/TimeOffice/EmployeeWiseAttendanceEntry/EditSave',
                    dataType: 'json',
                    data: $('#eaEntry_Edit_Form').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire("Modified Successfully").then(function () {
                                debugger
                                CloseSlider('eaEntry_Edit');
                                url = '/TimeOffice/EmployeeWiseAttendanceEntry/EmployeeWiseAttendanceEdit';
                                window.location.href = url + "?EmployeeGI=" + EmpGuid + "&Month=" + month;


                            });

                        }
                        else {
                            swal.fire(result.message).then(function () {                                              


                                $.ajax({

                                    type: 'GET',
                                    url: '/TimeOffice/EmployeeWiseAttendanceEntry/Edit',
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
    var payroll = $("#PayrollGroup_Code").val();
    var pname = $('#PayrollGroup_Code option:selected').text();
    var tableLength = $('#EmployeeWiseAttendanceList_Count').val();

    if (tableLength > 0) {
        url = "/TimeOffice/EmployeeWiseAttendanceEntry/ExportToExcel_EmployeeAttendanceList?Month=" + Month+
        "&Mname=" + $('#Month_Code option:selected').text() + "&totalRecords=" + tableLength + "&PgGRoup=" + payroll + "&Pname=" + pname;
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





















