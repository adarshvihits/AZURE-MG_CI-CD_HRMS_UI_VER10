using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.TimeOffice;
using Hrms.Lite.Services.IServices.TimeOffice.Report;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice;
using Hrms.Lite.Shared.TimeOffice.Report;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.TimeOffice
{
    public class DailyAttendanceLogService : IDailyAttendanceLogService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public DailyAttendanceLogService(IServiceBase httpService)
        {
            _baseURL = "DailyAttendanceLog";
            _httpService = httpService;
        }

        public async Task<ResponseEntity> DailyAttendanceLogBatchUpdate(DailyAttendanceLog model)
        {
            return await _httpService.PostAsync<ResponseEntity, DailyAttendanceLog>($"{_baseURL}/DailyAttendanceLogBatchUpdate", model);
        }

        public async Task<DailyAttendanceLog> DailyAttendanceLogList(string SelectedDate, Filter filter)
        {
            return await _httpService.PostAsync<DailyAttendanceLog, Filter>($"{_baseURL}/DailyAttendanceLogList?SelectedDate={SelectedDate}", filter);

        }

        public async Task<ResponseEntity> DailyAttendanceLogSave(DailyAttendanceLog model)
        {
            return await _httpService.PostAsync<ResponseEntity, DailyAttendanceLog>($"{_baseURL}/DailyAttendanceLogSave", model);

        }

        public async Task<ResponseEntity> EmployeeWiseDailyAttendanceLogSave(DailyAttendanceLog model)
        {
            return await _httpService.PostAsync<ResponseEntity, DailyAttendanceLog>($"{_baseURL}/EmployeeWiseDailyAttendanceLogSave", model);
        }

        public async Task<DataSet> GetApplicantTrackingReport_Excel(string SelectedDate, Filter filter)
        {
            return await _httpService.PostAsync<DataSet,Filter>($"{_baseURL}/DailyAttendanceLogList_Excel?SelectedDate={SelectedDate}", filter);
        }
    }
}
