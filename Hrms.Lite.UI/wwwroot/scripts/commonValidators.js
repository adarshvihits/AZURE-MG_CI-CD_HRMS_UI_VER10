var maxchars = 250;
var focus = 0;
const mask = (cc, num = 4, mask = '*') =>
    ('' + cc).slice(0, -num).replace(/./g, mask) + ('' + cc).slice(-num);
function onlyNumberKey(evt) {

    var ASCIICode = (evt.which) ? evt.which : evt.keyCode
    if (ASCIICode > 31 && (ASCIICode < 48 || ASCIICode > 57)) {
        swal.fire("Only Numbers are allowed")
        return false;
    }
    return true;
}

function onlyAmount(evt) {
 
    var ASCIICode = (evt.which) ? evt.which : evt.keyCode
    if (ASCIICode > 31 && (ASCIICode < 48 || ASCIICode > 57) && ASCIICode != 46) {
        swal.fire("Only Numbers are allowed")
        return false;
    }
    else {
        
        return true;
    }
}

function onlyLettersKey(evt) {

    var ASCIICode = (evt.which) ? evt.which : evt.keyCode
    if ((ASCIICode > 64 && ASCIICode < 91) || (ASCIICode > 96 && ASCIICode < 123) || ASCIICode == 32)
        return true;
    swal.fire("Only Alphabets are allowed")
    return false;
}

function onlyLetters_Space_DotKeys(evt) {

    var ASCIICode = (evt.which) ? evt.which : evt.keyCode
    if ((ASCIICode > 64 && ASCIICode < 91) || (ASCIICode > 96 && ASCIICode < 123) || ASCIICode == 32 || ASCIICode == 46  )
        return true;
    swal.fire("Only Alphabets are allowed")
    return false;
}


function validate(val, id) {
   
    if (val.value == "") {
        $(val).addClass('is-invalid');
        $("#" + id + "").addClass('error-message');
        $("#" + id + "").html("This field is required");
    }
    else {
        $(val).removeClass('is-invalid');
        $("#" + id + "").removeClass('error-message');
        $("#" + id + "").html("");

        /* val.value = val.value.replace(/(^\s*)|(\s*$)/gi, "");*/
        val.value = val.value.replace(/(^\s*)|(\s*$)/gi, "");

        return val.value;
    }
}


function validateMultiselect(val, id,divId) {
    debugger;
    if ($('#' + val).val().length == 0) {
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

function CheckProfileExtensionSize(e) {
   
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
    
}
function validateResumeDB(val, id, index) {
   
    if (val.value == "") {
        $(val).addClass('is-invalid');
        $("#" + id + index + "").addClass('error-message');
        $("#" + id + index + "").html("This field is required");
    }
    else {
        $(val).removeClass('is-invalid');
        $("#" + id + index + "").removeClass('error-message');
        $("#" + id + index + "").html("");

        /* val.value = val.value.replace(/(^\s*)|(\s*$)/gi, "");*/
        val.value = val.value.replace(/(^\s*)|(\s*$)/gi, "");
        return val.value;
    }
}


function validateById(elementId, spanId) {
                            
    if ($(elementId).val() == "") {
        $(elementId).addClass('is-invalid');
        $("#" + spanId + "").addClass('error-message');
        $("#" + spanId + "").html("This field is required");
        return false;
    }
    else {
        $(elementId).removeClass('is-invalid');
        $("#" + spanId + "").removeClass('error-message');
        $("#" + spanId + "").html("");
        //trim(val);        
        return true;
    }
}
function validateByIdForSearchDropdn(elementId, spanId,div) {
               debugger             
    if ($(elementId).val() == "") {
        $(div).addClass('is-invalid');
        $("#" + spanId + "").addClass('error-message');
        $("#" + spanId + "").html("This field is required");
        return false;
    }
    else {
        $(div).removeClass('is-invalid');
        $("#" + spanId + "").removeClass('error-message');
        $("#" + spanId + "").html("");
        //trim(val);        
        return true;
    }
}
function capitalizeFirstLetter(val, id) {

   

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
        //val.value = val.value.toLowerCase();
        val.value = val.value.
            replace(/(^\s*)|(\s*$)/gi, ""). // removes leading and trailing spaces
            replace(/[ ]{2,}/gi, " ").       // replaces multiple spaces with one space 
            replace(/\n +/, "\n");      
        //val.value = val.value.substr(0, 1).toUpperCase() + val.value.substr(1);

        var splitStr = val.value.toLowerCase().split(' ');
        for (var i = 0; i < splitStr.length; i++) {
            // You do not need to check if i is larger than splitStr length, as your for does that for you
            // Assign it back to the array
            splitStr[i] = splitStr[i].charAt(0).toUpperCase() + splitStr[i].substring(1);
        }
        // Directly return the joined string
        val.value = splitStr.join(' '); 

        return val.value;
    }
}
function capitalizeFirstLetter(val) {

   
    stringval = val.value;
    if (val.value == "") {
        
    }
    else {
        
        /* val.value = val.value.toLowerCase();*/
        val.value = val.value.
            replace(/(^\s*)|(\s*$)/gi, ""). // removes leading and trailing spaces
            replace(/[ ]{2,}/gi, " ").       // replaces multiple spaces with one space 
            replace(/\n +/, "\n");
        var splitStr = val.value.split(' ');
        for (var i = 0; i < splitStr.length; i++) {
            // You do not need to check if i is larger than splitStr length, as your for does that for you
            // Assign it back to the array
            splitStr[i] = splitStr[i].charAt(0).toUpperCase() + splitStr[i].substring(1);
        }
        // Directly return the joined string
        val.value = splitStr.join(' ');

        return val.value;
    }
}
function OnlycapitalizeFirstLetter(val, id) {

    
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
        /* val.value = val.value.toLowerCase();*/
        val.value = val.value.
            replace(/(^\s*)|(\s*$)/gi, ""). // removes leading and trailing spaces
            replace(/[ ]{2,}/gi, " ").       // replaces multiple spaces with one space 
            replace(/\n +/, "\n");
        var splitStr = val.value.split(' ');
        for (var i = 0; i < splitStr.length; i++) {
            // You do not need to check if i is larger than splitStr length, as your for does that for you
            // Assign it back to the array
            splitStr[i] = splitStr[i].charAt(0).toUpperCase() + splitStr[i].substring(1);
        }
        // Directly return the joined string
        val.value = splitStr.join(' ');

        return val.value;
    }
}
function RemarksLimitCheck(Id) {
   
    var tlength = $('#' + Id).val().length;
    if (tlength > maxchars) {
        swal.fire("You have entered more than 250 characters");
        return false;
    }
    else {
        return true;
    }
}

function RestrictCharacters(input) {
    
    let value = input.value;
    let numbers = value.replace(/[^0-9]/g, "");
    input.value = numbers;
}

function validateMobileNumber(val, id) {
   
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

function validateFifteenDigitMobileNumber(val, id) {
   
    mobile = $(val).val();
    if (mobile == "") {
        $(val).addClass('is-invalid');
        $("#" + id + "").addClass('error-message');
        $("#" + id + "").html("This field is required");
    }
    else {
        var filter = /^[1-9][0-9]{9,14}$/
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
function validatePan(id, spanid) {
   
    var val = $("#" + id + "").val();
    var pan = val.toUpperCase();
    if (val == "") {
        $("#" + spanid + "").addClass('error-message');
        $("#" + id + "").addClass('is-invalid');
        $("#" + spanid + "").html("This field is required");
    }
    else {
        var panPat = /^([a-zA-Z]{5})(\d{4})([a-zA-Z]{1})$/;
        var code = /([C,P,H,F,A,T,B,L,J,G])/;
        var code_chk = pan.substring(3, 4);
        if (pan.search(panPat) == -1) {
            $("#" + spanid + "").addClass('error-message');
            $("#" + id + "").addClass('is-invalid');
            $("#" + spanid).html("Invalid Pan Number");
            return false;
        }
        else if (code.test(code_chk) == false) {
            $("#" + spanid + "").addClass('error-message');
            $("#" + id + "").addClass('is-invalid');
            $("#" + spanid).html("Invalid Pan Number");
            return false;
        }
        else {
            $("#" + spanid + "").removeClass('error-message');
            $("#" + id + "").removeClass('is-invalid');
            $("#" + spanid).html("");

            val = val.toUpperCase();

            return val;
        }
    }
}
function validatePanWithoutMandatory(id, spanid) {
    debugger;
    var val = $("#" + id + "").val();
    if (val != '') {
    
        var pan = val.toUpperCase();

        var panPat = /^([a-zA-Z]{5})(\d{4})([a-zA-Z]{1})$/;
        var code = /([C,P,H,F,A,T,B,L,J,G])/;
        var code_chk = pan.substring(3, 4);
        if (pan.search(panPat) == -1) {
            $("#" + spanid + "").addClass('error-message');
            $("#" + id + "").addClass('is-invalid');
            $("#" + spanid).html("Invalid Pan Number");
            return false;
        }
        else if (code.test(code_chk) == false) {
            $("#" + spanid + "").addClass('error-message');
            $("#" + id + "").addClass('is-invalid');
            $("#" + spanid).html("Invalid Pan Number");
            return false;
        }
        else {
            $("#" + spanid + "").removeClass('error-message');
            $("#" + id + "").removeClass('is-invalid');
            $("#" + spanid).html("");

            val = val.toUpperCase();

            return val;
        }
    }
    return true;
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

            return true;
        }
    }
}

function validateAadharWithoutMandatory(val, id) {
    debugger;

    aadhar = $('#' + val).val();
    if (aadhar != '') {    
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
    return true;

}
function validateEmailID(val, id) {
    debugger
    email = val.value;
    if (val.value == "") {
        $("#" + id + "").addClass('error-message');
        $(val).addClass('is-invalid');
        $("#" + id + "").html("This field is required");
    }
    else {
        filter = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,10})+$/
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
            //trim(val);
            return true;
        }
    }
}
function validateEmailIDWithoutMandatory(val, id) {
    debugger
    email = val.value;
    if (val.value != "") {

        filter = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,10})+$/
        if (!filter.test(email)) {
            debugger;
            $("#" + id + "").addClass('error-message');
            $(val).addClass('is-invalid');
            $("#" + id + "").html('Invalid Email ID')
            return false;
        }
        else {
            $("#" + id + "").removeClass('error-message');
            $(val).removeClass('is-invalid');
            $("#" + id + "").html("");
            //trim(val);
            return true;
        }
    }
    else {
        $("#" + id + "").removeClass('error-message');
        $(val).removeClass('is-invalid');
        $("#" + id + "").html("");      
        return true;
    }
}
function validatePincode(val, id) {
    debugger;

    pin = val.value;
    if (val.value == "") {
        $("#" + id + "").addClass('error-message');
        $(val).addClass('is-invalid');
        $("#" + id + "").html("This field is required");
        return false;
    }
    else {
        if (pin.length == 6) {
            $("#" + id + "").removeClass('error-message');
            $(val).removeClass('is-invalid');
            $("#" + id + "").html("");
            return true;
        }
        else {
            $("#" + id + "").addClass('error-message');
            $(val).addClass('is-invalid');
            $("#" + id + "").html("Please enter a valid PIN number");
            return false;
        }
    }
}
function validateDateOfBirth(sender, id) {
    debugger;
    dateofbirth = $(sender).val();
    if (sender.value == "") {
        $("#" + id + "").addClass('error-message');
        $(sender).addClass('is-invalid');
        $("#" + id + "").html("This field is required");
    }
    else {
        debugger;

        var DOB = new Date(dateofbirth);
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
            $(sender).addClass('is-invalid');
            $("#" + id + "").html("Age must be greater than 18");
            return false;
        }

        else {
            $("#" + id + "").removeClass('error-message');
            $(sender).removeClass('is-invalid');
            $("#" + id + "").html("");
           /* trim(val);*/
            return true;
        }
    }
}


function validateDate(date) {
    var dateString = date;
    var parts = dateString.split(' '); // Split the string into date and time parts
    var datePart = parts[0];
    var timePart = parts[1];

    var dateParts = datePart.split('-'); // Split the date part into day, month, and year
    var year = parseInt(dateParts[2], 10);
    var month = parseInt(dateParts[1], 10) - 1; // Months are 0-based in JavaScript Date objects
    var day = parseInt(dateParts[0], 10);

    var timeParts = timePart.split(':'); // Split the time part into hours, minutes, and seconds
    var hour = parseInt(timeParts[0], 10);
    var minute = parseInt(timeParts[1], 10);
    var second = parseInt(timeParts[2], 10);

    var dateObject = new Date(year, month, day, hour, minute, second);
   /* console.log(dateObject);*/
    return dateObject;

}
