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
    public class TimeSheetActivityService: ITimeSheetActivityService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public TimeSheetActivityService(IServiceBase httpService)
        {
            _baseURL = "TimeSheetActivity";
            _baseURL1 = "Common";
            _httpService = httpService;
        }

        public async Task<TimeSheetActivity> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<TimeSheetActivity>($"{_baseURL}/List?TabIndex={TabIndex}");
        }
        public async Task<ResponseEntity> Create(TimeSheetActivity input)
        {
            return await _httpService.PostAsync<ResponseEntity, TimeSheetActivity>($"{_baseURL}/Create", input);
        }
        public async Task<TimeSheetActivity> GetActivityDetails(Guid ActivityGI, Guid LogGI, string Type)
        {
            return await _httpService.GetAsync<TimeSheetActivity>($"{_baseURL}/ActivityDetails?ActivityGI={ActivityGI}&&LogGI={LogGI}&&Type={Type}");
        }
        public async Task<ResponseEntity> ApproveOrReject(TimeSheetActivity model)
        {
            return await _httpService.PostAsync<ResponseEntity, TimeSheetActivity>($"{_baseURL}/ApproveOrReject", model);
        }
        public async Task<ResponseEntity> Delete(TimeSheetActivity model)
        {
            return await _httpService.PostAsync<ResponseEntity, TimeSheetActivity>($"{_baseURL}/Delete", model);
        }

        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        {
            return _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");

        }
    }
}
