using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.TimeOffice
{
  public  interface IMaternityLeaveApplicationService
    {
        Task<MaternityLeaveApplication> GetMaternityLeaveApplicationEmployeeList(Filter filter);
        Task<DataSet> GetMaternityLeaveApplicationEmployeeList_Excel( Filter filter);
        Task<MaternityLeaveApplication> GetTrackerDetails(Guid LeaveApplicationGI);
        Task<MaternityLeaveApplication> GetMaternityLeaveApplicationList(int leavePeriodCode, int leaveType, string TabIndex, Guid? EmployeeGI);
    }
}
