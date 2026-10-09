using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank.Reports;
using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank.Reports
{
    public class ExitAnalysisReportService: IExitAnalysisReportService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public ExitAnalysisReportService(IServiceBase httpService)
        {
            _baseURL = "ExitAnalysisReport";
            _httpService = httpService;
        }

        public async Task<ExitAnalysisReport> GetList(string FromDate, string ToDate, string Type, Filter filter)
        {
            return await _httpService.PostAsync<ExitAnalysisReport, Filter>($"{_baseURL}/GetList?FromDate={FromDate}&ToDate={ToDate}&Type={Type}", filter);
        }

        public async Task<ExitAnalysisReport> GetPopupList(string FromDate, string ToDate, Guid GI, string Type)
        {
            return await _httpService.GetAsync<ExitAnalysisReport>($"{_baseURL}/GetPopupList?FromDate={FromDate}&ToDate={ToDate}&GI={GI}&Type={Type}");
        }

        public async Task<DataSet> ExitAnalysisReport_Excel(Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/ExitAnalysisReport_Excel", filter);
        }
    }
}
