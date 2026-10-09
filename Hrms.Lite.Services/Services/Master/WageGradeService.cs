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
   public class WageGradeService:IWageGradeService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public WageGradeService(IServiceBase httpService)
        {
            _baseURL = "WageGradeMaster";
            _baseURL1 = "Common";
            _httpService = httpService;
        }
        public async Task<WageGradeMaster> GetWageGradeList(string TabIndex)
        {
            return await _httpService.GetAsync<WageGradeMaster>($"{_baseURL}/GetWageGradeList?TabIndex={TabIndex}");
        }
        public Task<ResponseEntity> Create(WageGradeMaster Input)
        {
            return _httpService.PostAsync<ResponseEntity, WageGradeMaster>($"{_baseURL}/Create", Input);
        }
        public Task<WageGradeMaster> GetWageGradeDetails(Guid WageGradeGI, Guid LogGI, string Type)
        {
            return _httpService.GetAsync<WageGradeMaster>($"{_baseURL}/GetWageGradeMasterDetails?WageGradeGI={WageGradeGI}&LogGi={LogGI}&Type={Type}");
        }
        public Task<ResponseEntity> ApproveOrReject(WageGradeMaster model)
        {
            return _httpService.PostAsync<ResponseEntity, WageGradeMaster>($"{_baseURL}/ApproveOrReject", model);
        }
        public Task<ResponseEntity> Delete(Guid GI)
        {
            return _httpService.DeleteAsync<ResponseEntity>($"{_baseURL}/Delete?GI={GI}");
        }

        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        {
            return _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");
        }
    }
}
