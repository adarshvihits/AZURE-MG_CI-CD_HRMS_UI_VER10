function GetList() {
    debugger;
    var month = $('#Month').val();
    var Deduction = $('#Deduction').val();
    var filter = $("#BasicFilter").serialize();
    $.ajax({

        type: 'POST',
        url: '/PayRoll/LWFReport/GetList',
        dataType: 'html',
        data: filter + "&Month=" + month + "&DeductionCode=" + Deduction,
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#LWFListID').html("");
                $('#LWFListID').html(result);
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



function LWFReportPrint() {


    debugger;

    var month = $('#Month').val();
    var Deduction = $('#Deduction').val();

 
    var url = "/PayRoll/LWFReport/GetLWFReportPrint?Month=" + month + "&DeductionCode=" + Deduction ;
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




function ExportToExcel() {
    debugger;
    var Month = $('#Month').val();
    var Deduction = $('#Deduction').val();
    var filter = $('#BasicFilter').serialize();
    var MonthName = $('#Month option:selected').text();




    var PARAM = 'data=' + filter + '&Month=' + Month + '&DeductionCode=' + Deduction+ '&MonthName=' + MonthName;
    var TableLength = $('#LWFReportList_Count').val();
    if (TableLength > 0) {
        url = '/PayRoll/LWFReport/ExportToExcel?' + PARAM + "&TotalRows=" + TableLength;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}



