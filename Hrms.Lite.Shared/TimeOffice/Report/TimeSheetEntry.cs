using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.TimeOffice.Report
{
    public class TimeSheetEntry
    {
        public int HeadCode { get; set; }
        public DateTime? Date { get; set; }
        public string Day { get; set; }
        public AttendanceStatus AttendanceStatus { get; set; }
        public TimeSheetActivity TimeSheetActivity { get; set; }
        public TimeSheetProject TimeSheetProject { get; set; }
        public string TotalHours { get; set; }
        public string Description { get; set; }
        public string Remarks { get; set; }
        public string Mode { get; set; }
        public MasterBase ApprovalStatus { get; set; }
        public string EmployeeID { get; set; }
        public string FullName { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        public string Location { get; set; }
        public int OffDayStatus { get; set; }
        public int HolidayStatus { get; set; }
        public int PendingCount { get; set; }
        public int PrevHistoryCount { get; set; }
        public List<TimeSheetEntry> TimeSheetEntryList { get; set; }

        public DateTime? WorkDate { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public string ProjectName { get; set; }
        public string ActivityName { get; set; }
        public string EmpDescription { get; set; }
        public string ApprovalBy { get; set; }

        public int totalRecords { get; set; }
        public int active { get; set; }
        public Filter SliderFilter { get; set; }
    }
}
