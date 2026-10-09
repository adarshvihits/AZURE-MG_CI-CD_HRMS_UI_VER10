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
   public class PayrollGroupService : IPayrollGroupService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        private readonly string _baseURL1;

        public PayrollGroupService(IServiceBase httpService)
        {
            _baseURL = "PayrollGroup";
            _baseURL1 = "Common";
            _httpService = httpService;
        }
        public async Task<PayrollGroup> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<PayrollGroup>($"{_baseURL}/List?TabIndex={TabIndex}");
        }
        public Task<PayrollGroup> GetPayrollGroupDetails(Guid PayrollGroupGI, Guid LogGI, string Type)
        {
            return _httpService.GetAsync<PayrollGroup>($"{_baseURL}/PayrollGroupDetails?PayrollGroupGI={PayrollGroupGI}&LogGi={LogGI}&Type={Type}");
        }
        public Task<ResponseEntity> Create(PayrollGroup input)
        {
            return _httpService.PostAsync<ResponseEntity, PayrollGroup>($"{_baseURL}/Create", input);
        }
        public Task<ResponseEntity> ApproveOrReject(PayrollGroup model)
        {
            return _httpService.PostAsync<ResponseEntity, PayrollGroup>($"{_baseURL}/ApproveOrReject", model);
        }
        public async Task<ResponseEntity> Delete(PayrollGroup model)
        {
            return await _httpService.PostAsync<ResponseEntity, PayrollGroup>($"{_baseURL}/Delete", model);

        }

        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        {
            return _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");
        }
    }
}
