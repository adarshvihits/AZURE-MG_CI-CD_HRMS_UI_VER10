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
    public class LessHourReportService : ILessHourReportService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public LessHourReportService(IServiceBase httpService)
        {
            _baseURL = "LessHourReport";
            _httpService = httpService;
        }

        public async Task<LessHourReport> LessHourReportList(string FromDate, string ToDate, Filter filter)
        {
            return await _httpService.PostAsync<LessHourReport, Filter>($"{_baseURL}/GetLessHourReportList?FromDate={FromDate}&ToDate={ToDate}", filter);
        }
        public async Task<DataSet> GetLessHourReportList_Excel(string FromDate, string ToDate, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetLessHourReportList_Excel?FromDate={FromDate}&ToDate={ToDate}", filter);
        }

    }
}
