
function ExportToExcel() {
 
    var i = 0;
    if (!validateById('#FromDate', 'fromDte'))
        i++;
    if (!validateById('#ToDate', 'toDte'))
        i++;
    $.ajax({
        url: '/TimeOffice/EmpMovementRpt/CountCheckAJAX',
        type: "POST",
        dataType: 'json',
        async: false,
        data: $('#EmpMovementRpt').serialize(),
        success: function (result) {
            debugger;

            if (result == true) {
                debugger;
                i++;
            }



        },


        error: function (xhr, textStatus, errorThrown) {
            $('body').html(xhr.responseText);
        }
    });
    if (i == 0) {
        return true;
        


    }



    //}
    else {
        return false;
    }
    //})
}

function getDataSetCountAJAX() {
    var i = 0;
    if (!validateById('#FromDate', 'fromDte'))
        i++;
    if (!validateById('#ToDate', 'toDte'))
        i++;
    if (i == 0) {
        $.ajax({
            url: '/TimeOffice/EmpMovementRpt/CountCheckAJAX',
            type: "POST",
            async: false,
            dataType: 'json',
            data: $('#EmpMovementRpt').serialize(),
            success: function (result) {
                debugger;
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    if (result == true) {
                        debugger;
                        $("#EmpMovementRpt").submit();
                        

                    }
                    else {
                        swal.fire("No Data Found");
                        return false;

                    }

                }


            },


            error: function (xhr, textStatus, errorThrown) {
                $('body').html(xhr.responseText);
            }
        });
    }
    else {
        return false;
    }
}

//function getDataSetCountAJAX() {
//    var i = 0;
//    if (!validateById('#FromDate', 'fromDte'))
//        i++;
//    if (!validateById('#ToDate', 'toDte'))
//        i++;
//    if (i == 0) {
//        debugger;
//        $("#EmpMovementRpt").submit();
//        return true;
//    }
//    else {
//        swal.fire("No Data Found");
//        return false;

//    }

              
//}

