$(document).ready(function () {

    if ($('#' + tableId).find('input[type="checkbox"]:checked').length == $('#' + tableId).find('input[type="checkbox"]').length)
        $('#' + allCheckBox).prop('checked', true);
    else
        $('#' + allCheckBox).prop('checked', false);

});
function Gridclear() {
    $('#SRBnkStatemntTable tbody').empty();
 
}
function getList() {
    debugger


    var i = 0;
    if (!validateById("#PayrollMonth_Code", 'Month'))
        i++;
    if (!validateById("#ReportFormat", 'RptFormat'))
        i++;

    var currentDate = new Date();
    currentDate.setHours(0, 0, 0, 0); // normalize to midnight

    var inputVal = $('#ExecutionDate').val();
    var ExDate = new Date(inputVal);
    ExDate.setHours(0, 0, 0, 0); // normalize as well

    if (ExDate < currentDate ) {
        $('#ExecutionDate').addClass('is-invalid');
        $("#exdate").addClass('error-message');
        $("#exdate").html("Please select future date......");
        i++;
    }
    if (i == 0) {
        showSpinner();
        ApplyFilter('apply');

    }

    else
        return false;
}
function ApplyFilter(mode) {
 

    if (mode == 'apply') {
        if (filterApplicable >= 1) {

            $.ajax({

                type: 'POST',
                url: '/PayRoll/SalaryReleaseBankStatement/GetList',
                dataType: 'html',
                data: $('#BasicFilter').serialize() + "&Month=" + $('#PayrollMonth_Code').val() + "&ReportFormat=" + $('#ReportFormat').val(),
                success: function (result) {
                    debugger
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();

                    }
                    else {
                        $('#SRBankStatementList').html("");
                        $('#SRBankStatementList').html(result);
                    }
                    hideSpinner();
                    CloseSlider('basicFilter');

                },


                error: function (xhr, textStatus, errorThrown) {
                    document.body.id = "kt_body";
                    $('body').html(xhr.responseText);
                }
            });

            $('#filterForm').removeClass('filter-apply');
        }
        else {
            showSpinner();
            $.ajax({

                type: 'POST',
                url: '/PayRoll/SalaryReleaseBankStatement/GetList',
                dataType: 'html',
                data: $('#BasicFilter').serialize() + "&Month=" + $('#PayrollMonth_Code').val() + "&ReportFormat=" + $('#ReportFormat').val(),
                success: function (result) {
                    debugger
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }
                    else {
                        $('#SRBankStatementList').html("");
                        $('#SRBankStatementList').html(result);
                    }
                    hideSpinner();
                    CloseSlider('basicFilter');
                },


                error: function (xhr, textStatus, errorThrown) {
                    document.body.id = "kt_body";
                    $('body').html(xhr.responseText);
                }
            });

            $('#filterForm').addClass('filter-apply');
        }

    }
    else {

        showSpinner();
        filterApplicable = 1;
        $('.reset').val('');
        $('#Optional_Filter_EmploymentStatus_Code').val(0);
        $('#filterForm').removeClass('filter-apply');
        $.ajax({

            type: 'POST',
            url: '/PayRoll/SalaryReleaseBankStatement/GetList',
            dataType: 'html',
            data: $('#BasicFilter').serialize() + "&Month=" + $('#PayrollMonth_Code').val() + "&ReportFormat=" + $('#ReportFormat').val(),
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();

                }
                else {
                    $('#SRBankStatementList').html("");
                    $('#SRBankStatementList').html(result);
                }
                hideSpinner();

            },


            error: function (xhr, textStatus, errorThrown) {
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);
            }
        });

    }
}

function searchRows() {
    debugger
    var input, filter, table, tr, td, i;
    var img = "<img src='/images/nodata.png' />";

    input = document.getElementById("SRBnkstmtSearch");

    filter = input.value.toUpperCase();
    table = document.getElementById("SRBnkStatemntTable");
    tr = table.getElementsByTagName("tr");
    tbody = table.getElementsByTagName("tbody");
    $('#noDataImg').remove();
    var noOfRows = tr.length;
    var show = true;
    var spannedRows = 0;

    for (i = 0; i < tr.length; i++) {

        debugger;
        td = tr[i].getElementsByTagName("td")[0];
        if (td) {
            txtValue = td.textContent || td.innerText;
            if (txtValue.toUpperCase().indexOf(filter) > -1) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
                noOfRows--;
            }
        }

    }


    if (noOfRows == 1) {

        $(table).find(tbody).append("<tr  id='noDataImg' class='exit-page'><td colspan=12><div  style='text-align:center; display:flex; align-items:center; justify-content:center;height:45vh;'><img src='/images/nodata.png'/></div ></td></tr>");

    }
}

function onSelectAllClick() {
    if ($('#SelectAll').is(':checked')) {
        $('#SRBnkStatemntTable').find('input[type=checkbox]').prop('checked', true);
    }
    else {
        $('#SRBnkStatemntTable').find('input[type=checkbox]').prop('checked', false);
    }
    var totalRows = $('#SRBnkStatemntTable tbody tr').length;
    for (let i = 0; i < totalRows; i++) {
        onCheckboxChange(document.getElementById('SalaryReleaseBankStatementList' + i + '__Applicability'), i)
    }
}



function ExportToExcel() {

    const table = document.getElementById("SRBnkStatemntTable");
    const checkedCheckboxes = table.querySelectorAll('tbody input[type="checkbox"]:checked');

    if (checkedCheckboxes.length === 0) {
        swal.fire("Please select at least one checkbox to proceed.");
        return;
    }

  

        var form = $('<form>', {
            method: 'POST',
            action: '/PayRoll/SalaryReleaseBankStatement/ExportToExcelReleaseStatement'
        });

  
        form.append($('<input>', { name: 'Month', value: $('#PayrollMonth_Code').val() }));
        form.append($('<input>', { name: 'MonthName', value: $('#PayrollMonth_Code option:selected').text() }));
        form.append($('<input>', { name: 'ReportFormat', value: $('#ReportFormat').val() }));
        form.append($('<input>', { name: 'ReportFormatNme', value: $('#ReportFormat option:selected').text() }));

        // Filter model
        $('#BasicFilter').serializeArray().forEach(function (item) {
            form.append($('<input>', {
                name: 'filter.' + item.name,
                value: item.value
            }));
        });

        // SalaryReleaseBankStatement model 
        $('#SRBankStatemntSave').serializeArray().forEach(function (item) {
            form.append($('<input>', {
                name: 'model.' + item.name,
                value: item.value
            }));
        });

        form.appendTo('body').submit().remove();

}

function TransactionHistory() {
    var MName = $('#PayrollMonth_Code option:selected').text();
    var MCode = $('#PayrollMonth_Code').val();
    window.location.href = '/PayRoll/SalaryReleaseBankStatement/TransactionHistory?Month=' + MCode + " &MonthName=" + MName
}
function GetHistoryList() {
   
    var MCode = $('#PayrollMonth_Code').val();
   
 
    if (MCode != '') {
        $.ajax({
            type: 'GET',
            url: '/PayRoll/SalaryReleaseBankStatement/GetPreviousList?MonthCode=' + MCode,
            dataType: 'html',
            data: $('#BasicFilter').serialize(),
            success: function (data) {
                $('#ListView').html('');
                $('#ListView').html(data);
                hideSpinner();
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

}
function PostToSFTP() {
    const table = document.getElementById("SRBnkStatemntTable");
    const checkedCheckboxes = table.querySelectorAll('tbody input[type="checkbox"]:checked');

    if (checkedCheckboxes.length === 0) {
        swal.fire("Please select at least one checkbox to proceed.");
        return;
    }
    var data = $('#SRBankStatemntSave').serialize() +  "&Month=" + $('#PayrollMonth_Code').val() + "&ReportFormat=" + $('#ReportFormat').val();
        $.ajax({
            type: 'POST',
            url: '/PayRoll/SalaryReleaseBankStatement/PostToSFTP',
            dataType: 'json',
            data: data,
            success: function (result) {
                debugger;
                if (result.success == true) {
                    getList();
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
