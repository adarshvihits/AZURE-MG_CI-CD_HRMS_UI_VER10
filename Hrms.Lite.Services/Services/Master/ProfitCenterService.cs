using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.Master;
using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.Master
{
    public class ProfitCenterService : IProfitCenterService
    {

        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public ProfitCenterService(IServiceBase httpService)
        {
            _baseURL = "ProfitCenter";
            _baseURL1 = "Common";
            _httpService = httpService;
        }

        public Task<ResponseEntity> Create(ProfitCenter input)
        {
            return _httpService.PostAsync<ResponseEntity, ProfitCenter>($"{_baseURL}/Create", input);
        }

        public async Task<ProfitCenter> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<ProfitCenter>($"{_baseURL}/list?TabIndex={TabIndex}");
        }

        public Task<ProfitCenter> GetProfitCenterDetails(Guid LogGI, Guid ProfitCenterGI, string Type)
        {
            return _httpService.GetAsync<ProfitCenter>($"{_baseURL}/GetProfitCenterDetails?LogGI={LogGI}&ProfitCenterGI={ProfitCenterGI}&Type={Type}");
        }

        public Task<ResponseEntity> ApproveOrReject(ProfitCenter input)
        {
            return _httpService.PostAsync<ResponseEntity, ProfitCenter>($"{_baseURL}/ApproveOrReject", input);
        }

        public Task<ResponseEntity> Delete(ProfitCenter input)
        {
            return _httpService.PostAsync<ResponseEntity, ProfitCenter>($"{_baseURL}/Delete", input);
        }
        public async Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        {
            return await _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");
        }
    }
}
