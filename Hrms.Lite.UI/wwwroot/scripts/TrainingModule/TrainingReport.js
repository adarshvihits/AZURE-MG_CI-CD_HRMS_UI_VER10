
function GetList() {
    debugger;
    var TrainingCode = $('#TrainingCode').val();
    var FYCode = $('#FYCode').val();
    var filter = $("#BasicFilter").serialize();
    $.ajax({

        type: 'POST',
        url: '/TrainingModule/TrainingReport/GetList',
        dataType: 'html',
        /*        data: "&TrainingCode=" +TrainingCode, "&FYCode=" :FYCode*/
        data: { TrainingCode: TrainingCode, FYCode: FYCode},
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#TrainingReportList').html("");
                $('#TrainingReportList').html(result);
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
function getTrainingCombo(data)
{
    $.ajaxSetup({
        async: false
    });
  
    var fycode = $(data).val();
    $.getJSON("/TrainingModule/TrainingReport/GetTrainingComboData", { FYCode: fycode }, function (data) {
        console.log(data);
        $('#TrainingCode option').remove();
        $('#TrainingCode').append('<option value>--Select Training--</option');

        for (var i = 0; i < data.length; i++) {
          
            $('#TrainingCode').append('<option value="' + data[i].code + '">' + data[i].name + '</option');
        }

    });
    GetList();
}
function ExportToExcel() {
    debugger;
    var TrainingCode = $('#TrainingCode').val();

    var TrainingName = $('#TrainingCode option:selected').text();
    var FYName = $('#FYCode option:selected').text();
    var FYName = $('#FYCode').val();
        var SettingsEmployeeListTableLength = $('#TrainingReportList_Count').val();
        if (SettingsEmployeeListTableLength > 0) {
            url = "/TrainingModule/TrainingReport/ExportToExcel?TrainingCode=" + $('#TrainingCode').val() + "&FYCode=" + $('#FYCode').val() + "&FYName=" + $('#FYCode option:selected').text() + "&TrainingName=" + $('#TrainingCode option:selected').text();
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
    }



