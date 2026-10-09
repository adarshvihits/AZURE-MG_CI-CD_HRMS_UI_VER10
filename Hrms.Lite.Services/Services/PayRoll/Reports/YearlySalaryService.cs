using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.PayRoll.Reports;
using Hrms.Lite.Shared.PayRoll.Reports;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.PayRoll.Reports
{
   public class YearlySalaryService: IYearlySalayService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public YearlySalaryService(IServiceBase httpService)
        {
            _baseURL = "YearlySalary";
            _httpService = httpService;
        }
        public async Task<YearlySalary> GetYearlySalaryDetails(YearlySalary model)
        {
            return await _httpService.PostAsync<YearlySalary, YearlySalary>($"{_baseURL}/GetDetails", model);
        }
    }
}
