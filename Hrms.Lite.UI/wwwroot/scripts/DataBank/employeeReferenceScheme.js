
function ExportToExcel() {

    debugger;
    var tableLength = 0;


    tableLength = $('#EmployeeReferenceSchemeList_Count').val();
    if (tableLength > 0) {

        url = "/DataBank/EmployeeReferenceScheme/ExportToExcel?TabIndex=" + tableLength;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }

}



function onEditClick(i) {
    debugger;
    $('#amtLabel1_' + i).addClass('hide');
    $('#amtLabel2_' + i).addClass('hide');
    $('#amtLabel3_' + i).addClass('hide');
    $('#amtLabel4_' + i).addClass('hide');
    $('#editbt2_' + i).addClass('hide');
    $('#amtTxtbox1_' + i).removeClass('hide');
    $('#amtTxtbox2_' + i).removeClass('hide');
    $('#amtTxtbox3_' + i).removeClass('hide');
    $('#amtTxtbox4_' + i).removeClass('hide');
    $('#updateCancel2_' + i).removeClass('hide');
}





function onCancelClick(i) {
    debugger;
    $('#amtLabel1_' + i).removeClass('hide');
    $('#amtLabel2_' + i).removeClass('hide');
    $('#amtLabel3_' + i).removeClass('hide');
    $('#amtLabel4_' + i).removeClass('hide');
    $('#editbt2_' + i).removeClass('hide');
    $('#amtTxtbox1_' + i).addClass('hide');
    $('#amtTxtbox2_' + i).addClass('hide');
    $('#amtTxtbox3_' + i).addClass('hide');
    $('#amtTxtbox4_' + i).addClass('hide');
    $('#updateCancel2_' + i).addClass('hide');
}


//function onUpdateClick(i) {
//    debugger;
//    $('#amtLabel1_' + i).html($('#EmployeeReferenceSchemeList_' + i + '__Ledger option:selected').text());
//    $('#amtLabel1_' + i).removeClass('hide');
//    $('#amtLabel2_' + i).html($('#EmployeeReferenceSchemeList_' + i + '__Ledger option:selected').text());
//    $('#amtLabel2_' + i).removeClass('hide');
//    $('#amtLabel3_' + i).html($('#EmployeeReferenceSchemeList_' + i + '__Ledger option:selected').text());
//    $('#amtLabel3_' + i).removeClass('hide');
//    $('#editbt2_' + i).removeClass('hide');
//    $('#amtTxtbox1_' + i).addClass('hide');
//    $('#amtTxtbox2_' + i).addClass('hide');
//    $('#amtTxtbox3_' + i).addClass('hide');
//    $('#updateCancel2_' + i).addClass('hide');


//}

function onUpdateClick(i, EmployeeReferenceCode) {
    debugger;

    var bonusAmount = $('#EmployeeReferenceSchemeList_' + i + '__BonusAmount').val();
    var firstPayout = $('#EmployeeReferenceSchemeList_' + i + '__FirstPayout').val();
    var secondPayout = $('#EmployeeReferenceSchemeList_' + i + '__SecondPayout').val();
    var additionalBonus = $('#EmployeeReferenceSchemeList_' + i + '__AdditionalBonus').val();
    $.ajax({
        type: 'POST',
        url: '/DataBank/EmployeeReferenceScheme/Save',
        dataType: 'json',
        data: {
            EmployeeReferenceCode: EmployeeReferenceCode,
            BonusAmount: bonusAmount,
            FirstPayout: firstPayout,
            SecondPayout: secondPayout,
            AdditionalBonus: additionalBonus
        },
        success: function (result) {
            if (result.success === true) {
                swal.fire("Updated Successfully").then(function () {
                    window.location.href = "/DataBank/EmployeeReferenceScheme/Index";
                });
            } else {
                swal.fire(result.message);
            }
        },
        error: function (xhr) {
            if ($($.parseHTML(xhr)).filter(".Login_Session").length > 0) {
                window.location.reload();
            } else {
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);
            }
        }
    });
}








function AddNewOrEditClick() {
    debugger;
    var GI=0
    
    var url = "/DataBank/EmployeeReferenceScheme/Create";
        $.ajax({

            type: 'GET',
            url: '/DataBank/EmployeeReferenceScheme/Create',
            dataType: 'html',
            
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('#AddNewOrEditSlider').html("");
                    $('#AddNewOrEditSlider').html(result);
                }


            },


            error: function (xhr, textStatus, errorThrown) {
                debugger
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);

            }
        });
    


}







function ReferenceSchemeSave(mode) {
    debugger;
    if (validateDepartment()) {

        swal.fire({
            text: "Do you want to " + mode + "?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({

                    type: 'POST',
                    url: '/DataBank/EmployeeReferenceScheme/SaveMaster',
                    dataType: 'json',
                    data: $('#ReferenceSchemesave').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                window.location.reload();

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
    else {
        return false;
    }
}




function validateDepartment() {
    var i = 0;
    if (!validateById('#Name', 'ReferenceSchemeName')) {
        i++;
    }
    if (!validateById('#ShortName', 'RShortName')) {
        i++;
    }

    if (i == 0)
        return true;
    else
        return false;
}