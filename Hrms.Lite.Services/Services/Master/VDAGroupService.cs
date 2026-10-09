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
    public class VDAGroupService:IVDAGroupService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;



        private readonly IServiceBase _httpService;
        public VDAGroupService(IServiceBase httpService)
        {
            _baseURL = "VDAGroup";
            _baseURL1 = "Common";

            _httpService = httpService;
        }

        public Task<ResponseEntity> ApproveOrReject(VDAGroup model)
        {
            return _httpService.PostAsync<ResponseEntity, VDAGroup>($"{_baseURL}/ApproveOrReject", model);
        }

        public Task<ResponseEntity> Create(VDAGroup input)
        {
            return _httpService.PostAsync<ResponseEntity, VDAGroup>($"{_baseURL}/Create", input);

        }

        public Task<ResponseEntity> Delete(VDAGroup model)
        {
            return _httpService.PostAsync<ResponseEntity, VDAGroup>($"{_baseURL}/Delete", model);
        }

        public async Task<VDAGroup> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<VDAGroup>($"{_baseURL}/List?TabIndex={TabIndex}");
        }

        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        {
            return _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");
        }

        public async Task<VDAGroup> VDAGroupDetails(Guid VDAGroupGI, Guid LogGI, string Type)
        {
            return await _httpService.GetAsync<VDAGroup>($"{_baseURL}/VDAGroupDetails?VDAGroupGI={VDAGroupGI}&LogGI={LogGI}&Type={Type}");

        }
    }
}
