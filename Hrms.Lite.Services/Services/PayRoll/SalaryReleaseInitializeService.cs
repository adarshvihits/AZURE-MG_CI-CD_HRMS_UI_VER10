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
 public  class SalaryReleaseInitializeService: ISalaryReleaseInitializeService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public SalaryReleaseInitializeService(IServiceBase httpService)
        {
            _baseURL = "SalaryReleaseInitialize";
            _httpService = httpService;
        }
        public async Task<SalaryReleaseInitialize> GetList(int Month)
        {
            return await _httpService.GetAsync<SalaryReleaseInitialize>($"{_baseURL}/GetSalaryReleaseInitializeList?Month={Month}");
        }
        public Task<ResponseEntity> Create(SalaryReleaseInitialize input)
        {
            return _httpService.PostAsync<ResponseEntity, SalaryReleaseInitialize>($"{_baseURL}/Save", input);
        }
    }
}
