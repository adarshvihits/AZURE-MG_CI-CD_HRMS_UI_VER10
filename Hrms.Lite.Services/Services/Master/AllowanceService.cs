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
   public class AllowanceService : IAllowanceService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public AllowanceService(IServiceBase httpService)
        {
            _baseURL = "Allowance";
            _baseURL1 = "Common";
            _httpService = httpService;
        }
        public async Task<Allowance> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<Allowance>($"{_baseURL}/List?TabIndex={TabIndex}");
        }
        public Task<ResponseEntity> Create(Allowance input)
        {
            return _httpService.PostAsync<ResponseEntity, Allowance>($"{_baseURL}/Create", input);
        }
        public Task<Allowance> GetAllowanceDetails(Guid AllowanceGI, Guid LogGI, string Type)
        {
            return _httpService.GetAsync<Allowance>($"{_baseURL}/AllowanceDetails?AllowanceGI={AllowanceGI}&LogGi={LogGI}&Type={Type}");
        }
        public Task<Allowance> GetDeductionSettings()
        {
            return _httpService.GetAsync<Allowance>($"{_baseURL}/GetDeductionSettings");
        }
        public Task<ResponseEntity> ApproveOrReject(Allowance model)
        {
            return _httpService.PostAsync<ResponseEntity, Allowance>($"{_baseURL}/ApproveOrReject", model);
        }
        public async Task<ResponseEntity> Delete(Allowance model)
        {
            return await _httpService.PostAsync<ResponseEntity, Allowance>($"{_baseURL}/Delete", model);

        }

        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        {
            return _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");
        }
    }
}
