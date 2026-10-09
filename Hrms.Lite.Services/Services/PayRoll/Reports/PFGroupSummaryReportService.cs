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
  public  class PFGroupSummaryReportService: IPFGroupSummaryReportService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public PFGroupSummaryReportService(IServiceBase httpService)
        {
            _baseURL = "PFGroupSummaryReport";
            _httpService = httpService;
        }
        public async Task<PFGroupSummaryReport> GetList(int Month, int PFGroup, Filter filter)
        {
            return await _httpService.PostAsync<PFGroupSummaryReport, Filter>($"{_baseURL}/GetPFGroupSummaryReport?Month={Month}&PFGroup={PFGroup}", filter);
        }
        public async Task<DataSet> GetPFGroupSummaryReport_Excel(int Month, int PFGroup, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetPFGroupSummaryReport_Excel?Month={Month}&PFGroup={PFGroup}", filter);
        }
    }
}
