using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.PayRoll;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using Hrms.Lite.Shared.PayRoll.AllowanceSettings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.PayRoll
{
    public class LeaveEncashmentSettingsService: ILeaveEncashmentSettingsService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public LeaveEncashmentSettingsService(IServiceBase httpService)
        {
            _baseURL = "LeaveEncashmentSettings";
            _httpService = httpService;
        }
        public async Task<ApplicableAllowance> GetApplicableAllowanceList()
        {
            return await _httpService.GetAsync<ApplicableAllowance>($"{_baseURL}/GetApplicableAllowanceList");
        }
        public async Task<ResponseEntity> SaveApplicableAllowance(ApplicableAllowance model)
        {
            return await _httpService.PostAsync<ResponseEntity, ApplicableAllowance>($"{_baseURL}/SaveApplicableAllowance", model);
        }
        public async Task<List<AllowanceSettingsEmployeeList>> GetSettingsEmployeeList(int LeaveCode, Filter filter)
        {
            return await _httpService.PostAsync<List<AllowanceSettingsEmployeeList>, Filter>($"{_baseURL}/GetSettingsEmployeeList?LeaveCode={LeaveCode}", filter);
        }
        public async Task<Allowance> GetBatchUpDateDetails(int AllowanceCode)
        {
            return await _httpService.GetAsync<Allowance>($"{_baseURL}/GetBatchUpdateDetails?AllowanceCode={AllowanceCode}");
        }
        public async Task<ResponseEntity> BatchUpdateEmployeeWiseSettings(AllowanceSettingsBatchUpdate model)
        {
            return await _httpService.PostAsync<ResponseEntity, AllowanceSettingsBatchUpdate>($"{_baseURL}/BatchUpdateEmployeeWiseSettings", model);
        }
        public async Task<ResponseEntity> SaveDefaultSettings(AllowanceSettingsBatchUpdate model)
        {
            return await _httpService.PostAsync<ResponseEntity, AllowanceSettingsBatchUpdate>($"{_baseURL}/SaveDefaultSettings", model);
        }
       public  async Task<ResponseEntity> SaveEmployeeWiseSettings(AllowanceSettingsSave model)
        {
            return await _httpService.PostAsync<ResponseEntity, AllowanceSettingsSave>($"{_baseURL}/SaveEmployeeWiseSettings", model);
        }
        public async Task<AllowanceSettingsBatchUpdate> GetDefaultSettingsDetails()
        {
            return await _httpService.GetAsync<AllowanceSettingsBatchUpdate>($"{_baseURL}/GetDefaultSettingsDetails");
        }
    }
}
