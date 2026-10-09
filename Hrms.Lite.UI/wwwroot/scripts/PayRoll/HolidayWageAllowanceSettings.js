var isselected;

function fillData() {

    debugger;

    var filter1 = $('#BasicFilter').serialize();

    var temp = filter1;

 
    var Allowance = $('#Allowance_Code').val();

    /*if (Allowance != '') {*/
    var dataTable = $("#SettingsEmployeeListtable").DataTable({

            "pagingType": 'full_numbers',
            "destroy": 'true',
            "ajax": {
                "url": "/PayRoll/HolidayWageSettings/GetWagesConfirmationList",
                "type": "POST",


                "datatype": "json",


                'data':

                    function (d) {
                        temp = '';


                        var start = d.start;

                        var searchValue = $('#dataTableSearch').val();


                        temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&Allowance=' + Allowance +'&dummy=dummy';
                        d.filter = temp;

                        return JSON.stringify(d);
                    },




                "dataSrc": function (d) {
                    $('#SettingsEmployeeList_Count').val(d.recordsTotal);

                    for (var i = 0, ien = d.data.length; i < ien; i++) {

                        d.data[i].employee.employeeCode = '<span onmouseenter="tooltipHover()" id = "hideAction_' + i + '" ><a data-bs-toggle="tooltip" title="Edit" type="button"class="btn btn-icon btn-edit icon-only" onclick="onEditClick(' + i + ',' + d.data[i].rate + ')"><i class="icon-xl fas fa-pencil-alt"></i></a></span ><div class="small-btn-group save_Cancel_' + i + ' hide"><a type="button" data-bs-toggle="tooltip" title="Update" class="btn btn-icon btn-update btn-cancel icon-only"><i class="icon-xl fas fa-sync-alt update-icon" onclick="onUpdateClick(' + i + ', ' + "'" + d.data[i].employee.employeeGI + "'" + ', ' + d.data[i].calcType.code + ')"></i></a><a type="button" data-bs-toggle="tooltip" title="Cancel" class="btn btn-icon btn-cancel icon-only"><i class="icon-xl fas fa-times cancel-btn" onclick="onCancelClick(' + i + ')"></i></a></div>'
                        d.data[i].rate = ' <td data-label="Rate" id="row_' + i + '"><span id="hideRate_' + i + '">' + d.data[i].rate + '</span><span id="rate12"></span><span id="raterow_' + i + '" class="hide"><input type"text" id="newRate_' + i + '" class="form-control form-control-sm" placeholder="Amt/RT in %" maxlength="8" onkeypress="return onlyNumberKey(event)" onpaste="return false"><span id="rate"></span></span></td>'

                        //d.data[i].calcType.name = '<td data-label="CalculationType" id="row_' + i + '"><span id="hidecalctype_' + i + '">' + d.data[i].calcType.name + '</span><span id="calctyperow_' + i + '" class="hide"><span id="calc"> </span></span></td>'

                        d.data[i].calcType.name = '<td data-label="CalculationType" id="row_' + i + '"><span id="hidecalctype_' + i + '">' + d.data[i].calcType.name + '</span><span id="calctyperow_' + i + '" class="hide"><span id="calc"> </span></span></td><select class="form-control form-control-sm hide"  id = "CalcTypeCode_' + i + '"  name = "CalcTypeCode" onchange = "validate(this,' + 'CalcType' + ')"><option value="">' + 'Select Calculation Type' + '</option> <option value=' + '1' + '>Fixed Amount</option>  <option value=' + '2' + '>Percentage Wise</option> </select >'


                    }

                    return d.data;
                }

            },

            "columns": [

                { "data": "employee.employeeID","width":"10%" },
                { "data": "employee.fullName", "width": "18%"  },
                { "data": "designationName", "width": "13%"  },
                { "data": "departmentName", "width": "13%" },
                { "data": "locationName", "width": "10%"  },
                { "data": "calcType.name", "width": "14%"  },
                { "data": "rate", "width": "12%" },
                { "data": "employee.employeeCode", "width": "10%"  },
           
                /*{ "data": "ptMonth" },*/


             



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
    setTimeout(tooltipHover, 500);
    //}
    //else {
    //    return false;
    //}
}


function validateDropdowns() {
    if (validateById('#Allowance_Code', 'Allowance_span')) {
        return true;
    }
    else {
        return false;
    }
}
function validateDropdownCalc() {
    if (!validateById('#CalcTypeCode', 'CalcType')) {
        return true;
    }
    else {
        return false;
    }
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

function getList() { 
    debugger;
    
    $("#edit").css("display", "block");
    $("#updatecancel").css("display", "none");
    Allowance= $('#Allowance_Code').val();
    if (validateDropdowns()) {
        showSpinner();
        $.ajax({

            type: 'GET',
            url: '/PayRoll/HolidayWageSettings/GetSettingsEmployeeList',
            dataType: 'html',
            data: { Allowance: Allowance  },
            success: function (result) {
                debugger;
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }

                else {
                    $('#SettingsEmployeeList').html("");
                    $('#SettingsEmployeeList').html(result);
                    $('.reset').val('');
                    $('#Optional_Filter_EmploymentStatus_Code').val(0);
                    $('#filterForm').removeClass('filter-apply');
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


function sliderChange() {
    debugger;
   
    AllowanceName = $('#Allowance_Code option:selected').text();
    Allowance = $('#Allowance_Code').val();



    var Count = $('#SettingsEmployeeList_Count').val();



    //    //$('body').append('<div class="form-overlay"></div>');
    //    var url = "/PayRoll/HolidayWageSettings/GetBatchUpdateDetails";
    //    /*window.location.href = url;*/
    //$('#batchUpdate').load(url, {  }, function () { });
    //CloseSlider('batchUpdateSlider');


    if (Count != 0) {


    if (validateDropdowns()) {
        $.ajax({

            type: 'GET',
            url: '/PayRoll/HolidayWageSettings/GetBatchUpdateDetails',
            dataType: 'html',
            data: { AllowanceCode: Allowance, AllowanceName: AllowanceName },
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

function applicableAllowanceClick() {
    debugger
    var i = 0
    var allowance = $('#Allowance_Code').val();
    if (allowance == "") {
        swal.fire("Please Select an allownce");
    }
    else {
        $.ajax({

            type: 'GET',
            url: '/PayRoll/HolidayWageSettings/GetApplicableAllowanceList',
            dataType: 'html',
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
    swal.fire({
        text: "Do you want to Save ?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: 'POST',
                url: '/PayRoll/HolidayWageSettings/SaveApplicableAllowance',
                dataType: 'json',
                data: $('#ApplicableAllowanceSave').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire("Saved Successfully").then(function () {
                            CloseSlider('applicableAllowanceSlider');
                            /* var url = "/PayRoll/HolidayWageSettings/GetApplicableAllowanceList";*/
                            //$('#applicableallowance').load(url, {}, function () { });
                            $.getJSON("/PayRoll/HolidayWageSettings/GetHolidayWageSettingsAllowanceDropdown", {}, function (data) {

                                $('#Allowance_Code option').remove();
                                $('#Allowance_Code').append('<option value>Select Holiday Wage Allowance</option');

                                for (var i = 0; i < data.length; i++) {
                                    $('#Allowance_Code').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
                                }

                            });
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
function onAllowanceCheckboxClick(chkid, tableid) {
    debugger;
    if ($('#' + chkid).is(':checked'))
        $('#' + tableid).find('input[type=checkbox]').prop('checked', true);
    else
        $('#' + tableid).find('input[type=checkbox]').prop('checked', false);
}
function defaultSettingsClick() {
    debugger;
    var i = 0
    var allowance = $('#Allowance_Code').val();
    if (allowance == "") {
        swal.fire("Please Select an allownce");
    }
    else {
    $.ajax({

        type: 'GET',
        url: '/PayRoll/HolidayWageSettings/GetDefaultSettingsList',
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
    //var url = "/PayRoll/HolidayWageSettings/GetDefaultSettingsList";
    ///*window.location.href = url;*/
    //$('#defaultsettings').load(url, {}, function () { });
    //CloseSlider('defaultSettingsSlider');
}

}
function ExportToExcel() {
    debugger;
    var allowanceName = $('#Allowance_Code option:selected').text();
    if (validateDropdowns()) {

        var SettingsEmployeeListTableLength = $('#SettingsEmployeeList_Count').val();

        if (SettingsEmployeeListTableLength > 0) {
            url = "/PayRoll/HolidayWageSettings/ExportToExcel?Allowance=" + $('#Allowance_Code').val() + "&allowanceName=" + allowanceName + "&totRecords=" + SettingsEmployeeListTableLength;
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
    if (isselected == null) {
        isselected = 1;
        $('#hideAction_' + index).addClass('hide');
        $('#hideRate_' + index).addClass('hide');
        $('#hidecalctype_' + index).addClass('hide');
        $('#raterow_' + index).removeClass('hide');
        $('#calctyperow_' + index).removeClass('hide');
        $('.save_Cancel_' + index).removeClass('hide');
        $('#CalcTypeCode_' + index).removeClass('hide');
        $('#newRate_' + index).val(origAmt);
        moveCursorToEnd('SettingsEmployeeList_' + index + '__CalcType_Code');
    }
}





//function onEditClick(index,oldRate) {
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


function onCancelClick(index) {
    debugger;
    $('#hideRate_' + index).removeClass('hide');
    $('#hidecalctype_' + index).removeClass('hide');
    $('#hideAction_' + index).removeClass('hide');
    $('.save_Cancel_' + index).addClass('hide');
    $('#raterow_' + index).addClass('hide');
    $('#calctyperow_' + index).addClass('hide');
    $('#CalcTypeCode_' + index).addClass('hide');
    isselected = null;
}


//function onCancelClick(index) {
//    debugger;
//    $('#hideRate_' + index).show();
//    $('#hidecalctype_' + index).show();
//    $('#hideAction_' + index).show();
//    $('.save_Cancel_' + index).hide();

//    $('#raterow_' + index).hide();
    

//    $('#calctyperow_' + index).hide();

//    $('#newRate_' + index).val('');//clear value on cancel
//    $("#SettingsEmployeeList_" + index + "__CalcType_Code").val('');//clear value on cancel
//    isselected = null;
//}

function onUpdateClick(index, EmployeeGI, CalcType) {
    debugger;
    isselected = null;
    UpdatedRate = $('#newRate_' + index).val();
    calcType = $('#CalcTypeCode_' + index).val();
    Allowance = $('#Allowance_Code').val();
    if (UpdatedRate == "" || UpdatedRate == null) {
        swal.fire("Amount should not be null");
        return false;
    }
    if (validateDropdowns(index)) {

        $.ajax({
            type: 'POST',
            url: '/PayRoll/HolidayWageSettings/SaveEmployeeWiseSettings',
            dataType: 'json',
            data: { UpdatedRate: UpdatedRate, UpdatedCalcType: calcType, EmployeeGI: EmployeeGI, AllowanceCode: Allowance },
            success: function (result) {
                debugger;
                if (result.success == true) {
                    swal.fire(result.message).then(function () {
                        fillData();
                        //var url = "/PayRoll/HolidayWageSettings/GetSettingsEmployeeList";
                        //$('#SettingsEmployeeList').load(url, { Allowance: $('#Allowance_Code').val() }, function () { });

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
}

//function onUpdateClick(index, EmployeeGI,CalcType) {
//    debugger;
//    isselected = 1;

//    UpdatedRate = $('#newRate_' + index).val();
//    calculationType = $("#SettingsEmployeeList_" + index +"__CalcType_Code").val();
//    if (UpdatedRate == "" || UpdatedRate== null)
//    {
//        swal.fire("Amount/Rate cannot be null");
//        return false;
//    }
//   // UpdatedCalcType = $('#calctyperow_Code').val();
//    Allowance = $('#Allowance_Code').val();
//   /* if (validateDropdowns()) {*/
//    $.ajax({
//        type: 'POST',
//        url: '/PayRoll/HolidayWageSettings/SaveEmployeeWiseSettings',
//        dataType: 'json',
//        data: { UpdatedRate: UpdatedRate, UpdatedCalcType: calculationType, EmployeeGI: EmployeeGI, AllowanceCode: Allowance },
//        success: function (result) {
//            debugger;
//            if (result.success == true) {
//                swal.fire(result.message).then(function () {
//                    var url = "/PayRoll/HolidayWageSettings/GetSettingsEmployeeList";
//                    $('#SettingsEmployeeList').load(url, { Allowance: $('#Allowance_Code').val() }, function () { });

//                });

//            }
//            else {
//                swal.fire(result.message).then(function () {

//                });
//            }

//        },
//        error: function (xhr, textStatus, errorThrown) {
//            document.body.id = "kt_body";
//            $('body').html(xhr.responseText);
//        }
//    });
        
//   /* }*/
//    isselected = null;

//}
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
                    url: '/PayRoll/HolidayWageSettings/BatchUpdateEmployeeWiseSettings',
                    dataType: 'json',
                    data: $('#BatchUpdateAllowance').serialize(),
                    success: function (result) {
                        debugger;
                        if (result.success == true) {
                            swal.fire("Updated Successfully").then(function () {
                                var url = "/PayRoll/HolidayWageSettings/GetSettingsEmployeeList";
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
    debugger;
    if (validateBatchUpdateDefaultDropdowns()) {
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
                    url: '/PayRoll/HolidayWageSettings/SaveDefaultSettings',
                    dataType: 'json',
                    data: $('#DefaultSettings').serialize(),
                    success: function (result) {
                        debugger;
                        if (result.success == true) {
                            swal.fire("Saved Successfully").then(function () {
                                var url = "/PayRoll/HolidayWageSettings/GetSettingsEmployeeList";
                                $('#SettingsEmployeeList').load(url, { Allowance: $('#Allowance_Code').val() }, function () { });
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
//function onfilterClick() {
//    debugger
 
//    var url = "/PayRoll/HolidayWageSettings/ApplyFilterLoad";
//    $('body').append('<div class="form-overlay"></div>');
//    $('#FilterPartial').load(url, function () { });
//}
//function ApplyFilter(mode) {
//    debugger
//    AllowanceName = $('#Allowance_Code option:selected').text();
//    Allowance = $('#Allowance_Code').val();
//    //var tabIndex = $('#Tabindex').val();
//    //var search = $('#SearchId').val();
//    if (mode == 'apply') {
//        $.ajax({

//            type: 'POST',
//            url: '/PayRoll/HolidayWageSettings/ApplyFilter',
//            dataType: 'html',
//            data: { filter : $('#HolidayWageFilter').serialize(), Allowance :Allowance },
//            success: function (result) {
//                debugger
//                CloseSlider('HolidayWageFilter');
//                //var url = "/PayRoll/HolidayWageSettings/GetSettingsEmployeeList";
//                //$('#SettingsEmployeeList').load(url, { Allowance: $('#Allowance_Code').val() }, function () { });
//                $('#SettingsEmployeeList').html("");
//                $('#SettingsEmployeeList').html(result);
//            },


//            error: function (xhr, textStatus, errorThrown) {
//                swal.fire(xhr.responseText);
//            }
//        });
//    }
//    else {
//        $('.reset').val('');
//        $.ajax({

//            type: 'POST',
//            url: '/PayRoll/HolidayWageSettings/ApplyFilter',
//            dataType: 'html',
//            data: $('#HolidayWageFilter').serialize(),
//            success: function (result) {
//                debugger

//                $('#SettingsEmployeeList').html("");
//                $('#SettingsEmployeeList').html(result);
               
//            },


//            error: function (xhr, textStatus, errorThrown) {
//                swal.fire(xhr.responseText);
//            }
//        });
//    }
//}

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
            //$.ajax({

            //    type: 'POST',
            //    url: '/PayRoll/HolidayWageSettings/ApplyFilter',
            //    dataType: 'html',
            //    data: $('#BasicFilter').serialize(),
            //    success: function (result) {
            //        debugger
            //        CloseSlider('basicFilter');
            //        if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
            //            window.location.reload();
            //        }

            //        else {
            //            $('#SettingsEmployeeList').html("");
            //            $('#SettingsEmployeeList').html(result);

            //            $('#SettingsEmployeeList').removeClass('hide');
            //        }
            //        hideSpinner();
            //        //$('#rewardsListPartial').addClass('hide');               
            //    },


            //    error: function (xhr, textStatus, errorThrown) {
            //        hideSpinner();
            //        document.body.id = "kt_body";
            //        $('body').html(xhr.responseText);
            //    }
            //});
            $('#filterForm').addClass('filter-apply');
        }
        
    }
    else {
        
        filterApplicable = 1;
        $('.reset').val('');
        $('#Optional_Filter_EmploymentStatus_Code').val(0);
        $('#filterForm').removeClass('filter-apply');
        fillData();
        //$.ajax({

        //    type: 'POST',
        //    url: '/PayRoll/HolidayWageSettings/ApplyFilter',
        //    dataType: 'html',
        //    data: $('#BasicFilter').serialize(),
        //    success: function (result) {
        //        debugger

        //        //CloseSlider('assetAllocationFilter');

        //        if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
        //            window.location.reload();
        //        }

        //        else {
        //            $('#SettingsEmployeeList').html("");
        //            $('#SettingsEmployeeList').html(result);

        //            $('#SettingsEmployeeList').removeClass('hide');
        //            //$('#rewardsListPartial').addClass('hide');
        //        }
        //        hideSpinner();
        //    },


        //    error: function (xhr, textStatus, errorThrown) {
        //        document.body.id = "kt_body";
        //        $('body').html(xhr.responseText);
        //        hideSpinner();
        //    }
        //});
    }
}

function process(input) {
    /*debugger;*/
    let value = input.value;
    
    let numbers = value.replace(/[^0-9]/g, "");
    if (numbers == "")
        swal.fire("Only Numbers are allowed")
        input.value = numbers;
    if ((validateById('#Rate', 'RateValidation'))) {
        return true;
    }

    else {
        return false;

    }
}