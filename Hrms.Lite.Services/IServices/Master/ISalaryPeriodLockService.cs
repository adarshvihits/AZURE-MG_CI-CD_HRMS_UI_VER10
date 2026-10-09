using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
   public interface ISalaryPeriodLockService
    {
        public Task<SalaryPeriodLock> GetList(int Month);

        Task<SalaryPeriodLock> GetLockUnLockLog(int MonthCode, int PGCode);
        public Task<SalaryPeriodLock> GetOpenMonth(int Month,int PyrollGroup);
        public Task<ResponseEntity> Create(SalaryPeriodLock input);
    }
}
