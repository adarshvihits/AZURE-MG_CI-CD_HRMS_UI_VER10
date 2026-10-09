using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.Master;
using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.Master
{
    public class AppraisalTypeMasterService:IAppraisalTypeMasterService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;

        private readonly IServiceBase _httpService;
        public AppraisalTypeMasterService(IServiceBase httpService)
        {
            _baseURL = "AppraisalTypeMaster";
            _baseURL1 = "Common";

            _httpService = httpService;
        }
        async Task<AppraisalTypeMaster> IAppraisalTypeMasterService.GetAppraisalTypeList(string TabIndex)
        {
            return await _httpService.GetAsync<AppraisalTypeMaster>($"{_baseURL}/GetAppraisalTypeList?Tabindex={TabIndex}");
        }
        async Task<AppraisalTypeMaster> IAppraisalTypeMasterService.GetAppraisalTypeMasterDetails(Guid AppraisalTypeGI, Guid LogGI, string Type)
        {
            return await _httpService.GetAsync<AppraisalTypeMaster>($"{_baseURL}/GetAppraisalTypeMasterDetails?AppraisalTypeGI={AppraisalTypeGI}&LogGI={LogGI}&Type={Type}");
        }
        async Task<ResponseEntity> IAppraisalTypeMasterService.Create(AppraisalTypeMaster model)
        {
            return await _httpService.PostAsync<ResponseEntity, AppraisalTypeMaster>($"{_baseURL}/Create", model);
        }
        async Task<ResponseEntity> IAppraisalTypeMasterService.ApproveOrReject(AppraisalTypeMaster model)
        {
            return await _httpService.PostAsync<ResponseEntity, AppraisalTypeMaster>($"{_baseURL}/ApproveOrReject", model);
        }
        async Task<ResponseEntity> IAppraisalTypeMasterService.Delete(Guid AppraisalTypeGI)
        {
            return await _httpService.DeleteAsync<ResponseEntity>($"{_baseURL}/Delete?AppraisalTypeGI={AppraisalTypeGI}");
        }

        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        {
            return _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");
        }
    }
}
