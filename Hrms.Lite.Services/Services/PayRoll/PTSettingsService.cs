using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.PayRoll;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.PayRoll
{
    public class PTSettingsService:IPTSettingsService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public PTSettingsService(IServiceBase httpService)
        {
            _baseURL = "PTSettings";
            _httpService = httpService;
        }
        public async Task<PTSettings> PTSettingsList(int ptGroup)
        {
            return await _httpService.GetAsync<PTSettings>($"{_baseURL}/PTSettingsList?PTGroupCode={ptGroup}");
        }
        public async Task<ResponseEntity> Save(PTSettings model)
        {
            return await _httpService.PostAsync<ResponseEntity, PTSettings>($"{_baseURL}/PTSettingsSave", model);
        }
        public async Task<PTSettings> GetPTSettingsAllowance(int ptGroup)
        {
            return await _httpService.GetAsync<PTSettings>($"{_baseURL}/PTAllowanceSettingsList?PTGroupCode={ptGroup}");
        }
        public async Task<ResponseEntity> AllowanceSave(PTSettings model)
        {
            return await _httpService.PostAsync<ResponseEntity, PTSettings>($"{_baseURL}/PTAllowanceSettingsSave", model);
        }
    }
}
