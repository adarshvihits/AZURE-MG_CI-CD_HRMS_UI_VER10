using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
    public interface IDutyRosterLockService
    {
       public Task<DutyRosterLock> GetList(int MonthCode);
        public Task<ResponseEntity> Save(DutyRosterLock model);
        public Task<ResponseEntity> BachUpdate(DutyRosterLock model);
    }
}
