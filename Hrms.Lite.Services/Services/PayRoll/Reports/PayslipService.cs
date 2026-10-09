using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.PayRoll.Reports;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll.Reports;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.PayRoll.Reports
{
    public class PayslipService : IPayslipService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public PayslipService(IServiceBase httpService)
        {
            _baseURL = "PaySlip";
            _baseURL1 = "Common";
            _httpService = httpService;
        }

        public async Task<PaySlip> GetMonths(int FinancialYearCode)
        {
            return await _httpService.GetAsync<PaySlip>($"{_baseURL}/GetMonths?FinancialYearCode={FinancialYearCode}");
        }
        public async Task<PaySlip> GetEmployeeList(int SalaryPeriodCode, Filter filter)
        {
            return await _httpService.PostAsync<PaySlip, Filter>($"{_baseURL}/GetEmployeeList?SalaryPeriodCode={SalaryPeriodCode}", filter);
        }

        public async Task<Url> GetUrl()
        {
            return await _httpService.GetAsync<Url>($"{_baseURL1}/GetAPIUrl");
        }

        public async Task<Url> GetPaySlip()
        {
            return await _httpService.GetAsync<Url>($"{_baseURL1}/GetPayslipActive");
        }
    }
}
