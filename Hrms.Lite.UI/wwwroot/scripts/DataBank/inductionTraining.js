function getInductionSlider() {
    debugger;
    if ($('#InductionTable').find('input[type="checkbox"]:checked').length == 0) {
        swal.fire("Please Select Atleast one Employee To Proceed")
    }
    else {
        var table = $('#InductionTable').DataTable();
        var data = table.$('input,select,textarea').serializeArray()
        $.ajax({

            type: 'POST',
            url: '/DataBank/InductionTraining/Create',
            dataType: 'html',
            data: data,
            success: function (result) {
                $('body').append('<div class="form-overlay"></div>');
                $('#InductionSlider').html("");
                $('#InductionSlider').html(result);
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
    
}

function onChkBoxClick(sender, allCheckBox, tableId) {
    debugger;
    

        debugger;
        //if ($('#' + tableId).find('input[type="checkbox"]:checked').length-1 == $('#' + tableId).find('input[type="checkbox"]').length - 1)
        //    $('#' + allCheckBox).prop('checked', true);
        //else
        //    $('#' + allCheckBox).prop('checked', false);

    if ($(sender).is(':checked')) {
        if (
            $('#dsg').find('input[type="checkbox"]:checked').length == $('#dsg').find('input[type="checkbox"]').length &&
            $('#dept').find('input[type="checkbox"]:checked').length == $('#dept').find('input[type="checkbox"]').length &&
            $('#loc').find('input[type="checkbox"]:checked').length == $('#loc').find('input[type="checkbox"]').length &&
            $('#cat').find('input[type="checkbox"]:checked').length == $('#cat').find('input[type="checkbox"]').length &&
            $('#grd').find('input[type="checkbox"]:checked').length == $('#grd').find('input[type="checkbox"]').length &&
            $('#pay').find('input[type="checkbox"]:checked').length == $('#pay').find('input[type="checkbox"]').length &&
            $('#div').find('input[type="checkbox"]:checked').length == $('#div').find('input[type="checkbox"]').length &&
            $('#sec').find('input[type="checkbox"]:checked').length == $('#sec').find('input[type="checkbox"]').length &&
            $('#empType').find('input[type="checkbox"]:checked').length == $('#empType').find('input[type="checkbox"]').length) {
            debugger;
            $('.SelectAll').prop('checked', true);
            if ($('#' + tableId).find('input[type="checkbox"]:checked').length == $('#' + tableId).find('input[type="checkbox"]').length)
                $('#' + allCheckBox).prop('checked', true);
            else
                $('#' + allCheckBox).prop('checked', false);
        }
        else {
            debugger;

            if ($('#' + tableId).find('input[type="checkbox"]:checked').length == $('#' + tableId).find('input[type="checkbox"]').length - 1)
                $('#' + allCheckBox).prop('checked', true);
            else
                $('#' + allCheckBox).prop('checked', false);
            if ($('#allDsg').is(':checked') && $('#allDept').is(':checked') &&
                $('#allLoc').is(':checked') && $('#allCat').is(':checked') &&
                $('#allGrd').is(':checked') && $('#allPay').is(':checked') &&
                $('#allDiv').is(':checked') && $('#allSec').is(':checked') &&
                $('#allEmpType').is(':checked')) {
                $('.SelectAll').prop('checked', true);
            }
            else {
                $('.SelectAll').prop('checked', false);
            }
        }
    }
    else {
        debugger;
        $('.SelectAll').prop('checked', false);
        if ($('#' + tableId).find('input[type="checkbox"]:checked').length == $('#' + tableId).find('input[type="checkbox"]').length)
            $('#' + allCheckBox).prop('checked', true);
        else
            $('#' + allCheckBox).prop('checked', false);
    }

   
}

function OnCheckBoxAllClick(colchkid, tableid) {
    debugger
    if ($('#' + colchkid).is(':checked'))
        $('#' + tableid).find('input[type=checkbox]').prop('checked', true);
    else
        $('#' + tableid).find('input[type=checkbox]').prop('checked', false);



}
//function onAllClick(chkid, tableid) {
//    if ($('#' + chkid).is(':checked'))
//        $('#' + tableid).find('input[type=checkbox]').prop('checked', true);
//    else
//        $('#' + tableid).find('input[type=checkbox]').prop('checked', false);
//}
function inductionSave() {
    debugger;
    var i = 0;
    if ($('#inductonTypeTable').find('input[type="checkbox"]:checked').length == 0) {
        swal.fire("Please Select Atleast one Training");
        i++;
    }
    if (!validateById('#AttendedDate', 'attendededdate')) {
        i++;
    }
    if (!validateById('#Remarks', 'remarks')) {
        i++;
    }
    if (i == 0) {
        swal.fire({
            text: "Do you want to Save",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({

                    type: 'POST',
                    url: '/DataBank/InductionTraining/Save',
                    dataType: 'json',
                    data: $('#InductionSliderSave').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                CloseSlider('CreateOrEditDiv');
                                ApplyFilter('apply');
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
    else
return false;

}

function getList() {
    $.ajax({

        type: 'GET',
        url: '/DataBank/InductionTraining/GetList',
        dataType: 'html',

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#InductionList').html("");
                $('#InductionList').html(result);
            }
            hideSpinner();
        },
        error: function (xhr, textStatus, errorThrown) {
            debugger
            hideSpinner();
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);


        }
    });
}


function ApplyFilter(mode) {
    debugger

    if (mode == 'apply') {
        if (filterApplicable >= 1) {
            $.ajax({

                type: 'POST',
                url: '/DataBank/InductionTraining/ApplyFilter',
                dataType: 'html',
                data: $('#BasicFilter').serialize(),
                success: function (result) {
                    debugger
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();

                    }
                    else {
                        $('#InductionList').html("");
                        $('#InductionList').html(result);
                    }
                    hideSpinner();
                    CloseSlider('basicFilter');

                },


                error: function (xhr, textStatus, errorThrown) {
                    document.body.id = "kt_body";
                    $('body').html(xhr.responseText);
                }
            });

            $('#filterForm').removeClass('filter-apply');
        }
        else {
            showSpinner();
            $.ajax({

                type: 'POST',
                url: '/DataBank/InductionTraining/ApplyFilter',
                dataType: 'html',
                data: $('#BasicFilter').serialize(),
                success: function (result) {
                    debugger
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }
                    else {
                        $('#InductionList').html("");
                        $('#InductionList').html(result);
                    }
                    hideSpinner();
                    CloseSlider('basicFilter');
                },


                error: function (xhr, textStatus, errorThrown) {
                    document.body.id = "kt_body";
                    $('body').html(xhr.responseText);
                }
            });

            $('#filterForm').addClass('filter-apply');
        }

    }
    else {
        showSpinner();
        filterApplicable = 1;
        $('.reset').val('');
        $('#Optional_Filter_EmploymentStatus_Code').val(0);
        $('#filterForm').removeClass('filter-apply');
        $.ajax({

            type: 'POST',
            url: '/DataBank/InductionTraining/ApplyFilter',
            dataType: 'html',
            data: $('#BasicFilter').serialize(),
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();

                }
                else {
                    $('#InductionList').html("");
                    $('#InductionList').html(result);
                }
                hideSpinner();

            },


            error: function (xhr, textStatus, errorThrown) {
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);
            }
        });

    }
}

function onExportToExcelClick() {

    debugger;
    var tableLength = $('#InductionTrainingList_Count').val();
    if (tableLength > 0) {
        //url = "/DataBank/JoiningReport/ExportToExcel?fromDate=" + fromDate + "&toDate=" + toDate + "&totRecords=" + tableLength;
        url = "/DataBank/InductionTraining/ExportToExcel?totRecords="+ tableLength;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }

}