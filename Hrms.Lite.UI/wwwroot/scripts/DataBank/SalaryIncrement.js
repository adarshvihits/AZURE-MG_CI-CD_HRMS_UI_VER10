var ArrangeOrder = 0;
function onClassificationApplicabilityClick(i,Mode)
{
    debugger
    var details1 = $("#ClassificationTable :input").serializeArray();
    var details2 = $("#SettingsList").serialize();
    ArrangeOrder = ArrangeOrder + 1;
    if (Mode == 'ALW')
    {
        $('#AllowanceList_' + i + '__Order').val(ArrangeOrder);
        $('#AllowanceList_' + i + '__Type').val(0);

    }
    else
    {
        $('#ClassificationList_' + i + '__Order').val(ArrangeOrder);
        $('#ClassificationList_' + i + '__Type').val(2);
    }

    $.ajax({

        type: 'POST',
        url: '/DataBank/SalaryIncrementTool/onClassificationApplicabilityClick',
        dataType: 'html',
        data: $("#ClassificationAndAllowance").serialize(),
        success: function (result) {

            $('#SettingsListTab').html("");
            $('#SettingsListTab').html(result);

        },
        error: function (xhr, textStatus, errorThrown) {
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
        }
    });
}

function ToolSettingsSave()
{
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
                url: '/DataBank/SalaryIncrementTool/Save',
                dataType: 'json',
                data: $('#SaveSettings').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function ()
                        {

                            window.location.reload();

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


function onImportExcelClick() {
    if (validateUploadExcel()) {
        swal.fire({
            text: "Do you want to Save?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                var MPPersonalInfo = new FormData($('#SalaryIncrementUpload')[0]);
                //$("#Excel_FileData").val('');
                $.ajax({

                    type: 'POST',
                    url: '/DataBank/SalaryIncrementTool/ImportFromExcel',
                    contentType: false,
                    processData: false,
                    data: MPPersonalInfo,
                    success: function (result) {
                        if (result.success == true) {
                            /*$("#Excel_FileData").val('');*/
                            swal.fire("Uploaded Successfully").then(function () {

                                $('.PrepareExcel').removeClass('hide');
                                $('.ImportExcel').addClass('hide');
                                $("#Excel_FileData").val('');
                            });
                        }
                        else {
                            $("#Excel_FileData").val('');
                            if (result.message != null) {
                                $('#Unmatchedlist').html('')
                                swal.fire(result.message);
                            }
                            else {
                                var m = $('.bd-example-modal-lg');
                                m.modal('show');
                                $('#Unmatchedlist').html('')
                                $('#Unmatchedlist').html(result);

                                $('.PrepareExcel').removeClass('hide');
                                $('.ImportExcel').addClass('hide');
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

function OnPrepareExcel() {
    if (validateUploadExcel()) {

        swal.fire({
            text: "Do you want to Upload?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                var MPPersonalInfo = new FormData($('#SalaryIncrementUpload')[0]);
                /* $("#Excel_FileData").val('');*/
                $.ajax({

                    type: 'POST',
                    url: '/DataBank/SalaryIncrementTool/ImportFromExcel',
                    contentType: false,
                    processData: false,
                    data: MPPersonalInfo,
                    success: function (result) {
                        if (result.success == true) {
                            //$("#Excel_FileData").val('');
                            swal.fire("Uploaded Successfully !!").then(function () {
                                window.location.href = '/DataBank/SalaryIncrementTool/Index';
                                //$('.ProcessExcel').removeClass('hide');
                                //$('.PrepareExcel').addClass('hide');

                            });
                        }
                        else {
                            $("#Excel_FileData").val('');
                            if (result.message != null) {
                                $('#Unmatchedlist').html('')
                                swal.fire(result.message);
                            }
                            else {
                                var m = $('.bd-example-modal-lg');
                                m.modal('show');
                                $('#Unmatchedlist').html('')
                                $('#Unmatchedlist').html(result);
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
function onProcessExcelClick() {
    if (validateUploadExcel()) {
        swal.fire({
            text: "Do you want to Process?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                var MPPersonalInfo = new FormData($('#SalaryIncrementUpload')[0]);
                /* $("#Excel_FileData").val('');*/
                $.ajax({

                    type: 'POST',
                    url: '/DataBank/SalaryIncrementTool/ProcessExcelClick',
                    contentType: false,
                    processData: false,
                    data: MPPersonalInfo,
                    success: function (result) {
                        if (result.success == true) {

                            swal.fire("Process Completed Please Save The File").then(function () {
                                $('.ImportExcel').removeClass('hide');
                                $('.ProcessExcel').addClass('hide');

                            });
                        }
                        else {

                            $("#Excel_FileData").val('');
                            if (result.message != null) {
                                swal.fire(result.message);
                            }
                            swal.fire(result.message);
                            $('.PrepareExcel').removeClass('hide');
                            $('.ProcessExcel').addClass('hide');
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
            else {
                return false;
            }
        })
    }
    else {
        return false;
    }
}
function validateUploadExcel() {
    var i = 0;
    if (!validateById('#TransitionType_Code', 'TransitionType')) {
        i++;
    }
    if (!validateById('#Reason_Code', 'Reason')) {
        i++;
    }
    if (!validateById('#Remark', 'currEmploymentComments')) {
        i++;
    }
    if (!validateById('#Excel_FileData', 'FileCheck')) {
        i++;
    }
    if (i == 0)
        return true;
    else
        return false;
}
