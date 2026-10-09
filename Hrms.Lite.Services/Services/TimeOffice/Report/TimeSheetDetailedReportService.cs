using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.TimeOffice.Report;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice.Report;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.TimeOffice.Report
{
   public class TimeSheetDetailedReportService: ITimeSheetDetailedReportService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public TimeSheetDetailedReportService(IServiceBase httpService)
        {
            _baseURL = "TimeSheetDetailedReport";
            _httpService = httpService;
        }
        public async Task<TimeSheetEntry> GetList(string From, string To, Filter filter)
        {
            return await _httpService.PostAsync<TimeSheetEntry, Filter>($"{_baseURL}/GetTimeSheetDetailReport?From={From}&To={To}", filter);
        }

        public async Task<DataSet> GetTimeSheetExcel(string fromDate, string toDate, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetTimeSheetDetail_Excel?From={fromDate}&To={toDate}", filter);
        }
    }
}
