using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static Hrms.Lite.Shared.Account.ForgetPassword;

namespace Hrms.Lite.Services.IServices.Account
{
    public interface IPasswordSettingsService
    {
        public Task<PasswordPolicySettings> GetPasswordPolicySettings();
        public Task<PasswordPolicySettings> GetForgetPasswordPasswordPolicySettings(string CompanyID);
        public Task<ResponseEntity> ChangePasswordSave(ChangePassword model);
        public Task<ResponseEntity> ForgetPasswordSave(ForgetPassword model);
        public Task<ResponseEntity> PasswordPolicySettingsSave(PasswordPolicySettings model);
        public Task<ResponseEntity> ForgetPasswordVerifyOTP(ForgetPassword model);
        public Task<ResponseEntity> ForgetPasswordGenerateOTP(ForgetPassword model);
        public Task<ResponseEntity> ChangePasswordVerifyOTP(string OTP);
        public Task<ResponseEntity> ChangePasswordGenerateOTP(string CurrPassword);

        //--------------password Reset--------------------------
        public Task<ResponseEntity> ResetPassword(SchedulePasswordGenertion model);
    }
}
