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
    public class VenueService: IVenueService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public VenueService(IServiceBase httpService)
        {
            _baseURL = "Venue";
            _baseURL1 = "Common";
            _httpService = httpService;
        }

        public Task<ResponseEntity> ApproveOrReject(Venue Input)
        {
            return _httpService.PostAsync<ResponseEntity, Venue>($"{_baseURL}/ApproveOrReject", Input);
        }

        public Task<ResponseEntity> Create(Venue Input)
        {
            return _httpService.PostAsync<ResponseEntity, Venue>($"{_baseURL}/Create", Input);
        }

        public Task<ResponseEntity> Delete(Venue Model)
        {
            return _httpService.PostAsync<ResponseEntity, Venue>($"{_baseURL}/Delete", Model);
        }

        public async Task<Venue> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<Venue>($"{_baseURL}/List?TabIndex={TabIndex}");
        }

        public Task<Venue> GetVenueDetails(Guid VenueGI, Guid LogGI, string Type)
        {
            return _httpService.GetAsync<Venue>($"{_baseURL}/GetVenueDetails?VenueGI={VenueGI}&LogGI={LogGI}&Type={Type}");
        }

        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        {
            return _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");
        }
    }
}
