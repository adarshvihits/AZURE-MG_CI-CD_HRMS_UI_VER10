


function GetList() {
    debugger;
    var date = $('#ProcesingDate').val();
    var empCode = $('#EmpId').val();

    if (validatedateandemp()) {

        $.ajax({

            type: 'GET',
            url: '/TimeOffice/DailyAttendanceProcessingIndividual/GetList?date=' + date + "&empCode=" + empCode,
            dataType: 'html',
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }

                else {

                    $('#IndAttnProccListId').html("");
                    $('#IndAttnProccListId').html(result);
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
    else
        return false;
}
function validateEmployee(val, divId, id) {
    debugger;
    if (val.value == "") {
        $("." + divId + "").addClass('is-invalid');
        $("#" + id + "").addClass('error-message');
        $("#" + id + "").html("This field is required");
    }
    else {
        $("." + divId + "").removeClass('is-invalid');
        $("#" + id + "").removeClass('error-message');
        $("#" + id + "").html("");
    }
}












function validatedateandemp() {
    var i = 0
    if (!validateById('#ProcesingDate', 'att_process_date'))
        i++;

    if ($("#EmpId").val() == "") {
        $('.emp_id').addClass('is-invalid');
        $("#empid").addClass('error-message');
        $("#empid").html("This field is required");
        $('#EmpId').focus();
        i++;
    }

    if (i == 0)
        return true;
    else
        return false;
}
function ExportToExcel() {

    debugger;
    var date = $('#ProcesingDate').val();
    var empCode = $('#EmpId').val();

    var tableLength = $('#DailyAttendanceProcessing_Count').val();
    if (tableLength > 0) {
        url = "/TimeOffice/DailyAttendanceProcessingIndividual/ExportToExcel?date="+ date + "&empCode=" + empCode;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }

}
