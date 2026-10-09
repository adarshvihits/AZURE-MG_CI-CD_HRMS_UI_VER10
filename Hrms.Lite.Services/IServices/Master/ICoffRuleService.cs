using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
    public interface ICoffRuleService
    {
        public Task<CoffLeaveRule> GetCoffRuleDetails(int Leavegroup);
        public Task<ResponseEntity> Create(CoffLeaveRule model);

    }
}
