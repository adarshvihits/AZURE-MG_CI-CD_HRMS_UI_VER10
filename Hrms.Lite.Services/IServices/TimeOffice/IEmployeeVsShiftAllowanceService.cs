using Hrms.Lite.Shared;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.TimeOffice
{
  public  interface IEmployeeVsShiftAllowanceService
    {
        public Task<EmployeeVsShiftAllowance> GetEmployeeVsShiftAllowanceList( int ShiftCode, Filter filter);
        public Task<DataSet> GetEmployeeVsShiftAllowanceList_Excel( int ShiftCode, Filter filter);
        public Task<ResponseEntity> Save(EmployeeVsShiftAllowance model);
    }
}
