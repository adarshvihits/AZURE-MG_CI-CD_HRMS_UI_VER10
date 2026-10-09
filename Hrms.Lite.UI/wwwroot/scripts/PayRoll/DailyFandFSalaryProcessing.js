$(document).ready(function () {

    if ($('#' + tableId).find('input[type="checkbox"]:checked').length == $('#' + tableId).find('input[type="checkbox"]').length)
        $('#' + allCheckBox).prop('checked', true);
    else
        $('#' + allCheckBox).prop('checked', false);

});
function onSelectAllClick() {
    if ($('#SelectAll').is(':checked')) {
        $('#FandFListTable').find('input[type=checkbox]').prop('checked', true);
    }
    else {
        $('#FandFListTable').find('input[type=checkbox]').prop('checked', false);
    }
    var totalRows = $('#FandFListTable tbody tr').length;
    for (let i = 0; i < totalRows; i++) {
        onCheckboxChange(document.getElementById('DailyFandFSalaryProcessingList' + i + '__Applicability'), i)
    }
}

function fillMonth(sender) {
    debugger;
    $.getJSON("/PayRoll/DailyFandFSalaryProcessing/GetMonth", { PayrollGrp: $(sender).val() }, function (data) {

        $('#PayrollMonth_Code option').remove();
        $('#PayrollMonth_Code').append('<option value>--Select Month-- </option');
        debugger;
        for (var i = 0; i < data.length; i++) {
            $('#PayrollMonth_Code').append('<option value="' + data[i].code + '">' + data[i].name + '</option');
        }

    });
}


function getList() {
    debugger
    var i = 0;
    if (!validateById("#PayrollMonth_Code", 'Month'))
        i++;
    if (!validateById("#PayrollGroup_Code", 'Payrollgroup'))
        i++;
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
                url: '/PayRoll/DailyFandFSalaryProcessing/GetList',
                dataType: 'html',
                data: $('#BasicFilter').serialize() + "&Month=" + $('#PayrollMonth_Code').val() + "&PayrollGroup=" + $('#PayrollGroup_Code').val(),
                success: function (result) {
                    debugger
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();

                    }
                    else {
                        $('#FandFList').html("");
                        $('#FandFList').html(result);
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
                url: '/PayRoll/DailyFandFSalaryProcessing/GetList',
                dataType: 'html',
                data: $('#BasicFilter').serialize() + "&Month=" + $('#PayrollMonth_Code').val() + "&PayrollGroup=" + $('#PayrollGroup_Code').val(),
                success: function (result) {
                    debugger
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }
                    else {
                        $('#FandFList').html("");
                        $('#FandFList').html(result);
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
              $('#filterForm').removeClass('filter-apply');
        $.ajax({

            type: 'POST',
            url: '/PayRoll/DailyFandFSalaryProcessing/GetList',
            dataType: 'html',
            data: $('#BasicFilter').serialize() + "&Month=" + $('#PayrollMonth_Code').val() + "&PayrollGroup=" + $('#PayrollGroup_Code').val(),
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();

                }
                else {
                    $('#FandFList').html("");
                    $('#FandFList').html(result);
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

    input = document.getElementById("FandFEmpSearch");

    filter = input.value.toUpperCase();
    table = document.getElementById("FandFListTable");
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

        $(table).find(tbody).append("<tr  id='noDataImg' class='exit-page'><td colspan=9><div  style='text-align:center; display:flex; align-items:center; justify-content:center;height:45vh;'><img src='/images/nodata.png'/></div ></td></tr>");

    }
}
function FandFSalaryProcessing() {
    const table = document.getElementById("FandFListTable");
    const checkedCheckboxes = table.querySelectorAll('tbody input[type="checkbox"]:checked');

    if (checkedCheckboxes.length === 0) {
        swal.fire("Please select at least one checkbox to proceed.");
        return;
    }
    var data = $('#DailyFandFSPSave').serialize() + "&PayrollMonth=" + $('#PayrollMonth_Code').val() + "&PayrollGroup=" + $('#PayrollGroup_Code').val();
    $.ajax({
        type: 'POST',
        url: '/PayRoll/DailyFandFSalaryProcessing/GetDailyFandF_SalaryProcessing',
        dataType: 'json',
        data: data,
        success: function (result) {
            debugger;
            if (result.success == true) {
                swal.fire(result.message).then(function () {
                    getList();
                });
            }
            else if (result.message == null) {
                swal.fire('Salary Processed Successfully').then(function () {
                    var m = $('.negativeSalaryPopUp');
                    m.find('.modal-body').html('');
                    m.find('.modal-body').html(result);
                    m.modal('show');
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
