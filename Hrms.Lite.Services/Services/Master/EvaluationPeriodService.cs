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
   public class EvaluationPeriodService: IEvaluationPeriodService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public EvaluationPeriodService(IServiceBase httpService)
        {
            _baseURL1 = "Common";
            _baseURL = "EvaluationPeriodMaster";
            _httpService = httpService;
        }
        public async Task<EvaluationPeriodMaster> GetEvaluationPeriodList(string TabIndex)
        {
            return await _httpService.GetAsync<EvaluationPeriodMaster>($"{_baseURL}/GetEvaluationPeriodList?TabIndex={TabIndex}");
        }
        public Task<EvaluationPeriodMaster> GetEvaluationPeriodMasterDetails(Guid EvaluationPeriodGI, Guid LogGI, string type)
        {
            return _httpService.GetAsync<EvaluationPeriodMaster>($"{_baseURL}/GetEvaluationPeriodMasterDetails?EvaluationPeriodGI={EvaluationPeriodGI}&LogGI={LogGI}&Type={type}");
        }
        public Task<ResponseEntity> Create(EvaluationPeriodMaster input)
        {
            return _httpService.PostAsync<ResponseEntity, EvaluationPeriodMaster>($"{_baseURL}/Create", input);
        }
        public Task<ResponseEntity> ApproveOrReject(EvaluationPeriodMaster model)
        {
            return _httpService.PostAsync<ResponseEntity, EvaluationPeriodMaster>($"{_baseURL}/ApproveOrReject", model);
        }
       public async Task<ResponseEntity> EvaluationPeriodDelete(Guid EvaluationPeriodGI)
        {
            return await _httpService.DeleteAsync<ResponseEntity>($"{_baseURL}/Delete?EvaluationPeriodGI={EvaluationPeriodGI}");
        }

        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        {
            return _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");
        }
    }
}
