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
  public  class PFFormExitReportService: IPFFormExitReportService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public PFFormExitReportService(IServiceBase httpService)
        {
            _baseURL = "PFFormExitReport";
            _httpService = httpService;
        }

        public async Task<PFExitReport> GetEmployeeList(int SalaryPeriod, int PfGroup, Filter filter)
        {
            return await _httpService.PostAsync<PFExitReport, Filter>($"{_baseURL}/GetPFFormExitReport?Period={SalaryPeriod}&PFGroup={PfGroup}", filter);
        }

        public async Task<DataSet> GetEmployeeList_Excel(int SalaryPeriod, int PfGroup, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetPFFormExitReport_Excel?Period={SalaryPeriod}&PFGroup={PfGroup}", filter);
        }
    }
}
