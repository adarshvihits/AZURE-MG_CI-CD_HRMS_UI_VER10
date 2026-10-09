

function fillData() {

    debugger;
    var filter1 = $('#BasicFilter').serialize();
    var AttendStatus = $("#AttnDay_Code").val();
    var temp = filter1;
    var date = $("#ProcesingDate").val();


    var dataTable = $("#AttendanceProcessingTable").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/TimeOffice/DailyAttendanceProcessing/GetList",
            "type": "POST",


            "datatype": "json",


            'data':

                function (d) {
                    temp = '';

                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();

                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&date=' + date+ '&AttendStatus=' + AttendStatus+ '&dummy=dummy';
                    d.filter = temp;


                    return JSON.stringify(d);
                },




            "dataSrc": function (d) {





               
                $('#DailyAttendanceProcessing_Count').val(d.recordsTotal);
                for (var i = 0, ien = d.data.length; i < ien; i++) {


                    //d.data[i].employeeid = '<div class="d-flex align-items-center"><div class="img-report"><img src="' + d.data[i].absoluteUri + '">  </div> <div class="name-post"><div>  <a href = "/DataBank/EmployeeProfile/GetEmployeeProfileDetails?EmployeeGI=' + d.data[i].employeeGI + '&Breadcrumb=JoiningReport&BreadcrumbName=Joining Report List" >' + d.data[i].employeeName + ' </a > </div> <div> (' + d.data[i].employeeid + ')' + ' </div></div> </div>'


                }

                return d.data;
            }

        },

        "columns": [

            { "data": "employeeID", "width": "9%" },
            { "data": "fullName", "width": "10%" },
            { "data": "designation.name", "width": "9%" },
            { "data": "department.name", "width": "9%" },
            { "data": "location.name", "width": "8%" },
            { "data": "shift.name", "width": "7%" },
            { "data": "in", "width": "5%" },
            { "data": "out", "width": "6%" },
            { "data": "totalTime", "width": "7%" },
            { "data": "inShiftTime", "width": "7%" },
            { "data": "lateIn", "width": "5%" },
            { "data": "earlyOut", "width": "6%" },
            { "data": "earlyIn", "width": "5%" },           
           
            { "data": "overTime", "width": "6%" },
            { "data": "session", "width": "6%" },         


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
    $.fn.dataTable.ext.errMode = function (settings, helpPage, message) {
        console.log(message);
    };
}



function validatefilter()
{
    debugger
    var i = 0
    if (!validateById('#ProcesingDate', 'att_process_date'))
        i++;
    if (i == 0) {

        $(".filteropen").addClass('slider-btn');
        $('.slider-btn').click(function () {
            $('body').append('<div class="form-overlay"></div>');
            $("div").removeClass('slider-forms-open');
            var elementClassName = $(this).attr('Id');

            $('div#' + elementClassName).toggleClass('slider-forms-open');
        });
        filterApplicable = 1;
        $('.reset').val('');
        $('#Optional_Filter_EmploymentStatus_Code').val(0);
        $('#filterForm').removeClass('filter-apply');
    }
}
function Validate(event) {
    debugger
    var i = 0
    if (!validateById('#ProcesingDate', 'att_process_date'))
        i++;

    if (i == 0) {
       
        $(".filteropen").addClass('slider-btn');
        $('.slider-btn').click(function () {
            $('body').append('<div class="form-overlay"></div>');
            $("div").removeClass('slider-forms-open');           
            var elementClassName = $(this).attr('Id');
       
            $('div#' + elementClassName).toggleClass('slider-forms-open');
        });
        filterApplicable = 1;
        $('.reset').val('');
        $('#Optional_Filter_EmploymentStatus_Code').val(0);
        $('#filterForm').removeClass('filter-apply');
        if (event == "onclick") {
            var ProcDate = new Date($("#ProcesingDate").val());
            var CurrDate = new Date();
            CurrDate = new Date(_GetFormattedDate(CurrDate));       
            var msg = "";
            var showCancelButton = true;  // Default behavior
            debugger
            if (ProcDate.setHours(0, 0, 0, 0) > CurrDate.setHours(0, 0, 0, 0)) {
                msg = "You have selected a future date. All employees will be marked as AB unless the selected date is a holiday or a weekly off.So You can't proceed this action.";
                showCancelButton = false;  // Hide cancel button
            }
            else if (ProcDate.setHours(0, 0, 0, 0) == CurrDate.setHours(0, 0, 0, 0))
                msg = "The current day attendance may not be accurate since the In/Out punches may not be done by the employee. Suggest the attendance processing after all the shifts are completed."
            else
                msg = "Do you want to Process?"

                $.getJSON("/TimeOffice/DailyAttendanceProcessing/CheckifAlreadyProcessed", { Date: $("#ProcesingDate").val() }, function (data) {
                    console.log(data);
                    debugger
                    if (data.success == true) {
                        swal.fire({
                            text: msg,
                            icon: 'warning',
                            showCancelButton: showCancelButton,
                            confirmButtonColor: '#3085d6',
                            cancelButtonColor: '#d33',
                            confirmButtonText: 'Ok'
                        }).then((result) => {
                            if (result.isConfirmed) {
                                if (showCancelButton) {
                                    AjaxCall(event);  // Proceed only if cancel button is shown
                                }
                            }

                        })
                    }
                    else
                        swal.fire("The Selected Date is Locked..Please check..");


                });
           

        }
        else
            AjaxCall(event);
    }
}

function AjaxCall(event)
{
    showSpinner();
    $.ajax({

        type: 'POST',
        url: '/TimeOffice/DailyAttendanceProcessing/GetDailyAttendanceProcessedSummary',
        dataType: 'html',
        data: $('#BasicFilter').serialize() + "&Date=" + $("#ProcesingDate").val() + "&Event=" + event,
        success: function (result) {
            debugger
            if (result == 0)
                swal.fire('ERROR');
            else {
                if (event == 'onclick') {
                    swal.fire("Daily Attendance has been successfully processed").then(function () {
                        $('#dap_summary').html("");
                        $('#dap_summary').html(result);
                    });
                }
                else {
                    $('#dap_summary').html("");
                    $('#dap_summary').html(result);
                }
               
            }
            hideSpinner();

        },
        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
            
        }
    });
}
    function ProceessToSave(event)
    {
        debugger
       
    }

function ExportToExcel(status) {

    debugger;
    var Attdate = $("#ProcesingDate").val();
    //var status = $("#AttnDay_Name").val();

    var tableLength = $('#DailyAttendanceProcessing_Count').val();
    if (tableLength > 0) {
        url = "/TimeOffice/DailyAttendanceProcessing/ExportToExcel?date=" + Attdate + "&totRecords=" + tableLength + "&AttendStatus=" + status
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }

}

function exportToexcel_PunchingId() {

    debugger;
    var Attdate = $("#ProcesingDate").val();
    //var status = $("#AttnDay_Name").val();

    var tableLength = $('#DailtAttendance_popup_Count').val();
    if (tableLength > 0) {
        url = "/TimeOffice/DailyAttendanceProcessing/ExportToExcelPunchingIdValidation?SelectedDate=" + Attdate
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }

}


function ApplyFilter(mode) {
    debugger

    var form = $("#FormName").val();
    if (mode == 'apply') {
        if (form == "innerList") {
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
        else
        {
            if (filterApplicable >= 1) {
                AjaxCall('onclick');
                CloseSlider('basicFilter');
                $('#filterForm').removeClass('filter-apply');
            }
            else {
                AjaxCall('onclick');
                CloseSlider('basicFilter');
                $('#filterForm').addClass('filter-apply');
            }
        }

        
    }
    else {

        if (form == "innerList") {
            filterApplicable = 1;
            $('.reset').val('');
            $('#Optional_Filter_EmploymentStatus_Code').val(0);
            $('#filterForm').removeClass('filter-apply');
            fillData();
        }
        else
        {
            filterApplicable = 1;
            $('.reset').val('');
            $('#Optional_Filter_EmploymentStatus_Code').val(0);
            $('#filterForm').removeClass('filter-apply');
            AjaxCall('onclick');
        }
       
    }
}


function ValidatePunchingId() {
    showSpinner();
    var i = 0;
    if (!validateById('#ProcesingDate', 'att_process_date'))
        i++;
    if (i == 0) {
        $.ajax({

            type: 'POST',
            url: '/TimeOffice/DailyAttendanceProcessing/DailyAttendanceProcessingValidation',           
            data: { SelectedDate: $("#ProcesingDate").val() },
            success: function (result) {
                debugger
                if (result.success == true)
                    Validate('onclick');
                else {
                    var m = $('.punchingIDPopUp');
                    m.find('.modal-body').html('');
                    m.find('.modal-body').html(result);
                    m.modal('show');
                }
                hideSpinner();

            },
            error: function (xhr, textStatus, errorThrown) {
                debugger
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);

            }
        });
    }
   
}