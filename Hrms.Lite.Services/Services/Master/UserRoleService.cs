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
   public class UserRoleService: IUserRoleService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;

        private readonly IServiceBase _httpService;
        public UserRoleService(IServiceBase httpService)
        {
            _baseURL = "UserRole";
            _baseURL1 = "Common";

            _httpService = httpService;
        }

        public async Task<UserRole> GetUserRoleList(string TabIndex)
        {
            return await _httpService.GetAsync<UserRole>($"{_baseURL}/GetUserRoleList?TabIndex={TabIndex}");
        }
        async Task<UserRole> IUserRoleService.GetUserRoleDetails(Guid UserRoleGI, Guid LogGI, string Type)
        {
            return await _httpService.GetAsync<UserRole>($"{_baseURL}/GetUserRoleDetails?UserRoleGI={UserRoleGI}&LogGI={LogGI}&Type={Type}");
        }
        async Task<UserRole> IUserRoleService.GetApplicabilityDetails(Head data, int count, int Mcount, string mode)
        {
            return await _httpService.PostAsync<UserRole, Head>($"{_baseURL}/GetApplicabilityDetails?count={count}&MCount={Mcount}&mode={mode}", data);
        }
        async Task<ResponseEntity> IUserRoleService.UserRoleSave(UserRole userRole)
        {
            return await _httpService.PostAsync<ResponseEntity, UserRole>($"{_baseURL}/Create", userRole);
        }
        async Task<ResponseEntity> IUserRoleService.UserRoleDelete(Guid UserRoleGI)
        {
            return await _httpService.DeleteAsync<ResponseEntity>($"{_baseURL}/Delete?UserRoleGI={UserRoleGI}");
        }
        async Task<ResponseEntity> IUserRoleService.UserRoleApprove(UserRole userRole)
        {
            return await _httpService.PostAsync<ResponseEntity, UserRole>($"{_baseURL}/ApproveOrReject", userRole);
        }

        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        {
            return _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");
        }
        public async Task<UserRole> GetUserRoleTabDetails(Guid UserRoleGI)
        {
            return await _httpService.GetAsync<UserRole>($"{_baseURL}/GetUserRoleTabDetails?UserRoleGI={UserRoleGI}");
        }
        async Task<ResponseEntity> IUserRoleService.TabApplicabilitySave(UserRole userRole)
        {
            return await _httpService.PostAsync<ResponseEntity, UserRole>($"{_baseURL}/TabApplicability", userRole);
        }
    }
}
