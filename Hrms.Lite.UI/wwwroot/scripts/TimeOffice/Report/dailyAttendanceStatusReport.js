
function ValidateFromAndToDate(sender,span) {

    var fromDate = new Date($('#FromDate').val());
    var toDate = new Date($('#ToDate').val());
    var d = new Date();
    var diffTime = toDate - fromDate;
    var diffDays = diffTime / (1000 * 3600 * 24);
    if (diffDays > 190) {
        $(sender).val('');
        $(sender).addClass('is-invalid');
        $(span).addClass('error-message');        
        $(span).html('From and To date should be less than or equal to 190 days');        
    }
    
}
function ExportToExcel() {
    //$.ajaxSetup({ async: false });
    //swal.fire({
    //    text: "Do you want to " + mode + "?",
    //    icon: 'warning',
    //    showCancelButton: true,
    //    confirmButtonColor: '#3085d6',
    //    cancelButtonColor: '#d33',
    //    confirmButtonText: 'Ok'
    //}).then((result) => {
    //    if (result.isConfirmed) {
    // $.ajaxSetup({ async: false });
    var i = 0;
    if (!validateById('#FromDate', 'fromDte'))
        i++;
    if (!validateById('#ToDate', 'toDte'))
        i++;
    $.ajax({
        url: '/TimeOffice/DailyAttendanceStatusReport/CountCheckAJAX',
        type: "POST",
        dataType: 'json',
     /*   async:false,*/
        data: $('#dailyAttendanceStatusReport').serialize(),
        success: function (result) {
            debugger;

                if (result == true) {
                    debugger;
                    i++;
                }



        },


        error: function (xhr, textStatus, errorThrown) {
            $('body').html(xhr.responseText);
        }
    });
    if (i == 0) {
        return true;
        //debugger;


        //var ds = $('#dailyAttendanceStatusReport').serialize();
        //var url = "/TimeOffice/DailyAttendanceStatusReport/ExportToExcel";

        //$.ajax({
        //    url: url,
        //    type: "POST",
        //    data: ds,
        //    success: function (response) {
        //        // Handle success response if needed

        //        // Reload the page
        //       // location.reload();
        //    },
        //    error: function (xhr, status, error) {
        //        // Handle error response if needed
        //    }
        //});

        //$.ajax({

        //    type: 'POST',
        //    url: '/TimeOffice/DailyAttendanceStatusReport/GetDetails',
        //    dataType: 'json',
        //    data: $('#dailyAttendanceStatusReport').serialize(),
        //    success: function (result) {
        //        debugger;
        //        if (result) {
   
        //            var url = '/TimeOffice/DailyAttendanceStatusReport/ExportToExcel';
        //            window.location.href = url;
        //        }
        //        else {
        //            swal.fire("No data found");
        //        }
        //    },
        //    error: function (xhr, textStatus, errorThrown) {
        //        debugger;
        //        document.body.id = "kt_body";
        //        $('body').html(xhr.responseText);
        //    }
        //});


    }



        //}
        else {
            return false;
        }
    //})
}

function getDataSetCountAJAX() {
    debugger;
    var i = 0;
    if (!validateById('#FromDate', 'fromDte'))
        i++;
    if (!validateById('#ToDate', 'toDte'))
        i++;
    if (i == 0) {
        //showSpinner();
        $.ajax({
            url: '/TimeOffice/DailyAttendanceStatusReport/CountCheckAJAX',
            type: "POST",
            async: false,
            dataType: 'json',
            data: $('#dailyAttendanceStatusReport').serialize(),
            success: function (result) {
                debugger;
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    if (result == true) {
                        debugger;
                        $("#dailyAttendanceStatusReport").submit();
                        
                    }
                    else {
                        swal.fire("No Data Found");
                        return false;

                    }

                }
                hideSpinner();

            },


            error: function (xhr, textStatus, errorThrown) {
                $('body').html(xhr.responseText);
                hideSpinner();
            }
        });
    }
    else {
        hideSpinner();
        return false;
    }
}


