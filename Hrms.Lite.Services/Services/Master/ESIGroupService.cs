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
    public class ESIGroupService:IESIGroupService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public ESIGroupService(IServiceBase httpService)
        {
            _baseURL = "ESIGroup";
            _baseURL1 = "Common";
            _httpService = httpService;
        }

        public Task<ResponseEntity> ApproveOrReject(ESIGroup model)
        {
            return _httpService.PostAsync<ResponseEntity, ESIGroup>($"{_baseURL}/ApproveOrReject", model);
        }

        public Task<ResponseEntity> Create(ESIGroup input)
        {
            return _httpService.PostAsync<ResponseEntity, ESIGroup>($"{_baseURL}/Create", input);
        }

        public Task<ResponseEntity> Delete(ESIGroup input)
        {
            return _httpService.PostAsync<ResponseEntity, ESIGroup>($"{_baseURL}/Delete", input);
        }

        public Task<ESIGroup> GetESIGroupDetails(Guid ESIGroupGI, Guid LogGI, string Type)
        {
            return _httpService.GetAsync<ESIGroup>($"{_baseURL}/ESIGroupDetails?ESIGroupGI={ESIGroupGI}&LogGI={LogGI}&Type={Type}");
        }

        public async Task<ESIGroup> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<ESIGroup>($"{_baseURL}/List?TabIndex={TabIndex}");
        }

        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        {
            return _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");
        }
    }
}
