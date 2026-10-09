
//function onDateChange() {
//    debugger;
//    var From = $('#From').val();
//    var To = $('#To').val();
//    var usercode = $('#UserCode').val();
//    var companycode = $('#CompanyCode').val();
//    var groupcompany = $('#GroupCompanyCode').val();

//    var fromMonth = $('#From option:selected').text();
//    var toMonth = $('#To option:selected').text();
//    if (From != "" && To != "") {
//        debugger;
//        var Link = $('#Url_Urllink').val();

//        var viewer = $("#PFForm10").data("telerik_ReportViewer");
//        viewer.reportSource({
//            report: viewer.reportSource().report,
//            parameters: {
//                //salaryperiod:0,

//                Url: Link + "/api/PFFormXReport/GetPFFormXReportDetails?mpFromCode=" + From + "&mpToCode=" + To + "&UserCode=" + usercode + "&CompanyCode=" + companycode + "&GroupCompanyCode=" + groupcompany,
//                from: fromMonth,
//                to: toMonth

//            }

//        });
//        $('#PFForm10').removeClass('hide');
//    }


//}



function ExportToPdf() {
    debugger;

    var param = $('#BasicFilter').serialize();
    //var a = param.Basic_Filter.Designation.Code
    var b = $('#Basic_Filter_Designation_Code').val();
    var From = $('#From').val();
    var To = $('#To').val();
    var usercode = $('#UserCode').val();
    var companycode = $('#CompanyCode').val();
    var groupcompany = $('#GroupCompanyCode').val();
    var usercode = $('#UserCode').val();
    var fromMonth = $('#From option:selected').text();
    var toMonth = $('#To option:selected').text();

    var Des = $('#Basic_Filter_Designation_Code').val();
    var Dept = $('#Basic_Filter_Department_Code').val();
    var Loc = $('#Basic_Filter_Location_Code').val();
    var Cat = $('#Basic_Filter_Category_Code').val();
    var Grade = $('#Basic_Filter_Grade_Code').val();
    var PayrollGp = $('#Basic_Filter_PayrollGroup_Code').val();
    var State = $('#Basic_Filter_State_Code').val();
    var region = $('#Basic_Filter_Region_Code').val();
    var Division = $('#Basic_Filter_Division_Code').val();
    var Section = $('#Basic_Filter_Section_Code').val();
    var EmpType = $('#Basic_Filter_EmploymentType_Code').val();
    var WorkLoc = $('#Basic_Filter_WorkLocation_Code').val();
    var wage = $('#Basic_Filter_WageGradeMaster_Code').val();
    var PfGp = $('#Basic_Filter_PFGroup_Code').val();
    var EsiGp = $('#Basic_Filter_ESIGroup_Code').val();
    var params = "&Designation=" + Des + "&Department=" + Dept + "&Location=" + Loc + "&Category=" + Cat + "&Grade=" + Grade + "&PayrollGp=" + PayrollGp + "&State=" + State + "Region=" + region + "&Division=" + Division + "&Section=" + Section + "&EmpType=" + EmpType + "&WorkLoc=" + WorkLoc + "&WageGrade=" + wage + "&PfGroup=" + PfGp + "&EsiGroup=" + EsiGp + "&mpFromCode=" + From + "&mpToCode=" + To + "&UserCode=" + usercode + "&CompanyCode=" + companycode + "&GroupCompanyCode=" + groupcompany;

    if (From != "" && To != "") {

        

        
        debugger;
        var Link = $('#Url_Urllink').val();

        var viewer = $("#PFForm10").data("telerik_ReportViewer");
        viewer.reportSource({
            report: viewer.reportSource().report,
            parameters: {
                //salaryperiod:0,

                Url: Link + "/api/PFFormXReport/GetPFFormXReportDetails?" + params,
                from: fromMonth,
                to: toMonth

            }

        });
        $('#PFForm10').removeClass('hide');
       

    }
}


function StartDateValidate() {
    debugger;


    var From = $('#From').val();
    var To = $('#To').val();

    if (From > To) {
        swal.fire("From date must be Less than To date");
        /*   $('#BreakingService_' + index + '__StartDate').val('');*/
       
        return true;
    }
    else
        return false;


}





function ApplyFilter(mode) {

    //$('#Report').data('kendoGrid').dataSource.read().then(function () {

    //    $('#Report').data('kendoGrid').dataSource.page(1);
    //});
    //$('#Report').data('kendoGrid').refresh();

    if (mode == 'apply') {

        if (filterApplicable >= 1) {

            CloseSlider('basicFilter');
            $('#filterForm').removeClass('filter-apply');
            ExportToPdf();
        }
        else {

            debugger;
            CloseSlider('basicFilter');
            $('#filterForm').addClass('filter-apply');
            ExportToPdf();
        }


    }

    else {
        filterApplicable = 1;
        $('.reset').val('');
        $('#Optional_Filter_EmploymentStatus_Code').val(0);
        $('#filterForm').removeClass('filter-apply');
        ExportToPdf();

    }


}