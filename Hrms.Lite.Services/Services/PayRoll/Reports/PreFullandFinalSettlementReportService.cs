using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.PayRoll.Reports;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.PayRoll.Reports
{
 public  class PreFullandFinalSettlementReportService: IPreFullandFinalSettlementReportService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public PreFullandFinalSettlementReportService(IServiceBase httpService)
        {
            _baseURL = "PreFullandFinalSettlementReport";
            _httpService = httpService;
        }

        public async Task<PreFullandFinalSettlement> FandFDetailedReport(int Month,Filter filter)
        {
            return await _httpService.PostAsync<PreFullandFinalSettlement, Filter>($"{_baseURL}/FandFDetailedReport?Month={Month}", filter);
        }
        public async Task<DataSet> FandFDetailedReport_EXCEL(int Month, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/FandFDetailedReport_EXCEL?Month=" + $"{Month}", filter);

        }
    }
}
