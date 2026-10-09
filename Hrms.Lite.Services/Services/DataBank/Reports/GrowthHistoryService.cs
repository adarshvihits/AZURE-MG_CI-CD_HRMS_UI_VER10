using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank.Reports;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank.Reports
{
    public class GrowthHistoryService: IGrowthHistoryService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public GrowthHistoryService(IServiceBase httpService)
        {
            _baseURL = "GrowthHistory";
            _httpService = httpService;
        }
        public async Task<EmployeeProfile> GetEmployeeList(int Tabindex, Filter filter)
        {
            return await _httpService.PostAsync<EmployeeProfile, Filter>($"{_baseURL}/GetEmployeeList?Tabindex={Tabindex}", filter);
        }
        public async Task<EmployeeProfile> GetGrowthHistory(Guid EmployeeGI)
        {
            return await _httpService.GetAsync<EmployeeProfile>($"{_baseURL}/GetGrowthHistory?EmployeeGI={EmployeeGI}");
        }
        public async Task<JobTransition> GetGrowthHistoryDetails(Guid EmployeeGI, Guid HistoryGI)
        {
            return await _httpService.GetAsync<JobTransition>($"{_baseURL}/GetGrowthHistoryDetails?EmployeeGI={EmployeeGI}&HistoryGI={HistoryGI}");
        }

      public async Task<DataSet> GetEmployeeList_Excel(int TabIndex, Filter filterdata)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetEmployeeList_Excel?Tabindex={TabIndex}", filterdata);
        }
    }
}
