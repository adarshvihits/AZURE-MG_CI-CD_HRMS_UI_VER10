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
    public class TDSVsAllowanceSettingsService: ITDSVsAllowanceSettingsService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public TDSVsAllowanceSettingsService(IServiceBase httpService)
        {
            _baseURL = "TDSVsAllowanceSettings";
            _httpService = httpService;
        }
        public async Task<TDSVsAllowanceSettings> GetTDSVsAllowanceSettingsList()
        {
            return await _httpService.GetAsync<TDSVsAllowanceSettings>($"{_baseURL}/GetTDSVsAllowanceSettingsList");
        }

        public async Task<ResponseEntity> TDSVsAllowanceSettingsAllowanceSave(TDSVsAllowanceSettings model)
        {
            return await _httpService.PostAsync<ResponseEntity, TDSVsAllowanceSettings>($"{_baseURL}/TDSVsAllowanceSettingsAllowanceSave", model);
        }
    }
}
