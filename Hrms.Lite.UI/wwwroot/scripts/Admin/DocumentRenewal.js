


function onAddNewOrEditClick(Type, GI, LogGI, index) {
    debugger;


    if (Type == 'MODIFY') {

            $.ajax({

                type: 'GET',
                url: '/Admin/DocumentRenewal/GetDocumentRenewalDetails',
                dataType: 'html',
                data: { DocumentRenewalGI: GI},
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
        var url = "/Admin/DocumentRenewal/Create";
        $.ajax({

            type: 'GET',
            url: '/Admin/DocumentRenewal/Create',
            dataType: 'html',
            data: { DocumentRenewalGI: GI },
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




function onDeleteClick(GI, LogGI, Type, index) {

    debugger;

 

        $.ajax({
            type: 'GET',
            url: '/Admin/DocumentRenewal/Delete',
            dataType: 'html',
            data: { DocumentRenewalGI: GI },
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


function validateDOC() {
    var i = 0;
    if (!validateById('#DocumentName', 'documentName')) {
        i++;
    }
    if (!validateById('#RenewalsCategory_Code', 'renewalsCategory')) {
        i++;
    }
    if (!validateById('#RenewalDate', 'renewalDate')) {
        i++;
    }
    if (i == 0)
        return true;
    else
        return false;
}




function Save(mode) {
    debugger;
    if (validateDOC()) {
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
                    url: '/Admin/DocumentRenewal/Save',
                    dataType: 'json',
                    data: $('#DocumentRenewalsave').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                CloseSlider('AddNewDiv');
                                var url = "/Admin/DocumentRenewal/GetDocumentRenewalList";
                                $('#BankList').load(url, function () { });


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
    else {
        return false;
    }
}

function confirmDelete(GI, LogGI) {

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
                url: '/Admin/DocumentRenewal/Delete',
                dataType: 'json',
                data: $('#DocumentRenewalDelete').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('Delete');
                            var url = "/Admin/DocumentRenewal/GetDocumentRenewalList";
                            $('#BankList').load(url, function () { });
                           
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Admin/DocumentRenewal/Delete?DocumentRenewalGI=" + GI ;
                            $('#DeleteSlider').load(url, function () { });
                            $('body').append('<div class="form-overlay"></div>');

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


function ExportToExcel() {

    debugger;
    var pendingTableLength = $('#DocumentRenewalList_Count').val();
        if (pendingTableLength > 0) {
            url = "/Admin/DocumentRenewal/ExportToExcel" ;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
     

   
}
