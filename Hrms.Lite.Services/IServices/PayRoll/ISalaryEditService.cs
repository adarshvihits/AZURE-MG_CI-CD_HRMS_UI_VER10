using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll
{
  public  interface ISalaryEditService
    {
        public Task<SalaryEdit> GetList(int Month, int PayrollGroupCode, Filter filter);
        public Task<SalaryEdit> GetSalaryEditDetails(int Month,Guid EmployeeGI);
        public Task<ResponseEntity> SalaryEditSave(SalaryEdit model);
    }
}
