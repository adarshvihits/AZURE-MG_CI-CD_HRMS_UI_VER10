using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll
{
    public interface ICostProcessingService
    {
        public Task<ResponseEntity<CostProcessing>> GetEmployeeVsCostCenterPendingList(int MonthCode, int PayrollGroup);
        public Task<DataSet> GetEmployeeVsCostCenterPendingList_Excel(int MonthCode, int PayrollGroup);

        Task<ResponseEntity> ProcessCost(int monthCode,int PayrollGroup);
 
    }
}
