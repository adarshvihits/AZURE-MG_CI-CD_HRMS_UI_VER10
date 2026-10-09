
function GetList() {
    debugger
    var monthCode = $('#Month_Code').val();
    $.ajax({

        type: 'GET',
        url: '/Master/DutyRosterLock/GetList',
        dataType: 'html',
        data: { Month: monthCode},
        success: function (result) {
            $('#ListView').html('');
            $('#ListView').html(result);
            DutyRosterLockDatePicker();

        },
        error: function (xhr, textStatus, errorThrown) {
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
        }
    });

   
}
function DutyRosterLockDatePicker() {
    var firstDayOfMonth = new Date($('#Month_Code option:selected').text())
    var lastDayOfMonth = new Date(firstDayOfMonth.getFullYear(), firstDayOfMonth.getMonth() + 1, 0);
    $('.kt_datepicker_12, .kt_datepicker_12_validate').datepicker({
        rtl: KTUtil.isRTL(),
        todayHighlight: true,
        orientation: "bottom left",        
        autoclose: true,



        format: "dd-M-yyyy",
        orientation: "auto",
        startDate: firstDayOfMonth,
        endDate: lastDayOfMonth,
        clearBtn: true
    });

    // input group layout for modal demo
    $('.kt_datepicker_12_modal').datepicker({
        rtl: KTUtil.isRTL(),
        todayHighlight: true,
        orientation: "bottom left",       
    });

}

function Save() {
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
                url: '/Master/DutyRosterLock/Save',
                dataType: 'json',
                data: $('#dutyRosterLock').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            GetList();
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

function onBatchUpdateClick() {
    var monthCode = $('#Month_Code').val();
    $.ajax({

        type: 'GET',
        url: '/Master/DutyRosterLock/GetBatchUpdate',
        dataType: 'html',
        data: { Month: monthCode },
        success: function (result) {
            $('#sliderView').html('');
            $('#sliderView').html(result);            
            $('body').append('<div class="form-overlay"></div>');
            DutyRosterLockDatePicker();
        },
        error: function (xhr, textStatus, errorThrown) {
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
        }
    });


}

function BatchUpdateSave() {
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
                url: '/Master/DutyRosterLock/BatchUpdateSave',
                dataType: 'json',
                data: $('#dutyRosterLockBatch').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('BatchUpdateDutyRosterLock');
                            GetList();
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