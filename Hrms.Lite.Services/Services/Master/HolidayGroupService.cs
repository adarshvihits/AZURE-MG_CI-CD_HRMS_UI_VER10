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
    public class HolidayGroupService:IHolidayGroupService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public HolidayGroupService(IServiceBase httpService)
        {
            _baseURL = "HolidayGroup";
            _baseURL1 = "Common";
            _httpService = httpService;
        }

        public Task<ResponseEntity> ApproveOrReject(HolidayGroup model)
        {
            return _httpService.PostAsync<ResponseEntity, HolidayGroup>($"{_baseURL}/ApproveOrReject", model);
        }

        public Task<ResponseEntity> Create(HolidayGroup input)
        {
            return _httpService.PostAsync<ResponseEntity, HolidayGroup>($"{_baseURL}/Create", input);
        }

        public Task<ResponseEntity> Delete(HolidayGroup input)
        {
            return _httpService.PostAsync<ResponseEntity, HolidayGroup>($"{_baseURL}/Delete", input);
        }

        public Task<HolidayGroup> GetHolidayGroupDetails(Guid HolidayGroupGI, Guid LogGI, string Type)
        {
            return _httpService.GetAsync<HolidayGroup>($"{_baseURL}/HolidayGroupDetails?HolidayGroupGI={HolidayGroupGI}&LogGI={LogGI}&Type={Type}");
        }

        public async Task<HolidayGroup> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<HolidayGroup>($"{_baseURL}/List?TabIndex={TabIndex}");
        }
        public async Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        {
            return await _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");
        }
    }
}
