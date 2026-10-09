using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.TimeOffice;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.TimeOffice
{
    public class OndutyWfhSettingsService : IOndutyWfhSettingsService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public OndutyWfhSettingsService(IServiceBase httpService)
        {
            _baseURL = "OndutyWfhSettings";
            _httpService = httpService;
        }
        public async Task<OndutyWfhSettings> GetOdWfhEmployeeList(Filter Filter)
        {
            return await _httpService.PostAsync<OndutyWfhSettings, Filter>($"{_baseURL}/GetOdWfhEmployeeListDetails", Filter);
        }
        public async Task<OndutyWfhSettings> GetOtherSettingsDetails(int LeaveGroup)
        {
            return await _httpService.GetAsync<OndutyWfhSettings>($"{_baseURL}/GetOtherSettings?LeaveGroup={LeaveGroup}");
        }
        public async Task<ResponseEntity> EmployeeWiseOdWfhLimitSave(OndutyWfhSettings model)
        {
            return await _httpService.PostAsync<ResponseEntity, OndutyWfhSettings>($"{_baseURL}/EmployeeWiseOdWfhLimitSave", model);
        }
        public Task<ResponseEntity> Save(OndutyWfhSettings input)
        {
            return _httpService.PostAsync<ResponseEntity, OndutyWfhSettings>($"{_baseURL}/OdWfhSettingsSave", input);
        }
        public async Task<DataSet> OdWfhEmployeeList_Excel(Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/OdWfhEmployeeList_Excel", filter);
        }
        public async Task<ResponseEntity<List<OndutyWfhSettings>>> ImportFromExcel(OndutyWfhSettings model)
        {
            return await _httpService.PostAsync<ResponseEntity<List<OndutyWfhSettings>>, OndutyWfhSettings>($"{_baseURL}/ImportFromExcel", model);

        }
    }
}
