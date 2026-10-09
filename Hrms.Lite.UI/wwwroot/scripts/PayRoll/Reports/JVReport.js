function ExportToExcelJVReport() {
    debugger;

    var MonthName = $('#PayrollMonth_Code option:selected').text();

    var i = 0;
    if (!validateById("#PayrollMonth_Code", 'month'))
        i++;
    if (!validateById("#PayrollGroup", 'payrollgrp'))
        i++;

    if (i == 0) {
        $.ajax({
            type: 'POST',
            url: '/PayRoll/JVReport/CheckDataCount',
            data: $('#ExportToExcelJVReport').serialize() + "&Month=" + MonthName,
            success: function (result) {
                debugger;
                if (result > 0) {
                    $('#ExportToExcelJVReport').submit();
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


function ExportToExcelERPJV() {
    debugger;

    var MonthName = $('#PayrollMonth_Code option:selected').text();

    var i = 0;
    if (!validateById("#PayrollMonth_Code", 'month'))
        i++;
    if (!validateById("#PayrollGroup", 'payrollgrp'))
        i++;

    if (i == 0) {
        //$.ajax({
        //    type: 'POST',
        //    url: '/PayRoll/JVReport/CheckDataCountERPJV',
        //    data: $('#ExportToExcelJVReport').serialize() + "&Month=" + MonthName,
        //    success: function (result) {
        //        debugger;
        //        if (result > 0) {
        //            $('#ExportToExcelJVReport').submit();
        //        }
        //        else {
        //            swal.fire("No data found");
        //        }
        //    },
        //    error: function (xhr, textStatus, errorThrown) {
        //        debugger;
        //        document.body.id = "kt_body";
        //        $('body').html(xhr.responseText);
        //    }
        //});
        var PARAM = $('#ExportToExcelJVReport').serialize() ;
        url = '/PayRoll/JVReport/ExportToExcelERPJV?' + PARAM + "&Month=" + MonthName;
        window.location.href = url;
    }
    else {
        return false;
    }

}