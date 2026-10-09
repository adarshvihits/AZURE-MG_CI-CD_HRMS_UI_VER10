using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.TimeOffice.Report
{
    public class LessHourReport
    {
        public UserBase User { get; set; }
        public decimal AllCount { get; set; }
        public int TotalRows { get; set; }
        public int TabIndex { get; set; }
        public Filter Filter { get; set; }
        public List<LessHourReportList> LessHourReportList { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

    }
    public class LessHourReportList
    {
        public int SLNo { get; set; }
        public int EmployeeCode { get; set; }
        public Guid EmployeeGI { get; set; }
        public string EmployeeID { get; set; }
        public string FullName { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        public string Location { get; set; }
        public string Shift { get; set; }
        public string PunchingDate { get; set; }
        public string IN { get; set; }
        public string LateIn { get; set; }
        public string EarlyIn { get; set; }
        public string OUT { get; set; }
        public string EarlyOut { get; set; }
        public string LateOut { get; set; }
        public string OverTime { get; set; }
        public string TotalTime { get; set; }
        public string InShiftTime { get; set; }
        public string Session { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public string TotalLessHours { get; set; }

    }
}
