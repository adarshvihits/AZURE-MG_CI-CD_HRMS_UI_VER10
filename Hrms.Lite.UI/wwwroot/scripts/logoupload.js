function uploadsvglogo()
{
    debugger;
    $.ajax({

        type: 'POST',
        url: '/customizesoftware/uploadsoftwarelogo',
        dataType: 'json',
        data: $('#CustomizeSoftware').serialize(),
        success: function (result) {
            if (result.success == true) {
                swal.fire(result.message).then(function ()
                {
                    

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

function uploadjpglogo()
{
    debugger;
    data = new FormData($('#uploadLogo')[0]);
    $.ajax({

        type: 'POST',
        url: '/customizesoftware/UploadJpgSoftwareLogo',
        contentType: false,
        processData: false,
        dataType: 'json',
        data: data,
        success: function (result) {
            if (result.success == true) {
                swal.fire(result.message).then(function () {

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
            else
            {
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);
            }
        }
    });
}