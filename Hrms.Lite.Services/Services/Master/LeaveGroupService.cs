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
    public class LeaveGroupService : ILeaveGroupService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public LeaveGroupService(IServiceBase httpService)
        {
            _baseURL = "LeaveGroup";
            _baseURL1 = "Common";
            _httpService = httpService;
        }
        public Task<ResponseEntity> ApproveOrReject(LeaveGroup model)
        {
            return _httpService.PostAsync<ResponseEntity, LeaveGroup>($"{_baseURL}/ApproveOrReject", model);
        }

        public Task<ResponseEntity> Create(LeaveGroup input)
        {
            return _httpService.PostAsync<ResponseEntity, LeaveGroup>($"{_baseURL}/Create", input);
        }

        public Task<ResponseEntity> Delete(LeaveGroup input)
        {
            return _httpService.PostAsync<ResponseEntity, LeaveGroup>($"{_baseURL}/Delete", input);
        }

        public Task<LeaveGroup> GetLeaveGroupDetails(Guid LeaveGroupGI, Guid LogGI, string Type)
        {
            return _httpService.GetAsync<LeaveGroup>($"{_baseURL}/LeaveGroupDetails?LeaveGroupGI={LeaveGroupGI}&LogGI={LogGI}&Type={Type}");
        }

        public async Task<LeaveGroup> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<LeaveGroup>($"{_baseURL}/List?TabIndex={TabIndex}");
        }

        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        {
            return _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");
        }
    }
}
