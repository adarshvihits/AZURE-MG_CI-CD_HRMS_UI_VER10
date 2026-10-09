function onAddClick() {
    var url = "/Admin/PhotoGallery/Createold";
        window.location.href = url;
}
function onAddNewClick() {
    //var url = "/Admin/PhotoGallery/Create";
    //    window.location.href = url;

    var url = "/Admin/DocumentRenewal/Create";
    $.ajax({

        type: 'GET',
        url: '/Admin/PhotoGallery/Create',
        dataType: 'html',
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

function RemoveImg() {
    $('#imgPreviewDiv').addClass('hide');
    $('#imgPreview').attr('src', '');
    $('#file_FileData').val('');
}

function previewImgPhotoGallery() {
    debugger;

    try {
        var path = URL.createObjectURL($('.uploadImg')[0].files[0]);
        window.open(path, "_blank");
    }
    catch (err) {
        if (absoluteURI != '' && absoluteURI != undefined)
            window.open(absoluteURI, "_blank");
        else
            swal.fire("No file uploaded");
    }

}
function Save() {
    debugger;
    var i = 0;
    var fileInput = document.getElementById('fileInput');
    var files = fileInput.files;
    
    if (files.length == 0) {
        swal.fire("No file uploaded");
        return false; // Prevent form submission
    }
    if (!validateById('#Description', 'description')) {
        i++;
    }
    //let totalSize = 0; // Initialize total size to 0

    //for (let i = 0; i < files.length; i++) {
    //    totalSize += files[i].size; // Add the size of each file to totalSize
    //}

    //const totalSizeInMB = (totalSize / (1024 * 1024)).toFixed(2); // Convert bytes to MB

    //alert(`Total uploaded size: ${totalSizeInMB} MB`);

    if (i == 0) {

        var data = new FormData($('#PhotoGallerysave')[0]);
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
                    url: '/Admin/PhotoGallery/Upload',
                    contentType: false,
                    processData: false,
                    dataType: 'json',
                    data: data,
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire("Saved Successfully").then(function () {

                                var url = "/Admin/PhotoGallery/Index" ;
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
function Edit(PhotoGalleryGI) {
    var url = "/Admin/PhotoGallery/Edit?PhotoGalleryGI=" + PhotoGalleryGI;
    window.location.href = url;
   
}

function GetDetails(PhotoGalleryGI) {
    $.ajax({

        type: 'GET',
        url: '/Admin/PhotoGallery/GetDetails',
        dataType: 'html',
        data: { PhotoGalleryGI: PhotoGalleryGI },
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
function Delete(PhotoGalleryGI) {
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

                type: 'GET',
                url: '/Admin/PhotoGallery/Delete',
                dataType: 'html',
                data: { PhotoGalleryGI: PhotoGalleryGI },
                success: function (result) {
                        if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }
                    else {
                        swal.fire("Deleted Successfully");
                            var url = "/Admin/PhotoGallery/Index";
                            window.location.href = url;
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
            return false;
        }
    })

}

function EditSave() {
    debugger;
    var i = 0;
    var fileInput = document.getElementById('fileInput');
    var files = fileInput.files;

    if (files.length == 0) {
        swal.fire("No file uploaded");
        return false; // Prevent form submission
    }
    if (!validateById('#Description', 'description')) {
        i++;
    }
    if (i == 0) {

        var data = new FormData($('#PhotoGallerysave')[0]);
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
                    url: '/Admin/PhotoGallery/Edit',
                    contentType: false,
                    processData: false,
                    dataType: 'json',
                    data: data,
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire("Saved Successfully").then(function () {

                                var url = "/Admin/PhotoGallery/Index";
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