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
    public class TrainingSubjectService : ITrainingSubjectService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public TrainingSubjectService(IServiceBase httpService)
        {
            _baseURL = "TrainingSubject";
            _baseURL1 = "Common";
            _httpService = httpService;
        }
        public async Task<TrainingSubject> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<TrainingSubject>($"{_baseURL}/List?TabIndex={TabIndex}");

        }
        public Task<ResponseEntity> Create(TrainingSubject input)
        {
            return _httpService.PostAsync<ResponseEntity, TrainingSubject>($"{_baseURL}/Create", input);
        }
        public Task<TrainingSubject> GetSubjectDetails(Guid TrainingSubjectGI, Guid LogGI, string type)
        {
            return _httpService.GetAsync<TrainingSubject>($"{_baseURL}/SubjectDetails?TrainingSubjectGI={TrainingSubjectGI}&LogGI={LogGI}&Type={type}");
        }
        public Task<ResponseEntity> ApproveOrReject(TrainingSubject model)
        {
            return _httpService.PostAsync<ResponseEntity, TrainingSubject>($"{_baseURL}/ApproveOrReject", model);
        }
        public async Task<ResponseEntity> Delete(TrainingSubject model)
        {
            return await _httpService.PostAsync<ResponseEntity, TrainingSubject>($"{_baseURL}/Delete", model);

        }
    }
}
