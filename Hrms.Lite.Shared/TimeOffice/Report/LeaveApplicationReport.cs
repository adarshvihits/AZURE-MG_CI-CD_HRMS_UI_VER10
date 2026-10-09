using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using Hrms.Lite.Shared.TimeOffice.Generic;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.TimeOffice.Report
{
    public class LeaveApplicationReport
    {
        public int AllCount { get; set; }
        public int LeaveCount { get; set; }
        public int OnDutyCount { get; set; }
        public int COFFCount { get; set; }
        public int WFHCount { get; set; }
        public int AttendanceRegularizationCount { get; set; }
        public int PermissionHourCount { get; set; }

        public List<LeaveApplicationReportList> LeaveApplicationReportList { get; set; }
        public Filter filter { get; set; }
        public LeaveApplication LeaveApplication { get; set; }
        public List<LeaveApplicationReportMyTeamList> LeaveApplicationReportMyTeamList { get; set; }
        public List<LeaveApplicationReportLeaveBalance> LeaveApplicationReportLeaveBalance { get; set; }
        public LeavePeriod LeavePeriod { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }

    }
    public class LeaveApplicationReportList
    {
        public string ID { get; set; }
        public string EmployeeName { get; set; }
        public DateTime AppliedOn { get; set; }
        public string Type { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public string NoOfDays { get; set; }
        public string Reason { get; set; }
        public int Aging { get; set; }
        public int AttendanceStatusTypeCode { get; set; }
        public string LeaveType { get; set; }
        public string Status { get; set; }
        public Guid LeaveApplicationGI { get; set; }
        public string EntitleDate { get; set; }
        public DateTime LeaveOn { get; set; }
        public Guid EmployeeGI { get; set; }
        public int active { get; set; }
        public int totalRecords { get; set; }
        public string StatusFrom { get; set; }

        public string Remarks { get; set; }

        public string ApprovedBy { get; set; }
        public string ApprovedFunDesg { get; set; }
        public DateTime? ApprovedDate { get; set; }
    }
    public class LeaveApplicationReportMyTeamList
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string LeaveType { get; set; }
        public decimal TotalDays { get; set; }
        public EmployeeHeader employeeHeader { get; set; }
    }
    public class LeaveApplicationReportLeaveBalance
    {
        public string LeaveType { get; set; }
        public decimal Taken { get; set; }
        public decimal Balance { get; set; }
        public decimal Applied { get; set; }

    }


}
