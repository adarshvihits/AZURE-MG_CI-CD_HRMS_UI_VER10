function onEmpClick(PreEnrollGI) {
    debugger

    //var url = '/Recruitment/JoinConfirmEmpActivation/GetConfirmDetails';
    //$('body').append('<div class="form-overlay"></div>');
  
    //$('#joinConfirmslider').load(url, { PreEnrollGI: PreEnrollGI }, function () { });


    $.ajax({

        type: 'GET',
        url: '/Recruitment/JoinConfirmEmpActivation/GetConfirmDetails',
        dataType: 'html',
        data: { PreEnrollGI: PreEnrollGI },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#joinConfirmslider').html("");
                $('#joinConfirmslider').html(result);
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

function joinConfirm(GI) {
    debugger

    swal.fire({
        text: "Do you Want to Confirm?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({

                type: 'POST',
                url: '/Recruitment/JoinConfirmEmpActivation/Approve',
                dataType: 'json',
                data: $("#_joinConfirm").serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            var url = "/Recruitment/JoinConfirmEmpActivation/PreEnrollJoiningConfirm";
                            window.location.href = url;
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {

                        });
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
function onExportToExcelClick() {
    debugger
    var TableLength = $('#OnBoardingList_Count').val();
    if (TableLength > 0) {
        url = "/Recruitment/JoinConfirmEmpActivation/ExportToExcel";
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}