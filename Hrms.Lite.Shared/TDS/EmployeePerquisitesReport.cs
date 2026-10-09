using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.TDS
{
    public class EmployeePerquisitesReport
    {
        public Filter filter { get; set; }
        public Dropdown AssessmentYear { get; set; }
        public Dropdown Month { get; set; }
        public Dropdown Perquisite { get; set; }
        public List<EmployeePerquisitesReport> EmployeePerquisitesReportList { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public string MonthName { get; set; }
        public string PerquisiteName { get; set; }
        public decimal Amount { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
    }
}
