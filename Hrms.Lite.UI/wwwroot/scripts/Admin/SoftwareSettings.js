function SoftwareSettingsQuerySave() {
    debugger;

    if (validateQuerySave())
    {
        swal.fire({
            text: "Do you want to Execute?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({

                    type: 'POST',
                    url: '/Admin/SoftwareSettings/Save',
                    dataType: 'json',
                    data: $('#QueryUpload').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                              
                                //var url = "/Admin/SoftwareSettings/GetRenewalsCategoryList?TabIndex=" + currentTab;
                                //$('#ActiveList').load(url, function () { });
                                url = "/Admin/SoftwareSettings/Index";
                                window.location.href = url;
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
    {
        return false;
    }

   
}


function validateQuerySave() {
    var i = 0;
    if (!validateById('#QueryMessage', 'qrymsg')) {
        i++;
    }
 
    if (i == 0)
        return true;
    else
        return false;
}
