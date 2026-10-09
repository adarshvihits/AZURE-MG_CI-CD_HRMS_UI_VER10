function enableDisableTextBoxes()
{
    debugger
    if (document.getElementById("Encashment_Appl").checked == false)
        $("#EncashmentMaxLimit").prop('readonly', true);
    if (document.getElementById("CarryForward_Appl").checked == false)
        $("#CarryForwardMaxLimit").prop('readonly', true);
    if (document.getElementById("ApplyProDataBasis").checked == false)
        $("#ProDataBasisNoOfLeaves").prop('readonly', true);
    if (document.getElementById("PreDatedApplAlwed").checked == false)
        $("#NoOfDaysPrior").prop('readonly', true);
    if (document.getElementById("Mandatory").checked == false)
        $("#NoOfLeavesForMandatoryDoc").prop('readonly', true);
    if (document.getElementById("AdvanceApplRequired").checked == false)
        $("#NoOfDaysAdvance").prop('readonly', true);
    if (document.getElementById("HolidaySandwichAppl").checked == false)
        $("#HolidaySandwitchNoOfLeaves").prop('readonly', true);
    if (document.getElementById("OffdaySandwichAppl").checked == false)
        $("#OffdaySandwitchNoOfLeaves").prop('readonly', true);

    if (document.getElementById("LeavelapsAppl").checked == false)
        $("#MandatoryTakenlaps").prop('readonly', true);

    if (document.getElementById("AdvleaveApplNecessary").checked == false)
        $("#AdvleaveApplNecessaryDays").prop('readonly', true);
}
function onEncashApplChange() {

    if ($('#Encashment_Appl').is(':checked'))
        $("#EncashmentMaxLimit").prop('readonly', false);
    else {
        $("#EncashmentMaxLimit").prop('readonly', true);
        $("#EncashmentMaxLimit").val('');
    }
}

function onLeaveLapsApplChange() {
    debugger;

    if ($('#LeavelapsAppl').is(':checked'))
        $("#MandatoryTakenlaps").prop('readonly', false);
    else {
        $("#MandatoryTakenlaps").prop('readonly', true);
        $("#MandatoryTakenlaps").val('');
    }
}


function onCFApplChange()
{

    if ($('#CarryForward_Appl').is(':checked'))
        $("#CarryForwardMaxLimit").prop('readonly', false);
    else {
        $("#CarryForwardMaxLimit").prop('readonly', true);
        $("#CarryForwardMaxLimit").val('');
    }
}


function onProDataChange() {

    if ($('#ApplyProDataBasis').is(':checked'))
        $("#ProDataBasisNoOfLeaves").prop('readonly', false);
    else {
        $("#ProDataBasisNoOfLeaves").prop('readonly', true);
        $("#ProDataBasisNoOfLeaves").val('');
    }
}
function onMandatoryDocApplChange() {
    debugger
    if ($('#Mandatory').is(':checked'))
        $("#NoOfLeavesForMandatoryDoc").prop('readonly', false);
    else {
        $("#NoOfLeavesForMandatoryDoc").prop('readonly', true);
        $("#NoOfLeavesForMandatoryDoc").val('');
    }
}
function onPreDatesApplChange() {

    if ($('#PreDatedApplAlwed').is(':checked'))
        $("#NoOfDaysPrior").prop('readonly', false);
    else {
        $("#NoOfDaysPrior").prop('readonly', true);
        $("#NoOfDaysPrior").val('');
    }
}

function onLeaveAdvanceChange() {

    if ($('#AdvanceApplRequired').is(':checked'))
        $("#NoOfDaysAdvance").prop('readonly', false);
    else {
        $("#NoOfDaysAdvance").prop('readonly', true);
        $("#NoOfDaysAdvance").val('');
    }
}
function onHoli_SandwichChange() {

    if ($('#HolidaySandwichAppl').is(':checked'))
        $("#HolidaySandwitchNoOfLeaves").prop('readonly', false);
    else {
        $("#HolidaySandwitchNoOfLeaves").prop('readonly', true);
        $("#HolidaySandwitchNoOfLeaves").val('');
    }
}
function onOffDay_SandwichChange() {

    if ($('#OffdaySandwichAppl').is(':checked'))
        $("#OffdaySandwitchNoOfLeaves").prop('readonly', false);
    else {
        $("#OffdaySandwitchNoOfLeaves").prop('readonly', true);
        $("#OffdaySandwitchNoOfLeaves").val('');
    }
}
function onAdvanceLeaveNecessarychange() {
    

    if ($('#AdvleaveApplNecessary').is(':checked')) {
        if ($('#PreDatedApplAlwed').is(':checked') == false && $('#AdvanceApplRequired').is(':checked') == false)
            $("#AdvleaveApplNecessaryDays").prop('readonly', false);
        else {
            swal.fire('Please uncheck Allow backdated applications and Advance Application required fields');
            $('#AdvleaveApplNecessary').prop('checked', false);
        }
    }
    else {
        $("#AdvleaveApplNecessaryDays").prop('readonly', true);
        $("#AdvleaveApplNecessaryDays").val('');
    }
}


function GetDetails() {
    debugger;

    var LeaveGroup = $('#LeaveGroup_Code').val();
    var Leave = $('#Leave_Code').val();
    var LeaveName = $('#Leave_Code option:selected').text();
  
        $.ajax({
            type: 'GET',
            url: '/Master/LeaveRule/GetLeaveRuleDetails',
            dataType: 'html',
            data: { LeaveGroup: LeaveGroup, Leave: Leave, LeaveName: LeaveName },
            success: function (result) {
                $('#LeaveRuleDetailsID').html('');
                $('#LeaveRuleDetailsID').html(result);
            },
            error: function (xhr, textStatus, errorThrown) {
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);

            }
        });
    
}

function GetClubbingDropdowns() {
    $.getJSON("/Master/LeaveRule/GetClubbingDropdown", { LeaveCode: $('#Leave_Code').val() }, function (data) {

        $('#ClubbingLeaves option').remove();
        

        for (var i = 0; i < data.length; i++) {
            debugger
            $('#ClubbingLeaves').append('<option value="' + data[i].code + '">' + data[i].name + '</option');
        }
        $('#ClubbingLeaves').multiselect('rebuild')
    });
    $('.clubDropdown').toggleClass('hide');
}

function validateDropdowns() {
    debugger

    var i = 0;
    if (!validateById('#LeaveGroup_Code', 'LeaveGp')) {
        i++;
    }
    if (!validateById('#Leave_Code', 'Leave')) {
        i++;
    }

    if (i == 0)
        return true;
    else
        return false;


}





function OnLeaveRuleSaveClick() {
    debugger;
    var LeaveGroup = $('#LeaveGroup_Code').val();
    var Leave = $('#Leave_Code').val();
    if (validateDropdowns()) {

        swal.fire({
            text: "Do you want to Save ?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({

                    type: 'POST',
                    url: '/Master/LeaveRule/Save',
                    dataType: 'json',
                    data: $('#Leave-Rule-Save').serialize() + "&LeaveGroup=" + LeaveGroup + "&Leave=" + Leave,
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                GetDetails();

                            });

                        }

                        else {
                            swal.fire(result.message).then(function () {

                            });
                        }

                    },
                    error: function (xhr, textStatus, errorThrown) {
                        document.body.id = "kt_body";
                        $('body').html(xhr.responseText);

                    }
                });
            }
            else {
                return false;
            }
        })

   
    }
}

function ExportToExcel() {
    debugger;
    var i = 0;
    if (!validateById('#LeaveGroup_Code', 'LeaveGp')) {
        i++;
    }
    if (!validateById('#Leave_Code', 'Leave')) {

        i++;
    }

    if (i > 0) {
        return false;
    }
    else {
        var LeaveGroup = $('#LeaveGroup_Code').val();
        var LGName = $('#LeaveGroup_Code option:selected').text();
        var Leave = $('#Leave_Code').val();
        var LeaveName = $('#Leave_Code option:selected').text();

        var TableLength = document.getElementById("LeaveRuleTable").rows.length - 1;
        if (TableLength > 0) {
            url = "/Master/LeaveRule/ExportToExcel?LeaveGroup=" + LeaveGroup + "&LGName=" + LGName + "&Leave=" + Leave + "&LeaveName=" + LeaveName;
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
    }
}

function decimalValidation(event) {
    debugger;
   
    const input = event.target.value;
    const parts = input.split('.');
    if (parts.length === 2) {
        if (parts[1].length > 0 && !['0', '5'].includes(parts[1])) {
            event.target.value = parts[0] + '.' + parts[1].slice(0, -1);
            swal.fire("only 0 and 5 are allowed after decimal");
            return false;
        }
        return true;
    }
}
    
