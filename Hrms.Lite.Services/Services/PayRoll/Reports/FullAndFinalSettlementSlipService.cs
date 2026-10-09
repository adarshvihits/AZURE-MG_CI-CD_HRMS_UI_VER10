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
    public class FullAndFinalSettlementSlipService: IFullAndFinalSettlementSlipService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public FullAndFinalSettlementSlipService(IServiceBase httpService)
        {
            _baseURL = "FullandFinalSettelment";
            _httpService = httpService;
        }

        public async Task<FullandFinalSettelment> GetList(string fromdate, string todate, Filter filter)
        {
            return await _httpService.PostAsync<FullandFinalSettelment, Filter>($"{_baseURL}/GetFullandFinalReport?fromdate={fromdate}&ToDate={todate}", filter);
        }
      public async Task<DataSet> GetFullandFinalReport_Excel(string FromDate, string ToDate, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetFullandFinalReport_Excel?fromdate={FromDate}&ToDate={ToDate}", filter);
        }
        public async Task<FullandFinalSettelment> GetEmployeeDetails(Guid EmployeeGI,Guid FinalSettlementGI, string Mode)
        {
            return await _httpService.GetAsync<FullandFinalSettelment>($"{_baseURL}/GetDetailsForPrint?EmployeeGI={EmployeeGI}&FinalSettlementGI={FinalSettlementGI}&Mode={Mode}");
        }

        public async Task<FullandFinalSettelment> FandFDetailedReport(int From, int To, Filter filter)
        {
            return await _httpService.PostAsync<FullandFinalSettelment, Filter>($"{_baseURL}/FandFDetailedReport?From={From}&To={To}", filter);
        }
    }
}
