function onChange() {
    var MonthCode = $('#AllowanceMonth_Code').val();
    $.ajax({

        type: 'GET',
        url: '/PayRoll/VariableAllowanceUpload/GetVariableAllowanceUploadList',
        dataType: 'html',
        data: { MonthCode: MonthCode },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {
                $('#uploadList').html("");
                $('#uploadList').html(result);
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
function validateAllowanceDropdowns() {
    var i = 0;
 
    if (!validateById('#AllowanceMonth_Code', 'MonthAllowance')) {
        i++;
    }
    if (i == 0) {
        
        var m = $('.bd-example-modal-lg');
        m.modal('show');

    }

}
function onImportExcelClick() {
    swal.fire({
        text: "Do you want to Upload?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            var MPPersonalInfo = new FormData($('#variableAllowanceUpload')[0]);
            $("#Excel_FileData").val('');
            debugger
            $.ajax({

                type: 'POST',
                url: '/PayRoll/VariableAllowanceUpload/ImportFromExcel',
                contentType: false,
                processData: false,
                data: MPPersonalInfo,
                success: function (result) {
                    if (result.success == true) {
                        $("#Excel_FileData").val('');
                        swal.fire("Uploaded Successfully").then(function () {

                            var m = $('.bd-example-modal-lg');
                            m.modal('hide');
                            onChange();
                        });
                    }
                    else {
                        $("#Excel_FileData").val('');
                        if (result.message != null) {
                            $('#Unmatchedlist').html('')
                            swal.fire(result.message);
                        }
                        else {
                            $('#Unmatchedlist').html(result)
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

