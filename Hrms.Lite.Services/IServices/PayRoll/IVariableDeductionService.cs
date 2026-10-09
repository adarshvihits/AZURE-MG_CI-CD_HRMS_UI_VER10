using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll
{
    public interface IVariableDeductionService
    {
        public Task<VariableDeduction> VariableDeductionList(int Month, int Deduction,Filter filter);
        public Task<ResponseEntity> VariableDeductionBatchUpdate(VariableDeduction model);
        public Task<ResponseEntity> EmployeeWiseVariableDeductionSave(VariableDeduction model);
        Task<DataSet> VariableDeductionList_Excel(int Month, int Deduction, Filter filter);
        public Task<ResponseEntity<List<VariableDeduction>>> ImportFromExcel(VariableDeduction model);

        //----------------------Variable Deduction Upload Month wise Only
        public Task<VariableDeduction> GetVariableDeductionUploadList(int Month);
        public Task<ResponseEntity<List<VariableDeduction>>> ImportFromExcelMultiple(VariableDeduction model);

    }
}
