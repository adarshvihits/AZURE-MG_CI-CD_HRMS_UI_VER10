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
    public class TrainingMasterService : ITrainingMasterService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public TrainingMasterService(IServiceBase httpService)
        {
            _baseURL = "TrainingMaster";
            _baseURL1 = "Common";
            _httpService = httpService;
        }
        async Task<TrainingMaster> ITrainingMasterService.GetTrainingList(string Tabindex)
        {
            return await _httpService.GetAsync<TrainingMaster>($"{_baseURL}/GetTrainingList?Tabindex={Tabindex}");
        }
        async Task<TrainingMaster> ITrainingMasterService.GetTrainingMasterDetails(Guid TrainingGI, Guid LogGI, string Type)
        {
            return await _httpService.GetAsync<TrainingMaster>($"{_baseURL}/GetTrainingMasterDetails?TrainingGI={TrainingGI}&LogGI={LogGI}&Type={Type}");
        }
        async Task<ResponseEntity> ITrainingMasterService.Create(TrainingMaster model)
        {
            return await _httpService.PostAsync<ResponseEntity, TrainingMaster>($"{_baseURL}/Create", model);
        }
        async Task<ResponseEntity> ITrainingMasterService.ApproveOrReject(TrainingMaster model)
        {
            return await _httpService.PostAsync<ResponseEntity, TrainingMaster>($"{_baseURL}/ApproveOrReject", model);
        }
        async Task<ResponseEntity> ITrainingMasterService.Delete(Guid TrainingGI)
        {
            return await _httpService.DeleteAsync<ResponseEntity>($"{_baseURL}/Delete?TrainingGI={TrainingGI}");
        }

        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        {
            return _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");

        }
    }
}
