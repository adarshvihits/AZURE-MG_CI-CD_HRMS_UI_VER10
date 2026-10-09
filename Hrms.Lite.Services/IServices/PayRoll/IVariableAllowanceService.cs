using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll
{
    public interface IVariableAllowanceService
    {
 
        public Task<VariableAllowance> GetVariableAllowanceList(int Month, int Allowance,Filter Filter);
        public Task<ResponseEntity> VariableAllowanceBatchUpdate(VariableAllowance model);
        public Task<ResponseEntity> EmployeeWiseVariableAllowanceSave(VariableAllowance model);
        Task<DataSet> VariableAllowanceList_Excel(int Month, int Allowance, Filter Filter);
        public Task<ResponseEntity<List<VariableAllowance>>> ImportFromExcel(VariableAllowance model);

        //----------------------Variable Allowance Upload Month wise Only
        public Task<VariableAllowance> GetVariableAllowanceUploadList(int Month);
        public Task<ResponseEntity<List<VariableAllowance>>> ImportFromExcelMultiple(VariableAllowance model);



        ////-----------WorkFlowAllowance Confirmation
        ///
        Task<WorkFlowConfirmation> ConfirmationList_GEO(string Tab, Filter filter, int Month,int AllowanceCode);
        public Task<ResponseEntity> EditUpdate(WorkFlowConfirmation model);
        Task<ResponseEntity> ConfirmWF(Filter filter, int Month ,int AllowanceCode);
        Task<DataSet> GetConfirmationList_Excel(string Tab, Filter filter, int Month, int AllowanceCode);
    }
}
