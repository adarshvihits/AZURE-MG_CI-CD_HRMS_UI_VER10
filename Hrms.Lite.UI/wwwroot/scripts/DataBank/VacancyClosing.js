function ClickOnJobTitle(GI, staus) {
    debugger;

    if (staus = 'C') {
        $.ajax({

            type: 'GET',
            url: '/DataBank/VacancyClosing/GetVacancyClosingDetails',
            dataType: 'html',
            data: { VacancyManagementGI: GI },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('#slider').html("");
                    $('#slider').html(result);
                }
            },


            error: function (xhr, textStatus, errorThrown) {
                debugger
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);
                hideSpinner();

            }
        });
    }
    else
        swal.fire('Vacancy Closed');
}



function closevacancy() {
    debugger;   

        swal.fire({
            text: "Do you want to Close?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({

                    type: 'POST',
                    url: '/DataBank/VacancyClosing/VacancyClose',
                    dataType: 'json',
                    data: $('#vacancycloseform').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                CloseSlider('closeslider');
                                debugger;
                                var url = "/DataBank/VacancyClosing/GetList";
                                $('#closelist').load(url, {}, function () { });

                            });

                        }

                        else {
                            swal.fire(result.message);  }

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
    var CancellationTable = $('#VacancyStatusReport_Count').val();
    if (CancellationTable > 0) {
        url = "/DataBank/VacancyClosing/ExportToExcel";
        window.location.href = url;

    }
    else {
        swal.fire("No data found to export");
    }
}