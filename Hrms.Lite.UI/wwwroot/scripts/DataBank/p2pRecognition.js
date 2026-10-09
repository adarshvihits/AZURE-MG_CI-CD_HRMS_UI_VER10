function Getp2pRecognitionlist() {
    var radiomode = $('input[name="radiomode"]:checked').val();
    $.ajax({

        type: 'GET',
        url: '/DataBank/p2pRecognition/GetList',
        dataType: 'html',
        data: { radiomode: radiomode },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#p2pRecognitionList').html("");
                $('#p2pRecognitionList').html(result);
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


function onEditClick(EmployeeGI, RewardCode) {
    var radiomode = $('input[name="radiomode"]:checked').val();
    debugger;
    $.ajax({
        type: 'GET',
        url: '/DataBank/p2pRecognition/GetDetails',
        dataType: 'html',
        data: { EmployeeGI: EmployeeGI, RewardCode: RewardCode, radiomode: radiomode },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('body').append('<div class="form-overlay"></div>');
                $('#p2pRecognitionSlider').html("");
                $('#p2pRecognitionSlider').html(result);
            }


        },
        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);


        }
    });
}


function Recognitionsave() {
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
        url: '/DataBank/p2pRecognition/Save',
        dataType: 'json',
        data: $('#RecognitionSlider').serialize(),
        success: function (result) {
            if (result.success == true) {
                swal.fire(result.message).then(function () {
                    CloseSlider('Recognition');
                    var url = "/DataBank/p2pRecognition/GetList?radiomode=" + radiomode;
                    $('p2pRecognitionList').load(url, function () { });
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



function onPointCriteriaChange(sender, spanId) {
    debugger;
    var code = $(sender).val();
    if (code == 0) {
        $("#" + spanId + "").addClass('error-message');
        $(sender).addClass('is-invalid');
        $("#" + spanId + "").html("This field is required");
    }
    else {
        $("#" + spanId + "").removeClass('error-message');
        $(sender).removeClass('is-invalid');
        $("#" + spanId + "").html("");

        $.getJSON("/DataBank/p2pRecognition/GetPoints", { PointCriteriaCode: code }, function (data) {
            debugger;
            console.log(data);
            $('.Points').html(data);
        });
    }
}



function onRewardChange(sender, spanId) {
    debugger
    var code = $(sender).val();
    if (code == 0) {
        $("#" + spanId + "").addClass('error-message');
        $(sender).addClass('is-invalid');
        $("#" + spanId + "").html("This field is required");
    }
    else {
        $("#" + spanId + "").removeClass('error-message');
        $(sender).removeClass('is-invalid');
        $("#" + spanId + "").html("");
        $.getJSON("/DataBank/p2pRecognition/GetPointCriteria", { RewardCode: code }, function (data) {
            console.log(data);
            $('#PointCriteria_Code option').remove();
            $('#PointCriteria_Code').append('<option value>Select Point Criteria</option');
            debugger;
            for (var i = 0; i < data.length; i++) {
                $('#PointCriteria_Code').append('<option value="' + data[i].code + '">' + data[i].name + '</option');
            }

        });
    }
}
