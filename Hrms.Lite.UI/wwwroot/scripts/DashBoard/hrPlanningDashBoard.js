
function onPlanningChange(sender) {
   
    var classification = $(sender).val()
    $.ajax({
        type: 'GET',
        url: '/DashBoard/HRPlanningDashBoard/getClassifnDiagram',
        dataType: 'html',
        data: { Period: classification },
        success: function (Data) {
            if ($($.parseHTML(Data)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
               
                $('.classfnChart').html('');
                $('.classfnChart').html(Data);
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
        }
    });
}