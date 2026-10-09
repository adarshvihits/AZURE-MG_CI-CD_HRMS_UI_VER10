using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.MIS;
using Hrms.Lite.Shared.DashBoard;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.MIS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.MIS
{
    public class CompanyMISService : ICompanyMISService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public CompanyMISService(IServiceBase httpService)
        {
            _baseURL = "CompanyMIS";
            _httpService = httpService;
        }
        public async Task<CompanyMIS> GetList(int report)
        {
            return await _httpService.GetAsync<CompanyMIS>($"{_baseURL}/GetList?report={report}");
        }

        public async Task<CompanyMIS> GetPayRollGP(string Diagram)
        {
            return await _httpService.GetAsync<CompanyMIS>($"{_baseURL}/GetPayRollGP?Diagram={Diagram}");
        }

      

    }
}
