using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.PayRoll;
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
  public  class SalaryReleaseReportService: ISalaryReleaseReportService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public SalaryReleaseReportService(IServiceBase httpService)
        {
            _baseURL = "SalaryReleaseReport";
            _httpService = httpService;
        }
        public async Task<SalaryReleaseReport> GetList(int Month,int PayrollGrp, Filter filter)
        {
            return await _httpService.PostAsync<SalaryReleaseReport, Filter>($"{_baseURL}/GetReportList?Month={Month}&PayrollGrp={PayrollGrp}", filter);
        }

        public async Task<DataSet> GetSalaryReleaseReport_Excel(int Month,int PayrollGrp, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetSalaryReleaseReport_Excel?Month={Month}&PayrollGrp={PayrollGrp}", filter);
        }

     
    }
}
