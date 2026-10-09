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
    public class LocationService:ILocationService
    {

        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public LocationService(IServiceBase httpService)
        {
            _baseURL = "Location";
            _baseURL1 = "Common";
            _httpService = httpService;
        }

        public Task<ResponseEntity> ApproveOrReject(Location Input)
        {
            return _httpService.PostAsync<ResponseEntity, Location>($"{_baseURL}/ApproveOrReject", Input);
        }

        public Task<ResponseEntity> Create(Location Input)
        {
            return _httpService.PostAsync<ResponseEntity, Location>($"{_baseURL}/Create", Input);
        }

        public Task<ResponseEntity> Delete(Location Model)
        {
            return _httpService.PostAsync<ResponseEntity, Location>($"{_baseURL}/Delete", Model);
        }

        public async Task<Location> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<Location>($"{_baseURL}/List?TabIndex={TabIndex}");
        }

        public Task<Location> GetLocationDetails(Guid LocationGI, Guid LogGI, string Type)
        {
            return _httpService.GetAsync<Location>($"{_baseURL}/GetLocationDetails?LocationGI={LocationGI}&LogGI={LogGI}&Type={Type}");
        }

        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        {
            return _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");
        }
    }
}
