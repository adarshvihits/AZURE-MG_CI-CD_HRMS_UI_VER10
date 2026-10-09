var proceedBtnClicked = 0;

function onloginGrpcompChange(sender) {
    var grp = $(sender).val();
    $.ajax({
        type: 'Get',
        url: '/DashBoard/LandingPage/GroupCompnyChange',
        dataType: 'json',
        async:false,    
        data: { GrpCompanyCode: $(sender).val() },
        success: function (response) {
            debugger
            console.log(response); if (response == true) {
                loginagain(grp);
            }
        },

        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
        }
    });
}

function loginagain(newgrp) {
    debugger

    // Create a FormData object
    var formData = new FormData();
    formData.append("CompanyCode", $('#NCompanyCode').val());
    formData.append("UserCode", $('#NUserCode').val());
    formData.append("Password", $('#NPassword').val());
    formData.append("GroupCompanyCode", newgrp);
    formData.append("isRedirected", $('#NisRedirected').val());
    formData.append("GrpChange", "YES");
    // AJAX POST request
    $.ajax({
        url: '/Account/Login', // Update with your controller's route
        type: 'POST',
        processData: false, // Prevent jQuery from processing the data
        contentType: false, // Set to false to let the browser set it automatically
        data: formData, // Send as FormData
        success: function (response) {
            debugger
            console.log(response);
            window.location.href = response.redirectUrl;
        },
        error: function (xhr, textStatus, errorThrown) {
            $('body').html(xhr.responseText);
        }
    });
}

function forgotPassword() {
    var url = "/Account/ForgotPassword";
    window.location.href = url;
}
function GetUserRoleDetails(userRoleCode) {
    debugger
    var url = "/Account/GetLogOutUserRoleDetails";
    $('#userRoleDetailsView').load(url, { UserRoleCode: userRoleCode }, function () { });
}
function GenerateOTP() {
    var i = 0;
    if (!validateById('#CompanyID', 'CmpId'))
        i++;
    if (!validateById('#EmployeeID', 'EmpId'))
        i++;
    if (!validateById('#DOB', 'DB'))
        i++;

    if (i == 0) {
        $.ajax({

            type: 'POST',
            url: '/Account/GenerateOTP',
            dataType: 'json',
            data: $('#genarateOTP').serialize(),
            success: function (result) {
                debugger;
                if (result.success == true) {
                    $('#verifyOtp').removeClass('hide');
                    swal.fire(result.message).then(function () {
                        $(".disable").prop('disabled', true);
                    });
                }
                else {
                    swal.fire(result.message);
                }
            },


            error: function (xhr, textStatus, errorThrown) {
                $('body').html(xhr.responseText);
            }
        });
    }
    
}

function Verify()
{
    debugger
    if ($("#EnterOTP").val() == '' || $("#EnterOTP").val() == null)
        swal.fire('Please Enter OTP')
    else
    {
        $(".disable").prop('disabled', false);
        $.ajax({

            type: 'POST',
            url: '/Account/VerifyOTP',
            dataType: 'json',
            data: $('#genarateOTP').serialize(),
            success: function (result) {
                debugger;
                if (result.success == true) {
                    swal.fire("OTP Verified").then(function () {
                        var url = "/Account/ForgetGetPasswordPolicy";
                        $('#changePWD').load(url, { CompanyID: $('#CompanyID').val() }, function (data) { console.log(data) });
                        $(".disable").prop('disabled', true);
                    });
                }
                else {
                    swal.fire(result.message);
                }
            },


            error: function (xhr, textStatus, errorThrown) {
                $('body').html(xhr.responseText);
            }
        });
    }
}

function ForgotPasswordSave() {
    var i = 0;
    $(".disable").prop('disabled', false);
    if (!validateById('#ConfirmPassword', 'confirmpwd')) {
        i++;
    }
        if (!validatePolicy())
            return false;
        if (i == 0) {
            $.ajax({

                type: 'POST',
                url: '/Account/ForgotPasswordSave',
                dataType: 'json',
                data: $('#genarateOTP').serialize(),
                success: function (result) {
                    debugger;
                    if (result.success) {
                        swal.fire("Saved Successfully").then(function () {
                            var url = '/Account/Login';
                            window.location.href = url;
                        });
                    }
                    else {
                        swal.fire(result.message);
                    }
                },


                error: function (xhr, textStatus, errorThrown) {
                    $('body').html(xhr.responseText);
                }
            });
        }
        else {
            return false;
        }
    }

function PasswordPolicySave() {
    var i = 0;
    if (!validateById('#MinimumLength', 'minLen'))
        i++;
    if (!validateById('#MinimumSpecialCharacter', 'minSplChar'))
        i++;
    if (!validateById('#MaximumLength', 'maxLen'))
        i++;
    if (!validateById('#MinimumNumericValues', 'minNum'))
        i++;
    if (!validateById('#MinimumUppercaseLetters', 'minUpper'))
        i++;
    if (!validateById('#PasswordExpiryDays', 'passExpiry'))
        i++;
    if (!validateById('#MinimumLowercaseLetters', 'minLower'))
        i++;
    if (!validateById('#EnforcePasswordHistory', 'passHistory'))    
        i++;

    if (i == 0) {
        swal.fire({
            text: "Do you want to save?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({

                    type: 'POST',
                    url: '/Master/PasswordPolicySettings/Save',
                    dataType: 'json',
                    data: $('#PWDPolicy').serialize(),
                    success: function (result) {
                        debugger;
                        if (result.success == true) {
                            swal.fire("Saved Successfully").then(function () {
                                //var url = '/Account/Login';
                                //window.location.href = url;
                            });
                        }
                        else {
                            swal.fire(result.message);
                        }
                    },


                    error: function (xhr, textStatus, errorThrown) {
                        $('body').html(xhr.responseText);
                    }
                });
            }
            else {
                return false;
            }
        })
    }
    else
        return false;
}

function changePwd() {
    var url = "/Account/ChangePassword?CompanyID=" + $('#CompanyID').val();
    window.location.href = url;
}

function maskPassword(e) {
    let input = e.target;
    let val = input.value;
    let hidden = document.getElementById("hdnPassword");
    // If user typed a new character, append it to hidden value
    if (val.length > input.dataset.lastLength) {
        let newChar = val.charAt(val.length - 1);
        hidden.value += newChar;
    } else {
        hidden.value = hidden.value.substring(0, hidden.value.length - 1);
    }
    input.value = "*".repeat(hidden.value.length);
    input.dataset.lastLength = hidden.value.length;
}
function SoftwareSettingsForm()
{
    var Password = document.getElementById("hdnPassword").value;
    if (Password == "") {
        swal.fire('Please Enter Password');
        return;
    }
    if (Password == "ihitshrms")
    {
        var url = "/Admin/SoftwareSettings/Index";
        window.location.href = url;
        return;
    }
    else
    {
        swal.fire('Please Enter a valid password.');
    }

   
}
function ChangePasswordGenerateOTP() {

    var i = 0;
    if (!validateById('#EnterCurrentPassword', 'currentpwd')) {
        i++;
    }
    if (!validateById('#ConfirmPassword', 'confirmpwd')) {
        i++;
    }
    if (!validatePolicy())
        return false;
    if (i == 0) {
        $.ajax({

            type: 'POST',
            url: '/Account/ChangePasswordGenerateOTP',
            dataType: 'json',
            data: { CurrPassword: encodeURIComponent($('#EnterCurrentPassword').val())},
            success: function (result) {
                debugger;
                if (result.success == true) {
                    $('#verifyOtp').removeClass('hide');
                    swal.fire(result.message).then(function () {
                        $(".genOtp").prop('disabled', true);
                        $("#EnterCurrentPassword").prop('disabled', true);
                        $("#NewPassword").prop('disabled', true);
                        $("#ConfirmPassword").prop('disabled', true);
                    });
                }
                else {
                    swal.fire(result.message);
                }
            },


            error: function (xhr, textStatus, errorThrown) {
                $('body').html(xhr.responseText);
            }
        });
    }

}

function ChangePasswordVerify() {
    debugger
    if ($("#OTP").val() == '' || $("#OTP").val() == null)
        swal.fire('Please Enter OTP')
    else { 
        $.ajax({
            type: 'GET',
            url: '/Account/ChangePasswordVerifyOTP',
            dataType: 'json',
            data: { Otp: $("#OTP").val()},
            success: function (result) {
                debugger;
                if (result.success == true) {
                    $('.chPwd').removeClass('hide');
                    swal.fire("OTP Verified Successfully").then(function () {
                        $("#verOTP").prop('disabled', true);
                        $("#OTP").prop('disabled', true);
                    });
                }
                else {
                    swal.fire(result.message);
                }
            },


            error: function (xhr, textStatus, errorThrown) {
                $('body').html(xhr.responseText);
            }
        });
    }
}

function ChangePassword() {
    debugger;
    
    var i = 0;
        if (!validateById('#EnterCurrentPassword', 'currentpwd')) {
            i++;
        }
        if (!validateById('#ConfirmPassword', 'confirmpwd')) {
            i++;
    }
    if (!validatePolicy())
        return false;
    if (i == 0) {
        $('.form-control').removeAttr('disabled')
        $.ajax({

            type: 'POST',
            url: '/Account/SavePassword',
            dataType: 'json',
            data: $('#changePass').serialize(),
            success: function (result) {
                debugger;
                if (result.success) {
                    swal.fire(result.message).then(function () {
                        var url = '/Account/Login';
                        window.location.href = url;
                    });
                }
                else {
                    swal.fire(result.message);
                }
            },


            error: function (xhr, textStatus, errorThrown) {
                $('body').html(xhr.responseText);
            }
        });
    }
    else {
        return false;
    }
}

function checkPassword(password) {

    if (password.length === 0) {
        display.innerHTML = "";
    }
    else if (!password.match(/[a-z]+/)) {
        display.innerHTML = "Missing lowercase letters";
    }
    else if (!password.match(/[A-Z]+/)) {
        display.innerHTML = "Missing uppercase letters";
    }
    else if (!password.match(/[0-9]+/)) {
        display.innerHTML = "Missing numbers";
    }
    else if (!password.match(/[$@@#&!]+/)) {
        display.innerHTML = "Missing special chanracters";
    }
    else if (password.length < 8 && password.length != 0) {
        display.innerHTML = "Minimum password length is 8";
    }
    else if (password.length >= 8) {
        display.innerHTML = "";
    }
}

function validatePolicy() {
    debugger;
    var i = 0;    
    password = $('#NewPassword').val();
    pwdLength = password.length;
    minLength = $('#PasswordPolicySettings_MinimumLength').val();
    maxLength = $('#PasswordPolicySettings_MaximumLength').val();
    minUpper = $('#PasswordPolicySettings_MinimumUppercaseLetters').val();
    minLower = $('#PasswordPolicySettings_MinimumLowercaseLetters').val();
    minSpecial = $('#PasswordPolicySettings_MinimumSpecialCharacter').val();
    minNumerical = $('#PasswordPolicySettings_MinimumNumericValues').val();
    upperCharacter = password.match(/[A-Z]/g);
    var upperLength = 0, lowerLength = 0, specialLength = 0, numericalLength = 0;
    if (upperCharacter != null)
        upperLength = upperCharacter.length;
    lowerCharacter = password.match(/[a-z]/g);
    if (lowerCharacter != null)
        lowerLength = lowerCharacter.length;
    specialCharacter = password.match(/[!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?]/gi);
    if (specialCharacter != null)
    specialLength = specialCharacter.length;
    numericalValue = password.match(/[0-9]/g);
    if (numericalValue != null)
    numericalLength = numericalValue.length;
    if (minLower != 0) {
        if (lowerLength >= minLower) {
            $('#lowerPassword').addClass('list-styleicon');
            $('#lowerPassword').removeClass('list-cancelicon');
        }
        else {
            i++;
            $('#lowerPassword').removeClass('list-styleicon');
            $('#lowerPassword').addClass('list-cancelicon');
        }
    }
    else {
        $('#upperPassword').addClass('hide');
    }
    if (minUpper != 0) {
        if (upperLength >= minUpper) {
            $('#upperPassword').addClass('list-styleicon');
            $('#upperPassword').removeClass('list-cancelicon');
        }
        else {
            i++;
            $('#upperPassword').removeClass('list-styleicon');
            $('#upperPassword').addClass('list-cancelicon');
        }
    }
    else {
        $('#lowerPassword').addClass('hide');
    }
    if (minNumerical != 0) {
        if (numericalLength >= minNumerical) {
            $('#numeralPassword').addClass('list-styleicon');
            $('#numeralPassword').removeClass('list-cancelicon');
        }
        else {
            i++;
            $('#numeralPassword').removeClass('list-styleicon');
            $('#numeralPassword').addClass('list-cancelicon');
        }
    }
    else {
        $('#numeralPassword').addClass('hide');
    }
    if (minSpecial != 0) {
        if (specialLength >= minSpecial) {
            $('#specialPassword').addClass('list-styleicon');
            $('#specialPassword').removeClass('list-cancelicon');
        }
        else {
            i++;
            $('#specialPassword').removeClass('list-styleicon');
            $('#specialPassword').addClass('list-cancelicon');
        }
    }
    else {
        $('#specialPassword').addClass('hide');
    }
    if (pwdLength < minLength) {
        $('#NewPassword').addClass('is-invalid');
        $("#newpwd").addClass('error-message');
        $("#newpwd").html("Minimum " + minLength + " Characters Required");
        $('#minmaxPassword').removeClass('list-styleicon')
        $('#minmaxPassword').addClass('list-cancelicon')
        return false;
       
    }
    else {
        $('#minmaxPassword').addClass('list-styleicon')
        $('#minmaxPassword').removeClass('list-cancelicon')
        $('#NewPassword').removeClass('is-invalid');
        $("#newpwd").removeClass('error-message');
        $("#newpwd").html("");
    }
    if (pwdLength > maxLength) {
        $('#NewPassword').addClass('is-invalid');
        $("#newpwd").addClass('error-message');
        $("#newpwd").html("Maximum " + maxLength + " Characters Allowed");
        $('#minmaxPassword').removeClass('list-styleicon')
        $('#minmaxPassword').addClass('list-cancelicon')
        return false;

    }
    else {
        $('#minmaxPassword').addClass('list-styleicon')
        $('#minmaxPassword').removeClass('list-cancelicon')
        $('#NewPassword').removeClass('is-invalid');
        $("#newpwd").removeClass('error-message');
        $("#newpwd").html("");

    }
    if (pwdLength >= minLength && pwdLength <= maxLength) {
        $('#minmaxPassword').addClass('list-styleicon')
        $('#minmaxPassword').removeClass('list-cancelicon')
    }
    if (upperLength < minUpper) {
        $('#NewPassword').addClass('is-invalid');
        $("#newpwd").addClass('error-message');
        $("#newpwd").html("Minimum " + minUpper + " uppercase letters Required");
        $('#upperPassword').removeClass('list-styleicon')
        $('#upperPassword').addClass('list-cancelicon')
        return false;

    }
    else {
        $('#upperPassword').addClass('list-styleicon')
        $('#upperPassword').removeClass('list-cancelicon')
        $('#NewPassword').removeClass('is-invalid');
        $("#newpwd").removeClass('error-message');
        $("#newpwd").html("");

    }
    if (lowerLength < minLower) {
        $('#NewPassword').addClass('is-invalid');
        $("#newpwd").addClass('error-message');
        $("#newpwd").html("Minimum " + minLower + " lowercase letters Required");
        $('#lowerPassword').removeClass('list-styleicon')
        $('#lowerPassword').addClass('list-cancelicon')
        return false;

    }
    else {
        $('#lowerPassword').addClass('list-styleicon')
        $('#lowerPassword').removeClass('list-cancelicon')
        $('#NewPassword').removeClass('is-invalid');
        $("#newpwd").removeClass('error-message');
        $("#newpwd").html("");

    }
    if (specialLength < minSpecial) {
        $('#NewPassword').addClass('is-invalid');
        $("#newpwd").addClass('error-message');
        $("#newpwd").html("Minimum " + minSpecial + " Special Characters Required");
        $('#specialPassword').removeClass('list-styleicon')
        $('#specialPassword').addClass('list-cancelicon')
        return false;

    }
    else {
        $('#specialPassword').addClass('list-styleicon')
        $('#specialPassword').removeClass('list-cancelicon')
        $('#NewPassword').removeClass('is-invalid');
        $("#newpwd").removeClass('error-message');
        $("#newpwd").html("");

    }
    if (numericalLength < minNumerical) {
        $('#NewPassword').addClass('is-invalid');
        $("#newpwd").addClass('error-message');
        $("#newpwd").html("Minimum " + minNumerical + " Numerical Characters Required");
        $('#numeralPassword').removeClass('list-styleicon')
        $('#numeralPassword').addClass('list-cancelicon')
        return false;

    }
    else {
        $('#numeralPassword').addClass('list-styleicon')
        $('#numeralPassword').removeClass('list-cancelicon')
        $('#NewPassword').removeClass('is-invalid');
        $("#newpwd").removeClass('error-message');
        $("#newpwd").html("");

    }
    if (i > 0)
        return false;
    else
      return true;
}

function validateconfirm(val) {
    debugger;
    if ($('#NewPassword').val() != $('#ConfirmPassword').val()) {
        $(val).addClass('is-invalid');
        $("#confirmpwd").addClass('error-message');
        $("#confirmpwd").html("Password doesn't matched");
    }
    else {
        $(val).removeClass('is-invalid');
        $("#confirmpwd").removeClass('error-message');
        $("#confirmpwd").html("");
    }
}

function fillGroupCompanyDropdown() {
    $.getJSON("/Account/GetGroupCompanyDropdown", { CompanyCode: $('#CompanyCode').val(), UserCode: $('#UserCode').val() }, function (data) {

        $('#GroupCompanyCode option').remove();
        $('#GroupCompanyCode').append('<option value>--Select Group Company--</option');

        for (var i = 0; i < data.length; i++) {
            $('#GroupCompanyCode').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
        }

        
        if (data.length == 1) {
            $('#GroupCompanyCode').val(data[0].value);

        }

    });
}

function loginProceed() {
    debugger
    var i = 0;
    if (!validateById('#CompanyCode', 'CmpCode'))
        i++;
    if (!validateById('#UserCode', 'UsrCode'))
        i++;
    if (!validateById('#Password', 'Pwd'))
        i++;
    
    if (i == 0) {        
        $.ajax({

            type: 'POST',
            url: '/Account/Proceed?Data=' + localStorage.getItem('data'),
            dataType: 'json',
            async:false,
            data: $('#HrmsLogin').serialize(),
            success: function (result) {
                debugger;
                if (result.success == true) {
                    $('#grpCmpDiv').html('<select class="form-control form-control-sm" data-val="true" data-val-required="The GroupCompanyCode field is required." id="GroupCompanyCode" name="GroupCompanyCode"><option value="" onchange="validate(this,"grpCmpSpan")">--Select Group Company--</option></select>')
                    fillGroupCompanyDropdown();
                    //$('#GroupCompanyCode').removeClass('hide');
                    $('.proceed-login-btn').attr('disabled', true);
                    $('#LoginBtn').removeAttr('disabled');
                    $('#LoginBtn').removeClass('hide');
                    $('.text-danger').html('');
                    $('#validationMsgSpan').html('');
                    proceedBtnClicked = 1;
                }
                else {

                    if (result.message == 'Please Enter Valid Captcha') {
                        //$('#dntCaptchaRefreshButton').trigger('click');
                        $('#Captcha').addClass('is-invalid');
                        $("#cap").addClass('error-message');
                        $('#cap').html(result.message);
                    }
                    else {
                        /*$('#dntCaptchaRefreshButton').trigger('click');*/
                        LoadLoginControls();
                        
                        $('#validationMsgSpan').html(result.message);
                    }

                }
            },


            error: function (xhr, textStatus, errorThrown) {
                $('body').html(xhr.responseText);
            }
        });
    }

   
}


function validateGrpCmpy() {
    var i = 0;
    if (!validateById('#GroupCompanyCode', 'grpCmpSpan'))
        i++;
    if (i == 0) {
        return true;
    }
    else {
        return false;
    }

}

function CompanyCodeChange()
{
    debugger;
    var CompanyCode = document.getElementById('CompanyCode');
    var Code = CompanyCode.value.toUpperCase();
    $("#CompanyCode").val(Code);
}



function onLoginControlsChange(event) {
    if (event.keyCode != 13) {
        if (proceedBtnClicked == 1) {
            LoadLoginControls();
            proceedBtnClicked = 0;
            $('#LoginBtn').attr('disabled', 'true');
            $('.proceed-login-btn').removeAttr('disabled');

            //$('#dntCaptchaRefreshButton').trigger('click');
        }
    }
}




function LoadLoginControls() {
    debugger
    $.ajax({

        type: 'POST',
        url: '/Account/LoginControls',
        dataType: 'html',
        async:false,
        data: $('#HrmsLogin').serialize(),
        success: function (result) {
            debugger;
            $('#loginControls').html('');
            $('#loginControls').html(result);
            refreshCaptcha();
            $('#Captcha').val('');
        },
        error: function (xhr, textStatus, errorThrown) {
            $('body').html(xhr.responseText);
        }
    });
}

function onPwdVisibility() {
    debugger;
    var x = document.getElementById("Password");
    if (x.type === "password") {
        x.type = "text";
        $('#togglePassword').removeClass("fa-eye-slash");
        $('#togglePassword').addClass("fa-eye");

    } else {
        x.type = "password";
        $('#togglePassword').addClass("fa-eye-slash");
        $('#togglePassword').removeClass("fa-eye");
    }
}

