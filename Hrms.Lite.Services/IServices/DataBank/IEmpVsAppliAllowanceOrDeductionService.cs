using Hrms.Lite.Shared.DataBank;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
 public   interface IEmpVsAppliAllowanceOrDeductionService
    {
        public Task<EmpVsAppliAllowanceOrDeduction> GetAllowanceOrDeductionList(int Type, int AllowOrDeduction, int BasedOn, Filter filter);
        Task<ResponseEntity> EmpVsApplAllowOrDedSave(EmpVsAppliAllowanceOrDeduction model);
        public Task<ResponseEntity<List<ApplAllowOrDedList>>> ImportFromExcel(EmpVsAppliAllowanceOrDeduction model);
    }
}
