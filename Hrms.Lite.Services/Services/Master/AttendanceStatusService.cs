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
    public class AttendanceStatusService : IAttendanceStatusService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public AttendanceStatusService(IServiceBase httpService)
        {
            _baseURL = "AttendanceStatus";
            _baseURL1 = "Common";
            _httpService = httpService;
        }
        public async Task<AttendanceStatus> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<AttendanceStatus>($"{_baseURL}/List?TabIndex={TabIndex}");
        }
        public Task<ResponseEntity> Create(AttendanceStatus input)
        {
            return _httpService.PostAsync<ResponseEntity, AttendanceStatus>($"{_baseURL}/Create", input);
        }
        public Task<AttendanceStatus> GetAttendanceStatusDetails(Guid AttendanceStatusGI, Guid LogGI, string Type)
        {
            return _httpService.GetAsync<AttendanceStatus>($"{_baseURL}/AttendanceStatusDetails?AttendanceStatusGI={AttendanceStatusGI}&LogGi={LogGI}&Type={Type}");
        }
        public Task<ResponseEntity> ApproveOrReject(AttendanceStatus model)
        {
            return _httpService.PostAsync<ResponseEntity, AttendanceStatus>($"{_baseURL}/ApproveOrReject", model);
        }
        public async Task<ResponseEntity> Delete(AttendanceStatus model)
        {
            return await _httpService.PostAsync<ResponseEntity, AttendanceStatus>($"{_baseURL}/Delete", model);

        }

        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        {
            return _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");
        }
    }
}
