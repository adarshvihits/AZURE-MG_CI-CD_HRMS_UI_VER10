
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice.Report;
using System;
using System.Data;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.TimeOffice.Report
{
  public  interface ILeaveApplicationReportService
    {
        public Task<LeaveApplicationReport> GetLeaveApplicationList(int currntLp,string TabIndex,string fromDate,string toDate, string Type, Filter filter);

        Task<DataSet> LeaveApplicationReport_Excel( int LeavePeriod, string TabIndex, string fromDate, string toDate, string Type, Filter filterdata);
        public Task<LeaveApplicationReport> GetTrackerDetails(Guid EmployeeGI, string LeaveType);
    }
}
