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
    public class AssetCategoryService : IAssetCategoryService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;

        private readonly IServiceBase _httpService;
        public AssetCategoryService(IServiceBase httpService)
        {
            _baseURL = "AssetCategory";
            _baseURL1 = "Common";

            _httpService = httpService;
        }

        public async Task<AssetCategory> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<AssetCategory>($"{_baseURL}/List?TabIndex={TabIndex}");
        }
        public Task<ResponseEntity> Create(AssetCategory input)
        {
            return _httpService.PostAsync<ResponseEntity, AssetCategory>($"{_baseURL}/Create", input);
        }
        public Task<AssetCategory> GetAssetCategoryDetails(Guid AssetCategoryGI, Guid LogGI, string Type)
        {
            return _httpService.GetAsync<AssetCategory>($"{_baseURL}/AssetCategoryDetails?AssetCategoryGI={AssetCategoryGI}&LogGi={LogGI}&Type={Type}");
        }
        public Task<ResponseEntity> ApproveOrReject(AssetCategory model)
        {
            return _httpService.PostAsync<ResponseEntity, AssetCategory>($"{_baseURL}/ApproveOrReject", model);
        }
        public async Task<ResponseEntity> Delete(AssetCategory model)
        {
            return await _httpService.PostAsync<ResponseEntity, AssetCategory>($"{_baseURL}/Delete", model);

        }

        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        {
            return _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");
        }
    }
}
