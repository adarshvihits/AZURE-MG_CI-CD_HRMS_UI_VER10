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
   public  class MonthlyWorkHourReportService : IMonthlyWorkHourReportService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public MonthlyWorkHourReportService(IServiceBase httpService)

        {
            _baseURL = "MonthlyWorkingHoursReport";
            _httpService = httpService;
        }



        public async Task<WorkHourDetails> GetMonthlyWorkingHoursReport(int Month, Filter Filter)
        {
            return await _httpService.PostAsync<WorkHourDetails, Filter>($"{_baseURL}/GetMonthlyWorkingHoursReport?Month={Month}", Filter);
        }


        public async Task<DataSet> GetMonthlyWorkingHoursReport_Excel(int Month, Filter Filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetMonthlyWorkingHoursReport_Excel?Month={Month}", Filter);
        }

    }
}
