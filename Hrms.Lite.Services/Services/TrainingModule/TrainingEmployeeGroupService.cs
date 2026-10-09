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
   public class TrainingEmployeeGroupService: ITrainingEmployeeGroupService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public TrainingEmployeeGroupService(IServiceBase httpService)
        {
            _baseURL = "TrainingEmployeeGroup";
            _baseURL1 = "Common";
            _httpService = httpService;
        }
        public async Task<TrainingEmployeeGroup> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<TrainingEmployeeGroup>($"{_baseURL}/List?TabIndex={TabIndex}");
        }
        public Task<ResponseEntity> Create(TrainingEmployeeGroup input)
        {
            return _httpService.PostAsync<ResponseEntity,TrainingEmployeeGroup>($"{_baseURL}/Create", input);
        }
        public async Task<TrainingEmployeeGroup> GEtCreateTraining()
        {
            return await _httpService.GetAsync<TrainingEmployeeGroup>($"{_baseURL}/GEtCreateTraining");
        }
        public Task<TrainingEmployeeGroup> GetDetails(Guid TrainingGroupGI, Guid LogGI, string Type)
        {
            return _httpService.GetAsync<TrainingEmployeeGroup>($"{_baseURL}/Details?TrainingGroupGI={TrainingGroupGI}&LogGI={LogGI}&Type={Type}");
        }
        public async Task<ResponseEntity> Delete(TrainingEmployeeGroup model)
        {
            return await _httpService.PostAsync<ResponseEntity, TrainingEmployeeGroup>($"{_baseURL}/Delete", model);

        }
        public Task<ResponseEntity> ApproveOrReject(TrainingEmployeeGroup model)
        {
            return _httpService.PostAsync<ResponseEntity, TrainingEmployeeGroup>($"{_baseURL}/ApproveOrReject", model);
        }
        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        {
            return _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");
        }
    }
}
