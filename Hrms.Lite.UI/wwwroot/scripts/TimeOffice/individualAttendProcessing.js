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
function fillEmployeeDropdown()
{
    var status = $("input[type='radio']:checked").val();
    debugger
    $.getJSON("/TimeOffice/IndividualAttendanceProcessing/GetEmployeeDropdown", { Month:$("#Month_Code").val(),Empstatus: status  }, function (data) {
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
    debugger;
    var i = 0;

    var monthCode = $('#Month_Code').val();
    var EmployeeCode = $('#Employee_Code').val();
    
    if (!validateById('#Month_Code', 'ip_Month')) {
        i++;
    }
    if (!validateEmployee(document.getElementById('Employee_Code'), 'ip_Emp', 'ip_Empspan')) {
        i++;
    }
    if (i == 0) {

        $.ajax({
            type: 'GET',
            url: "/TimeOffice/IndividualAttendanceProcessing/GetIndividual_AttendanceProcessingValidations",
            dataType: 'html',
            data: { Month: monthCode, EmployeeCode: EmployeeCode },
            success: function (Data) {
                if ($($.parseHTML(Data)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {                    
                    var m = $('.individual_Attendance_Popup');
                    m.find('.modal-body').html('');
                    m.find('.modal-body').html(Data); 
                    var Employee_GI=$("#Employee_GI").val()
                    if ($("#Validation_Msg_FLAG").val() == 0) {
                        if ($("#MonthlyAttendanceProcessing_Validations_Count").val() == 0) {
                            onOkClick(monthCode, EmployeeCode, Employee_GI);
                        }
                        m.modal('show');
                        $('#monthSpan').html($('#Month_Code option:selected').text());
                        $('#empSpan').html($('#Employee_Code option:selected').text().split(' (')[0]);

                    }
                        else {
                          
                            swal.fire($("#Validation_Msg_Description").val());
                            return false;
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
function AttendanceProcessingList(monthCode, EmployeeCode, EmployeeGI, result) {
    $.ajax({
        type: 'GET',
        url: "/TimeOffice/IndividualAttendanceProcessing/Individual_MonthlyAttendanceProcessingList",
        dataType: 'html',
        data: { Month: monthCode, EmployeeCode: EmployeeCode, EmployeeGI: EmployeeGI },
        success: function (Data) {
            if ($($.parseHTML(Data)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $("#iap_processing_List").html("");
                $("#iap_processing_List").html(Data);
                $(".individual_Attendance_Process_EmpHeader").removeClass('hide');

                $('#processingResponse').html("Processed Successfully");
                $('#monthSpan').html($('#Month_Code option:selected').text());
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);

        }
    });
}
function onOkClick(monthCode, EmployeeCode, EmployeeGI) {
    swal.fire({
        text: "Do you want to Process?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            showSpinner();
            $.ajax({

                type: 'POST',
                url: '/TimeOffice/IndividualAttendanceProcessing/Process',
                dataType: 'json',
                data: $('#Individual_AttendanceProcessing').serialize(),
                success: function (result) {

                    if (result.success == true) {                       
                        debugger
                        var m = $('.individual_Attendance_Popup');                
                        m.modal('hide');
                        debugger            
                        AttendanceProcessingList(monthCode, EmployeeCode, EmployeeGI,result);

                    }                
                        hideSpinner();

                },
                error: function (xhr, textStatus, errorThrown) {
                    hideSpinner();
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

