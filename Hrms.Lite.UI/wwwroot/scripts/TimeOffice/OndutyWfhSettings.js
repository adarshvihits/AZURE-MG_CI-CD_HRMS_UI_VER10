function enableDisableTextBoxes() {
    debugger
    if (document.getElementById("OdPreDatedApplAlwed").checked == false)
        $("#OdNoOfDaysPrior").prop('readonly', true);
    if (document.getElementById("OdAdvanceApplRequired").checked == false)
        $("#OdNoOfDaysAdvance").prop('readonly', true);
    if (document.getElementById("WfhPreDatedApplAlwed").checked == false)
        $("#WfhNoOfDaysPrior").prop('readonly', true);
    if (document.getElementById("WfhAdvanceApplRequired").checked == false)
        $("#WfhNoOfDaysAdvance").prop('readonly', true);
}
function onOdAdvanceChange() {

    if ($('#OdAdvanceApplRequired').is(':checked'))
        $("#OdNoOfDaysAdvance").prop('readonly', false);
    else {
        $("#OdNoOfDaysAdvance").prop('readonly', true);
        $("#OdNoOfDaysAdvance").val('');
    }
}
function onOdPreDatesApplChange() {

    if ($('#OdPreDatedApplAlwed').is(':checked'))
        $("#OdNoOfDaysPrior").prop('readonly', false);
    else {
        $("#OdNoOfDaysPrior").prop('readonly', true);
        $("#OdNoOfDaysPrior").val('');
    }
}
function onWfhAdvanceChange() {

    if ($('#WfhAdvanceApplRequired').is(':checked'))
        $("#WfhNoOfDaysAdvance").prop('readonly', false);
    else {
        $("#WfhNoOfDaysAdvance").prop('readonly', true);
        $("#WfhNoOfDaysAdvance").val('');
    }
}
function onWfhPreDatesApplChange() {

    if ($('#WfhPreDatedApplAlwed').is(':checked'))
        $("#WfhNoOfDaysPrior").prop('readonly', false);
    else {
        $("#WfhNoOfDaysPrior").prop('readonly', true);
        $("#WfhNoOfDaysPrior").val('');
    }
}
function GetSettingsDetails() {
    debugger;

    var LeaveGroup = $('#LeaveGroup_Code').val();

    $.ajax({
        type: 'GET',
        url: '/TimeOffice/OndutyWfhSettings/GetOtherSettingsDetails',
        dataType: 'html',
        data: { LeaveGroup: LeaveGroup },
        success: function (result) {
            $('#EmployeesSettingList').hide();
            $('#filterSlider').hide();
            $('#BtnSaveSetting').show();
            $('#OtherSettingsDetailsID').html('');
            $('#OtherSettingsDetailsID').html(result);
        },
        error: function (xhr, textStatus, errorThrown) {
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);

        }
    });

}
function GetOtherDetails() {
    GetSettingsDetails();
}

function validateDropdowns() {
    debugger

    var i = 0;
    if (!validateById('#LeaveGroup_Code', 'LeaveGp')) {
        i++;
    }

    if (i == 0)
        return true;
    else
        return false;


}



function OnLeaveRuleSaveClick() {
    debugger;
    var LeaveGroup = $('#LeaveGroup_Code').val();
    var Leave = $('#Leave_Code').val();
    if (validateDropdowns()) {

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
                    url: '/Master/LeaveRule/Save',
                    dataType: 'json',
                    data: $('#Leave-Rule-Save').serialize() + "&LeaveGroup=" + LeaveGroup + "&Leave=" + Leave,
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                GetDetails();

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
    var i = 0;
    if (!validateById('#LeaveGroup_Code', 'LeaveGp')) {
        i++;
    }
    if (!validateById('#Leave_Code', 'Leave')) {

        i++;
    }

    if (i > 0) {
        return false;
    }
    else {
        var LeaveGroup = $('#LeaveGroup_Code').val();
        var LGName = $('#LeaveGroup_Code option:selected').text();
        var Leave = $('#Leave_Code').val();
        var LeaveName = $('#Leave_Code option:selected').text();

        var TableLength = document.getElementById("LeaveRuleTable").rows.length - 1;
        if (TableLength > 0) {
            url = "/Master/LeaveRule/ExportToExcel?LeaveGroup=" + LeaveGroup + "&LGName=" + LGName + "&Leave=" + Leave + "&LeaveName=" + LeaveName;
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
    }
}
var prevTab = 'Employeewise';
var currentTab = 'MonthlyLimit';

function OnTabClick(Tab, TabIndex) {
    debugger;


    currentTab = TabIndex;

    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");

    if (TabIndex == "MonthlyLimit") {

        $("#LeaveGroup_Code").addClass("hide");
        $('#EmployeesSettingList').show();
        $('#OtherSettingsDetailsID').hide();
        $('#filterForm').show();
        $('#searchDiv').show();
        $("#BtnSaveSetting").hide();
        $('#ImpExel').show();
    }
    else {
        $("#LeaveGroup_Code").removeClass("hide");
        $('#OtherSettingsDetailsID').show();
        $('#filterForm').hide();
        $('#searchDiv').hide();
        $("#BtnSaveSetting").show();
        $('#ImpExel').hide();
        //var preenrolstatus = 1;
    }

    if (TabIndex == "MonthlyLimit") {
        fillData();

        //filter = $("#BasicFilter").serialize();
        //$.ajax({

        //    type: 'GET',
        //    url: '/TimeOffice/OndutyWfhSettings/GetList?TabIndex=' + currentTab,
        //    dataType: 'html',
        //    data: filter,

        //    success: function (result) {
        //        debugger
        //        if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
        //            window.location.reload();
        //        }
        //        else {
        //            $('#EmployeesSettingList').html("");
        //            $('#EmployeesSettingList').html(result);
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


    }
    else {
        GetSettingsDetails();

    }
    prevTab = Tab;
}



function fillData() {

    debugger;

    $("#LeaveGroup_Code").addClass("hide");
    $("#BtnSaveSetting").hide();
    debugger;
    var filter1 = $('#BasicFilter').serialize();

    var temp = filter1;


    var dataTable = $("#OdWfhList_Table").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": '/TimeOffice/OndutyWfhSettings/GetList?TabIndex=' + currentTab,
            "type": "POST",


            'data':

                function (d) {
                    temp = '';

                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();
                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&Tabindex=' + currentTab + '&dummy=dummy';
                    d.filter = temp;

                    return JSON.stringify(d);
                },


            "dataSrc": function (d) {
                debugger;

                $('#OndutyWfhSettingsList_Count').val(d.recordsTotal);
                for (var i = 0, ien = d.data.length; i < ien; i++) {
                    if (d.data[i].odLimit != null)
                        d.data[i].odLimit = '<span id = "odLabel_' + i + '">' + d.data[i].odLimit + '</span><span id="odTxtbox_' + i + '" class="hide"><input type = "text" id = "newOdLimit_' + i + '" class="form-control form-control-sm" placeholder = "OD Limit" maxlength = 8 onkeypress = "return onlyAmount(event)"></span>'
                    else
                        d.data[i].odLimit = '<span id = "odLabel_' + i + '">' + '' + '</span><span id="odTxtbox_' + i + '" class="hide"><input type = "text" id = "newOdLimit_' + i + '" class="form-control form-control-sm" placeholder = "OD Limit" maxlength = 8 onkeypress = "return onlyAmount(event)"></span>'

                    if (d.data[i].wfhLimit != null)
                        d.data[i].wfhLimit = '<span id = "wfhLabel_' + i + '">' + d.data[i].wfhLimit + '</span><span id="wfhTxtbox_' + i + '" class="hide"><input type = "text" id = "newWfhLimit_' + i + '" class="form-control form-control-sm" placeholder = "WFH Limit" maxlength = 8 onkeypress = "return onlyAmount(event)"></span>'
                    else
                        d.data[i].wfhLimit = '<span id = "wfhLabel_' + i + '">' + '' + '</span><span id="wfhTxtbox_' + i + '" class="hide"><input type = "text" id = "newWfhLimit_' + i + '" class="form-control form-control-sm" placeholder = "WFH Limit" maxlength = 8 onkeypress = "return onlyAmount(event)"></span>'


                    d.data[i].Action = '<span id="editbt_' + i + '"><a onmouseenter="tooltipHover()" onclick = "onEditClick(' + i + ')"  data-bs-toggle="tooltip" title = "Edit" type = "button"class="btn btn-icon btn-edit icon-only" ><i class="icon-xl fas fa-pencil-alt"></i></a></span><span id = "updateCancel_' + i + '" class="hide"><a onmouseenter="tooltipHover()" type = "button" data-bs-toggle="tooltip" title = "Update" class="btn btn-icon btn-update icon-only " onclick = "onUpdateClick(' + i + ',' + "'" + d.data[i].employeeGI + "'" + ')"> <i class="icon-xl fas fa-sync-alt"></i> </a><a onmouseenter="tooltipHover()" type = "button" data-bs-toggle="tooltip" title = "Cancel" class="btn btn-icon btn-cancel icon-only " onclick = "onCancelClick(' + i + ')"> <i class="icon-xl fas fa-times"></i></a></span>'


                }

                return d.data;
            }

        },

        "columns": [

            { "data": "employeeID", "width": "10%" },
            { "data": "employeeName", "width": "18%" },
            { "data": "designation.name", "width": "13%" },
            { "data": "department.name", "width": "13%" },
            { "data": "location.name", "width": "12%" },
            { "data": "leaveGroup.name", "width": "12%" },
            { "data": "odLimit", "width": "7%" },

            { "data": "wfhLimit", "width": "7%" },
            { "data": "Action", "width": "8%" },

        ],

        /* "dom": '<t><"my-wrapper"<"my-inner2"l><"my-inner1"i><"my-inner3"p>>',*/


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


    const myTimeout = setTimeout(tooltipHover, 500);


}
function onEditClick(i) {
    debugger

    $('#newOdLimit_' + i).val($('#odLabel_' + i).text());
    $('#odLabel_' + i).addClass('hide');
    $('#editbt_' + i).addClass('hide');
    $('#odTxtbox_' + i).removeClass('hide');
    $('#updateCancel_' + i).removeClass('hide');

    $('#newWfhLimit_' + i).val($('#wfhLabel_' + i).text());
    $('#wfhLabel_' + i).addClass('hide');
    $('#editbt_' + i).addClass('hide');
    $('#wfhTxtbox_' + i).removeClass('hide');
    $('#updateCancel_' + i).removeClass('hide');

}
function onUpdateClick(i, EmployeeGI) {
    debugger
    $('#OndutyWfhSettingsList_' + i + '__OdLimit').val($('#newOdLimit_' + i).val());
    $('#odLabel_' + i).html($('#newOdLimit_' + i).val());
    $('#odLabel_' + i).removeClass('hide');
    $('#OndutyWfhSettingsList_' + i + '__WfhLimt').val($('#newWfhLimit_' + i).val());
    $('#wfhLabel_' + i).html($('#newWfhLimit_' + i).val());
    $('#wfhLabel_' + i).removeClass('hide');
    $('#editbt_' + i).removeClass('hide');
    $('#odTxtbox_' + i).addClass('hide');
    $('#wfhTxtbox_' + i).addClass('hide');
    $('#updateCancel_' + i).addClass('hide');
    var EmployeeGIValue = EmployeeGI;
    var OdLmt = $('#newOdLimit_' + i).val();
    var WfhLmt = $('#newWfhLimit_' + i).val()

    if (OdLmt == "" || OdLmt == null) {
        swal.fire("Onduty limit should not be null");
        fillData();
        return false;
    }
    if (WfhLmt == "" || WfhLmt == null) {
        swal.fire("Work from home limit should not be null");
        fillData();
        return false;
    }
    debugger
    $.ajax({
        type: 'POST',
        url: '/TimeOffice/OndutyWfhSettings/EmployeeWiseSave',
        dataType: 'json',
        data: "UpdatedOdLimit=" + OdLmt + "&UpdatedWfhLimit=" + WfhLmt + "&EmployeeGI=" + EmployeeGI,
        success: function (result) {
            debugger;
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
    debugger;

    var fData = new FormData($('#odWfhSettingsUpload')[0]);
    $("#Excel_FileData").val('');
    $.ajax({

        type: 'POST',
        url: '/TimeOffice/OndutyWfhSettings/ImportFromExcel',
        contentType: false,
        processData: false,
        data: fData,
        success: function (result) {
            debugger;
            if (result.success == true) {
                $("#Excel_FileData").val('');
                swal.fire("Uploaded Successfully").then(function () {
                    fillData()
                    var m = $('.bd-example-modal-lg');
                    m.modal('hide');
                });
            }
            else {
                $("#Excel_FileData").val('');
                if (result.message != null) {
                    $('#Unmatched').html('')
                    swal.fire(result.message);
                }
                else {
                    $('#Unmatched').html(result)
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
    return false;
}

function onCancelClick(i) {
    $('#odLabel_' + i).removeClass('hide');
    $('#wfhLabel_' + i).removeClass('hide');
    $('#editbt_' + i).removeClass('hide');
    $('#odTxtbox_' + i).addClass('hide');
    $('#wfhTxtbox_' + i).addClass('hide');
    $('#updateCancel_' + i).addClass('hide');
    $('#newWfhLimit_' + i).val(0);
    $('#newOdLimit_' + i).val(0);
}
function OnLeaveRuleSaveClick() {
    debugger;
    var LeaveGroup = $('#LeaveGroup_Code').val();
    if (validateDropdowns()) {

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
                    url: '/TimeOffice/OndutyWfhSettings/Save',
                    dataType: 'json',
                    data: $('#Other-Settings-Save').serialize() + "&LeaveGroup=" + LeaveGroup,
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                GetDetails();

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
        CloseSlider('basicFilter');
    }
}
function onExportToExcelClick() {
    debugger;
    var searchValue = $('#dataTableSearch').val();
    var actvacCount = $("#OndutyWfhSettingsList_Count").val();
    if (currentTab == 'MonthlyLimit') {

        if (actvacCount > 0) {
            url = "/TimeOffice/OndutyWfhSettings/onExportToExcelMonrhlyLimit?totalRecord=" + actvacCount + '&searchValue=' + searchValue + '&data=' + $('#BasicFilter').serialize();
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
    }

    else {
        var i = 0;
        if (!validateById('#LeaveGroup_Code', 'LeaveGp')) {
            i++;
        }
        if (i > 0) {
            return false;
        }
        else {
            var LeaveGroup = $('#LeaveGroup_Code').val();
            var LGName = $('#LeaveGroup_Code option:selected').text();

            var TableLength = document.getElementById("OtherSettingsTable").rows.length - 1;
            if (TableLength > 0) {
                url = "/TimeOffice/OndutyWfhSettings/ExportToExcelOtherSettings?LeaveGroup=" + LeaveGroup + "&LGName=" + LGName;
                window.location.href = url;
            }
            else {
                swal.fire("No data found to export");
            }

        }
    }
}
function ImportExcel() {
    var m = $('.bd-example-modal-lg');
    m.modal('show');
}