var CTC = 0;
var RdiffDays = 0;
var Req = 0;;
var retirementType = 'DOJ';
var selectedTab = '';
var duplicationData;
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
//Adhar validation
function validateAadharPreEnroll(val, id, mandatory) {
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

//pin validation
function validatePin(val, id) {
    debugger;
        var pin=$('#PermanentAddress_PinCode').val();
    //if (val.value == null || val.value == 'undefined') {

    //    pin = '';
    //}
    //else {

    //pin = val.value;
    //}
    if (pin == "") {
        $("#" + id + "").addClass('error-message');
        $('#PermanentAddress_PinCode').addClass('is-invalid');
        $("#" + id + "").html("This field is required");
        return false;
    }
    else {
        if (pin.length == 6) {
            $("#" + id + "").removeClass('error-message');
            $('#PermanentAddress_PinCode').removeClass('is-invalid');
            $("#" + id + "").html("");
            return true;
        }
        else {
            $("#" + id + "").addClass('error-message');
            $('#PermanentAddress_PinCode').addClass('is-invalid');
            $("#pin").html("Please enter a valid PIN number");
            return false;
        }
    }
}

//Firstletter Capital validation
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
function validatePanPreEnroll(id, mandatory) {
    debugger;
    var val = document.getElementById('BasicInfo_PanNo');
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
            var val = document.getElementById('BasicInfo_PanNo');
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

//mobile validation

function validateMobile(val, id) {
    debugger
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
          
            return true;
        }
    }
}

//email validation
function validateEmail(val, id,mandatory) {
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

//DOB validation
function validateDOB(val, id) {
   
    dateofbirth = $('#BasicInfo_DOB').val();
    if (val.value == "") {
        $("#" + id + "").addClass('error-message');
        $(val).addClass('is-invalid');
        $("#" + id + "").html("This field is required");
    }
    else {
      

        var DOB = new Date(document.getElementById('BasicInfo_DOB').value);
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

//DOJ validation
function validateDOJ(val, id,NextClick) {
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
        $.getJSON("/DataBank/EditEmployeeInfo/CheckifAlreadySalaryProcessed", { Date: val.value  }, function (data) {
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

                    if (daysDiff >= 90 && NextClick !=1) {
                        swal.fire("The entered Date of Joining is 90 days or more after current date.");
                        //$("#BasicInfo_AcceptedDOJ").val('');
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
   // var url = "/DataBank/PreEnrollmentTest/GetCompensationGrid";
    //$("#preEnroll_Compensation").load(url, { GradeCode: Code }, function () {
    //});
}

function CTCCalculation()
{
    debugger;
    var TotAmt = parseFloat("0");
    var TotGrossAmt = parseFloat("0");
    var TotCTCAmt = parseFloat("0");
    var TotDedAmt = parseFloat("0");
    var TakeHome = parseFloat("0");
    $('.AllowanceAmount').each(function (i, obj) {//Gross Salary
        var Amt = parseFloat($("#Compensation_FixedAllowance_" + i + "__Amount").val());
       
        debugger;
        if (isNaN(Amt)) {
            Amt = parseFloat("0");
        }
        TotAmt = TotAmt + Amt;
       
        TotGrossAmt = TotGrossAmt + Amt;
       
        $("#GrossSal").html(TotGrossAmt);
        $("#Compensation_GrossSalary").val(TotGrossAmt);
    });
    $('.ProvisionsAmount').each(function (i, obj) {//CTC
        var Amt = parseFloat($("#Compensation_FixedProvisions_" + i + "__Amount").val());
        var CtcApp = parseInt($("#Compensation_FixedProvisions_" + i + "__CtcApplicable").val());
        debugger;
        if (isNaN(Amt)) {
            Amt = parseFloat("0");
        }
        if (CtcApp == 2) {
            TotCTCAmt = TotCTCAmt + Amt;
        }
       
        
    });
 
    $("#ctc").html(TotCTCAmt + TotGrossAmt);
    $("#Compensation_CTC").val(TotCTCAmt + TotGrossAmt);

    $('.DeductionsAmount').each(function (i, obj) {//Total Dedcutions
        var Amt = parseFloat($("#Compensation_FixedDeduction_" + i + "__Amount").val());        
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


function validateDOR(val, id) {
    debugger;
    const doj = new Date($("#BasicInfo_AcceptedDOJ").val());
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
function onNextClick(tabName, StepperName) {
    $.ajaxSetup({
        async: false
    });
    var i = 0;
    selectedTab = StepperName;
    if (tabName == "preEnroll_personal") {

        var a = $("#BasicInfo_File_FileData").length;
        if ($("#FiledValidations_1__Applicability").val() != "False") {
            if ($("#BasicInfo_File_FileName").val() == "") {
                swal.fire('Candidate photo is required');
                i++;
            }
        }
        if (!validateById("#BasicInfo_Salutation_Code", 'Saln')) {
            $('#BasicInfo_Salutation_Code').focus();
            i++;
        }
        if (!validateById("#BasicInfo_FullName", 'fullname')) {
            $('#BasicInfo_FullName').focus();
            i++;
        }

        if (!validateMobile('PermanentAddress_MobileNo', 'mobile')) {
            debugger;
            $('#PermanentAddress_MobileNo').focus();
            i++;
        }
        if (!validateEmail(document.getElementById('PermanentAddress_MailID'), 'email', 'FiledValidations_0__Applicability')) {
            $('#PermanentAddress_MailID').focus();
            i++;
        }
        /*        var a = validateDOJ(document.getElementById('BasicInfo_AcceptedDOJ'), 'doj');*/

        if (!validateDOJ(document.getElementById('BasicInfo_AcceptedDOJ'), 'doj',1)) {
            debugger
            $('#BasicInfo_AcceptedDOJ').focus();
            $('.dojpicker').datepicker('hide');
            i++;
        }
        if (!validateDOB(document.getElementById('BasicInfo_DOB'), 'dob')) {

            $('#BasicInfo_DOB').focus();
            $('.dobdatepicker').datepicker('hide');
            i++;
        }
        if (!validateAadharNew('hdnAadhar', 'aadhar', document.getElementById("hdnAadhar").value)) {
            $('#BasicInfo_AdharNo').focus();
            i++;
        }
        //if (!validateAadharPreEnroll('BasicInfo_AdharNo', 'aadhar', 'FiledValidations_2__Applicability')) {

        //    $('#BasicInfo_AdharNo').focus();
        //    i++;
        //}
        if (!validatePanPreEnroll('pan', 'FiledValidations_3__Applicability')) {

            $('#BasicInfo_PanNo').focus();
            i++;
        }
        if ($("#BasicInfo_Gender_Code").val() == "") {
            $("#BasicInfo_Gender_Code").addClass('is-invalid');
            $("#gender").addClass('error-message');
            $("#gender").html("This field is required");
            $('#BasicInfo_Gender_Code').focus();
            i++;
        }
        if ($("#BasicInfo_FathersName").val() == "") {
            $("#BasicInfo_FathersName").addClass('is-invalid');
            $("#father").addClass('error-message');
            $("#father").html("This field is required");
            $('#BasicInfo_FathersName').focus();
            i++;
        }
        if (i == 0) {           
            debugger            
                $.ajax({
                    type: 'GET',
                    async:false,
                    url: '/DataBank/PreEnrollmentTest/AlreadyExistCheck',
                    dataType: 'json',
                    data: { EmployeeGI: $('#EnrollGUID').val(), Mobile: $("#PermanentAddress_MobileNo").val(), Pan: $("#BasicInfo_PanNo").val(), Aadhar: document.getElementById("hdnAadhar").value },
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
                                        data: { AadharNo: document.getElementById("hdnAadhar").value },
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

                                if (data.alreadyExists_OnBoarding[i].code == 2) {
                                    debugger;
                                    pan = 2;
                                    $.ajax({
                                        type: 'GET',
                                        async: false,
                                        url: '/DataBank/PreEnrollmentTest/GetPANDuplication',
                                        dataType: 'html',
                                        data: { PANno: $("#BasicInfo_PanNo").val() },
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

                                if (data.alreadyExists_OnBoarding[i].code == 1) {
                                    mob = 3;
                                    $.ajax({
                                        type: 'GET',
                                        async: false,
                                        url: '/DataBank/PreEnrollmentTest/GetMobDuplication',
                                        dataType: 'html',
                                        data: { Mobile: $("#PermanentAddress_MobileNo").val() },
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
                               

                           


                            //else {

                            //    var msg = alertMessage(data.alreadyExists_OnBoarding);
                            //    swal.fire({
                            //        text: msg,
                            //        icon: 'warning',
                            //        showCancelButton: true,
                            //        confirmButtonColor: '#3085d6',
                            //        cancelButtonColor: '#d33',
                            //        confirmButtonText: 'Ok'
                            //    }).then((result) => {
                            //        if (result.isConfirmed) {
                            //            $('#preEnroll_address').addClass('hide');
                            //            $('#preEnroll_jobdetails').removeClass('hide');
                            //            $('#preEnroll_personal').addClass('hide');
                            //            $('#preEnroll_Compensation').addClass('hide');
                            //        }
                            //    })

                            //}


                        }
                        else {
                            $('#preEnroll_address').addClass('hide');
                            $('#preEnroll_jobdetails').removeClass('hide');
                            $('#preEnroll_personal').addClass('hide');
                            $('#preEnroll_Compensation').addClass('hide');
                        }
                    },
                    error: function (xhr, textStatus, errorThrown) {
                        swal.fire(xhr.responseText);

                    }
                });                
                
          
        }
    }
    else if (tabName == "preEnroll_jobdetails") {

        debugger;
        if ($("#JobDetails_Category_Code").val() == "") {

            $("#JobDetails_Category_Code").addClass('is-invalid');
            $("#category").addClass('error-message');
            $("#category").html("This field is required");
            $('#JobDetails_Category_Code').focus();
            i++;
        }
        if ($("#JobDetails_Grade_Code").val() == "") {

            $("#JobDetails_Grade_Code").addClass('is-invalid');
            $("#grade").addClass('error-message');
            $("#grade").html("This field is required");
            $('#JobDetails_Grade_Code').focus();
            i++;
        }
        if ($("#JobDetails_Designation_Code").val() == "") {

            $("#JobDetails_Designation_Code").addClass('is-invalid');
            $("#designation").addClass('error-message');
            $("#designation").html("This field is required");
            $('#JobDetails_Designation_Code').focus();
            i++;
        }
        if ($("#JobDetails_Department_Code").val() == "") {

            $("#JobDetails_Department_Code").addClass('is-invalid');
            $("#department").addClass('error-message');
            $("#department").html("This field is required");
            $('#JobDetails_Department_Code').focus();
            i++;
        }
        if ($("#JobDetails_Location_Code").val() == "") {

            $("#JobDetails_Location_Code").addClass('is-invalid');
            $("#location").addClass('error-message');
            $("#location").html("This field is required");
            $('#JobDetails_Location_Code').focus();
            i++;
        }
        //if ($("#JobDetails_PayrollGroup_Code").val() == "") {

        //    $("#JobDetails_PayrollGroup_Code").addClass('is-invalid');
        //    $("#payrollgroup").addClass('error-message');
        //    $("#payrollgroup").html("This field is required");
        //    $('#JobDetails_PayrollGroup_Code').focus();
        //    i++;
        //}
       
        if (retirementType != 'DOB') {
            if ($('#BasicInfo_RenewalDate').val() == '') {
                $("#dor").addClass('error-message');
                $('#BasicInfo_RenewalDate').addClass('is-invalid');
                $("#dor").html("This field is required");
                $('#BasicInfo_RenewalDate').focus();
                $('.renewaldatepicker').datepicker('hide');
                i++;
            }
        }
        if ($("#FiledValidations_4__Applicability").val() != "False") { 
            if ($("#JobDetails_NoticePeriod").val() == "") {

                $("#JobDetails_NoticePeriod").addClass('is-invalid');
                $("#nop").addClass('error-message');
                $("#nop").html("This field is required");
                $('#JobDetails_NoticePeriod').focus();
                i++;
        }
    }
        if (retirementType != 'DOB') {
            if ($('#BasicInfo_RenewalDate').val() != '') {

                debugger;
                var val = document.getElementById('BasicInfo_RenewalDate');
                var id = 'dor';
                const doj = new Date($("#BasicInfo_AcceptedDOJ").val());
                const dor = new Date(val.value);
                if (doj > dor) {
                    $(val).addClass('is-invalid');
                    $("#" + id + "").addClass('error-message');
                    $("#" + id + "").html("Renewal date should be greater than Joining date");
                    $('#BasicInfo_RenewalDate').focus();
                    $('.renewaldatepicker').datepicker('hide');
                    i++;
                }

                else {
                    if (RdiffDays != 0) {
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

                                }
                                else {
                                    $('#BasicInfo_RenewalDate').focus();
                                    $('.renewaldatepicker').datepicker('hide');
                                    val.value = "";
                                    i = 0;
                                }
                            })
                        }
                        else {
                            $("#" + id + "").removeClass('error-message');
                            $(val).removeClass('is-invalid');
                            $("#" + id + "").html("");

                        }
                    }
                    

                }

            }
        }
        if (i == 0) {
            $('#preEnroll_jobdetails').addClass('hide');
            $('#preEnroll_personal').addClass('hide');
            $('#preEnroll_address').addClass('hide');
            $('#preEnroll_Compensation').removeClass('hide');
        }
        
    }
        

    else if (tabName == "preEnroll_Compensation") {
 /* Commented . Need to verify - Rosmy */
        //if (parseFloat($("#Compensation_OfferedSalary").val()) != parseFloat($("#Compensation_CTC").val())) {
        //    removeActiveTab()
        //    $(".tabThree").addClass("active");
        //    $("#tabThree").addClass("active");
        //    $("#tabThree").css("display", "block");
        //    swal.fire("Approved CTC and CTC not same!");
        //    i++;
        //}
 /* Commented . Need to verify - Rosmy */

        //if (parseFloat($("#Compensation_GrossSalary").val()) != parseFloat($("#Compensation_ApprovedGross").val())) {
        //    removeActiveTab()
        //    $(".tabThree").addClass("active");
        //    $("#tabThree").addClass("active");
        //    $("#tabThree").css("display", "block");
        //    //console.log(parseFloat($("#PreEnrollment_Compensation_OfferedSalary").val()));
        //    //console.log(parseFloat($("#PreEnrollment_Compensation_CTC").val()));
        //    /*$('#Compensation_OfferedSalary').focus();*/
        //    swal.fire("Approved Gross and Gross Salary not same!");
        //    i++;
        //}
       
        /*else if */if($("#Compensation_CTC").val() > 21000 && $("#Compensation_ESI").is(':checked')) {
            removeActiveTab()
            //var EsicheckBox = document.getElementById('PreEnrollment_Compensation_ESI');
            //if (checkBox.checked == true) {
            //    alert("Checked");
            //}
            $(".tabThree").addClass("active");
            $("#tabThree").addClass("active");
            $("#tabThree").css("display", "block");

            swal.fire("ESI Applicable Allowance is greater than 21000. So please remove ESI applicability");
            i++;
        }
        debugger;
        var warningMsg = "";
     

         if (parseInt($('#Compensation_OfferedSalary').val()) < parseInt($('#MinimumSalary').val()))
             warningMsg = "Offered salary less than minimum salary. Do you want to continue?";
         else if ($("#Compensation_CTC").val() <= 0 || $("#Compensation_CTC").val() == null)
             warningMsg = "Candidate salary amount is zero. Do you want to continue?";
        else
            warningMsg = "";

        
        if (i == 0) {

            if (warningMsg != "") {
                swal.fire({
                    text: warningMsg,
                    icon: 'warning',
                    showCancelButton: true,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33',
                    confirmButtonText: 'Ok'
                }).then((result) => {
                    if (result.isConfirmed) {

                        $('#preEnroll_address').removeClass('hide');
                        $('#preEnroll_Compensation').addClass('hide');
                        $('#preEnroll_jobdetails').addClass('hide');
                        $('#preEnroll_personal').addClass('hide');
                    }
                    
                })
            }
            else {
                $('#preEnroll_address').removeClass('hide');
                $('#preEnroll_Compensation').addClass('hide');
                $('#preEnroll_jobdetails').addClass('hide');
                $('#preEnroll_personal').addClass('hide');
            }
        }
    }
}
function savePreEnroll(formName) {
    debugger;
    var renewaldate = $('#BasicInfo_RenewalDate').val();
    var i = 0;
    if (formName != "OfferProcessing")
        url = "/DataBank/PreEnrollmentTest/Create";
    else
        url = "/Recruitment/OfferProcessing/SaveOfferProcessing";
    var a = $("#BasicInfo_File_FileData").length;
    if ($("#FiledValidations_1__Applicability").val() != "False") {
        if ($("#BasicInfo_File_FileName").val() == "") {
            swal.fire('Candidate photo is required');
            i++;
        }
    }
    if (!validateById("#BasicInfo_FullName",'fullname')) {  
        $('#BasicInfo_FullName').focus();
        i++;
    }
 
    if (!validateMobile('PermanentAddress_MobileNo', 'mobile')) {
        debugger;
        $('#PermanentAddress_MobileNo').focus();
        i++;
    }
    if (!validateEmail(document.getElementById('PermanentAddress_MailID'), 'email', 'FiledValidations_0__Applicability')) {
        $('#PermanentAddress_MailID').focus();
        i++;
    }

    if (!validateDOJ(document.getElementById('BasicInfo_AcceptedDOJ'), 'doj')) {

        $('#BasicInfo_AcceptedDOJ').focus();
        $('.dojpicker').datepicker('hide');
        i++;
    }
    
    
    if (!validateById("#PermanentAddress_House", 'house'))
    {              
         $('#PermanentAddress_House').focus();
         i++;
    }
    if (!validateById("#PermanentAddress_Street", 'street'))
    {        
       $('#PermanentAddress_Street').focus();
       i++;
     }
    if (!validateById("#PermanentAddress_City", 'city'))
    {            
     $('#PermanentAddress_City').focus();
     i++;
     }
    if (!validatePin($('#PermanentAddress_PinCode'), 'pin')) {
           $('#PermanentAddress_PinCode').focus();
           i++;
             }    
    if (!validateById("#PermanentAddress_Country_Code", 'country'))
    {        
      $('#PermanentAddress_Country_Code').focus();
      i++;
            }
    if (!validateById("#PermanentAddress_State_Code",'state')) {
           
                $('#PermanentAddress_State_Code').focus();
                i++;
    }
    if (!validateById("#PermanentAddress_District_Code", 'district'))
    {             
     $('#PermanentAddress_District_Code').focus();
     i++;
      }
            if (i > 0) {
                return false;
            }
            else {
                swal.fire({
                    text: "Do you want to Save?",
                    icon: 'warning',
                    showCancelButton: true,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33',
                    confirmButtonText: 'Ok'
                }).then((result) => {
                    if (result.isConfirmed) {
                       
                        var MPPersonalInfo = new FormData($('#preSave')[0]);
                        MPPersonalInfo.append('Renewaldt', renewaldate);
                        $.ajax({
                            type: 'POST',
                            url: url ,
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
                                        if (formName != "OfferProcessing") {
                                            var url = "/DataBank/OnBoarding";
                                            window.location.href = url;
                                        }
                                        else {
                                            var url = '/Recruitment/OfferProcessing/OfferVerification';
                                            window.location.href = url;
                                        }
                                      
                                    });

                                }
                                else {
                                    swal.fire(result.message).then(function () {

                                    });
                                }

                            },
                            error: function (xhr, textStatus, errorThrown) {
                                document.body.id = "kt_body";
                                $('body').html(xhr.responseText);
                            }
                        });
                    }
                });
                

            }
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
    $.ajaxSetup({
        async: false
    });
    $.getJSON("/DataBank/PreEnrollment/GetState", { id: cid }, function (data) {
      
        $('#PermanentAddress_State_Code option').remove();
        $('#PermanentAddress_State_Code').append('<option value>--Select State--</option');

        for (var i = 0; i < data.length; i++) {
            debugger
            $('#PermanentAddress_State_Code').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
        }
     
    });
    //Make Dtsrict Dropdwn Empty when no Country is Selected
    //if (cid == 0) {
    $.getJSON("/DataBank/PreEnrollment/GetDistrict", { id: $("#PermanentAddress_State_Code").val() }, function (data) {
            debugger
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
        debugger
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
    var doj = $('#BasicInfo_AcceptedDOJ').val();
    var dob = $('#BasicInfo_DOB').val();
    //var CategoryName = $('#JobDetails_Category_Code option:selected').text();
    //var FirstFourCategoryName = CategoryName.substring(0, 4);
    //if (FirstFourCategoryName == "Perm") {
    //    $('#renewal').html("Retirement Date");
    //    $('#BasicInfo_RenewalDate').attr('placeholder', 'Retirement Date');
    //    $('#BasicInfo_RenewalDate').attr('disabled', true);
    //}
    //else {
    //    $('#renewal').html("Renewal Date");
    //    $('#BasicInfo_RenewalDate').attr('placeholder', 'Renewal Date');
    //}
    
    //url = "/DataBank/PreEnrollmentTest/GetRenewalDate?CategoryCode=" + categoryCode+"&DOB="+dob+"&DOJ="+doj;
    //window.location.href = url;
    $.getJSON("/DataBank/PreEnrollmentTest/GetRenewalDate", { CategoryCode: categoryCode, DOB: dob, DOJ: doj }, function (data) {
        debugger;
        $('#BasicInfo_RenewalDate').remove();
        $('#renewaldt').append('<input type = "text" id = "BasicInfo_RenewalDate" class="form-control kt_datepicker_2 renewaldatepicker" placeholder = "Renewal Date"  onchange = "validateDOR(this,\'dor\')" onclick = "$(\'.renewaldatepicker\').datepicker(\'show\')" >');
        $('#BasicInfo_RenewalDate').val(_GetFormattedDate(data));
    });
    $.getJSON("/DataBank/PreEnrollmentTest/GetRetirementType", { CategoryCode: categoryCode, DOB: dob, DOJ: doj }, function (data) {
        debugger;
        /*$('#PreEnrollment_BasicInfo_RetirementType').val(data);*/
        if (data == "DOB") {
            retirementType = data;
            $('.renewaldiv').addClass('hide');
        }
        else {
            retirementType = data;
            $('.renewaldiv').removeClass('hide');
        }
        console.log(data);

    });
   
}

//function getRetirement(categoryCode) {
//    debugger;
//    var doj = $('#PreEnrollment_BasicInfo_AcceptedDOJ').val();
//    var dob = $('#PreEnrollment_BasicInfo_DOB').val();
//    $.getJSON("/DataBank/PreEnrollmentTest/GetRetirementType", { CategoryCode: categoryCode, DOB: dob, DOJ: doj }, function (data) {
//        debugger;
//        /*$('#PreEnrollment_BasicInfo_RetirementType').val(data);*/
//        console.log(data);

//    });
//}

function fillOnlyOneDropdown() {
    debugger
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

function onStepCircleClick(StepperName) {
    debugger;
    if (StepperName == "Personal-Info") {
        $('#preEnroll_address').addClass('hide');
        $('#preEnroll_Compensation').addClass('hide');
        $('#preEnroll_jobdetails').addClass('hide');
        $('#preEnroll_personal').removeClass('hide');
    }
    else if (StepperName == "Job-Details") {
        $('#preEnroll_address').addClass('hide');
        $('#preEnroll_Compensation').addClass('hide');
        $('#preEnroll_jobdetails').removeClass('hide');
        $('#preEnroll_personal').addClass('hide');
    }
    else if (StepperName == "Compensation") {
        $('#preEnroll_address').addClass('hide');
        $('#preEnroll_Compensation').removeClass('hide');
        $('#preEnroll_jobdetails').addClass('hide');
        $('#preEnroll_personal').addClass('hide');
    }
    else if (StepperName == "Address") {
        $('#preEnroll_address').removeClass('hide');
        $('#preEnroll_Compensation').addClass('hide');
        $('#preEnroll_jobdetails').addClass('hide');
        $('#preEnroll_personal').addClass('hide');
    }
}


function onSalaryStructureChange() {

   //int GCCODE, int LOCCODE, decimal CTC, string Department, decimal GROSS
    var code = $("#Compensation_SalaryStructure_Code").val();
    var CTC = $('#Compensation_OfferedSalary').val();
    var GrossSalary = $('#Compensation_ApprovedGross').val();
    var VDAGroup = $('#JobDetails_VDAGroup_Code').val();
    var GroupCompany = $('#JobDetails_Company_Code').val();
    var LocationCode = $('#JobDetails_Location_Code').val();
    var DepartmentCode = $('#JobDetails_Department_Code').val();
    //var gross = parseFloat(GrossSalary);

   
    if (validateById("#Compensation_SalaryStructure_Code", "salaryStruct") && validateById("#Compensation_OfferedSalary", 'salary')) {
        if (parseFloat(CTC) > 0 && parseFloat(GrossSalary)>0) {
            var url = "/DataBank/PreEnrollmentTest/GetSalaryStructureAlwDedGrid";
            $('#preEnroll_Compensation').load(url, { SalaryStructureCode: code, CTC: CTC, GrossSalary: GrossSalary, GroupCompany: GroupCompany, LocationCode: LocationCode, DepartmentCode: DepartmentCode, VDAGroup: VDAGroup },
                function () { CTCCalculation(); });
        }
        
    }
    else {
        $('.compensationPreenrollTables tbody tr').remove();
        $("#takeHome").html('');
        $("#totDeductions").html('');
        $("#ctc").html('');
        $("#Compensation_CTC").val(0);
        //$("#GrossSal").html('');
        //$("#Compensation_GrossSalary").val(0);
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
    if ($("#BasicInfo_File_FileData").val() == null)
        swal.fire("No file Uploaded");

    else {
        if (CheckProfilePic(document.getElementById('BasicInfo_File_FileData'))) {
            var photo = new FormData($('#preSave')[0]);
            //console.log(photo);
            $.ajax({

                type: 'POST',
                url: '/DataBank/PreEnrollmentTest/SaveProfileToFolder?SelectedTab=' + selectedTab,
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
function fillNoticeperiod(sender)
{
    debugger
    $.getJSON("/DataBank/PreEnrollmentTest/GetNoticeperiod", { DesignationCode:$(sender).val()}, function (data) {

        $("#JobDetails_NoticePeriod").val(data);

    });
}
function madatoryCheck() {
    debugger;
    if ($("#FiledValidations_2__Applicability").val() == "False")
        $('#adharMandatory').html("");
    if ($("#FiledValidations_3__Applicability").val() == "False") 
        $('#panMandatory').html("");
}

function DuplicationOkClick() {
    if (duplicationData.alreadyExists_OnBoarding.length <= 1) {
        $('#preEnroll_address').addClass('hide');
        $('#preEnroll_jobdetails').removeClass('hide');
        $('#preEnroll_personal').addClass('hide');
        $('#preEnroll_Compensation').addClass('hide');
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
                $('#preEnroll_address').addClass('hide');
                $('#preEnroll_jobdetails').removeClass('hide');
                $('#preEnroll_personal').addClass('hide');
                $('#preEnroll_Compensation').addClass('hide');
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
    $.getJSON("/DataBank/PreEnrollmentTest/GetPayrollGroup", { id: cid }, function (data) {
        debugger
        $('#JobDetails_PayrollGroup_Code option').remove();
        $('#JobDetails_PayrollGroup_Code').append('<option value>--Payroll Group--</option');

        for (var i = 0; i < data.length; i++) {
            $('#JobDetails_PayrollGroup_Code').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
        }
        if (data.length > 0) {
            $('#JobDetails_PayrollGroup_Code').val(data[0].value);
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
    $.getJSON("/DataBank/PreEnrollmentTest/GetSalaryStructure", { id: cid }, function (data) {
        debugger
        $('#Compensation_SalaryStructure_Code option').remove();
        $('#Compensation_SalaryStructure_Code').append('<option value>--Salary Band--</option');

        for (var i = 0; i < data.length; i++) {
            $('#Compensation_SalaryStructure_Code').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
        }
        if (data.length > 0) {
            $('#Compensation_SalaryStructure_Code').val(data[0].value);
        }
    });
   
    onSalaryStructureChange();
    fillMinCTC();
}
function fillMinCTC() {
    debugger
    var LocationCode = $('#JobDetails_Location_Code').val();
    var WageGradeCode  = $('#JobDetails_WageGrade_Code').val();
    if (WageGradeCode != null && LocationCode != null) {
        $.getJSON("/DataBank/PreEnrollmentTest/GetMinCTC", { WageGradeCode: WageGradeCode, LocationCode: LocationCode }, function (data) {

            $("#Compensation_MinCTC").val(data);

        });
    }
}
function PopUp_Preview() {
    debugger
    var LocationCode = $('#JobDetails_Location_Code').val();
    var WageGradeCode = $('#JobDetails_WageGrade_Code').val();
    var CTC = $('#Compensation_CTC').val();

    if (WageGradeCode != null && WageGradeCode != "" && LocationCode != null && LocationCode != "" && parseFloat(CTC)>0) {
        debugger
        var MPPersonalInfo = new FormData($('#preSave')[0]);

        $.ajax({
            type: 'POST',
            url: '/DataBank/PreEnrollmentTest/SalarySummary',
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
                $('#edoj').text('EDOJ:    ' + $('#BasicInfo_AcceptedDOJ').val());
            },
            error: function (xhr, textStatus, errorThrown) {
                console.log(xhr.responseText);
                swal.fire(xhr.responseText);

            }
        });
    }

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