using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.Account;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.Account
{
    public class PasswordSettingsService :IPasswordSettingsService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public PasswordSettingsService(IServiceBase httpService)
        {
            _baseURL = "PasswordSettings";
            _httpService = httpService;
        }
        async Task<PasswordPolicySettings> IPasswordSettingsService.GetPasswordPolicySettings()
        {
            return await _httpService.GetAsync<PasswordPolicySettings>($"{_baseURL}/GetPasswordPolicySettings");
        }
        async Task<PasswordPolicySettings> IPasswordSettingsService.GetForgetPasswordPasswordPolicySettings(string CompanyID)
        {
            return await _httpService.GetAsync<PasswordPolicySettings>($"{_baseURL}/GetForgetPasswordPasswordPolicySettings?CompanyID={CompanyID}");
        }
        async Task<ResponseEntity> IPasswordSettingsService.ChangePasswordSave(ChangePassword model)
        {
            return await _httpService.PostAsync<ResponseEntity, ChangePassword>($"{_baseURL}/ChangePasswordSave", model);
        }
        async Task<ResponseEntity> IPasswordSettingsService.ForgetPasswordSave(ForgetPassword model)
        {
            return await _httpService.PostAsync<ResponseEntity, ForgetPassword>($"{_baseURL}/ForgetPasswordSave", model);
        }
        async Task<ResponseEntity> IPasswordSettingsService.PasswordPolicySettingsSave(PasswordPolicySettings model)
        {
            return await _httpService.PostAsync<ResponseEntity, PasswordPolicySettings>($"{_baseURL}/PasswordPolicySettingsSave", model);
        }
        async Task<ResponseEntity> IPasswordSettingsService.ForgetPasswordVerifyOTP(ForgetPassword model)
        {
            return await _httpService.PostAsync<ResponseEntity, ForgetPassword>($"{_baseURL}/ForgetPasswordVerifyOTP", model);
        }
        async Task<ResponseEntity> IPasswordSettingsService.ForgetPasswordGenerateOTP(ForgetPassword model)
        {
            return await _httpService.PostAsync<ResponseEntity, ForgetPassword>($"{_baseURL}/ForgetPasswordGenerateOTP", model);
        }

        async Task<ResponseEntity> IPasswordSettingsService.ChangePasswordVerifyOTP(string OTP)
        {
            return await _httpService.GetAsync<ResponseEntity>($"{_baseURL}/ChangePasswordVerifyOTP?OTP={OTP}");
        }
        async Task<ResponseEntity> IPasswordSettingsService.ChangePasswordGenerateOTP(string CurrPassword)
        {
            return await _httpService.GetAsync<ResponseEntity>($"{_baseURL}/ChangePasswordGenerateOTP?CurrPassword={CurrPassword}");
        }

        public async Task<ResponseEntity> ResetPassword(SchedulePasswordGenertion model)
        {
            return await _httpService.PostAsync<ResponseEntity, SchedulePasswordGenertion>($"{_baseURL}/ResetPassword", model);
        }
    }
}
