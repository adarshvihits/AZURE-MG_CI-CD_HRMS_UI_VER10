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
    public class TrainingCostService : ITrainingCostService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public TrainingCostService(IServiceBase httpService)
        {
            _baseURL = "TrainingCost";
            _baseURL1 = "Common";
            _httpService = httpService;
        }
        public async Task<TrainingCost> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<TrainingCost>($"{_baseURL}/List?TabIndex={TabIndex}");

        }
        public Task<ResponseEntity> Create(TrainingCost input)
        {
            return _httpService.PostAsync<ResponseEntity, TrainingCost>($"{_baseURL}/Create", input);
        }
        public Task<TrainingCost> GetDetails(Guid TrainingCostHeadGI, Guid LogGI, string type)
        {
            return _httpService.GetAsync<TrainingCost>($"{_baseURL}/TrainingCostDetails?TrainingCostHeadGI={TrainingCostHeadGI}&LogGI={LogGI}&Type={type}");
        }
        public Task<ResponseEntity> ApproveOrReject(TrainingCost model)
        {
            return _httpService.PostAsync<ResponseEntity, TrainingCost>($"{_baseURL}/ApproveOrReject", model);
        }
        public async Task<ResponseEntity> Delete(TrainingCost model)
        {
            return await _httpService.PostAsync<ResponseEntity, TrainingCost>($"{_baseURL}/Delete", model);

        }

    }
}
