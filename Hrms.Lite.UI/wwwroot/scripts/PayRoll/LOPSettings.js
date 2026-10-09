function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode != 46 && charCode > 31
        && (charCode < 48 || charCode > 57))
        return false;

    return true;
}


function OnSaveClick() {
    debugger
    var table = $('#allowancesettings').DataTable();
    var data = table.$('input').serializeArray();
    //console.log(data)
/*    $('#HR_Atten_Approval_Form').serialize(),*/

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
                url: '/PayRoll/LOPSettings/SaveLOPSettings',
                dataType: 'json',
                data: data,
                success: function (result) {
                    debugger;
                    if (result.success == true) {
                        swal.fire("Saved Successfully").then(function () {

                            window.location.href = "/PayRoll/LOPSettings/Index";


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



function EnableDisableTextLOP(index) {
    debugger;

    if ($("#LOPSettingsList_" + index + "__LopStatus").is(":checked") == true) {

        debugger;
        $("#LOPSettingsList_" + index + "__LopPercent").prop("readonly", false)
        $("#LOPSettingsList_" + index + "__LopPercent").focus();

        $("#LOPSettingsList_" + index + "__LopPercent").val('100.0');

    }
    else {
        debugger;
        $("#LOPSettingsList_" + index + "__LopPercent").val('0.00');
        $("#LOPSettingsList_" + index + "__LopPercent").prop("readonly", true)
     
    }
}

function EnableDisableTextESI(index) {
    debugger;

    if ($("#LOPSettingsList_" + index + "__EsiLeaveStatus").is(":checked") == true) {


        $("#LOPSettingsList_" + index + "__EsiLeavePercent").prop("readonly", false)
        $("#LOPSettingsList_" + index + "__EsiLeavePercent").focus();
        $("#LOPSettingsList_" + index + "__EsiLeavePercent").val('100.0');
    }
    else {
        $("#LOPSettingsList_" + index + "__EsiLeavePercent").val('0.00');
        $("#LOPSettingsList_" + index + "__EsiLeavePercent").prop("readonly", true)
    }

}

function EnableDisableTextLATEHour(index) {
    debugger;

    if ($("#LOPSettingsList_" + index + "__LateHourStatus").is(":checked") == true) {


        $("#LOPSettingsList_" + index + "__LateHourPercent").prop("readonly", false)
        $("#LOPSettingsList_" + index + "__LateHourPercent").focus();
        $("#LOPSettingsList_" + index + "__LateHourPercent").val('100.0');
    }
    else {
        $("#LOPSettingsList_" + index + "__LateHourPercent").val('0.00');
        $("#LOPSettingsList_" + index + "__LateHourPercent").prop("readonly", true)
    }
}

function EnableDisableTextSuspension(index) {
    debugger;

    if ($("#LOPSettingsList_" + index + "__SuspensionStatus").is(":checked") == true) {


        $("#LOPSettingsList_" + index + "__SuspensionPercent").prop("readonly", false)
        $("#LOPSettingsList_" + index + "__SuspensionPercent").focus();
        $("#LOPSettingsList_" + index + "__SuspensionPercent").val('100.0');
    }
    else {
        $("#LOPSettingsList_" + index + "__SuspensionPercent").val('0.00');
        $("#LOPSettingsList_" + index + "__SuspensionPercent").prop("readonly", true)
    }
}

function EnableDisabletextLayOff(index) {
    debugger;

    if ($("#LOPSettingsList_" + index + "__LayOffStatus").is(":checked") == true) {


        $("#LOPSettingsList_" + index + "__LayOffPercent").prop("readonly", false)
        $("#LOPSettingsList_" + index + "__LayOffPercent").focus();
        $("#LOPSettingsList_" + index + "__LayOffPercent").val('100.0');

    }
    else {
        $("#LOPSettingsList_" + index + "__LayOffPercent").val('00.0');
        $("#LOPSettingsList_" + index + "__LayOffPercent").prop("readonly", true)

    }
}
function EnableDisabletextMaternityLeave(index) {
    debugger;

    if ($("#LOPSettingsList_" + index + "__MaternityLeaveStatus").is(":checked") == true) {


        $("#LOPSettingsList_" + index + "__MaternityLeavePercent").prop("readonly", false)
        $("#LOPSettingsList_" + index + "__MaternityLeavePercent").focus();
        $("#LOPSettingsList_" + index + "__MaternityLeavePercent").val('100.0');

    }
    else {
        $("#LOPSettingsList_" + index + "__MaternityLeavePercent").val('00.0');
        $("#LOPSettingsList_" + index + "__MaternityLeavePercent").prop("readonly", true)

    }

}