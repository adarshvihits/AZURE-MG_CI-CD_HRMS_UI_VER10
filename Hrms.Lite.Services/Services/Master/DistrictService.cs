using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.Master;
using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.Master
{
    public class DistrictService : IDistrictService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public DistrictService(IServiceBase httpService)
        {
            _baseURL = "District";
            _baseURL1 = "Common";
            _httpService = httpService;
        }

        public Task<ResponseEntity> Create(District input)
        {
            return _httpService.PostAsync<ResponseEntity, District>($"{_baseURL}/Create", input);
        }

        public async Task<District> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<District>($"{_baseURL}/list?TabIndex={TabIndex}");
        }

        public Task<District> GetDistrictDetails(Guid LogGI, Guid DistrictGI, string Type)
        {
            return _httpService.GetAsync<District>($"{_baseURL}/GetDistrictDetails?LogGI={LogGI}&DistrictGI={DistrictGI}&Type={Type}");
        }

        public Task<ResponseEntity> ApproveOrReject(District input)
        {
            return _httpService.PostAsync<ResponseEntity, District>($"{_baseURL}/ApproveOrReject", input);
        }

        public Task<ResponseEntity> Delete(District input)
        {
            return _httpService.PostAsync<ResponseEntity, District>($"{_baseURL}/Delete", input);
        }
        public async Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        {
            return await _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");
        }
    }
}
