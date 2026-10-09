using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.TDS.Reports;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TDS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.TDS.Reports
{
    public class TDSSlipService: ITDSSlipService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public TDSSlipService(IServiceBase httpService)
        {
            _baseURL = "TDSSlip";
            _baseURL1 = "Common";
            _httpService = httpService;
        }
        public async Task<TDSSlip> GetMonths(int AssessmentYearCode)
        {
            return await _httpService.GetAsync<TDSSlip>($"{_baseURL}/GetMonths?AssessmentYearCode={AssessmentYearCode}");
        }
        public async Task<TDSSlip> GetEmployeeList(int SalaryPeriodCode, Filter filter)
        {
            return await _httpService.PostAsync<TDSSlip, Filter>($"{_baseURL}/GetEmployeeList?SalaryPeriodCode={SalaryPeriodCode}", filter);
        }

        public async Task<Url> GetUrl()
        {
            return await _httpService.GetAsync<Url>($"{_baseURL1}/GetAPIUrl");
        }

        public async Task<Url> GetTDSSlip()
        {
            return await _httpService.GetAsync<Url>($"{_baseURL1}/GetPayslipActive");
        }
    }
}
