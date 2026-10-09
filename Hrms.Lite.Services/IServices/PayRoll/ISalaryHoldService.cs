using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll
{
    public interface ISalaryHoldService
    {
        public Task<SalaryHold> GetList(int Month,  int PayrollGroupCode, Filter filter);
        public Task<DataSet> GetSalaryHold_Excel(int Month, int PayrollGroupCode, Filter filter);
        public Task<SalaryHold> GetSalaryHoldDetails(Guid EmpGI, int month);
        public Task<ResponseEntity> Save(SalaryHold model);    
        public Task<ResponseEntity<List<SalaryHold>>> ImportFromExcel(SalaryHold model,  int PayrollGroupCode);


    }
}
