using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll
{
  public  interface IDailyFandFSalaryProcessingService
    {
        public Task<DailyFandFSalaryProcessing> GetList(int Month, int PayrollGroup, Filter filter);
        public Task<ResponseEntity<DailyFandFSalaryProcessing>> GetDailyFandF_SalaryProcessing(DailyFandFSalaryProcessing model);
    }
}
