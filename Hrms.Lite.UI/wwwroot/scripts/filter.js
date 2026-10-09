var filterApplicable = 1;
function onDesignationChange() {
    debugger
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
function onRegionFilterChange() {
    debugger
    $('#Basic_Filter_Region_Name').val($('#Basic_Filter_Region_Code option:selected').text())
    if ($('#Basic_Filter_Region_Code').val() == '') {
        filterApplicable += 1;
    }
    else {
        filterApplicable -= 1;
    }
}
function onStateFilterChange() {
    $('#Basic_Filter_State_Name').val($('#Basic_Filter_State_Code option:selected').text())
    if ($('#Basic_Filter_State_Code').val() == '') {
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
function OnDateTypeChange() {
    debugger;
    var type = $('#Date_Filter_DateType_Code').val();
    if (type == 0 || type == null) {
        $('#fromDate').addClass('hide');
        $('#toDate').addClass('hide');
    }
    else {
        $('#fromDate').removeClass('hide');
        $('#toDate').removeClass('hide');
    }
    $('#Date_Filter_DateType_Name').val($('#Date_Filter_DateType_Code option:selected').text())
    if ($('#Basic_Filter_DateType_Code').val() == '') {
        filterApplicable += 1;
    }
    else {
        filterApplicable -= 1;
    }
}



function onPfGroupFilterChange() {
    $('#Basic_Filter_PFGroup_Name').val($('#Basic_Filter_PFGroup_Code option:selected').text())
    if ($('#Basic_Filter_PFGroup_Code').val() == '') {
        filterApplicable += 1;
    }
    else {
        filterApplicable -= 1;
    }
}


function onEsiGroupFilterChange() {
    $('#Basic_Filter_ESIGroup_Name').val($('#Basic_Filter_ESIGroup_Code option:selected').text())
    if ($('#Basic_Filter_ESIGroup_Code').val() == '') {
        filterApplicable += 1;
    }
    else {
        filterApplicable -= 1;
    }
}





function onAgeAboveOrBelowChange()
{
    debugger
    if ($('#Basic_Filter_AgeAboveOrBelow').val() == '') {
        filterApplicable += 1;
    }
    else {
        filterApplicable -= 1;
    }
}

function onYSAboveOrBelowChange()
{
    debugger
    if ($('#Basic_Filter_YSAboveOrBelow').val() == '') {
        filterApplicable += 1;
    }
    else {
        filterApplicable -= 1;
    }
}

function OnAgeValChange() {
    debugger
    if ($('#Advance_Filter_AgeAboveBelow').val() == '') {
        filterApplicable += 1;
    }
    else {
        filterApplicable -= 1;
    }
}
function OnYosValChange() {
    debugger
    if ($('#Advance_Filter_YrsOfService').val() == '') {
        filterApplicable += 1;
    }
    else {
        filterApplicable -= 1;
    }
}


function onAgeClick(val) {
    debugger;
    //$('#Advance_Filter_AgeFrom').val('');
    //$('#Advance_Filter_AgeTo').val('');
    AgeText = $('#Advance_Filter_AgeAboveBelow option:selected').text();
    $('#Advance_Filter_AgeAboveOrBelow').val(AgeText);
    if (val.value == "") {
        $('#age1Div').addClass('hide');
        $('#ageToDiv').addClass('hide');
    }
    if (val.value == 1 || val.value == 2) {
        $('#age1Div').removeClass('hide');
    }
    if (val.value == 3) {
        $('#ageToDiv').removeClass('hide');
        $('#age1Div').removeClass('hide');



    }
    else {
        $('#ageToDiv').addClass('hide');
        $('#ageDiv').removeClass('col-md-2');
        $('#ageDiv').addClass('col-md-2');
    }
}
function onYOSclick(val) {
    debugger;
    //$('#Advance_Filter_YrsOfExperience').val('');
    //$('#Advance_Filter_YSTo').val('');
    YosText = $('#Advance_Filter_YrsOfService option:selected').text();
    $('#Advance_Filter_YSAboveOrBelow').val(YosText);

    if (val.value == "") {
        $('#yosDiv').addClass('hide');
        $('#yostwoDiv').addClass('hide');
    }
    if (val.value == 1 || val.value == 2) {
        $('#yosDiv').removeClass('hide');
    }
    if (val.value == 3) {

        $('#yostwoDiv').removeClass('hide');
        $('#yosDiv').removeClass('hide');

       
    }
    else {
        $('#yostwoDiv').addClass('hide');

        $('#yosDiv').removeClass('col-md-2');
       
        $('#yosDiv').addClass('col-md-2');
    }
}

function checkFilterApplied() {
    if ($('#Basic_Filter_Designation_Code').val() != '' || $('#Basic_Filter_Department_Code').val() != '' ||
        $('#Basic_Filter_Location_Code').val() != '' || $('#Basic_Filter_Category_Code').val() != '' ||
        $('#Basic_Filter_Grade_Code').val() != '' || $('#Basic_Filter_PayrollGroup_Code').val() != '' ||
        $('#Basic_Filter_Region_Code').val() != '' || $('#Basic_Filter_State_Code').val() != '' ||
        $('#Optional_Filter_Division_Code').val() != '' || $('#Optional_Filter_Section_Code').val() != '' ||
        $('#Optional_Filter_EmploymentType_Code').val() != '' || $('#Optional_Filter_WorkLocation_Code').val() != '' ||
        $('#Optional_Filter_WageGradeMaster_Code').val() != '' || $('#Optional_Filter_Gender_Code').val() != '')
    {
        $('#filterForm').addClass('filter-apply');
    }
    else { 
        $('#filterForm').removeClass('filter-apply');
    }
}