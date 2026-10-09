using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.TimeOffice.Report;
using Hrms.Lite.Shared.TimeOffice.Report;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.TimeOffice.Report
{
    public class MobilePunchingLogReportService : IMobilePunchingLogReportService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public MobilePunchingLogReportService(IServiceBase httpService)
        {
            _baseURL = "MobilePunchingLog";
            _httpService = httpService;
        }

        public async Task<ReaderLogReport> GetMobileLogListReport(ReaderLogReport ReaderLog)
        {
            return await _httpService.PostAsync<ReaderLogReport, ReaderLogReport>($"{_baseURL}/GetMobileLogListReport", ReaderLog);
        }
        public async Task<DataSet> GetMobileLogListReport_Excel(ReaderLogReport ReaderLog)
        {
            return await _httpService.PostAsync<DataSet, ReaderLogReport>($"{_baseURL}/GetMobileLogListReport_Excel", ReaderLog);

        }
    }
}
