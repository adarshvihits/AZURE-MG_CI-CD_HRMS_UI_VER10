function onDeductionChange() {
    $.ajax({

        type: 'GET',
        url: '/PayRoll/FormulaBasedDeduction/GetFormulaBasedDeductionDetails',
        dataType: 'html',
        data: { DeductionCode: $('#Deduction_Code').val() },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {

                $('#DedAndAlwDiv').html("");
                $('#DedAndAlwDiv').html(result);
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

function formulaBasedDeductionSave() {
    var i = 0;
    if (!validateById('#Deduction_Code', 'dCode'))
        i++;
    if (!validateById('#Operations_Code', 'oCode'))
        i++;
    if (!validateById('#Value', 'vals'))
        i++;

    if (i == 0) {

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
                    url: '/PayRoll/FormulaBasedDeduction/FormulaBasedDeductionSave',
                    dataType: 'json',
                    data: $('#formulaBasedDeductionSave').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire("Saved Successfully").then(function () {
                                onDeductionChange();
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
}