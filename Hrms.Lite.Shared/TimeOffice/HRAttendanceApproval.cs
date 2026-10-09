using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.TimeOffice
{
   public class HRAttendanceApproval
    {
         public string Tabindex { get; set; }
         public string FirstTab { get; set; }
        

        public int AllCount { get; set; }
        public int LeaveCount { get; set; }
        public int OnDutyCount { get; set; }
        public int COFFCount { get; set; }
        public int WFHCount { get; set; }
        public int PermissionHoursCount { get; set; }
        public int MaternityLeaveCount { get; set; }
        public int AttendanceRegularizationCount { get; set; }
        public int PreviousHistoryCount { get; set; }
        public int AuthorityChangeCount { get; set; }
        public List<HRAttendanceApprovalList> HRAttendanceApprovalList { get; set; }
         public Filter Filter { get; set; }
        public MasterBase LeavePeriod { get; set; }
        public MasterBase Month { get; set; }
        public MasterBase PayrollGroup { get; set; }
        public int AuthorityLevelCode { get; set; }
        public int AuthorityCode { get; set; }
      public string Remarks { get; set; }
        public string Mode { get; set; }

    }
    public class HRAttendanceApprovalList
    {
        public int EmployeeCode { get; set; }
        public string ID { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public string Type { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public string NoOfDays { get; set; }
        public int Aging { get; set; }
        public int AttendanceStatusTypeCode { get; set; }
        public string LeaveType { get; set; }
        public string Status { get; set; }
        public Guid LeaveApplicationGI { get; set; }
        public bool Applicable { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public string Authority { get; set; }
        public string LeaveReason { get; set; }
    }
}
