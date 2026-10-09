function getList() {
    debugger
    $.ajax({

        type: 'GET',
        url: '/PayRoll/MonthlyVDASettings/GetMonthlyVDASettingsList',
        dataType: 'html',
        data: { SalaryperiodCode: $("#Month_Code").val() },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#MVDASettings').html("");
                $('#MVDASettings').html(result);
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

function calculateVDAAmount(index)
{
    debugger
    var vdapoint = parseFloat($("#MonthlySetting_" + index + "__VDAPoint").val()).toFixed(2);
    var std = parseFloat($("#MonthlySetting_" + index + "__StandardDeduction").val()).toFixed(2);
    var daindex = parseFloat($("#MonthlySetting_" + index + "__DAIndex").val()).toFixed(2);

    var vdaAmount = (vdapoint - std) * daindex;
    vdaAmounttofixed = parseFloat(vdaAmount).toFixed(2);
    $("#MonthlySetting_" + index + "__VDAAmount").val(vdaAmounttofixed);
    if (isNaN(vdaAmounttofixed))
        $("#MonthlySetting_" + index + "__VDAAmount").val('0.00');
}
function Save() {

    var i = 0;
    if (!validateById('#Month_Code', 'mvda_month')) {
        i++;
    }
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
                    url: '/PayRoll/MonthlyVDASettings/Save',
                    dataType: 'json',
                    data: $('#MVDAsettingsform').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                getList();
                            });

                        }
                        else {
                            swal.fire(result.message);

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