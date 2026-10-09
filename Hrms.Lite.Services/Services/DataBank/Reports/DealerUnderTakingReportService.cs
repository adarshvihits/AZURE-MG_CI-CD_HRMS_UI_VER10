using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank.Reports;
using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using System;
using System.Data;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank.Reports
{
    public class DealerUnderTakingReportService : IDealerUnderTakingReportService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public DealerUnderTakingReportService(IServiceBase httpService)
        {
            _baseURL = "DealerUnderTakingReport";
            _baseURL1 = "Common";
            _httpService = httpService;

        }
        public async Task<DealerUnderTakingReport> GetDealerUnderTakingDetails(Guid EmployeeGI, string Type)
        {
            return await _httpService.GetAsync<DealerUnderTakingReport>($"{_baseURL}/GetDealerUnderTakingDetails?EmployeeGI={EmployeeGI}&&Type={Type}");
        }

        async Task<DealerUnderTakingReport> IDealerUnderTakingReportService.GetList(int Tabindex, Filter model)
        {
            return await _httpService.PostAsync<DealerUnderTakingReport, Filter>($"{_baseURL}/GetList?Tabindex={Tabindex}", model);
        }

        public async Task<DataSet> GetList_Excel(int Tabindex, Filter model)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetList_Excel?Tabindex={Tabindex}", model);
        }
    }
}
