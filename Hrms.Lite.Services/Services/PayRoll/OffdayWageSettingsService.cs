using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.PayRoll;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Hrms.Lite.Shared.PayRoll.AllowanceSettings;
using Hrms.Lite.Shared.Master;
using System.Data;

namespace Hrms.Lite.Services.Services.PayRoll
{
    public class OffdayWageSettingsService : IOffdayWageSettingsService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public OffdayWageSettingsService(IServiceBase httpService)
        {
            _baseURL = "OffdayWageSettings";
            _httpService = httpService;
        }
        async Task<List<AllowanceSettingsEmployeeList>> IOffdayWageSettingsService.GetSettingsEmployeeList(int AllowanceCode, Filter filter)
        {
            return await _httpService.PostAsync<List<AllowanceSettingsEmployeeList>, Filter>($"{_baseURL}/GetSettingsEmployeeList?AllowanceCode={AllowanceCode}", filter);
        }
        async Task<ApplicableAllowance> IOffdayWageSettingsService.GetApplicableAllowanceList()
        {
            return await _httpService.GetAsync<ApplicableAllowance>($"{_baseURL}/GetApplicableAllowanceList");
        }

       
        async Task<Allowance> IOffdayWageSettingsService.GetBatchUpDateDetails(int AllowanceCode)
        {
            return await _httpService.GetAsync<Allowance>($"{_baseURL}/GetBatchUpDateDetails?AllowanceCode={AllowanceCode}");
        }

        async Task<ResponseEntity> IOffdayWageSettingsService.SaveApplicableAllowance(ApplicableAllowance model)
        {
            return await _httpService.PostAsync<ResponseEntity, ApplicableAllowance>($"{_baseURL}/SaveApplicableAllowance", model);
        }

        async Task<ResponseEntity> IOffdayWageSettingsService.SaveEmployeeWiseSettings(AllowanceSettingsSave model)
        {
            return await _httpService.PostAsync<ResponseEntity, AllowanceSettingsSave>($"{_baseURL}/SaveEmployeeSettings", model);
        }
        public async Task<ResponseEntity> BatchUpdateEmployeeWiseSettings(AllowanceSettingsBatchUpdate model)
        {
            return await _httpService.PostAsync<ResponseEntity, AllowanceSettingsBatchUpdate>($"{_baseURL}/BatchUpdateEmployeeSettings", model);
        }
        public async Task<ResponseEntity> SaveDefaultSettings(AllowanceSettingsBatchUpdate model)
        {
            return await _httpService.PostAsync<ResponseEntity, AllowanceSettingsBatchUpdate>($"{_baseURL}/SaveDefaultSettings", model);
        }
        

            async Task<AllowanceSettingsBatchUpdate> IOffdayWageSettingsService.GetDefaultSettingsDetails()
        {
            return await _httpService.GetAsync<AllowanceSettingsBatchUpdate>($"{_baseURL}/GetDefaultSettingsDetails");
        }

        public async Task<DataSet> GetSettingsEmployeeList_Excel(int allowanceCode, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetSettingsEmployeeList_Excel?allowanceCode={allowanceCode}", filter);
        }
    }
}
