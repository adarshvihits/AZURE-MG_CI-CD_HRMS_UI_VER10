


function onLeavegroupClick() {
    debugger;
    var LGroup = $('#LeaveGroup_Code').val();
    $.ajax({

        type: 'GET',
        url: '/Master/CoffRule/GetCoffRuleDetails',
        dataType: 'html',
        data: { Leavegroup: LGroup },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#coffRuleDetailsList').html("");
                $('#coffRuleDetailsList').html(result);
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
function GetClubbingDropdowns() {
    $.getJSON("/Master/CoffRule/GetClubbingDropdown", { LeaveCode: $('#Leave_Code').val() }, function (data) {

        $('#ClubbingLeaves option').remove();


        for (var i = 0; i < data.length; i++) {
            debugger
            $('#ClubbingLeaves').append('<option value="' + data[i].code + '">' + data[i].name + '</option');
        }
        $('#ClubbingLeaves').multiselect('rebuild')
    });
    $('.clubDropdown').toggleClass('hide');
}

function CoffCreditApprovalRequiredClick() {
    $('.order1').toggleClass('hide');
    if ($('#CoffCreditApprovalRequired').is(':checked') != true) {
        $("#HRMSApprovalRequired").prop("checked", false);
        $("#ESSPApprovalRequired").prop("checked", false);
    }
}

function validateDropdown() {
    debugger

    var i = 0;
    if (!validateById('#LeaveGroup_Code', 'LeaveGp')) {
        i++;
    }
    if ($('#CoffCreditApprovalRequired').is(':checked')) {
        if ($('#HRMSApprovalRequired').is(':checked') != true && $('#ESSPApprovalRequired').is(':checked') != true) {
            swal.fire('Please select any Coff Credit Approval (HRMS/ESSP)');
            i++;
        }
    }
    if (i == 0)
        return true;
    else
        return false;


}


function CoffRuleSave()
{
    debugger;
    var LGroup = $('#LeaveGroup_Code').val();
    if (validateDropdown()) {

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
                    url: '/Master/CoffRule/Save',
                    dataType: 'json',
                    data: $('#COff-Rule-Save').serialize() + "&LeaveGroup=" + LGroup,
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                onLeavegroupClick();

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




function ExportToexcel() {
    debugger;
    var i = 0;
    if (!validateById('#LeaveGroup_Code', 'LeaveGp')) {
        i++;
    }
    if (i > 0) {
        return false;
    }
    else {
        var LeaveGroup = $('#LeaveGroup_Code').val();
        var LGName = $('#LeaveGroup_Code option:selected').text();


        var TableLength = document.getElementById("coffruleTable").rows.length - 1;
        if (TableLength > 0) {
            url = "/Master/CoffRule/ExportToExcel?LeaveGroup=" + LeaveGroup + "&LGName=" + LGName;
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
    }
}