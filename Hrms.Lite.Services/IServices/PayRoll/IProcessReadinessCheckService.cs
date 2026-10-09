using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;


namespace Hrms.Lite.Services.IServices.PayRoll
{
 public interface IProcessReadinessCheckService
    {
        public Task<ProcessReadinessCheck> GetProcessReadinessCheckList(int Month, int PayrollGroup);
        public Task<DataSet> GetProcessReadinessCheckList_Excel(int Month, int PayrollGroup);
        public Task<ProcessReadinessCheck> MonthlyAttendanceProcessingListPopUp(int Month, int PayrollGroup, int Tabindex, string Description, Filter filter);
        public Task<ProcessReadinessCheck> SalaryProcessReadinessCheckPopUp(int SalaryPeriodCode, int PayrollGroup, string TabName, string Description);    
     
        //public Task<DataSet> GetSalaryProcessReadinessCheckList_Excel(int Month, int PayrollGroup);
        public Task<ProcessReadinessCheck> GetBlankStatDaysPopList(Guid EmployeeGI, int Month, string Description);

    }
}
