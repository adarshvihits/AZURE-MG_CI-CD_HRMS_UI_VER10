
function getList(sender) {
    debugger;
    showSpinner();
    var selValue = sender.value;
    if (selValue != '') {
        $('.filterDropDown').val(selValue);
        $.ajax({

            type: 'GET',
            url: '/DataBank/EConsoleSubmissionReport/GetList?ConsoleType=' + selValue,
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
    else {
        var url = "/DataBank/EConsoleSubmissionReport/Index";
        window.location.href = url;
    }
}

function ApplyFilter(mode) {
    debugger
    var Type = $('#Console').val();
    if (mode == 'apply') {
        if (filterApplicable >= 1) {
            $.ajax({
                type: 'GET',
                url: '/DataBank/EConsoleSubmissionReport/GetList?ConsoleType=' + Type,
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
            CloseSlider('basicFilter');
            $('#filterForm').removeClass('filter-apply');
        }
        else {
            $.ajax({
                type: 'GET',
                url: '/DataBank/EConsoleSubmissionReport/GetList?ConsoleType=' + Type,
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
            CloseSlider('basicFilter');
            $('#filterForm').addClass('filter-apply');
        }
    }
    else {
        filterApplicable = 1;
        $('.reset').val('');
        $('#filterForm').removeClass('filter-apply');
        $.ajax({
            type: 'GET',
            url: '/DataBank/EConsoleSubmissionReport/GetList?ConsoleType=' + Type,
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
function onDownloadClick(EmployeeCode) {
    debugger
    var console = $('#ConsoleCode').val();
    var url = "/DataBank/EConsoleSubmissionReport/GetEConsoleSubmissionReportPDF?EmployeeCode=" + EmployeeCode + '&Console=' + console;
    window.location.href = url;
}
function ExportToExcel() {
    debugger;
    var Type = $('#Console').val();
    var tYpename = $('#Console option:selected').text();
    var filterdata = $('#BasicFilter').serialize();
    var PARAM = 'data=' + filterdata;

    var tableLength = $('#EConsoleSubmissionReportList_Count').val();
 
    if (tableLength > 0) {
        url = '/Databank/EConsoleSubmissionReport/ExportToExcel?' + PARAM + '&TypeCode=' + Type + '&Type=' + tYpename;
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
}
