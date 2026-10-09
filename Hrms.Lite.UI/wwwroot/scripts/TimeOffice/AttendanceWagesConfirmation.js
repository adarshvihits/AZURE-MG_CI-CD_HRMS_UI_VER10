

var prevTab = 'Holiday';
var currentTab = 'Holiday'; 

function fillMonth(sender) {
    debugger;
    $.getJSON("/PayRoll/SalaryProcessing/GetMonth", { PayrollGrp: $(sender).val() }, function (data) {

        $('#Month_Code option').remove();
        $('#Month_Code').append('<option value>--Select Month- </option');
        debugger;
        for (var i = 0; i < data.length; i++) {
            $('#Month_Code').append('<option value="' + data[i].code + '">' + data[i].name + '</option');
        }

    });
   
}
function onTabClick(tabName, tabIndex) {
    debugger
    showSpinner();
    var month = $("#Month_Code").val();
    var PGroup = $("#PGroup_Code").val();
    var ConfirmationValue = $("#ConfirmationStatus").val();
    currentTab = tabIndex;
    $("." + prevTab).removeClass("active");
    $("." + tabName).addClass("active");
 
    $.ajax({

        type: 'GET',
        url: '/TimeOffice/AttendanceWagesConfirmation/GetAttendancewagesList',  
        dataType: 'html',
        data: { TabIndex: tabIndex, month: month, PGroup:PGroup,ConfirmationStatus: ConfirmationValue},

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#AttendanceConfirmationList').html("");
                $('#AttendanceConfirmationList').html(result);
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

    if (currentTab == 'Holiday' ) $(".batch_hidebutton").removeClass('hide');
    else $(".batch_hidebutton").addClass('hide');
    prevTab = tabName;
}
function onbatchUpdateclick() {
    debugger;
    var Month = $("#Month_Code").val();
    var MonName = $('#Month_Code option:selected').text();
    var PGroup = $("#PGroup_Code").val();
    var PGroupName = $("#PGroup_Code option:selected").text();
    $.ajax({

        type: 'GET',
        url: '/TimeOffice/AttendanceWagesConfirmation/BatchUpdate',
        dataType: 'html',
        data: { TabIndex: currentTab, Month: Month, MonName: MonName, PGroup: PGroup, PGroupName: PGroupName  },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#EditConfirmationSlider').html("");
                $('#EditConfirmationSlider').html(result);
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
function batchUpdate() {
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
                url: '/TimeOffice/AttendanceWagesConfirmation/BatchUpdate',
                dataType: 'json',
                async: false,
                data: $("#wage_batch").serialize() +  $('#BasicFilter').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('batch');
                            GetList();
                        });
                    }

                    else {
                        swal.fire(result.message);

                    }

                },
                error: function (xhr, textStatus, errorThrown) {
                    document.body.id = "kt_body";
                    $('body').html(xhr.responseText);
                    hideSpinner();
                }
            });
        }
    });
}
function OnEditClick( EmployeeGI) {
    debugger;
    var month = $("#Month_Code").val();
    var ConfirmationValue = $("#ConfirmationStatus").val();
    



    $.ajax({

        type: 'GET',
        url: '/TimeOffice/AttendanceWagesConfirmation/GetEditConfirmation',
        dataType: 'html',
        data: { TabIndex: currentTab, month: month, ConfirmationStatus: ConfirmationValue, EmployeeGI: EmployeeGI},

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#EditConfirmationSlider').html("");
                $('#EditConfirmationSlider').html(result);
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
function GetList() {
    debugger;
    showSpinner();
    var month = $("#Month_Code").val();
    var PGroup = $("#PGroup_Code").val();
    var ConfirmationValue = $("#ConfirmationStatus").val();
    if (ConfirmationValue == 0) {
        $("#confimbtn").removeClass('hide');
        $(".batch_hidebutton").removeClass('hide');
    }
    else {
        $("#confimbtn").addClass('hide');
        $(".batch_hidebutton").addClass('hide');
    }

    
    $.ajax({

        type: 'GET',
        url: '/TimeOffice/AttendanceWagesConfirmation/GetAttendancewagesList',
        dataType: 'html',
        data: { TabIndex: currentTab, month: month, PGroup: PGroup, ConfirmationStatus: ConfirmationValue},

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#AttendanceConfirmationList').html("");
                $('#AttendanceConfirmationList').html(result);
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
function GetMonthWiseList() {
    debugger;
    showSpinner();
    var month = $("#Month_Code").val();
    var ConfirmationValue = $("#ConfirmationStatus").val();
    var PGroup = $("#PGroup_Code").val();

    $.ajax({

        type: 'GET',
        url: '/TimeOffice/AttendanceWagesConfirmation/GetAttendancewagesList',
        dataType: 'html',
        data: { TabIndex: currentTab, month: month, PGroup:PGroup,ConfirmationStatus: ConfirmationValue},

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#AttendanceConfirmationList').html("");
                $('#AttendanceConfirmationList').html(result);
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
function OnSliderSaveClick(EmployeeGI) {
    debugger
    var month = $("#Month_Code").val();
    var Remarks = $("#Details_Remarks").val();
    var value = $("#Details_ConfirmedValues").val();
    var i = 0;
    if (!validateById('#Details_ConfirmedValues', 'confValues'))
        i++;
    if (!validateById('#Details_Remarks', 'rmrks'))
        i++;
    if (i == 0) {
        swal.fire({
            text: "Do you want to Confirm?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({

                    type: 'POST',
                    url: '/TimeOffice/AttendanceWagesConfirmation/EmployeeWiseSave',
                    dataType: 'json',
                    async: false,
                    data: { TabIndex: currentTab, EmployeeGI: EmployeeGI, month: month, Value: value, Remarks: Remarks },
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                CloseSlider('EditconfirmationSlider');
                                GetList();
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
                        hideSpinner();
                    }
                });
            }
        });
    }
  

}

function ApplyFilter(mode) {
    debugger
    var tabIndex = $('#Tabindex').val();
    var search = $('#SearchId').val();

    $("#SearchId").val($('#FilterList_Code').val())
    var month = $("#Month_Code").val();
    var PGroup = $("#PGroup_Code").val();
    var TabIndex = currentTab;
    var ConfirmationValue = $("#ConfirmationStatus").val();
  
    if (mode == 'apply') {
            showSpinner();
        if (filterApplicable >= 1) {
            $.ajax({

                type: 'POST',
                url: '/TimeOffice/AttendanceWagesConfirmation/ApplyFilter',
                dataType: 'html',
                data: $('#BasicFilter').serialize() + "&Tab=" + TabIndex + "&month=" + month + "&ConfirmationStatus=" + ConfirmationValue + "&PGroup=" + PGroup,
                success: function (result) {
                    debugger
                    CloseSlider('basicFilter');

                 
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }
                    else {
                        $('#AttendanceConfirmationList').html("");
                        $('#AttendanceConfirmationList').html(result);
                    }
                    hideSpinner();
                    //$('#rewardsListPartial').addClass('hide');               
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
            $.ajax({

                type: 'POST',
                url: '/TimeOffice/AttendanceWagesConfirmation/ApplyFilter',
                dataType: 'html',
                data: $('#BasicFilter').serialize() + "&Tab=" + TabIndex + "&month=" + month + "&ConfirmationStatus=" + ConfirmationValue + "&PGroup=" + PGroup,

                success: function (result) {
                    debugger
                    CloseSlider('basicFilter');

                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }
                    else {
                        $('#AttendanceConfirmationList').html("");
                        $('#AttendanceConfirmationList').html(result);
                    }
                    hideSpinner();
                    //$('#rewardsListPartial').addClass('hide');               
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
        filterApplicable = 1;
        $('.reset').val('');
        $('#filterForm').removeClass('filter-apply');

        $.ajax({

            type: 'POST',
            url: '/TimeOffice/AttendanceWagesConfirmation/ApplyFilter',
            dataType: 'html',
            data: $('#BasicFilter').serialize() + "&Tab=" + TabIndex + "&month=" + month + "&ConfirmationStatus=" + ConfirmationValue + "&PGroup=" + PGroup,

            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('#AttendanceConfirmationList').html("");
                    $('#AttendanceConfirmationList').html(result);
                }
                hideSpinner(); 
            },
            error: function (xhr, textStatus, errorThrown) {
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);
                hideSpinner();

            }
        });
    }
}





function SubmitAllAttendanceList() {
    debugger
    if ($('#ConfirmationList_Count').val()==0) {
        swal.fire("No data found in list");
    }
    else {
        var month = $("#Month_Code").val();
        var confirmvalue = $('input[name="ConfirmationStatus"]:checked').val();
        var table = $('#AttendanceConfirmationTable').DataTable();
        var data = table.$('input,select,textarea').serializeArray();
        //console.log(data);
        /*  data.push({ name: 'MonthCode', value: month, name: 'ConfirmationStatus', value: confirmvalue, name: 'TabValues', value: currentTab });*/
        data.push({ name: 'ConfirmationStatus', value: confirmvalue });
        data.push({ name: 'Month.Code', value: month });
        data.push({ name: 'TabValues', value: currentTab });

        swal.fire({
            text: "Do you want to Confirm?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    type: 'POST',
                    url: '/TimeOffice/AttendanceWagesConfirmation/AttendanceWagesConfirmationSave',
                    dataType: 'json',
                    data: data,
                    success: function (result) {
                        debugger;
                        if (result.success == true) {
                            swal.fire("Confirmed Successfully").then(function () {

                                //var url = "/TimeOffice/AttendanceWagesConfirmation/Index";
                                //$('#AttendanceConfirmationList').load(url, function () { });
                                window.location.href = "/TimeOffice/AttendanceWagesConfirmation/Index";


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
        });
    }
   
    
}




function GetEmployeeWiseDetailsPopUp(EmployeeGI, confirmvalue,tabvalue) {
    debugger;
    var month = $("#Month_Code").val();
    //var confirmvalue = $("#Details_ConfirmedValues").val();
    var confirmvalue = $('input[name="ConfirmationStatus"]:checked').val();

    if (currentTab != "Shift") {
        $('#popupTitle').html(currentTab);
    }
    $.ajax({
        type: 'GET',
        url: "/TimeOffice/AttendanceWagesConfirmation/GetEmployeeWisePopUpDetails?TabIndex=" + currentTab + '&month=' + month + '&Value=' + confirmvalue + '&EmployeeGI=' + EmployeeGI + "&tabValue=" + tabvalue,
        dataType: 'html',

        success: function (Data) {
            var m = $('.bd-example-modal-lg');
            m.find('.modal-body').html('');
            m.find('.modal-body').html(Data);
            m.modal('show');
        },
        error: function (xhr, textStatus, errorThrown) {
            swal.fire(xhr.responseText);

        }
    });
}


function ExportToExcel() {
    debugger;
    var month = $("#Month_Code").val();
    var MonthName = $("#Month_Code option:selected").text();
    var PGroup = $("#PGroup_Code").val();
    var PGroupName = $("#PGroup_Code option:selected").text();
    var ConfirmationValue = $("#ConfirmationStatus").val();
    var pendingTableLength = $('#ConfirmationList_Count').val();
    if (pendingTableLength > 0) {
        url = "/TimeOffice/AttendanceWagesConfirmation/ExportToExcel?TabIndex=" + currentTab + "&Month=" + month +
            "&ConfirmationStatus=" + ConfirmationValue + "&MonthName=" + MonthName + "&PGroup=" + PGroup +"&PGroupName="+PGroupName;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}

function Pop_Up_ExportToExcel(GI,Name) {
    debugger;
    var month = $("#Month_Code").val();
    var MonthName = $("#Month_Code option:selected").text();
    var ConfirmationValue = $("#ConfirmationStatus").val();


    var pendingTableLength = $('#AttnDetails_Count').val();
    if (pendingTableLength > 0) {
        url = "/TimeOffice/AttendanceWagesConfirmation/Pop_Up_ExportToExcel?TabIndex=" + currentTab + "&Month=" + month + "&ConfirmationStatus=" + ConfirmationValue + "&MonthName=" + MonthName + "&EmployeeGI=" + GI + "&Name=" + Name;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}

function onOTEditClick(i) {
    $('#updatecancel_' +i).css('display', 'block');
    $('#confVal_' + i).css('display', 'block');
    $('#edit_' + i).css('display', 'none');
    $('#conf_' + i).css('display', 'none');

    $('#confMntsVal_' + i).css('display', 'block');
    $('#confMnts_' + i).css('display', 'none');

    $('#confirmedVal_' + i).css('display', 'block');
    $('#confirmed_' + i).css('display', 'none');
}



function onUpdateClick(i, tabvalue, EmployeeGI, Mode) {

    debugger
    var msg = '';
    var sucessMsg = '';
    var month = $('#Month_Code').val();
    var confirmvaluehrs = parseInt($('.confValues_' + i).val());
    var confirmvalueMnts = parseInt($('.confMntsValues_' + i).val());
    if (tabvalue == 'Shift' || tabvalue == 'Holiday' || tabvalue == 'OffDay')
        var confirmvalue = parseInt($('.confMntsValues_' + i).val());
    else
        var confirmvalue = parseInt(confirmvaluehrs * 60 + confirmvalueMnts)
    if (Mode == undefined) {
        //Mode = ''
        msg = 'Save'
        sucessMsg = "Saved Successfully"
    }
    if (Mode == 'Edit') {
        msg = 'Modify';
        sucessMsg = 'Updated Successfully'
    }
    if (Mode == 'Delete') {
        msg = 'Delete'
        sucessMsg = 'Deleted Successfully'
    }

    swal.fire({
        text: "Do you want to " + msg+"?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: 'GET',
                url: "/TimeOffice/AttendanceWagesConfirmation/EmployeeWiseAttendanceWagesConfirmedValueUpdate?tabValue=" + tabvalue + '&month=' + month + '&confirmvalue=' + confirmvalue + '&EmployeeGI=' + EmployeeGI + '&Mode=' + Mode,
                dataType: 'json',

                success: function (result) {
                    debugger;
                    if (result.success == true) {
                        swal.fire(sucessMsg).then(function () {
                            if (tabvalue == "Holiday")
                                onTabClick('Holiday', 'Holiday');

                            if (tabvalue == "OffDay")
                                onTabClick('OffDay', 'OffDay');

                            if (tabvalue == "OTHours")
                                onTabClick('OTHours', 'OTHours');

                            if (tabvalue == "LessHours")
                                onTabClick('LessHours', 'LessHours');

                            if (tabvalue == "Shift")
                                onTabClick('Shift', 'Shift');

                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {


                        });
                    }
                },
                error: function (xhr, textStatus, errorThrown) {
                    swal.fire(xhr.responseText);

                }
            });
        }
        else {
            return false;
        }
    })


   
}

function onCancelClick(i) {
    $('#updatecancel_' + i).css('display', 'none');
    $('#confVal_' + i).css('display', 'none');
    $('#edit_' + i).css('display', 'block');
    $('#conf_' + i).css('display', 'block');

    $('#confMntsVal_' + i).css('display', 'none');
    $('#confMnts_' + i).css('display', 'block');

    $('#confirmedVal_' + i).css('display', 'none');
    $('#confirmed_' + i).css('display', 'block');
}