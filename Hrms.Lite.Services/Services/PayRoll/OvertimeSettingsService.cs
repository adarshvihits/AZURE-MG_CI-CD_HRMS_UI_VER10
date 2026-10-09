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
    public class OvertimeSettingsService : IOvertimeSettingsService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public OvertimeSettingsService(IServiceBase httpService)
        {
            _baseURL = "OvertimeSettings";
            _httpService = httpService;
        }
        async Task<ApplicableAllowance> IOvertimeSettingsService.GetApplicableAllowanceList()
        {
            return await _httpService.GetAsync<ApplicableAllowance>($"{_baseURL}/GetApplicableAllowanceList");
        }
        async Task<ResponseEntity> IOvertimeSettingsService.SaveApplicableAllowance(ApplicableAllowance model)
        {
            return await _httpService.PostAsync<ResponseEntity, ApplicableAllowance>($"{_baseURL}/SaveApplicableAllowance", model);
        }
        async Task<List<AllowanceSettingsEmployeeList>> IOvertimeSettingsService.GetSettingsEmployeeList(int AllowanceCode, Filter filter)
        {
            return await _httpService.PostAsync<List<AllowanceSettingsEmployeeList>, Filter>($"{_baseURL}/GetSettingsEmployeeList?AllowanceCode={AllowanceCode}", filter);
        }





        async Task<Allowance> IOvertimeSettingsService.GetBatchUpDateDetails(int AllowanceCode)
        {
            return await _httpService.GetAsync<Allowance>($"{_baseURL}/GetBatchUpdateDetails?AllowanceCode={AllowanceCode}");
        }
        public async Task<ResponseEntity> BatchUpdateEmployeeWiseSettings(AllowanceSettingsBatchUpdate model)
        {
            return await _httpService.PostAsync<ResponseEntity, AllowanceSettingsBatchUpdate>($"{_baseURL}/BatchUpdateEmployeeSettings", model);
        }
        public async Task<ResponseEntity> SaveDefaultSettings(AllowanceSettingsBatchUpdate model)
        {
            return await _httpService.PostAsync<ResponseEntity, AllowanceSettingsBatchUpdate>($"{_baseURL}/SaveDefaultSettings", model);
        }
        async Task<ResponseEntity> IOvertimeSettingsService.SaveEmployeeWiseSettings(AllowanceSettingsSave model)
        {
            return await _httpService.PostAsync<ResponseEntity, AllowanceSettingsSave>($"{_baseURL}/SaveEmployeeSettings", model);
        }
        public async Task<AllowanceSettingsBatchUpdate> GetDefaultSettingsDetails()
        {
            return await _httpService.GetAsync<AllowanceSettingsBatchUpdate>($"{_baseURL}/GetDefaultSettingsDetails");
        }
        public async Task<DataSet> GetSettingsEmployeeList_Excel(int allowanceCode, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetSettingsEmployeeList_Excel?AllowanceCode={allowanceCode}", filter);
        }
    }
}
