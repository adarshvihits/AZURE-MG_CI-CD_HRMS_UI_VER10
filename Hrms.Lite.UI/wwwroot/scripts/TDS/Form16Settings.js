function onSubmit() {
    debugger
    if (validateEntry()) {
       

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
                    url: '/TDS/Form16Settings/SaveForm16Settings',
                    dataType: 'json',
                    data: $('#Form16_Settings').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire("Saved Successfully").then(function () {
                                window.location.href = "/TDS/Form16Settings/Create";
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
            else
                return false;
        })
    }
    else
        return false;
}
function validateEntry() {
    var i = 0;
    if (!validateById('#AssessmentYear_Code', 'Assessment'))
        i++;
    if (!validateById('#GroupCompany_Code', 'groupcmpy'))
        i++;
    if (!validateById('#Address', 'address'))
        i++;
    if (!validatePan('PAN', 'pan'))
        i++;
    if (!validateById('#TAN', 'tan'))
        i++;
    if (!validateById('#Name', 'name'))
        i++;
    if (!validateById('#FatherName', 'fathername'))
        i++;
    if (!validateById('#Designation', 'designation'))
        i++;
    if (!validateById('#Place', 'place'))
        i++;
    if (!validateById('#PrintDate', 'printdate'))
        i++;
   
    if (i == 0)
        return true;

    else {
        return false;
    }

}
function fillDetails()
{
    var AssessmentYear = $('#AssessmentYear_Code').val();
    $.ajax({
        url: '/TDS/Form16Settings/FillFinYearDetails',
        async: false,
        dataType: 'json',
        data: {
            AssessmentYear: AssessmentYear
        },
        success: function (data) {
         
            debugger;
            $('#FinancialYear').val(data.financialYear);
            $('#FromDate').val(data.fromDate);
            $('#ToDate').val(data.toDate);
        }
    });
}