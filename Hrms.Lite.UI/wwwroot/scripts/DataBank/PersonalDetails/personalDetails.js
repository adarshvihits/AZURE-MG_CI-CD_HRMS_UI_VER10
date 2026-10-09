
/*const { block } = require("../../../js/scripts.bundle");*/

//state dropdown enable
filter = /^\w+([\.-]?\w+)@\w+([\.-]?\w+)(\.\w{2,3})+$/;
function CheckExtensionAndSize(e) {
    debugger;
    console.log(e);
    var size = $(e)[0].files[0].size;
    var filename = $(e)[0].files[0].name;
    var fileExtension = ['jpeg', 'jpg', 'png', 'doc', 'docx', 'pdf'];
    console.log(size);
    if (size > 1024 * 1024) {

        swal.fire('File size should be less than 1 MB.');
        $(e).val('');
        pressed();
        return false;
    }
    else if ($.inArray($(e).val().split('.').pop().toLowerCase(), fileExtension) == -1) {
        swal.fire("Only pdf,jpg,jpeg,png,doc and docx formats are allowed");
        $(e).val('');
        pressed();
        return false;
    }

}
function onCountryChangepd(sender, id) {
    debugger;
    var cid = $(sender).val();
    if (cid == "") {
        $(sender).addClass('is-invalid');
        $("#" + id + "").addClass('error-message');
        $("#" + id + "").html("This field is required");
    }
    else {
        $(sender).removeClass('is-invalid');
        $("#" + id + "").removeClass('error-message');
        $("#" + id + "").html("");
    }
    $.getJSON("/DataBank/PersonalDetails/GetState", { id: cid }, function (data) {

        $('#Address_PermanentAddress_State_Code option').remove();
        $('#Address_PermanentAddress_State_Code').append('<option value="">--Select State--</option');

        for (var i = 0; i < data.length; i++) {
            $('#Address_PermanentAddress_State_Code').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
        }

    });
    //Make Dtsrict Dropdwn Empty when no Country is Selected
    /*if (cid == 0) {*/
        $.getJSON("/DataBank/PersonalDetails/GetDistrict", { id: 0 }, function (data) {

            $('#Address_PermanentAddress_District_Code option').remove();
            $('#Address_PermanentAddress_District_Code').append('<option value="">--Select District--</option');

            for (var i = 0; i < data.length; i++) {
                $('#Address_PermanentAddress_District_Code').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
            }
        });
   /* }*/
}

//district dropdown enable
function onStateChangepd(sender, id) {
    debugger;
    var cid = $(sender).val();
    if (cid == 0 || cid == "") {
        $(sender).addClass('is-invalid');
        $("#" + id + "").addClass('error-message');
        $("#" + id + "").html("This field is required");
    }
    else {
        $(sender).removeClass('is-invalid');
        $("#" + id + "").removeClass('error-message');
        $("#" + id + "").html("");
    }
    $.getJSON("/DataBank/PersonalDetails/GetDistrict", { id: cid }, function (data) {

        $('#Address_PermanentAddress_District_Code option').remove();
        $('#Address_PermanentAddress_District_Code').append('<option value="">--Select District--</option');

        for (var i = 0; i < data.length; i++) {
            $('#Address_PermanentAddress_District_Code').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
        }

    });
}

function onDistrictChangepd(sender, id) {
    debugger;
    var cid = $(sender).val();
    if (cid == "" || cid == 0) {
        $(sender).addClass('is-invalid');
        $("#" + id + "").addClass('error-message');
        $("#" + id + "").html("This field is required");
    }
    else {
        $(sender).removeClass('is-invalid');
        $("#" + id + "").removeClass('error-message');
        $("#" + id + "").html("");
    }
}

function fillAddress() {
    $.ajaxSetup({
        async: false
    });
    debugger;
    //if ($(this).attr('checked')) {

    if ($("#Address_SameAsAddressCheck").prop('checked') == true) {
        debugger;
        $("#Address_PresentAddress_House").val($("#Address_PermanentAddress_House").val());
        $("#Address_PresentAddress_Street").val($("#Address_PermanentAddress_Street").val());
        $("#Address_PresentAddress_City").val($("#Address_PermanentAddress_City").val());
        $("#Address_PresentAddress_PinCode").val($("#Address_PermanentAddress_PinCode").val());
        $("#Address_PresentAddress_Country_Code").val($("#Address_PermanentAddress_Country_Code").val());
        $.getJSON("/DataBank/PersonalDetails/GetState", { id: $("#Address_PermanentAddress_Country_Code").val() }, function (data) {

            $('#Address_PresentAddress_State_Code option').remove();
            $('#Address_PresentAddress_State_Code').append('<option value="">--Select--</option');

            for (var i = 0; i < data.length; i++) {
                $('#Address_PresentAddress_State_Code').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
            }
            document.getElementById('Address_PresentAddress_State_Code').selectedIndex = document.getElementById('Address_PermanentAddress_State_Code').selectedIndex;
        });

        $.getJSON("/DataBank/PersonalDetails/GetDistrict", { id: $("#Address_PermanentAddress_State_Code").val() }, function (data) {

            $('#Address_PresentAddress_District_Code option').remove();
            $('#Address_PresentAddress_District_Code').append('<option value="">--Select--</option');

            for (var i = 0; i < data.length; i++) {
                $('#Address_PresentAddress_District_Code').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
            }
            document.getElementById('Address_PresentAddress_District_Code').selectedIndex = document.getElementById('Address_PermanentAddress_District_Code').selectedIndex;
        });
        //$("#Address_PresentAddress_State_Code").val($("#Address_PermanentAddress_State_Code").val());
        //$("#Address_PresentAddress_District_Code").val($("#Address_PermanentAddress_District_Code").val());

        validateById('#Address_PresentAddress_House','prhouse');
        validateById('#Address_PresentAddress_Street','prstreet');
        validateById('#Address_PresentAddress_City','prcity');
        validateById('#Address_PresentAddress_PinCode','prpin');
        validateById('#Address_PresentAddress_Country_Code','prcountry');
        validateById('#Address_PresentAddress_State_Code','prstate');
        validateById('#Address_PresentAddress_District_Code','prdistrict');        
        

    }
    else {
        $("#Address_PresentAddress_House").val('');
        $("#Address_PresentAddress_Street").val('');
        $("#Address_PresentAddress_City").val('');
        $("#Address_PresentAddress_PinCode").val('');
        /*$('#Address_PresentAddress_Country_Code').selectedIndex = 0;*/
        document.getElementById('Address_PresentAddress_Country_Code').selectedIndex = 0;
        document.getElementById('Address_PresentAddress_State_Code').selectedIndex = 0;
        document.getElementById('Address_PresentAddress_District_Code').selectedIndex = 0;
    }
}


function onCountryChangepresentpd(sender,id) {
    debugger
    var cid = $(sender).val();
    if (cid == "") {
        $(sender).addClass('is-invalid');
        $("#" + id + "").addClass('error-message');
        $("#" + id + "").html("This field is required");
    }
    else {
        $(sender).removeClass('is-invalid');
        $("#" + id + "").removeClass('error-message');
        $("#" + id + "").html("");
    }
    $.getJSON("/DataBank/PersonalDetails/GetState", { id: cid }, function (data) {

        $('#Address_PresentAddress_State_Code option').remove();
        $('#Address_PresentAddress_State_Code').append('<option value="">--Select State--</option');

        for (var i = 0; i < data.length; i++) {
            $('#Address_PresentAddress_State_Code').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
        }

    });
    //Make Dtsrict Dropdwn Empty when no Country is Selected
    /*if (cid == 0) {*/
    $.getJSON("/DataBank/PersonalDetails/GetDistrict", { id: 0 }, function (data) {

        $('#Address_PresentAddress_District_Code option').remove();
        $('#Address_PresentAddress_District_Code').append('<option value="">--Select District--</option');

        for (var i = 0; i < data.length; i++) {
            $('#Address_PresentAddress_District_Code').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
        }
    });
    /* }*/
}

function onSkillChange(sender, id) {
    debugger;
    var cid = $(sender).val();
    if (cid == "") {
        $(sender).addClass('is-invalid');
        $("#" + id + "").addClass('error-message');
        $("#" + id + "").html("This field is required");
    }
    else {
        $(sender).removeClass('is-invalid');
        $("#" + id + "").removeClass('error-message');
        $("#" + id + "").html("");
    }
    $.getJSON("/DataBank/PersonalDetails/GetSkill", { id: cid }, function (data) {

        $('#GetSkills_Skill_Code option').remove();
        $('#GetSkills_Skill_Code').append('<option value="">--Select--</option');

        for (var i = 0; i < data.length; i++) {
            $('#GetSkills_Skill_Code').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
        }

    });

}

function onQualificationTypeChange(sender, id) {
    debugger;
    var cid = $(sender).val();
    if (cid == "") {
        $(sender).addClass('is-invalid');
        $("#" + id + "").addClass('error-message');
        $("#" + id + "").html("This field is required");
    }
    else {
        $(sender).removeClass('is-invalid');
        $("#" + id + "").removeClass('error-message');
        $("#" + id + "").html("");
    }
    $.getJSON("/DataBank/PersonalDetails/GetQualification", { id: cid }, function (data) {

        $('#GetEducation_Qualification_Code option').remove();
        $('#GetEducation_Qualification_Code').append('<option value>--Select Qualification--</option');

        for (var i = 0; i < data.length; i++) {
            $('#GetEducation_Qualification_Code').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
        }

    });
    //Make Dtsrict Dropdwn Empty when no Country is Selected
    /*if (cid == 0) {*/
    //niv$.getJSON("/DataBank/PersonalDetails/GetDistrict", { id: 0 }, function (data) {

    //    $('#Address_PresentAddress_District_Code option').remove();
    //    $('#Address_PresentAddress_District_Code').append('<option value="0">--Select--</option');

    //    for (var i = 0; i < data.length; i++) {
    //        $('#Address_PresentAddress_District_Code').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
    //    }
    //});
    /* }*/
}

function onQualificationChange(sender, id) {
    debugger;
    var cid = $(sender).val();
    if (cid == "") {
        $(sender).addClass('is-invalid');
        $("#" + id + "").addClass('error-message');
        $("#" + id + "").html("This field is required");
    }
    else {
        $(sender).removeClass('is-invalid');
        $("#" + id + "").removeClass('error-message');
        $("#" + id + "").html("");
    }
    $.getJSON("/DataBank/PersonalDetails/GetSpecialisation", { id: cid }, function (data) {

        $('#GetEducation_Specialisation_Code option').remove();
        $('#GetEducation_Specialisation_Code').append('<option value>--Select Specialization of Branch--</option');

        for (var i = 0; i < data.length; i++) {
            $('#GetEducation_Specialisation_Code').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
        }

    });
    //Make Dtsrict Dropdwn Empty when no Country is Selected
    /*if (cid == 0) {*/
    //niv$.getJSON("/DataBank/PersonalDetails/GetDistrict", { id: 0 }, function (data) {

    //    $('#Address_PresentAddress_District_Code option').remove();
    //    $('#Address_PresentAddress_District_Code').append('<option value="0">--Select--</option');

    //    for (var i = 0; i < data.length; i++) {
    //        $('#Address_PresentAddress_District_Code').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
    //    }
    //});
    /* }*/
}

//district dropdown enable
function onStateChangepresentpd(sender,id) {
    debugger;
    var cid = $(sender).val();
    if (cid == "" || cid == 0) {
        $(sender).addClass('is-invalid');
        $("#" + id + "").addClass('error-message');
        $("#" + id + "").html("This field is required");
    }
    else {
        $(sender).removeClass('is-invalid');
        $("#" + id + "").removeClass('error-message');
        $("#" + id + "").html("");
    }
    $.getJSON("/DataBank/PersonalDetails/GetDistrict", { id: cid }, function (data) {

        $('#Address_PresentAddress_District_Code option').remove();
        $('#Address_PresentAddress_District_Code').append('<option value="">--Select District--</option');

        for (var i = 0; i < data.length; i++) {
            $('#Address_PresentAddress_District_Code').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
        }

    });
}

function onDistrictChangepresentpd(sender, id) {
    debugger;
    var cid = $(sender).val();
    if (cid == "" || cid == 0) {
        $(sender).addClass('is-invalid');
        $("#" + id + "").addClass('error-message');
        $("#" + id + "").html("This field is required");
    }
    else {
        $(sender).removeClass('is-invalid');
        $("#" + id + "").removeClass('error-message');
        $("#" + id + "").html("");
    }
}

//pin validation
function validatePin(val, id) {
    debugger;

    pin = val.value;
    var pinSixDigit = /^[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}$/;
    if (val.value == "") {
        /*$(sender).addClass('is-invalid');*/
        $("#" + id + "").addClass('error-message');
        $("#" + id + "").html("This field is required");
    }
    else {
        if (pin.match(pinSixDigit)) {
            $(id).removeClass('is-invalid');
            $("#" + id + "").removeClass('error-message');
            $("#" + id + "").html("");
            return true;
        }
        else {
            $(id).addClass('is-invalid');
            $("#" + id + "").addClass('error-message');
            $("#" + id).html("Please enter a valid PIN number");
            return false;
        }
    }
}

function RequiredVal(val, id) {

    debugger;
    stringval = val.value;
    if (val.value == "") {
        $(sender).addClass('is-invalid');
        $("#" + id + "").addClass('error-message');
        $("#" + id + "").html("This field is required");
    }
    else {
        $(sender).removeClass('is-invalid');
        $("#" + id + "").removeClass('error-message');
        $("#" + id + "").html("");
        return val.value;
    }
}

//mobile validation
function validateMobile(val, id) {
    debugger;
    mobile = $(val).val();
    if (mobile == "") {
        $(val).addClass('is-invalid');
        $("#" + id + "").addClass('error-message');
        $("#" + id + "").html("This field is required");
    }
    else {
        var filter = /^[1-9]{1}[0-9]{9}$/
        if (!filter.test(mobile)) {
            debugger; 
            $(val).addClass('is-invalid');
            $("#" + id + "").addClass('error-message');
            $("#" + id + "").html('Invalid Mobile number');
    /*        $(val).focus();*/
        }
        else {
            $(val).removeClass('is-invalid');
            $("#" + id + "").removeClass('error-message');
            $("#" + id + "").html("");
            /*trim(val);*/
            return true;
        }
    }
}

//email validation
function validateEmail(val, id) {
    email = $(val).val();
    if (email == "") {
        $(val).addClass('is-invalid');
        $("#" + id + "").addClass('error-message');
        $("#" + id + "").html("This field is required");
    }
    else {
        /*filter = /^\w+([\.-]?\w+)@\w+([\.-]?\w+)(\.\w{2,3})+$/*/
       
        filter = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;



        if (!filter.test(email)) {
            debugger;
            $(val).addClass('is-invalid');
            $("#" + id + "").addClass('error-message');
            $("#" + id + "").html('Invalid Email ID');
          
        }
        else {
            $(val).removeClass('is-invalid');
            $("#" + id + "").removeClass('error-message');
            $("#" + id + "").html("");
           /* trim(val);*/
            return true;
        }
    }
}

//Firstletter Capital validation
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
function firstLetterCapsWithOutValidation(val) {
    debugger;
    stringval = val.value;   
    val.value = val.value.toLowerCase();
    val.value = val.value.
    replace(/(^\s*)|(\s*$)/gi, ""). // removes leading and trailing spaces
    replace(/[ ]{2,}/gi, "").       // replaces multiple spaces with one space
    replace(/\n +/, "\n");
    val.value = val.value.substr(0, 1).toUpperCase() + val.value.substr(1);
    return val.value;   
}


//function calcExp() {
//    debugger;
//    var from = $('#GetPreviousEmployment_Fromdate').val();
//    var to = $('#GetPreviousEmployment_Todate').val();
//    date1 = new Date(from);  
//    date2 = new Date(to); 
//    const oneDay = 24 * 60 * 60 * 1000; // hours*minutes*seconds*milliseconds
//    const firstDate = date2;
//    const secondDate = date1;
//    const diffDays = Math.round(Math.abs((firstDate - secondDate) / oneDay));
//    debugger;
//    var diff = parseFloat(diffDays);
//    debugger;   
//    diff = diff / 365.00;
//    $("#GetPreviousEmployment_Experience").val(diff.toFixed(2));
//    debugger;
//}

function calcExp(val, id) {
    debugger;
    /*alert($(val).val());*/
    var from = $('#GetPreviousEmployment_Fromdate').val();
    var to = $('#GetPreviousEmployment_Todate').val();
    date1 = new Date(from);
    date2 = new Date(to);
    const oneDay = 24 * 60 * 60 * 1000; // hours*minutes*seconds*milliseconds
   /* alert(oneDay);*/
    const firstDate = date2;
    const secondDate = date1;
    const diffDays = Math.round(Math.abs((firstDate - secondDate) / oneDay));
   /* alert(diffDays);*/
    debugger;
    var diff = parseFloat(diffDays);
    debugger;
    diff = diff / 365;
    /*alert(diff);*/
    var monthWiseDiff = diff / 0.0833344444;
   /* alert(monthWiseDiff);*/
    monthWiseDiff = parseInt(monthWiseDiff);
    /*alert(monthWiseDiff);*/
    abc = monthWiseDiff % 12;
    monthWiseDiff = monthWiseDiff / 12;
    //alert(abc);
    //alert(monthWiseDiff);
    var month = 0.01 * abc;
    var year = parseInt(monthWiseDiff);
    //alert(year);
    //alert(month);
    var experience = year + month;
    /*alert(experience);*/
    $("#GetPreviousEmployment_Experience").val(experience);
    debugger;
    if ($(val).val() == "") {
        $(val).addClass('is-invalid');
        $("#" + id + "").addClass('error-message');
        $("#" + id + "").html("This field is required");
    }
    else {

        var ToDate = new Date($(val).val());
        if (date1 > date2) {
            $(val).addClass('is-invalid');
            $("#" + id + "").addClass('error-message');
            $("#" + id + "").html("To date should be greater than From date");
            return false;
        }
        else {
            $(val).removeClass('is-invalid');
            $("#" + id + "").removeClass('error-message');
            $("#" + id + "").html("");
            return true;
        }
    }
}


function validateDOW(val, id) {
    if (val.value == "") {
        alert(val.value);
        $(val).addClass('is-invalid');
        $("#" + id + "").addClass('error-message');
        $("#" + id + "").html("This field is required");
    }
    else {
        $(val).removeClass('is-invalid');
        $("#" + id + "").removeClass('error-message');
        $("#" + id + "").html("");
        /*trim(val);*/
    }
}

//function DOWStatus(sender,id) {
//    debugger;
//    var cid = $(sender).val();
//    if (cid == "" || cid == 0) {
//        $(sender).addClass('is-invalid');
//        $("#" + id + "").addClass('error-message');
//        $("#" + id + "").html("This field is required");
//    }
//    else {
//        $(sender).removeClass('is-invalid');
//        $("#" + id + "").removeClass('error-message');
//        $("#" + id + "").html("");
//    }
//    var maritalStatus = $('#PersonalInfo_MaritalStatus_Code').val();
//    if (maritalStatus == 2) {
//        //$('#PersonalInfo_DOW').prop('disabled', false);
//        $('#PersonalInfo_wedding').removeAttr('disabled');
//        $("#PersonalInfo_wedding").focus();
//    }
//    else {
//        $('#PersonalInfo_wedding').prop('disabled', true);
//        $("#dow").html("");
//        $('#PersonalInfo_wedding').val('');
//    }
//}

function validateDOB(val, id) {
    debugger;
    console.log(val.value);
    if (val.value == "") {
        $(val).addClass('is-invalid');
        $("#" + id + "").addClass('error-message');
        $("#" + id + "").html("This field is required");
    }
    else {
        debugger;
        var DOB = new Date(document.getElementById('GetFamilyDetails_DateOfBirth').value);
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
            $("#" + id + "").html("Age must be greater than 18");
            $(val).addClass('is-invalid');
            $("#" + id + "").addClass('error-message');
            return false;
        }
        else if (age > 58) {
            $(val).addClass('is-invalid');
            $("#" + id + "").addClass('error-message');
            if (confirm("Candidate age greater than 58. Do you want to continue?")) {
                $("#" + id + "").html("");
                return true;
            }
            else {
                val.value = "";
                $(val).removeClass('is-invalid');
                $("#" + id + "").removeClass('error-message');
                return false;
            }
        }       
            $("#" + id + "").html("");
            trim(val);
            $(val).removeClass('is-invalid');
            $("#" + id + "").removeClass('error-message');
            return true;
    }
}

function validateAllAddress() {
    debugger;
    var i = 0;
    if (!validateById('#Address_PermanentAddress_House', 'house')) {
        i++;
    }
    if (!validateById('#Address_PermanentAddress_Street', 'street')) {
        i++
    }
    if (!validateById('#Address_PermanentAddress_City', 'city')) {
        i++;
    }
    if (!validateById('#Address_PermanentAddress_PinCode', 'pin')) {
        i++;
    }
    if (!validateById('#Address_PermanentAddress_Country_Code', 'country')) {
        i++;
    }
    if (!validateById('#Address_PermanentAddress_State_Code', 'state')) {
        i++;
    }
    if (!validateById('#Address_PermanentAddress_District_Code', 'district')) {
        i++;
    }
    if (!validateById('#Address_PresentAddress_House', 'prhouse')) {
        i++;
    }
    if (!validateById('#Address_PresentAddress_Street', 'prstreet')) {
        i++;
    }
    if (!validateById('#Address_PresentAddress_City', 'prcity')) {
        i++;
    }
    if (!validatePincode(document.getElementById('Address_PresentAddress_PinCode'), 'prpin')) {
        i++;
    }
    if (!validateById('#Address_PresentAddress_Country_Code', 'prcountry')) {
        i++
    }
    if (!validateById('#Address_PresentAddress_State_Code', 'prstate')) {
        i++;
    }
    if (!validateById('#Address_PresentAddress_District_Code', 'prdistrict')) {
        i++;
    }
    if (i == 0) {
        swal.fire({
            text: "Do you want to Save?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({

                    type: 'POST',
                    url: '/DataBank/PersonalDetails/Create',
                    dataType: 'json',
                    data: $('#Address').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                $('.tabOne').removeClass('active');
                                $('#tabOne').removeClass('active');
                                $('#tabOne').css('display', 'none');
                                $('.tabContactDetails').addClass('active');
                                $('#tabContactDetails').addClass('active');
                                $('#tabContactDetails').css('display', 'block');
                                $('#addrFooter').html('');
                                $('#addrFooter').prepend('<label  class="new_approval_pend_btn">Approval Pending</label >')
                            });

                        }
                       
                        else {
                            swal.fire(result.message).then(function () {
                            });
                        }

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
        });
       
    }
    else {
        return false;
    }
 
}


function validateAllContactDetails() {
    debugger;
    var i = 0;
    if (!validateMobile('#ContactDetails_PersonalMobile', 'pmobile')) {
        $("#ContactDetails_PersonalMobile").focus();
        i++;
    }   
    if (!validateEmail('#ContactDetails_PersonalEmail', 'pemail')) {
        $("#ContactDetails_PersonalEmail").focus();
        i++;
    }
    //if (!validateMobile('#ContactDetails_OfficeMobile', 'omobile')) {
    //    $("#ContactDetails_OfficeMobile").focus();
    //   i++;
    //}
    if (!validateEmailIDWithoutMandatory(document.getElementById('ContactDetails_OfficeEmail'), 'oemail')) {
        $("#ContactDetails_OfficeEmail").focus();
        i++;
    }
    if (!validateById('#ContactDetails_EmergencyContactPerson', 'econtactperson')) {
        i++;
    }
    if (!validateMobile('#ContactDetails_EmergencyContactNumber', 'econtactnumber')) {
        $("#ContactDetails_EmergencyContactNumber").focus();
        i++;
    }
    if (!validateById('#ContactDetails_Relation_Code', 'relation')) {
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
                $.ajax({

                    type: 'POST',
                    url: '/DataBank/PersonalDetails/Create',
                    dataType: 'json',
                    data: $('#ContactDetails').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                $('.tabContactDetails').removeClass('active');
                                $('#tabContactDetails').removeClass('active');
                                $('#tabContactDetails').css('display', 'none');
                                $('.tabTwo').addClass('active');
                                $('#tabTwo').addClass('active');
                                $('#tabTwo').css('display', 'block');
                                $('#contactFooter').html('');
                                $('#contactFooter').prepend('<label  class="new_approval_pend_btn">Approval Pending</label >')
                            });

                        }
                      
                        else {
                            swal.fire(result.message).then(function () {


                            });
                        }

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
        });
       
    }
}
function showpopup() {
    $.ajax({
        type: 'GET',
        url: '/DataBank/PersonalDetails/getPopUp',
        dataType: 'html',

        success: function (Data) {
            var m = $('.bd-example-modal-lg');
            m.find('.modal-body').html('');
            m.find('.modal-body').html(Data);
            m.modal('show');
        },
        error: function (xhr, textStatus, errorThrown) {
            swal.fire(xhr.responseText);

        }
    });
}
function clearPopUp() {
    var m = $('.bd-example-modal-lg');                                                                                                                                                  
    m.find('.modal-body').html('');
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

function validateProfilePhoto()
{
    debugger
    if ($("#enrollment_PreEnrollment_BasicInfo_File_FileData").val() == null)
        swal.fire("No file Uploaded");

    else {
        if (CheckProfilePic(document.getElementById('enrollment_PreEnrollment_BasicInfo_File_FileData'))) {
        var photo = new FormData($('#per_profilephoto')[0]);
        console.log(photo);
        $.ajax({

            type: 'POST',
            url: '/DataBank/PersonalDetails/SaveProfileToFolder',
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
function validateAllPersonalInfo() {
    debugger;
    //if ($("#PersonalInfo_BloodGroup_Code").val() == "") {
    //    $("#bloodgroup").html("This field is required");
    //    $('#PersonalInfo_BloodGroup_Code').focus();
    //    return false;
    var i = 0;
    var maritalStatus = $('#PersonalInfo_MaritalStatus_Code').val();
        if (!validateById('#PersonalInfo_BloodGroup_Code', 'bloodgroup')) {
            i++;
        }
        if (!validateById('#PersonalInfo_MaritalStatus_Code', 'maritalstatus')) {
            i++;
        }
        if (maritalStatus == 2 && !validateById('#PersonalInfo_wedding', 'dow')) {
            
            i++;
        }
        if (!validateById('#PersonalInfo_ReligiousView_Code', 'religiousview')) {
            i++;
    }
    if (!validateById('#PersonalInfo_Category_Code', 'cate')) {
        i++;
    }
    if (!validateById('#PersonalInfo_Caste', 'castetext')) {
        i++;
    }
        if (!validateById('#PersonalInfo_Language_Code', 'language')) {
            i++;
            debugger;
        }
        if (!validateById('#PersonalInfo_Hobbies_Code', 'hobbies')) {
            i++;
            debugger;
        }
        if (i > 0) {
            return false;
        }
        else {
            debugger;
            swal.fire({
                text: "Do you want to Save?",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Ok'
            }).then((result) => {
                if (result.isConfirmed) {
                    $.ajax({

                        type: 'POST',
                        url: '/DataBank/PersonalDetails/Create',
                        dataType: 'json',
                        data: $('#Personalinfo').serialize(),
                        success: function (result) {
                            if (result.success == true) {
                                swal.fire(result.message).then(function () {
                                    $('.tabTwo').removeClass('active');
                                    $('#tabTwo').removeClass('active');
                                    $('#tabTwo').css('display', 'none');
                                    $('.tabThree').addClass('active');
                                    $('#tabThree').addClass('active');
                                    $('#tabThree').css('display', 'block');
                                    $('#perInfoFooter').html('');
                                    $('#perInfoFooter').prepend('<label  class="new_approval_pend_btn">Approval Pending</label >')
                                });

                            }
                         
                            else {
                                swal.fire(result.message).then(function () {


                                });
                            }

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
            });
       
    }
}
function validateAllFamilySave(mode) {
    debugger;
    var msg;
    var r = [1, 2];
    if (mode != "Modify") {
        msg = "Do you want to Save?";
    }
    else {
        msg = "Do you want to Modify?";
    }
    var i = 0;
    if (!validateById('#GetFamilyDetails_Name', 'fmlyname')) {
        i++;
    }
    if (!validateById('#GetFamilyDetails_Relation_Code', 'familyrelation')) {
        i++;
    }
    //if (!validateById('.datepkr', 'fmlydob')) {
    //    i++;
    //}
    if (!validateById('#GetFamilyDetails_Status_Code', 'status')) {
        i++;
    }
    
    for (let j = 0; j < $('#family tbody tr').length; j++) {
        if (mode != 'Modify') {
            if (r.includes(parseInt($('#GetFamilyDetails_Relation_Code').val())) && $('#FamilyDetails_' + j + '__RelationCode').val() == $('#GetFamilyDetails_Relation_Code').val()) {
                i++;
                swal.fire('Selected relation already exists');
            }
        }
        else {
            if ($('#FamilyDetails_' + j + '__FamilyLogGI').val() != $('#GetFamilyDetails_FamilyLogGI').val()) {
                if (r.includes(parseInt($('#GetFamilyDetails_Relation_Code').val())) && $('#FamilyDetails_' + j + '__RelationCode').val() == $('#GetFamilyDetails_Relation_Code').val()) {
                    i++;
                    swal.fire('Selected relation already exists');
                }
            }
            
        }
        
    }

    if (i > 0) {
        return false;
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
                $.ajax({

                    type: 'POST',
                    url: '/DataBank/PersonalDetails/familyCreate',
                    dataType: 'json',
                    data: { Name: $('#GetFamilyDetails_Name').val(), relation: $('#GetFamilyDetails_Relation_Code').val(), dob: $('.datepkr').val(), occupation: $('#GetFamilyDetails_Occupation').val(), status: $('#GetFamilyDetails_Status_Code').val(), TabIndex: "Family Details", EmpGI: $('#EmployeeGI').val(), fmlyGI: $('#GetFamilyDetails_FamilyLogGI').val() },
                    success: function (result) {
                        debugger;
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {                              
                                CloseSlider('fam');
                            
                                $.ajax({

                                    type: 'GET',
                                    url: '/DataBank/PersonalDetails/GetFamilyList',
                                    data: { EmployeeGI: $('#EmployeeGI').val()  },
                                    dataType: 'html',
                                    success: function (result) {
                                        debugger
                                        if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                                            window.location.reload();
                                        }
                                        else {

                                            $('#tabThree').html("");
                                            $('#tabThree').html(result);
                                        }


                                    },


                                    error: function (xhr, textStatus, errorThrown) {
                                        debugger
                                        document.body.id = "kt_body";
                                        $('body').html(xhr.responseText);


                                    }
                                });
                              
                            });

                        }
                   
                        else {
                            if (result.message == "FATHER_DUPLICATION_ERROR" || result.message == "MOTHER_DUPLICATION_ERROR" || result.message == "SPOUSE_DUPLICATION_ERROR") {
                                swal.fire("Selected relation already exists");
                            }
                            else {
                                swal.fire(result.message);
                            }
                        }

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
        });
       
    }
}

function validateAllEducationSave(mode) {
    debugger;
    var msg;
    if (mode != "Modify") {
        msg = "Do you want to Save?";
    }
    else {
        msg = "Do you want to Modify?";
    }
    var i = 0;
    if (!validateById('#GetEducation_QualificationType_Code','qualificationtype')) {
        i++;
    }
    if (!validateById('#GetEducation_Qualification_Code', 'qualification')) {
        i++;
    }
    if (!validateById('#GetEducation_Specialisation_Code', 'specialisation')) {
        i++;
    }
    if (!validateById('#GetEducation_YearOfCompletion_Code', 'yoc')) {
        i++;
    }
    if (i > 0) {
        return false;
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
                data = new FormData($('#eduk')[0]);
                $("#eduID").prop('disabled', true);
                $.ajax({

                    type: 'POST',
                    url: '/DataBank/PersonalDetails/Create',
                    dataType: 'json',
                    contentType: false,
                    processData: false,
                    data: data,
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                $('.tabOne').removeClass('active');
                                $('#tabOne').removeClass('active');
                                $('#tabOne').css('display', 'none');                              
                                CloseSlider('edu');              
                                $.ajax({

                                    type: 'GET',
                                    url: '/DataBank/PersonalDetails/GetEducationList',
                                    data: { EmployeeGI: $('#EmployeeGI').val() },
                                    dataType: 'html',
                                    success: function (result) {
                                        debugger
                                        if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                                            window.location.reload();
                                        }
                                        else {

                                            $('#tabFive').html("");
                                            $('#tabFive').html(result);
                                        }


                                    },


                                    error: function (xhr, textStatus, errorThrown) {
                                        debugger
                                        document.body.id = "kt_body";
                                        $('body').html(xhr.responseText);


                                    }
                                });
                          
                            });

                        }
                     
                        else {
                            swal.fire(result.message).then(function () {


                            });
                        }

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
        });
       
    }
}

function validateAllCertificationSave(mode) {

    debugger;
    var msg;
    if (mode != "Modify") {
        msg = "Do you want to Save?";
    }
    else {
        msg = "Do you want to Modify?";
    }
    var i = 0;
    if (!validateById('#GetCertifications_Certificate', 'certificate')) {
        i++;
    }
 //if (!validateById('#GetCertifications_CertificateType_Code', 'certificatetype')) {
 //    i++;
 //   }
 //    if (!validateById('#GetCertifications_IssuedBy', 'issuedby')) {
 //        i++;
 //   }
     if (!validateById('#GetCertifications_Issued', 'issueddate')) {
         i++;
    }
    if (!validateById('#GetCertifications_ExamDate', 'examdate')) {
        i++;
    }
    if (!validateById('#GetCertifications_Renewaldate', 'renewabledate')) {
        i++;
   }
    // if (!validateById('#GetCertifications_CertificateMode_Code', 'certificatemode')) {
    //     i++;
    //}
    // if (!validateById('#GetCertifications_CertificateValidity_Code', 'validity')) {
    //     i++;
    //}
   // if (!validateById('#GetCertifications_Renewaldate', 'renewabledate')) {
   //     i++;
   //}
    //if ($('#GetCertifications_CertificateValidity_Code').val() == 2 && !validateRenewalDate('#GetCertifications_Renewaldate', 'renewabledate'))
    //    i++;
    //if ($('#GetCertifications_CertificateValidity_Code').val() == 1) {
    //    i = 0;
    //    $("#renewabledate").html("");
    //    $('#GetCertifications_Renewaldate').removeClass('is-invalid');
    //    $("#renewabledate").removeClass('error-message');
    //}
    if (i == 0) {
        data = new FormData($('#CertificateSave')[0]);

        swal.fire({
            text: msg,
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({

                    type: 'POST',
                    url: '/DataBank/PersonalDetails/Create',
                    dataType: 'json',
                    contentType: false,
                    processData: false,
                    data: data,
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                CloseSlider('cer');                     
                                $.ajax({

                                    type: 'GET',
                                    url: '/DataBank/PersonalDetails/GetCertificateList',
                                    data: { EmployeeGI: $('#EmployeeGI').val() },
                                    dataType: 'html',
                                    success: function (result) {
                                        debugger
                                        if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                                            window.location.reload();
                                        }
                                        else {

                                            $('#tabSeven').html("");
                                            $('#tabSeven').html(result);
                                        }


                                    },


                                    error: function (xhr, textStatus, errorThrown) {
                                        debugger
                                        document.body.id = "kt_body";
                                        $('body').html(xhr.responseText);


                                    }
                                });

                            });

                        }

                        else {
                            swal.fire(result.message).then(function () {


                            });
                        }

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
        });
        
    }
    else
        return false;
}

function validateAllSkillSave(mode) {
    debugger;
    var msg;
    if (mode != "Modify") {
        msg = "Do you want to Save?";
    }
    else {
        msg = "Do you want to Modify?";
    }
    var i = 0;
    if (!validateById('#GetSkills_Skill_Code', 'skill')) {
        i++;
    }
    if (!validateById('#GetSkills_SkillType_Code', 'skilltype')) {
        i++;
    }
    if (!validateById('#GetSkills_SkillProficiency_Code', 'skillproficiency')) {
        i++;
    }
    if (i == 0) {
        swal.fire({
            text: msg,
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({

                    type: 'POST',
                    url: '/DataBank/PersonalDetails/Create',
                    dataType: 'json',
                    data: $('#skillSubmit').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {

                                CloseSlider('skillSliderclose');

                                $.ajax({

                                    type: 'GET',
                                    url: '/DataBank/PersonalDetails/GetSkillsList',
                                    data: { EmployeeGI: $('#EmployeeGI').val() },
                                    dataType: 'html',
                                    success: function (result) {
                                        debugger
                                        if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                                            window.location.reload();
                                        }
                                        else {

                                            $('#tabSix').html("");
                                            $('#tabSix').html(result);
                                        }


                                    },


                                    error: function (xhr, textStatus, errorThrown) {
                                        debugger
                                        document.body.id = "kt_body";
                                        $('body').html(xhr.responseText);


                                    }
                                });

                            });

                        }

                        else {
                            swal.fire(result.message).then(function () {


                            });
                        }

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
        });
        
    }
    else
        return false;
}
function validateAllEmpVsNomineeSave(mode) {
    debugger;
    var msg;
    if (mode != "Modify") {
        msg = "Do you want to Save?";
    }
    else {
        msg = "Do you want to Modify?";
    }
    var i = 0;
    if (!validateById('#GetEmployeeVsNominee_NominationCategory_Code', 'NominationCat')) {
        i++;
    }
    if (!validateById('#GetEmployeeVsNominee_Relation_Code', 'relationDD')) {
        i++;
    }
    if (!validateById('#GetEmployeeVsNominee_Name', 'NomName')) {
        i++;
    }
    if (!validateById('#GetEmployeeVsNominee_Percentage', 'Percent')) {
        i++;
    }
    if ($('#GetEmployeeVsNominee_Percentage').val() == 0) {
        swal.fire("Percentage should be greater than zero!!!");
    }
    if (!validateById('#GetEmployeeVsNominee_Comments', 'CommentsText')) {
        i++;
    }
    if (i == 0) {
        swal.fire({
            text: msg,
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                showSpinner();
                $.ajax({

                    type: 'POST',
                    url: '/DataBank/PersonalDetails/Create',
                    dataType: 'json',
                    data: $('#empVsNomineeSubmit').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            hideSpinner();
                            swal.fire(result.message).then(function () {
                                CloseSlider('EmpVsNomineeSliderclose');
                                $.ajax({
                                    type: 'GET',
                                    url: '/DataBank/PersonalDetails/GetEmpVsNomineeList',
                                    data: { EmployeeGI: $('#EmployeeGI').val() },
                                    dataType: 'html',
                                    success: function (result) {
                                        debugger
                                        if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                                            window.location.reload();
                                        }
                                        else {

                                            $('#tabNine').html("");
                                            $('#tabNine').html(result);
                                        }


                                    },


                                    error: function (xhr, textStatus, errorThrown) {
                                        debugger
                                        document.body.id = "kt_body";
                                        $('body').html(xhr.responseText);


                                    }
                                });

                            });

                        }

                        else {
                            hideSpinner();

                            swal.fire(result.message).then(function () {


                            });
                        }

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
        });
        
    }
    else
        return false;
}



function onExportToExcelClick1() {
    debugger;
    var searchid = $('#PersonalDetailsFilter_Code').val();
    /*var searchid = $('#AssetAllocationFilter_Code').val();*/

    var pendingTableLength = $('#PersonalDetailsLists_Count').val();
    if (pendingTableLength > 0) {
        url = "/DataBank/PersonalDetails/ExportToExcel?SearchId=" + searchid + "&TotRecords=" + pendingTableLength;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }


}
function validateAllPreviousEmployment(mode) {
    debugger;
    var msg;
    if (mode != "Modify") {
        msg = "Do you want to Save?";
    }
    else {
        msg = "Do you want to Modify?";
    }
    var i = 0;
    if (!validateById('#GetPreviousEmployment_CompanyName', 'prvscompany')) {
        i++;
    }
    if (!validateById('#GetPreviousEmployment_Location', 'prvslocation')) {
        i++;
    }
    if (!validateById('#GetPreviousEmployment_Designation', 'prvsdesignation')) {
        i++;
    }
    if (!validateById('#GetPreviousEmployment_Fromdate', 'fromdate')) {
        i++;
    }
   if (!calcExp('#GetPreviousEmployment_Todate', 'todate')) {
       i++;
    }
    if (!validateById('#GetPreviousEmployment_ContanctPersonName', 'cont_Name')) {
        i++;
    }
    if (!validateMobile('#GetPreviousEmployment_ContanctPersonNumber', 'cont_Mob')) {
        i++;
    }
    if (!validateById('#GetPreviousEmployment_ContanctPersonDesigantion', 'cont_Desgn')) {
        i++;
    }
    
    if(i==0) {
        debugger;
        swal.fire({
            text: msg,
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                data = new FormData($('#prev')[0]);
                $.ajax({

                    type: 'POST',
                    url: '/DataBank/PersonalDetails/Create',
                    dataType: 'json',                  
                    contentType: false,
                    processData: false,
                    data: data ,                 
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                $('.tabOne').removeClass('active');
                                $('#tabOne').removeClass('active');
                                $('#tabOne').css('display', 'none');
                                CloseSlider('pre');                          
                                $.ajax({

                                    type: 'GET',
                                    url: '/DataBank/PersonalDetails/GetPreviousEmploymentList',
                                    data: { EmployeeGI: $('#EmployeeGI').val() },
                                    dataType: 'html',                                   
                                    success: function (result) {
                                        debugger
                                        if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                                            window.location.reload();
                                        }
                                        else {
                                          
                                            $('#tabEight').html("");
                                            $('#tabEight').html(result);
                                        }


                                    },


                                    error: function (xhr, textStatus, errorThrown) {
                                        debugger
                                        document.body.id = "kt_body";
                                        $('body').html(xhr.responseText);


                                    }
                                });
                             
                            });

                        }
                      
                        else {
                            swal.fire(result.message).then(function () {


                            });
                        }

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
        });
       
    }
}

function onEducationSave(logGI) {
    debugger;  
    var EmployeeGI = $('#EmployeeGI').val(); 
   
        $.ajax({

        type: 'GET',
            url: '/DataBank/PersonalDetails/EducationSave',
        dataType: 'html',
        data: { LogGI: logGI, EmployeeGI: EmployeeGI },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('body').append('<div class="form-overlay"></div>');
                $('#educationSav').html("");
                $('#educationSav').html(result);
            }


        },
        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);


        }
    });
    
    
}
function onCertificateSave(CertificationCode) {
    debugger;

    var EmployeeGI = $('#EmployeeGI').val();
    $.ajax({

        type: 'GET',
        url: '/DataBank/PersonalDetails/CertificateSave',
        dataType: 'html',
        data: { TrnCode: CertificationCode, EmployeeGI: EmployeeGI },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('body').append('<div class="form-overlay"></div>');
                $('#certificatesave').html("");
                $('#certificatesave').html(result);
            }


        },


        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);


        }
    });
    $('#certificatesave').removeClass('hide');  
    $('.cer').show();
}
function onEmpVsNomineeSave(NomCatCode,RelationCode) {
    debugger;

    var EmployeeGI = $('#EmployeeGI').val();
    $.ajax({

        type: 'GET',
        url: '/DataBank/PersonalDetails/EmpVsNomineeSave',
        dataType: 'html',
        data: { NomCatCode: NomCatCode, RelationCode: RelationCode, EmployeeGI: EmployeeGI },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('body').append('<div class="form-overlay"></div>');
                $('#empNomineesave').html("");
                $('#empNomineesave').html(result);
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
        }
    });
    $('#empNomineesave').removeClass('hide');
    $('.EmpVsNomineeSliderclose').show();
}

function SkillAddOrEditClick(logGI) {
    debugger;
    var EmployeeGI = $('#EmployeeGI').val();
    $.ajax({

        type: 'GET',
        url: '/DataBank/PersonalDetails/GetSkills',
        dataType: 'html',
        data: { LogGI: logGI, EmployeeGI: EmployeeGI },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('body').append('<div class="form-overlay"></div>');
                $('#SkillsDiv').html("");
                $('#SkillsDiv').html(result);
            }


        },


        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);


        }
    });
    $('#SkillsDiv').removeClass('hide');
   
}




function onFamilySave(logGI) {
    debugger;
    var EmployeeGI = $('#EmployeeGI').val();
    
    
    $.ajax({

        type: 'GET',
        url: '/DataBank/PersonalDetails/FamilySave',
        dataType: 'html',
        data: { LogGI: logGI, EmployeeGI: EmployeeGI },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('body').append('<div class="form-overlay"></div>');
                $('#familySav').html("");
                $('#familySav').html(result);
            }


        },


        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);


        }
    });
    debugger;
    
}

function onPrevEmploymentSave(logGI) {
    debugger;
    var EmployeeGI = $('#EmployeeGI').val();
    $.ajax({

        type: 'GET',
        url: '/DataBank/PersonalDetails/PrevEmploymentSave',
        dataType: 'html',
        data: { LogGI: logGI, EmployeeGI: EmployeeGI },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('body').append('<div class="form-overlay"></div>');
                $('#previousSav').html("");
                $('#previousSav').html(result);
            }


        },


        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);


        }
    });
}


function onEducationDelete(GI) {
    debugger;
    swal.fire({
        text:  "Do you want to delete?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
    //    // Save it
    
    $.ajax({

        type: 'POST',
        url: '/DataBank/PersonalDetails/EducationDelete',
        dataType: 'json',
        data: { LogGI: GI },
        success: function (result) {
            if (result.success == true) {
                swal.fire(result.message).then(function () {
                    $('.tabOne').removeClass('active');
                    $('#tabOne').removeClass('active');
                    $('#tabOne').css('display', 'none');                   
                    $.ajax({

                        type: 'GET',
                        url: '/DataBank/PersonalDetails/GetEducationList',
                        data: { EmployeeGI: $('#EmployeeGI').val() },
                        dataType: 'html',
                        success: function (result) {
                            debugger
                            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                                window.location.reload();
                            }
                            else {
                                $('#tabFive').html("");
                                $('#tabFive').html(result);
                            }
                        },
                        error: function (xhr, textStatus, errorThrown) {
                            debugger
                            document.body.id = "kt_body";
                            $('body').html(xhr.responseText);


                        }
                    });
                   
                });

            }
         
            else {
                swal.fire("Something went wrong.").then(function () {


                });
            }

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
        else {
            return false;
        }
    })
}

function onCertificateDelete(CertificationCode) {
    debugger;
    swal.fire({
        text: "Do you want to delete?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            //    // Save it

            $.ajax({

                type: 'POST',
                url: '/DataBank/PersonalDetails/CertificateDelete',
                dataType: 'json',
                data: { TrnCode: CertificationCode },
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {                         
               
                            $.ajax({

                                type: 'GET',
                                url: '/DataBank/PersonalDetails/GetCertificateList',
                                data: { EmployeeGI: $('#EmployeeGI').val() },
                                dataType: 'html',
                                success: function (result) {
                                    debugger
                                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                                        window.location.reload();
                                    }
                                    else {
                                        $('#tabSeven').html("");
                                        $('#tabSeven').html(result);
                                    }
                                },
                                error: function (xhr, textStatus, errorThrown) {
                                    debugger
                                    document.body.id = "kt_body";
                                    $('body').html(xhr.responseText);


                                }
                            });
                         
                        });

                    }                 
                    else {
                        swal.fire("Something went wrong.").then(function () {


                        });
                    }

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
        else {
            return false;
        }
    })
}
function onEmpVsNomineeDelete(NomcatCode,relationCode) {
    debugger;
    swal.fire({
        text: "Do you want to delete?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({

                type: 'POST',
                url: '/DataBank/PersonalDetails/EmpVsNomineeDelete',
                dataType: 'json',
                data: { NomcatCode: NomcatCode, RelationCode: relationCode, EmployeeGI: $('#EmployeeGI').val()},
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {                         
               
                            $.ajax({

                                type: 'GET',
                                url: '/DataBank/PersonalDetails/GetEmpVsNomineeList',
                                data: { EmployeeGI: $('#EmployeeGI').val() },
                                dataType: 'html',
                                success: function (result) {
                                    debugger
                                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                                        window.location.reload();
                                    }
                                    else {
                                        $('#tabNine').html("");
                                        $('#tabNine').html(result);
                                    }
                                },
                                error: function (xhr, textStatus, errorThrown) {
                                    debugger
                                    document.body.id = "kt_body";
                                    $('body').html(xhr.responseText);


                                }
                            });
                         
                        });

                    }                 
                    else {
                        swal.fire("Something went wrong.").then(function () {


                        });
                    }

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
        else {
            return false;
        }
    })
}
function onFamilyDelete(GI) {
    
    debugger;
    swal.fire({
        text: "Do you want to delete?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            //    // Save it

            $.ajax({

                type: 'POST',
                url: '/DataBank/PersonalDetails/FamilyDelete',
                dataType: 'json',
                data: { LogGI: GI },
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            $('.tabOne').removeClass('active');
                            $('#tabOne').removeClass('active');
                            $('#tabOne').css('display', 'none');                        
                            $.ajax({

                                type: 'GET',
                                url: '/DataBank/PersonalDetails/GetFamilyList',
                                data: { EmployeeGI: $('#EmployeeGI').val()},
                                dataType: 'html',
                                success: function (result) {
                                    debugger
                                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                                        window.location.reload();
                                    }
                                    else {
                                        $('#tabThree').html("");
                                        $('#tabThree').html(result);
                                    }
                                },
                                error: function (xhr, textStatus, errorThrown) {
                                    debugger
                                    document.body.id = "kt_body";
                                    $('body').html(xhr.responseText);


                                }
                            });
                          
                        });
                    }
                
                    else {
                        swal.fire("Something went wrong.").then(function () {


                        });
                    }

                },
                error: function (xhr, textStatus, errorThrown) {
                    document.body.id = "kt_body";
                    $('body').html(xhr.responseText);
                }
            });
        }
        else {
            return false;
        }
    })
}

function SkillDeleteClick(GI) {

    debugger;
    swal.fire({
        text: "Do you want to delete?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            //    // Save it

            $.ajax({

                type: 'POST',
                url: '/DataBank/PersonalDetails/SkillDelete',
                dataType: 'json',
                data: { LogGI: GI },
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {                        
                       
                            $.ajax({

                                type: 'GET',
                                url: '/DataBank/PersonalDetails/GetSkillsList',
                                data: { EmployeeGI: $('#EmployeeGI').val() },
                                dataType: 'html',
                                success: function (result) {
                                    debugger
                                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                                        window.location.reload();
                                    }
                                    else {
                                        $('#tabSix').html("");
                                        $('#tabSix').html(result);
                                    }
                                },
                                error: function (xhr, textStatus, errorThrown) {
                                    debugger
                                    document.body.id = "kt_body";
                                    $('body').html(xhr.responseText);


                                }
                            });
                        });

                    }
                  
                    else {
                        swal.fire("Something went wrong.").then(function () {


                        });
                    }

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
        else {
            return false;
        }
    })
}

function onPrevEmploymentDelete(logGI) {
   
    debugger;
    swal.fire({
        text: "Do you want to delete?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            //    // Save it

            $.ajax({

                type: 'POST',
                url: '/DataBank/PersonalDetails/PrevEmploymentDelete',
                dataType: 'json',
                data: { LogGI: logGI },
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            $('.tabOne').removeClass('active');
                            $('#tabOne').removeClass('active');
                            $('#tabOne').css('display', 'none');                    
                            $.ajax({

                                type: 'GET',
                                url: '/DataBank/PersonalDetails/GetPreviousEmploymentList',
                                data: { EmployeeGI: $('#EmployeeGI').val() },
                                dataType: 'html',
                                success: function (result) {
                                    debugger
                                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                                        window.location.reload();
                                    }
                                    else {
                                        $('#tabEight').html("");
                                        $('#tabEight').html(result);
                                    }
                                },
                                error: function (xhr, textStatus, errorThrown) {
                                    debugger
                                    document.body.id = "kt_body";
                                    $('body').html(xhr.responseText);


                                }
                            });
                          
                        });

                    }
                
                    else {
                        swal.fire("Something went wrong.").then(function () {


                        });
                    }

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
        else {
            return false;
        }
    })
}

function CloseSlider(id) {
    debugger;
    $("." + id).removeClass('slider-forms-open');
    $(".form-overlay").remove();
}

function validateRenewalDate(val, id) {
    debugger;
    if ($('#GetCertifications_CertificateValidity_Code').val() == 2) {
        if ($(val).val() == "") {
            $(val).addClass('is-invalid');
            $("#" + id + "").addClass('error-message');
            $("#" + id + "").html("This field is required");
        }
        else {

            var issueDate = new Date($('#GetCertifications_Issued').val());
            var renewalDate = new Date($(val).val());
            if (issueDate > renewalDate) {
                $(val).addClass('is-invalid');
                $("#" + id + "").addClass('error-message');
                $("#" + id + "").html("Renewal Date should be greater than issued date");
                return false;
            }
            else {
                $(val).removeClass('is-invalid');
                $("#" + id + "").removeClass('error-message');
                $("#" + id + "").html("");
                return true;
            }
        }
    }
    else {
        $(val).removeClass('is-invalid');
        $("#" + id + "").removeClass('error-message');
        $("#" + id + "").html("");
        return true;
    }
}

function DOWStatus(sender, id) {
    debugger;
    var cid = $(sender).val();
    if (cid == "" || cid == 0) {
        $(sender).addClass('is-invalid');
        $("#" + id + "").addClass('error-message');
        $("#" + id + "").html("This field is required");
    }
    else {
        $(sender).removeClass('is-invalid');
        $("#" + id + "").removeClass('error-message');
        $("#" + id + "").html("");
    }
    if (cid != 2) {
        $('#weddingDte').addClass('hide');
        $('#PersonalInfo_wedding').val('');
    }
    else {
        $('#weddingDte').removeClass('hide');
    }
}

function onLifetimeChange(sender, id) {
    debugger;
    var validity = $('#GetCertifications_CertificateValidity_Code').val();
    if (validity == 2) {
        //$('#PersonalInfo_DOW').prop('disabled', false);
        $(sender).removeClass('is-invalid');
        $("#" + id + "").removeClass('error-message');
        $("#" + id + "").html("");
        /*$('#GetCertifications_Renewaldate').prop('disabled', false);*/
        $("#renewalID").removeClass('hide');
        $('#GetCertifications_Renewaldate').val('');
    }
    else if (validity == 1) {
        //$('#GetCertifications_Renewaldate').prop('disabled', true);
        $("#renewalID").addClass('hide');
        $('#GetCertifications_Renewaldate').val('');
        $("#renewabledate").html("");
        $('#GetCertifications_Renewaldate').removeClass('is-invalid');
        $("#" + id + "").removeClass('error-message');
        $("#" + id + "").html("");
        $(sender).removeClass('is-invalid');
    }
    else {
        $(sender).addClass('is-invalid');
        $("#" + id + "").addClass('error-message');
        $("#" + id + "").html("This field is required");
    }
}



function ApplyFilter(mode) {
    debugger
    //var search = $('#Item2_PersonalDetailsFilter_Code').val();
    //$("#SearchId").val($('#Item2_PersonalDetailsFilter_Code').val())
    if (mode == 'apply') {
        if (filterApplicable >= 1) {
            
            $('#filterForm').removeClass('filter-apply');
            fillData();
            CloseSlider('basicFilter');
        }
        else {
            $('#filterForm').addClass('filter-apply');
            fillData();
            CloseSlider('basicFilter');
        }

    }
    else {
        filterApplicable = 1;
        $('.reset').val('');
        $('#Optional_Filter_EmploymentStatus_Code').val(0);
        $('#filterForm').removeClass('filter-apply');
        fillData();
        CloseSlider('basicFilter');
    }
}

function fillData() {

    debugger;
    //$('.ActiveCount').html("@Model.ActiveCount");
    //$('.ResignedCount').html("@Model.ResignedCount");

    var filter1 = $('#BasicFilter').serialize();
    var dropval;
    var fltrdCount = 0;
    fltrdCount = $('.filterDropDown').val();
    console.log(fltrdCount);
    var temp = filter1;





    var dataTable = $("#PersonalDetailsTable").DataTable({
        //"paging": true,
        "pagingType": 'full_numbers',
        "destroy": 'true',

        //"fnPreDrawCallback": function (oSettings) {
        //    // if ($('#test').val() == 1) {
        //    return false;
        //    // }
        //},



        "ajax": {
            "url": "/Databank/PersonalDetails/GetList",
            "type": "POST",


            "datatype": "json",
           


            'data':

                function (d) {
                    temp = '';

                    var current = $('#PersonalDetailsTable').DataTable();


                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();

                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&filterCount=' + fltrdCount+ '&dummy=dummy';
                    d.filter1 = temp;
                    d.filterCount = fltrdCount;
                    // alert(d.filter);

                    return JSON.stringify(d);
                },




            "dataSrc": function (d) {

                //if (d.data.length != 0) {
                //    $('.ActiveCount').html(d.data[0].active);
                //    $('.ResignedCount').html(d.data[0].resigned);
                //}
                //else {

                    // alert(d.data[0])

                    //  $('.ActiveCount').html(d.recordsTotal);
                //}

               
                $('#PersonalDetailsLists_Count').val(d.recordsTotal);

                for (var i = 0, ien = d.data.length; i < ien; i++) {

                    // alert(JSON.stringify(d));

                    d.data[i].employeeName = '<a href="/DataBank/PersonalDetails/Create?EmployeeGI=' + d.data[i].employeeGI + '&Breadcrumb=PersonalDetails&BreadcrumbName=Employee Profile List">' + d.data[i].employeeName + '</a>'
                    console.log(d.data);
                    d.data[i].doj = _GetFormattedDate(d.data[i].doj);
                    if (d.data[i].photo != true) {
                        d.data[i].photo = '<button type="button"class="btn btn-icon btn-close border-radius"> <i class="icon-xl fas fa-times"></i> </button>'
                    }
                    else {
                        d.data[i].photo = '<button type="button" class="btn btn-icon "><i class="icon-xl fas fa-check btn-tick2"></i></button>'
                    }
                    if (d.data[i].addr != true) {
                        d.data[i].addr = '<button type="button"class="btn btn-icon btn-close border-radius"> <i class="icon-xl fas fa-times"></i> </button>'
                    }
                    else {
                        d.data[i].addr = '<button type="button" class="btn btn-icon "><i class="icon-xl fas fa-check btn-tick2"></i></button>'
                    }
                    if (d.data[i].cont != true) {
                        d.data[i].cont = '<button type="button"class="btn btn-icon btn-close border-radius"> <i class="icon-xl fas fa-times"></i> </button>'
                    }
                    else {
                        d.data[i].cont = '<button type="button" class="btn btn-icon "><i class="icon-xl fas fa-check btn-tick2"></i></button>'
                    }
                    if (d.data[i].pers != true) {
                        d.data[i].pers = '<button type="button"class="btn btn-icon btn-close border-radius"> <i class="icon-xl fas fa-times"></i> </button>'
                    }
                    else {
                        d.data[i].pers = '<button type="button" class="btn btn-icon "><i class="icon-xl fas fa-check btn-tick2"></i></button>'
                    }
                    if (d.data[i].fmly != true) {
                        d.data[i].fmly = '<button type="button"class="btn btn-icon btn-close border-radius"> <i class="icon-xl fas fa-times"></i> </button>'
                    }
                    else {
                        d.data[i].fmly = '<button type="button" class="btn btn-icon "><i class="icon-xl fas fa-check btn-tick2"></i></button>'
                    }
                    if (d.data[i].educ != true) {
                        d.data[i].educ = '<button type="button"class="btn btn-icon btn-close border-radius"> <i class="icon-xl fas fa-times"></i> </button>'
                    }
                    else {
                        d.data[i].educ = '<button type="button" class="btn btn-icon "><i class="icon-xl fas fa-check btn-tick2"></i></button>'
                    }
                    if (d.data[i].skills != true) {
                        d.data[i].skills = '<button type="button"class="btn btn-icon btn-close border-radius"> <i class="icon-xl fas fa-times"></i> </button>'
                    }
                    else {
                        d.data[i].skills = '<button type="button" class="btn btn-icon "><i class="icon-xl fas fa-check btn-tick2"></i></button>'
                    }
                    if (d.data[i].cert != true) {
                        d.data[i].cert = '<button type="button"class="btn btn-icon btn-close border-radius"> <i class="icon-xl fas fa-times"></i> </button>'
                    }
                    else {
                        d.data[i].cert = '<button type="button" class="btn btn-icon "><i class="icon-xl fas fa-check btn-tick2"></i></button>'
                    }
                    if (d.data[i].prev != true) {
                        d.data[i].prev = '<button type="button"class="btn btn-icon btn-close border-radius"> <i class="icon-xl fas fa-times"></i> </button>'
                    }
                    else {
                        d.data[i].prev = '<button type="button" class="btn btn-icon "><i class="icon-xl fas fa-check btn-tick2"></i></button>'
                    }
                    if (d.data[i].employeeVsNominee != true) {
                        d.data[i].employeeVsNominee = '<button type="button"class="btn btn-icon btn-close border-radius"> <i class="icon-xl fas fa-times"></i> </button>'
                    }
                    else {
                        d.data[i].employeeVsNominee = '<button type="button" class="btn btn-icon "><i class="icon-xl fas fa-check btn-tick2"></i></button>'
                    }
                   
                    var percent = d.data[i].percentage + '%';
                    if (d.data[i].percentage > 75) {
                        d.data[i].percentage = '<div data-bs-toggle="tooltip" class="profile-matching-line" data-bs-original-title="' + d.data[i].percentage +'"><div class="progress-bar"><div class="high" style="width:' + percent + '"><span>' + percent + '</span></div></div></div>'
                    }
                    else if (d.data[i].percentage >= 50) {
                        d.data[i].percentage = '<div data-bs-toggle="tooltip" class="profile-matching-line" data-bs-original-title="' + d.data[i].percentage + '"><div class="progress-bar"><div class="medium" style="width:' + percent + '"><span>' + percent + '</span></div></div></div>'
                    }
                    else if (d.data[i].percentage < 50) {
                        d.data[i].percentage = '<div data-bs-toggle="tooltip" class="profile-matching-line" data-bs-original-title="' + d.data[i].percentage + '"><div class="progress-bar"><div class="low" style="width:' + percent + '"><span>' + percent + '</span></div></div></div>'
                    }
                    
                    
                }
                debugger;
                var averagePerc = "0%";
                if (d.recordsTotal != 0) {
                    averagePerc = d.data[0].average + '%';
                    $('#pers').html(averagePerc);
                    if (d.data[0].average > 75) {
                        $('#pers').css('background-color', 'green');
                        $('#pers').css('width', averagePerc);
                    }
                    else if (d.data[0].average >= 50 && d.data[0].average < 75) {
                        $('#pers').css('background-color', 'blue');
                        $('#pers').css('width', averagePerc);
                    }
                    else if (d.data[0].average >= 25 && d.data[0].average < 50) {
                        $('#pers').css('background-color', 'orange');
                        $('#pers').css('width', averagePerc);
                    }
                    else if (d.data[0].average == 0) {
                        $('#pers').css('background-color', '#e8f1ff');
                        $('#pers').css('width', "100%");
                        $('#pers').css('color', "black");
                    }
                    else {
                        $('#pers').css('background-color', 'red');
                        $('#pers').css('width', averagePerc);
                    }
                }
               

                console.log(d);
               /* console.log(sum);*/
                return d.data;
            }

        },


        "columns": [

            { "data": "employeeid","width":"10%" },
            { "data": "employeeName","width": "10%" },
            { "data": "designation","width": "10%" },
            { "data": "department","width": "10%" },
            { "data": "location","width": "8%" },
            { "data": "doj","width": "8%" },
            { "data": "photo","width": "4%" },
            { "data": "addr","width": "4%" },
            { "data": "cont","width": "4%" },
            { "data": "pers","width": "4%" },
            { "data": "fmly","width": "4%" },
            { "data": "educ","width": "4%" },
            { "data": "skills","width": "4%" },
            { "data": "cert","width": "4%" },
            { "data": "prev","width": "4%" },
            { "data": "employeeVsNominee","width": "4%" },
            { "data": "percentage","width": "8%" },


        ],


        "dom": 'rt<"bottom"><"my-wrapper"<"my-inner2"l><"my-inner1"i><"my-inner3"p>>',

        "serverSide": "true",
        "scrollY": '52vh',
        "sScrollX": "100%",
        "sScrollXInner": "100%",

        "autoWidth": false,

        "scrollCollapse": false,

        "order": [0, "asc"],
        "processing": "true",
       
        language: {
            processing: '<img width="100%" src="/images/Loader_img1 (7).gif">',
            emptyTable: '<div id="noDataFound" style="text-align: center;display: flex;align-items: center;justify-content: center;height: 45vh;"><img src="/images/nodata.png" /></div >',

            zeroRecords: '<div id="noDataFound" style="text-align: center;display: flex;align-items: center;justify-content: center;height: 45vh;"><img src="/images/nodata.png" /></div >',

            sLengthMenu: "_MENU_",
        
            paginate: {


                next: '<img width="100%" src="/images/icons/Icon awesome-arrow-right-1.png" >',
                previous: '<img src="/images/icons/Icon awesome-arrow-right.png">',
                first: '<img src="/images/icons/Icon metro-next-1.png">',
                last: '<img src="/images/icons/Icon metro-next.png">'


            }
        }



    });



    $('.ihits-table table').removeClass("dataTable no-footer");


    $('.paginate_button.next').attr("data-bs-toggle", "tooltip");
    $('.paginate_button.next').attr("title", "Next");
    $('.paginate_button.previous').attr("data-bs-toggle", "tooltip");
    $('.paginate_button.previous').attr("title", "Previous");
    $('.paginate_button.first').attr("data-bs-toggle", "tooltip");
    $('.paginate_button.first').attr("title", "First");
    $('.paginate_button.last').attr("data-bs-toggle", "tooltip");
    $('.paginate_button.last').attr("title", "Last");


    $("#dataTableSearch").on("keyup search paste cut", function () {
        dataTable.search(this.value).draw();
        $('#noDataFound').css("display", 'none');
    });

    $('.search-slider-inner').click(function () {
       
        $('#dataTableSearch').val('');
        $('#dataTableSearch').focus();
        dataTable.search('').draw();
    });

    dataTable.on('draw', function () {
        $('.paginate_button.next').attr("data-bs-toggle", "tooltip");
        $('.paginate_button.next').attr("title", "Next");
        $('.paginate_button.previous').attr("data-bs-toggle", "tooltip");
        $('.paginate_button.previous').attr("title", "Previous");
        $('.paginate_button.first').attr("data-bs-toggle", "tooltip");
        $('.paginate_button.first').attr("title", "First");
        $('.paginate_button.last').attr("data-bs-toggle", "tooltip");
        $('.paginate_button.last').attr("title", "Last");

    });

    $.fn.DataTable.ext.pager.numbers_length = 5;


}

function onExecute(e) {
    if (e.command =="SaveImageEditorCommand")
    alert("Hello");
}




//function validateAllEmployeeVsNominee() {
//    debugger;
//    var i = 0;
//    if (!validateById('#EmployeeVsNominee_Name', 'EVNname')) {
//        i++;
//    }
//    if (!validateById('#EmployeeVsNominee_Relation_Code', 'EVNrelation')) {
//        i++;
//    }
//    if (!validateMobile('#EmployeeVsNominee_Mobile', 'EVNmobile')) {        
//        i++;
//    }        
//    if (!validateAadhar('EmployeeVsNominee_Aadhar', 'EVNaadhar')) {
//        i++;
//    }

//    if (i > 0) {
//        return false;
//    }
//    else {
//        swal.fire({
//            text: "Do you want to Save?",
//            icon: 'warning',
//            showCancelButton: true,
//            confirmButtonColor: '#3085d6',
//            cancelButtonColor: '#d33',
//            confirmButtonText: 'Ok'
//        }).then((result) => {
//            if (result.isConfirmed) {
//                $.ajax({

//                    type: 'POST',
//                    url: '/DataBank/PersonalDetails/Create',
//                    dataType: 'json',
//                    data: $('#EmployeeVsNominee').serialize(),
//                    success: function (result) {
//                        if (result.success == true) {
//                            swal.fire(result.message).then(function () {                                

//                            });
//                            $('#empVsNomFooter').html();
//                            $('#empVsNomFooter').prepend('<label  class="new_approval_pend_btn">Approval Pending</label >')
//                        }

//                        else {
//                            swal.fire(result.message).then(function () {


//                            });
//                        }

//                    },
//                    error: function (xhr, textStatus, errorThrown) {
//                        if ($($.parseHTML(xhr.responseText)).filter(".Login_Session").length > 0) {
//                            window.location.reload();
//                        }
//                        else {
//                            document.body.id = "kt_body";
//                            $('body').html(xhr.responseText);
//                        }
//                    }
//                });
//            }
//        });

//    }
//}

//------------------CERTIFICTE DOCUMENT VIEW-------------------
function GetCertificateDocPopUp(LogCode) {

    $.ajax({
        type: 'GET',
        url: "/DataBank/PersonalDetails/GetPopUpforCertiDocView",
        dataType: 'html',
        data: { LogCode: LogCode},
        success: function (Data) {

            var m = $('.bd-Certificte-modal-lg');
            m.find('.modal-body').html('');
            m.find('.modal-body').html(Data);
            m.modal('show');
            $('#popUpName').text('Uploaded Documents for Certifictions');


        },
        error: function (xhr, textStatus, errorThrown) {
            alert(xhr.responseText)
            toastr.error(xhr.responseText);
        }
    });
}



function ViewDocument(Code, DocType) {

    var url = "/DataBank/PersonalDetails/ViewDocument?Code=" + Code + "&DocType=" + DocType;
    window.open(url, "_blank");
}

function PreviewCertiDoc(filename,DocCode) {
    debugger;
    
    const fileInput = $('#GetCertifications_File_FileData').val(); // file input
  
    const ActualFileName = filename;
    const FileName = $('#GetCertifications_File_FileName').val();
    //BEFORE SAVE (file selected but not saved yet)
    if (fileInput != "") {
        var path = URL.createObjectURL($('.PreviewImage')[0].files[0]);
        window.open(path, "_blank");
        return;
    }

    //AFTER SAVE (already uploaded file)
    if (ActualFileName  !== "" && fileInput == '') {
        ViewDocument(DocCode, 'Certi')
        return;
    }

    // NO FILE
    Swal.fire("No file uploaded");
}
function decimalValidation(event) {
    debugger;

    const input = event.target.value;
    const parts = input.split('.');
    if (parts.length === 2) {
        if (parts[1].length > 0 && !['0', '5'].includes(parts[1])) {
            event.target.value = parts[0] + '.' + parts[1].slice(0, -1);
            swal.fire("only 0 and 5 are allowed after decimal");
            return false;
        }
        return true;
    }
}