function CheckifAlreadyProcessed()
{
    $.getJSON("/TimeOffice/DailyAttendanceLog/CheckifAlreadyProcessed", { Date: $("#SelectedDate").val() }, function (data) {
        console.log(data);
        debugger
        if (data.success == true) {
            swal.fire({
                text: "Do you want to Process?",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Ok'
            }).then((result) => {
                if (result.isConfirmed) {
                /*    AjaxCall(event);*/
                    fillData()

                }

            })
        }
        else
            swal.fire(data.message);


    });
}

function fillData() {


    debugger;
    var seldate = $("#SelectedDate").val();

    var filter1 = $('#BasicFilter').serialize();

    var temp = filter1;
   
    var search = $('#SearchId').val();
    var filtercode = $("#FilterCombo_Code").val();
    var dataTable = $("#daily_attendance_Log_Table").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/TimeOffice/DailyAttendanceLog/GetList",
            "type": "POST",

            "datatype": "json",


            'data':

                function (d) {
                    temp = '';


                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();


                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&SelectedDate=' + seldate + '&SearchId=' + search + '&dummy=dummy';
                    d.filter = temp;


                    return JSON.stringify(d);
                },




            "dataSrc": function (d) {



                $('#LogList_Count').val(d.recordsTotal);

                debugger;


                for (var i = 0, ien = d.data.length; i < ien; i++)
                {
                    

                    d.data[i].inTime = '<div class="form-group" onmouseenter="timepicker()"><div class="input-group "><input class="form-control form-control-sm  kt_timepicker_2 inTIME" id="inTime_'+i+'" name="InTime"  placeholder="In Time" type="text" value="' + d.data[i].inTime + '"><div class="input-group-append"><span class="input-group-text"><i class="la la-clock-o"></i></span></div></div><span id="inTime"></span></div>'

                    d.data[i].outTime = '<div class="form-group" onmouseenter="timepicker()"><div class="input-group "><input class="form-control form-control-sm  kt_timepicker_2 outTIME" id="outTime_'+i+'" name="OutTime"  placeholder="Out Time" type="text" value="' + d.data[i].outTime + '"><div class="input-group-append"><span class="input-group-text"><i class="la la-clock-o"></i></span></div></div><span id="outTime"></span></div>'


                    d.data[i].selectedDate = '<a onmouseenter="tooltipHover()" data-bs-toggle="tooltip" title = "Update"class="btn btn-icon btn-update icon-only " onclick = "onUpdateClick(' + "'" + d.data[i].employeeGI + "'" + ',' + i +  ')" > <i class="icon-xl fas fa-sync-alt"></i></a ><a data-bs-toggle="tooltip" title="Cancel"class="btn btn-icon btn-cancel icon-only " onclick="onDeleteClick(' + i + ')"><i class="icon-xl fas fa-times"></i></a>'



                }

                return d.data;
            }

        },

        "columns": [

            { "data": "employeeID","width":"10%" },
            { "data": "employeeName","width": "16%"},
            { "data": "designation.name","width": "16%" },
            { "data": "department.name","width": "16%"},
            { "data": "location.name","width": "12%"},
            { "data": "inTime","width": "10%"},
            { "data": "outTime","width": "10%"},
            { "data": "selectedDate","width": "10%" },
            //{ "data": "employeeCode" },
          



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


    setTimeout(timepicker, 1000);
    const myTimeout = setTimeout(tooltipHover, 500);
}


function timepicker() {
  
    debugger
    $('.kt_timepicker_2, .kt_timepicker_2_modal').timepicker({
        minuteStep: 1,
        defaultTime: '',
        showSeconds: false,
        showMeridian: false,
        autoclose: true
    });
}



function onBatchClick()
{
   var i = 0;
    
    if (!validateById('#SelectedDate', 'dailylog_date')) {
        i++;
    }
    var TableLength = $('#LogList_Count').val();
    if (TableLength == 0) {
        swal.fire("No data found for Batch Update");
        i++;
    }
    if (i == 0) {
        $.ajax({

            type: 'GET',
            url: '/TimeOffice/DailyAttendanceLog/BatchUpdate',
            dataType: 'html',
            success: function (Data) {
                var m = $('.logBatchUpdate_modal');
                m.find('.modal-body').html('');
                m.find('.modal-body').html(Data);
                m.modal('show');
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

    $("#InTime").val('');
    $("#OutTime").val('');

}

function onDeleteClick(index) 
{

   $('#inTime_' + index).val('');
    $('#outTime_' + index).val('');
}
function ApplyFilter(mode) {
    debugger;


  /*  showSpinner();*/
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
function onFilterClick() {
    //    debugger
    ///*    if (validateEntryDropdowns()) {*/
    $.ajax({

        type: 'GET',
        url: '/TimeOffice/DailyAttendanceLog/Filter',
        dataType: 'html',
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                         
                 $('#dailyLog_Filter').html("");
                $('#dailyLog_Filter').html(result);
                $("#filterForm").addClass('slider-forms-open');

               
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
function onUpdateClick(EmployeeGI,index)
{
    debugger;

    var In = $('#inTime_'+index).val();
    var OUT = $('#outTime_' + index).val();
    var SelectedDate = $("#SelectedDate").val();
    if (In != "" & OUT != "") {
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
                    url: '/TimeOffice/DailyAttendanceLog/EmployeeWiseDailyAttendanceLogSave',
                    dataType: 'json',
                    data: { EmployeeGI: EmployeeGI, SelectedDate: $("#SelectedDate").val(), InTime: In, OutTime: OUT },
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire("Updated Successfully").then(function () {
                                fillData();
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
    else {
        return false;
    }
}
function logSave() {

    debugger;
    swal.fire({
        text: "Do you want to Save?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({

                type: 'POST',
                url: '/TimeOffice/DailyAttendanceLog/DailyAttendanceLogSave',
                dataType: 'json',
                data: $('#dailyLog_List_Form').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        debugger
                        swal.fire("Saved Successfully").then(function () {                          

                            fillData();
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

function BatchUpdateSave() {

    var i = 0;
    debugger

    if (!validateById('#InTime', 'log_batch_in')) {
        i++;
    }
    if (!validateById('#OutTime', 'log_batch_out')) {
        i++;
    }
    if (i == 0) {
        debugger;
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
                    url: '/TimeOffice/DailyAttendanceLog/DailyAttendanceLogBatchUpdate',
                    dataType: 'json',
                    data: $('#dailyLog_Batch').serialize() + "&SelectedDate=" + $("#SelectedDate").val(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire("Updated Successfully").then(function () {

                                var url = '/TimeOffice/DailyAttendanceLog/Index';
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
            else {
                return false;
            }
        })

    }


}

function ExportToExcel() {
    debugger;
    /*if (validateDropdowns()) {*/
        
    var TableLength = $('#LogList_Count').val();
        if (TableLength > 0) {
            url = "/TimeOffice/DailyAttendanceLog/ExportToExcel?SelectedDate=" + $('#SelectedDate').val() + "&TableLength=" + TableLength;
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
    }
