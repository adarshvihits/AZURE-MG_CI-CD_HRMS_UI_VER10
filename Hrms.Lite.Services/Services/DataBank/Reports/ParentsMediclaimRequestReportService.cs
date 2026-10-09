using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank.Reports;
using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using System;
using System.Data;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank.Reports
{
    public class ParentsMediclaimRequestReportService : IParentsMediclaimRequestReportService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public ParentsMediclaimRequestReportService(IServiceBase httpService)
        {
            _baseURL = "ParentsMediclaimRequestReport";
            _baseURL1 = "Common";
            _httpService = httpService;

        }


        async Task<ParentsMediclaimRequestReport> IParentsMediclaimRequestReportService.GetList(int FinancialYearcode, Filter filterdata)
        {
            return await _httpService.PostAsync<ParentsMediclaimRequestReport, Filter>($"{_baseURL}/GetList?FinancialYearcode={FinancialYearcode}", filterdata);
        }


        public async Task<DataSet> GetList_Excel(int FinancialYearcode, Filter Filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetList_Excel?FinancialYearcode={FinancialYearcode}", Filter);
        }
      
    }
}
