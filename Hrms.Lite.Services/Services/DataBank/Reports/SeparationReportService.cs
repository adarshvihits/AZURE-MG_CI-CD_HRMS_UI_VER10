using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank.Reports;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank.Reports
{
    public class SeparationReportService : ISeparationReportService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public SeparationReportService(IServiceBase httpService)
        {
            _baseURL = "JoiningAndSeperationReport";
            _httpService = httpService;
        }
        public async Task<SeparationReport> GetSeparationReport(string fromDate, string toDate, Filter filter)
        {
            return await _httpService.PostAsync<SeparationReport,Filter>($"{_baseURL}/GetSeparationReport?fromDate={fromDate}&toDate={toDate}", filter);
        }
        public async Task<SeparationReport> GetSeparationGroupSummary(int type, string fromDate, string toDate)
        {
            return await _httpService.GetAsync<SeparationReport>($"{_baseURL}/GetSeparationGroupSummary?type=" + type + "&fromDate=" + fromDate + "&toDate=" + toDate);
        }

        public async Task<DataSet> GetSeparationReport_Excel(string FromDate, string ToDate, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetSeparationReport_Excel?fromDate={FromDate}&toDate={ToDate}", filter);
        }
    }
}
