
function GetServiceRecordPdf(empcode,link) {
    debugger;
    var viewer = $("#servivceRecord").data("telerik_ReportViewer");
    viewer.reportSource({
        report: viewer.reportSource().report,
        parameters: {
          
         /*   EmpCode: parseInt(empcode)*/
            Url: link + "/api/ServiceRecordReport/GetServiceRecordJSON?EmpCode=" + empcode

        }

    });
    $('#servivceRecord').removeClass('hide');
}