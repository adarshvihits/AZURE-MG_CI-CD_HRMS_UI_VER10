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
    public class TrainingCategoryService: ITrainingCategoryService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public TrainingCategoryService(IServiceBase httpService)
        {
            _baseURL = "TrainingCategory";
            _baseURL1 = "Common";
            _httpService = httpService;
        }
        public async Task<TrainingCategory> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<TrainingCategory>($"{_baseURL}/List?TabIndex={TabIndex}");
        }
        public Task<TrainingCategory> GetTrainingCategoryDetails(Guid TrainingCategoryGI, Guid LogGI, string type)
        {
            return _httpService.GetAsync<TrainingCategory>($"{_baseURL}/TrainingCategoryDetails?TrainingCategoryGI={TrainingCategoryGI}&LogGI={LogGI}&Type={type}");
        }
        public Task<ResponseEntity> Create(TrainingCategory input)
        {
            return _httpService.PostAsync<ResponseEntity, TrainingCategory>($"{_baseURL}/Create", input);
        }

        public Task<ResponseEntity> ApproveOrReject(TrainingCategory model)
        {
            return _httpService.PostAsync<ResponseEntity, TrainingCategory>($"{_baseURL}/ApproveOrReject", model);
        }
        public async Task<ResponseEntity> Delete(TrainingCategory model)
        {
            return await _httpService.PostAsync<ResponseEntity, TrainingCategory>($"{_baseURL}/Delete", model);

        }
    }
}
