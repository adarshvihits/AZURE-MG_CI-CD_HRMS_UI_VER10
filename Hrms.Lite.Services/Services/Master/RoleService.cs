using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.Master;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Data;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.Master
{
    public class RoleService : IRoleService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public RoleService(IServiceBase httpService)
        {
            _baseURL = "Role";
            _baseURL1 = "Common";
            _httpService = httpService;
        }

        public Task<ResponseEntity> ApproveOrReject(Role model)
        {
            return _httpService.PostAsync<ResponseEntity, Role>($"{_baseURL}/ApproveOrReject", model);
        }

        public Task<ResponseEntity> Create(Role input)
        {
            return _httpService.PostAsync<ResponseEntity, Role>($"{_baseURL}/Create", input);
        }

        public Task<ResponseEntity> Delete(Role model)
        {
            return _httpService.PostAsync<ResponseEntity, Role>($"{_baseURL}/Delete", model);
        }

        public Task<Role> GetRoleDetails(Guid RoleGI, Guid LogGI, string Type)
        {
            return _httpService.GetAsync<Role>($"{_baseURL}/RoleDetails?RoleGI={RoleGI}&LogGI={LogGI}&Type={Type}");
        }

        public async Task<Role> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<Role>($"{_baseURL}/List?TabIndex={TabIndex}");
        }

        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        {
            return _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");
        }

        public async Task<DataSet> GetList_Excel(string TabIndex)
        {
            return await _httpService.GetAsync<DataSet>($"{_baseURL}/GetList_Excel?TabIndex={TabIndex}");
        }
    }
}
