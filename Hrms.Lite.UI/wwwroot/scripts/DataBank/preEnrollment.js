var CTC = 0;
var RdiffDays;
function CheckImageExtensionSize(e) {
    debugger;
    console.log(e);
    debugger;   
    var size = $(e)[0].files[0].size;
    var filename = $(e)[0].files[0].name;
    var fileExtension = ['jpeg', 'jpg', 'png', 'bmp'];
    if (size > 2048 * 1024) {
        swal.fire('File size cannot exceeds 2 Mb');
        $(e).val('');
    }
    else if ($.inArray($(e).val().split('.').pop().toLowerCase(), fileExtension) == -1) {
        swal.fire("Only jpg, jpeg, png and bmp formats are allowed");
        $(e).val('');
    }
    else {
        $('#disp').hide();
    }
}

//Adhar validation
function validateAadhar(val, id) {
    debugger;

    aadhar = $('#'+val).val();
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

//pin validation
function validatePin(val, id) {
    debugger;

    pin = val.value;
    var pinSixDigit = /^[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}$/;
    if (val.value == "") {
        $("#" + id + "").addClass('error-message');
        $(val).addClass('is-invalid');
        $("#" + id + "").html("This field is required");
    }
    else {
        if (pin.match(pinSixDigit)) {
            $("#" + id + "").removeClass('error-message');
            $(val).removeClass('is-invalid');
            $("#" + id + "").html("");
            return true;
        }
        else {
            $("#" + id + "").addClass('error-message');
            $(val).addClass('is-invalid');
            $("#pin").html("Please enter a valid PIN number");
            return false;
        }
    }
}

//Firstletter Capital validation
function firstLetterCaps(val, id) {
    debugger;

    stringval= val.value;
    if (val.value == "") {
        $("#" + id + "").addClass('error-message');
        $(val).addClass('is-invalid');
        $("#" + id + "").html("This field is required");
    }
    else {
        val.value = val.value.toLowerCase();
        val.value = val.value.
            replace(/(^\s*)|(\s*$)/gi, ""). // removes leading and trailing spaces
            replace(/[ ]{2,}/gi, "").       // replaces multiple spaces with one space 
            replace(/\n +/, "\n");
        val.value = val.value.substr(0, 1).toUpperCase() + val.value.substr(1).toLowerCase();
        $("#" + id + "").removeClass('error-message');
        $(val).removeClass('is-invalid');
        $("#" + id + "").html("");
        return val.value;
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
    var val = document.getElementById('BasicInfo_PanNo');    
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

//mobile validation
function validateMobile(val, id) {
    mobile = $('#'+val).val();
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

//email validation
function validateEmail(val, id) {
    email = val.value;
    if (val.value == "") {
        $("#" + id + "").addClass('error-message');
        $(val).addClass('is-invalid');
        $("#" + id + "").html("This field is required");
    }
    else {
        filter = /^\w+([\.-]?\w+)@\w+([\.-]?\w+)(\.\w{2,3})+$/
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

//DOB validation
function validateDOB(val, id) {
    debugger;
    dateofbirth = $('#BasicInfo_DOB').val();
    if (val.value == "") {
        $("#" + id + "").addClass('error-message');
        $(val).addClass('is-invalid');
        $("#" + id + "").html("This field is required");
    }
    else {
        debugger;

        var DOB = new Date(document.getElementById('BasicInfo_DOB').value);
        var DOBYear = Math.abs(DOB.getFullYear());

        var CurDate = new Date();
        var CurDateYear = Math.abs(CurDate.getFullYear())

        var years = parseInt(CurDateYear) - parseInt(DOBYear);

        var age = parseInt(years);
        //var now = new Date();
        //var birthdate = dateofbirth.split("/");
        //var born = new Date(birthdate[2], birthdate[0] - 1, birthdate[1]);
        //var birthday = new Date(now.getFullYear(), born.getMonth(), born.getDate());
        //if (now >= birthday)
        //    var age =  now.getFullYear() - born.getFullYear();
        //else
        //    var age = now.getFullYear() - born.getFullYear() - 1;
        if (age < 18) {
            $("#" + id + "").addClass('error-message');
            $(val).addClass('is-invalid');
            $("#" + id + "").html("Age must be greater than 18");
            return false;
        }
        else if (age > 58) {
            if (swal.fire("Candidate age greater than 58. Do you want to continue?")) {
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
            return true;
        }
    }
}

//DOJ validation
function validateDOJ(val, id) {
    debugger;
    if (val.value == "") {
        $("#" + id + "").addClass('error-message');
        $(val).addClass('is-invalid');
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
        $("#" + id + "").removeClass('error-message');
        $(val).removeClass('is-invalid');
        $("#" + id + "").html("");
        //trim(val);
    }
}

function onPhotoUploadChange(sender,id) {
    if (sender.value != "") {
        $('#removePhoto').show();
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
    var url = "/DataBank/PreEnrollment/GetCompensationGrid";
    $(".Compensation").load(url, { GradeCode: Code }, function () {
    });
}
function CTCCalculation() {
    debugger;
    var TotAmt = parseFloat("0");
    var TotGrossAmt = parseFloat("0");
    $('.AllowanceAmount').each(function (i, obj) {
        var Amt = parseFloat($("#Compensation_FixedAllowance_" + i + "__Amount").val());
        var type = $("#Compensation_FixedAllowance_" + i + "__AllowanceType").val();
        debugger;
        if (isNaN(Amt)) {
            Amt = parseFloat("0");
        }
        TotAmt = TotAmt + Amt;
        if (type != 'CTC Component') {
            TotGrossAmt = TotGrossAmt + Amt;
        }
        $("#Compensation_CTC").val(TotAmt);
        $("#Compensation_GrossSalary").val(TotGrossAmt);
    });

}

function validateDOR(val, id) {
    debugger;
    const doj = new Date($("#BasicInfo_AcceptedDOJ").val());
    const dor = new Date(val.value);
    if (doj > dor) {
        $(val).addClass('is-invalid');
        $("#" + id + "").addClass('error-message');
        $("#" + id + "").html("Renewal date should be greater than Joining date");
        return false;
    }
    if (val.value == "") {
        $("#" + id + "").addClass('error-message');
        $(val).addClass('is-invalid');
        $("#" + id + "").html("This field is required");
        return false;
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
            return true;
        }
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

//validate before submit
function validateAll() {
    debugger;
    //$.ajaxSetup({
    //    async: false
    //});

    
        //swal.fire(
        //    'Deleted!',
        //    'Your file has been deleted.',
        //    'success'
        //)

        var a = $("#BasicInfo_File_FileData").length;
        if ($("#BasicInfo_File_FileData").val() == "") {
            swal.fire('Candidate photo is required');
            return false;
        }
        else if ($("#BasicInfo_FullName").val() == "") {
            $("#BasicInfo_FullName").addClass('is-invalid');
            $("#fullname").addClass('error-message');
            $("#fullname").html("This field is required");
            $('#BasicInfo_FullName').focus();
            return false;
        }
        else if ($("#BasicInfo_AcceptedDOJ").val() == "") {
            debugger;
            $("#BasicInfo_AcceptedDOJ").addClass('is-invalid');
            $("#doj").addClass('error-message');
            $("#doj").html("This field is required");
            $('#BasicInfo_AcceptedDOJ').focus();
            return false;
        }
        else if (!validateMobile('PermanentAddress_MobileNo', 'mobile')) {
            debugger;
            //$("#PermanentAddress_MobileNo").addClass('is-invalid');
            //$("#mobile").addClass('error-message');
            //$("#mobile").html("This field is required");
            $('#PermanentAddress_MobileNo').focus();
           
            return false;
        }
        else if ($("#PermanentAddress_MailID").val() == "") {
            debugger;
            $("#PermanentAddress_MailID").addClass('is-invalid');
            $("#email").addClass('error-message');
            $("#email").html("This field is required");
            $('#PermanentAddress_MailID').focus();
            return false;
        }
        else {
            
            if ($("#BasicInfo_DOB").val() == "") {
                debugger;
                removeActiveTab()
                $("#tabOneTitle").addClass("active");
                $("#tabOne").addClass("active");
                $("#tabOne").css("display", "block");
                $("#BasicInfo_DOB").addClass('is-invalid');
                $("#dob").addClass('error-message');
                $("#dob").html("This field is required");
                $('#BasicInfo_DOB').focus();
                return false;
            }
            else if ($("#BasicInfo_Gender_Code").val() == "") {
                debugger;
                removeActiveTab()
                $("#tabOneTitle").addClass("active");
                $("#tabOne").addClass("active");
                $("#tabOne").css("display", "block");
                $("#BasicInfo_Gender_Code").addClass('is-invalid');
                $("#gender").addClass('error-message');
                $("#gender").html("This field is required");
                $('#BasicInfo_Gender_Code').focus();
                return false;
            }
            else if ($("#BasicInfo_FathersName").val() == "") {
                debugger;
                removeActiveTab()
                $("#tabOneTitle").addClass("active");
                $("#tabOne").addClass("active");
                $("#tabOne").css("display", "block");
                $("#BasicInfo_FathersName").addClass('is-invalid');
                $("#father").addClass('error-message');
                $("#father").html("This field is required");
                $('#BasicInfo_FathersName').focus();
                return false;
            }
            else if (!validateAadhar('BasicInfo_AdharNo','aadhar')) {
                debugger;
                removeActiveTab()
                $("#tabOneTitle").addClass("active");
                $("#tabOne").addClass("active");
                $("#tabOne").css("display", "block");
                //$("#BasicInfo_AdharNo").addClass('is-invalid');
                //$("#aadhar").addClass('error-message');
                //$("#aadhar").html("This field is required");
                $('#BasicInfo_AdharNo').focus();
                return false;
            }
            else if (!validatePan('pan')) {
                debugger;
                removeActiveTab()
                $("#tabOneTitle").addClass("active");
                $("#tabOne").addClass("active");
                $("#tabOne").css("display", "block");
                //$("#BasicInfo_PanNo").addClass('is-invalid');
                //$("#pan").addClass('error-message');
                $("#pan").html("This field is required");
                $('#BasicInfo_PanNo').focus();
                return false;
            }
            else if ($("#JobDetails_Category_Code").val() == "") {
                debugger;
                removeActiveTab()
                $("#tabTwoTitle").addClass("active");
                $("#tabTwo").addClass("active");
                $("#tabTwo").css("display", "block");
                $("#JobDetails_Category_Code").addClass('is-invalid');
                $("#category").addClass('error-message');
                $("#category").html("This field is required");
                $('#JobDetails_Category_Code').focus();
                return false;
            }
            else if ($("#JobDetails_Grade_Code").val() == "") {
                removeActiveTab()
                $("#tabTwoTitle").addClass("active");
                $("#tabTwo").addClass("active");
                $("#tabTwo").css("display", "block");
                $("#JobDetails_Grade_Code").addClass('is-invalid');
                $("#grade").addClass('error-message');
                $("#grade").html("This field is required");
                $('#JobDetails_Grade_Code').focus();
                return false;
            }
            else if ($("#JobDetails_Designation_Code").val() == "") {
                removeActiveTab()
                $("#tabTwoTitle").addClass("active");
                $("#tabTwo").addClass("active");
                $("#tabTwo").css("display", "block");
                $("#JobDetails_Designation_Code").addClass('is-invalid');
                $("#designation").addClass('error-message');
                $("#designation").html("This field is required");
                $('#JobDetails_Designation_Code').focus();
                return false;
            }
            else if ($("#JobDetails_Department_Code").val() == "") {
                removeActiveTab()
                $("#tabTwoTitle").addClass("active");
                $("#tabTwo").addClass("active");
                $("#tabTwo").css("display", "block");
                $("#JobDetails_Department_Code").addClass('is-invalid');
                $("#department").addClass('error-message');
                $("#department").html("This field is required");
                $('#JobDetails_Department_Code').focus();
                return false;
            }
            else if ($("#JobDetails_Location_Code").val() == "") {
                removeActiveTab()
                $("#tabTwoTitle").addClass("active");
                $("#tabTwo").addClass("active");
                $("#tabTwo").css("display", "block");
                $("#JobDetails_Location_Code").addClass('is-invalid');
                $("#location").addClass('error-message');
                $("#location").html("This field is required");
                $('#JobDetails_Location_Code').focus();
                return false;
            }
            else if ($("#JobDetails_PayrollGroup_Code").val() == "") {
                removeActiveTab()
                $("#tabTwoTitle").addClass("active");
                $("#tabTwo").addClass("active");
                $("#tabTwo").css("display", "block");
                $("#JobDetails_PayrollGroup_Code").addClass('is-invalid');
                $("#payrollgroup").addClass('error-message');
                $("#payrollgroup").html("This field is required");
                $('#JobDetails_PayrollGroup_Code').focus();
                return false;
            }
            else if (!validateDOR(document.getElementById('BasicInfo_RenewalDate'), 'dor')) {
                removeActiveTab();
                $("#tabTwoTitle").addClass("active");
                $("#tabTwo").addClass("active");
                $("#tabTwo").css("display", "block");
                $('#BasicInfo_RenewalDate').focus();
                debugger;
                return false;
            }
            //else if ($("#BasicInfo_RenewalDate").val() == "") {
            //    removeActiveTab()
            //    $("#tabTwoTitle").addClass("active");
            //    $("#tabTwo").addClass("active");
            //    $("#tabTwo").css("display", "block");
            //    $("#BasicInfo_RenewalDate").addClass('is-invalid');
            //    $("#dor").addClass('error-message');
            //    $("#dor").html("This field is required");
            //    $('#BasicInfo_RenewalDate').focus();
            //    return false;
            //}
            //else if (!calcExp('#BasicInfo_RenewalDate', 'todate')) {
            //    return false;
            //}
            //else if (RdiffDays < 15) {
            //    removeActiveTab()   
            //    swal.fire("Renewal Date less than 15 days. Do you want to continue?");
            //    $("#tabTwoTitle").addClass("active");
            //    $("#tabTwo").addClass("active");
            //    $("#tabTwo").css("display", "block");
            //    $('#BasicInfo_RenewalDate').focus();
            //    return false;
            //}
            else if ($("#JobDetails_NoticePeriod").val() == "") {
                debugger;
                removeActiveTab()
                $("#tabTwoTitle").addClass("active");
                $("#tabTwo").addClass("active");
                $("#tabTwo").css("display", "block");
                $("#JobDetails_NoticePeriod").addClass('is-invalid');
                $("#nop").addClass('error-message');
                    $("#nop").html("This field is required");
                $('#JobDetails_NoticePeriod').focus();
                return false;
            }

            else if (parseFloat($("#Compensation_OfferedSalary").val()) != parseFloat($("#Compensation_CTC").val())) {
                removeActiveTab()
                $(".tabThree").addClass("active");
                $("#tabThree").addClass("active");
                $("#tabThree").css("display", "block");
                //console.log(parseFloat($("#PreEnrollment_Compensation_OfferedSalary").val()));
                //console.log(parseFloat($("#PreEnrollment_Compensation_CTC").val()));
                $('#Compensation_OfferedSalary').focus();
                swal.fire("Offered salary and CTC not same!");
                return false;
            }
            else if ($("#Compensation_CTC").val() <= 0 || $("#Compensation_CTC").val() == null) {
                removeActiveTab()
                $(".tabThree").addClass("active");
                $("#tabThree").addClass("active");
                $("#tabThree").css("display", "block");
                if (swal.fire("Candidate salary amount is zero. Do you want to continue?"))
                    return true;
                else
                    return false;
            }
            else if ($("#Compensation_CTC").val() > 21000 && $("#Compensation_ESI").is(':checked')) {
                removeActiveTab()
                //var EsicheckBox = document.getElementById('PreEnrollment_Compensation_ESI');
                //if (checkBox.checked == true) {
                //    alert("Checked");
                //}
                $(".tabThree").addClass("active");
                $("#tabThree").addClass("active");
                $("#tabThree").css("display", "block");

                swal.fire("ESI Applicable Allowance is greater than 21000. So please remove ESI applicability");
                return false;
            }
            else if ($("#PermanentAddress_House").val() == "") {
                removeActiveTab()
                $("#tabFourTitle").addClass("active");
                $("#tabFour").addClass("active");
                $("#tabFour").css("display", "block");
                $("#PermanentAddress_House").addClass('is-invalid');
                $("#house").addClass('error-message');
                $("#house").html("This field is required");
                $('#PermanentAddress_House').focus();
                return false;
            }
            else if ($("#PermanentAddress_Street").val() == "") {
                removeActiveTab()
                $("#tabFourTitle").addClass("active");
                $("#tabFour").addClass("active");
                $("#tabFour").css("display", "block");
                $("#PermanentAddress_Street").addClass('is-invalid');
                $("#street").addClass('error-message');
                $("#street").html("This field is required");
                $('#PermanentAddress_Street').focus();
                return false;
            }
            else if ($("#PermanentAddress_City").val() == "") {
                removeActiveTab()
                $("#tabFourTitle").addClass("active");
                $("#tabFour").addClass("active");
                $("#tabFour").css("display", "block");
                $("#PermanentAddress_City").addClass('is-invalid');
                $("#city").addClass('error-message');
                $("#city").html("This field is required");
                $('#PermanentAddress_City').focus();
                return false;
            }
            else if ($("#PermanentAddress_PinCode").val() == "") {
                removeActiveTab()
                $("#tabFourTitle").addClass("active");
                $("#tabFour").addClass("active");
                $("#tabFour").css("display", "block");
                $("#PermanentAddress_PinCode").addClass('is-invalid');
                $("#pin").addClass('error-message');
                $("#pin").html("This field is required");
                $('#PermanentAddress_PinCode').focus();
                return false;
            }
            else if ($("#PermanentAddress_Country_Code").val() == "") {
                removeActiveTab()
                $("#tabFourTitle").addClass("active");
                $("#tabFour").addClass("active");
                $("#tabFour").css("display", "block");
                $("#PermanentAddress_Country_Code").addClass('is-invalid');
                $("#country").addClass('error-message');
                $("#country").html("This field is required");
                $('#PermanentAddress_Country_Code').focus();
                return false;
            }
            else if ($("#PermanentAddress_State_Code").val() == "") {
                removeActiveTab()
                $("#tabFourTitle").addClass("active");
                $("#tabFour").addClass("active");
                $("#tabFour").css("display", "block");
                $("#PermanentAddress_State_Code").addClass('is-invalid');
                $("#state").addClass('error-message');
                $("#state").html("This field is required");
                $('#PermanentAddress_State_Code').focus();
                return false;
            }
            else if ($("#PermanentAddress_District_Code").val() == "") {
                removeActiveTab()
                $("#tabFourTitle").addClass("active");
                $("#tabFour").addClass("active");
                $("#tabFour").css("display", "block");
                $("#PermanentAddress_District_Code").addClass('is-invalid');
                $("#district").addClass('error-message');
                $("#district").html("This field is required");
                $('#PermanentAddress_District_Code').focus();
                return false;
            }
            else {
                $.getJSON("/DataBank/PreEnrollment/AlreadyExistCheck", { Mobile: $("#PermanentAddress_MobileNo").val(), Pan: $("#BasicInfo_PanNo").val(), Aadhar: $("#BasicInfo_AdharNo").val() }, function (data) {
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
                                $("#preEnrollID").prop('disabled', true);
                                var MPPersonalInfo = new FormData($('#preSave')[0]);
                                // console.log(formData);
                                // $('#preSave').submit(function (e) {
                                //   e.preventDefault();
                                $.ajax({
                                    type: 'POST',
                                    url: "/DataBank/PreEnrollment/Create",
                                    //enctype: 'multipart/form-data',
                                    contentType: false,
                                    processData: false,
                                    dataType: 'json',
                                    data: MPPersonalInfo,
                                    //processData: false,
                                    //contentType: false,
                                    success: function (result) {
                                        if (result.success == true) {
                                            swal.fire(result.message).then(function () {

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
                        $("#preEnrollID").prop('disabled', true);
                        var MPPersonalInfo = new FormData($('#preSave')[0]);
                        // console.log(formData);
                        // $('#preSave').submit(function (e) {
                        //   e.preventDefault();
                        $.ajax({
                            type: 'POST',
                            url: "/DataBank/PreEnrollment/Create",
                            //enctype: 'multipart/form-data',
                            contentType: false,
                            processData: false,
                            dataType: 'json',
                            data: MPPersonalInfo,
                            //processData: false,
                            //contentType: false,
                            success: function (result) {
                                if (result.success == true) {
                                    swal.fire(result.message).then(function () {

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
                });

            }
        }
        //if ($("#Compensation_OfferedSalary").val() == $("#Compensation_CTC").val()) {
        //    console.log("CTC=offered salary");
        //}
        //else {
        //    swal.fire("Offered salary and CTC not same!");
        //    $('#Compensation_OfferedSalary').focus();
        //}
    



   
}

function removeActiveTab() {
    debugger
    $("#tabOne").removeClass("active");
    $("#tabOne").css("display", "none");
    $("#tabOneTitle").removeClass("active");

    $("#tabTwo").removeClass("active");
    $("#tabTwo").css("display", "none");
    $("#tabTwoTitle").removeClass("active");

    $(".tabThree").removeClass("active");
    $("#tabThree").css("display", "none");
    $("#tabThreeTitle").removeClass("active");

    $("#tabFour").removeClass("active");
    $("#tabFour").css("display", "none");
    $("#tabFourTitle").removeClass("active");
}
//state dropdown enable
function onCountryChangep(sender,id) {
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
    $.getJSON("/DataBank/PreEnrollment/GetState", { id: cid }, function (data) {

        $('#PermanentAddress_State_Code option').remove();
        $('#PermanentAddress_State_Code').append('<option value>--Select State--</option');

        for (var i = 0; i < data.length; i++) {
            $('#PermanentAddress_State_Code').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
        }
     
    });
    //Make Dtsrict Dropdwn Empty when no Country is Selected
    //if (cid == 0) {
        $.getJSON("/DataBank/PreEnrollment/GetDistrict", { id: 0 }, function (data) {

            $('#PermanentAddress_District_Code option').remove();
            $('#PermanentAddress_District_Code').append('<option value>--Select District--</option');

            for (var i = 0; i < data.length; i++) {
                $('#PermanentAddress_District_Code').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
            }
        });
       // }
}

//district dropdown enable
function onStateChangep(sender,id) {
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
    $.getJSON("/DataBank/PreEnrollment/GetDistrict", { id: cid }, function (data) {

        $('#PermanentAddress_District_Code option').remove();
        $('#PermanentAddress_District_Code').append('<option value>--Select District--</option');

        for (var i = 0; i < data.length; i++) {
            $('#PermanentAddress_District_Code').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
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

function onCategoryChange(val) {
    debugger;
    validate(val, 'category')
    var categoryCode = val.value;
    if (categoryCode == 3) {
        $('#renewal').html("Renewal Date");
        $('#BasicInfo_RenewalDate').attr('placeholder', 'Renewal Date');
        $('#asicInfo_RenewalDate').attr('disabled', true);
    }
    else if (categoryCode == 1) {
        $('#renewal').html("Probation Completion");
        $('#BasicInfo_RenewalDate').attr('placeholder', 'Probation Completion');
        $('#BasicInfo_RenewalDate').attr('disabled', false);
    }
    else if (categoryCode == 4) {
        $('#renewal').html("Contract Expiry");
        $('#BasicInfo_RenewalDate').attr('placeholder', 'Contract Expiry');
        $('#BasicInfo_RenewalDate').attr('disabled', false);
    }
    else if (categoryCode == 2) {
        $('#renewal').html("Trainee Completion");
        $('#BasicInfo_RenewalDate').attr('placeholder', 'Trainee Completion');
        $('#BasicInfo_RenewalDate').attr('disabled', false);
    }
}

//function CTCCalculation(Amount) {
//    debugger;
//    CTC = parseFloat(CTC) + parseFloat($(Amount).val());
   
//    if (isNaN(CTC)) {
//        CTC = parseFloat(0) + parseFloat($("#Compensation_OfferedSalary").val());
//        $("#Compensation_OfferedSalary").val(CTC);
//    }
//    else {
//        $("#Compensation_OfferedSalary").val(CTC);
//    }
//  }

//fill single value drpdown

function fillOnlyOneDropdown() {
    var category = $('#JobDetails_Category_Code > option').length;
    if (category == 2) {
        document.getElementById('JobDetails_Category_Code').selectedIndex = 1;
    }
    var grade = $('#JobDetails_Grade_Code > option').length;
    if (grade == 2) {
        document.getElementById('JobDetails_Grade_Code').selectedIndex = 1;
        var Code = $('#JobDetails_Grade_Code').val();
        var url = "/DataBank/PreEnrollment/GetCompensationGrid";
        $(".Compensation").load(url, { GradeCode: Code }, function () {
        });

    }
    var designation = $('#JobDetails_Designation_Code > option').length;
    if (designation == 2) {
        document.getElementById('JobDetails_Designation_Code').selectedIndex = 1;
    }
    var department = $('#JobDetails_Department_Code > option').length;
    if (department == 2) {
        document.getElementById('JobDetails_Department_Code').selectedIndex = 1;
    }
    var location = $('#JobDetails_Location_Code > option').length;
    if (location == 2) {
        document.getElementById('JobDetails_Location_Code').selectedIndex = 1;
    }
    var payroll = $('#JobDetails_PayrollGroup_Code > option').length;
    if (payroll == 2) {
        document.getElementById('JobDetails_PayrollGroup_Code').selectedIndex = 1;
    }
    var country = $('#PermanentAddress_Country_Code > option').length;
    if (country == 2) {
        document.getElementById('PermanentAddress_Country_Code').selectedIndex = 1;
    }

}

function validateNop(val, id) {
    debugger;
    var nop = $("#JobDetails_NoticePeriod").val();
    if (val.value == "") {
        $(val).addClass('is-invalid');
        $("#" + id + "").addClass('error-message');
        $("#" + id + "").html("This field is required");
    }
    else if (nop == 0) {
        $(val).addClass('is-invalid');
        $("#" + id + "").addClass('error-message');
        $("#" + id + "").html("Notice period never be 0");
    }
    else {
        $(val).removeClass('is-invalid');
        $("#" + id + "").removeClass('error-message');
        $("#" + id + "").html("");
        return val.value;
    }
    
    }

