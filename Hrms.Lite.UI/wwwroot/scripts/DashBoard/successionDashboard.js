function onEmpClick(EmployeeGI, KeyPositionGI) {
    $.ajax({

        type: 'GET',
        url: '/DashBoard/SuccessionPlanningDashBoard/GetSuccessorList',
        dataType: 'html',
        data: { EmployeeGI: EmployeeGI, KeyPositionGI: KeyPositionGI },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
               
                var m = $('.notif_popup');
                m.find('.modal-body').html('');
                m.find('.modal-body').html(result);
                m.modal('show');
            }



        },
        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
            hideSpinner();

        }
    });

}
function ViewEmployee(EmpGI) {
    debugger;
    var FinancialYear = null;
    url = "/DataBank/EmployeeProfile/GetEmployeeProfileDetails?EmployeeGI=" + EmpGI + "&FinancialYear=" + FinancialYear + "&Breadcrumb=" + 'SuccessionPlanning' + "&BreadcrumbName=" + 'Succession Planning List';
    //window.location.href = url;
    window.open(url, '_blank');
}
function onClassificationDropdownChange() {
   
    var classification = $('#Classifn').val()
    $.ajax({
        type: 'GET',
        url: '/DashBoard/SuccessionPlanningDashBoard/getClassifnDiagram',
        dataType: 'html',
        data: { Type: classification },
        success: function (Data) {
            if ($($.parseHTML(Data)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                if (classification=="Department")
                    $(".ClassfnTitle").html('Exposure by Department');
                else $(".ClassfnTitle").html('Exposure by Region')
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