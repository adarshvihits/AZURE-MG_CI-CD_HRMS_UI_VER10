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
    public class EmployeeWiseOffdaySettingsService: IEmployeeWiseOffdaySettingsService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public EmployeeWiseOffdaySettingsService(IServiceBase httpService)
        {
            _baseURL = "EmployeeWiseOffdaySettings";
            _httpService = httpService;
        }
        public async Task<DataSet> GetOffdaySettingsList_Excel(int Year, int ShiftType, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetOffdaySettingsList_Excel?Year={Year}&ShiftType={ShiftType}", filter);
        }
        public async Task<EmployeeWiseOffdaySettings> GetOffdaySettingsList(int? year,int ShiftType, Filter filter)
        {
            return await _httpService.PostAsync<EmployeeWiseOffdaySettings, Filter>($"{_baseURL}/GetOffdaySettingsList?Year={year}&ShiftType={ShiftType}", filter);
        }
        public async Task<EmployeeWiseOffdaySettings> GetOffdaySettingsBatchUpdateDetails(int YearCode, int ShiftType)
        {
            return await _httpService.GetAsync<EmployeeWiseOffdaySettings>($"{_baseURL}/GetOffdaySettingsBatchUpdateDetails?Year={YearCode}&ShiftType={ShiftType}");
        }
        public async Task<EmployeeWiseOffdaySettings> GetOffdaySettingsEditDetails(int YearCode, Guid EmpGI)
        {
            return await _httpService.GetAsync<EmployeeWiseOffdaySettings>($"{_baseURL}/GetOffdaySettingsEditDetails?Year={YearCode}&EmployeeGI={EmpGI}");
        }
        public async Task<ResponseEntity> BatchUpdateSave(EmployeeWiseOffdaySettings model)
        {
            return await _httpService.PostAsync<ResponseEntity, EmployeeWiseOffdaySettings>($"{_baseURL}/BatchUpdateSettings", model);
        }
        public async Task<ResponseEntity> EmployeeWiseSave(EmployeeWiseOffdaySettings model)
        {
            return await _httpService.PostAsync<ResponseEntity, EmployeeWiseOffdaySettings>($"{_baseURL}/SaveSettings", model);
        }
        public async Task<EmployeeWiseOffdaySettings> GetEmployeeOffdaySettingsDetailstPopUp(int YearCode, Guid EmpGI)
        {
            return await _httpService.GetAsync<EmployeeWiseOffdaySettings>($"{_baseURL}/GetOffdaySettingsGetDetails?Year={YearCode}&EmployeeGI={EmpGI}");
        }
    }
}
