using Hrms.Lite.Shared;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice;
using Hrms.Lite.Shared.TimeOffice.Generic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.TimeOffice
{
    public interface ILeaveApplicationService
    {
        Task<LeaveApplication> LeaveApplicationEmployeeList(Filter filter);
        public Task<List<Shared.TimeOffice.Generic.LeaveBalance>> GetLeaveBalance(int leavePeriodCode,int employeeCode);
        public Task<LeaveDetails> GetLeaveDayWiseDetails(LeaveApplication model, Guid EmployeeGI);
        public Task<ResponseEntity> SaveLeave(LeaveApplication model);
        Task<LeaveApplication> GetLeaveApplicationList(int leavePeriodCode, int leaveType, string TabIndex,Guid EmployeeGI);
        Task<LeaveApplication> GetTrackerDetails(Guid LeaveApplicationGI);
        Task<DataSet> LeaveApplication_Excel(Filter filter);
        Task<File> DownloadDoc(Guid RequestGI,string LeaveTypeName);

    }
}
