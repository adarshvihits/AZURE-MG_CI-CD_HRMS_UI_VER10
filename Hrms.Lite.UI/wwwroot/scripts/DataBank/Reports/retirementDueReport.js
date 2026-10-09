function GetList() {

    var fromdate = $('#FromDate').val();
    var todate = $('#ToDate').val();
    var TabIndex = $('input[name="BasedOn"]:checked').val();
    if (TabIndex == 2)
        $('.dteDiv').removeClass('hide')
    else
        $('.dteDiv').addClass('hide')
    if (TabIndex == 1 || (TabIndex == 2 && fromdate != '' && todate != '')) {
        debugger;
        $.ajax({
            type: 'POST',
            url: '/DataBank/RetirementDueReport/GetList?Tab=' + TabIndex + '&Fromdate=' + fromdate + '&Todate=' + todate,
            dataType: 'html',
            data: $('#BasicFilter').serialize(),
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('#ListView').html("");
                    $('#ListView').html(result);
                }
            },
            error: function (xhr, textStatus, errorThrown) {
                debugger
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);
            }
        });
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
       

    }
}


function ExportToExcel() {

    debugger;
    var fromdate = $('#FromDate').val();
    var todate = $('#ToDate').val();
    var TabIndex = $('input[name="BasedOn"]:checked').val();
    var tableLength = $('#JobTransitionDueList_Count').val();
    if (tableLength > 0) {
        url = "/DataBank/RetirementDueReport/ExportToExcel?Tab=" + TabIndex + '&FromDate=' + fromdate + '&ToDate=' + todate + '&data=' + $('#BasicFilter').serialize();
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }

}
