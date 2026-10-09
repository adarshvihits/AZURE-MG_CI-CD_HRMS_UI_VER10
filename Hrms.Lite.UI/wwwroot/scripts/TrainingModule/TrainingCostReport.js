
function GetList() {
    debugger;
    var CategoryCode = $('#CategoryCode').val();
    var FYCode = $('#FYCode').val();
    var filter = $("#BasicFilter").serialize();
    $.ajax({

        type: 'POST',
        url: '/TrainingModule/TraningCostReport/GetList',
        dataType: 'html',
        /*        data: "&TrainingCode=" +TrainingCode, "&FYCode=" :FYCode*/
        data: { FYCode: FYCode, CategoryCode: CategoryCode},
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#TrainingCostReportList').html("");
                $('#TrainingCostReportList').html(result);
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
    var FYcode = $('#FYCode').val();

    var FYName = $('#FYCode option:selected').text();
    var CatName = $('#CategoryCode option:selected').text();
    var CatCode = $('#CategoryCode').val();
    var SettingsEmployeeListTableLength = $('#TrainingCostReportList_Count').val();
        if (SettingsEmployeeListTableLength > 0) {
            url = "/TrainingModule/TraningCostReport/ExportToExcel?CategoryCode=" + CatCode + "&FYCode=" + FYcode + "&FYName=" + FYName + "&CategoryName=" + CatName;
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
    }



