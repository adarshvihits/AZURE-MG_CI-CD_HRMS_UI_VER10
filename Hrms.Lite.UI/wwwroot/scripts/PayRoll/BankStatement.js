function ExportToExcelbankStatement() {
    debugger;

    var MonthName = $('#PayrollMonth_Code option:selected').text();

    var i = 0;
    if (!validateById("#PayrollMonth_Code", 'month'))
        i++;
    if (!validateById("#ReportFormat", 'rptformat'))
        i++;
    if (!validateById("#ExecutionDate", 'exdate'))
        i++;
    if (i == 0)
{
        $.ajax({
            type: 'POST',
            url: '/PayRoll/BankStatement/CheckDataCount',
            data: $('#ExportToExcelbankStatement').serialize() + "&PayrollMonth=" + MonthName,
            success: function (result) {
                debugger;
                if (result > 0) {
                    $('#ExportToExcelbankStatement').submit();
                }
                else {
                    swal.fire("No data found");
                }
            },
            error: function (xhr, textStatus, errorThrown) {
                debugger;
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);
            }
        });

    }
    else {
        return false;
    }

}

//function PostToSFTP() {

 
//    var MonthName = $('#PayrollMonth_Code option:selected').text();

//    var i = 0;
//    if (!validateById("#PayrollMonth_Code", 'month'))
//        i++;

//    if (!validateById("#ExecutionDate", 'exdate'))
//        i++;
//    if (i == 0) {
//        $.ajax({

//            type: 'POST',
//            url: '/PayRoll/BankStatement/CheckIfAlreadyPostedtoSFTP',
//            dataType: 'json',
//            data: {MonthCode: $('#PayrollMonth_Code').val()},
//            success: function (result) {
//                if (result.success == true) {

//                    $.ajax({
//                        type: 'POST',
//                        url: '/PayRoll/BankStatement/PostToSFTP',
//                        data: $('#ExportToExcelbankStatement').serialize() + "&PayrollMonth=" + MonthName,
//                        success: function (result) {
//                            debugger;
//                            if (result.success == true) {
//                                swal.fire("Posted Successfully...");
//                            }
//                            else {
//                                swal.fire("No data found");
//                            }
//                        },
//                        error: function (xhr, textStatus, errorThrown) {
//                            debugger;
//                            document.body.id = "kt_body";
//                            $('body').html(xhr.responseText);
//                        }
//                    });

//                }

//                else {
//                    swal.fire(result.message).then(function () {

//                    });
                  
//                }

//            },
//            error: function (xhr, textStatus, errorThrown) {
//                if ($($.parseHTML(xhr.responseText)).filter(".Login_Session").length > 0) {
//                    window.location.reload();
//                }
//                else {
//                    document.body.id = "kt_body";
//                    $('body').html(xhr.responseText);
//                }
//            }
//        });








     

//    }
//    else {
//        return false;
//    }

//}


function PostToSFTP()
{
    var MonthName = $('#PayrollMonth_Code option:selected').text();

    var i = 0;
    if (!validateById("#PayrollMonth_Code", 'month'))
        i++;

    if (!validateById("#ExecutionDate", 'exdate'))
        i++;
    if (i == 0) {
        $.getJSON("/PayRoll/BankStatement/CheckIfAlreadyPostedtoSFTP", { MonthCode: $('#PayrollMonth_Code').val() }, function (data) {
            //console.log(data);
            debugger
            if (data.success == true) {

                $.ajax({
                    type: 'POST',
                    url: '/PayRoll/BankStatement/PostToSFTP',
                    data: $('#ExportToExcelbankStatement').serialize() + "&PayrollMonth=" + MonthName,
                    success: function (result) {
                        debugger;
                        if (result.success == true) {
                            swal.fire("Posted Successfully...");
                        }
                        else {
                            swal.fire("No data found");
                        }
                    },
                    error: function (xhr, textStatus, errorThrown) {
                        debugger;
                        document.body.id = "kt_body";
                        $('body').html(xhr.responseText);
                    }
                });

            }
            else
                swal.fire(data.message);


        });
    }
}