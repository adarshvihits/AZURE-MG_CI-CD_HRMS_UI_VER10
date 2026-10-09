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
   public class AssetService: IAssetService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public AssetService(IServiceBase httpService)
        {
            _baseURL = "Asset";
            _baseURL1 = "Common";
            _httpService = httpService;
        }
        public async Task<Asset> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<Asset>($"{_baseURL}/GetList?TabIndex={TabIndex}");
        }
        public Task<Asset> GetAssetDetails(Guid AssetGI, Guid LogGI, string type)
        {
            return _httpService.GetAsync<Asset>($"{_baseURL}/GetAssetDetails?AssetGI={AssetGI}&LogGI={LogGI}&Type={type}");
        }
        public Task<ResponseEntity> Create(Asset input)
        {
            return _httpService.PostAsync<ResponseEntity, Asset>($"{_baseURL}/AssetSave", input);
        }
        public Task<ResponseEntity> ApproveOrReject(Asset model)
        {
            return _httpService.PostAsync<ResponseEntity, Asset>($"{_baseURL}/ApproveOrReject", model);
        }
        public async Task<ResponseEntity> Delete(Guid GI)
        {
            return await _httpService.DeleteAsync<ResponseEntity>($"{_baseURL}/AssetDelete?AssetGI={GI}");

        }

        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        {
            return _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");
        }
    }
}
