function GetList() {
  
    var pCode = $('#PayrollGrpCode').val();
    var mCode = $('#MonthCode').val();
    if (pCode != '' && mCode != '') {
        showSpinner();
        $.ajax({
            url: "/PayRoll/NegativeSalaryCorrection/GetList?PayrollGrpCode=" + pCode + "&MonthCode=" + mCode,
            type: "GET",
            dataType: "html",
            data: $('#BasicFilter').serialize(),
            success: function (result) {
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('#listView').html("");
                    $('#listView').html(result);
                }
                hideSpinner();
            },
            error: function (xhr, textStatus, errorThrown) {
                debugger
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);
                hideSpinner();

            }

        })
    }
  
}


function ApplyFilter(mode) {
    debugger

    if (mode == 'apply') {

        if (filterApplicable >= 1) {
            GetList();
            CloseSlider('basicFilter');
            $('#filterForm').removeClass('filter-apply');
        }
        else {
            GetList();
            CloseSlider('basicFilter');
            $('#filterForm').addClass('filter-apply');
        }

    }
    else {

        filterApplicable = 1;
        $('.reset').val('');
        $('#Optional_Filter_EmploymentStatus_Code').val(0);
        $('#filterForm').removeClass('filter-apply');
        GetList();
        CloseSlider('basicFilter');
    }
}

function ExportToExcel() {
    debugger;
    var month = $("#MonthCode").val();
    var MonthName = $("#MonthCode option:selected").text();
    var payroll = $('#PayrollGrpCode').val();
    var payrollGrpName = $("#PayrollGrpCode option:selected").text();

    var pendingTableLength = $('#NegativeSalaryList_Count').val();
    if (pendingTableLength > 0) {
        url = "/PayRoll/NegativeSalaryCorrection/ExportToExcel?MonthCode=" + month + "&MonthName=" + MonthName + "&PayrollGrpCode=" + payroll + "&PayrollGrpName=" + payrollGrpName + "&data=" + $('#BasicFilter').serialize();
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}