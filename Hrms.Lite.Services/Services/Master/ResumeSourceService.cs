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
    public class ResumeSourceService : IResumeSourceService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public ResumeSourceService(IServiceBase httpService)
        {
            _baseURL = "ResumeSource";
            _baseURL1 = "Common";
            _httpService = httpService;
        }

        public Task<ResponseEntity> ApproveOrReject(ResumeSources model)
        {
            return _httpService.PostAsync<ResponseEntity, ResumeSources>($"{_baseURL}/ApproveOrReject", model);
        }

        public Task<ResponseEntity> Create(ResumeSources input)
        {
            return _httpService.PostAsync<ResponseEntity, ResumeSources>($"{_baseURL}/Create", input);
        }

        public async Task<ResponseEntity> Delete(ResumeSources model)
        {
            return await _httpService.PostAsync<ResponseEntity, ResumeSources>($"{_baseURL}/Delete", model);

        }

        public Task<ResumeSources> GetDetails(Guid ResumeSourceGI, Guid LogGI, string Type)
        {
            return _httpService.GetAsync<ResumeSources>($"{_baseURL}/GetResumeSourceDetails?ResumeSourceGI={ResumeSourceGI}&LogGI={LogGI}&Type={Type}");
        }

        public async Task<ResumeSources> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<ResumeSources>($"{_baseURL}/List?TabIndex={TabIndex}");
        }

        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        {
            return _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");
        }
    }
}
