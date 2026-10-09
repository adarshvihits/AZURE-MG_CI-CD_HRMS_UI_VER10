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
    public class JobDescriptionService : IJobDescriptionService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public JobDescriptionService(IServiceBase httpService)
        {
            _baseURL = "JobDescription";
            _baseURL1 = "Common";
            _httpService = httpService;
        }
        public async Task<JobDescription> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<JobDescription>($"{_baseURL}/List?TabIndex={TabIndex}");
        }
        public async Task<JobDescription> GetJobDescriptionDetails(Guid JobDescriptionGI, Guid LogGI, string Type)
        {
            return await _httpService.GetAsync<JobDescription>($"{_baseURL}/JobDescriptionDetails?JobDescriptionGI={JobDescriptionGI}&LogGI={LogGI}&Type={Type}");
        }
        public async Task<ResponseEntity> JobDescriptionSave(JobDescription model)
        {
            return await _httpService.PostAsync<ResponseEntity, JobDescription>($"{_baseURL}/Create", model);
        }
        public async Task<ResponseEntity> JobDescriptionDelete(JobDescription model)
        {
            return await _httpService.PostAsync<ResponseEntity, JobDescription>($"{_baseURL}/Delete", model);
        }
        public async Task<ResponseEntity> ApproveOrReject(JobDescription model)
        {
            return await _httpService.PostAsync<ResponseEntity, JobDescription>($"{_baseURL}/ApproveOrReject", model);
        }
        public async Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        {
            return await _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");
        }
        public async Task<ResponseEntity> GetJDVSDesignation(string DesignationCode, int? JobDescriptionCode)
        {
            return await _httpService.GetAsync<ResponseEntity>($"{_baseURL}/GetJDVSDesignation?DesignationCode={DesignationCode}&JobDescriptionCode={JobDescriptionCode}");
        }
        public async Task<JobDescription> GetMasterJobDetails(int PositionPlanCode)
        {
            return await _httpService.GetAsync<JobDescription>($"{_baseURL}/GetMasterJobDetails?PositionPlanCode={PositionPlanCode}");
        }
    }
}
