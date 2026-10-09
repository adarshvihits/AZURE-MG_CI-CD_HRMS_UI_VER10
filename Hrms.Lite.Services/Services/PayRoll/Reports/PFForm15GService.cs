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
   public class PFForm15GService: IPFForm15GService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public PFForm15GService(IServiceBase httpService)
        {
            _baseURL = "PFForm15GEmpList";
            _httpService = httpService;
        }

        public async Task<PF15GReport> GetEmployeeList(Filter filter)
        {
            return await _httpService.PostAsync<PF15GReport, Filter>($"{_baseURL}/GetPF15GEmpList", filter);
        }
    }
}
