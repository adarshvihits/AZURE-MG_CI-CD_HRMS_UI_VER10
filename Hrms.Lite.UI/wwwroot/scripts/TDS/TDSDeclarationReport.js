function fillData() {
    debugger
    var AssessmentYear = $('#AssessmentYear_Code').val();

    $.ajax({

        type: 'GET',
        url: '/TDS/TDSDeclarationReport/GetList',
        dataType: 'html',
        data: { AssessmentYear: AssessmentYear },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#DeclarationList').html("");
                $('#DeclarationList').html(result);
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

function ShowPopup(CategoryCode) {
    debugger;
    var Category = CategoryCode;
    var AssessmentYear = $('#AssessmentYear_Code').val();
    $.ajax({

        type: 'GET',
        url: '/TDS/TDSDeclarationReport/GetTdsEmployeeDetails',
        dataType: 'html',
        data: { CategoryCode: Category, AssessmentYear: AssessmentYear },
        success: function (result) {
            debugger;
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {
                debugger;
                var m = $('.bd-example-modal-lg');
                m.find('.modal-body').html('');
                m.find('.modal-body').html(result);
                m.modal('show');
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

function TDSDetailRptExportToExcel() {
    debugger;
    var AssessmentYear = $('#AssessmentYear_Code').val();
    var AssessmentYearhName = $('#AssessmentYear_Code option:selected').text();

    //if (validateDropdowns()) {
    url = "/TDS/TDSDeclarationReport/TDSReportExportToExcel?AssessmentYear=" + AssessmentYear + "&AssessmentYearName=" + AssessmentYearhName;




            window.location.href = url;
       
        //else {
        //    swal.fire("No data found to export");
        //}
    
}