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
    public class ZoneService: IZoneService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public ZoneService(IServiceBase httpService)
        {
            _baseURL = "Zone";
            _baseURL1 = "Common";
            _httpService = httpService;
        }

        public Task<ResponseEntity> ApproveOrReject(Zone model)
        {
            return _httpService.PostAsync<ResponseEntity, Zone>($"{_baseURL}/ApproveOrReject", model);
        }

        public Task<ResponseEntity> Create(Zone Input)
        {
            return _httpService.PostAsync<ResponseEntity, Zone>($"{_baseURL}/Create", Input);
        }

        public Task<ResponseEntity> Delete(Zone Input)
        {
            return _httpService.PostAsync<ResponseEntity, Zone>($"{_baseURL}/Delete", Input);
        }

        public async Task<Zone> GetZoneDetails(Guid ZoneGI, Guid LogGI, int ZoneCode, string Type)
        {
            return await _httpService.GetAsync<Zone>($"{_baseURL}/GetZoneDetails?ZoneGI={ZoneGI}&&LogGI={LogGI}&&Type={Type}&&ZoneCode={ZoneCode}");
        }

        public async Task<Zone> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<Zone>($"{_baseURL}/List?TabIndex={TabIndex}");
        }
        public async Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        {
            return await _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");
        }
    }
}
