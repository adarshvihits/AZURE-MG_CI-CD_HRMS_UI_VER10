function GetList() {
    debugger;
    showSpinner();
    var month = $('#Month_Code').val();
    $.ajax({
        type: 'GET',
        url: '/Master/MonthlyProfitBasedSettings/GetList',
        dataType: 'html',
        data: { Month: month },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#ProfitBasedList').html("");
                $('#ProfitBasedList').html(result);
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
function validateMonthDropdown() {
    var i = 0;

    debugger;
    if (!validateById('#Month_Code', 'MonthDD')) {
        i++;
    }
    if (i == 0) {
        var m = $('.bd-example-modal-lg');
        m.modal('show');
    }

}
function onImportExcelClick() {

    debugger;

    swal.fire({
        text: "Do you want to Upload?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            showSpinner();
            var ProfitUploadInfo = new FormData($('#ProfitUploadData')[0]);
            $("#Excel_FileData").val('');
            $.ajax({

                type: 'POST',
                url: '/Master/MonthlyProfitBasedSettings/ImportFromExcel',
                contentType: false,
                processData: false,
                data: ProfitUploadInfo,
                success: function (result) {
                    debugger;
                    if (result.success == true) {
                        $("#Excel_FileData").val('');
                        swal.fire("Uploaded Successfully").then(function () {
                            GetList();
                            var m = $('.bd-example-modal-lg');
                            m.modal('hide');
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
                    hideSpinner();
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
function ExportToExcel() {
    debugger;
    var month = $('#Month_Code').val();
    var tableLength = $('#MonthlyProfitBasedSettingsList_Count').val();
    if (tableLength > 0) {
        url = "/Master/MonthlyProfitBasedSettings/ExportToExcel?Month=" + month;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
}