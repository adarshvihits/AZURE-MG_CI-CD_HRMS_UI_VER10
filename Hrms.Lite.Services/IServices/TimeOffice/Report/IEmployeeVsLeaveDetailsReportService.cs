using Hrms.Lite.Shared.TimeOffice.Report;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.TimeOffice.Report
{
    public interface IEmployeeVsLeaveDetailsReportService
    {
        public Task<LeaveRegister> GetEmployeeVsLeaveReports(int LeavePeriod, int EmployeeCode);
        public Task<LeaveRegister> GetEmployeeVsLeaveReportDetails(int LeavePeriod, int EmployeeCode, int LeaveCode);
        Task<DataSet> GetEmployeeVsLeaveReports_Excel(int LeavePeriod, int EmployeeCode);
        Task<DataSet> GetEmployeeVsLeaveDetailsReports_Excel(int LeavePeriod, int EmployeeCode, int LeaveCode); 
    }
}
