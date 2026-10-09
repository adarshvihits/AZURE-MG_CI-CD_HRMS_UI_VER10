function Proceed() {
    debugger
    var AssessmentYear = parseInt($('#AssessmentYear').val());
    var Location = parseInt($('#Location').val());
    var Department = parseInt($('#Department').val());
    var Designation = parseInt($('#Designation').val());
    var Category = parseInt($('#Category').val());
    var Grade = parseInt($('#Grade').val());
    var PayrollGroup = parseInt($('#PayrollGroup').val());

    var url = "/TDS/TDSVariationReport/GetVariationReportrPDF?AssessmentYear=" + AssessmentYear + "&Location=" + Location + "&Department=" + Department + "&Designation=" + Designation + "&Category=" + Category + "&Grade" + Grade + "&PayrollGroup" + PayrollGroup;
    window.location.href = url;
   
}
