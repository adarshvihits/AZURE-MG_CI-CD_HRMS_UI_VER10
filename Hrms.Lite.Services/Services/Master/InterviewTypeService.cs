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
    public class InterviewTypeService:IInterviewTypeService
    {

        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public InterviewTypeService(IServiceBase httpService)
        {
            _baseURL = "InterviewType";
            _baseURL1 = "Common";
            _httpService = httpService;
        }
        public async Task<InterviewType> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<InterviewType>($"{_baseURL}/List?TabIndex={TabIndex}");
        }
        public Task<ResponseEntity> Save(InterviewType Input)
        {
            return _httpService.PostAsync<ResponseEntity, InterviewType>($"{_baseURL}/Create", Input);
        }
        public Task<InterviewType> GetInterviewTypeDetails(Guid InterviewTypeGI, Guid LogGI, string Type)
        {
            return _httpService.GetAsync<InterviewType>($"{_baseURL}/InterviewTypeDetails?InterviewTypeGI={InterviewTypeGI}&LogGI={LogGI}&Type={Type}");
        }
        public Task<ResponseEntity> ApproveOrReject(InterviewType model)
        {
            return _httpService.PostAsync<ResponseEntity, InterviewType>($"{_baseURL}/ApproveOrReject", model);
        }
        public Task<ResponseEntity> Delete(InterviewType Input)
        {
            return _httpService.PostAsync<ResponseEntity, InterviewType>($"{_baseURL}/Delete", Input);
        }
        public async Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        {
            return await _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");
        }
    }
}
