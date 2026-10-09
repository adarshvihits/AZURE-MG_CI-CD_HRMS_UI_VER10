using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
 public   interface ILeaveGroupService
    {
        public Task<LeaveGroup> GetList(string TabIndex);
        public Task<ResponseEntity> Create(LeaveGroup input);
        public Task<LeaveGroup> GetLeaveGroupDetails(Guid LeaveGroupGI, Guid LogGI, string Type);
        public Task<ResponseEntity> ApproveOrReject(LeaveGroup model);
        public Task<ResponseEntity> Delete(LeaveGroup input);
        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code);


    }
}
