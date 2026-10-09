var isselected;


function validateDropdowns() {
    if (validateById('#AttendanceStatus_Code', 'Allowance')) {
        return true;
    }
    else {
        return false;
    }
}



function fillData() {

    debugger;

    var filter1 = $('#BasicFilter').serialize();

    var temp = filter1;




    var LeaveCode = $('#AttendanceStatus_Code').val();

    /* if (Allowance != '') {*/
    var dataTable = $("#SettingsEmployeeListtable").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/PayRoll/LeaveEncashmentSettings/GetSettingsList",
            "type": "POST",


            "datatype": "json",


            'data':

                function (d) {
                    temp = '';




                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();


                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&LeaveCode=' + LeaveCode + '&dummy=dummy';
                    d.filter = temp;

                    return JSON.stringify(d);
                },




            "dataSrc": function (d) { 
                $('#SettingsEmployeeList_Count').val(d.recordsTotal);
                for (var i = 0, ien = d.data.length; i < ien; i++) {
                    var a = "calctyvalid_"+i;
                    debugger;
                    d.data[i].employee.employeeCode = '<span  onmouseenter="tooltipHover()" id = "hideAction_' + i + '" ><a data-bs-toggle="tooltip" title="Edit" type="button"class="btn btn-icon btn-edit icon-only" onclick="onEditClick(' + i + ',' + d.data[i].rate + ')"><i class="icon-xl fas fa-pencil-alt"></i></a></span ><div class="small-btn-group save_Cancel_' + i + ' hide"><a type="button" data-bs-toggle="tooltip" title="Update" class="btn btn-icon btn-update btn-cancel icon-only"><i class="icon-xl fas fa-sync-alt update-icon" onclick="onUpdateClick(' + i + ', ' + "'" + d.data[i].employee.employeeGI + "'" + ', ' + d.data[i].calcType.code + ')"></i></a><a type="button" data-bs-toggle="tooltip" title="Cancel" class="btn btn-icon btn-cancel icon-only"><i class="icon-xl fas fa-times cancel-btn" onclick="onCancelClick(' + i + ')"></i></a></div>'
                    d.data[i].rate = ' <td data-label="Rate" id="row_' + i + '"><span id="hideRate_' + i + '">' + d.data[i].rate + '</span><span id="rate12"></span><span id="raterow_' + i + '" class="hide"><input type"text" id="newRate_' + i + '" class="form-control form-control-sm" placeholder="Amt/RT in %" maxlength="8" onkeypress="return onlyNumberKey(event)" onpaste="return false"><span id="rate"></span></span></td>'
                    if (d.data[i].calcType.code==0)
                        d.data[i].calcType.name = '<td data-label="CalculationType" id="row_' + i + '"><div class="form-group"><span id="hidecalctype_' + i + '">' + d.data[i].calcType.name + '</span><span id="calctyperow_' + i + '" class="hide"><span id="calc"> </span></span></td><select class="form-control form-control-sm hide"  id = "CalcTypeCode_' + i + '"  name = "CalcTypeCode" onchange = "validate(this,\'' + a + '\')"><option value="" selected>' + '--Select--' + '</option> <option value=' + '1' + '>Fixed Amount</option>  <option value=' + '2' + '>Percentage Wise</option> </select><span id="calctyvalid_' + i + '"></span></div>'
                    else if (d.data[i].calcType.code == 1)
                        d.data[i].calcType.name = '<td data-label="CalculationType" id="row_' + i + '"><div class="form-group"><span id="hidecalctype_' + i + '">' + d.data[i].calcType.name + '</span><span id="calctyperow_' + i + '" class="hide"><span id="calc"> </span></span></td><select class="form-control form-control-sm hide"  id = "CalcTypeCode_' + i + '"  name = "CalcTypeCode" onchange = "validate(this,\'' + a + '\')"><option value="">' + '--Select--' + '</option> <option value=' + '1' + ' selected>Fixed Amount</option>  <option value=' + '2' + '>Percentage Wise</option> </select><span id="calctyvalid_' + i + '"></span></div>'
                    else
                        d.data[i].calcType.name = '<td data-label="CalculationType" id="row_' + i + '"><div class="form-group"><span id="hidecalctype_' + i + '">' + d.data[i].calcType.name + '</span><span id="calctyperow_' + i + '" class="hide"><span id="calc"> </span></span></td><select class="form-control form-control-sm hide"  id = "CalcTypeCode_' + i + '"  name = "CalcTypeCode" onchange = "validate(this,\'' + a + '\')"><option value="">' + '--Select--' + '</option> <option value=' + '1' + '>Fixed Amount</option>  <option value=' + '2' + ' selected>Percentage Wise</option> </select><span id="calctyvalid_' + i + '"></span></div>'
                   
                }

                return d.data;
            }

        },

        "columns": [

            { "data": "employee.employeeID", "width": "10%" },
            { "data": "employee.fullName", "width": "20%" },
            { "data": "designationName", "width": "15%" },
            { "data": "departmentName", "width": "15%" },
            { "data": "locationName", "width": "10%" },
            { "data": "calcType.name", "width": "12%" },
            { "data": "rate", "width": "10%" },
            { "data": "employee.employeeCode", "width": "8%" },

        ],






        "dom": '<t><"my-wrapper"<"my-inner2"l><"my-inner1"i><"my-inner3"p>>',

        "serverSide": "true",
        "scrollY": '52vh',
        "sScrollX": "100%",
        "sScrollXInner": "100%",
        "AutoWidth": false,

        "order": [0, "asc"],
        "processing": "true",
        "language": {

            emptyTable: '<div style="text-align: center;display: flex;align-items: center;justify-content: center;height: 45vh;"><img src="/images/nodata.png" /></div >',

            zeroRecords: '<div style="text-align: center;display: flex;align-items: center;justify-content: center;height: 45vh;"><img src="/images/nodata.png" /></div >',

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


    setTimeout(tooltipHover, 500);


    //}
    //else {
    //    return false;
    //}

}


function validateBatchUpdateDefaultDropdowns() {
    debugger

    var i = 0;
    if (!validateById('#CalcTypeCode', 'CalcType')) {
        i++;
    }
    if (!validateById('#Rate', 'RateValidation')) {
        i++;
    }

    if (i == 0)
        return true;
    else
        return false;


}
function applicableAllowanceClick() {
    debugger;
    //var url = "/PayRoll/OvertimeSettings/GetApplicableAllowanceList";
    //$('#applicableallowance').load(url, {}, function () { });
    //CloseSlider('applicableAllowanceSlider');
    var AttendanceStatus = $('#AttendanceStatus_Code').val();
    if (AttendanceStatus == "") {
        swal.fire("Please Select Leave Encashment Allowance");
    }
    else {


        $.ajax({

            type: 'GET',
            url: '/PayRoll/LeaveEncashmentSettings/GetApplicableAllowanceList',
            dataType: 'html',
            data: { AttendanceStatus: AttendanceStatus },
            success: function (result) {
                debugger;
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }

                else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('#applicableallowance').html("");
                    $('#applicableallowance').html(result);

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
function onAllowanceCheckboxClick(chkid, tableid) {
    debugger;
    if ($('#' + chkid).is(':checked'))
        $('#' + tableid).find('input[type=checkbox]').prop('checked', true);
    else
        $('#' + tableid).find('input[type=checkbox]').prop('checked', false);
}
function ApplicableAllowanceSave() {
    debugger;
    if (ValidateAllowance()) {


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
                    url: '/PayRoll/LeaveEncashmentSettings/SaveApplicableAllowance',
                    dataType: 'json',
                    data: $('#ApplicableAllowanceSave').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire("Saved Successfully").then(function () {
                                CloseSlider('applicableAllowanceSlider');
                                debugger;
                                //var url = "/PayRoll/OvertimeSettings/Index";
                                //windows.location.href = url;
                                $.getJSON("/PayRoll/LeaveEncashmentSettings/GetOvertimeSettingsAllowanceDropdown", {}, function (data) {

                                    $('#AttendanceStatus_Code option').remove();
                                    $('#AttendanceStatus_Code').append('<option value>Select Attendance Status</option');

                                    for (var i = 0; i < data.length; i++) {
                                        $('#AttendanceStatus_Code').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
                                    }

                                });
                                /*   $('#applicableallowance').load(url, {}, function () { });*/
                                //    $('#kt_content').load(url, {}, function () { });
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
            else {
                return false;
            }
        })


    }
}
function onAllowanceCheckboxClick(chkid, tableid) {
    debugger;
    if ($('#' + chkid).is(':checked'))
        $('#' + tableid).find('input[type=checkbox]').prop('checked', true);
    else
        $('#' + tableid).find('input[type=checkbox]').prop('checked', false);
}
function getList() {
    debugger;
    showSpinner();
    $("#edit").css("display", "block");
    $("#updatecancel").css("display", "none");
    AttendanceStatus = $('#AttendanceStatus_Code').val();
    /* if (validateDropdowns()) {*/
    var url = "/PayRoll/LeaveEncashmentSettings/GetSettingsEmployeeList";
    $('#SettingsEmployeeList').load(url, { Allowance: $('#AttendanceStatus_Code').val() }, function () { hideSpinner(); });
    $('.reset').val('');
    $('#Optional_Filter_EmploymentStatus_Code').val(0);
    $('#filterForm').removeClass('filter-apply');
    /* }*/
}

function sliderChange() {

    debugger;
    var i = 0;
    if (!validateById('#CalcTypeCode', 'CalcType')) {
        i++;
    }
    if (i == 0) {
        preventENterFlag = 0;
        LeaveName = $('#AttendanceStatus_Code option:selected').text();
        LeaveCode = $('#AttendanceStatus_Code').val();

        //    //$('body').append('<div class="form-overlay"></div>');
        //    var url = "/PayRoll/HolidayWageSettings/GetBatchUpdateDetails";
        //    /*window.location.href = url;*/
        //$('#batchUpdate').load(url, {  }, function () { });
        //CloseSlider('batchUpdateSlider');
        if (validateDropdowns()) {
            //$('body').append('<div class="form-overlay"></div>');
            //var url = "/PayRoll/OvertimeSettings/GetBatchUpdateDetails";
            //$('#batchUpdate').load(url, { AllowanceCode: Allowance, AllowanceName: AllowanceName }, function () { });
            $.ajax({

                type: 'GET',
                url: '/PayRoll/LeaveEncashmentSettings/GetBatchUpdateDetails',
                dataType: 'html',
                data: { LeaveCode: LeaveCode, LeaveName: LeaveName },
                success: function (result) {
                    debugger;
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }

                    else {
                        $('body').append('<div class="form-overlay"></div>');
                        $('#batchUpdate').html("");
                        $('#batchUpdate').html(result);
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
}

function defaultSettingsClick() {
    debugger;
    //var url = "/PayRoll/OvertimeSettings/GetDefaultSettingsList";
    //$('#defaultsettings').load(url, {}, function () { });
    //CloseSlider('defaultSettingsSlider');

    var AttendanceStatus = $('#AttendanceStatus_Code').val();
    if (AttendanceStatus == "") {
        swal.fire("Please Select Leave Encashment Allowance");
    }
    else {


        $.ajax({

            type: 'GET',
            url: '/PayRoll/LeaveEncashmentSettings/GetDefaultSettingsList',
            dataType: 'html',
            success: function (result) {
                debugger;
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }

                else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('#defaultsettings').html("");
                    $('#defaultsettings').html(result);
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
function ExportToExcel() {
    debugger;
    var AttendanceStatusName = $('#AttendanceStatus_Code option:selected').text();
    if (validateDropdowns()) {

        var SettingsEmployeeListTableLength = $('#SettingsEmployeeList_Count').val();
        if (SettingsEmployeeListTableLength > 0) {
            url = "/PayRoll/LeaveEncashmentSettings/ExportToExcel?Allowance=" + $('#AttendanceStatus_Code').val() + '&AllowanceName=' + AttendanceStatusName + '&Totalrow=' + SettingsEmployeeListTableLength;
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
    }
}


//function onEditClick(index, origAmt) {
//    debugger
//    if (isselected == null) {
//        isselected = 1;
//        $('#hideAction_' + index).addClass('hide');
//        $('#hideRate_' + index).addClass('hide');
//        $('#hidecalctype_' + index).addClass('hide');
//        $('#raterow_' + index).removeClass('hide');
//        $('#calctyperow_' + index).removeClass('hide');
//        $('.save_Cancel_' + index).removeClass('hide');

//        $('#newRate_' + index).val(origAmt);
//        moveCursorToEnd('SettingsEmployeeList_' + index + '__CalcType_Code');
//    }
//}




function onEditClick(index, origAmt) {
    debugger
    if (isselected == null) 
        isselected = 1;
        $('#hideAction_' + index).addClass('hide');
        $('#hideRate_' + index).addClass('hide');
        $('#hidecalctype_' + index).addClass('hide');
        $('#raterow_' + index).removeClass('hide');
        $('#calctyperow_' + index).removeClass('hide');
        $('.save_Cancel_' + index).removeClass('hide');

        $('#CalcTypeCode_' + index).removeClass('hide');
        $('#newRate_' + index).val(origAmt);
    moveCursorToEnd('newRate_' + index);
}
function onCancelClick(index) {
    debugger;
    var i = 0;
    if (!validateById('#CalcTypeCode_' + index, 'calctyvalid_' + index)) {
        i++;
    }
    if (i > 0) {
        return false;
    }
    else {
        $('#hideRate_' + index).removeClass('hide');
        $('#hidecalctype_' + index).removeClass('hide');
        $('#hideAction_' + index).removeClass('hide');
        $('.save_Cancel_' + index).addClass('hide');
        $('#raterow_' + index).addClass('hide');
        $('#calctyperow_' + index).addClass('hide');
        $('#CalcTypeCode_' + index).addClass('hide');
        isselected = null;
    }
}








//function onEditClick(index, oldRate) {
//    debugger
//    if (isselected == null) {
//        isselected = 1;
//        $('#hideAction_' + index).hide();
//        $('#hideRate_' + index).hide();
//        $('#hidecalctype_' + index).hide();

//        $('#newRate_' + index).val(oldRate);
//        $('#raterow_' + index).show();
//        $('#calctyperow_' + index).show();
//        $('.save_Cancel_' + index).show();        
//        moveCursorToEnd('SettingsEmployeeList_' + index + '__CalcType_Code');
//    }
//}

//function onCancelClick(index) {
//    debugger;
//    $('#hideRate_' + index).removeClass('hide');
//    $('#hidecalctype_' + index).removeClass('hide');
//    $('#hideAction_' + index).removeClass('hide');
//    $('.save_Cancel_' + index).addClass('hide');
//    $('#raterow_' + index).addClass('hide');
//    $('#calctyperow_' + index).addClass('hide');
//    isselected = null;
//}


function validateCaculationType(index) {

}



function onUpdateClick(index, EmployeeGI, CalcType) {
    debugger;

    if (validateCaculationType(index)) {

    }


    //UpdatedRate = $('#newRate_' + index).val();
    UpdatedRate = $('#newRate_' + index).val();

    /*    calculationType = $("#SettingsEmployeeList_" + index + "__CalcTypeCode_").val();*/
    calcType = $('#CalcTypeCode_' + index).val();
    AttendanceStatus = $('#AttendanceStatus_Code').val();

    if (UpdatedRate == "" || UpdatedRate == null) {
        swal.fire("Amount/Rate cannot be blank");
        return false;
    }
    var i = 0;
    if (!validateById('#CalcTypeCode_' + index, 'calctyvalid_'+index)) {
        i++;
    }
    if (i > 0) {
        return false;
    }
    else {
        $.ajax({
            type: 'POST',
            url: '/PayRoll/LeaveEncashmentSettings/SaveEmployeeWiseSettings',
            dataType: 'json',
            data: { UpdatedRate: UpdatedRate, UpdatedCalcType: calcType, EmployeeGI: EmployeeGI, AllowanceCode: AttendanceStatus },
            success: function (result) {
                debugger;
                if (result.success == true) {
                    swal.fire(result.message).then(function () {
                        /*   var url = "/PayRoll/OvertimeSettings/GetSettingsEmployeeList";*/
                        fillData();
                        /*    $('#SettingsEmployeeList').load(url, { Allowance: $('#Allowance_Code').val() }, function () { });*/

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
    isselected = null;

}










function BatchUpdateClick() {
    debugger;
    if (validateBatchUpdateDefaultDropdowns()) {
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
                    url: '/PayRoll/LeaveEncashmentSettings/BatchUpdateEmployeeWiseSettings',
                    dataType: 'json',
                    data: $('#BatchUpdateAllowance').serialize(),
                    success: function (result) {
                        debugger;
                        if (result.success == true) {
                            swal.fire("Updated Successfully").then(function () {
                                /*  var url = "/PayRoll/OvertimeSettings/GetSettingsEmployeeList";*/
                                //fillData();
                                //$('#SettingsEmployeeList').load(url, { Allowance: $('#Allowance_Code').val() }, function () { });
                                ApplyFilter('apply');
                                CloseSlider('batchUpdateDiv');
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
            else {
                return false;
            }
        })

    }
}
function DefaultSettingsUpdate() {
    debugger; if (validateBatchUpdateDefaultDropdowns()) {
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
                    url: '/PayRoll/LeaveEncashmentSettings/SaveDefaultSettings',
                    dataType: 'json',
                    data: $('#DefaultSettings').serialize(),
                    success: function (result) {
                        debugger;
                        if (result.success == true) {
                            swal.fire("Saved Successfully").then(function () {
                                /*   var url = "/PayRoll/OvertimeSettings/GetSettingsEmployeeList";*/
                                fillData();
                                /*   $('#SettingsEmployeeList').load(url, { Allowance: $('#Allowance_Code').val() }, function () { });*/
                                CloseSlider('defaultSettingsSlider');
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
            else {
                return false;
            }
        })

    }
}

function ApplyFilter(mode) {
    debugger

    if (mode == 'apply') {
        //showSpinner();
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
        //showSpinner();
        filterApplicable = 1;
        $('.reset').val('');
        $('#Optional_Filter_EmploymentStatus_Code').val(0);
        $('#filterForm').removeClass('filter-apply');
        fillData();

    }
}


function process(input) {
    debugger;
    let value = input.value;

    let numbers = value.replace(/[^0-9]/g, "");
    if (numbers == "")
        swal.fire("Only Numbers are allowed")
    input.value = numbers;

}



function ValidateAllowance() {
    debugger;
    var checked_checkboxes = $("#ApplicableAllowance input[type=checkbox]:checked");
    if (checked_checkboxes.length == 0) {
        swal.fire("Select atleast one Allowance");

        return false;
    }
    return true;
}

