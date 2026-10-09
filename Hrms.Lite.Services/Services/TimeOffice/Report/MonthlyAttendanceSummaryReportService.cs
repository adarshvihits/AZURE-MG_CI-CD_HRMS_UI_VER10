using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.TimeOffice.Report;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice;
using Hrms.Lite.Shared.TimeOffice.Report;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.TimeOffice.Report
{
    public class MonthlyAttendanceSummaryReportService: IMonthlyAttendanceSummaryReportService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public MonthlyAttendanceSummaryReportService(IServiceBase httpService)
        {
            _baseURL = "MonthlyAttendanceSummaryReport";
            _httpService = httpService;
        }
        async Task<MonthlyAttendanceSummaryReport> IMonthlyAttendanceSummaryReportService.MonthlyAttendanceSummReport(int Month,Filter filterdata)
        {
            return await _httpService.PostAsync<MonthlyAttendanceSummaryReport, Filter>($"{_baseURL}/MonthlyAttendanceSummaryReportList?Month={Month}", filterdata);
        }
        public async Task<DataSet> MonthlyAttendanceSummaryReport_Excel(int Month, Filter filterdata)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/MonthlyAttendanceSummaryReport_Excel?Month={Month}", filterdata);
        }
    }
}
