using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.TrainingModule;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TrainingModule;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.TrainingModule
{
   public class AttendanceEntryMobilePunchService: IAttendanceEntryMobilePunchService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public AttendanceEntryMobilePunchService(IServiceBase httpService)
        {
            _baseURL = "AttendanceEntryMobilePunch";
            _httpService = httpService;
        }
        public async Task<AttendanceEntryMobilePunch> GetScheduleList(int ScheduleCode, Filter filter, int radiomode,int radio1)
        {
            return await _httpService.PostAsync<AttendanceEntryMobilePunch, Filter>($"{_baseURL}/GetScheduleList?ScheduleCode={ScheduleCode}&radiomode={radiomode}&radio1={radio1}", filter);
        }

        public async Task<ResponseEntity> Save(int ScheduleCode, AttendanceEntryMobilePunch model, int radiomode)
        {
            return await _httpService.PostAsync<ResponseEntity, AttendanceEntryMobilePunch>($"{_baseURL}/Save?ScheduleCode={ScheduleCode}&radiomode={radiomode}", model);
        }

        public async Task<AttendanceEntryMobilePunch> GetTopicsPopUp(int ScheduleCode, int EmployeeCode)
        {
            return await _httpService.GetAsync<AttendanceEntryMobilePunch>($"{_baseURL}/GetTopicsPopUp?ScheduleCode={ScheduleCode}&EmployeeCode={EmployeeCode}");
        }
    }
}
