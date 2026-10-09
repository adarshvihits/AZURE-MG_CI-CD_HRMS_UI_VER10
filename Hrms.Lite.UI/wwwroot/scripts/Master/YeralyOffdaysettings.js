
function onRowApplicabilityClick(index) {
    debugger
    if ($('.childCheck_' + index + ':checked').length == $('.Sub_' + index).length) {
        $('.parentCheck_' + index).prop('checked', true);
    } else {
        $('.parentCheck_' + index).prop('checked', false);
    }
    $("#DateTable_tbody").empty();
}

function onColumnApplicabilityClick(index) {
    debugger
    if ($('.parentCheck_' + index).is(':checked'))
        $('.childCheck_' + index).prop('checked', true);
    else
        $('.childCheck_' + index).prop('checked', false);
    $("#DateTable_tbody").empty();
}


function onDeleteClick(GI) {
    debugger;
    //var url = "/Master/YearlyOffdaySettings/Delete?OffdaySettingsGI=" + GI;
    //$('#DeleteSlider').load(url, function () { });
    //$('body').append('<div class="form-overlay"></div>');
    $.ajax({
        type: 'GET',
        url: '/Master/YearlyOffdaySettings/Delete',
        dataType: 'html',
        data: { OffdaySettingsGI: GI },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('body').append('<div class="form-overlay"></div>');
                $('#DeleteSlider').html("");
                $('#DeleteSlider').html(result);
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



function OnAddNeworEditClick(mode,GI) {
    debugger;
    if (mode == 'Create') {

        //var url = "/Master/YearlyOffdaySettings/Create";
        //$('#AddNewOrEditSlider').load(url, function () { });
        //$('body').append('<div class="form-overlay"></div>');



        $.ajax({

            type: 'GET',
            url: '/Master/YearlyOffdaySettings/Create',
            dataType: 'html',
           /* data: { OffdaySettingsGI: GI, LogGI: LogGI },*/
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('#AddNewOrEditSlider').html("");
                    $('#AddNewOrEditSlider').html(result);
                }


            },


            error: function (xhr, textStatus, errorThrown) {
                debugger
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);


            }
        });
    }
    else {
        debugger;
        //var url = "/Master/YearlyOffdaySettings/Edit?OffdayGI=" + GI ;
        //$('#AddNewOrEditSlider').load(url, function () { });
        //$('body').append('<div class="form-overlay"></div>');

        $.ajax({

            type: 'GET',
            url: '/Master/YearlyOffdaySettings/Edit',
            dataType: 'html',
            data: { OffdayGI: GI },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('#AddNewOrEditSlider').html("");
                    $('#AddNewOrEditSlider').html(result);
                }


            },


            error: function (xhr, textStatus, errorThrown) {
                debugger
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);

            }
        });
    }
  
}


function onNextClick() {
    debugger
    var year = $('#Year_Code').val();

    if (validateOffdaySettings()) {

        console.log($('#offdaylistSubmit').serialize());
        $.ajax({

            type: 'POST',
            url: '/Master/YearlyOffdaySettings/GetDays?Year=' + year,
            dataType: 'html',
            data: $('#offdaylistSubmit').serialize(),
            success: function (result) {


                debugger;
                $('.bd-example-modal-lg').modal('hide');


                $('#DayGridSlider').html(result);
                $('#totcount').html($('#Dates_Count').val());
            },
            error: function (xhr, textStatus, errorThrown) {

                toastr.error(xhr.responseText);
            }
        });
    }
}

function YearlyOffdaySave(mode) {
    $.ajaxSetup({
        async: false
    });

    if (validateOffdaySettings() && ValidateDates()) {
        debugger
        swal.fire({
            text: "Do you want to " + mode + "?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({

                    type: 'POST',
                    url: '/Master/YearlyOffdaySettings/Save',
                    dataType: 'json',
                    data: $('#offdaylistSubmit').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                CloseSlider('AddNewOredit');
                                debugger
                                var url = "/Master/YearlyOffdaySettings/Index";
                                window.location.href = url;

                            });

                        }
                        else {
                            swal.fire(result.message).then(function () {
                                //var url = "/Master/YearlyOffdaySettings/Delete?OffdaySettingsGI=" + GI;
                                //$('#DeleteSlider').load(url, function () { });
                                //$('body').append('<div class="form-overlay"></div>');

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


function ValidateDates() {
    debugger;
    var checked_checkboxes = $("#daystable input[type=checkbox]:checked");
    if (checked_checkboxes.length == 0) {
        swal.fire("Select atleast one Days Combination");
        /* $(".error").show();*/
        return false;
    }
    if ($("#Dates_Count").val() == 0) {
        swal.fire("Dates grid should not be empty:Click Proceed button for filling grid");
       
        return false;
    }
    return true;
}




function validateOffdaySettings() {
    var i = 0;
    if (!validateById('#Year_Code', 'year')) {
        i++;
    }
    if (!validateById('#CombinationName', 'comboname')) {
        i++;
    }
    if (i == 0)
        return true;
    else
        return false;
}


function confirmDelete(GI) {
    debugger
    swal.fire({
        text: "Do you want to Delete?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({

                type: 'POST',
                url: '/Master/YearlyOffdaySettings/Delete',
                dataType: 'json',
                data: $('#Deleteoffday').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('DeleteDiv');
                            debugger
                            var url = "/Master/YearlyOffdaySettings/Index";
                            window.location.href = url;
                         
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/YearlyOffdaySettings/Delete?OffdaySettingsGI=" + GI;
                            $('#DeleteSlider').load(url, function () { });
                            $('body').append('<div class="form-overlay"></div>');

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


function ExportToExcel() {
    debugger
    var pendingTableLength = $('#List_Count').val();
    if (pendingTableLength > 0) {
        url = "/Master/YearlyOffdaySettings/ExportToExcel";
        window.location.href = url;
    }
    else {
        swal.fire(NoDataFoundMsg);
    }
}

