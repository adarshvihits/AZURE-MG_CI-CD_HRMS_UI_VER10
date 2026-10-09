using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.PayRoll;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.PayRoll
{
    public class ESISettingsService : IESISettingsService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public ESISettingsService(IServiceBase httpService)
        {
            _baseURL = "ESISettings";
            _httpService = httpService;
        }
        async Task<ESISettings> IESISettingsService.GetESISettingsApplicableAllowanceList(int DeductionCode)
        {
            return await _httpService.GetAsync<ESISettings>($"{_baseURL}/GetESISettingsApplicableAllowanceList?DeductionCode={DeductionCode}");
        }
        async Task<ESISettings> IESISettingsService.GetESIGeneralsettingsList(int DeductionCode)
        {
            return await _httpService.GetAsync<ESISettings>($"{_baseURL}/GetESIGeneralsettingsList?DeductionCode={DeductionCode}");
        }
        async Task<ESISettings> IESISettingsService.GetESIEmployeeWiseSettingsList(int DeductionCode, int MonthCode,Filter filter)
        {
            return await _httpService.PostAsync<ESISettings,Filter>($"{_baseURL}/GetESIEmployeeWiseSettingsList?DeductionCode={DeductionCode}&MonthCode={MonthCode}",filter);
        }
        async Task<ESISettings> IESISettingsService.GetESIEmployeeWiseSettingsDetails(Guid EmployeeGI, int DeductionCode, int MonthCode)
        {
            return await _httpService.GetAsync<ESISettings>($"{_baseURL}/GetESIEmployeeWiseSettingsDetails?EmployeeGI={EmployeeGI}&DeductionCode={DeductionCode}&MonthCode={MonthCode}");
        }
        async Task<ResponseEntity> IESISettingsService.ESISettingsAllowanceSave(ESISettings model)
        {
            return await _httpService.PostAsync<ResponseEntity, ESISettings>($"{_baseURL}/ESISettingsAllowanceSave", model);
        }
        async Task<ResponseEntity> IESISettingsService.ESIGeneralsettingsSave(ESISettings model)
        {
            return await _httpService.PostAsync<ResponseEntity, ESISettings>($"{_baseURL}/ESIGeneralsettingsSave", model);
        }
        async Task<ResponseEntity> IESISettingsService.ESIEmployeewisesettingsSave(ESISettings model)
        {
            return await _httpService.PostAsync<ResponseEntity, ESISettings>($"{_baseURL}/ESIEmployeewisesettingsSave", model);
        }
        public async Task<DataSet> GetESIEmployeeWiseSettingsList_Excel(int DeductionCode, int MonthCode, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetESIEmployeeWiseSettingsList_Excel?DeductionCode={DeductionCode}&MonthCode={MonthCode}", filter);
        }
    }
}
