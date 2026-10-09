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
    public class LoanService : ILoanService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public LoanService(IServiceBase httpService)
        {
            _baseURL = "LoanSettings";
            _baseURL1 = "Common";
            _httpService = httpService;
        }

        public Task<ResponseEntity> ApproveOrReject(LoanSettings model)
        {
            return _httpService.PostAsync<ResponseEntity, LoanSettings>($"{_baseURL}/ApproveOrReject", model);
        }

        public Task<ResponseEntity> Create(LoanSettings input)
        {
            return _httpService.PostAsync<ResponseEntity, LoanSettings>($"{_baseURL}/Create", input);
        }

        public Task<ResponseEntity> Delete(LoanSettings model)
        {
            return _httpService.PostAsync<ResponseEntity, LoanSettings>($"{_baseURL}/Delete", model);
        }

        public Task<LoanSettings> GetLoanDetails(Guid LoanGI, Guid LogGI, string Type)
        {
            return _httpService.GetAsync<LoanSettings>($"{_baseURL}/LoanSettingsDetails?LoanSettingsGI={LoanGI}&LogGI={LogGI}&Type={Type}");
        }

        public async Task<LoanSettings> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<LoanSettings>($"{_baseURL}/List?TabIndex={TabIndex}");
        }

        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        {
            return _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");
        }
    }
}
