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
   public class ExitQuestionnaireService:IExitQuestionnaireService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public ExitQuestionnaireService(IServiceBase httpService)
        {
            _baseURL = "ExitInterviewQuestionnaire";
            _baseURL1 = "Common";
            _httpService = httpService;
        }

        public async Task<ExitInterviewQuestionnaire> GetDetails(Guid QuestionnaireGI, Guid LogGI, string Type)
        {
            return await _httpService.GetAsync<ExitInterviewQuestionnaire>($"{_baseURL}/ExitGroupVsQuestionDetails?QuestionGI={QuestionnaireGI}&&LogGI={LogGI}&&Type={Type}");
        }

        public async Task<ExitInterviewQuestionnaire> GetQuestionnaireList(string TabIndex)
        {
            return await _httpService.GetAsync<ExitInterviewQuestionnaire>($"{_baseURL}/List?TabIndex={TabIndex}");
        }

        public async Task<ResponseEntity> Create(ExitInterviewQuestionnaire input)
        {
            return await _httpService.PostAsync<ResponseEntity, ExitInterviewQuestionnaire>($"{_baseURL}/Create",input);
        }

        public async Task<ResponseEntity> ResultSave(List<PossibleResultsClassifications> input)
        {
            return await _httpService.PostAsync<ResponseEntity, List<PossibleResultsClassifications>>($"{_baseURL}/ResultSave", input);
        }

        public async Task<ResponseEntity> Delete(Guid QuestionnaireGI)
        {
            return await _httpService.DeleteAsync<ResponseEntity>($"{_baseURL}/Delete?QuestionGI={QuestionnaireGI}");
        }

        public async Task<ResponseEntity> ApproveOrReject(ExitInterviewQuestionnaire input)
        {
            return await _httpService.PostAsync<ResponseEntity, ExitInterviewQuestionnaire>($"{_baseURL}/ApproveOrReject", input);
        }

        public async Task<ExitInterviewQuestionnaire> GetResultList()
        {
            return await _httpService.GetAsync<ExitInterviewQuestionnaire>($"{_baseURL}/GetResultList");
        }

        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        {
            return _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");
        }
    }
}
