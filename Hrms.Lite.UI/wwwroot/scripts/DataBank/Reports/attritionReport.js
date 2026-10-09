function getList(sender) {
    debugger;
    
    var url = "/DataBank/AttritionReport/GetAttritionReport";
    var calculation = $('#Calculation').val();
    var grooupby =$('#GroupBy').val();
    var from = $('#From').val();
    var to = $('#To').val();
    if (calculation != "" && grooupby != "" && from != "" && to != "") {
        /*$('#AttritionList').load(url, { From: from, To: to, groupbyCode: grooupby, calculationCode: calculation }, function () { });*/

        $.ajax({

            type: 'POST',
            url: '/DataBank/AttritionReport/GetAttritionReport',
            dataType: 'html',
            data: { From: from, To: to, groupbyCode: grooupby, calculationCode: calculation },

            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('#AttritionList').html("");
                    $('#AttritionList').html(result);
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
    if (calculation == 1)
        $('#equationID').html('Formula Used : No. Of employees that left workforce / Average no. of employees');
    else if (calculation == 2)
        $('#equationID').html('Formula Used : No. Of employees that left workforce / Opening no. of employees');
    else
        $('#equationID').html('Formula Used : No. Of employees that left workforce / Closing no. of employees');
    //var url = "/DataBank/AttritionReport/GetAttritionReport?From=" + from + "&To=" + to + "&groupbyCode=" + $(sender).val() +"&calculationCode="+calculation;
    //window.location.href = url;
}

function exportToexcel() {
    debugger;
    var from = $('#From').val();
    var to = $('#To').val();
    var calculation = $('#Calculation').val();
    var grooupby = $('#GroupBy').val();
    var calculationname = $('#Calculation option:selected').text();
    var grooupbyname = $('#GroupBy option:selected').text();
    if (!($('#AttritionTable').length))
        swal.fire("No data found to export");
    var TableLength = $('#Rows_Count').val();
    if (TableLength > 0) {
        url = '/DataBank/AttritionReport/ExportToexcel?From=' + from + '&To=' + to + '&groupbyName=' + grooupbyname + '&calculatioName=' + calculationname + '&GroupBy=' + grooupby + '&Calculation=' + calculation;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}
