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
   public class TrainingLocationService : ITrainingLocationService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public TrainingLocationService(IServiceBase httpService)
        {
            _baseURL = "TrainingLocation";
            _baseURL1 = "Common";
            _httpService = httpService;
        }
        public async Task<TrainingLocation> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<TrainingLocation>($"{_baseURL}/List?TabIndex={TabIndex}");

        }
        public Task<ResponseEntity> Create(TrainingLocation input)
        {
            return _httpService.PostAsync<ResponseEntity, TrainingLocation>($"{_baseURL}/Create", input);
        }
        public Task<TrainingLocation> GetDetails(Guid TrainingExternalLocationGI, Guid LogGI, string type)
        {
            return _httpService.GetAsync<TrainingLocation>($"{_baseURL}/TrainerDetails?TrainingExternalLocationGI={TrainingExternalLocationGI}&LogGI={LogGI}&Type={type}");
        }
        public Task<ResponseEntity> ApproveOrReject(TrainingLocation model)
        {
            return _httpService.PostAsync<ResponseEntity, TrainingLocation>($"{_baseURL}/ApproveOrReject", model);
        }
        public async Task<ResponseEntity> Delete(TrainingLocation model)
        {
            return await _httpService.PostAsync<ResponseEntity, TrainingLocation>($"{_baseURL}/Delete", model);

        }
    }
}
