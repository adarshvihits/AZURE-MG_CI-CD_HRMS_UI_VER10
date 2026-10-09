var filterApplicable = 1;
function onDesignationChange() {
    $('#Basic_Filter_Designation_Name').val($('#Basic_Filter_Designation_Code option:selected').text())
    if ($('#Basic_Filter_Designation_Code').val() == '') {
        filterApplicable += 1;
    }
    else {
        filterApplicable -= 1;
    }
}
function onDepartmentChange() {
    debugger;
    $('#Basic_Filter_Department_Name').val($('#Basic_Filter_Department_Code option:selected').text())
    if ($('#Basic_Filter_Department_Code').val() == '') {
        filterApplicable += 1;
    }
    else {
        filterApplicable -= 1;
    }
}
function onLocationChange() {
    $('#Basic_Filter_Location_Name').val($('#Basic_Filter_Location_Code option:selected').text())
    if ($('#Basic_Filter_Location_Code').val() == '') {
        filterApplicable += 1;
    }
    else {
        filterApplicable -= 1;
    }
}
function onCategoryChange() {
    $('#Basic_Filter_Category_Name').val($('#Basic_Filter_Category_Code option:selected').text())
    if ($('#Basic_Filter_Category_Code').val() == '') {
        filterApplicable += 1;
    }
    else {
        filterApplicable -= 1;
    }
}
function onGradeChange() {
    $('#Basic_Filter_Grade_Name').val($('#Basic_Filter_Grade_Code option:selected').text())
    if ($('#Basic_Filter_Grade_Code').val() == '') {
        filterApplicable += 1;
    }
    else {
        filterApplicable -= 1;
    }
}
function onPayRollGroup() {
    $('#Basic_Filter_PayrollGroup_Name').val($('#Basic_Filter_PayrollGroup_Code option:selected').text())
    if ($('#Basic_Filter_PayrollGroup_Code').val() == '') {
        filterApplicable += 1;
    }
    else {
        filterApplicable -= 1;
    }
}
function OnDivisionchange() {
    $('#Optional_Filter_Division_Name').val($('#Optional_Filter_Division_Code option:selected').text())
    if ($('#Basic_Filter_Division_Code').val() == '') {
        filterApplicable += 1;
    }
    else {
        filterApplicable -= 1;
    }
}
function onSectionChange() {
    $('#Optional_Filter_Section_Name').val($('#Optional_Filter_Section_Code option:selected').text())
    if ($('#Basic_Filter_Section_Code').val() == '') {
        filterApplicable += 1;
    }
    else {
        filterApplicable -= 1;
    }
}
function OnEmploymentTypeChange() {
    $('#Optional_Filter_EmploymentType_Name').val($('#Optional_Filter_EmploymentType_Code option:selected').text())
    if ($('#Basic_Filter_EmploymentType_Code').val() == '') {
        filterApplicable += 1;
    }
    else {
        filterApplicable -= 1;
    }
}
function OnWorkLocationChange() {
    $('#Optional_Filter_WorkLocation_Name').val($('#Optional_Filter_WorkLocation_Code option:selected').text())
    if ($('#Basic_Filter_WorkLocation_Code').val() == '') {
        filterApplicable += 1;
    }
    else {
        filterApplicable -= 1;
    }
}
function onWageGradeChange() {    
    $('#Optional_Filter_WageGradeMaster_Name').val($('#Optional_Filter_WageGradeMaster_Code option:selected').text())    
    if ($('#Basic_Filter_WageGradeMaster_Code').val() == '') {
        filterApplicable += 1;
    }
    else {
        filterApplicable -= 1;
    }
}
function OnEmploymentStatusChange() {
  
    $('#Optional_Filter_Employmentstatus_Name').val($('#Optional_Filter_EmploymentStatus_Code option:selected').text())
    if ($('#Basic_Filter_EmploymentStatus_Code').val() == '') {
        filterApplicable += 1;
    }
    else {
        filterApplicable -= 1;
    }
}
function OnGenderChange() {
    $('#Optional_Filter_Gender_Name').val($('#Optional_Filter_Gender_Code option:selected').text())
    if ($('#Basic_Filter_Gender_Code').val() == '') {
        filterApplicable += 1;
    }
    else {
        filterApplicable -= 1;
    }
}
function OnDateTypeChangechange() {
    $('#Date_Filter_DateType_Name').val($('#Date_Filter_DateType_Code option:selected').text())
    if ($('#Basic_Filter_DateType_Code').val() == '') {
        filterApplicable += 1;
    }
    else {
        filterApplicable -= 1;
    }
}