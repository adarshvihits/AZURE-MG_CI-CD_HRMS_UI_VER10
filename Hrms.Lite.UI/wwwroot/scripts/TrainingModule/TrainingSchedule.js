

function fillHeading()
{
    var StatusCode = $('input[name="Status"]:checked').val();
    var FinancialYearCode = $('#FinancialYear_Code').val();
    $.ajax({

        type: 'GET',
        url: '/TrainingModule/TrainingSchedule/GetList',
        dataType: 'html',
        data: { StatusCode: StatusCode, FinancialYearCode: FinancialYearCode},
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#TrainingScheduleList').html("");
                $('#TrainingScheduleList').html(result);
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

function AddNewTrainingSchedule(Type)
{
    
        $.ajax({

            type: 'GET',
            url: '/TrainingModule/TrainingSchedule/Create',
            dataType: 'html',
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('#AddNewTrainingSchedule').html("");
                    $('#AddNewTrainingSchedule').html(result);
                
                }


            },


            error: function (xhr, textStatus, errorThrown) {
                debugger
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);

            }
        });
   
   
}

function Edit(ScheduleCode, Type)
{
    $.ajax({

        type: 'GET',
        url: '/TrainingModule/TrainingSchedule/Edit',
        dataType: 'html',
        data: { ScheduleCode: ScheduleCode },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('body').append('<div class="form-overlay"></div>');
                $('#AddNewTrainingSchedule').html("");
                $('#AddNewTrainingSchedule').html(result);
                fillHeading();
            }


        },


        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);

        }
    });
}

function DateSelect()
{
    var Month=$('#Month_Code').val();
    $.getJSON("/TrainingModule/TrainingSchedule/GetFRomToDates", { Month: Month },
        function (data)
        {
       
            $("#StartDate").val(_GetFormattedDate(data));
            $("#EndDate").val(_GetFormattedDate(data));

    });
}



function AddDatechange()
{
    var StartDate = $('#StartDate').val();

    $("#EndDate").val(_GetFormattedDate(StartDate));

}





function fillAssessmentRequired()
{
    var StatusCode = $('input[name="PostassessmentRequired"]:checked').val();
    if (StatusCode == 2) {
        $('.AssessmentDate').addClass('hide')
    }
    else
    {
        $('.AssessmentDate').removeClass('hide')
    }
}

function TrainingScheduleSave()
{
    if (validateTrainingScheduleSave()) {

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
                    url: '/TrainingModule/TrainingSchedule/Save',
                    dataType: 'json',
                    data: $('#TrainingScheduleSave').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function ()
                            {
                                CloseSlider('TrainingSchedule');
                                fillHeading();
                            });

                        }

                        else {
                            swal.fire(result.message).then(function () {

                            });
                        }

                    },
                    error: function (xhr, textStatus, errorThrown) {
                        debugger
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
    else {
        return false;
    }
}

function validateTrainingScheduleSave() {
    var i = 0;
    if (!validateById('#Training_Code', 'TrainingCode')) {
        i++;
    }

    if (!validateById('#Month_Code', 'MonthCode')) {
        i++;
    }
    if (!validateById('#StartDate', 'StartDateschedule')) {
        i++;
    }
    if (!validateById('#EndDate', 'EndDateSchedule')) {
        i++;
    }
    if (!validateById('#RequestUptoDate', 'RequestUpTo')) {
        i++;
    }
    if (!validateById('#RequestType_Code', 'RequestCodeType')) {
        i++;
    }
    if (!validateById('#RequestUpTo', 'RequestDateUpTo')) {
        i++;
    }
    //if (!validateById('#Fromperiod', 'fromdate')) {
    //    i++;
    //}
    //if (!validateById('#Toperiod', 'Todate')) {
    //    i++;
    //}

    if (i == 0)
        return true;
    else
        return false;
}

function Delete(ScheduleCode) {
    $.ajax({

        type: 'GET',
        url: '/TrainingModule/TrainingSchedule/Delete',
        dataType: 'html',
        data: { ScheduleCode: ScheduleCode },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('body').append('<div class="form-overlay"></div>');
                $('#AddNewTrainingSchedule').html("");
                $('#AddNewTrainingSchedule').html(result);
                fillAssessmentRequired();
            }


        },


        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);

        }
    });
}


function TrainingScheduleDelete()
{
    $.ajax({

        type: 'POST',
        url: '/TrainingModule/TrainingSchedule/Delete',
        dataType: 'json',
        data: $('#TrainingScheduleDelete').serialize(),
        success: function (result) {
            if (result.success == true) {
                swal.fire(result.message).then(function () {
                    CloseSlider('DeleteSchedule');
                    fillHeading();
                });

            }

            else {
                swal.fire(result.message).then(function () {

                });
            }

        },
        error: function (xhr, textStatus, errorThrown) {
            debugger
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

function AddTopic(ScheduleCode, TrainingCode) {

    $.ajax({

        type: 'GET',
        url: '/TrainingModule/TrainingSchedule/AddTopic',
        dataType: 'html',
        data: { ScheduleCode: ScheduleCode, TrainingCode: TrainingCode},
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('body').append('<div class="form-overlay"></div>');
                $('#AddNewTrainingSchedule').html("");
                $('#AddNewTrainingSchedule').html(result);
              /*  fillTopicList(ScheduleCode);*/

            }


        },


        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);

        }
    });


}
function FilltrainerCombo()
{
    var Type = $('#TrainerType_Code').val();

    $.getJSON("/TrainingModule/TrainingSchedule/GetTrainer", { id: Type }, function (data) {
        debugger
        $('#Trainer_Code option').remove();
        $('#Trainer_Code').append('<option value>--Select Trainer--</option');

        for (var i = 0; i < data.length; i++) {
            $('#Trainer_Code').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
        }
    });
}

function FillVenueCombo() {
    var Type = $('#VenueType_Code').val();

    $.getJSON("/TrainingModule/TrainingSchedule/GetVenue", { id: Type }, function (data) {
        debugger
        $('#Venue_Code option').remove();
        $('#Venue_Code').append('<option value>--Select Venue--</option');

        for (var i = 0; i < data.length; i++) {
            $('#Venue_Code').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
        }
    });
}

function TrainingTopicSave() {

    if (validateTrainingTopicSave()) {

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
                    url: '/TrainingModule/TrainingSchedule/SaveTopic',
                    dataType: 'json',
                    data: $('#TrainingTopicSave').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function ()
                            {
                                filltopiclist();
                            });

                        }

                        else {
                            swal.fire(result.message).then(function () {

                            });
                        }

                    },
                    error: function (xhr, textStatus, errorThrown) {
                        debugger
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
    else {
        return false;
    }
}

function validateTrainingTopicSave() {
    var i = 0;
    if (!validateById('#Subject_Code', 'TrainingSub')) {
        i++;
    }

    if (!validateById('#TrainerType_Code', 'TrainerType')) {
        i++;
    }
    if (!validateById('#Trainer_Code', 'TrainerTopic')) {
        i++;
    }
    if (!validateById('#VenueType_Code', 'VenueType')) {
        i++;
    }
    if (!validateById('#Venue_Code', 'Venue')) {
        i++;
    }
    if (!validateById('#TrainingDate', 'TrainingDateTopic')) {
        i++;
    }
    if (!validateById('#StartTime', 'StartTimeTopic')) {
        i++;
    }
    if (!validateById('#EndTime', 'EndTime')) {
        i++;
    }
    //if (!validateById('#Toperiod', 'Todate')) {
    //    i++;
    //}

    if (i == 0)
        return true;
    else
        return false;
}


function filltopiclist()
{
    var ScheduleCode = $('#ScheduleCode').val();

    $.ajax({

        type: 'GET',
        url: '/TrainingModule/TrainingSchedule/GetTopicList',
        dataType: 'html',
        data: { ScheduleCode: ScheduleCode },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0)
            {
                window.location.reload();
            }
            else {
                $('#TopicList').html("");
                $('#TopicList').html(result);
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

function DeleteTopic(TransactionCode)
{
    

    $.ajax({

        type: 'POST',
        url: '/TrainingModule/TrainingSchedule/DeleteTopic',
        dataType: 'json',
        data: { TransactionCode: TransactionCode },
        success: function (result) {
            if (result.success == true) {
                swal.fire(result.message).then(function () {
                    
                    filltopiclist();
                });

            }

            else {
                swal.fire(result.message).then(function () {

                });
            }

        },
        error: function (xhr, textStatus, errorThrown) {
            debugger
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


function TrainingDateSelect() {
    debugger

    var startDateStr = $('#StartDate').val(); 
    var endDateStr = $('#EndDate').val(); 
    var trainingDateStr = $('#TrainingDate').val(); 



   
    var startDate = parseDate(startDateStr, 'DD-MM-YYYY HH:mm:ss');
    var endDate = parseDate(endDateStr, 'DD-MM-YYYY HH:mm:ss');

    
    var trainingDate = parseDate(trainingDateStr, 'DD-MMM-YYYY');
    

    if ((trainingDate < startDate) || (trainingDate > endDate)) {

        swal.fire('Date Selected is Not Between Start date and End date').then(function () {
            $('#TrainingDate').val($('#TrainingDate').val());
        });

    }


}



function parseDate(dateStr, format) {
    var parts, dateObj;

    if (format === 'DD-MM-YYYY HH:mm:ss') {
        
        parts = dateStr.split(' ');
        var dateParts = parts[0].split('-'); 
        dateObj = new Date(dateParts[2], dateParts[1] - 1, dateParts[0], ...parts[1].split(':'));
    } else if (format === 'DD-MMM-YYYY') {
       
        parts = dateStr.split('-');
        var monthNames = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
        var monthIndex = monthNames.indexOf(parts[1]);
        dateObj = new Date(parts[2], monthIndex, parts[0]);
    }

    return dateObj;
}


function ExportToExcel()
{
    debugger;

    var StatusCode = $('input[name="Status"]:checked').val();
    var FinancialYearCode = $('#FinancialYear_Code').val();

    var TableLength = $('#TrainingScheduleList_Count').val();
    if (TableLength > 0) {
        url = "/TrainingModule/TrainingSchedule/ExportToExcel? &FinancialYearCode=" + FinancialYearCode + "&StatusCode=" + StatusCode ;
        window.location.href = url;
    }
    else
    {
        swal.fire(NoDataFoundMsg);
    }
}
