using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.TrainingModule;
using Hrms.Lite.Shared.TrainingModule;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.TrainingModule
{
  public  class TrainingQuestionsService : ITrainingQuestionsService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public TrainingQuestionsService(IServiceBase httpService)
        {
            _baseURL = "TrainingQuestions";
            _baseURL1 = "Common";
            _httpService = httpService;
        }


        public async Task<TrainingQuestions> GetList(string TabIndex,int TrainingCode)
        {
            return await _httpService.GetAsync<TrainingQuestions>($"{_baseURL}/List?TabIndex={TabIndex}&TrainingCode={TrainingCode}");
        }
        public Task<ResponseEntity> Create(TrainingQuestions input)
        {
            return _httpService.PostAsync<ResponseEntity, TrainingQuestions>($"{_baseURL}/Create", input);
        }
        public Task<TrainingQuestions> GetTrainingQuestionsDetails(Guid TrainingQuestionGI, Guid LogGI, string type)
        {
            return _httpService.GetAsync<TrainingQuestions>($"{_baseURL}/GetTrainingQuestionsDetails?TrainingQuestionGI={TrainingQuestionGI}&LogGI={LogGI}&Type={type}");
        }
        public async Task<ResponseEntity> Delete(TrainingQuestions model)
        {
            return await _httpService.PostAsync<ResponseEntity, TrainingQuestions>($"{_baseURL}/Delete", model);

        }
        public Task<ResponseEntity> ApproveOrReject(TrainingQuestions model)
        {
            return _httpService.PostAsync<ResponseEntity, TrainingQuestions>($"{_baseURL}/ApproveOrReject", model);
        }
    }
}
