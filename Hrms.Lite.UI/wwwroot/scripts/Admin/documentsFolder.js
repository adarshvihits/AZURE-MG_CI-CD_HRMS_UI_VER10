function GetList() {
    debugger
    var CategoryCode = $('#CategoryDropdown').val();
    $.ajax({
            type: 'Get',
            url: '/Admin/DocumentsFolder/GetList?Code=' + CategoryCode,
            dataType: 'html',
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('#DocumentsList').html("");
                    $('#DocumentsList').html(result);
                    }
            },
            error: function (xhr, textStatus, errorThrown) {
                debugger
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);
            }
        });
}

function onAddNewOrEditOrDeleteClick(mode, DocCategoryCode, DocGI) {
    debugger;
    $.ajax
        ({
            type: 'GET',
            url: '/Admin/DocumentsFolder/CreateOrEdit',
            dataType: 'html',
            data: { Mode: mode, DocCategoryCode: DocCategoryCode, DocGI: DocGI },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    debugger
                    $('body').append('<div class="form-overlay"></div>');
                    $('#newRequestSlider').html("");
                    $('#newRequestSlider').html(result);
                    debugger
                    if (mode == 'Create')
                        $('.document_categoryparameter_lists').addClass('hide');
                    }
            },

            error: function (xhr, textStatus, errorThrown) {
                debugger
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);

            }
     });
}

function DocSave(mode) {
    debugger;

    if (validateDocumentFolder()) {

        if ($('#File_FileData').val() == '' && $('#File_AbsoluteUri').val() == '') {

            swal.fire("Please upload the document");
        }
        else {
            swal.fire
                ({
                    text: "Do you want to " + mode + "?",
                    icon: 'warning',
                    showCancelButton: true,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33',
                    confirmButtonText: 'Ok'
                }).then((result) => {
                    if (result.isConfirmed) {
                        debugger;
                        var data = new FormData($('#documentsFolderSave')[0]);
                        $.ajax
                            ({
                                type: 'POST',
                                url: '/Admin/DocumentsFolder/DocumentSave',
                                contentType: false,
                                processData: false,
                                dataType: 'json',
                                data: data,

                                success: function (result) {
                                    if (result.success == true) {
                                        swal.fire(result.message).then(function () {
                                            CloseSlider('documentsFolderSaveclose');
                                            var url = "/Admin/DocumentsFolder/Index";
                                            window.location.href = url;
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
    }
    else {
        return false;
    }
}

function OnCategorySelect() {
    debugger
    var CategoryCode = $('#CategoryDropdown_Code').val();
    
    $.ajax({

        type: 'GET',
        url: '/Admin/DocumentsFolder/GetDocCategoryDetails' ,
        dataType: 'html',
        data: { DocCategoryCode: CategoryCode },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#ParametersListDiv').html("");
                $('#ParametersListDiv').html(result);
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
        }
    });
}

function CheckImageExtensionSize(e) {
    debugger;
    var size = $(e)[0].files[0].size;
    var filename = $(e)[0].files[0].name;
    var fileExtension = ['pdf', 'jpeg', 'jpg', 'png', 'bmp'];
    if (size > 1024 * 1024) {
        swal.fire('File size cannot exceeds 1 Mb');
        $(e).val('');
        return false;
    }
    else if ($.inArray($(e).val().split('.').pop().toLowerCase(), fileExtension) == -1) {
        swal.fire("Only pdf, jpg, jpeg, png and bmp formats are allowed");
        $(e).val('');
        return false;
    }
    else {
        return true;
    }

}

function confirmDelete(Mode, DoccategoryCode, DocGI) {
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
            debugger;
            $.ajax({
                type: 'POST',
                url: '/Admin/DocumentsFolder/Delete',
                dataType: 'html',
                data: { DocumentCategoryCode: DoccategoryCode, DocGI: DocGI},
                dataType: 'json',
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('documentsFolderSaveclose');
                            var url = "/Admin/DocumentsFolder/Index";
                            window.location.href = url;

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

function validateDocumentFolder() {
    debugger;
    var i = 0;
    if (!validateById('#CategoryDropdown_Code', 'DocumentCategoryDD')) {
        i++;
    }
    if (!validateById('#DocumentName', 'DocumentNameSpan')) {
        i++;
    }
    if (!validateById('#Description', 'DescriptionSpan')) {
        i++;
    }

    if (i == 0)
        return true;
    else
        return false;
}
