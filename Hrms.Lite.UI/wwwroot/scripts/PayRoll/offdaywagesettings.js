var isselected;



function fillData() {

    debugger;

    var filter1 = $('#BasicFilter').serialize();

    var temp = filter1;




    var Allowance = $('#Allowance_Code').val();

   /* if (Allowance != '') {*/
        var dataTable = $("#SettingsEmployeeListtable").DataTable({

            "pagingType": 'full_numbers',
            "destroy": 'true',
            "ajax": {
                "url": "/PayRoll/OffdayWageSettings/GetList",
                "type": "POST",


                "datatype": "json",


                'data':

                    function (d) {
                        temp = '';




                        var start = d.start;

                        var searchValue = $('#dataTableSearch').val();






                        temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&Allowance=' + Allowance  + '&dummy=dummy';
                        d.filter = temp;

                        return JSON.stringify(d);
                    },




                "dataSrc": function (d) {




                    $('#SettingsEmployeeList_Count').val(d.recordsTotal);



                    for (var i = 0, ien = d.data.length; i < ien; i++)

                    {

                        //d.data[i].deduction = '<span id="editbt_' + i + '"><a onclick = "onEditClick(' + i + ')"  data-bs-toggle="tooltip" title = "Edit" type = "button"class="btn btn-icon btn-edit icon-only" ><i class="icon-xl fas fa-pencil-alt"></i></a></span><span id = "updateCancel_' + i + '" class="hide"><a type = "button" data-bs-toggle="tooltip" title = "Update" class="btn btn-icon btn-update icon-only " onclick = "onUpdateClick(' + i + ',' + "'" + d.data[i].employeeGI + "'" + ')"> <i class="icon-xl fas fa-sync-alt"></i> </a><a type = "button" data-bs-toggle="tooltip" title = "Cancel" class="btn btn-icon btn-cancel icon-only " onclick = "onCancelClick(' + i + ')"> <i class="icon-xl fas fa-times"></i></a></span>'


                        debugger;


                       //d.data[i].employee.employeeCode = '<span onmouseenter="tooltipHover()" id = "hideAction_' + i + '" ><a data-bs-toggle="tooltip" title="Edit" type="button"class="btn btn-icon btn-edit icon-only" onclick="onEditClick(' + i + ',' + d.data[i].rate + ')"><i class="icon-xl fas fa-pencil-alt"></i></a></span ><div class="small-btn-group save_Cancel_' + i + ' hide"><a type="button" data-bs-toggle="tooltip" title="Update" class="btn btn-icon btn-update btn-cancel icon-only"><i class="icon-xl fas fa-sync-alt update-icon" onclick="onUpdateClick(' + i + ', ' + "'" + d.data[i].employeeGI + "'" + ', ' + d.data[i].calcType.code + ')"></i></a><a type="button" data-bs-toggle="tooltip" title="Cancel" class="btn btn-icon btn-cancel icon-only"><i class="icon-xl fas fa-times cancel-btn" onclick="onCancelClick(' + i + ')"></i></a></div>'
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
                { "data": "employee.fullName","width": "15%" },
                { "data": "designationName","width": "13%" },
                { "data": "departmentName", "width": "13%" },
                { "data": "locationName", "width": "13%" },
                { "data": "calcType.name", "width": "13%" },
                { "data": "rate", "width": "10%"},
                { "data": "employee.employeeCode", "width": "8%" },
               
              



            ],






            
            "dom": 'rt<"bottom"><"my-wrapper"<"my-inner2"l><"my-inner1"i><"my-inner3"p>>',
            "serverSide": "true",
            "scrollY": '52vh',
            "sScrollX": "100%",
            "sScrollXInner": "100%",
            "AutoWidth": false,

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
    setTimeout(tooltipHover, 500);




    //}
    //else {
    //    return false;
    //}

}




















function validateDropdowns(index) {
   
    debugger;
    i = 0;
    /*if (!validateById('#SettingsEmployeeList_CalcType_Code', 'calc'))*/
    if (!validateById("#SettingsEmployeeList_" + index+"__CalcType_Code", 'calc'))
    {
        i++;
    }
    if (!validateById("#SettingsEmployeeList_" + index + "__Rate", 'rate12')) {
        i++;
    }
   
    if (i == 0)
        return true;
    else
        return false;
    //if (validateById('#Allowance_Code', 'Allowance')) {
    //    return true;
    //}
    //else {
    //    return false;
    //}
}
function validateBatchUpdateDefaultDropdowns()
{
    debugger;
    i = 0;
    if (!validateById('#CalcTypeCode', 'CalcType'))
    {
        i++;
    }
    if (!validateById('#Rate', 'RateValidation'))
    {
        i++
    }

    if (i == 0) 
        return true;
        else
        return false;
    
    
}

function getList() {
    debugger;
    showSpinner();
    $("#edit").css("display", "block");
    $("#updatecancel").css("display", "none");
    Allowance = $('#Allowance_Code').val();
    $.ajax({

        type: 'GET',
        url: '/PayRoll/OffdayWageSettings/GetSettingsEmployeeList',
        dataType: 'html',
        data: { Allowance: Allowance },
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


function sliderChange() {
    debugger;

    AllowanceName = $('#Allowance_Code option:selected').text();
    Allowance = $('#Allowance_Code').val();
    var i = 0;
    if (!validateById('#Allowance_Code', 'Allowance'))
        i++;

    if (i == 0)
/* if (validateDropdowns())*/ {
        $.ajax({

            type: 'GET',
            url: '/PayRoll/OffdayWageSettings/GetBatchUpdateDetails',
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
    else {
        return false;
    }

}
function getSettingsEmployeeList(AllowanceCode) {
    debugger;

    $.ajax({
        type: 'POST',
        url: '/PayRoll/OffdayWageSettings/GetSettingsEmployeeList',
        dataType: 'json',
        data: { Allowance: Allowance },
        success: function (result) {
            if (result.success == true) {
                swal.fire("Saved Successfully").then(function () {
                    
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
                if ($($.parseHTML(xhr.responseText)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    document.body.id = "kt_body";
                    $('body').html(xhr.responseText);
                }
            }
        }
    });

}

function ApplicableAllowanceSave() {
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
                url: '/PayRoll/OffdayWageSettings/SaveApplicableAllowance',
                dataType: 'json',
                data: $('#ApplicableAllowanceSave').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        debugger;
                        swal.fire("Saved Successfully").then(function () {
                            CloseSlider('applicableAllowanceSlider');
                            //$('#applicableallowance').load(url, {}, function () { });
                            $.getJSON("/PayRoll/OffdayWageSettings/GetOffdayWageSettingsAllowanceDropdown", {}, function (data) {

                                $('#Allowance_Code option').remove();
                                $('#Allowance_Code').append('<option value>Select Offday Wage Settings</option');

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
function applicableAllowanceClick() {
    debugger;
    var i = 0;
    if (!validateById('#Allowance_Code', 'Allowance')) {
        i++;
    }
    if (i == 0) {
        $.ajax({

            type: 'GET',
            url: '/PayRoll/OffdayWageSettings/GetApplicableAllowanceList',
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



//function onEditClick(index,origAmt) {
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
//    $('#hideRate_' + index).removeClass('hide');
//    $('#hidecalctype_' + index).removeClass('hide');
//    $('#hideAction_' + index).removeClass('hide');
//    $('.save_Cancel_' + index).addClass('hide');
//    $('#raterow_' + index).addClass('hide');
//    $('#calctyperow_' + index).addClass('hide');
//    isselected = null;
//}

//function onUpdateClick(index, EmployeeGI, CalcType) {
//    debugger;
//    isselected = null;
//    UpdatedRate = $('#newRate_' + index).val();
//    //UpdatedRate = $('#Rate').val();
//    calculationType = $("#SettingsEmployeeList_" + index + "__CalcType_Code").val();
//    console.log(calculationType);
//    Allowance = $('#Allowance_Code').val();
//    if (UpdatedRate == "" || UpdatedRate == null || UpdatedRate == 0) {
//        swal.fire("Amount should not be null");
//        return false;
//    }
//    if (validateDropdowns(index)) {
//        $.ajax({
//            type: 'POST',
//            url: '/PayRoll/OffdayWageSettings/SaveEmployeeWiseSettings',
//            dataType: 'json',
//            data: { UpdatedRate: UpdatedRate, UpdatedCalcType: calculationType, EmployeeGI: EmployeeGI, AllowanceCode: Allowance },
//            success: function (result) {
//                debugger;
//                if (result.success == true) {
//                    swal.fire(result.message).then(function () {
//                        var url = "/PayRoll/OffdayWageSettings/GetSettingsEmployeeList";
//                        $('#SettingsEmployeeList').load(url, { Allowance: $('#Allowance_Code').val() }, function () { });

//                    });

//                }
//                else {
//                    swal.fire(result.message).then(function () {

//                    });
//                }

//            },
//            error: function (xhr, textStatus, errorThrown) {
//                document.body.id = "kt_body";
//                $('body').html(xhr.responseText);
//            }
//        });
//    }
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
            url: '/PayRoll/OffdayWageSettings/SaveEmployeeWiseSettings',
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
                    url: '/PayRoll/OffdayWageSettings/BatchUpdateEmployeeWiseSettings',
                    dataType: 'json',
                    data: $('#BatchUpdateAllowance').serialize(),
                    success: function (result) {
                        debugger;
                        if (result.success == true) {
                            swal.fire("Updated Successfully").then(function () {
                                var url = "/PayRoll/OffdayWageSettings/GetSettingsEmployeeList";
                                //$('#SettingsEmployeeList').load(url, { Allowance: $('#Allowance_Code').val() }, function () { });
                               /* ApplyFilter('apply');*/
                                CloseSlider('batchUpdateDiv');
                                fillData();
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
function defaultSettingsClick() {
    var i = 0;
    if (!validateById('#Allowance_Code', 'Allowance'))
    {
        i++;
    }
    if (i == 0) {

        $.ajax({

            type: 'GET',
            url: '/PayRoll/OffdayWageSettings/GetDefaultSettingsList',
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
                    url: '/PayRoll/OffdayWageSettings/SaveDefaultSettings',
                    dataType: 'json',
                    data: $('#DefaultSettings').serialize(),
                    success: function (result) {
                        debugger;
                        if (result.success == true) {
                            swal.fire("Saved Successfully").then(function () {
                                var url = "/PayRoll/OffdayWageSettings/GetSettingsEmployeeList";
                                //$('#SettingsEmployeeList').load(url, { Allowance: $('#Allowance_Code').val() }, function () { });
                                fillData();
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
function ExportToExcel() {
    debugger;
    if (validateDropdowns()) {

        var SettingsEmployeeListTableLength = $('#SettingsEmployeeList_Count').val();
        if (SettingsEmployeeListTableLength > 0) {
            url = "/PayRoll/OffdayWageSettings/ExportToExcel?Allowance=" + $('#Allowance_Code').val() + "&AllowanceName=" + $('#Allowance_Code option:selected').text() + "&totRecords=" + SettingsEmployeeListTableLength;
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
    }
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

function process(input) {
    debugger;
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