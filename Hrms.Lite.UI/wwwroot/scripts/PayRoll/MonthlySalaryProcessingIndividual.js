function validateEmployee(val, divId, id) {
    debugger;
    if (val.value == "") {
        $("." + divId + "").addClass('is-invalid');
        $("#" + id + "").addClass('error-message');
        $("#" + id + "").html("This field is required");
        return false;
    }
    else {
        $("." + divId + "").removeClass('is-invalid');
        $("#" + id + "").removeClass('error-message');
        $("#" + id + "").html("");
        return true;
    }
}
function fillEmployeeDropdown() {

    debugger
    var status = $("input[id='EmployeeStatus']:checked").val();
/*  var a=  $('#EmployeeStatus').val();*/
    
    $.getJSON("/PayRoll/MonthlySalaryProcessingIndividual/GetEmployeeDropdown", { Month: $("#PayrollMonth_Code").val(), Empstatus: status }, function (data) {
        console.log(data);
        $('#Employee_Code option').remove();
        $('#Employee_Code').append('<option value>Search by Employee name/ID</option');
        debugger;
        for (var i = 0; i < data.length; i++) {
            $('#Employee_Code').append('<option value="' + data[i].code + '">' + data[i].name + '</option');
        }
        $('.selectpicker').selectpicker('refresh');

    });

}
function onProcessClick() {   
    var i = 0;
    
    var monthCode = $('#PayrollMonth_Code').val();
    var EmployeeCode = $('#Employee_Code').val();
    if (!validateById('#PayrollMonth_Code', 'payrollM')) {
        i++;
    }
    if (!validateEmployee(document.getElementById('Employee_Code'), 'em', 'empC')) {
        i++;
    }
    if (i == 0) {
        $('#procBtn').prop('disabled', true);
    $.ajax({
        type: 'GET',
        url: "/PayRoll/MonthlySalaryProcessingIndividual/GetIndividual_SalaryProcessingValidations",
        dataType: 'html',
        data: { SalaryPeriodCode: monthCode, EmployeeCode: EmployeeCode },
        success: function (Data) {
            if ($($.parseHTML(Data)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                debugger;
                var m = $('.bd-example-modal-lg');
                m.find('.modal-body').html('');
                m.find('.modal-body').html(Data);
                
                if ($("#Validation_Msg_FLAG").val() == 0) {
                    if ($('#Statutory_Count').val() > 0) {
                        $('#monthSpan').html($('#PayrollMonth_Code option:selected').text());
                        $('#empSpan').html($('#Employee_Code option:selected').text().split('  -')[0]);
                        m.modal('show');
                    }
                    else {
                        //MonthlySalaryProcessingIndividual($('#PayrollMonth_Code').val(), $('#Employee_Code').val(), $('#Employee_GI').val());
                        $('#procBtn').prop('disabled', false);
                        $.ajax({

                            type: 'POST',
                            url: '/PayRoll/MonthlySalaryProcessingIndividual/GetIndividual_SalaryProcessing',                            
                            data: $('#Individual_SalaryProcessing').serialize(),
                            success: function (result) {
                                
                                if (result.success == true) {
                                    swal.fire(result.message).then(function () {
                                        $('#mName').html($('#PayrollMonth_Code option:selected').text());
                                        MonthlySalaryProcessingIndividual($('#PayrollMonth_Code').val(), $('#Employee_Code').val(), $('#Employee_GI').val());
                                        var m = $('.bd-example-modal-lg');
                                        m.find('.modal-body').html('');
                                        m.modal('hide');
                                        $('#procBtn').prop('disabled', true);
                                    });
                                }
                                else if (result.message == null) {
                                    debugger;
                                    var m = $('.negativeSalaryPopUp');
                                    m.find('.modal-body').html('');
                                    m.find('.modal-body').html(result);
                                    m.modal('show');


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
                }
                else {
                    swal.fire($("#Validation_Msg_Description").val());
                    $('#procBtn').prop('disabled', false);
                }
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

function onOkClick() {
    swal.fire({
        text: "Do you want to Process?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({

                type: 'POST',
                url: '/PayRoll/MonthlySalaryProcessingIndividual/GetIndividual_SalaryProcessing',
                dataType: 'json',
                data: $('#Individual_SalaryProcessing').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            $('#mName').html($('#PayrollMonth_Code option:selected').text());
                            MonthlySalaryProcessingIndividual($('#PayrollMonth_Code').val(), $('#Employee_Code').val(), $('#Employee_GI').val());
                            var m = $('.bd-example-modal-lg');
                            m.find('.modal-body').html('');
                            m.modal('hide');
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

function MonthlySalaryProcessingIndividual(monthCode, EmployeeCode, EmployeeGI) {
    $.ajax({
        type: 'GET',
        url: "/PayRoll/MonthlySalaryProcessingIndividual/Individual_SalaryProcessing_Informations",
        dataType: 'html',
        data: { Month: monthCode, EmployeeCode: EmployeeCode, EmployeeGI: EmployeeGI },
        success: function (Data) {
            if ($($.parseHTML(Data)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $("#listView").html("");
                $("#listView").html(Data);               
                
                $('#mName').html($('#PayrollMonth_Code option:selected').text());
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);

        }
    });
}