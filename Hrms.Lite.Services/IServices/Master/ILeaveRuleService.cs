using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
    public interface ILeaveRuleService
    {
        public Task<LeaveRule> GetLeaveRuleDetails(int LeaveGroup,int Leave);
        public Task<ResponseEntity> Save(LeaveRule input);
    }
}
