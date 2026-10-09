using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.PayRoll.Reports;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.PayRoll.Reports
{
    public class IncentivePostingReportService: IIncentivePostingReportService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public IncentivePostingReportService(IServiceBase httpService)
        {
            _baseURL = "IncentivePostingReport";
            _httpService = httpService;
        }
        public async Task<IncentivePostingReport> GetIncentivePostingReport(int Month, Filter filter)
        {
            return await _httpService.PostAsync<IncentivePostingReport, Filter>($"{_baseURL}/GetIncentivePostingReport?Month={Month}", filter);
        }
        public async Task<DataSet> GetIncentivePostingReport_Excel(int Month, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetIncentivePostingReport_Excel?Month={Month}", filter);
        }
    }
}