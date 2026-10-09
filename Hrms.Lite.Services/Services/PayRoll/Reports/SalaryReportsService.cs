using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.PayRoll.Reports;
using Hrms.Lite.Shared.PayRoll.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.PayRoll.Reports
{
    public class SalaryReportsService: ISalaryReportsService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public SalaryReportsService(IServiceBase httpService)
        {
            _baseURL = "SalaryReports";
            _httpService = httpService;
        }
        public async Task<SalaryIntegrityReport> GetSalaryIntegrityReportList(SalaryIntegrityReport model)
        {
            return await _httpService.PostAsync<SalaryIntegrityReport, SalaryIntegrityReport>($"{_baseURL}/GetSalaryIntegrityReport", model);
        }
       public async Task<DataSet> GetSalaryIntegrityReportList_Excel(SalaryIntegrityReport model)
        {
            return await _httpService.PostAsync<DataSet, SalaryIntegrityReport>($"{_baseURL}/GetSalaryIntegrityReportList_Excel", model);
        }
        public async Task<SalaryIntegrityReport> GetSalaryIntegrityReport_CheckList()
        {
            return await _httpService.GetAsync<SalaryIntegrityReport>($"{_baseURL}/GetSalaryIntegrityReport_CheckList");
        }

        public async Task<SalaryIntegrityReport> GetSalaryIntegrityReport_Details(int MonthA, int MonthB, int EmployeeCode)
        {
            return await _httpService.GetAsync<SalaryIntegrityReport>($"{_baseURL}/GetSalaryIntegrityReport_Details?MonthA={MonthA}&MonthB={MonthB}&EmployeeCode={EmployeeCode}");
        }

     
    }
}
