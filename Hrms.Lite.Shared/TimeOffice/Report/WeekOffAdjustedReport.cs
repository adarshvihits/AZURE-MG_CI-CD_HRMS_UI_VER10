using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.TimeOffice.Report
{
    public class WeekOffAdjustedReport
    {
        public Filter Filter { get; set; }
        public int Month { get; set; }
        public List<WeekOffAdjustedReport> WeekOffAdjustedReportList { get; set; }


        public Guid EmployeeGI { get; set; }
        public int EmployeeCode { get; set; }
        public string AbsoluteUri { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public string ActualPRDays { get; set; }
        public string OFFDAYS { get; set; }
        public string AllocatedOffDays { get; set; }
        public string AdjOffday { get; set; }


    }
}
