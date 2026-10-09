
function ExportToExcel() {

    debugger;
    var tableLength = 0;


    tableLength = $('#WageGradeVsSumAssuredList_Count').val();
    if (tableLength > 0) {

        url = "/DataBank/WageGradeVsSumAssured/ExportToExcel?TabIndex=" + tableLength ;
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
    $('#editbt2_' + i).addClass('hide');
    $('#amtTxtbox1_' + i).removeClass('hide');
    $('#amtTxtbox2_' + i).removeClass('hide');
    $('#amtTxtbox3_' + i).removeClass('hide');
    $('#updateCancel2_' + i).removeClass('hide');
}





function onCancelClick(i) {
    debugger;
    $('#amtLabel1_' + i).removeClass('hide');
    $('#amtLabel2_' + i).removeClass('hide');
    $('#amtLabel3_' + i).removeClass('hide');
    $('#editbt2_' + i).removeClass('hide');
    $('#amtTxtbox1_' + i).addClass('hide');
    $('#amtTxtbox2_' + i).addClass('hide');
    $('#amtTxtbox3_' + i).addClass('hide');
    $('#updateCancel2_' + i).addClass('hide');
}


//function onUpdateClick(i) {
//    debugger;
//    $('#amtLabel1_' + i).html($('#WageGradeVsSumAssuredList_' + i + '__Ledger option:selected').text());
//    $('#amtLabel1_' + i).removeClass('hide');
//    $('#amtLabel2_' + i).html($('#WageGradeVsSumAssuredList_' + i + '__Ledger option:selected').text());
//    $('#amtLabel2_' + i).removeClass('hide');
//    $('#amtLabel3_' + i).html($('#WageGradeVsSumAssuredList_' + i + '__Ledger option:selected').text());
//    $('#amtLabel3_' + i).removeClass('hide');
//    $('#editbt2_' + i).removeClass('hide');
//    $('#amtTxtbox1_' + i).addClass('hide');
//    $('#amtTxtbox2_' + i).addClass('hide');
//    $('#amtTxtbox3_' + i).addClass('hide');
//    $('#updateCancel2_' + i).addClass('hide');


//}

function onUpdateClick(i, WageGradeCode) {
    debugger;

    var mediClaim = $('#WageGradeVsSumAssuredList_' + i + '__MediClaim').val();
    var personalAccident = $('#WageGradeVsSumAssuredList_' + i + '__PersonalAccident').val();
    var gtli = $('#WageGradeVsSumAssuredList_' + i + '__GTLI').val();

    $.ajax({
        type: 'POST',
        url: '/DataBank/WageGradeVsSumAssured/Save',
        dataType: 'json',
        data: {
            WageGradeCode: WageGradeCode,
            MediClaim: mediClaim,
            PersonalAccident: personalAccident,
            GTLI: gtli
        },
        success: function (result) {
            if (result.success === true) {
                swal.fire("Updated Successfully").then(function () {
                    window.location.href = "/DataBank/WageGradeVsSumAssured/Index";
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
