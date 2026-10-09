function GetList() {
    $('.mCode').val($('#MonthCode').val());
    $.ajax({

        type: 'POST',
        url: '/Master/SalaryPeriodLock/GetList',
        dataType: 'html',
        data: { currentmonth: $('#MonthCode').val() },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#LockUnlockList').html("");
                $('#LockUnlockList').html(result);
            }            


        },


        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
            hideSpinner();

        }
    });

}

function GetLockUnlockPopUp(mode) {
    debugger
   
    $.ajax({
        type: 'GET',
        url: "/Master/SalaryPeriodLock/GetPopUp?mode="+mode,
        dataType: 'html',

        success: function (Data) {
            debugger;
            var m = $('.bd-example-modal-lg');
            m.find('.modal-body').html('');
            m.find('.modal-body').html(Data);
            $("#myDiv").css("display", "block");
          

        },
        error: function (xhr, textStatus, errorThrown) {
            swal.fire(xhr.responseText);

        }
    });
}


function GetOpenMonthPopUp(mode) {
    debugger

    $.ajax({
        type: 'GET',
        url: "/Master/SalaryPeriodLock/GetPopUp?mode=" + mode,
        dataType: 'html',

        success: function (Data) {
            debugger;
            console.log(Data);
            var m = $('.rs_settings_applicable');
            m.find('.modal-body1div').html('');
            debugger;
            m.find('.modal-body1div').html(Data);


        },
        error: function (xhr, textStatus, errorThrown) {
            swal.fire(xhr.responseText);

        }
    });
}

function GetMonthPopUpList()
{
    showSpinner();
    var month = $('.popupOpenMonth').val();
    var pgroup = $('.popupOpenPGCode').val();
    var url = "/Master/SalaryPeriodLock/GetMonthPopUpList?month=" + month + "&PGcode=" + pgroup;
    $('#openmonthTable').load(url, function () { hideSpinner(); });
}

function GetLockUnLockList() {
    showSpinner();
    var month = $('.popupLockMonth').val();
    var pgroup = $('.popupLockPGCode').val();
    var url = "/Master/SalaryPeriodLock/GetLockUnLockList?month=" + month + "&PGcode=" + pgroup;
    $('#LockunlockTable').load(url, function () { hideSpinner(); });
}






function SaveUnLockClick(locktype,lock,PgCode) {
    debugger;
    //console.log($('#LockUnlock_Save').serialize());    
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
                url: '/Master/SalaryPeriodLock/Save',
                dataType: 'json',
                data: $('#LockUnlock_Save').serialize() + "&LockType=" + locktype + "&Lock=" + lock + "&PGCode=" + PgCode + "&MonthCode=" + $('#MonthCode').val(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            debugger;
                            //var url = "/Master/SalaryPeriodLock/Index";
                            //window.location.href = url;
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

                }
            });
        }
        else {
            return false;
        }
    })


}


function SaveLockClick(locktype, lock, PgCode, reason) {
    debugger;
    //console.log($('#LockUnlock_Save').serialize());
    if (locktype == "LockSalaryProcessing" || locktype == "LockAttendanceProcessing") {
        $.ajax({

            type: 'POST',
            url: '/Master/SalaryPeriodLock/Save',
            dataType: 'json',
            data: $('#LockUnlock_Save').serialize() + "&LockType=" + locktype + "&Lock=" + lock + "&PGCode=" + PgCode + "&Remarks=" + reason + "&MonthCode=" + $('#MonthCode').val(),
            success: function (result) {
                if (result.success == true) {
                    swal.fire(result.message).then(function () {
                        debugger;
                        //var url = "/Master/SalaryPeriodLock/Index";
                        //window.location.href = url;
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

            }
        });
    }
    else {

        $.ajax({

            type: 'POST',
            url: '/Master/SalaryPeriodLock/Save',
            dataType: 'json',
            data: $('#LockUnlock_Save').serialize() + "&LockType=" + locktype + "&Lock=" + lock + "&PGCode=" + PgCode + "&Remarks=" + reason + "&MonthCode=" + $('#MonthCode').val(),
            success: function (result) {
                if (result.success == true) {
                    swal.fire(result.message).then(function () {
                        debugger;
                        //var url = "/Master/SalaryPeriodLock/Index";
                        //window.location.href = url;
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

            }
        });


    //swal.fire({
    //    text: "Do you want to Save?",
    //    icon: 'warning',
    //    showCancelButton: true,
    //    confirmButtonColor: '#3085d6',
    //    cancelButtonColor: '#d33',
    //    confirmButtonText: 'Ok'
    //}).then((result) => {

    //    debugger;
    //    /*   showReasonBox(result);*/

    //    if (result.isConfirmed) {
            
    //    }
    //    else {
    //        return false;
    //    }
    //})
}

}

function CheckSalaryLockOrNot(locktype, lock, PgCode, index,lockStatus) {
    debugger;
    var salLock = $('#SalaryPeriodLockList_' + index + '__salaryProcessing').val();
    if (salLock != 'True' || lockStatus =='NotSaved')
        showReasonBoxAndSave(locktype, lock, PgCode, lockStatus);
    else
        swal.fire("Salary has already been processed, attendance cannot be unlocked. Please unlock the salary for the month first, and then try again.");
}


function showReasonBoxAndSave(locktype, lock, PgCode, lockStatus) {
    debugger;
    var title = '';
    if (locktype == "LockSalaryProcessing" && lockStatus !='NotSaved') {
        title = 'Please note : Unlocking the salary will delete all the processed salary data from the database. Do you want to continue?';
    }
    else if (locktype == "LockAttendanceProcessing" && lockStatus != 'NotSaved') {
        title = 'Unlocking the attendance will delete all the monthly attendance records used for salary processing from the database. After unlocking, you will need to process the monthly attendance again to proceed with salary processing. Do you want to continue?';
    }
    else {
        title = "Do you want to unlock?"
    }
    swal.fire({
        title: title,
        //text: "Reason to unlock",
        inputLabel: 'Reason to unlock',
        input: 'text',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok',

          preConfirm: (value) => {
            if (!value) {
                Swal.showValidationMessage(
                    'Reason for unlock is mandatory'
                )
            }
        }

    }).then((result) => {
         if (result.value) {           
            //console.log("Result: " + result.value);
            this.SaveLockClick(locktype, lock, PgCode, result.value);
        }
       
});
  
  
}



function getOpen_UnlockExcel(type, sal_month, sal_payrollgrp)
{
    debugger
   
    var month = $('.' + sal_month).val();
    var pgroup = $('.' + sal_payrollgrp).val();
    var monthName = $('.' + sal_month+' option:selected').text();
    var pgName = $('.' + sal_payrollgrp+' option:selected').text();
   
    var length = $('#LockUnlock_Count').val();
    if (length > 0) {
        url = "/Master/SalaryPeriodLock/ExportToExcel?Type=" + type +"&Month=" + month + " & PGcode=" + pgroup + " &MName=" + monthName + " &PName=" + pgName;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
   
}
