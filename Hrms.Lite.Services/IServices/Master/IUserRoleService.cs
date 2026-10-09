using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
   public interface IUserRoleService
    {
        public Task<UserRole> GetUserRoleList(string TabIndex);
        public Task<UserRole> GetUserRoleDetails(Guid UserRoleGI,Guid LogGI,string Type);
        public Task<UserRole> GetApplicabilityDetails(Head data, int count, int Mcount, string mode);
        public Task<ResponseEntity> UserRoleSave(UserRole model);
        public Task<ResponseEntity> UserRoleDelete(Guid UserRoleGI);
        public Task<ResponseEntity> UserRoleApprove(UserRole model);
        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code);
        public Task<UserRole> GetUserRoleTabDetails(Guid UserRoleGI);
        public Task<ResponseEntity> TabApplicabilitySave(UserRole input);
    }
}
