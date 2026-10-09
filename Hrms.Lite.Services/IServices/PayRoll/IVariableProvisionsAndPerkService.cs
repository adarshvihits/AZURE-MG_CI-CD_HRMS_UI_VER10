using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll
{
    public interface IVariableProvisionsAndPerkService
    {
        public Task<VariableAllowance> GetVariableProvisionList(int Month, int Allowance, Filter Filter);
        public Task<ResponseEntity> EmployeeWiseUpdate(VariableAllowance model);
        public Task<ResponseEntity> VariableProvisionsBatchUpdate(VariableAllowance model);
        Task<DataSet> VariableAllowanceList_Excel(int Month, int Allowance, Filter Filter,string Type);

        public Task<ResponseEntity<List<VariableAllowance>>> ImportFromExcel(VariableAllowance model);


    }
}
