function filldata() {
    debugger
    var EmployeeCode = $('#Employee_Code').val();
    var FinancialYearCode = $('#FinancialYear_Code').val();
   


    if (EmployeeCode !== '' && FinancialYearCode !== '' )
    {

        $.ajax({

            type: 'GET',
            url: '/TimeOffice/AnnualLeaveRegister/AnnualLeaveRegister',
            dataType: 'html',
            data: { EmployeeCode: EmployeeCode, FinancialYearCode: FinancialYearCode },

            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('#ListID').html("");
                    $('#ListID').html(result);
                    $('.countSummaryDetails').html($('#countSummary').val());
                }
              


            },
            error: function (xhr, textStatus, errorThrown) {
                debugger
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);
            

            }
        });

        return true;
    }
    else {
      
        return false;
    }

}



function ExportToExcel() {
    debugger;
    var EmployeeCode = $('#Employee_Code').val();
    var FinancialYearCode = $('#FinancialYear_Code').val();

    var FinancialYearName = $('#FinancialYear_Code option:selected').text();
    var EmployeeName = $('#Employee_Code option:selected').text();

    var leaveOpeningTableLength = $('#AnnualLeaveRegisterPagingList_Count').val();
        if (leaveOpeningTableLength > 0) {
            url = "/TimeOffice/AnnualLeaveRegister/AnnualLeaveRegisterExcel?EmployeeCode=" + EmployeeCode + "&FinancialYearCode=" + FinancialYearCode
                + "&FinancialYearName=" + FinancialYearName + "&EmployeeName=" + EmployeeName + "&totRecords=" + leaveOpeningTableLength;
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
   
}