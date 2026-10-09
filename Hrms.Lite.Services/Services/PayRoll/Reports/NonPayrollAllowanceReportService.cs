using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.PayRoll.Reports;
using Hrms.Lite.Shared.PayRoll.Reports;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.Master;

namespace Hrms.Lite.Services.Services.PayRoll.Reports
{
    public class NonPayrollAllowanceReportService: INonPayrollAllowanceReportService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public NonPayrollAllowanceReportService(IServiceBase httpService)
        {
            _baseURL = "SalaryReports";
            _httpService = httpService;
        }


        public async Task<SalaryReports> GetList(SalaryReports model)
        {
            return await _httpService.PostAsync<SalaryReports, SalaryReports>($"{_baseURL}/GetMonthlyGratuityDetailedReport", model);
        }
        public async Task<SalaryReports> GetPopUp()
        {
            return await _httpService.GetAsync<SalaryReports>($"{_baseURL}/GetGratuityReport_Allowances");

        }

        async Task<DataSet> INonPayrollAllowanceReportService.GetMonthlyGratuityDetailedReport_Excel(SalaryReports model)
        {
            return await _httpService.PostAsync<DataSet, SalaryReports>($"{_baseURL}/GetMonthlyGratuityDetailedReport_Excel", model);
        }

        public async Task<DataSet> GetNonPayrollDetailedReportHeader(int SalaryPeriodCode, SalaryReports model)
        {
            return await _httpService.PostAsync<DataSet, SalaryReports>($"{_baseURL}/GetNonPayrollDetailedReportHeader?SalaryPeriodCode={SalaryPeriodCode}", model);
        }
        public async Task<DataSet> GetNonPayrollDetailedReportData(int SalaryPeriod, SalaryReports model)
        {
            return await _httpService.PostAsync<DataSet, SalaryReports>($"{_baseURL}/GetNonPayrollDetailedReportData?SalaryPeriodCode={SalaryPeriod}", model);
        }


        //-------------NonPayroll allowance Draft-----//
        public async Task<DataSet> GetNonPayrollDetailedReportDraftData(int SalaryPeriodCode, SalaryReports model)
        {
            return await _httpService.PostAsync<DataSet, SalaryReports>($"{_baseURL}/GetNonPayrollDetailedReportDraftData?SalaryPeriodCode={SalaryPeriodCode}", model);
        }

   

        public async Task<SalaryReports> GetDraftList(SalaryReports model)
        {
            return await _httpService.PostAsync<SalaryReports, SalaryReports>($"{_baseURL}/GetMonthlyGratuityDetailedReport", model);
        }
    }
}
