




function onEditClk(RowIndex,DepartmentCode) {
    {

        var GroupCompanyCode = $('#GroupCompany_Code').val();
        //var EmployeeGI = $("#employeeGI_" + RowIndex).val();

        $.ajax({
            type: 'GET',
            url: '/DataBank/DepartmentWiseProfitCenter/EditList',
            dataType: 'html',
            data: { GroupCompanyCode: GroupCompanyCode, DepartmentCode :  DepartmentCode},
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('#SliderView').html("");
                    $('#SliderView').html(result);
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

















function ExportToExcel() {
    debugger;
    var tableLength = $('#DepartmentWiseProfitCenterList_Count').val();
    if (tableLength > 0) {
        url = "/DataBank/DepartmentWiseProfitCenter/ExportToExcel?totRecords=" + tableLength;
        window.location.href = url;

    }
    else {
        swal.fire("No data found to export");
    }
}

function GetList() {
    debugger;
    var company = $('#GroupCompany_Code').val();
    $.ajax({
        type: 'GET',
        url: '/DataBank/DepartmentWiseProfitCenter/GetList',
        data: { groupCompanyCode: company },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {

                $('#DepartmentWiseProfitCenterList').html("");
                $('#DepartmentWiseProfitCenterList').html(result);
            }

         
        },
        error: function (xhr, textStatus, errorThrown) {
            swal.fire("An error occurred while contacting the server.");
        }
    });
}



function Save(mode) {
    debugger;
  
    var table = document.getElementById("ProfitCenterTable");
    var rows = table.getElementsByTagName("tr");
    
    let total = 0;
    let j = 0;

    for (let ind = 0; ind < rows.length; ind++) {
        let checkbox = $('#DepartmentWiseProfitCenterList_' + ind + '__Applicability');
        let percentageInput = $('#DepartmentWiseProfitCenterList_' + ind + '__Percentage');

        if (checkbox.is(':checked')) {
            j++;

            let val = percentageInput.val().trim();

            // If blank, set to 0 for consistency
            if (val === "") {
                percentageInput.val(0);
                val = "0";
            }

            let value = parseFloat(val);

            // Validation: must be > 0
            if (isNaN(value) || value <= 0) {
                swal.fire("Please enter a valid percentage greater than 0 for all selected profit centers.");
                percentageInput.addClass("is-invalid");
                return false;
            }

            percentageInput.removeClass("is-invalid");
            total += value;
        }
    }

    // Validation: No checkboxes selected
    if (j === 0) {
        swal.fire("Please select at least one applicable profit center.");
        return false;
    }

    // Validation: total must be exactly 100
    if (parseFloat(total) !== 100) {
        swal.fire("Profit centers percentage total should be exactly 100.");
        return false;
    }

    
    
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
                url: '/DataBank/DepartmentWiseProfitCenter/Save?GroupCompanyCode=' + $('#GroupCompany_Code').val()+'&DepartmentCode='+ $('#DepartmentCode').val(),
                dataType: 'json',
                data: $('#ProfitCenterSave').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('AddNewOrEdit');
                            GetList();
                        });

                    }

                    else {
                        swal.fire(result.message).then(function () {

                        });
                    }

                },
                error: function (xhr, textStatus, errorThrown) {
                    debugger
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










function checkBoxChange(val) {
    debugger;
    if ($('#DepartmentWiseProfitCenterList_' + val + '__Applicability').is(':checked')) {
        $('#DepartmentWiseProfitCenterList_' + val + '__Percentage').attr("disabled", false);
    }
    else {
        $('#DepartmentWiseProfitCenterList_' + val + '__Percentage').val(0);
        $('#DepartmentWiseProfitCenterList_' + val + '__Percentage').attr("disabled", true);
    }

}


function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode != 46 && charCode > 31
        && (charCode < 48 || charCode > 57))
        return false;

    return true;
}



function validateProfitCenterForm() {
    var isValid = true;

    $('#ProfitCenterTable tbody tr').each(function (index) {
        var checkbox = $('#DepartmentWiseProfitCenterList_' + index + '__Applicability');
        var percentageInput = $('#DepartmentWiseProfitCenterList_' + index + '__Percentage');

        if (checkbox.length && percentageInput.length && checkbox.is(':checked')) {
            var val = percentageInput.val().trim();
            if (val === "") {
                isValid = false;
                percentageInput.addClass("is-invalid");
            } else {
                percentageInput.removeClass("is-invalid");
            }
        }
    });

    if (!isValid) {
        alert("Please enter a value for all checked profit centers.");
    }

    return isValid;
}
