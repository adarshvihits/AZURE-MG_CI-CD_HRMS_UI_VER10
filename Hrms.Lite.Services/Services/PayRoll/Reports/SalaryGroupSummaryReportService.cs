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
    public class SalaryGroupSummaryReportService: ISalaryGroupSummaryReportService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public SalaryGroupSummaryReportService(IServiceBase httpService)
        {
            _baseURL = "SalaryReports";
            _httpService = httpService;
        }
   
        public Task<System.Data.DataSet> GetDetails(int SalaryPeriod,int GroupBy, SalaryGroupSummaryReport model)
        {
            
            return _httpService.PostAsync<System.Data.DataSet, SalaryGroupSummaryReport>($"{_baseURL}/GetSalaryGrpSummaryReport?SalaryPeriod={SalaryPeriod}&GroupBy={GroupBy}", model);
        }

 
    }
}
