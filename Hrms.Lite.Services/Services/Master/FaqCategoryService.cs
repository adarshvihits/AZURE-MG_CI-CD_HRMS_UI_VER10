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
  public  class FaqCategoryService : IFaqCategoryService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public FaqCategoryService(IServiceBase httpService)
        {
            _baseURL = "FaqCategory";
            _baseURL1 = "Common";
            _httpService = httpService;
        }
        public async Task<FaqCategory> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<FaqCategory>($"{_baseURL}/List?TabIndex={TabIndex}");
        }

        public Task<ResponseEntity> Create(FaqCategory input)
        {
            return _httpService.PostAsync<ResponseEntity, FaqCategory>($"{_baseURL}/Create", input);
        }

        public Task<FaqCategory> GetFaqCategoryDetails(Guid FaqCategoryGI, Guid LogGI, string type)
        {
            return _httpService.GetAsync<FaqCategory>($"{_baseURL}/FaqCategoryDetails?FaqCategoryGI={FaqCategoryGI}&LogGI={LogGI}&Type={type}");
        }
        public Task<ResponseEntity> ApproveOrReject(FaqCategory model)
        {
            return _httpService.PostAsync<ResponseEntity, FaqCategory>($"{_baseURL}/ApproveOrReject", model);
        }
        public async Task<ResponseEntity> Delete(FaqCategory model)
        {
            return await _httpService.PostAsync<ResponseEntity, FaqCategory>($"{_baseURL}/Delete", model);

        }
    }
}
