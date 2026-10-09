var CTC = 0;
var RdiffDays;
function CheckImageExtensionSize(e) {
    debugger;
    console.log(e);
    var size = $(e)[0].files[0].size;
    var filename = $(e)[0].files[0].name;
    var fileExtension = ['jpeg', 'jpg', 'png', 'bmp'];
    console.log(size);
    if (size > 2048 * 1024) {
        swal.fire('File size cannot exceeds 2 Mb');
        $(e).val('');
    }
    else if ($.inArray($(e).val().split('.').pop().toLowerCase(), fileExtension) == -1) {
        swal.fire("Only jpg, jpeg, png and bmp formats are allowed");
        $(e).val('');
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

function fillSingleValueDropdown()
{
    debugger;
    var bank = $('#StatutoryClassification_Bank_Code > option').length;
    if (bank == 2) {
        document.getElementById('StatutoryClassification_Bank_Code').selectedIndex = 1;
    }
    var category = $('#PreEnrollment_JobDetails_Category_Code > option').length;
    if (category == 2) {
        document.getElementById('PreEnrollment_JobDetails_Category_Code').selectedIndex = 1;
    }
    //var grade = $('#PreEnrollment_JobDetails_Grade_Code > option').length;
    //if (grade == 2) {
    //    document.getElementById('PreEnrollment_JobDetails_Grade_Code').selectedIndex = 1;
    //    var Code = $('#PreEnrollment_JobDetails_Grade_Code').val();
    //    var url = "/DataBank/Enrollment/GetCompensationGrid";
    //    $(".Compensation").load(url, { GradeCode: Code }, function () {
    //    });
    //}
    var designation = $('#PreEnrollment_JobDetails_Designation_Code > option').length;
    if (designation == 2) {
        document.getElementById('PreEnrollment_JobDetails_Designation_Code').selectedIndex = 1;
    }
    var department = $('#PreEnrollment_JobDetails_Department_Code > option').length;
    if (department == 2) {
        document.getElementById('PreEnrollment_JobDetails_Department_Code').selectedIndex = 1;
    }
    var location = $('#PreEnrollment_JobDetails_Location_Code > option').length;
    if (location == 2) {
        document.getElementById('PreEnrollment_JobDetails_Location_Code').selectedIndex = 1;
    }
    var payroll = $('#PreEnrollment_JobDetails_PayrollGroup_Code > option').length;
    if (payroll == 2) {
        document.getElementById('PreEnrollment_JobDetails_PayrollGroup_Code').selectedIndex = 1;
    }
    var country = $('#PreEnrollment_PermanentAddress_Country_Code > option').length;
    if (country == 2) {
        document.getElementById('PreEnrollment_PermanentAddress_Country_Code').selectedIndex = 1;
    }
    debugger;
    var shiftDetails = $('#AttendanceSettings_ShiftDetailsIds > option').length;
    if (shiftDetails == 2) {
        document.getElementById('AttendanceSettings_ShiftDetailsIds').selectedIndex = 1;
    }
    
    
}

function onCategoryChange(val) {
    debugger;
    validate(val, 'category')
    var categoryCode = val.value;
    if (categoryCode == 3) {
        $('#renewal').html("Renewal Date");
        $('#PreEnrollment_BasicInfo_RenewalDate').attr('placeholder', 'Renewal Date');
        $('#PreEnrollment_BasicInfo_RenewalDate').attr('disabled', true);
    }
    else if (categoryCode == 1) {
        $('#renewal').html("Probation Completion");
        $('#PreEnrollment_BasicInfo_RenewalDate').attr('placeholder', 'Probation Completion');
        $('#PreEnrollment_BasicInfo_RenewalDate').attr('disabled', false);
    }
    else if (categoryCode == 4) {
        $('#renewal').html("Contract Expiry");
        $('#PreEnrollment_BasicInfo_RenewalDate').attr('placeholder', 'Contract Expiry');
        $('#PreEnrollment_BasicInfo_RenewalDate').attr('disabled', false);
    }
    else if (categoryCode == 2) {
        $('#renewal').html("Trainee Completion");
        $('#PreEnrollment_BasicInfo_RenewalDate').attr('placeholder', 'Trainee Completion');
        $('#PreEnrollment_BasicInfo_RenewalDate').attr('disabled', false);
    }
}
function firstLetterCaps(val, id) {

    debugger;
    stringval = val.value;
    if (val.value == "") {
        $(val).addClass('is-invalid');
        $("#" + id + "").addClass('error-message');
        $("#" + id + "").html("This field is required");
    }
    else {
        $(val).removeClass('is-invalid');
        $("#" + id + "").removeClass('error-message');
        $("#" + id + "").html("");
        val.value = val.value.toLowerCase();
        val.value = val.value.
            replace(/(^\s*)|(\s*$)/gi, ""). // removes leading and trailing spaces
            replace(/[ ]{2,}/gi, "").       // replaces multiple spaces with one space 
            replace(/\n +/, "\n");
        val.value = val.value.substr(0, 1).toUpperCase() + val.value.substr(1);        
        return val.value;
    }
}


function validateDOJ(val, id)
{
    debugger;
    if (val.value == "") {
        $(val).addClass('is-invalid');
        $("#" + id + "").addClass('error-message');
        $("#" + id + "").html("This field is required");
    }
    else {                
        var doj = new Date(val.value);
        currDate = new Date();
        if (doj > currDate) {
        const diffTime = Math.abs(currDate - doj);
        const daysDiff = Math.ceil(diffTime / (1000 * 60 * 60 * 24)); 
                
        if (daysDiff >= 30) {
            swal.fire("The entered Date of Joining is 30 days or more after current date. Do you want to proceed ?");
        }
        }
        $(val).removeClass('is-invalid');
        $("#" + id + "").removeClass('error-message');
        $("#" + id + "").html("");
        trim(val);
    }
}


function validateMobile(val, id) {
    mobile = $('#' + val).val();
    if ($('#' + val).val() == "") {
        $("#" + id + "").addClass('error-message');
        $('#' + val).addClass('is-invalid');
        $("#" + id + "").html("This field is required");

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

function validateEmail(val, id)
{
    email = val.value;
    if (val.value == "") {
        $("#" + id + "").addClass('error-message');
        $(val).addClass('is-invalid');
        $("#" + id + "").html("This field is required");
    }
    else {
        filter = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/
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
            trim(val);
            return true;
        }
    }
}

function validateDOB(val, id)
{
    debugger;
    dateofbirth = $('#PreEnrollment_BasicInfo_DOB').val();
    if (val.value == "") {
        $("#" + id + "").addClass('error-message');
        $(val).addClass('is-invalid');
        $("#" + id + "").html("This field is required");
    }
    else {
        debugger;

        var DOB = new Date(document.getElementById('PreEnrollment_BasicInfo_DOB').value);
        var DOBYear = Math.abs(DOB.getFullYear());

        var CurDate = new Date();
        var CurDateYear = Math.abs(CurDate.getFullYear())

        var years = parseInt(CurDateYear) - parseInt(DOBYear);

        var age = parseInt(years);
        
        if (age < 18) {
            $("#" + id + "").addClass('error-message');
            $(val).addClass('is-invalid');
            $("#" + id + "").html("Age must be greater than 18");
            return false;
        }
        else if(age > 58)
        {
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
        else
        {
            $("#" + id + "").removeClass('error-message');
            $(val).removeClass('is-invalid');
            $("#" + id + "").html("");
            trim(val);
            return true;
        }
    }
}
function validateAadhar(val, id) {
    debugger;

    aadhar = $('#' + val).val();
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
            //trim(val);
            return true;
        }
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
function validatePan(id) {
    debugger;
    var val = document.getElementById('PreEnrollment_BasicInfo_PanNo');
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
            //trim(val);
            val.value = val.value.toUpperCase();
            //console.log(val.value);
            return val.value;
        }
    }
}


function onGradeChange(sender,id) {
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


function CTCCalculation() {
    debugger;
    var TotAmt = parseInt("0");
    var TotGrossAmt = parseInt("0");
    $('.AllowanceAmount').each(function (i, obj) {
        var Amt = parseInt($("#PreEnrollment_Compensation_FixedAllowance_" + i + "__Amount").val());
        var type = $("#PreEnrollment_Compensation_FixedAllowance_" + i + "__AllowanceType").val();
        debugger;
        if (isNaN(Amt)) {
            Amt = parseInt("0");
        }
        TotAmt = TotAmt + Amt;
        if (type != 'CTC Component') {
            TotGrossAmt = TotGrossAmt + Amt;
        }
        $("#PreEnrollment_Compensation_CTC").val(TotAmt);
        $("#PreEnrollment_Compensation_GrossSalary").val(TotGrossAmt);
    });

}

function validateDOR(val, id) {

    const doj = new Date($("#PreEnrollment_BasicInfo_AcceptedDOJ").val());
    const dor = new Date(val.value);
    
    if (val.value == "") {
        $("#" + id + "").addClass('error-message');
        $(val).addClass('is-invalid');
        $("#" + id + "").html("This field is required");
    }
    else {
        const diffTime = Math.abs(dor - doj);
        RdiffDays = Math.ceil(diffTime / (1000 * 60 * 60 * 24));        
        console.log(RdiffDays + " days");
        if (RdiffDays < 15) {
            if (swal.fire("Renewal Date less than 15 days. Do you want to continue?")) {

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
        trim(val);
        }
    }    
}


function onShiftTypeChange(sender) {
    debugger;
    var value = $(sender).val();
    if (value == 1) {

        $('#singleselect').show();
        $('#multipleselect').hide();
        $("#sselect").prop('selectedIndex', 0);                
    }
    else if (value == 2) {
        $('#singleselect').hide();
        $('#multipleselect').show();
        $("#AttendanceSettings_ShiftDetailsIds").val('');
        $('#sselect').removeClass('is-invalid');
        $('#shiftdetails').removeClass('error-message');
        $('#shiftdetails').html("");
    }
    else {
        $('#singleselect').hide();
        $('#multipleselect').show();
        $("#AttendanceSettings_ShiftDetailsIds").val('');
        $('#sselect').removeClass('is-invalid');
        $('#shiftdetails').removeClass('error-message');
        $('#shiftdetails').html("");
    }
    $("#shifttype").removeClass('error-message');
    $("#AttendanceSettings_ShiftType_Code").removeClass('is-invalid');
    $("#shifttype").html("");
}


function onCountryChange(sender,id) {
    
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
            $('#PreEnrollment_PermanentAddress_State_Code').append('<option value="0">--Select--</option');

            for (var i = 0; i < data.length; i++) {
                $('#PreEnrollment_PermanentAddress_State_Code').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
            }

        });
        //Make Dtsrict Dropdwn Empty when no Country is Selected
        //if (cid == 0) {
        $.getJSON("/DataBank/Enrollment/GetDistrict", { id: 0 }, function (data) {

            $('#PreEnrollment_PermanentAddress_District_Code option').remove();
            $('#PreEnrollment_PermanentAddress_District_Code').append('<option value="0">--Select--</option');

            for (var i = 0; i < data.length; i++) {
                $('#PreEnrollment_PermanentAddress_District_Code').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
            }
        });    
    //}
}

function onStateChange(sender,id) {
    
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
            $('#PreEnrollment_PermanentAddress_District_Code').append('<option value="0">--Select--</option');

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
    
    if (val.value == 2 || val.value == 4) {            
        $('#bankdetails').hide();            
    }
    else {
        $('#bankdetails').show();
    }        
    
} 
function onAccountNumValidation() {

    debugger;
    var BankCode = $("#StatutoryClassification_Bank_Code").val()
    var Length_Ac = $("#StatutoryClassification_AccountNo").length()
   $.getJSON("/DataBank/Enrollment/GetDigitsInAccountNumber", { BankCode: BankCode }, function (data) {
       var leng = console.log(data);
       if (leng != Length_Ac) {
           $('#AccountNoValidation').show();
           $('#AccountNoValidation').text('Digits should be equal to ' + leng);
       }
       else {
           $('#AccountNoValidation').hide();
       }
    });

}
function validateShiftDetails(val, id) {
    debugger;
    if ($("#AttendanceSettings_ShiftType_Code").val() != 3) {

        if (val.value == "") {
            $(val).addClass('is-invalid');
            $("#" + id + "").addClass('error-message');
            $("#" + id + "").html("This field is required");
        }
        else {
            $(val).removeClass('is-invalid');
            $("#" + id + "").removeClass('error-message');
            $("#" + id + "").html("");

        }
    }
    else {

        $("#shiftdetails").removeClass('error-message');
        $("#shiftdetails").html("");//$("#mselect").val() == ""
    }
}

function validatePunchingMethod(val, id) {
    debugger;
    if (val.value == "") {
        $(val).addClass('is-invalid');
        $("#" + id + "").addClass('error-message');
        $("#" + id + "").html("This field is required");
    }
    else {
        $(val).removeClass('is-invalid');
        $("#" + id + "").removeClass('error-message');
        $("#" + id + "").html("");

    }
}


function validateReportingManager(val,divId, id) {
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

function removeActiveTab() {
    $("#tabOne").removeClass("active");
    $("#tabOne").css("display", "none");
    $("#tabOneTitle").removeClass("active");

    $("#tabTwo").removeClass("active");
    $("#tabTwo").css("display", "none");
    $("#tabTwoTitle").removeClass("active");

    $("#tabThree").removeClass("active");
    $("#tabThree").css("display", "none");
    $("#tabThreeTitle").removeClass("active");

    $("#tabFour").removeClass("active");
    $("#tabFour").css("display", "none");
    $("#tabFourTitle").removeClass("active");

    $("#tabSix").removeClass("active");
    $("#tabSix").css("display", "none");
    $("#tabSixTitle").removeClass("active");

    $("#tabSeven").removeClass("active");
    $("#tabSeven").css("display", "none");
    $("#tabSevenTitle").removeClass("active");

    $("#tabEight").removeClass("active");
    $("#tabEight").css("display", "none");
    $("#tabEightTitle").removeClass("active");
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
        if (data[i].code == 3) {
            if (msg != '')
                msg = msg + ", Aadhar number";
            else
                msg = msg + "Aadhar number";
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


function validateAll() {
    debugger;    
   
    //$.getJSON("/DataBank/Enrollment/AlreadyExistCheck", { EmployeeGI: $('#EnrollGUID').val(), Mobile: $("#PreEnrollment_PermanentAddress_MobileNo").val(), Pan: $("#PreEnrollment_BasicInfo_PanNo").val(), Aadhar: $("#PreEnrollment_BasicInfo_AdharNo").val() }, function (data) {
    //    debugger;
    //    if (data.message != "NO ISSUES")
    //        swal.fire(data.message);

    //});    
    if ($("#PreEnrollment_BasicInfo_File_FileData").val() == "" && $("#PreEnrollment_BasicInfo_File_AbsoluteUri").val() == "") {
        swal.fire('Candidate photo is required');
        return false;
    }
    else if ($("#PreEnrollment_BasicInfo_FullName").val() == "") {        
        $("#fullname").html("This field is required");
        $('#PreEnrollment_BasicInfo_FullName').focus();
        return false;
    }
    else if ($("#PreEnrollment_BasicInfo_AcceptedDOJ").val() == "") {
        $("#doj").html("This field is required");
        $('#PreEnrollment_BasicInfo_AcceptedDOJ').focus();
        return false;
    }
    else if (!validateMobile('PreEnrollment_PermanentAddress_MobileNo','mobile')) {
        //$("#mobile").html("This field is required");
        $('#PreEnrollment_PermanentAddress_MobileNo').focus();
        return false;
    }
    else if ($("#PreEnrollment_PermanentAddress_MailID").val() == "") {
        $("#email").html("This field is required");
        $('#PreEnrollment_PermanentAddress_MailID').focus();
        return false;
    }
    else {
        
        if ($('#PreEnrollment_BasicInfo_DOB').val() == "") {
            removeActiveTab()
            $("#tabOneTitle").addClass("active");
            $("#tabOne").addClass("active");
            $("#tabOne").css("display", "block");
            $("#dob").html("This field is required");
            $('#PreEnrollment_BasicInfo_DOB').focus();
            return false;
        }
        else if ($("#PreEnrollment_BasicInfo_Gender").val() == "") {
            removeActiveTab()
            $("#tabOneTitle").addClass("active");
            $("#tabOne").addClass("active");
            $("#tabOne").css("display", "block");
            $("#gender").html("This field is required");
            $('#PreEnrollment_BasicInfo_Gender').focus();
            return false;
        }
        else if ($("#PreEnrollment_BasicInfo_FathersName").val() == "") {
            removeActiveTab()
            $("#tabOneTitle").addClass("active");
            $("#tabOne").addClass("active");
            $("#tabOne").css("display", "block");
            $("#father").html("This field is required");
            $('#PreEnrollment_BasicInfo_FathersName').focus();
            return false;
        }
        else if (!validateAadhar('PreEnrollment_BasicInfo_AdharNo','aadhar')) {
            removeActiveTab()
            $("#tabOneTitle").addClass("active");
            $("#tabOne").addClass("active");
            $("#tabOne").css("display", "block");
            //$("#aadhar").html("This field is required");
            $('#PreEnrollment_BasicInfo_AdharNo').focus();
            return false;
        }
        else if (!validatePan('pan')) {
            removeActiveTab()
            $("#tabOneTitle").addClass("active");
            $("#tabOne").addClass("active");
            $("#tabOne").css("display", "block");
            //$("#pan").html("This field is required");
            $('#PreEnrollment_BasicInfo_PanNo').focus();
            return false;
        }
        else if ($("#PreEnrollment_JobDetails_Category_Code").val() == "") {
            removeActiveTab()
            $("#tabTwoTitle").addClass("active");
            $("#tabTwo").addClass("active");
            $("#tabTwo").css("display", "block");
            $("#category").html("This field is required");
            $('#PreEnrollment_JobDetails_Category_Code').focus();
            return false;
        }
        else if ($("#PreEnrollment_JobDetails_Grade_Code").val() == "") {
            removeActiveTab()
            $("#tabTwoTitle").addClass("active");
            $("#tabTwo").addClass("active");
            $("#tabTwo").css("display", "block");
            $("#grade").html("This field is required");
            $('#PreEnrollment_JobDetails_Grade_Code').focus();
            return false;
        }
        else if ($("#PreEnrollment_JobDetails_Designation_Code").val() == "") {
            removeActiveTab()
            $("#tabTwoTitle").addClass("active");
            $("#tabTwo").addClass("active");
            $("#tabTwo").css("display", "block");
            $("#designation").html("This field is required");
            $('#PreEnrollment_JobDetails_Designation_Code').focus();
            return false;
        }
        else if ($("#PreEnrollment_JobDetails_Department_Code").val() == "") {
            removeActiveTab()
            $("#tabTwoTitle").addClass("active");
            $("#tabTwo").addClass("active");
            $("#tabTwo").css("display", "block");
            $("#department").html("This field is required");
            $('#PreEnrollment_JobDetails_Department_Code').focus();
            return false;
        }
        else if ($("#PreEnrollment_JobDetails_Location_Code").val() == "") {
            removeActiveTab()
            $("#tabTwoTitle").addClass("active");
            $("#tabTwo").addClass("active");
            $("#tabTwo").css("display", "block");
            $("#location").html("This field is required");
            $('#PreEnrollment_JobDetails_Location_Code').focus();
            return false;
        }
        else if ($("#PreEnrollment_JobDetails_PayrollGroup_Code").val() == "") {
            removeActiveTab()
            $("#tabTwoTitle").addClass("active");
            $("#tabTwo").addClass("active");
            $("#tabTwo").css("display", "block");
            $("#payroll").html("This field is required");
            $('#PreEnrollment_JobDetails_PayrollGroup_Code').focus();
            return false;
        }
        //else if ($('#PreEnrollment_BasicInfo_RenewalDate').val() == "") {
        //    $("#tabTwoTitle").addClass("active");
        //    $("#tabTwo").addClass("active");
        //    $("#tabTwo").css("display", "block");
        //    $("#dor").html("This field is required");
        //    $('#PreEnrollment_BasicInfo_RenewalDate').focus();
        //    return false;
        //}
        else if (RdiffDays < 15)
        {
            removeActiveTab()
            swal.fire("Renewal Date less than 15 days. Do you want to continue?");
            $("#tabTwoTitle").addClass("active");
            $("#tabTwo").addClass("active");
            $("#tabTwo").css("display", "block");
            $('#PreEnrollment_BasicInfo_RenewalDate').focus();
            return false;
        }
        else if ($('#PreEnrollment_JobDetails_NoticePeriod').val() == "") {
            removeActiveTab()
            $("#tabTwoTitle").addClass("active");
            $("#tabTwo").addClass("active");
            $("#tabTwo").css("display", "block");
            $("#noticeperiod").html("This field is required");
            $('#PreEnrollment_JobDetails_NoticePeriod').focus();
            return false;
        }
        else if ($("#PreEnrollment_Compensation_OfferedSalary").val() == "") {
            removeActiveTab()
            $("#tabThreeTitle").addClass("active");
            $("#tabThree").addClass("active");
            $("#tabThree").css("display", "block");
            $("#salary").html("This field is required");
            $('#PreEnrollment_Compensation_OfferedSalary').focus();
            return false;
        }
        else if ($("#PreEnrollment_Compensation_CTC").val() <= 0 || $("#PreEnrollment_Compensation_CTC").val() == null) {
            removeActiveTab()
            $("#tabThreeTitle").addClass("active");
            $("#tabThree").addClass("active");
            $("#tabThree").css("display", "block");
            if (swal.fire("Candidate salary amount is zero. Do you want to continue?"))
                return true;
            else
                return false;
        }
        else if (parseFloat($("#PreEnrollment_Compensation_OfferedSalary").val()) != parseFloat($("#PreEnrollment_Compensation_CTC").val())) {
            removeActiveTab()
            $("#tabThreeTitle").addClass("active");
            $("#tabThree").addClass("active");
            $("#tabThree").css("display", "block");
            $('#PreEnrollment_Compensation_OfferedSalary').focus();            
            swal.fire("Offered salary and CTC not same!");
            return false;
        }       
        else if ($("#PreEnrollment_Compensation_CTC").val() > 21000 && $("#PreEnrollment_Compensation_ESI").is(':checked'))
        {            
            removeActiveTab()
            $("#tabThreeTitle").addClass("active");
            $("#tabThree").addClass("active");
            $("#tabThree").css("display", "block");
            
            swal.fire("ESI Applicable Allowance is greater than 21000. So please remove ESI applicability");
            return false;
        }
        else if ($("#PreEnrollment_PermanentAddress_House").val() == "") {
            removeActiveTab()
            $("#tabFourTitle").addClass("active");
            $("#tabFour").addClass("active");
            $("#tabFour").css("display", "block");
            $("#house").html("This field is required");
            $('#PreEnrollment_PermanentAddress_House').focus();
            return false;
        }
        else if ($("#PreEnrollment_PermanentAddress_Street").val() == "") {
            removeActiveTab()
            $("#tabFourTitle").addClass("active");
            $("#tabFour").addClass("active");
            $("#tabFour").css("display", "block");
            $("#street").html("This field is required");
            $('#PreEnrollment_PermanentAddress_Street').focus();
            return false;
        }
        else if ($("#PreEnrollment_PermanentAddress_City").val() == "") {
            removeActiveTab()
            $("#tabFourTitle").addClass("active");
            $("#tabFour").addClass("active");
            $("#tabFour").css("display", "block");
            $("#city").html("This field is required");
            $('#PreEnrollment_PermanentAddress_City').focus();
            return false;
        }
        else if ($("#PreEnrollment_PermanentAddress_PinCode").val() == "") {
            removeActiveTab()
            $("#tabFourTitle").addClass("active");
            $("#tabFour").addClass("active");
            $("#tabFour").css("display", "block");
            $("#pin").html("This field is required");
            $('#PreEnrollment_PermanentAddress_PinCode').focus();
            return false;
        }
        else if ($("#PreEnrollment_PermanentAddress_Country").val() == "") {
            removeActiveTab()
            $("#tabFourTitle").addClass("active");
            $("#tabFour").addClass("active");
            $("#tabFour").css("display", "block");
            $("#country").html("This field is required");
            $('#PreEnrollment_PermanentAddress_Country').focus();
            return false;
        }
        else if ($("#PreEnrollment_PermanentAddress_State").val() == "") {
            removeActiveTab()
            $("#tabFourTitle").addClass("active");
            $("#tabFour").addClass("active");
            $("#tabFour").css("display", "block");
            $("#state").html("This field is required");
            $('#PreEnrollment_PermanentAddress_State').focus();
            return false;
        }
        else if ($("#PreEnrollment_PermanentAddress_District").val() == "") {
            removeActiveTab()
            $("#tabFourTitle").addClass("active");
            $("#tabFour").addClass("active");
            $("#tabFour").css("display", "block");
            $("#district").html("This field is required");
            $('#PreEnrollment_PermanentAddress_District').focus();
            return false;
        }
        else if ($("#AttendanceSettings_ShiftType_Code").val() == "") {
            removeActiveTab()
            $("#tabSixTitle").addClass("active");
            $("#tabSix").addClass("active");
            $("#tabSix").css("display", "block");
            $("#AttendanceSettings_ShiftType_Code").addClass('is-invalid');
            $("#shifttype").addClass('error-message');
            $("#shifttype").html("This field is required");
            $('#AttendanceSettings_ShiftType_Code').focus();
            return false;
        }
        else if ($("#AttendanceSettings_ShiftType_Code").val() == 1 && $('#sselect').val() == "") {
            removeActiveTab()
            $("#tabSixTitle").addClass("active");
            $("#tabSix").addClass("active");
            $("#tabSix").css("display", "block");            
            $("#sselect").addClass('is-invalid');
            $("#shiftdetails").addClass('error-message');
            $("#shiftdetails").html("This field is required");
            $('#AttendanceSettings_ShiftType_Code').focus();
            return false;
        }
        else if ($("#AttendanceSettings_ShiftType_Code").val() == 2 && $("#AttendanceSettings_ShiftDetailsIds").val() == "") {
            removeActiveTab()
            $("#tabSixTitle").addClass("active");
            $("#tabSix").addClass("active");
            $("#tabSix").css("display", "block");    
            $("#mselect").addClass('is-invalid');
            $("#shiftdetails").addClass('error-message');
            $("#shiftdetails").html("This field is required");
            $('#AttendanceSettings_ShiftType_Code').focus();
            return false;
        }
        else if ($('#AttendanceSettings_PunchingMethodIDs').val().length == 0) {
            removeActiveTab()
            $("#tabSixTitle").addClass("active");
            $("#tabSix").addClass("active");
            $("#tabSix").css("display", "block");
            $("#AttendanceSettings_PunchingMethodIDs").addClass('is-invalid');
            $("#punchingmethod").addClass('error-message');
            $("#punchingmethod").html("This field is required");
            $('#AttendanceSettings_PunchingMethodIDs').focus();
            return false;
        }
        else if ($("#StatutoryClassification_PaymentMode_Code").val() == "") {
            removeActiveTab()
            $("#tabSevenTitle").addClass("active");
            $("#tabSeven").addClass("active");
            $("#tabSeven").css("display", "block");
            $("#StatutoryClassification_PaymentMode_Code").addClass('is-invalid');
            $("#payment").addClass('error-message');
            $("#payment").html("This field is required");
            $('#StatutoryClassification_PaymentMode_Code').focus();
            return false;
        }
        else if ($("#StatutoryClassification_PaymentMode_Code").val() == 1 || $("#StatutoryClassification_PaymentMode_Code").val() == 3) {
            
            if ($("#StatutoryClassification_Bank_Code").val() == "") {
                removeActiveTab()
                $("#tabSevenTitle").addClass("active");
                $("#tabSeven").addClass("active");
                $("#tabSeven").css("display", "block");
                $('#StatutoryClassification_Bank_Code').addClass('is-invalid');
                $("#bankname").addClass('error-message');
                $("#bankname").html("This field is required");
                $('#StatutoryClassification_Bank_Code').focus();
                return false;
            }
            else if ($("#StatutoryClassification_AccountNo").val() == "") {
                removeActiveTab()
                $("#tabSevenTitle").addClass("active");
                $("#tabSeven").addClass("active");
                $("#tabSeven").css("display", "block");
                $('#StatutoryClassification_AccountNo').addClass('is-invalid');
                $("#accno").addClass('error-message');
                $("#accno").html("This field is required");
                $('#StatutoryClassification_AccountNo').focus();
                return false;
            }
            else if ($("#StatutoryClassification_IFSC").val() == "") {
                removeActiveTab()
                $("#tabSevenTitle").addClass("active");
                $("#tabSeven").addClass("active");
                $("#tabSeven").css("display", "block");
                $('#StatutoryClassification_IFSC').addClass('is-invalid');
                $("#ifsc").addClass('error-message');
                $("#ifsc").html("This field is required");
                $('#StatutoryClassification_IFSC').focus();
                return false;
            }            
        }
        if ($("#EmployeeSettings_ReportingManager_Code").val() == "") {
            removeActiveTab()
            $("#tabEightTitle").addClass("active");
            $("#tabEight").addClass("active");
            $("#tabEight").css("display", "block");
            $('.rptMgr').addClass('is-invalid');
            $("#reportingmanager").addClass('error-message');
            $("#reportingmanager").html("This field is required");
            $('#EmployeeSettings_ReportingManager_Code').focus();
            return false;
        }
        else if ($("#EmployeeSettings_EmployeeID").val() == "") {
            removeActiveTab()
            $("#tabEightTitle").addClass("active");
            $("#tabEight").addClass("active");
            $("#tabEight").css("display", "block");
            $("#empid").html("This field is required");
            $('#EmployeeSettings_EmployeeID').focus();
            return false;
        }
       
        else {                                    
           
            $.getJSON("/DataBank/Enrollment/AlreadyExistCheck", { EmployeeGI: $('#EnrollGUID').val(), Mobile: $("#PreEnrollment_PermanentAddress_MobileNo").val(), Pan: $("#PreEnrollment_BasicInfo_PanNo").val(), Aadhar: $("#PreEnrollment_BasicInfo_AdharNo").val() }, function (data) {
              
                debugger;
                if (data.alreadyExists_OnBoarding != null) {
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
                            debugger;
                            var MPPersonalInfo = new FormData($('#enrollSave')[0]);
                            console.log(MPPersonalInfo);
                            // $('#preSave').submit(function (e) {
                            //   e.preventDefault();
                            $.ajax({
                                type: 'POST',
                                url: "/DataBank/Enrollment/Create",
                                //enctype: 'multipart/form-data',
                                contentType: false,
                                processData: false,
                                dataType: 'json',
                                data: MPPersonalInfo,
                                //processData: false,
                                //contentType: false,
                                success: function (result) {
                                    if (result.success == true) {
                                        swal.fire("Saved Successfully").then(function () {

                                            var url = "/DataBank/OnBoarding";
                                            window.location.href = url;
                                            //$('#leaveOpeningList').load(url, { LeavePeriod: LeavePeriod, LeaveGroup: LeaveGroup, Leave: Leave }, function () { });
                                            //CloseSlider('batchUpdateSlider');
                                        });

                                    }
                                    else {
                                        swal.fire(result.message).then(function () {

                                        });
                                    }

                                },
                                error: function (xhr, textStatus, errorThrown) {

                                }
                            });
                        }
                        else {
                            return false;
                        }
                    })

                }
                else {
                    debugger;
                    var MPPersonalInfo = new FormData($('#enrollSave')[0]);
                    // console.log(formData);
                    // $('#preSave').submit(function (e) {
                    //   e.preventDefault();
                    $.ajax({
                        type: 'POST',
                        url: "/DataBank/Enrollment/Create",
                        //enctype: 'multipart/form-data',
                        contentType: false,
                        processData: false,
                        dataType: 'json',
                        data: MPPersonalInfo,
                        //processData: false,
                        //contentType: false,
                        success: function (result) {
                            if (result.success == true) {
                                swal.fire("Saved Successfully").then(function () {

                                    var url = "/DataBank/OnBoarding";
                                    window.location.href = url;
                                    //$('#leaveOpeningList').load(url, { LeavePeriod: LeavePeriod, LeaveGroup: LeaveGroup, Leave: Leave }, function () { });
                                    //CloseSlider('batchUpdateSlider');
                                });

                            }
                            else if (result.message == "PF_NUMBER_DUPLICATION_ERROR")
                            {
                                swal.fire("PF Number is already exists");
                            }
                            else if (result.message == "UAN_NUMBER_DUPLICATION_ERROR") {
                                swal.fire("UAN Number is already exists");
                            }
                            else if (result.message == "ESI_NUMBER_DUPLICATION_ERROR") {
                                swal.fire("ESI Number is already exists");
                            }
                            else if (result.message == "EMPLOYEE_ID_DUPLICATION_ERROR") {
                                swal.fire("Employee ID is already exists");
                            }
                            else if (result.message == "PUNCHING_ID_DUPLICATION_ERROR") {
                                swal.fire("Punching ID is already exists");
                            }
                            else {
                                swal.fire(result.message).then(function () {

                                });
                            }

                        },
                        error: function (xhr, textStatus, errorThrown) {

                        }
                    });
                }
            });
        }
    }
}


function process(input) {
    debugger;
    let value = input.value;
    let numbers = value.replace(/[^0-9]/g, "");
    input.value = numbers;
}