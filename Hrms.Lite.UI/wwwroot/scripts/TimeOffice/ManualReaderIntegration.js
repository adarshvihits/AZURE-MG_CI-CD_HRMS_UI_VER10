
function Download() {
    debugger;
  
        swal.fire({
            text: "Do you want to download?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({

                    type: 'POST',
                    url: '/TimeOffice/ManualReaderIntegration/ManualReaderIntegration',
                   
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                               
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