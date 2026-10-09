using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Data;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
    public interface IRoleService
    {
        public Task<Role> GetList(string TabIndex);
        public Task<ResponseEntity> Create(Role input);
        public Task<Role> GetRoleDetails(Guid RoleGI, Guid LogGI, string Type);
        public Task<ResponseEntity> ApproveOrReject(Role model);
        public Task<ResponseEntity> Delete(Role model);
        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code);
        public Task<DataSet> GetList_Excel(string TabIndex);
    }
}
