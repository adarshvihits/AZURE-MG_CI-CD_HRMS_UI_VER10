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
    public class NightAllowanceTrackingService: INightAllowanceTrackingService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public NightAllowanceTrackingService(IServiceBase httpService)
        {
            _baseURL = "NightAllowanceTracking";
            _httpService = httpService;
        }
        public async Task<NightAllowanceTracking> GetList(int MonthCode, Filter filter)
        {
            return await _httpService.PostAsync<NightAllowanceTracking, Filter>($"{_baseURL}/GetList?MonthCode={MonthCode}", filter);
        }
        public async Task<DataSet> GetList_Excel(int MonthCode, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetList_Excel?MonthCode={MonthCode}", filter);
        }
        public async Task<NightAllowanceTracking> GetTrackerDetails(int HdCode)
        {
            return await _httpService.GetAsync<NightAllowanceTracking>($"{_baseURL}/GetTrackerDetails?HdCode={HdCode}");

        }
    }
}
