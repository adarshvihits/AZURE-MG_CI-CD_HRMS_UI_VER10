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
    public class AttendanceRequestReasonService: IAttendanceRequestReasonService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;

        private readonly IServiceBase _httpService;
        public AttendanceRequestReasonService(IServiceBase httpService)
        {
            _baseURL = "AttendanceRequestReason";
            _baseURL1 = "Common";
            _httpService = httpService;
        }
        public async Task<AttendanceRequestReason> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<AttendanceRequestReason>($"{_baseURL}/List?TabIndex={TabIndex}");
        }
        public async Task<AttendanceRequestReason> GetLeaveTypeSettings()
        {
            return await _httpService.GetAsync<AttendanceRequestReason>($"{_baseURL}/GetLeaveTypeSettings");
        }
        public Task<ResponseEntity> ApproveOrReject(AttendanceRequestReason model)
        {
            return _httpService.PostAsync<ResponseEntity, AttendanceRequestReason>($"{_baseURL}/ApproveOrReject", model);
        }

        public Task<ResponseEntity> Create(AttendanceRequestReason input)
        {
            return _httpService.PostAsync<ResponseEntity, AttendanceRequestReason>($"{_baseURL}/Create", input);
        }

        public Task<ResponseEntity> Delete(AttendanceRequestReason model)
        {
            return _httpService.PostAsync<ResponseEntity, AttendanceRequestReason>($"{_baseURL}/Delete", model);
        }
        public async Task<AttendanceRequestReason> GetDetails(Guid ReasonGI, Guid LogGI, string Type)
        {
            return await _httpService.GetAsync<AttendanceRequestReason>($"{_baseURL}/ReasonDetails?ReasonGI={ReasonGI}&LogGI={LogGI}&Type={Type}");
        }

        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        {
            return _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");

        }
    }
}
