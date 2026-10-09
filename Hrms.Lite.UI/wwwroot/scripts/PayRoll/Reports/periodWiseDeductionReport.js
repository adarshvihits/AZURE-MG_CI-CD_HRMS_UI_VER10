function GetList() {
   
    if ($('#DeductionCode').val() != '' && $('#From').val() != '' && $('#To').val() != '') {
        showSpinner();
        $.ajax({
            type: 'GET',
            url: '/PayRoll/PeriodWiseDeductionReport/GetList?Deduction=' + $('#DeductionCode').val() + '&MonthFrom=' + $('#From').val() + '&MonthTo=' + $('#To').val(),
            dataType: 'html',
            data: $('#BasicFilter').serialize(),
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('#ListView').html("");
                    $('#ListView').html(result);
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
}

function GetToMonthDropdown() {
    $.getJSON("/PayRoll/PeriodWiseDeductionReport/GetToMonth", { code: $('#From').val() }, function (data) {

        $('#To option').remove();
        $('#To').append('<option value="">--Select--</option');

        for (var i = 0; i < data.length; i++) {
            $('#To').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
        }
        GetList();
    });
}
function GetDeductionDropdown() {
    if ($('#From').val() != '' && $('#To').val() != '') {
        $.getJSON("/PayRoll/PeriodWiseDeductionReport/GetDeductionDropdown", { MonthFrom: $('#From').val(), MonthTo: $('#To').val() }, function (data) {

            $('#DeductionCode option').remove();
            $('#DeductionCode').append('<option value="">--Select--</option');

            for (var i = 0; i < data.length; i++) {
                $('#DeductionCode').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
            }
            GetList();
        });
    }
}

function ApplyFilter(mode) {
    debugger;


    if (mode == 'apply') {
        if (filterApplicable >= 1) {
            CloseSlider('basicFilter');
            GetList();
            $('#filterForm').removeClass('filter-apply');
        }
        else {
            CloseSlider('basicFilter');
            GetList();
            $('#filterForm').addClass('filter-apply');
        }

    }
    else {
        filterApplicable = 1;
        $('.reset').val('');
        $('#filterForm').removeClass('filter-apply');
        GetList();


    }
}

function ExportToExcel() {

    var tableLength = $('#PeriodWiseDeductionRptTable tbody tr').length;
    if (tableLength > 0) {
        //url = "/DataBank/JoiningReport/ExportToExcel?fromDate=" + fromDate + "&toDate=" + toDate + "&totRecords=" + tableLength;
        if ($('#DeductionCode').val() != '' && $('#From').val() != '' && $('#To').val() != '') {
            url = '/PayRoll/PeriodWiseDeductionReport/ExportToExcel?Deduction=' + $('#DeductionCode').val() + '&MonthFrom=' + $('#From').val() + '&MonthTo=' + $('#To').val() + '&data=' + $('#BasicFilter').serialize() + '&FromName=' + $('#From option:selected').text() + '&ToName=' + $('#To option:selected').text() + '&DeductionName=' + $('#DeductionCode option:selected').text();
            window.location.href = url;
        }
    }
    //}
    //else {
    //    swal.fire("No data found to export");
    //}

}