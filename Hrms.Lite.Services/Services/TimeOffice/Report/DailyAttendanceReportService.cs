using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.TimeOffice.Report;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.TimeOffice.Report
{
    public class DailyAttendanceReportService : IDailyAttendanceReportService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public DailyAttendanceReportService(IServiceBase httpService)
        {
            _baseURL = "DailyAttendanceEntry";
            _httpService = httpService;
        }

        public async Task<DailyAttendanceEntry> GetDailyAttendanceReportList(DailyAttendanceEntry model)
        {
            return await _httpService.PostAsync<DailyAttendanceEntry, DailyAttendanceEntry>($"{_baseURL}/DailyAttendanceReport", model);


        }
        public async Task<DataSet> DailyAttendanceReport_Excel(DailyAttendanceEntry model)
        {
            return await _httpService.PostAsync<DataSet, DailyAttendanceEntry>($"{_baseURL}/DailyAttendanceReport_Excel", model);
        }
    }
}
