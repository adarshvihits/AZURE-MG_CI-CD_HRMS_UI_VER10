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
   public class PFGroupService:IPFGroupService
    {

        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public PFGroupService(IServiceBase httpService)
        {
            _baseURL = "PFGroup";
            _baseURL1 = "Common";
            _httpService = httpService;
        }
        public async Task<PFGroup> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<PFGroup>($"{_baseURL}/List?TabIndex={TabIndex}");
        }
        public Task<ResponseEntity> Save(PFGroup Input)
        {
            return _httpService.PostAsync<ResponseEntity, PFGroup>($"{_baseURL}/Create", Input);
        }
        public Task<PFGroup> GetPFGroupDetails(Guid PFGroupGI, Guid LogGI, string Type)
        {
            return _httpService.GetAsync<PFGroup>($"{_baseURL}/PFGroupDetails?PFGroupGI={PFGroupGI}&LogGI={LogGI}&Type={Type}");
        }
        public Task<ResponseEntity> ApproveOrReject(PFGroup model)
        {
            return _httpService.PostAsync<ResponseEntity, PFGroup>($"{_baseURL}/ApproveOrReject", model);
        }
        public Task<ResponseEntity> Delete(PFGroup Input)
        {
            return _httpService.PostAsync<ResponseEntity, PFGroup>($"{_baseURL}/Delete", Input);
        }
        public async Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        {
            return await _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");
        }
    }
}
