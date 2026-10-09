using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.TrainingModule;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TrainingModule;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.TrainingModule
{
   public class TrainingExternalTrainerService : ITrainingExternalTrainerService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public TrainingExternalTrainerService(IServiceBase httpService)
        {
            _baseURL = "TrainingExternalTrainer";
            _baseURL1 = "Common";
            _httpService = httpService;
        }
        public async Task<TrainingExternalTrainer> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<TrainingExternalTrainer>($"{_baseURL}/List?TabIndex={TabIndex}");

        }
        public Task<ResponseEntity> Create(TrainingExternalTrainer input)
        {
            return _httpService.PostAsync<ResponseEntity, TrainingExternalTrainer>($"{_baseURL}/Create", input);
        }
        public Task<TrainingExternalTrainer> GetSubjectDetails(Guid TrainingExternalTrainerGI, Guid LogGI, string type)
        {
            return _httpService.GetAsync<TrainingExternalTrainer>($"{_baseURL}/TrainerDetails?TrainingExternalTrainerGI={TrainingExternalTrainerGI}&LogGI={LogGI}&Type={type}");
        }
        public Task<ResponseEntity> ApproveOrReject(TrainingExternalTrainer model)
        {
            return _httpService.PostAsync<ResponseEntity, TrainingExternalTrainer>($"{_baseURL}/ApproveOrReject", model);
        }
        public async Task<ResponseEntity> Delete(TrainingExternalTrainer model)
        {
            return await _httpService.PostAsync<ResponseEntity, TrainingExternalTrainer>($"{_baseURL}/Delete", model);

        }
    }
}
