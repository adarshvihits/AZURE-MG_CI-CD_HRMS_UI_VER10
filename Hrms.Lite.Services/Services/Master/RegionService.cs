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
    public class RegionService: IRegionService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public RegionService(IServiceBase httpService)
        {
            _baseURL = "Region";
            _baseURL1 = "Common";
            _httpService = httpService;
        }
        public async Task<Region> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<Region>($"{_baseURL}/List?TabIndex={TabIndex}");
        }
        public Task<Region> GetRegionDetails(Guid RegionGI, Guid LogGI, string type)
        {
            return _httpService.GetAsync<Region>($"{_baseURL}/RegionDetails?RegionGI={RegionGI}&LogGI={LogGI}&Type={type}");
        }
        public Task<ResponseEntity> Create(Region input)
        {
            return _httpService.PostAsync<ResponseEntity, Region>($"{_baseURL}/Create", input);
        }
        public Task<ResponseEntity> ApproveOrReject(Region model)
        {
            return _httpService.PostAsync<ResponseEntity, Region>($"{_baseURL}/ApproveOrReject", model);
        }
        public async Task<ResponseEntity> Delete(Region model)
        {
            return await _httpService.PostAsync<ResponseEntity, Region>($"{_baseURL}/Delete", model);

        }
        public async Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        {
            return await _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");
        }
    }
}
