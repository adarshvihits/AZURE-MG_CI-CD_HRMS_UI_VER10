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
   public class AccountsHeadService:IAccountsHeadService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;

        private readonly IServiceBase _httpService;
        public AccountsHeadService(IServiceBase httpService)
        {
            _baseURL = "AccountsHead";
            _baseURL1 = "Common";

            _httpService = httpService;
        }

        public Task<ResponseEntity> ApproveOrReject(AccountsHead model)
        {
            return _httpService.PostAsync<ResponseEntity, AccountsHead>($"{_baseURL}/ApproveOrReject", model);
        }

        public Task<ResponseEntity> Create(AccountsHead input)
        {
            return _httpService.PostAsync<ResponseEntity, AccountsHead>($"{_baseURL}/Create", input);
        }

        public Task<ResponseEntity> Delete(AccountsHead model)
        {
            return _httpService.PostAsync<ResponseEntity, AccountsHead>($"{_baseURL}/Delete", model);
        }

        public async Task<AccountsHead> GetAccountsDetails(Guid AccountsGI, Guid LogGI, string Type)
        {
            return await _httpService.GetAsync<AccountsHead>($"{_baseURL}/AccountsHeadDetails?AccountsHeadGI={AccountsGI}&&LogGI={LogGI}&&Type={Type}");
        }
  

        public async Task<AccountsHead> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<AccountsHead>($"{_baseURL}/List?TabIndex={TabIndex}");
        }

        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        {
            return _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");
        }
    }
}
