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
    public class PTGroupService :IPTGroupService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public PTGroupService(IServiceBase httpService)
        {
            _baseURL = "PTGroup";
            _baseURL1 = "Common";
            _httpService = httpService;
        }

        public Task<ResponseEntity> ApproveOrReject(PTGroup model)
        {
            return _httpService.PostAsync<ResponseEntity, PTGroup>($"{_baseURL}/ApproveOrReject", model);
        }

        public Task<ResponseEntity> Create(PTGroup input)
        {
            return _httpService.PostAsync<ResponseEntity, PTGroup>($"{_baseURL}/Create", input);
        }

        public Task<ResponseEntity> Delete(PTGroup input)
        {
            return _httpService.PostAsync<ResponseEntity, PTGroup>($"{_baseURL}/Delete", input);
        }

        public async Task<PTGroup> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<PTGroup>($"{_baseURL}/List?TabIndex={TabIndex}");
        }

        public Task<PTGroup> GetPTGroupDetails(Guid PtGroupGI, Guid LogGI, string Type)
        {
            return _httpService.GetAsync<PTGroup>($"{_baseURL}/PTGroupDetails?PtGroupGI={PtGroupGI}&LogGI={LogGI}&Type={Type}");
        }
        public async Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        {
            return await _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");
        }

    }
}
