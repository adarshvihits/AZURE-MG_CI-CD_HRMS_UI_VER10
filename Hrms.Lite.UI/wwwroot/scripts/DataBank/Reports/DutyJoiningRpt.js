function getList(sender) {
    debugger;
    showSpinner();
    /*   var selValue = $('#LoanTopUpDetails_LoanType_Code').val();*/
    var selValue = sender.value;
    //$('#LoanTopUpDetails_LoanType_Code').val(selValue);
    if (selValue != '') {
        $('.filterDropDown').val(selValue);
        $.ajax({

            type: 'GET',
            url: '/DataBank/DutyJoiningRpt/GetList',
            dataType: 'html',
            data: { },
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
    else {
        var url = "/DataBank/DutyJoiningRpt/Index";
        window.location.href = url;
    }
}

function ApplyFilter(mode) {
    debugger
    if (mode == 'apply') {


        if (filterApplicable >= 1) {
            fillData();
            CloseSlider('basicFilter');
            $('#filterForm').removeClass('filter-apply');
        }
        else {
            fillData();
            CloseSlider('basicFilter');
            $('#filterForm').addClass('filter-apply');
        }

    }
    else {
        filterApplicable = 1;
        $('.reset').val('');
        $('#filterForm').removeClass('filter-apply');
        fillData();

    }
}

function onDownloadClick(EmployeeCode) {
    debugger
   
    var url = "/DataBank/DutyJoiningRpt/GetDutyJoiningRptPDF?EmployeeCode=" + EmployeeCode ;
    window.location.href = url;

}


function onEmpInfoDownloadClick(EmployeeCode) {
    debugger

    var url = "/DataBank/DutyJoiningRpt/GetEmployeeInformationSheetPDF?EmployeeCode=" + EmployeeCode;
    window.location.href = url;

}