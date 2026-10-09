using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.PayRoll;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.PayRoll
{
    public class NegativeSalaryCorrectionService : INegativeSalaryCorrectionService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public NegativeSalaryCorrectionService(IServiceBase httpService)
        {
            _baseURL = "NeagativeSalary";
            _httpService = httpService;
        }
        public async Task<NegativeSalary> GetList(int SalaryPeriod, int payrollgroup, Filter filter)
        {
            return await _httpService.PostAsync<NegativeSalary, Filter>($"{_baseURL}/NegativeSalaryList?SalaryPeriod={SalaryPeriod}&payrollgroup={payrollgroup}", filter);

        }
    }
}
