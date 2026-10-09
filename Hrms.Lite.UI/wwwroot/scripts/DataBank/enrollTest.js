var CTC = 0;
var RdiffDays = 0;
var Req = 0;
var selectedTab = '';
var duplicationData;
function onNextClick(tabName, StepperName) {
    debugger
    selectedTab = StepperName;
    if (tabName == "Enroll_jobdetails") {
        var i = 0;
        if (!validateById('#PreEnrollment_JobDetails_Category_Code', 'category')) {            
            $('#PreEnrollment_JobDetails_Category_Code').focus();
            i++;
        }
        if (!validateById('#PreEnrollment_JobDetails_Grade_Code', 'grade')) {            
            $('#PreEnrollment_JobDetails_Grade_Code').focus();
            i++;
        }
        if (!validateById('#PreEnrollment_JobDetails_Designation_Code', 'designation')) {            
            $('#PreEnrollment_JobDetails_Designation_Code').focus();
            i++;
        }
        if (!validateById('#PreEnrollment_JobDetails_Department_Code', 'department')) {            
            $('#PreEnrollment_JobDetails_Department_Code').focus();
            i++;
        }
        if (!validateById('#PreEnrollment_JobDetails_Location_Code', 'location')) {            
            $('#PreEnrollment_JobDetails_Location_Code').focus();
            i++;
        }
        if (!validateById('#PreEnrollment_JobDetails_PayrollGroup_Code', 'payroll')) {            
            $('#PreEnrollment_JobDetails_PayrollGroup_Code').focus();
            i++;
        }
       
        //else if ($('#PreEnrollment_BasicInfo_RenewalDate').val() == "") {
        //    $("#tabTwoTitle").addClass("active");
        //    $("#tabTwo").addClass("active");
        //    $("#tabTwo").css("display", "block");
        //    $("#dor").html("This field is required");
        //    $('#PreEnrollment_BasicInfo_RenewalDate').focus();
        //    return false;
        //}
        //if (RdiffDays < 15) {            
        //    swal.fire("Renewal Date less than 15 days. Do you want to continue?");            
        //    $('#PreEnrollment_BasicInfo_RenewalDate').focus();            
        //}
        if (!validateById('#PreEnrollment_JobDetails_NoticePeriod', 'noticeperiod')) {            
            $('#PreEnrollment_JobDetails_NoticePeriod').focus();
            i++;
        }
        if (i == 0) {
            if ($('#PreEnrollment_BasicInfo_RenewalDate').val() == '') {
                validateDOR(document.getElementById('PreEnrollment_BasicInfo_RenewalDate'), 'dor');
            }
            else {

                if (RdiffDays != 0) {                    

                    validateDOR(document.getElementById('PreEnrollment_BasicInfo_RenewalDate'), 'dor');
                    if (Req == 0) {
                        $('#Enroll_compensation').removeClass('hide');
                        $('#Enroll_address').addClass('hide');
                        $('#Enroll_jobdetails').addClass('hide');
                        $('#Enroll_personal').addClass('hide');
                        $('#Enroll_attendance').addClass('hide');
                        $('#Enroll_payment').addClass('hide');
                        $('#Enroll_classification').addClass('hide');
                        $('#Enroll_generateEmpId').addClass('hide');
                        $('#Enroll_leave').addClass('hide');
                    }
                    


                }
                else {

                    $('#Enroll_compensation').removeClass('hide');
                    $('#Enroll_address').addClass('hide');
                    $('#Enroll_jobdetails').addClass('hide');
                    $('#Enroll_personal').addClass('hide');
                    $('#Enroll_attendance').addClass('hide');
                    $('#Enroll_payment').addClass('hide');
                    $('#Enroll_classification').addClass('hide');
                    $('#Enroll_generateEmpId').addClass('hide');
                    $('#Enroll_leave').addClass('hide');
                }

            }

            }

        
    }
    else if (tabName == "Enroll_personal") {

        var i = 0;
        if ($("#PreEnrollment_FiledValidations_1__Applicability").val() != "False") {
            if ($("#PreEnrollment_BasicInfo_File_FileData").val() == "" && $("#PreEnrollment_BasicInfo_File_AbsoluteUri").val() == "") {
                swal.fire('Candidate photo is required');
                i++;
            }
        }
        if (!validateEmail(document.getElementById('PreEnrollment_PermanentAddress_MailID'), 'email','PreEnrollment_FiledValidations_0__Applicability')) {
            $('#PreEnrollment_PermanentAddress_MailID').focus();
            i++;
        }
        if (!validateMobile('PreEnrollment_PermanentAddress_MobileNo', 'mobile')) {
            $('#PreEnrollment_PermanentAddress_MobileNo').focus();
            i++;
        }
        if (!validateDOJ(document.getElementById('PreEnrollment_BasicInfo_AcceptedDOJ'), 'doj',1)) {
            $('#PreEnrollment_BasicInfo_AcceptedDOJ').focus();
            $('#PreEnrollment_BasicInfo_AcceptedDOJ').datepicker('hide');
            i++;            
        }
        if (!validateById('#PreEnrollment_BasicInfo_FullName', 'fullname')) {            
            $('#PreEnrollment_BasicInfo_FullName').focus();
            i++;            
        }                     
        if (!validatePanEnroll('pan','PreEnrollment_FiledValidations_3__Applicability')) {
            $('#PreEnrollment_BasicInfo_PanNo').focus();
            i++;
        }
        if (!validateAadharNew('hdnAadhar', 'aadhar', document.getElementById("hdnAadhar").value)) {
            $('#PreEnrollment_BasicInfo_AdharNo').focus();
            i++;
        }
        //if (!validateAadharEnroll('PreEnrollment_BasicInfo_AdharNo', 'aadhar','PreEnrollment_FiledValidations_2__Applicability')) {
        //    $('#PreEnrollment_BasicInfo_AdharNo').focus();
        //    i++;
        //}
        if (!validateById('#PreEnrollment_BasicInfo_FathersName', 'father')) {

            $('#PreEnrollment_BasicInfo_FathersName').focus();
            i++;
        }
        if (!validateById('#PreEnrollment_BasicInfo_Gender_Code', 'gender')) {

            $('#PreEnrollment_BasicInfo_Gender').focus();
            i++;
        }
        if (!validateDOB(document.getElementById('PreEnrollment_BasicInfo_DOB', 'dob'))) {
            $('#PreEnrollment_BasicInfo_DOB').focus();
            $('#PreEnrollment_BasicInfo_DOB').datepicker('hide');
            i++;            
        }                       
                
        if (i == 0) {
          

                $.ajax({
                    type: 'GET',
                    async: false,
                    url: '/DataBank/Enrollment/AlreadyExistCheck',
                    dataType: 'json',
                    data: { EmployeeGI: $('#EnrollGUID').val(), Mobile: $("#PreEnrollment_PermanentAddress_MobileNo").val(), Pan: $("#PreEnrollment_BasicInfo_PanNo").val(), Aadhar: document.getElementById("hdnAadhar").value  },
                    success: function (data) {
                        duplicationData = data;
                        if (data.alreadyExists_OnBoarding != null) {
                            var aadhar = 0;
                            for (let i = 0; i < data.alreadyExists_OnBoarding.length; i++) {
                                if (data.alreadyExists_OnBoarding[i].code == 3) {
                                    aadhar = 1;
                                    $.ajax({
                                        type: 'GET',
                                        async: false,
                                        url: '/DataBank/PreEnrollmentTest/GetAadharDuplication',
                                        dataType: 'html',
                                        data: { AadharNo: document.getElementById("hdnAadhar").value  },
                                        success: function (result) {
                                            var m = $('#aadharDuplication');
                                            m.find('.modal-body').html('');
                                            m.find('.modal-body').html(result);
                                            m.modal('show');
                                            $('#popupHeader').html(data.alreadyExists_OnBoarding[i].message);
                                        },
                                        error: function (xhr, textStatus, errorThrown) {
                                            swal.fire(xhr.responseText);

                                        }
                                    });

                                }
                            }
                            if (aadhar == 0) {
                                var msg = alertMessage(data.alreadyExists_OnBoarding);
                                swal.fire({
                                    text: msg,
                                    icon: 'warning',
                                    showCancelButton: true,
                                    confirmButtonColor: '#3085d6',
                                    cancelButtonColor: '#d33',
                                    confirmButtonText: 'Ok'
                                }).then((result) => {
                                    if (result.isConfirmed) {
                                        DuplicationOkClick();
                                    }
                                })
                            }


                        }
                        else {
                            $('#Enroll_compensation').addClass('hide');
                            $('#Enroll_address').addClass('hide');
                            $('#Enroll_jobdetails').removeClass('hide');
                            $('#Enroll_personal').addClass('hide');
                            $('#Enroll_attendance').addClass('hide');
                            $('#Enroll_payment').addClass('hide');
                            $('#Enroll_classification').addClass('hide');
                            $('#Enroll_generateEmpId').addClass('hide');
                            $('#Enroll_leave').addClass('hide');
                        }
                    },
                    error: function (xhr, textStatus, errorThrown) {
                        swal.fire(xhr.responseText);

                    }
                });
         
            
        }
        if (!validateById('#PreEnrollment_BasicInfo_Position_Code', 'position')) {
            $('#PreEnrollment_BasicInfo_Position_Code').focus();
            i++;
        }
    }
    else if (tabName == "Enroll_compensation") {
        debugger
        var i = 0;
        if (!validateById('#PreEnrollment_Compensation_ApprovedGross', 'salary')) {
            $('#PreEnrollment_Compensation_ApprovedGross').focus();
            i++;
        }
        if ($("#PreEnrollment_Compensation_CTC").val() <= 0 || $("#PreEnrollment_Compensation_CTC").val() == null) {            
            swal.fire("Candidate salary amount is zero. Do you want to continue?");
        }
        //if ($("#PreEnrollment_Compensation_OfferedSalary").val() != '') {

        //    if (parseFloat($("#PreEnrollment_Compensation_OfferedSalary").val()) != parseFloat($("#PreEnrollment_Compensation_GrossSalary").val())) {
        //    $('#PreEnrollment_Compensation_OfferedSalary').focus();
        //        swal.fire("Offered salary and Gross Salary not same!");
        //    i++;
        //}
        //}
        if ($("#PreEnrollment_Compensation_CTC").val() > 21000 && $("#PreEnrollment_Compensation_ESI").is(':checked')) {            
            swal.fire("ESI Applicable Allowance is greater than 21000. So please remove ESI applicability");
            i++;
        }
        if (i == 0) {
            $('#Enroll_compensation').addClass('hide');
            $('#Enroll_address').removeClass('hide');
            $('#Enroll_jobdetails').addClass('hide');
            $('#Enroll_personal').addClass('hide');
            $('#Enroll_attendance').addClass('hide');
            $('#Enroll_payment').addClass('hide');
            $('#Enroll_leave').addClass('hide');
            $('#Enroll_classification').addClass('hide');
            $('#Enroll_generateEmpId').addClass('hide');
        }

    }
    else if (tabName == "Enroll_address") {
        var i = 0;

        if (!validateById('#PreEnrollment_PermanentAddress_House', 'house')) {           
            $('#PreEnrollment_PermanentAddress_House').focus();
            i++;
        }
        if (!validateById('#PreEnrollment_PermanentAddress_Street', 'street')) {            
            $('#PreEnrollment_PermanentAddress_Street').focus();
            i++;
        }
        if (!validateById('#PreEnrollment_PermanentAddress_City', 'city')) {            
            $('#PreEnrollment_PermanentAddress_City').focus();
            i++;
        }
        if (!validatePin('pin')) {

            $('#PreEnrollment_PermanentAddress_PinCode').focus();
            i++;
        }
      

        if (!validateById('#PreEnrollment_PermanentAddress_Country_Code', 'country')) {            
            $('#PreEnrollment_PermanentAddress_Country').focus();
            i++;
        }
        if (!validateById('#PreEnrollment_PermanentAddress_State_Code', 'state')) {            
            $('#PreEnrollment_PermanentAddress_State').focus();
            i++;
        }
        if (!validateById('#PreEnrollment_PermanentAddress_District_Code', 'district')) {            
            $('#PreEnrollment_PermanentAddress_District').focus();
            i++;
        }

        if (i == 0) {
            $('#Enroll_compensation').addClass('hide');
            $('#Enroll_compensation').addClass('hide');
            $('#Enroll_address').addClass('hide');
            $('#Enroll_jobdetails').addClass('hide');
            $('#Enroll_personal').addClass('hide');
            $('#Enroll_attendance').removeClass('hide');
            $('#Enroll_leave').addClass('hide');
            $('#Enroll_payment').addClass('hide');
            $('#Enroll_classification').addClass('hide');
            $('#Enroll_generateEmpId').addClass('hide');
        }

    }

    else if (tabName == "Enroll_attendance") {
        var i = 0;

        if (!validateById('#AttendanceSettings_ShiftType_Code', 'shifttype')) {            
            if (!validateById('#AttendanceSettings_ShiftDetailsIds', 'shiftdetails'))
            i++;
        }
        if ($("#AttendanceSettings_ShiftType_Code").val() == 1 && $('#sselect').val() == "") {            
            $("#sselect").addClass('is-invalid');
            $("#shiftdetails").addClass('error-message');
            $("#shiftdetails").html("This field is required");
            $('#sselect').focus();
            i++;
        }
        if ($("#AttendanceSettings_ShiftType_Code").val() == 2 && $("#AttendanceSettings_ShiftDetailsIds").val() == "") {            

            validateShiftDetails(document.getElementById('AttendanceSettings_ShiftDetailsIds'), 'shiftdetails', 'sDetails');
            i++;
        }
    
        if ($("#AttendanceSettings_ShiftType_Code").val() == 3 && $("#AttendanceSettings_ShiftDetailsIds").val() == "") {
         
            validateShiftDetails(document.getElementById('AttendanceSettings_ShiftDetailsIds'), 'shiftdetails', 'sDetails');
            i++;
        }
        if (!validateById('#AttendanceSettings_PunchingApplicability_Code', 'punchingAppl')) {
            i++;
        }
        if (!validateById('#AttendanceSettings_ShiftCircle_Code', 'shiftcircle')) {
            i++;
        }
        if ($("#AttendanceSettings_PunchingApplicability_Code").val() == 1)
        {
            if (!validatePunchingMethod(document.getElementById('AttendanceSettings_PunchingMethodIDs'), 'punchingmethod', 'pMethod')) {
                i++;
            }
        }
     
        if (!validateById('#AttendanceSettings_HolidayGroup_Code', 'holidayGroup')) {           
            i++;
        }
        if ($("#PreEnrollment_FiledValidations_4__Applicability").val() != "False") {
            if (!validateById('#AttendanceSettings_OffdaySettings_Code', 'OffdaySettings')) {

                i++;
            }
        }
       
        if (i == 0) {         
          
            $('#Enroll_compensation').addClass('hide');
            $('#Enroll_address').addClass('hide');
            $('#Enroll_jobdetails').addClass('hide');
            $('#Enroll_personal').addClass('hide');
            $('#Enroll_attendance').addClass('hide');
            $('#Enroll_payment').addClass('hide');
            $('#Enroll_classification').addClass('hide');
            $('#Enroll_generateEmpId').addClass('hide');
            $('#Enroll_leave').removeClass('hide');
            
        }


    }
    else if (tabName == "Enroll_leave")
    {
        //var i = 0;
        //if (!validateById('#AttendanceSettings_LeaveGroup_Code', 'leaveGrp')) {
        //    i++;
        //}       
        //if ($('.activeCheckbox:checked').length == 0) {
        //    swal.fire("Please Select Atleast one Leave");
        //    i++;
        //}
        //if ($('#AttendanceSettings_LeaveGroup_Code').val() != '' && $("#ELeaveGrid input[type=checkbox]:checked").length == 0) {
        //    i++;
        //    swal.fire("Please select atleast one Leave");
        //}
        /*if (i == 0) {*/
            $('#Enroll_compensation').addClass('hide');
            $('#Enroll_address').addClass('hide');
            $('#Enroll_jobdetails').addClass('hide');
            $('#Enroll_personal').addClass('hide');
            $('#Enroll_attendance').addClass('hide');
            $('#Enroll_payment').removeClass('hide');
            $('#Enroll_classification').addClass('hide');
            $('#Enroll_generateEmpId').addClass('hide');
            $('#Enroll_leave').addClass('hide');
        //}
    }
    else if (tabName == "Enroll_payment") {
        var i = 0;
        var AccNum = '';
        if (!validateById('#StatutoryClassification_PaymentMode_Code', 'payment')) {            
            $('#StatutoryClassification_PaymentMode_Code').focus();
            i++;
        }
        if ($('#StatutoryClassification_PaymentMode_Code').val() != 2) {
            AccNum = $('#StatutoryClassification_AccountNo').val();
            if (!validateById('#StatutoryClassification_Bank_Code', 'bankname')) {
                $('#StatutoryClassification_Bank_Code').focus();
                i++;
            }
            if (!validateById('#StatutoryClassification_AccountNo', 'accno')) {
                $('#StatutoryClassification_AccountNo').focus();
                i++;
            }
            if (!validateById('#StatutoryClassification_IFSC', 'ifsc')) {
                $('#StatutoryClassification_IFSC').focus();
                i++;
            }

        }

        if (i == 0) {
            $.getJSON("/DataBank/EnrollmentTest/AlreadyExistCheck", { AccountNo: AccNum,EmployeeGI: $('#EnrollGUID').val(), PFNumber: $("#StatutoryClassification_PFNumber").val(), ESINumber: $("#StatutoryClassification_ESINumber").val(), UANNumber: $("#StatutoryClassification_UANNumber").val() }, function (data) {

                debugger;
                if (data.alreadyExists_OnBoarding != null) {
                    var msg = alertMessage(data.alreadyExists_OnBoarding);
                    if (data.alreadyExists_OnBoarding[0].code == 9) {
                        var msg = data.alreadyExists_OnBoarding[0].message;
                        console.log(data);

                        swal.fire({
                            text: msg,
                            icon: 'warning',

                            confirmButtonColor: '#3085d6',
                            cancelButtonColor: '#d33',
                            confirmButtonText: 'Ok'
                        }).then((result) => {
                            debugger;
                            if (result.isConfirmed) {
                                debugger;
                                return false;
                            }

                        })

                    }
                    else {
                        swal.fire({
                            text: msg,
                            icon: 'warning',
                            showCancelButton: true,
                            confirmButtonColor: '#3085d6',
                            cancelButtonColor: '#d33',
                            confirmButtonText: 'Ok'
                        }).then((result) => {
                            if (result.isConfirmed) {
                                debugger;
                                $('#Enroll_compensation').addClass('hide');
                                $('#Enroll_address').addClass('hide');
                                $('#Enroll_jobdetails').addClass('hide');
                                $('#Enroll_personal').addClass('hide');
                                $('#Enroll_attendance').addClass('hide');
                                $('#Enroll_payment').addClass('hide');
                                $('#Enroll_leave').addClass('hide');
                                //$('#Enroll_classification').removeClass('hide');
                                $('#Enroll_generateEmpId').removeClass('hide');
                            }
                        })
                    }

                   

                }
                else {
                    debugger;
                    $('#Enroll_compensation').addClass('hide');
                    $('#Enroll_address').addClass('hide');
                    $('#Enroll_jobdetails').addClass('hide');
                    $('#Enroll_personal').addClass('hide');
                    $('#Enroll_attendance').addClass('hide');
                    $('#Enroll_payment').addClass('hide');
                    $('#Enroll_leave').addClass('hide');
                    //$('#Enroll_classification').removeClass('hide');
                    $('#Enroll_generateEmpId').removeClass('hide');
                }
            });           
        }


    }
    else if (tabName == "Enroll_classification") {

        var i = 0;
        if ($("#AddOnClassification_DivisionExists").val() == 'True') {
            if (!validateById('#PreEnrollment_JobDetails_Division_Code', 'division')) {
                $('#AddOnClassification_DivisionExists').focus();
                i++;
            }
        }
        if ($("#AddOnClassification_SectionExists").val() == 'True') {
            if (!validateById('#PreEnrollment_JobDetails_Section_Code', 'section')) {
                i++;
            }
        }
        if ($("#AddOnClassification_EmploymentTypeExists").val() == 'True') {
            if (!validateById('#PreEnrollment_JobDetails_EmploymentType_Code', 'EmpType')) {
                i++;
            }
        }
        if ($("#AddOnClassification_WorkLocationExists").val() == 'True') {
            if (!validateById('#PreEnrollment_JobDetails_WorkLocation_Code', 'WrkLoc')) {
                i++;
            }
        }
        if ($("#AddOnClassification_WageGradeExists").val() == 'True') {
            if (!validateById('#PreEnrollment_JobDetails_WageGrade_Code', 'WageGrd')) {
                i++;
            }
        }
        if (i == 0) {
            $('#Enroll_compensation').addClass('hide');
            $('#Enroll_address').addClass('hide');
            $('#Enroll_jobdetails').addClass('hide');
            $('#Enroll_personal').addClass('hide');
            $('#Enroll_attendance').addClass('hide');
            $('#Enroll_payment').addClass('hide');
            $('#Enroll_classification').addClass('hide');
            $('#Enroll_leave').addClass('hide');
            $('#Enroll_generateEmpId').removeClass('hide');
        }


    }

}


function validatePin(id) {
    debugger;
    var pin = $('#PreEnrollment_PermanentAddress_PinCode').val();
    //if (val.value == null || val.value == 'undefined') {

    //    pin = '';
    //}
    //else {

    //pin = val.value;
    //}
    if (pin == "") {
        $("#" + id + "").addClass('error-message');
        $('#PreEnrollment_PermanentAddress_PinCode').addClass('is-invalid');
        $("#" + id + "").html("This field is required");
        return false;
    }
    else {
        if (pin.length == 6) {
            $("#" + id + "").removeClass('error-message');
            $('#PreEnrollment_PermanentAddress_PinCode').removeClass('is-invalid');
            $("#" + id + "").html("");
            return true;
        }
        else {
            $("#" + id + "").addClass('error-message');
            $('#PreEnrollment_PermanentAddress_PinCode').addClass('is-invalid');
            $("#pin").html("Please enter a valid PIN number");
            return false;
        }
    }
}


function onEditClick() {
    
    $('.editbtn').hide();
    $('#PreEnrollment_BasicInfo_File_FileData').removeAttr('disabled', false);
    $('#PreEnrollment_BasicInfo_Salutation_Code').removeAttr('disabled', false);
    $('#PreEnrollment_BasicInfo_FullName').removeAttr('disabled', false);
    $('#PreEnrollment_BasicInfo_AcceptedDOJ').removeAttr('disabled', false);
    $('#PreEnrollment_PermanentAddress_MobileNo').removeAttr('disabled', false);
    $('#PreEnrollment_PermanentAddress_MailID').removeAttr('disabled', false);
    moveCursorToEnd('PreEnrollment_BasicInfo_FullName');
}
function Save(sender) {
    debugger;
    $(sender).attr("disabled", true);
    var i = 0;
    var renewaldate = $('#PreEnrollment_BasicInfo_RenewalDate').val();
    var successUrl = "";
    if ($('#Breadcrumb').val()=="OnBoarding")
        successUrl = "/DataBank/OnBoarding/Index";
    else if ($('#Breadcrumb').val() == "Employee Activation Edit")
        successUrl = "/DataBank/EnrollmentTest/Index";
    else if ($('#Breadcrumb').val() == "Employee Rejoin")
        successUrl = "/DataBank/EmployeeRejoin/Index";
    else
        successUrl = "/Recruitment/JoinConfirmEmpActivation/EmployeeActivation"

    
    //if ($("#EmployeeSettings_ReportingManager_Code").val() == "") {        
    //    $('.rptMgr').addClass('is-invalid');
    //    $("#reportingmanager").addClass('error-message');
    //    $("#reportingmanager").html("This field is required");
    //    $('#EmployeeSettings_ReportingManager_Code').focus();
    //    i++;
    //}

    //company general settings
    if ($("#AutoGeneratedEmployeeID").val() == 'False') {
        if (!validateById('#EmployeeSettings_EmployeeID', 'empid')) {
            $('#EmployeeSettings_EmployeeID').focus();
            i++;
        }

        if ($("#PunchingIDSameasEmployeeID").val() == 0) {
            if (!validateById('#EmployeeSettings_PunchingID', 'punchingid')) {
                $('#EmployeeSettings_PunchingID').focus();
                i++;
            }
        }
    }

    else {
        if ($("#PunchingIDSameasEmployeeID").val() == 0)
        {
            if (!validateById('#EmployeeSettings_PunchingID', 'punchingid')) {
                $('#EmployeeSettings_PunchingID').focus();
                i++;
            }
        }
    }

    if ($("#PreEnrollment_FiledValidations_1__Applicability").val() != "False") {
        if ($("#PreEnrollment_BasicInfo_File_FileData").val() == "" && $("#PreEnrollment_BasicInfo_File_AbsoluteUri").val() == "") {
            swal.fire('Candidate photo is required');
            i++;
        }
    }
    if (!validateEmail(document.getElementById('PreEnrollment_PermanentAddress_MailID'), 'email', 'PreEnrollment_FiledValidations_0__Applicability')) {
        $('#PreEnrollment_PermanentAddress_MailID').focus();
        i++;
    }
    if (!validateMobile('PreEnrollment_PermanentAddress_MobileNo', 'mobile')) {
        $('#PreEnrollment_PermanentAddress_MobileNo').focus();
        i++;
    }
    if (!validateDOJ(document.getElementById('PreEnrollment_BasicInfo_AcceptedDOJ'), 'doj',1)) {
        $('#PreEnrollment_BasicInfo_AcceptedDOJ').focus();
        $('#PreEnrollment_BasicInfo_AcceptedDOJ').datepicker('hide');
        i++;

    }
    if (!validateById('#PreEnrollment_BasicInfo_FullName', 'fullname')) {
        $('#PreEnrollment_BasicInfo_FullName').focus();
        i++;
    }
    var SaveMsg = 'Do you want to Save?';
    if ($('#authorityTable tbody tr').length == 0) {
        SaveMsg = 'Authority is not assigned. Do you want to Save?'
    }
    
    if (i == 0) {
        debugger;
        $('#DOB_Password').val(_GetFormattedDateWithSlash($('#PreEnrollment_BasicInfo_DOB').val()));
        var url = "";
        if ($('#Breadcrumb').val() == "Employee Rejoin")
            url = "/DataBank/EmployeeRejoin/Create";
        else
            url = "/DataBank/EnrollmentTest/Create";

        swal.fire({
            text: SaveMsg,
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                var MPPersonalInfo = new FormData($('#enrollSave')[0]);
   
                MPPersonalInfo.append('Renewaldt', renewaldate);
                $.ajax({
                    type: 'POST',
                    url: url,
                    contentType: false,
                    processData: false,
                    dataType: 'json',
                    data: MPPersonalInfo,
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire("Saved Successfully").then(function () {
                                window.location.href = successUrl;
                            });

                        }
                        else if (result.message == "EMPLOYEE_ID_DUPLICATION_ERROR") {
                            swal.fire("Employee ID is already exists");
                            $(sender).attr("disabled", false);
                        }
                        else if (result.message == "PUNCHING_ID_DUPLICATION_ERROR") {
                            swal.fire("Punching ID is already exists");
                            $(sender).attr("disabled", false);
                        }
                        else {
                            swal.fire(result.message).then(function () {
                                $(sender).attr("disabled", false);
                            });
                        }

                    },
                    error: function (xhr, textStatus, errorThrown) {
                        document.body.id = "kt_body";
                        $('body').html(xhr.responseText);
                        $(sender).attr("disabled", false);
                    }
                });
            }
            else {
                $(sender).attr("disabled", false);
            }
        });
       
       
    }
    else {
        $(sender).attr("disabled", false);
    }
}

function onStepCircleClick(StepperName) {
    debugger;
    selectedTab = StepperName;
    if (StepperName == "Personal-Info") {
        $('#Enroll_compensation').addClass('hide');
        $('#Enroll_address').addClass('hide');
        $('#Enroll_jobdetails').addClass('hide');
        $('#Enroll_personal').removeClass('hide');
        $('#Enroll_attendance').addClass('hide');
        $('#Enroll_payment').addClass('hide');
        $('#Enroll_classification').addClass('hide');
        $('#Enroll_generateEmpId').addClass('hide');
        $('#Enroll_leave').addClass('hide');
    }
    if (StepperName == "Job-Details") {
        $('#Enroll_compensation').addClass('hide');
        $('#Enroll_address').addClass('hide');
        $('#Enroll_jobdetails').removeClass('hide');
        $('#Enroll_personal').addClass('hide');
        $('#Enroll_attendance').addClass('hide');
        $('#Enroll_payment').addClass('hide');
        $('#Enroll_classification').addClass('hide');
        $('#Enroll_generateEmpId').addClass('hide');
        $('#Enroll_leave').addClass('hide');
    }
    if (StepperName == "Compensation") {
        $('#Enroll_compensation').removeClass('hide');
        $('#Enroll_address').addClass('hide');
        $('#Enroll_jobdetails').addClass('hide');
        $('#Enroll_personal').addClass('hide');
        $('#Enroll_attendance').addClass('hide');
        $('#Enroll_payment').addClass('hide');
        $('#Enroll_classification').addClass('hide');
        $('#Enroll_leave').addClass('hide');
        $('#Enroll_generateEmpId').addClass('hide');
    }
    if (StepperName == "Address") {
        $('#Enroll_compensation').addClass('hide');
        $('#Enroll_address').removeClass('hide');
        $('#Enroll_jobdetails').addClass('hide');
        $('#Enroll_personal').addClass('hide');
        $('#Enroll_attendance').addClass('hide');
        $('#Enroll_payment').addClass('hide');
        $('#Enroll_classification').addClass('hide');
        $('#Enroll_generateEmpId').addClass('hide');
        $('#Enroll_leave').addClass('hide');
    }
    if (StepperName == "Attendance-Settings") {
        $('#Enroll_compensation').addClass('hide');
        $('#Enroll_address').addClass('hide');
        $('#Enroll_jobdetails').addClass('hide');
        $('#Enroll_personal').addClass('hide');
        $('#Enroll_attendance').removeClass('hide');
        $('#Enroll_payment').addClass('hide');
        $('#Enroll_classification').addClass('hide');
        $('#Enroll_generateEmpId').addClass('hide');
        $('#Enroll_leave').addClass('hide');
    }
    if (StepperName == "leave") {
        $('#Enroll_compensation').addClass('hide');
        $('#Enroll_address').addClass('hide');
        $('#Enroll_jobdetails').addClass('hide');
        $('#Enroll_personal').addClass('hide');
        $('#Enroll_attendance').addClass('hide');
        $('#Enroll_payment').addClass('hide');
        $('#Enroll_classification').addClass('hide');
        $('#Enroll_generateEmpId').addClass('hide');
        $('#Enroll_leave').removeClass('hide');
    }
    if (StepperName == "StatutoryAndPayment") {
        $('#Enroll_compensation').addClass('hide');
        $('#Enroll_address').addClass('hide');
        $('#Enroll_jobdetails').addClass('hide');
        $('#Enroll_personal').addClass('hide');
        $('#Enroll_attendance').addClass('hide');
        $('#Enroll_payment').removeClass('hide');
        $('#Enroll_classification').addClass('hide');
        $('#Enroll_generateEmpId').addClass('hide');
        $('#Enroll_leave').addClass('hide');
    }
    if (StepperName == "Classification") {
        $('#Enroll_compensation').addClass('hide');
        $('#Enroll_address').addClass('hide');
        $('#Enroll_jobdetails').addClass('hide');
        $('#Enroll_personal').addClass('hide');
        $('#Enroll_attendance').addClass('hide');
        $('#Enroll_payment').addClass('hide');
        $('#Enroll_leave').addClass('hide');
        $('#Enroll_classification').removeClass('hide');
        $('#Enroll_generateEmpId').addClass('hide');
    }
    if (StepperName == "GenerateEmpId") {
        $('#Enroll_compensation').addClass('hide');
        $('#Enroll_address').addClass('hide');
        $('#Enroll_jobdetails').addClass('hide');
        $('#Enroll_personal').addClass('hide');
        $('#Enroll_attendance').addClass('hide');
        $('#Enroll_payment').addClass('hide');
        $('#Enroll_classification').addClass('hide');
        $('#Enroll_leave').addClass('hide');
        $('#Enroll_generateEmpId').removeClass('hide');

    }
}



function onRemovePhotoClick() {
    debugger;
    $("#PreEnrollment_BasicInfo_File_AbsoluteUri").val("");
    $("#PreEnrollment_BasicInfo_File_IsNewUpload").val(true);
}

function fillPunchingMethodDropdown() {
    $.getJSON("/DataBank/Enrollment/GetPunchingMethod", function (data) {
        debugger;
        var opts = [];
        for (var i = 0; i < data.length; i++) {
            opts.push({
                label: data[i].text,
                value: data[i].value,
            })
        }
        var multi = new SelectPure(".punching-select", {
            options: opts,
            value: [],
            multiple: true,
            autocomplete: true,
            icon: "fa fa-times",
            onchange: value => { console.log(value); },
        });
    });
}

function fillSingleValueDropdown() {
    debugger;
    var bank = $('#StatutoryClassification_Bank_Code > option').length;
    if (bank == 2) {
        document.getElementById('StatutoryClassification_Bank_Code').selectedIndex = 1;
    }
    
  
  
    var shiftDetails = $('#AttendanceSettings_ShiftDetailsIds > option').length;
    if (shiftDetails == 2) {
        document.getElementById('AttendanceSettings_ShiftDetailsIds').selectedIndex = 1;
    }


}

function onCategoryChange(val) {
    debugger;
    validate(val, 'category')
    var categoryCode = val.value;
    var doj = $('#PreEnrollment_BasicInfo_AcceptedDOJ').val();
    var dob = $('#PreEnrollment_BasicInfo_DOB').val();
   
    $.getJSON("/DataBank/PreEnrollmentTest/GetRenewalDate", { CategoryCode: categoryCode, DOB: dob, DOJ: doj }, function (data) {
        debugger;
        $('#PreEnrollment_BasicInfo_RenewalDate').remove();
        $('#renewaldt').append('<input type = "text" id = "PreEnrollment_BasicInfo_RenewalDate" class="form-control kt_datepicker_2 renewaldatepicker" placeholder = "Renewal Date"  onchange = "validateDOR(this,\'dor\')" onclick = "$(\'.renewaldatepicker\').datepicker(\'show\')" >');
        $('#PreEnrollment_BasicInfo_RenewalDate').val(data);
        console.log(data);
        
    });
    $.getJSON("/DataBank/PreEnrollmentTest/GetRetirementType", { CategoryCode: categoryCode, DOB: dob, DOJ: doj }, function (data) {
        debugger;
        /*$('#PreEnrollment_BasicInfo_RetirementType').val(data);*/
        if (data == "DOB") {
            $('.renewaldivEnroll').addClass('hide');
        }
        else {
            $('.renewaldivEnroll').removeClass('hide');
        }
        console.log(data);

    });
}
function firstLetterCaps(val, id) {
    debugger;
    if (val.value == "") {
        $(val).addClass('is-invalid');
        $("#" + id).addClass('error-message').html("This field is required");
    } else {
        $(val).removeClass('is-invalid');
        $("#" + id).removeClass('error-message').html("");

        // Trim and format the value first
        val.value = val.value
            .replace(/(^\s*)|(\s*$)/gi, "") // removes leading and trailing spaces
            .replace(/[ ]{2,}/gi, " ")      // replaces multiple spaces with one space
            .replace(/\n +/, "\n");

        // Capitalize the first letter of each word
        val.value = val.value
            .split(' ')
            .map(word => word.charAt(0).toUpperCase() + word.substring(1).toLowerCase())
            .join(' ');

        return val.value;
    }
}


function validateDOJ(val, id, NextClick) {
    debugger;
    var i = 0;
    if (val.value == "") {
        $(val).addClass('is-invalid');
        $("#" + id + "").addClass('error-message');
        $("#" + id + "").html("This field is required");
        status = false;

    }
    else {
        var doj = new Date(val.value);
        currDate = new Date();
        $.getJSON("/DataBank/EditEmployeeInfo/CheckifAlreadySalaryProcessed", { Date: val.value }, function (data) {
            console.log(data);
            debugger
            if (data.success == false) {
                i++;
                swal.fire(data.message);


            }
            else {
                if (doj > currDate) {
                    const diffTime = Math.abs(currDate - doj);
                    const daysDiff = Math.ceil(diffTime / (1000 * 60 * 60 * 24));

                    if (daysDiff >= 30 && NextClick != 1) {
                        swal.fire("The entered Date of Joining is 30 days or more after current date.");
                        //$("#PreEnrollment_BasicInfo_AcceptedDOJ").val('');
                    }
                }
                $(val).removeClass('is-invalid');
                $("#" + id + "").removeClass('error-message');
                $("#" + id + "").html("");

            }

        });

    }
    if (i == 0)
        return true;
    else
        return false;
}


function validateMobile(val, id) {
    mobile = $('#' + val).val();
    if ($('#' + val).val() == "") {
        $("#" + id + "").addClass('error-message');
        $('#' + val).addClass('is-invalid');
        $("#" + id + "").html("This field is required");
        return false;
    }
    else {
        var filter = /^[1-9]{1}[0-9]{9}$/
        if (!filter.test(mobile)) {
            debugger;
            $("#" + id + "").addClass('error-message');
            $('#' + val).addClass('is-invalid');
            $("#" + id + "").html('Invalid Mobile number')

            return false;
        }
        else {
            $("#" + id + "").removeClass('error-message');
            $('#' + val).removeClass('is-invalid');
            $("#" + id + "").html("");
            //trim(val);
            return true;
        }
    }
}

function validateEmail(val, id, mandatory) {
    email = val.value;
    debugger;
    if ($("#" + mandatory + "").val() != "False") {
        if (val.value == "") {
            $("#" + id + "").addClass('error-message');
            $(val).addClass('is-invalid');
            $("#" + id + "").html("This field is required");
            return false;
        }
        else {
            filter = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,5})+$/
            if (!filter.test(email)) {
                debugger;
                $("#" + id + "").addClass('error-message');
                $(val).addClass('is-invalid');
                $("#" + id + "").html('Invalid Email ID')
            }
            else {
                $("#" + id + "").removeClass('error-message');
                $(val).removeClass('is-invalid');
                $("#" + id + "").html("");
                return true;
            }
        }
    }
    else {
        if (val.value != "") {
            filter = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,5})+$/
            if (!filter.test(email)) {
                debugger;
                $("#" + id + "").addClass('error-message');
                $(val).addClass('is-invalid');
                $("#" + id + "").html('Invalid Email ID')
            }

            else {
                $("#" + id + "").removeClass('error-message');
                $(val).removeClass('is-invalid');
                $("#" + id + "").html("");
                return true;
            }
        }
        else
            return true

    }
}





function validateDOB(val, id) {
    debugger;
    dateofbirth = $('#PreEnrollment_BasicInfo_DOB').val();
    if (val.value == "") {
        $("#" + id + "").addClass('error-message');
        $(val).addClass('is-invalid');
        $("#" + id + "").html("This field is required");
        return false;
    }
    else {
        debugger;

        var DOB = new Date(document.getElementById('PreEnrollment_BasicInfo_DOB').value);
        var DOBYear = Math.abs(DOB.getFullYear());

        var CurDate = new Date();
        var CurDateYear = Math.abs(CurDate.getFullYear())

        var years = parseInt(CurDateYear) - parseInt(DOBYear);

        var age = parseInt(years);
        var monthDifference = CurDate.getMonth() - DOB.getMonth();

        if (monthDifference < 0 || (monthDifference === 0 && CurDate.getDate() < DOB.getDate())) {
            age--;
        }
        if (age < 18) {
            $("#" + id + "").addClass('error-message');
            $(val).addClass('is-invalid');
            $("#" + id + "").html("Age must be greater than 18");
            return false;
        }
        else if (age > 58) {
            if (swal.fire("Candidate age greater than 58. Do you want to continue?")) {
                $("#" + id + "").removeClass('error-message');
                $(val).removeClass('is-invalid');
                $("#" + id + "").html("");
                return true;
            }
            else {
                val.value = "";
                return false;
            }
        }
        else {
            $("#" + id + "").removeClass('error-message');
            $(val).removeClass('is-invalid');
            $("#" + id + "").html("");            
            return true;
        }
    }
}
function validateAadharEnroll(val, id, mandatory) {
    aadhar = $('#' + val).val();
    if ($("#" + mandatory + "").val() != "False") {

        if ($('#' + val).val() == "") {
            $("#" + id + "").addClass('error-message');
            $('#' + val).addClass('is-invalid');
            $("#" + id + "").html("This field is required");
        }
        else {
            var filter = /^[2-9]{1}[0-9]{11}$/
            if (!filter.test(aadhar)) {
                debugger;
                $("#" + id + "").addClass('error-message');
                $('#' + val).addClass('is-invalid');
                $("#" + id + "").html('Invalid Aadhar number')
                return false;
            }
            else {
                $("#" + id + "").removeClass('error-message');
                $('#' + val).removeClass('is-invalid');
                $("#" + id + "").html("");

                return true;
            }
        }
    }
    else {
        var filter = /^[2-9]{1}[0-9]{11}$/
        if (!filter.test(aadhar)) {
            debugger;
            $("#" + id + "").addClass('error-message');
            $('#' + val).addClass('is-invalid');
            $("#" + id + "").html('Invalid Aadhar number')
            return false;
        }
        else {
            $("#" + id + "").removeClass('error-message');
            $('#' + val).removeClass('is-invalid');
            $("#" + id + "").html("");

            return true;
        }
        return true;
    }
}








function spacesNotAllowed(evt) {

    var ASCIICode = (evt.which) ? evt.which : evt.keyCode
    if (ASCIICode == 32) {
        swal.fire("White spaces not allowed");
        return false;
    }
    else {
        return true;
    }
}

//pan validation
function validatePanEnroll(id, mandatory) {
    debugger;
    var val = document.getElementById('PreEnrollment_BasicInfo_PanNo');
    if ($("#" + mandatory + "").val() != "False") {
        var pan = val.value.toUpperCase();
        if (val.value == "") {
            $("#" + id + "").addClass('error-message');
            $(val).addClass('is-invalid');
            $("#" + id + "").html("This field is required");
        }
        else {
            var panPat = /^([a-zA-Z]{5})(\d{4})([a-zA-Z]{1})$/;
            var code = /([C,P,H,F,A,T,B,L,J,G])/;
            var code_chk = pan.substring(3, 4);
            if (pan.search(panPat) == -1) {
                $("#" + id + "").addClass('error-message');
                $(val).addClass('is-invalid');
                $("#pan").html("Invalid Pan Number");
                return false;
            }
            else if (code.test(code_chk) == false) {
                $("#" + id + "").addClass('error-message');
                $(val).addClass('is-invalid');
                $("#pan").html("Invalid Pan Number");
                return false;
            }
            else {
                $("#" + id + "").removeClass('error-message');
                $(val).removeClass('is-invalid');
                $("#pan").html("");

                val.value = val.value.toUpperCase();

                return val.value;
            }
        }
    }
    else {
        if (val.value != "") {
            var val = document.getElementById('PreEnrollment_BasicInfo_PanNo');
            var pan = val.value.toUpperCase();

            var panPat = /^([a-zA-Z]{5})(\d{4})([a-zA-Z]{1})$/;
            var code = /([C,P,H,F,A,T,B,L,J,G])/;
            var code_chk = pan.substring(3, 4);
            if (pan.search(panPat) == -1) {
                $("#" + id + "").addClass('error-message');
                $(val).addClass('is-invalid');
                $("#pan").html("Invalid Pan Number");
                return false;
            }
            else if (code.test(code_chk) == false) {
                $("#" + id + "").addClass('error-message');
                $(val).addClass('is-invalid');
                $("#pan").html("Invalid Pan Number");
                return false;
            }

            else {
                $("#" + id + "").removeClass('error-message');
                $(val).removeClass('is-invalid');
                $("#pan").html("");

                val.value = val.value.toUpperCase();

                return val.value;
            }
        }
        else
            return true;

    }
}


function onGradeChange(sender, id) {
    debugger;
    var Code = $(sender).val();

    if (Code == "") {
        $("#" + id + "").addClass('error-message');
        $(sender).addClass('is-invalid');
        $("#" + id + "").html("This field is required");
    }
    else {
        $("#" + id + "").removeClass('error-message');
        $(sender).removeClass('is-invalid');
        $("#" + id + "").html("");
    }

    var url = "/DataBank/Enrollment/GetCompensationGrid";
    $(".Compensation").load(url, { GradeCode: Code }, function () {
    });
}





function validateDOR(val, id) {
    debugger;
    const doj = new Date($("#PreEnrollment_BasicInfo_AcceptedDOJ").val());
    const dor = new Date(val.value);
    if (doj > dor) {
        $(val).addClass('is-invalid');
        $("#" + id + "").addClass('error-message');
        $("#" + id + "").html("Renewal date should be greater than Joining date");
        Req = 1;
        return false;
    }
    if (val.value == "" || val.value == undefined) {
        $("#" + id + "").addClass('error-message');
        $(val).addClass('is-invalid');
        $("#" + id + "").html("This field is required");
        Req = 1;
        return false;
    }
    else {
        const diffTime = Math.abs(dor - doj);
        RdiffDays = Math.ceil(diffTime / (1000 * 60 * 60 * 24));        
        if (RdiffDays < 15) {

            swal.fire({
                text: "Renewal Date less than 15 days. Do you want to continue?",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Ok'
            }).then((result) => {
                if (result.isConfirmed) {
                    $("#" + id + "").removeClass('error-message');
                    $(val).removeClass('is-invalid');
                    $("#" + id + "").html("");
                    RdiffDays = 0;
                    return true;
                }
                else {
                    val.value = "";
                    RdiffDays = 1;
                    return false;
                }
            })
        }
        else {
            $("#" + id + "").removeClass('error-message');
            $(val).removeClass('is-invalid');
            $("#" + id + "").html("");
            Req = 0;
            return true;
        }

    }
}


function OnLeaveGroupChange(sender) {
    debugger;
    $.ajax({

        type: 'GET',
        url: '/DataBank/EnrollmentTest/GetEnrollLeaveOpeningDetails',
        dataType: 'html',
        data: { DOJ: $("#PreEnrollment_BasicInfo_AcceptedDOJ").val(), LeaveGroup: $(sender).val()},

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                debugger;
                $('.openingGrid').html("");
                $('.openingGrid').html(result);
                debugger;             
            }
            hideSpinner();


        },


        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
           

        }
    });}

function onShiftTypeChange(sender) {
    debugger;
    var value = $(sender).val();
    if (value == 1) {

        $('#singleselect').show();
        $('#multipleselect').hide();
        $("#sselect").prop('selectedIndex', 0);
        $('.sDetails').removeClass('is-invalid');
        $('#shiftdetails').removeClass('error-message');
        $('#shiftdetails').html("");
    }
    else if (value == 2) {
        $('#singleselect').hide();
        $('#multipleselect').show();
        $("#AttendanceSettings_ShiftDetailsIds").val('');
        $("#sselect").val('');
        $('#sselect').removeClass('is-invalid');
        $('#shiftdetails').removeClass('error-message');
        $('#shiftdetails').html("");
        $('.sDetails').removeClass('is-invalid');
    }
    else {
        $('#singleselect').hide();
        $('#multipleselect').show();
        $("#AttendanceSettings_ShiftDetailsIds").val('');
        $("#sselect").val('');
        $('#sselect').removeClass('is-invalid');
        $('#shiftdetails').removeClass('error-message');
        $('#shiftdetails').html("");
        $('.sDetails').removeClass('is-invalid');
    }
    $("#shifttype").removeClass('error-message');
    $("#AttendanceSettings_ShiftType_Code").removeClass('is-invalid');
    $("#shifttype").html("");
}


function onCountryChange(sender, id) {

    debugger
    var cid = $(sender).val();
    if (cid == "") {
        $("#" + id + "").addClass('error-message');
        $(sender).addClass('is-invalid');
        $("#" + id + "").html("This field is required");
    }
    else {
        $("#" + id + "").removeClass('error-message');
        $(sender).removeClass('is-invalid');
        $("#" + id + "").html("");
    }
    $.getJSON("/DataBank/Enrollment/GetState", { id: cid }, function (data) {

        $('#PreEnrollment_PermanentAddress_State_Code option').remove();
        $('#PreEnrollment_PermanentAddress_State_Code').append('<option value="">--Select--</option');

        for (var i = 0; i < data.length; i++) {
            $('#PreEnrollment_PermanentAddress_State_Code').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
        }

    });
    //Make Dtsrict Dropdwn Empty when no Country is Selected
    //if (cid == 0) {
    $.getJSON("/DataBank/Enrollment/GetDistrict", { id: 0 }, function (data) {

        $('#PreEnrollment_PermanentAddress_District_Code option').remove();
        $('#PreEnrollment_PermanentAddress_District_Code').append('<option value="">--Select--</option');

        for (var i = 0; i < data.length; i++) {
            $('#PreEnrollment_PermanentAddress_District_Code').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
        }
    });
    //}
}

function onStateChange(sender, id) {

    debugger;
    var cid = $(sender).val();
    if (cid == "" || cid == 0) {
        $("#" + id + "").addClass('error-message');
        $(sender).addClass('is-invalid');
        $("#" + id + "").html("This field is required");
    }
    else {
        $("#" + id + "").removeClass('error-message');
        $(sender).removeClass('is-invalid');
        $("#" + id + "").html("");
    }
    $.getJSON("/DataBank/Enrollment/GetDistrict", { id: cid }, function (data) {

        $('#PreEnrollment_PermanentAddress_District_Code option').remove();
        $('#PreEnrollment_PermanentAddress_District_Code').append('<option value="">--Select--</option');

        for (var i = 0; i < data.length; i++) {
            $('#PreEnrollment_PermanentAddress_District_Code').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
        }

    });
}
function onDistrictChange(sender, id) {
    debugger;
    var cid = $(sender).val();
    if (cid == "" || cid == 0) {
        $("#" + id + "").addClass('error-message');
        $(sender).addClass('is-invalid');
        $("#" + id + "").html("This field is required");
    }
    else {
        $("#" + id + "").removeClass('error-message');
        $(sender).removeClass('is-invalid');
        $("#" + id + "").html("");
    }
}

function onPaymentModeChange(val) {

    if (val.value == 2) {
        $('#bankdetails').hide();
        $("#PayMode").css("display", "none");
    }
    else {
        $('#bankdetails').show();
        $("#PayMode").css("display", "block");
    }
    
}
function onPunchingApplChange(val) {
    debugger
   
   
    if (val.value == 1) {
        $(".pun_MethodDiv").removeClass('hide');              
    }
    else {
        $(".pun_MethodDiv").addClass('hide');
       /* AttendanceSettings_PunchingMethodIDs.length = 0;*/
     
    }

}

function validateShiftDetails(val, id,divId) {
    debugger;
    if ($("#AttendanceSettings_ShiftType_Code").val() != 1) {

        if (val.value == "") {
            $('.' + divId).addClass('is-invalid');
            $("#" + id + "").addClass('error-message');
            $("#" + id + "").html("This field is required");
            return false
        }
        else {
            $('.' + divId).removeClass('is-invalid');
            $("#" + id + "").removeClass('error-message');
            $("#" + id + "").html("");
            return true;
        }
    }
    else {

        $("#shiftdetails").removeClass('error-message');
        $("#shiftdetails").html("");//$("#mselect").val() == ""
        return true;
    }
}

function validatePunchingMethod(val, id,divId) {
    debugger;
    if (val.value == "") {
        $('.' + divId).addClass('is-invalid');
        $("#" + id + "").addClass('error-message');
        $("#" + id + "").html("This field is required");
        return false;
    }
    else {
        $('.' + divId).removeClass('is-invalid');
        $("#" + id + "").removeClass('error-message');
        $("#" + id + "").html("");
        return true;
    }
}


function validateReportingManager(val, divId, id) {
    debugger;
    if (val.value == "") {
        $("." + divId + "").addClass('is-invalid');
        $("#" + id + "").addClass('error-message');
        $("#" + id + "").html("This field is required");
    }
    else {
        $("." + divId + "").removeClass('is-invalid');
        $("#" + id + "").removeClass('error-message');
        $("#" + id + "").html("");
    }
}




function alertMessage(data) {
    var msg = '';
    for (let i = 0; i < data.length; i++) {
        if (data[i].code == 1)
            msg = msg + "Mobile number";
        if (data[i].code == 2) {
            if (msg != '')
                msg = msg + ", Pan number";
            else
                msg = msg + "Pan number";
        }
        //if (data[i].code == 3) {
        //    if (msg != '')
        //        msg = msg + ", Aadhar number";
        //    else
        //        msg = msg + "Aadhar number";
        //}
        if (data[i].code == 6)
            msg = msg + "PF number";
        if (data[i].code == 7) {
            if (msg != '')
                msg = msg + ", UAN number";
            else
                msg = msg + "UAN number";
        }
        if (data[i].code == 8) {
            if (msg != '')
                msg = msg + ", ESI number";
            else
                msg = msg + "ESI number";
        }
    }


    msg = msg + " already exists. Do you want to continue?";

    return msg;
    //console.log(msg);

    //if (msg == "MOBILE_NUMBER_DUPLICATION_ERROR") {
    //    var msg = "Mobile number already exists. Do you want to continue?";
    //    return msg;
    //}
    //else if (msg == "ADHAAR_NUMBER_DUPLICATION_ERROR") {
    //    var msg = "Aadhar number already exists. Do you want to continue?";
    //    return msg;
    //}
    //else if (msg == "PAN_NUMBER_DUPLICATION_ERROR")
    //{
    //    var msg = "Pan number already exists. Do you want to continue?";
    //    return msg;
    //}
    //else {
    //    var msg = "Something went wrong";
    //    return msg;
    //}
}

function CTCCalculation() {
    debugger;
    var TotAmt = parseFloat("0");
    var TotGrossAmt = parseFloat("0");
    var TotCTCAmt = parseFloat("0");
    var TotDedAmt = parseFloat("0");
    var TakeHome = parseFloat("0");
    $('.AllowanceAmount').each(function (i, obj) {
        var Amt = parseFloat($("#PreEnrollment_Compensation_FixedAllowance_" + i + "__Amount").val());

      
        if (isNaN(Amt)) {
            Amt = parseFloat("0");
        }
        TotAmt = TotAmt + Amt;

        TotGrossAmt = TotGrossAmt + Amt;

        $("#GrossSal").html(TotGrossAmt);
        $("#PreEnrollment_Compensation_GrossSalary").val(TotGrossAmt);
    });
    $('.ProvisionsAmount').each(function (i, obj) {
        var Amt = parseFloat($("#PreEnrollment_Compensation_FixedProvisions_" + i + "__Amount").val());
        var CtcApp = parseInt($("#PreEnrollment_Compensation_FixedProvisions_" + i + "__CtcApplicable").val());
        debugger;
        if (isNaN(Amt) || Amt == 'undefined') {
            Amt = parseFloat("0");
        }
        if (CtcApp == 2) {
            TotCTCAmt = TotCTCAmt + Amt;
        }
        

       
    });

    $("#ctc").html(TotCTCAmt + TotGrossAmt);
    $("#PreEnrollment_Compensation_CTC").val(TotCTCAmt + TotGrossAmt);

    $('.DeductionsAmount').each(function (i, obj) {//Total Dedcutions
        var Amt = parseFloat($("#PreEnrollment_Compensation_FixedDeduction_" + i + "__Amount").val());
        debugger;
        if (isNaN(Amt)) {
            Amt = parseFloat("0");
        }
        TotDedAmt = TotDedAmt + Amt;

        $("#totDeductions").html(TotDedAmt);
        //$("#Compensation_CTC").val(TotCTCAmt + TotGrossAmt);

    });

    TakeHome = TotGrossAmt - TotDedAmt;//Take Home

    $("#takeHome").html(TakeHome);
    //$("#Compensation_CTC").val(TotCTCAmt + TotGrossAmt);

}

function onSalaryStructureChange() {
    debugger
    var code = $("#PreEnrollment_Compensation_SalaryStructure_Code").val();
    var GrossSalary = parseFloat($('#PreEnrollment_Compensation_ApprovedGross').val());
    var CTC = parseFloat($('#PreEnrollment_Compensation_CTC').val());
    var VDAGroup = $('#PreEnrollment_JobDetails_VDAGroup_Code').val();
    var PreEnrollCode = $('#PreEnrollment_PreEnrollCode').val();
    if (validateById("#PreEnrollment_Compensation_SalaryStructure_Code", "salaryStruct") && validateById("#PreEnrollment_Compensation_CTC", 'salary')) {
        var url = "/DataBank/EnrollmentTest/GetSalaryStructureAlwDedGrid";
        $('#Enroll_compensation').load(url, { PreEnrollCode: PreEnrollCode, SalaryStructureCode: code, CTC: CTC, VDAGroup: VDAGroup}, function () { CTCCalculation(); });

    }
    else {
        $('.compensationTables tbody tr').remove();
        $("#totDeductions").html('');
        $("#takeHome").html('');
        $("#ctc").html('');
        $("#PreEnrollment_Compensation_CTC").val(0);
        $("#GrossSal").html('');
        $("#PreEnrollment_Compensation_GrossSalary").val(0);
    }
    //window.location.href = url + "?SalaryStructureCode=" + code + "&GrossSalary" + GrossSalary;
}

function process(input) {
    debugger;
    let value = input.value;
    let numbers = value.replace(/[^0-9]/g, "");
    input.value = numbers;
}

function CheckProfilePic(e) {
    debugger;

    debugger;
    var size = $(e)[0].files[0].size;
    var filename = $(e)[0].files[0].name;
    var fileExtension = ['jpeg', 'jpg'];
    //if (size > 2048 * 1024) {
    //    swal.fire('File size cannot exceeds 2 Mb').then(function () {
    //        $(e).val('');
    //        return false;
    //    });
    //}
    if ($.inArray($(e).val().split('.').pop().toLowerCase(), fileExtension) == -1) {
        swal.fire("Incorrect format. Only jpeg and jpg images are allowed").then(function () {
            $(e).val('');
            return false;
        });
    }
    else {
        return true;
    }

}
function validateProfilePhoto() {
    debugger
    if ($("#PreEnrollment_BasicInfo_File_FileData").val() == null)
        swal.fire("No file Uploaded");

    else {
        if (CheckProfilePic(document.getElementById('PreEnrollment_BasicInfo_File_FileData'))) {
            var photo = new FormData($('#enrollSave')[0]);
            //console.log(photo);
            $.ajax({

                type: 'POST',
                url: '/DataBank/EnrollmentTest/SaveProfileToFolder?SelectedTab=' + selectedTab + '&Breadcrumb=' + $('#Breadcrumb').val(),
                dataType: 'html',
                data: photo,
                contentType: false,
                processData: false,
                success: function (Data) {
                    var m = $('.bd-example-modal-lg');
                    m.find('.modal-body').html('');
                    m.find('.modal-body').html(Data);
                    m.modal('show');
                },
                error: function (xhr, textStatus, errorThrown) {
                    if ($($.parseHTML(xhr.responseText)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }
                    else {
                        document.body.id = "kt_body";
                        $('body').html(xhr.responseText);
                    }
                }
            });
        }
    }


}

function fillNoticeperiod(sender) {
    debugger
    $.getJSON("/DataBank/PreEnrollmentTest/GetNoticeperiod", { DesignationCode: $(sender).val() }, function (data) {

        $("#PreEnrollment_JobDetails_NoticePeriod").val(data);

    });
}
function madatoryCheck() {
    debugger;
    if ($("#PreEnrollment_FiledValidations_2__Applicability").val() == "False")
        $('#adharMandatory').html("");
    if ($("#PreEnrollment_FiledValidations_3__Applicability").val() == "False")
        $('#panMandatory').html("");
}


function DuplicationOkClick() {
    if (duplicationData.alreadyExists_OnBoarding.length <= 1) {
        $('#Enroll_compensation').addClass('hide');
        $('#Enroll_address').addClass('hide');
        $('#Enroll_jobdetails').removeClass('hide');
        $('#Enroll_personal').addClass('hide');
        $('#Enroll_attendance').addClass('hide');
        $('#Enroll_payment').addClass('hide');
        $('#Enroll_classification').addClass('hide');
        $('#Enroll_generateEmpId').addClass('hide');
        $('#Enroll_leave').addClass('hide');
    }
    else {

        var msg = alertMessage(duplicationData.alreadyExists_OnBoarding);
        swal.fire({
            text: msg,
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                $('#Enroll_compensation').addClass('hide');
                $('#Enroll_address').addClass('hide');
                $('#Enroll_jobdetails').removeClass('hide');
                $('#Enroll_personal').addClass('hide');
                $('#Enroll_attendance').addClass('hide');
                $('#Enroll_payment').addClass('hide');
                $('#Enroll_classification').addClass('hide');
                $('#Enroll_generateEmpId').addClass('hide');
                $('#Enroll_leave').addClass('hide');
            }
        })
    }
    CloseDuplicationPopup()
}

function CloseDuplicationPopup() {
    var m = $('#aadharDuplication');
    m.find('.modal-body').html('');
    m.modal('hide');;
}

function GetAuthorityDetails() {
    $.ajax({
        type: 'GET',
        url: '/DataBank/EnrollmentTest/GetAuthorityDetails',
        dataType: 'html',        
        success: function (result) {
            var m = $('.addAuthorityPopup');
            m.find('.modal-body').html('');
            m.find('.modal-body').html(result);
            m.modal('show');
        },
        error: function (xhr, textStatus, errorThrown) {
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);
            }
        }
    });
}

function AddToList() {
    valid = 0;
    for (let i = 0; i < $('#addleveltable tbody tr').length; i++) {
        if ($('#RequestVsAuthoritySettings_' + i + '__AuthorityCode').val() == '' || $('#RequestVsAuthoritySettings_' + i + '__AuthorityCode').val() == '0') {
            $('#RequestVsAuthoritySettings_' + i + '__AuthorityCode').val('');
            validateByIdForSearchDropdn('#RequestVsAuthoritySettings_' + i + '__AuthorityCode', 'authCode_' + i, '.authCodeDropdn_' + i);
            valid++;
        }
    }
    if (valid == 0) {
    
        $('.reqCode').val($('#RequestType_Code').val())
        $('.reqName').val($('#RequestType_Code option:selected').text())
        // var popupData = $('#reqVsAuthSave').serialize();
        var popupData = $('#addleveltable :input').serialize();
        //var savedData = $('#authTable').serialize();
        var savedData = $("#authorityTable :input").serialize();

        $.ajax({
            type: 'POST',
            url: '/DataBank/EnrollmentTest/AddToList?ReqCode=' + $('#RequestType_Code').val(),
            dataType: 'html',
            data: popupData + savedData,
            success: function (result) {
                debugger;
                $('#reqvsAuthListView').html('');
                $('#authorityTableDiv').html('');
                $('#authorityTable tbody').empty();
                $('#reqvsAuthListView').html(result);

                var m = $('.addAuthorityPopup');
                m.find('.modal-body').html('');
                m.modal('hide');

                //$("#authorityTable").clone().appendTo("#authorityTableDiv");
                //$('#authorityTableDiv table').attr('id', '')
            },
            error: function (xhr, textStatus, errorThrown) {
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    document.body.id = "kt_body";
                    $('body').html(xhr.responseText);
                }
            }
        });
    }
}

function requestTypeDropdnChange(sender) {

    var tableLen = $('#addleveltable tbody tr').length;
    for (let i = 0; i < tableLen; i++) {
        if (i != 0)
            $('#row_OI_' + i).remove();
        else {
           
            $('#RequestVsAuthoritySettings_' + i + '__AuthorityName').val('');
            $('#RequestVsAuthoritySettings_' + i + '__AuthorityCode').val('');
            $('.authCodeDropdn_0').val('')
            $('.authCodeDropdn_0').selectpicker('refresh');
        }
    }
     
    if (parseInt($(sender).val()) == 1) {
        $('#objAdd_0').addClass('hide');
    }
    else {
        $('#objAdd_0').removeClass('hide');
    }
}

function OnAuthorityDropdownChange(index, sender) {
    debugger;
    $('#RequestVsAuthoritySettings_' + index + '__AuthorityName').val($(sender).find("option:selected").text());
    $('#RequestVsAuthoritySettings_' + index + '__AuthorityCode').val($(sender).val());
    validateByIdForSearchDropdn('#RequestVsAuthoritySettings_' + index + '__AuthorityCode', 'authCode_' + index, '.authCodeDropdn_' + index);
   
}


function AddNewResultRow(tableId) {
    debugger;

    var valid = 0;
    for (let i = 0; i < $('#addleveltable tbody tr').length; i++) {
        if ($('#RequestVsAuthoritySettings_' + i + '__AuthorityCode').val() == '' || $('#RequestVsAuthoritySettings_' + i + '__AuthorityCode').val() == '0') {
            $('#RequestVsAuthoritySettings_' + i + '__AuthorityCode').val('');
            validateByIdForSearchDropdn('#RequestVsAuthoritySettings_' + i + '__AuthorityCode', 'authCode_' + i, '.authCodeDropdn_' + i);
            valid++;
        }
    }
    if (valid == 0) {
        $('.authoritydropdn').selectpicker('destroy');
        var count = 0;
        count = $('#' + tableId + ' tr[id^="row_OI_"]').length;
        if (count < 5) {
            var template = $('#' + tableId + ' #row_OI_0').clone(); // clone the row
            $.each(template.find('input, textarea'), function () {
                var name = $(this).attr('name');
                if (typeof name != 'undefined') {
                    name = name.replace(/\[0\]./g, '[' + count + '].');
                    $(this).attr('name', name);
                }
                var id = $(this).attr('id');
                if (typeof id != 'undefined') {
                    id = id.replace(/_0__/g, '_' + count + '__');
                    $(this).attr('id', id);
                }
                //---------------------------------For row Increment When onkeyup
                var onkeyup = $(this).attr('onkeyup');
                if (onkeyup !== undefined) {
                    onkeyup = onkeyup.replace(/\(0\)/g, '(' + count + ')');
                    $(this).attr('onkeyup', onkeyup);
                }
                //---------------------------------------For row Increment When onchange
                var onchange = $(this).attr('onchange');
                if (onchange !== undefined) {
                    onchange = onchange.replace(/\(0\)/g, '(' + count + ')');
                    $(this).attr('onchange', onchange);
                }
                //---------------------------------------
            });
            $.each(template.find('select'), function () {
                var name = $(this).attr('name');
                name = name.replace(/\[0\]./g, '[' + count + '].');
                $(this).attr('name', name);

                var id = $(this).attr('id');
                id = id.replace(/_0__/g, '_' + count + '__');
                $(this).attr('id', id);
                //---------------------------------------For row Increment When onchange
                var onchange = $(this).attr('onchange');
                if (onchange !== undefined) {
                    onchange = onchange.replace(/\(0,/g, '(' + count + ',');
                    debugger
                    $(this).attr('onchange', onchange);
                }
                //---------------------------------------
            });

            $.each(template.find('a'), function () {
                var onclick = $(this).attr('onclick');
                onclick = onclick.replace(/\(0\)/g, '(' + count + ')');
                $(this).attr('onclick', onclick);
            });
            /*template.find('td[id=grid_slno]').text(count + 1);*/
            $.each(template.find('.addObj'), function () {
                $(this).attr("id", "objAdd_" + count);
            });
            $.each(template.find('.delObj'), function () {
                $(this).attr("id", "objDel_" + count);
            });
            $.each(template.find('#lvlName_0'), function () {
                $(this).attr("id", "lvlName_" + count);
            });
            $.each(template.find('#authCode_0'), function () {
                $(this).attr("id", "authCode_" + count);
            });
            $.each(template.find('.authCodeDropdn_0'), function () {
                $(this).removeClass('authCodeDropdn_0');
                $(this).addClass('authCodeDropdn_' + count);
            });
            $('#' + tableId + '').append('<tr id="row_OI_' + count + '">' + template.html() + '  </tr>');
            //------------to clear when new row---------
            $('#' + tableId + ' tr[id="row_OI_' + count + '"] input[type=text]').val('');
            $('#' + tableId + ' tr[id="row_OI_' + count + '"] textarea').val('');
            $('#' + tableId + ' tr[id="row_OI_' + count + '"] select').val('');
            //---------------------------------
            $('.authoritydropdn').selectpicker();
            //make numbers only inputs.

            $('#objAdd_' + (count - 1)).addClass('hide');
            $('#objAdd_' + count).removeClass('hide');
            $('#objDel_' + count).removeClass('hide');
            $('#lvlName_' + count).html('');
            $('#lvlName_' + count).html('Level ' + (count + 1));
            $('#RequestVsAuthoritySettings_' + count + '__LevelCode').val((count + 1));
            $('#RequestVsAuthoritySettings_' + count + '__LevelName').val('Level ' + (count + 1));
            $('#RequestVsAuthoritySettings_' + count + '__AuthorityCode').val('');
        }
        else {
            swal.fire('Cannot add more than 5 Level');
        }
        
    }
   
}


function DeleteResultRow(index) {
    debugger;



    swal.fire({
        text: "Do you want to Delete?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {



            // if (confirm("Do you want to delete..?"))


            //{
            var count = 0;
            count = $('tr[id^="row_OI_"]').length;
            if (count > 1) {
                $("#row_OI_" + index).remove();
                for (var j = index + 1; j < count; j++) {
                    var template = $('#row_OI_' + j).clone(); // clone the row
                    var code = $("#Results_" + j + "__Name").val();

                    $.each(template.find('input, textarea'), function () {
                        var name = $(this).attr('name');
                        if (typeof name != 'undefined') {
                            name = name.replace(new RegExp("" + j, "g"), "" + (j - 1));
                            $(this).attr('name', name);
                        }
                        var id = $(this).attr('id');
                        if (typeof id != 'undefined') {
                            id = id.replace(new RegExp("" + j, "g"), "" + (j - 1));
                            $(this).attr('id', id);
                        }
                        //---------------------------------For row Increment When onkeyup
                        var onkeyup = $(this).attr('onkeyup');
                        if (onkeyup !== undefined) {
                            onkeyup = onkeyup.replace('(' + (j) + ')', '(' + (j - 1) + ')');
                            $(this).attr('onkeyup', onkeyup);
                        }
                        //---------------------------------------For row Increment When onchange
                        var onchange = $(this).attr('onchange');
                        if (onchange !== undefined) {
                            onchange = onchange.replace('(' + (j) + ')', '(' + (j - 1) + ')');
                            $(this).attr('onchange', onchange);
                        }
                        //---------------------------------------
                    });
                    $.each(template.find('select'), function () {
                        var name = $(this).attr('name');
                        name = name.replace(new RegExp("" + j, "g"), "" + (j - 1));
                        $(this).attr('name', name);
                        var id = $(this).attr('id');
                        id = id.replace(new RegExp("" + j, "g"), "" + (j - 1));
                        $(this).attr('id', id);
                        //---------------------------------------For row Increment When onchange
                        var onchange = $(this).attr('onchange');
                        if (onchange !== undefined) {
                            onchange = onchange.replace('(' + (j) + ')', '(' + (j - 1) + ')');
                            // onchange = onchange.replace(/\(0\)/g, '(' + (j - 1) + ')');
                            $(this).attr('onchange', onchange);
                        }
                        //---------------------------------------
                    });
                    $.each(template.find('button[type=button]'), function () {
                        var onclick = $(this).attr('onclick');
                        onclick = onclick.replace(new RegExp("(" + j + ")", "g"), "" + (j - 1));
                        $(this).attr('onclick', onclick);
                    });


                    $.each(template.find('a'), function () {
                        debugger
                        var onclick = $(this).attr('onclick');
                        onclick = onclick.replace(new RegExp("(" + j + ")", "g"), "" + (j - 1));
                        $(this).attr('onclick', onclick);
                    });
                    $.each(template.find('.addObj'), function () {

                        $(this).attr("id", "objAdd_" + (j - 1));
                    });

                    template.find('td[id=grid_slno]').text(j);
                    $("#row_OI_" + j).html(template.html());
                    $('#row_OI_' + j).attr('id', 'row_OI_' + (j - 1));


                    $("#Results_" + (j - 1) + "__Name").val(code);


                }
            }
            else {
                $("#Results_0__Name").val("");


            }
        }
        if ((count - 1) == index) {
            debugger;
            $('#objAdd_' + index).addClass('hide');
            $('#objAdd_' + (index - 1)).removeClass('hide');
        }
    });
}
function onAccountNumValidation(AccountNo) {

    debugger;
    var BankCode = $("#StatutoryClassification_Bank_Code").val()
    const accountNum = AccountNo.value;
    const length = accountNum.length;

    $.getJSON("/DataBank/EnrollmentTest/GetDigitsInAccountNumber", { BankCode: BankCode }, function (data) {
        debugger;
        var expectedLength = data.digitsAccountNumber;
        if (expectedLength != length && parseInt(expectedLength) >0) {
            $('#AccountNoValidation').show();
            $('#AccountNoValidation').text('No of digits should be equal to ' + expectedLength);
        }
        else {
            $('#AccountNoValidation').hide();
        }
    });

}


function onRelationChange(sender, id) {
    debugger;
    var relation = $(sender).val();
    if (relation == "") {
        $("#" + id + "").addClass('error-message');
        $(sender).addClass('is-invalid');
        $("#" + id + "").html("This field is required");
    }
    else {
        $("#" + id + "").removeClass('error-message');
        $(sender).removeClass('is-invalid');
        $("#" + id + "").html("");
    }
    if (relation == 1) {
        $('#labRel').html("Father's Name");
        $('#BasicInfo_FathersName').attr('placeholder', "Father's Name")

    }
    else {
        $('#labRel').html('Spouse Name');
        $('#BasicInfo_FathersName').attr('placeholder', 'Spouse Name')
    }
}

function OnCompanyChange(sender, id) {
    debugger;
    var cid = $(sender).val();
    if (cid == "" || cid == 0) {
        $("#" + id + "").addClass('error-message');
        $(sender).addClass('is-invalid');
        $("#" + id + "").html("This field is required");
    }
    else {
        $("#" + id + "").removeClass('error-message');
        $(sender).removeClass('is-invalid');
        $("#" + id + "").html("");
    }
    $.getJSON("/DataBank/EnrollmentTest/GetPayrollGroup", { id: cid }, function (data) {
        debugger
        $('#PreEnrollment_JobDetails_PayrollGroup_Code option').remove();
        $('#PreEnrollment_JobDetails_PayrollGroup_Code').append('<option value>--Payroll Group--</option');

        for (var i = 0; i < data.length; i++) {
            $('#PreEnrollment_JobDetails_PayrollGroup_Code').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
        }
        if (data.length > 0) {
            $('#PreEnrollment_JobDetails_PayrollGroup_Code').val(data[0].value);
        }

    });
}
function OnWageGradeChange(sender, id) {
    debugger;
    var cid = $(sender).val();
    if (cid == "" || cid == 0) {
        $("#" + id + "").addClass('error-message');
        $(sender).addClass('is-invalid');
        $("#" + id + "").html("This field is required");
    }
    else {
        $("#" + id + "").removeClass('error-message');
        $(sender).removeClass('is-invalid');
        $("#" + id + "").html("");
    }
    $.getJSON("/DataBank/EnrollmentTest/GetSalaryStructure", { id: cid }, function (data) {
        debugger
        $('#PreEnrollment_Compensation_SalaryStructure_Code option').remove();
        $('#PreEnrollment_Compensation_SalaryStructure_Code').append('<option value>--Salary Band--</option');

        for (var i = 0; i < data.length; i++) {
            $('#PreEnrollment_Compensation_SalaryStructure_Code').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
        }
        if (data.length > 0) {
            $('#PreEnrollment_Compensation_SalaryStructure_Code').val(data[0].value);
        }
    });

    onSalaryStructureChange();
}
function NewOrReplacement(key) {
    debugger

    var LocationCode = $('#PreEnrollment_JobDetails_Location_Code').val();
    var DesignationCode = $('#PreEnrollment_JobDetails_Designation_Code').val();
    var DepartmentCode = $('#PreEnrollment_JobDetails_Department_Code').val();
  
    if (DesignationCode != null && DesignationCode != "" && LocationCode != null && LocationCode != "" && DepartmentCode != null && DepartmentCode != "" ) {

        {
            if (key == 'N') {
                $('#positionInfo').attr('placeholder', 'New Psition');
                $.getJSON("/DataBank/EnrollmentTest/GetNewPosition", { Designation: DesignationCode, Location: LocationCode, Departmnet: DepartmentCode}, function (data) {
                    debugger
                    $('#PreEnrollment_BasicInfo_Position_Code option').remove();
                    $('#PreEnrollment_BasicInfo_Position_Code').append('<option value>--Position--</option');

                    for (var i = 0; i < data.length; i++) {
                        $('#PreEnrollment_BasicInfo_Position_Code').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
                    }
                    //$('#PreEnrollment_BasicInfo_Position_PositionNewReplacement').val('N');
                    $('input[name="PreEnrollment.BasicInfo.PositionNewReplacement"]:checked').val('N');
                    if (data.length > 0) {
                        $('#PreEnrollment_BasicInfo_Position_Code').val(data[0].value);
                    }
                });;
            }
            else {
                $('#positionInfo').attr('placeholder', 'Replacement')
                $.getJSON("/DataBank/EnrollmentTest/GetReplacementEmployee", { Designation: DesignationCode, Location: LocationCode, Departmnet: DepartmentCode }, function (data) {
                    debugger
                    $('#PreEnrollment_BasicInfo_Position_Code option').remove();
                    $('#PreEnrollment_BasicInfo_Position_Code').append('<option value>--Employee--</option');

                    for (var i = 0; i < data.length; i++) {
                        $('#PreEnrollment_BasicInfo_Position_Code').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
                    }
                    //$('#PreEnrollment_BasicInfo_Position_PositionNewReplacement').val('R');
                    $('input[name="PreEnrollment.BasicInfo.PositionNewReplacement"]:checked').val('R');
                    if (data.length > 0) {
                        $('#PreEnrollment_BasicInfo_Position_Code').val(data[0].value);
                    }
                });;
            }
        }
    }
}
function fillCostCenter(sender) {
    debugger
    var id = $(sender).val()
    $.getJSON("/DataBank/EnrollmentTest/fillCostCenter", { IncApplicable: id }, function (data) {
            debugger
        $('#PreEnrollment_JobDetails_IncentiveCostCenter_Code option').remove();
        $('#PreEnrollment_JobDetails_IncentiveCostCenter_Code').append('<option value>--Incentive CostCenter--</option');

            for (var i = 0; i < data.length; i++) {
                $('#PreEnrollment_JobDetails_IncentiveCostCenter_Code').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
            }
            if (data.length > 0 && parseInt(id)==0) {
                $('#PreEnrollment_JobDetails_IncentiveCostCenter_Code').val(data[0].value);
            }
        });
 
}
function PopUp_Preview() {
    debugger
    var LocationCode = $('#PreEnrollment_JobDetails_Location_Code').val();
    var WageGradeCode = $('#PreEnrollment_Compensation_SalaryStructure_Code').val();
    //var WageGradeCode = $('#PreEnrollment_JobDetails_WageGrade_Code').val();
    var CTC = $('#PreEnrollment_Compensation_CTC').val();

    if (WageGradeCode != null && WageGradeCode != "" && LocationCode != null && LocationCode != "" && parseFloat(CTC) > 0) {
        debugger
        var MPPersonalInfo = new FormData($('#enrollSave')[0]);

        $.ajax({
            type: 'POST',
            url: '/DataBank/EnrollmentTest/SalarySummary',
            dataType: 'html',
            contentType: false,
            processData: false,
            data: MPPersonalInfo,//{'PreEnrollGI' :GI},
            success: function (Data) {
                console.log(Data);
                var m = $('.salary_summary');
                m.find('.modal-body').html('');
                m.find('.modal-body').html(Data);
                m.modal('show');
                //  alert($('#BasicInfo_AcceptedDOJ').val());
                $('#edoj').text('EDOJ:    ' + $('#PreEnrollment_BasicInfo_AcceptedDOJ').val());
            },
            error: function (xhr, textStatus, errorThrown) {
                console.log(xhr.responseText);
                swal.fire(xhr.responseText);

            }
        });
    }

}
function OnWageGradeChanged() {
    debugger;
    var selectedValue = $('#WageGradeDropDown').val();

    $.getJSON("/DataBank/EnrollmentTest/GetWageGradeVsApplSaleryBand", { WageGradeCode: selectedValue }, function (data) {

        $('#PreEnrollment_Compensation_SalaryStructure_Code option').remove();
        $('#PreEnrollment_Compensation_SalaryStructure_Code').append('<option value="">--Select Salary Band--</option>');
        for (var i = 0; i < data.length; i++) {
            $('#PreEnrollment_Compensation_SalaryStructure_Code').append('<option value="' + data[i].value + '">' + data[i].text + '</option>');
        }

    });
}
function capitalizeAllLetter(val) {

    if (val.value === "") {
        return;
    }

    // Trim spaces and normalize multiple spaces
    val.value = val.value
        .replace(/(^\s*)|(\s*$)/gi, "")   // remove leading & trailing spaces
        .replace(/[ ]{2,}/gi, " ")        // replace multiple spaces with single space
        .replace(/\n +/, "\n");

    // Convert entire string to uppercase
    val.value = val.value.toUpperCase();

    return val.value;
}
function CheckDocImageExtensionSize(e) {
    debugger;
    var size = $(e)[0].files[0].size;
    var filename = $(e)[0].files[0].name;
    var fileExtension = ['jpeg', 'jpg', 'png', 'bmp', 'pdf'];
    if (size > 5 * 1024 * 1024) {
        swal.fire('File size cannot exceeds 5 Mb');
        $(e).val('');
        return false;
    }
    else if ($.inArray($(e).val().split('.').pop().toLowerCase(), fileExtension) == -1) {
        swal.fire("Only jpg, jpeg, png, bmp and pdf formats are allowed");
        $(e).val('');
        return false;
    }
    else {
        return true;
    }

}
function Preview() {

    debugger;
    if ($('#StatutoryClassification_BankFile_FileData').val() != '') {
        var path = URL.createObjectURL($('.PreviewImage')[0].files[0]);//($('.' + Doc)[0].files[0]);
        if (path == "") {
            swal.fire("No file Uploaded")
        }
        else {
            window.open(path);
        }
    }
    else {
        var url = "/DataBank/EnrollmentTest/ViewDocument?EnrollGI=" + $('#PreEnrollment_PreEnrollGI').val();
        $.ajax({
            url: url,
            type: "GET",
            success: function () {
                // File exists → open in new tab
                window.open(url, "_blank");
            },
            error: function (xhr) {
                let response = JSON.parse(xhr.responseText);
                Swal.fire({
                    icon: "error",
                    title: "Error",
                    text: response.message  // localized message
                });
            }
        });
    }

}
function onFileChange() {
    debugger;

    var fileInput = document.getElementById('StatutoryClassification_BankFile_FileData');
    var label = document.getElementById('fileLabel');

    if (fileInput.files.length === 0) {
        label.innerHTML = "No File Chosen";
    } else {
        var fileName = fileInput.files[0].name;

        let replacemnt = '..';
        let shortName = fileName.slice(0, 18);

        label.innerHTML = shortName.substring(0, shortName.length - 2) + replacemnt;
    }
}

function maskAadhar(input) {
    debugger
    let val = input.value;
    // Store original in hidden field
    document.getElementById("hdnAadhar").value = val;
    var filter = /^[2-9]{1}[0-9]{11}$/
    if (filter.test(input.value)) {
        debugger;
        // Show only last 4 digits
        input.value = val.replace(/\d(?=\d{4})/g, "*");
    }
}
function validateAadharNew(val, id, mandatory) {
    debugger;
    aadhar = $('#' + val).val();
    if ($("#" + mandatory + "").val() != "False") {

        if ($('#' + val).val() == "") {
            $("#" + id + "").addClass('error-message');
            $('#' + val).addClass('is-invalid');
            $("#" + id + "").html("This field is required");
        }
        else {
            var filter = /^[2-9]{1}[0-9]{11}$/
            if (!filter.test(aadhar)) {
                debugger;
                $("#" + id + "").addClass('error-message');
                $('#' + val).addClass('is-invalid');
                $("#" + id + "").html('Invalid Aadhar number')
                return false;
            }
            else {
                $("#" + id + "").removeClass('error-message');
                $('#' + val).removeClass('is-invalid');
                $("#" + id + "").html("");

                return true;
            }
        }
    }
    else {
        var filter = /^[2-9]{1}[0-9]{11}$/
        if (!filter.test(aadhar)) {
            debugger;
            $("#" + id + "").addClass('error-message');
            $('#' + val).addClass('is-invalid');
            $("#" + id + "").html('Invalid Aadhar number')
            return false;
        }
        else {
            $("#" + id + "").removeClass('error-message');
            $('#' + val).removeClass('is-invalid');
            $("#" + id + "").html("");

            return true;
        }
        return true;
    }
}