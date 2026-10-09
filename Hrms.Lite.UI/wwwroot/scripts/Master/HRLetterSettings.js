////function Save() {
////    debugger;
////    var type = $('#LetterType').val();
////    if (validateHRLetterSettings()) {
////        swal.fire({
////            text: "Do you want to Save?",
////            icon: 'warning',
////            showCancelButton: true,
////            confirmButtonColor: '#3085d6',
////            cancelButtonColor: '#d33',
////            confirmButtonText: 'Ok'
////        }).then((result) => {
////            if (result.isConfirmed) {
////                $.ajax({

////                    type: 'POST',
////                    url: '/Master/HRLetterSettings/Save',
////                    dataType: 'json',
////                    data: $('#HRLetterSettings').serialize() + "&LetterType=" + type,
////                    success: function (result) {
////                        if (result.success == true) {
////                            swal.fire(result.message).then(function () {
////                                url = "/Master/HRLetterSettings/Index";
////                                window.location.href = url;
////                            });

////                        }

////                        else {
////                            swal.fire(result.message).then(function () {

////                            });
////                        }

////                    },
////                    error: function (xhr, textStatus, errorThrown) {
////                        if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
////                            window.location.reload();
////                        }

////                        else {
////                            document.body.id = "kt_body";
////                            $('body').html(xhr.responseText);
////                        }
////                    }
////                });
////            }
////            else {
////                return false;
////            }
////        })
////    }
////    else {
////        return false;
////    }
////}


function GetDetails() {
    debugger
    var LetterType = $('#LetterType').val();

    $.ajax({

        type: 'GET',
        url: '/Master/HRLetterSettings/GetHRLetterSettingsDetails',
        dataType: 'html',
        data: { LetterType: LetterType},
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                

                $('#List').html("");
                $('#List').html(result);
               
            }


        },


        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);


        }
    });
}






function Save() {
    debugger
    var LetterType = $('#LetterType').val();

    if (validateHRLetterSettings()) {
        console.log($('#HRLetterSettings').serialize());
         data = new FormData($('#HRLetterSettings')[0]);
        debugger;
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
                    url: '/Master/HRLetterSettings/Save?LetterType=' + LetterType,
                    contentType: false,
                    processData: false,
                    dataType: 'json',
                    data: data ,
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire("Saved Successfully").then(function () {
                                

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
        });
    }
}









function validateHRLetterSettings() {
    var i = 0;
    if (!validateById('#LetterType', 'Lettype')) {
        i++;
    }
    if (!validateById('#SigName', 'SigNamespan')) {
        i++;
    }
    if (!validateById('#SigDesignation', 'SigDesignationspan')) {
        i++;
    }
    if (!validateById('#CompanyName', 'CompanyNamespan')) {
        i++;
    }
    if (!validateById('#Attachments', 'Attachments')) {
        i++;
    }
    if (!validateById('#IndexNum', 'IndexNumspan')) {
        i++;
    }
    //if (!validateById('#Attachment2', 'Attachment2')) {
    //    i++;
    //}
    //if (!validateById('#Attachment1', 'Attachment1')) {
    //    i++;
    //}

    if (i == 0)
        return true;
    else
        return false;
}





function Preview() {
    debugger
    if ($('#Attachment1_FileData').val() == '' && $('#Attachment1_AbsoluteUri').val() == '') {
        swal.fire("No file uploaded");
    }

    else if ($('#Attachment1_FileData').val() != '') {
        var path = URL.createObjectURL($('.PreviewImage')[0].files[0]);
        window.open(path, "_blank");
    }
    else {
        window.open($('#Attachment1_AbsoluteUri').val(), '_blank');
    }
}
