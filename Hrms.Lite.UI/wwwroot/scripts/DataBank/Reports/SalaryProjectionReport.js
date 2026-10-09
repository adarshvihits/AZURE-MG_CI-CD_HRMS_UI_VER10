function getProjPopup() {
    debugger;
    $.ajax({
        type: 'GET',
        url: "/DataBank/SalaryProjectionReport/GetPopUp",

        dataType: 'html',

        success: function (Data) {
            if ($($.parseHTML(Data)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                var m = $('.salaryprojection');
                m.find('.modal-body').html('');
                m.find('.modal-body').html(Data);
                $('#salaryprojectionreport').html('Add Projection');
                $('.salaryprojection').modal('show');
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            swal.fire(xhr.responseText);
        }
    });
}


function OnApplyClick() {
    debugger;
    var AmountType = $('#AmountType').val();
    var Type = $('#Type').val();
    var Amount = $('#Amount').val();
    var Allowance = $('#Allowance').val();
    var Dsg = $('#salaryprojsave').serialize();

    $.ajax({

        type: 'POST',
        url: '/DataBank/SalaryProjectionReport/GetList',

        dataType: 'html',
        //data: $('#salaryprojsave').serialize(),
        data: $('#salaryprojsave').serialize()+ "&AmountType=" + AmountType + "&Type=" + Type + "&Amount=" + Amount + "&Allowance=" + Allowance,
        success: function (result) {
            debugger;
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                var m = $('.salaryprojection');
                m.find('.modal-body').html('');
                m.modal('hide');
                $('#SalaryProjectionReportList').html(result);

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


function ExportToExcel() {
    debugger;
    //var TableLength = $('#SalaryProjectionList_Count').val();
    //if (TableLength > 0) {
        //url = "/DataBank/SalaryProjectionReport/ExportToExcel?totRecords="  + TableLength;
        url = "/DataBank/SalaryProjectionReport/ExportToExcel";
        window.location.href = url;
    //}
    //else {
    //    swal.fire("No data found to export");
    //}
}


function OnTypeChange() {
    debugger
    var type = $('#Type').val();
    if (type == 1)
        $('.AllowanceDiv').css("display", "block");
    else {
        $('.AllowanceDiv').css("display", "none");
    }
}