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
   public class ApproverRoleService : IApproverRoleService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        private readonly string _baseURL1;

        public ApproverRoleService(IServiceBase httpService)
        {
            _baseURL = "ApproverRole";
            _baseURL1 = "Common";
            _httpService = httpService;
        }
        public async Task<ApproverRole> GetList(string TabIndex, int ClassificationType, int country)
        {
            return await _httpService.GetAsync<ApproverRole>($"{_baseURL}/List?TabIndex={TabIndex}&ClassificationType={ClassificationType}&country={country}");
        }
        public Task<ApproverRole> GetApproverRoleDetails(int AuthorityEntityCode,  string Type)
        {
            return _httpService.GetAsync<ApproverRole>($"{_baseURL}/ApproverRoleDetails?AuthorityEntityCode={AuthorityEntityCode}&Type={Type}");
        }
        public Task<ResponseEntity> Create(ApproverRole input)
        {
            return _httpService.PostAsync<ResponseEntity, ApproverRole>($"{_baseURL}/Create", input);
        }
        
        public async Task<ResponseEntity> Delete(ApproverRole model)
        {
            return await _httpService.PostAsync<ResponseEntity, ApproverRole>($"{_baseURL}/Delete", model);

        }
        public Task<ApproverRole> GetApplicabilityDropdown(int? RoleCode,int? Country)
        {
            return _httpService.GetAsync<ApproverRole>($"{_baseURL}/GetApplicabilityDropdown?RoleCode={RoleCode}&Country={Country}");
        }
        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        {
            return _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");
        }
        public async Task<List<Dropdown>> GetEditApplicabilityDropdown(int? RoleCode, int? Country)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetEditApplicabilityDropdown?RoleCode={RoleCode}&Country={Country}");
        }
    }
}
