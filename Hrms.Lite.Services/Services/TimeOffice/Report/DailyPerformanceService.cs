using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.TimeOffice.Report;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice.Report;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;


namespace Hrms.Lite.Services.Services.TimeOffice.Report
{
    public class DailyPerformanceService: IDailyPerformanceService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public DailyPerformanceService(IServiceBase httpService)
        {
            _baseURL = "DailyPerformance";
            _httpService = httpService;
        }
        public async Task<DailyPerformance> DailyPerformanceEmployeeList(string Date,int NoOfPunch, Filter filter)
        {
            return await _httpService.PostAsync<DailyPerformance, Filter>($"{_baseURL}/DailyPerformanceEmployeeList?Date={Date}&NoOfPunch={NoOfPunch}", filter);
        }
        public async Task<DataSet> GetDailyPerformanceList_Excel(string Date, int NoOfPunch, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetDailyPerformanceList_Excel?Date={Date}&NoOfPunch={NoOfPunch}", filter);
        }
        public Task<DailyPerformance> GetDetails(Guid EmployeeGI, string Date)
        {
            return _httpService.GetAsync<DailyPerformance>($"{_baseURL}/GetDetails?EmployeeGI={EmployeeGI}&Date={Date}");
        }
    }
}
