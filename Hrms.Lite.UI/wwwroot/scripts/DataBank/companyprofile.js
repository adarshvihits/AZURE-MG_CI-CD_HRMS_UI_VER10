function onGroupCompanyChange()
{
    $.ajax({
        type: 'POST',
        url: '/DataBank/CompanyProfile/GetCompanyProfile?GroupCompanyCode='+$('#GrpCmpCode').val(),
        dataType: 'html',
        data: $('#BasicFilter').serialize(),
        success: function (result) {
            debugger            
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#cProfile').html("");
                $('#cProfile').html(result);                
            }
            hideSpinner();
        },
        error: function (xhr, textStatus, errorThrown) {
            $('body').html(xhr.responseText);
        }
    });
}

function ExportToExcel() {

    debugger;


    url = "/DataBank/CompanyProfile/ExportToExcel?data=" + $('#BasicFilter').serialize() + "&GroupCompanyCode=" + $('#GrpCmpCode').val() + '&GroupCompanyName=' + $('#GrpCmpCode option:selected').text();
      
            window.location.href = url;
        
       
           
        
}
function ExportToExcelCompanyProfileList(CODE, Type, CodeName) {
    debugger;
    var tableLength = $('#CompanyProfileCount').val();

    if (tableLength > 0) {
        url = "/DataBank/CompanyProfile/ExportToExcelCompanyProfileList?CODE=" + CODE + "&Type=" + Type + "&CodeName=" + CodeName;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
    //url = "/DataBank/CompanyProfile/ExportToExcelCompanyProfileList?CODE=" + CODE + "&Type=" + Type + "&CodeName=" + CodeName;
    //window.location.href = url;
}

