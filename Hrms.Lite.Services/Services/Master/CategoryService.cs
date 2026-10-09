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
    public class CategoryService : ICategoryService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public CategoryService(IServiceBase httpService)
        {
            _baseURL = "Category";
            _baseURL1 = "Common";
            _httpService = httpService;
        }

        public Task<ResponseEntity> ApproveOrReject(Category model)
        {
            return _httpService.PostAsync<ResponseEntity, Category>($"{_baseURL}/ApproveOrReject", model);
        }

        public Task<ResponseEntity> Create(Category input)
        {
            return _httpService.PostAsync<ResponseEntity, Category>($"{_baseURL}/Create", input);
        }

        public Task<ResponseEntity> Delete(Category model)
        {
            return _httpService.PostAsync<ResponseEntity, Category>($"{_baseURL}/Delete", model);
        }

        public Task<Category> GetCategoryDetails(Guid CategoryGI ,Guid LogGI, string Type)
        {
            return _httpService.GetAsync<Category>($"{_baseURL}/GetCategoryDetails?CategoryGI={CategoryGI}&LogGI={LogGI}&Type={Type}");
        }

        public async Task<Category> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<Category>($"{_baseURL}/list?TabIndex={TabIndex}");
        }
        public async Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        {
            return await _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");
        }
    }
}
