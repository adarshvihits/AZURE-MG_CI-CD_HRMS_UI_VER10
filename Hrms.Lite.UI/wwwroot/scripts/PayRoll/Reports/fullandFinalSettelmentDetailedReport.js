function FillData() {
    debugger
    var FROM = $('#FromMonth_Code').val();
    var TO = $('#ToMonth_Code').val();
    if (parseInt(FROM) > parseInt(TO)) {
        swal.fire("'From' month must be earlier than the 'To' month.");
        return false;
    } else {
        $.ajax({

            type: 'POST',
            url: "/PayRoll/FullandFinalDetailedReport/GetList?FROM=" + FROM + "&TO=" + TO,
            dataType: 'html',
            data: $('#BasicFilter').serialize(),
            success: function (result) {
                debugger
                CloseSlider('basicFilter');
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }

                else {
                    $('#EmployeeList').html("");
                    $('#EmployeeList').html(result);

                }

            },


            error: function (xhr, textStatus, errorThrown) {
                swal.fire(xhr.responseText);
            }
        });
    }
}


function ExportToExcel() {
    debugger;
    var FROM = $('#FromMonth_Code').val();
    var TO = $('#ToMonth_Code').val();
    var fromName = $('#FromMonth_Code option:selected').text();
    var ToName = $('#ToMonth_Code option:selected').text();
    var TableLength = document.getElementById("EmpGroupSummaryTable").rows.length - 1;

    if (TableLength > 0) {
        url = "/PayRoll/FullandFinalDetailedReport/ExportToExcel?FROM=" + FROM + "&Month1=" + fromName + "&TO=" + TO + "&Month2=" + ToName+  '&data=' + $('#BasicFilter').serialize() ;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}

function ApplyFilter(mode) {

    var search = $('#SearchId').val();

    $("#SearchId").val($('#FilterList_Code').val())
    if (mode == 'apply') {
        if (filterApplicable >= 1) {
            FillData();
            CloseSlider('basicFilter');
            $('#filterForm').removeClass('filter-apply');
        }
        else {

            FillData();
            CloseSlider('basicFilter');
            $('#filterForm').addClass('filter-apply');
        }
    }
    else {

        filterApplicable = 1;
        $('.reset').val('');
        $('#Optional_Filter_EmploymentStatus_Code').val(0);
        $('#filterForm').removeClass('filter-apply');
        FillData();

    }
}