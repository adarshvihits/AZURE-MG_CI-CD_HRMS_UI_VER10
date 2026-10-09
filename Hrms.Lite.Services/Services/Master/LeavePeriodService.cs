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
   public class LeavePeriodService : ILeavePeriodService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public LeavePeriodService(IServiceBase httpService)
        {
            _baseURL = "LeavePeriod";
            _baseURL1 = "Common";
            _httpService = httpService;
        }
        public async Task<LeavePeriod> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<LeavePeriod>($"{_baseURL}/List?TabIndex={TabIndex}");
        }
        public Task<ResponseEntity> Create(LeavePeriod input)
        {
            return _httpService.PostAsync<ResponseEntity, LeavePeriod>($"{_baseURL}/Create", input);
        }
        public Task<LeavePeriod> GetLeavePeriodDetails(Guid LeavePeriodGI, Guid LogGI, string Type)
        {
            return _httpService.GetAsync<LeavePeriod>($"{_baseURL}/LeavePeriodDetails?LeavePeriodGI={LeavePeriodGI}&LogGi={LogGI}&Type={Type}");
        }
        public Task<ResponseEntity> ApproveOrReject(LeavePeriod model)
        {
            return _httpService.PostAsync<ResponseEntity, LeavePeriod>($"{_baseURL}/ApproveOrReject", model);
        }
        public async Task<ResponseEntity> Delete(LeavePeriod model)
        {
            return await _httpService.PostAsync<ResponseEntity, LeavePeriod>($"{_baseURL}/Delete", model);

        }

        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        {
            return _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");
        }
    }
}
