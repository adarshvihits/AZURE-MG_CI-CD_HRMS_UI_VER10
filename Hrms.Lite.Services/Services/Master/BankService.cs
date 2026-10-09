using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.Master;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.Master
{
    public class BankService:IBankService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public BankService(IServiceBase httpService)
        {
            _baseURL = "Bank";
            _baseURL1 = "Common";
            _httpService = httpService;
        }

        public Task<ResponseEntity> ApproveOrReject(Bank model)
        {
            return _httpService.PostAsync<ResponseEntity, Bank>($"{_baseURL}/ApproveOrReject", model);
        }

        public Task<ResponseEntity> Create(Bank input)
        {
            return _httpService.PostAsync<ResponseEntity, Bank>($"{_baseURL}/Create", input);
        }

        public Task<ResponseEntity> Delete(Bank model)
        {
            return _httpService.PostAsync<ResponseEntity, Bank>($"{_baseURL}/Delete", model);
        }

        public Task<Bank> GetBankDetails(Guid BankGI, Guid LogGI, string Type)
        {
            return _httpService.GetAsync<Bank>($"{_baseURL}/BankDetails?BankGI={BankGI}&LogGI={LogGI}&Type={Type}");
        }

        public async Task<Bank> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<Bank>($"{_baseURL}/List?TabIndex={TabIndex}");
        }

        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        {
            return _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");
        }
    }
}
