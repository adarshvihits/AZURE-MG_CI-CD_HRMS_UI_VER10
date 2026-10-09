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
   public class SalarySummaryReportService : ISalarySummaryReportService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public SalarySummaryReportService(IServiceBase httpService)
        {
            _baseURL = "SalaryReports";
            _httpService = httpService;
        }
        public async Task<SalaryReports> GetList(int salCode,Filter filter)
        {
            return await _httpService.PostAsync<SalaryReports, Filter>($"{_baseURL}/GetSalarySummary?SalaryPeriodCode={salCode}", filter);
        }
        public async Task<SalaryReports> GetGroupSummaryDetails(int firstgroupById, int salCode, Filter filter)
        {
            return await _httpService.PostAsync<SalaryReports,Filter>($"{_baseURL}/GetSalarySummary_GroupSummary?GROUPBY={firstgroupById}&SalaryPeriodCode={salCode}",filter);

        }

      public async Task<DataSet> GetSalarySummary_Excel(int SalaryPeriodCode, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetSalarySummary_Excel?SalaryPeriodCode={SalaryPeriodCode}", filter);
        }
    }
}
