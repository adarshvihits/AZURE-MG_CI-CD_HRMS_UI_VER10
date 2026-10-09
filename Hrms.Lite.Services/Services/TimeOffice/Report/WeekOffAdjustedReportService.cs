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
   public  class WeekOffAdjustedReportService : IWeekOffAdjustedReportService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public WeekOffAdjustedReportService(IServiceBase httpService)

        {
            _baseURL = "WeekOffAdjustedReport";
            _httpService = httpService;
        }



        public async Task<WeekOffAdjustedReport> GetWeekOffAdjustedReport(int Month, Filter Filter)
        {
            return await _httpService.PostAsync<WeekOffAdjustedReport, Filter>($"{_baseURL}/GetWeekOffAdjustedReport?Month={Month}", Filter);
        }



        public async Task<DataSet> WeekOffAdjustedReport_Excel(int Month, Filter Filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetWeekOffAdjustedReport_Excel?Month={Month}", Filter);
        }

    }
}
