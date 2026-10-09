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
    public class StateService : IStateService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public StateService(IServiceBase httpService)
        {
            _baseURL = "State";
            _baseURL1 = "Common";
            _httpService = httpService;
        }

        public Task<ResponseEntity> Create(State input)
        {
            return _httpService.PostAsync<ResponseEntity, State>($"{_baseURL}/Create", input);
        }
      
        public Task<State> GetStateDetails(Guid StateGI,Guid LogGI, string Type)
        {
            return _httpService.GetAsync<State>($"{_baseURL}/GetStateDetails?StateGI={StateGI}&LogGI={LogGI}&Type={Type}");

        }

        public async Task<State> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<State>($"{_baseURL}/list?TabIndex={TabIndex}");
        }

        public Task<ResponseEntity> ApproveOrReject(State model)
        {
            return _httpService.PostAsync<ResponseEntity, State>($"{_baseURL}/ApproveOrReject", model);
        }

        public async Task<ResponseEntity> Delete(State model)
        {
            return await _httpService.PostAsync<ResponseEntity, State>($"{_baseURL}/Delete", model);
        }

        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        {
            return _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");
        }
    }
}
