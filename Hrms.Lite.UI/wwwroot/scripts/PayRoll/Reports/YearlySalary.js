

function fillEmployeeDropdown() {

    debugger
    $.ajaxSetup({
        async: false
    });


    $.getJSON("/PayRoll/YearlySalary/GetEmployeeVsFinancialYear", { FinancialYeaR: $("#FinancialYear_Code").val() }, function (data) {
        console.log(data);
        $('#EmpId option').remove();
        $('#EmpId').append('<option value>Search by Employee name/ID</option');
        debugger;
        for (var i = 0; i < data.length; i++) {
            $('#EmpId').append('<option value="' + data[i].code + '">' + data[i].name + '</option');
        }
        $('.selectpicker').selectpicker('refresh');
        debugger
        GetYearlySalaryDeatils();
    });
 
}
function GetYearlySalaryDeatils()
{
    debugger;
    var Code = $('#EmpId').val();
    var finCode = $('#FinancialYear_Code').val();
    showSpinner();
        $.ajax({

            type: 'GET',
            url: '/PayRoll/YearlySalary/GetYearlySalaryDetails',
            dataType: 'html',
            data: { finCode: finCode, EmpCode: Code },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }

                else {

                    $('#YearlySalaryDetailsList').html("");
                    $('#YearlySalaryDetailsList').html(result);
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

function ExportToExcel() {
    debugger;
    var FinYear = $('#FinancialYear_Code').val();
    var FinYearName = $('#FinancialYear_Code option:selected').text();
    var EmpCode = $('#EmpId').val();

    var TableLength = $('#Data_Count').val();
    if (TableLength > 0) {
        url = "/PayRoll/YearlySalary/ExportToExcel?FinYear=" + FinYear + "&FinYearName=" + FinYearName + "&EmpCode=" + EmpCode ;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}