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
    public class AttendanceSettingService: IAttendanceSettingService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public AttendanceSettingService(IServiceBase httpService)
        {
            _baseURL = "AttendanceSetting";
            _httpService = httpService;
        }
       public async Task<AttendanceSetting> AttendanceSettingList(int GroupBy, int Classification,int TypeCode,Filter filter)
        {
            return await _httpService.PostAsync<AttendanceSetting, Filter>($"{_baseURL}/AttendanceSettingList?GroupBy={GroupBy}&Classification={Classification}&TypeCode={TypeCode}", filter);
        }
        public async Task<ResponseEntity> AttendanceSettingSave(AttendanceSetting model)
        {
            return await _httpService.PostAsync<ResponseEntity, AttendanceSetting>($"{_baseURL}/AttendanceSettingSave", model);
        }
        public async Task<AttendanceSetting> GetAttendanceSettingsEditDetails(Guid EmpGI)
        {
            return await _httpService.GetAsync<AttendanceSetting>($"{_baseURL}/AttendanceSettingDetails?EmployeeGI={EmpGI}");
        }

        public async Task<DataSet> GetList_Excel(int GroupBy, int Classification, int TypeCode, Filter model)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetList_Excel?GroupBy={GroupBy}&Classification={Classification}&TypeCode={TypeCode}", model);

        }
    }
}
