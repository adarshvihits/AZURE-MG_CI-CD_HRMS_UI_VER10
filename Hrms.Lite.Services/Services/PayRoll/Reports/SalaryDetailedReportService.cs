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
   
    public class SalaryDetailedReportService :  ISalaryDetailedReportService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public SalaryDetailedReportService(IServiceBase httpService)
        {
            _baseURL = "SalaryReports";
            _httpService = httpService;
        }
        public async Task<SalaryReports> GetList(SalaryReports model)
        {
            return await _httpService.PostAsync<SalaryReports, SalaryReports>($"{_baseURL}/GetSalaryDetailedReport",model);
        }
        public async Task<SalaryReports> GetPopUp()
        {
            return await _httpService.GetAsync<SalaryReports>($"{_baseURL}/GetSalaryDetailed_Allowances_and_Deductions");

        }

        public async Task<DataSet> GetSalaryDetailedReport_Excel(SalaryReports model)
        {
            return await _httpService.PostAsync<DataSet, SalaryReports>($"{_baseURL}/GetSalaryDetailedReport_Excel", model);

        }

        public async Task<DataSet> GetSalaryDetailedHeaders(SalaryReports model,int SalaryPeriod,  string allowance, string deduction)
        {
            return await _httpService.PostAsync<DataSet, SalaryReports>($"{_baseURL}/GetSalaryDetailedReportHeaders?SalaryPeriodCode={SalaryPeriod}&allowance={allowance}&deduction={deduction}", model);
        }
        public async Task<DataSet> GetSalaryDetailedReport(int SalaryPeriod, SalaryReports model, string allowance, string deduction)
        {
            return await _httpService.PostAsync<DataSet, SalaryReports>($"{_baseURL}/GetSalaryDetailedReportDatas?SalaryPeriodCode={SalaryPeriod}&allowance={allowance}&deduction={deduction}", model);
        }



    }
}
