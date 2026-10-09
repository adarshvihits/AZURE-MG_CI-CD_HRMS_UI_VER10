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
  public  class ESIGroupSummaryReportService: IESIGroupSummaryReportService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public ESIGroupSummaryReportService(IServiceBase httpService)
        {
            _baseURL = "ESIGroupSummaryReport";
            _httpService = httpService;
        }
        public async Task<ESIGroupSummaryReport> GetList(int Month, int ESIGroup, Filter filter)
        {
            return await _httpService.PostAsync<ESIGroupSummaryReport, Filter>($"{_baseURL}/GetESIGroupSummaryReport?Month={Month}&ESIGroup={ESIGroup}", filter);
        }
        public async Task<DataSet> GetESIGroupSummaryReport_Excel(int Month, int ESIGroup, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetESIGroupSummaryReport_Excel?Month={Month}&ESIGroup={ESIGroup}", filter);
        }
    }
}
