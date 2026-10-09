function GetIncidentlist() {
    var radiomode = $('input[name="radiomode"]:checked').val();
    $.ajax({

        type: 'GET',
        url: '/DataBank/IncidentReport/GetList',
        dataType: 'html',
        data: { radiomode: radiomode },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#IncidentReportList').html("");
                $('#IncidentReportList').html(result);
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


function onEditClick(EmployeeGI, IncidentCode) {
    var radiomode = $('input[name="radiomode"]:checked').val();
    debugger;
    $.ajax({
        type: 'GET',
        url: '/DataBank/IncidentReport/GetDetails',
        dataType: 'html',
        data: { EmployeeGI: EmployeeGI, IncidentCode: IncidentCode, radiomode: radiomode },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('body').append('<div class="form-overlay"></div>');
                $('#IncidentReportSlider').html("");
                $('#IncidentReportSlider').html(result);
            }


        },
        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);


        }
    });
}


function Incidentsave() {
    var radiomode = $('input[name="radiomode"]:checked').val();

    swal.fire({
        text: "Do you want to save?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {

    $.ajax({
        type: 'POST',
        url: '/DataBank/IncidentReport/Save',
        dataType: 'json',
        data: $('#IncidentSlider').serialize(),
        success: function (result) {
            if (result.success == true) {
                swal.fire(result.message).then(function () {
                    CloseSlider('Feedback');
                    var url = "/DataBank/IncidentReport/GetList?radiomode=" + radiomode;
                    $('IncidentReportList').load(url, function () { });
                });
            } else {
                swal.fire(result.message).then(function () {
                    // Optional: Add handling for unsuccessful cases here
                });
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            debugger;
            if ($($.parseHTML(xhr.responseText)).filter(".Login_Session").length > 0) {
                window.location.reload();
            } else {
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