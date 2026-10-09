

function GetList() {
    debugger;
    var Month = $('#Month_Code').val();
    
    /* showSpinner();*/
    if (Month !== '')
    {
        var url = "/PayRoll/AccountStatement/GetList?Month=" + Month;
        $('#AccountStatementList').load(url, function () { /*hideSpinner();*/ });
    }
    else {

        return false;
    }
}


function ExportToExcel() {
    debugger;
    var Month = $('#Month_Code').val();
    
    var MonthName = $('#Month_Code option:selected').text();
    

    var TableLength = $('#AccountStatement_Count').val();
    if (TableLength > 0) {
        url = "/PayRoll/AccountStatement/ExportToExcel?Month=" + Month +"&MonthName=" + MonthName ;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}

function OnEmployeeCountClick(desc,Type,Code){
    debugger;
     var Month = $('#Month_Code').val();
    var MonthName = $('#Month_Code option:selected').text();

   
        debugger;
        $.ajax({
            type: 'GET',
            url: "/PayRoll/AccountStatement/GetEmployeeDetails?Month=" + Month + "&MonthName=" + MonthName + "&desc=" + desc + "&Type=" + Type + "&Code=" + Code,
            dataType: 'html',

            success: function (Data) {
                var m = $('.bd-example-modal-lg');
                m.find('.modal-body').html('');
                m.find('.modal-body').html(Data);
                

            },
            error: function (xhr, textStatus, errorThrown) {
                swal.fire(xhr.responseText);

            }
        });
}



function exportPdf() {
    debugger;
    var Month = $('#Month_Code').val();
    url = "/PayRoll/AccountStatement/ExportToPdf?Month=" + Month;
    window.location.href = url;
}


function ExportToExcelPopUp(Desc, Type, code)
{
    debugger;
    var Month = $('#Month_Code').val();
    var MonthName = $('#Month_Code option:selected').text();
    var TableLength = $('#EmployeeList_Count').val();

    if (TableLength > 0) {
        url = "/PayRoll/AccountStatement/PopupExportToExcel?Month=" + Month + "&MonthName=" + MonthName + "&desc=" + Desc + "&Type=" + Type + "&Code=" + code;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}