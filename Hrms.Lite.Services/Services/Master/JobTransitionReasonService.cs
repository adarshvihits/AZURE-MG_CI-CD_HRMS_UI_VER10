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
    public class JobTransitionReasonService : IJobTransitionReasonService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
    public JobTransitionReasonService(IServiceBase httpService)
    {
        _baseURL = "JobTransitionReason";
            _baseURL1 = "Common";
            _httpService = httpService;
    }

        public async Task<JobTransitionReason> GetJobTransitionReasonList(string TabIndex)
        {
            return await _httpService.GetAsync<JobTransitionReason>($"{_baseURL}/List?TabIndex={TabIndex}");
        }
        public Task<ResponseEntity> Create(JobTransitionReason input)
        {
            return _httpService.PostAsync<ResponseEntity, JobTransitionReason>($"{_baseURL}/Create", input);
        }
        public Task<JobTransitionReason> GetJobTransitionReasonDetails(Guid ReasonGI, Guid LogGI, string Type)
        {
            return _httpService.GetAsync<JobTransitionReason>($"{_baseURL}/GetJobTransitionReasonDetails?ReasonGI={ReasonGI}&LogGi={LogGI}&Type={Type}");
       }
        public Task<ResponseEntity> ApproveOrReject(JobTransitionReason model)
        {
            return _httpService.PostAsync<ResponseEntity, JobTransitionReason>($"{_baseURL}/ApproveOrReject", model);
        }
        public async Task<ResponseEntity> Delete(JobTransitionReason model)
        {
            return await _httpService.PostAsync<ResponseEntity, JobTransitionReason>($"{_baseURL}/Delete", model);

        }

        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        {
            return _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");
        }
    }
}
