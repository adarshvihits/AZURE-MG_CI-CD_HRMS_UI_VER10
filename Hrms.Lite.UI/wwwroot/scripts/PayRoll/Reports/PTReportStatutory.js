function PTReportStatutoryPrint() {


    debugger;

    var month = $('#Month').val();

    var url = "/PayRoll/PTReportStatutory/GetPTReportStatutoryPrint?Month=" + month ;
    window.location.href = url;

}




function ApplyFilter(mode) {
    debugger;


    if (mode == 'apply') {
        if (filterApplicable >= 1) {
            CloseSlider('basicFilter');
            GetList();
            $('#filterForm').removeClass('filter-apply');
        }
        else {
            CloseSlider('basicFilter');
            GetList();
            $('#filterForm').addClass('filter-apply');
        }

    }
    else {
        filterApplicable = 1;
        $('.reset').val('');
        $('#filterForm').removeClass('filter-apply');
        GetList();


    }
}





function GetList() {
    debugger;
    var month = $('#PTGroupMonth').val();
  
    var filter = $("#BasicFilter").serialize();
    $.ajax({

        type: 'POST',
        url: '/PayRoll/PTReportStatutory/GetList',
        dataType: 'html',
        data: filter + "&Month=" + month ,
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#ListView').html("");
                $('#ListView').html(result);
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