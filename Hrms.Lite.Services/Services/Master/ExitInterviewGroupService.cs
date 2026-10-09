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
    public class ExitInterviewGroupService : IExitInterviewGroupService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public ExitInterviewGroupService(IServiceBase httpService)
        {
            _baseURL = "ExitInterviewGroup";
            _baseURL1= "Common";
            _httpService = httpService;
        }

        public Task<ResponseEntity> ApproveOrReject(ExitInterviewGroup model)
        {
            return _httpService.PostAsync<ResponseEntity, ExitInterviewGroup>($"{_baseURL}/ApproveOrReject", model);
        }

        public Task<ResponseEntity> Create(ExitInterviewGroup input)
        {
            return _httpService.PostAsync<ResponseEntity, ExitInterviewGroup>($"{_baseURL}/Create", input);
        }

        public Task<ResponseEntity> Delete(ExitInterviewGroup model)
        {
            return _httpService.PostAsync<ResponseEntity, ExitInterviewGroup>($"{_baseURL}/Delete", model);
        }

        public Task<ExitInterviewGroup> GetinterviewGroupDetails(Guid InterviewGruopGI, Guid LogGI, string Type)
        {
            return _httpService.GetAsync<ExitInterviewGroup>($"{_baseURL}/ExitGroupDetails?GroupGI={InterviewGruopGI}&LogGI={LogGI}&Type={Type}");
            
        }

        public async Task<ExitInterviewGroup> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<ExitInterviewGroup>($"{_baseURL}/List?TabIndex={TabIndex}");
        }

        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        {
            return _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");

        }
    }
}
