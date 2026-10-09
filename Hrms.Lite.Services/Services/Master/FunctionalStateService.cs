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
    public class FunctionalStateService : IFunctionalStateService
    {

        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public FunctionalStateService(IServiceBase httpService)
        {
            _baseURL = "FunctionalState";
            _baseURL1 = "Common";
            _httpService = httpService;
        }

        public Task<ResponseEntity> Create(FunctionalState input)
        {
            return _httpService.PostAsync<ResponseEntity, FunctionalState>($"{_baseURL}/Create", input);
        }

        public async Task<FunctionalState> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<FunctionalState>($"{_baseURL}/list?TabIndex={TabIndex}");
        }

        public Task<FunctionalState> GetFunctionalStateDetails(Guid LogGI, Guid FunctionalStateGI, string Type)
        {
            return _httpService.GetAsync<FunctionalState>($"{_baseURL}/GetFunctionalStateDetails?LogGI={LogGI}&FunctionalStateGI={FunctionalStateGI}&Type={Type}");
        }

        public Task<ResponseEntity> ApproveOrReject(FunctionalState input)
        {
            return _httpService.PostAsync<ResponseEntity, FunctionalState>($"{_baseURL}/ApproveOrReject", input);
        }

        public Task<ResponseEntity> Delete(FunctionalState input)
        {
            return _httpService.PostAsync<ResponseEntity, FunctionalState>($"{_baseURL}/Delete", input);
        }
        public async Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        {
            return await _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");
        }
    }
}
