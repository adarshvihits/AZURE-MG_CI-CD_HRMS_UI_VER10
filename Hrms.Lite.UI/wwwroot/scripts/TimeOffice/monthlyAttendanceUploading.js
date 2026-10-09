function OnApplyAndLockClick() {
    debugger
    var i = 0;
    debugger;

    if (!validateById('#MonthCode', 'MAU_month')) {
        i++;
    }

    if (!validateById('#PayrollGroup', 'MAU_PG')) {
        i++;
    }
    if (i == 0) {
        swal.fire({
            text: "Once the month is locked no further changes is the attendance module can be done. The month will not display in the drop down of any transactions. Do you want to Lock?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({

                    type: 'POST',
                   
                    url: '/TimeOffice/MonthlyAttendanceProcessing/ProcessingPeriodLock',
                    dataType: 'json',
                    data: $('#BasicFilter').serialize() + "&Month=" + $("#MonthCode").val() + "&PayrollGroup=" + $("#PayrollGroup").val(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                window.location.href = '/TimeOffice/MonthlyAttendanceUploading/Index';
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
        })
    }

}
function fillMonth(sender) {
    debugger;
    $.getJSON("/TimeOffice/MonthlyAttendanceUploading/GetMonth", { PayrollGrp: $(sender).val() }, function (data) {

        $('#MonthCode option').remove();
        $('#MonthCode').append('<option value>--Select-- </option');
        debugger;
        for (var i = 0; i < data.length; i++) {
            $('#MonthCode').append('<option value="' + data[i].code + '">' + data[i].name + '</option');
        }

    });
}
function ApplyFilter(mode) {
    debugger  

    
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
function ExportToExcel() {
    debugger
    var month = $("#MonthCode").val();
    var monthName = $("#MonthCode option:selected").text();
    var PG = $("#PayrollGroup").val();
    var PGNAME = $("#PayrollGroup option:selected").text();
    var TableLength = $('#MonthlyAttendanceUploading_Data_Count').val();
    if (TableLength > 0) {
        url = "/TimeOffice/MonthlyAttendanceUploading/ExportToExcel?month=" + month + "&monthName=" + monthName + "&TotalRow=" + TableLength+ '&Pg=' + PG + "&PgName=" + PGNAME+
            '&data=' + $('#BasicFilter').serialize() ;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}
6




function validateMonthAndPG() {
    var i = 0;
    debugger;
  
    if (!validateById('#MonthCode', 'MAU_month')) {
        i++;
    }

    if (!validateById('#PayrollGroup', 'MAU_PG')) {
        i++;
    }
    if (i == 0) {        
        var m = $('.MAU_ExcelDiv');
        m.modal('show');

    }

}

function fillexistingData(index)
{
    debugger
    $('#newLOP_' + index).val($("#MonthlyAttendanceUploading_Data_"+index+"__Lop").val());
    $('#newHP_' + index).val($("#MonthlyAttendanceUploading_Data_" + index + "__HolidayPresent").val());
    $('#newOP_' + index).val($("#MonthlyAttendanceUploading_Data_" + index + "__OffdayPresent").val());
    $('#newLateHrs_' + index).val($("#MonthlyAttendanceUploading_Data_" + index + "__LateHrs").val());
    $('#newOTHrs_' + index).val($("#MonthlyAttendanceUploading_Data_" + index + "__OTHrs").val());
}
function onEditClick(i) {
    debugger

    fillexistingData(i);
    $('#lopLabel_' + i).addClass('hide');      
    $('#lopTxtbox_' + i).removeClass('hide');

    $('#opLabel_' + i).addClass('hide');
    $('#opTxtbox_' + i).removeClass('hide');

    $('#hpLabel_' + i).addClass('hide');
    $('#hpTxtbox_' + i).removeClass('hide');

    $('#lateHrsLabel_' + i).addClass('hide');
    $('#lateHrsTxtbox_' + i).removeClass('hide');

    $('#otHrsLabel_' + i).addClass('hide');
    $('#otHrsTxtbox_' + i).removeClass('hide');


    $('#editbt_' + i).addClass('hide');
    $('#updateCancel_' + i).removeClass('hide');
    //moveCursorToEnd('newAmount_' + i);
}



function onCancelClick(i) {
    $('#lopLabel_' + i).removeClass('hide');   
    $('#lopTxtbox_' + i).addClass('hide');

    $('#opLabel_' + i).removeClass('hide');
    $('#opTxtbox_' + i).addClass('hide');

    $('#hpLabel_' + i).removeClass('hide');
    $('#hpTxtbox_' + i).addClass('hide');

    $('#lateHrsLabel_' + i).removeClass('hide');
    $('#lateHrsTxtbox_' + i).addClass('hide');

    $('#otHrsLabel_' + i).removeClass('hide');
    $('#otHrsTxtbox_' + i).addClass('hide');




    $('#editbt_' + i).removeClass('hide');
    $('#updateCancel_' + i).addClass('hide');
    
}
function onUpdateClick(i, EmployeeGI) {
    debugger
    var Month = $("#MonthCode").val();
   
    var LOP = $('#newLOP_' + i).val();
    var HP = $('#newHP_' + i).val();
    var OP = $('#newOP_' + i).val();
    var LATE = $('#newLateHrs_' + i).val();
    var OT = $('#newOTHrs_' + i).val();
   

    debugger
    $.ajax({
        type: 'POST',
        url: '/TimeOffice/MonthlyAttendanceUploading/Update',
        dataType: 'json',
        data: { EmployeeGI: EmployeeGI, Month: Month, LOP: LOP, HP: HP, OP: OP,  LATE: LATE, OT:OT},
        success: function (result) {
            debugger;
            if (result.success == true) {
                swal.fire("Updated Successfully").then(function () {
                    
                    fillData();

                });

            }
            else {
                swal.fire(result.message);                
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





function onImportExcelClick() {
    debugger    
    swal.fire({
        text: "Do you want to Upload?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            var MPPersonalInfo = new FormData($('#MAU_excelform')[0]);
            $("#Excel_FileData").val('');
            $.ajax({

                type: 'POST',
                url: '/TimeOffice/MonthlyAttendanceUploading/ImportFromExcel',
                contentType: false,
                processData: false,
                data: MPPersonalInfo,
                success: function (result) {
                    debugger;
                    if (result.success == true) {
                        $("#Excel_FileData").val('');
                        swal.fire("Uploaded Successfully").then(function () {                           
                            fillData();
                            var m = $('.MAU_ExcelDiv');
                            m.modal('hide');
                        });
                    }
                    else {

                        $("#Excel_FileData").val('');
                        if (result.message != null) {
                            $('#Unmatchedlist').html('');
                            swal.fire(result.message);
                        }
                        else {
                            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                                window.location.reload();
                            }
                            else {
                                $('#Unmatchedlist').html(result)
                            }
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

function fillData() {

    debugger;
    var filter1 = $('#BasicFilter').serialize();

    var temp = filter1;
    var month = $("#MonthCode").val();
    var PG = $("#PayrollGroup").val();

    var dataTable = $("#MAUploadingTable").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/TimeOffice/MonthlyAttendanceUploading/GetEmployeeList",

            "type": "POST",
            "datatype": "json",
            'data':

                function (d) {
                    temp = '';

                    var start = d.start;
                    var searchValue = $('#dataTableSearch').val();
                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' +
                        searchValue + '&Month=' + month +"&PayrollGroup="+PG+ '&dummy=dummy';

                    d = temp;

                    return JSON.stringify(d);
                },




            "dataSrc": function (d) {
                $('#MonthlyAttendanceUploading_Data_Count').val(d.recordsTotal);

                for (var i = 0, ien = d.data.length; i < ien; i++) {

                    d.data[i].employeeID = '<input id="MonthlyAttendanceUploading_Data_' + i + '__EmployeeGI" type="hidden" value="' + d.data[i].employeeGI + '">' + d.data[i].employeeID

                    d.data[i].lop = '<input id = "MonthlyAttendanceUploading_Data_' + i + '__Lop" type = "hidden" value = "' + d.data[i].lop + '" ><span id = "lopLabel_' + i + '">' + d.data[i].lop + '</span><span id="lopTxtbox_' + i + '" class="hide"><input type = "text" id = "newLOP_' + i + '" class="form-control form-control-sm"  maxlegth="6" onkeypress = "return onlyAmount(event)"></span>'

                    d.data[i].offdayPresent = '<input id = "MonthlyAttendanceUploading_Data_' + i + '__OffdayPresent" type = "hidden" value = "' + d.data[i].offdayPresent + '" ><span id = "opLabel_' + i + '">' + d.data[i].offdayPresent + '</span><span id="opTxtbox_' + i + '" class="hide"><input type = "text" id = "newOP_' + i + '" class="form-control form-control-sm"  maxlegth="6" onkeypress = "return onlyAmount(event)"></span>'

                    d.data[i].holidayPresent = '<input id = "MonthlyAttendanceUploading_Data_' + i + '__HolidayPresent" type = "hidden" value = "' + d.data[i].holidayPresent + '" ><span id = "hpLabel_' + i + '">' + d.data[i].holidayPresent + '</span><span id="hpTxtbox_' + i + '" class="hide"><input type = "text" id = "newHP_' + i + '" class="form-control form-control-sm"  maxlegth="6" onkeypress = "return onlyAmount(event)"></span>'

                    d.data[i].lateHrs = '<input id = "MonthlyAttendanceUploading_Data_' + i + '__LateHrs" type = "hidden" value = "' + d.data[i].lateHrs + '" ><span id = "lateHrsLabel_' + i + '">' + d.data[i].lateHrs + '</span><span id="lateHrsTxtbox_' + i + '" class="hide"><input type = "text" id = "newLateHrs_' + i + '" class="form-control form-control-sm"  maxlegth="6" onkeypress = "return onlyAmount(event)"></span>'

                    d.data[i].otHrs = '<input id = "MonthlyAttendanceUploading_Data_' + i + '__OTHrs" type = "hidden" value = "' + d.data[i].otHrs + '" ><span id = "otHrsLabel_' + i + '">' + d.data[i].otHrs + '</span><span id="otHrsTxtbox_' + i + '" class="hide"><input type = "text" id = "newOTHrs_' + i + '" class="form-control form-control-sm"  maxlegth="6" onkeypress = "return onlyAmount(event)"></span>'


                    d.data[i].remarks = '<span id="editbt_' + i + '"><a onmouseenter="tooltipHover()" onclick = "onEditClick(' + i + ')"  data-bs-toggle="tooltip" title = "Edit" type = "button"class="btn btn-icon btn-edit icon-only" ><i class="icon-xl fas fa-pencil-alt"></i></a></span><span id = "updateCancel_' + i + '" class="hide"><a onmouseenter="tooltipHover()" type = "button" data-bs-toggle="tooltip" title = "Update" class="btn btn-icon btn-update icon-only " onclick = "onUpdateClick(' + i + ',' + "'" + d.data[i].employeeGI + "'" + ')"> <i class="icon-xl fas fa-sync-alt"></i> </a><a onmouseenter="tooltipHover()" type = "button" data-bs-toggle="tooltip" title = "Cancel" class="btn btn-icon btn-cancel icon-only " onclick = "onCancelClick(' + i + ')"> <i class="icon-xl fas fa-times"></i></a></span>'

                }


                return d.data;
            }

        },

        "columns": [

            { "data": "employeeID" },
            { "data": "fullName" },
            { "data": "presentDays" },
            { "data": "ra" },
            { "data": "absent" },
            { "data": "lop" },
            { "data": "esiLeave" },
            { "data": "leave" },
            { "data": "coff" },
            { "data": "onDuty" },
            { "data": "wfh" },
            { "data": "holiDay" },
            { "data": "holidayPresent" },
            { "data": "offDay" },
            { "data": "offdayPresent" },
            { "data": "lateHrs" },
            { "data": "otHrs" },
            { "data": "ljp" },
            { "data": "ltp" },
            { "data": "previousLOP" },
            { "data": "previousArrear" },
            { "data": "suspension" },
            { "data": "totalLOP" },
            { "data": "paidDays" },
            { "data": "remarks" },
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