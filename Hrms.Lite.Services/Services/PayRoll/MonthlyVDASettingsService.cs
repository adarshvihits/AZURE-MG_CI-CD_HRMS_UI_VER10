using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.PayRoll;
using Hrms.Lite.Services.IServices.PayRoll.Reports;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using Hrms.Lite.Shared.PayRoll;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.PayRoll
{    
    public class MonthlyVDASettingsService : IMonthlyVDASettingsService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public MonthlyVDASettingsService(IServiceBase httpService)
        {
            _baseURL = "MonthlyVDASettings";
            _httpService = httpService;
        }
        async Task<VDAGroup> IMonthlyVDASettingsService.GetMonthlyVDASettingsList(int SalaryPeriodCode)
        {
            return await _httpService.GetAsync<VDAGroup>($"{_baseURL}/GetMonthlyVDASettingsList?SalaryPeriodCode={SalaryPeriodCode}");
        }

        async Task<ResponseEntity> IMonthlyVDASettingsService.MonthlyVDASettingsSave(VDAGroup model)
        {
            return await _httpService.PostAsync<ResponseEntity, VDAGroup>($"{_baseURL}/MonthlyVDASettingsSave", model);
        }
    }
}
