using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.Master;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.Master
{
    public class CompanyVsPayrollGroupService : ICompanyVsPayrollGroupService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public CompanyVsPayrollGroupService(IServiceBase httpService)
        {
            _baseURL = "CompanyVsPayrollGroup";
            _httpService = httpService;
        }

        
        public async Task<CompanyVsPayrollGroup> GetList(int GroupCompanyCode)
        {
            return await _httpService.GetAsync<CompanyVsPayrollGroup>($"{_baseURL}/GetList?GroupCompanyCode={GroupCompanyCode}");
        }



        async Task<ResponseEntity> ICompanyVsPayrollGroupService.Save(CompanyVsPayrollGroup model)
        {
            return await _httpService.PostAsync<ResponseEntity, CompanyVsPayrollGroup>($"{_baseURL}/Save", model);

        }
    }
}
