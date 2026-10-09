using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.TrainingModule.Report
{
    public class TrainingPostTestReport
    {       
        public string EmployeeID { get; set; }
        public string FullName { get; set; }
        public string Location { get; set; }
        public string Department { get; set; }
        public string Category { get; set; }
        public string Designation { get; set; }
        public DateTime? DOJ { get; set; }
        public decimal PercentageAcheived { get; set; }
        public string TestPassedStatus { get; set; }
        public UserBase User { get; set; }
        public int active { get; set; }
        public int totalRecords { get; set; }
        public Filter Filter { get; set; }
        public List<TrainingPostTestReport> TrainingPostTestReportList { get; set; }
        public Dropdown Training { get; set; }

    }
}
