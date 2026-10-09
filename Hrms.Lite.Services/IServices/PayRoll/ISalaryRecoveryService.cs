using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll
{
    public interface ISalaryRecoveryService
    {
        public Task<SalaryRecovery> GetList(int Month, int AllowanceCode, Filter filter);
        public Task<ResponseEntity> Save(SalaryRecovery model);
        public Task<ResponseEntity<List<SalaryRecovery>>> ImportFromExcel(SalaryRecovery model);

        Task<DataSet> SalaryRecoveryList_Excel(int Month, int Allowance, Filter Filter);
    }
   
}
