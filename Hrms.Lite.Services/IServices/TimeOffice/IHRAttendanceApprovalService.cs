using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.TimeOffice
{
   public interface IHRAttendanceApprovalService 
    {
        Task<HRAttendanceApproval> HRAttendanceApprovalList(string TabIndex, int LeavePeriodCode, int Level, int Month, int AuthEmpCode,int PayrollCode, Filter filter);
      
        Task<ResponseEntity> HRAttendanceApprove(HRAttendanceApproval model);
        Task<ResponseEntity> HRAttendanceReject(HRAttendanceApproval model);
        Task<DataSet> HRAttendanceApprovalList_Excel(string TabIndex, int LeavePeriodCode, int Level, int Month,int PayrollCode, Filter filter);


    }
}
