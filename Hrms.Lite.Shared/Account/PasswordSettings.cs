using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.Account
{
    public class PasswordSettings
    {
        public bool IncludeLowercase { get; set; }
        public bool IncludeUppercase { get; set; }
        public bool IncludeNumeric { get; set; }
        public bool IncludeSpecial { get; set; }
        public int Length { get; set; }
    }
    public class PasswordPolicySettings
    {      
        public int MinimumLength { get; set; }
        public int MaximumLength { get; set; }
        public int MinimumUppercaseLetters { get; set; }
        public int MinimumLowercaseLetters { get; set; }
        public int MinimumSpecialCharacter { get; set; }
        public int MinimumNumericValues { get; set; }
        public int PasswordExpiryDays { get; set; }
        public int EnforcePasswordHistory { get; set; }
        public bool ChangePasswordOTPRequired { get; set; }
    }
    public class ChangePassword
    {        
        public string EnterCurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public string PasswordHash_NewPassword { get; set; }
        public string PasswordSalt_NewPassword { get; set; }
        public string PasswordHash_ConfirmPassword { get; set; }
        public string PasswordSalt_ConfirmPassword { get; set; }
        public string PasswordHash_CurrentPassword { get; set; }
        public string PasswordSalt_CurrentPassword { get; set; }
        public string OTP { get; set; }
        public PasswordPolicySettings PasswordPolicySettings { get; set; }

    }
    public class ForgetPassword
    {
        public Guid EmployeeGI { get; set; }
        public string CompanyID { get; set; }
        public string EmployeeID { get; set; }
        public DateTime? DOB { get; set; }
        public string EnterOTP { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public string PasswordHash_NewPassword { get; set; }
        public string PasswordSalt_NewPassword { get; set; }
        public string PasswordHash_ConfirmPassword { get; set; }
        public string PasswordSalt_ConfirmPassword { get; set; }
        public PasswordPolicySettings PasswordPolicySettings { get; set; }
    }
        public class SchedulePasswordGenertion
        {
        public int UserCode { get; set; }
        public int EmployeeCode { get; set; }
            public string HashPassword { get; set; }
            public string SaltPassword { get; set; }
            public string Password { get; set; }
    }

    }
