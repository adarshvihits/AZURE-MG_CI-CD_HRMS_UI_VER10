using Hrms.Lite.Services.IServices.TimeOffice.Report;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice.Report;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.TimeOffice.Report
{
   public interface ILeaveRegisterService
    {
        public Task<LeaveRegister> GetGridSettings();
        public Task<LeaveRegister> GetLeaveRegisterReports(int LeavePeriod,int TabIndex, LeaveRegister model);
        Task<DataSet> GetLeaveRegisterReports_Excel(int LeavePeriod,int TabIndex, LeaveRegister model);

        ///Annual Leave Register
        ///
        public Task<AnnualLeaveRegister> AnnualLeaveRegister(int FinancialYearCode, int EmployeeCode, Filter Filter);
        public Task<DataSet> AnnualLeaveRegisterExcel(int FinancialYearCode, int EmployeeCode, Filter Filter);


    }
}
