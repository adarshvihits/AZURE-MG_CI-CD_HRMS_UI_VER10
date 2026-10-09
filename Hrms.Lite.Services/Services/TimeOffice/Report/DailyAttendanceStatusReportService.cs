using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.TimeOffice.Report;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice.Report;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.TimeOffice.Report
{
    public class DailyAttendanceStatusReportService: IDailyAttendanceStatusReportService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public DailyAttendanceStatusReportService(IServiceBase httpService)
        {
            _baseURL = "DailyAttendanceStatusReport";
            _httpService = httpService;
        }
        public Task<System.Data.DataSet> GetDetails(DailyAttendanceStatusReport model)
        {
            return _httpService.PostAsync<System.Data.DataSet, DailyAttendanceStatusReport>($"{_baseURL}/GetDetails", model);
        }
    }
}
