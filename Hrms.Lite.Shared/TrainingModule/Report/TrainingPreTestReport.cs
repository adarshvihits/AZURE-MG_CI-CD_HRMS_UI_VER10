using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.TrainingModule.Report
{
   public class TrainingPreTestReport
    {

        public int Training { get; set; }
        public string EmployeeID { get; set; }
        public string FullName { get; set; }
        public string Location { get; set; }
        public string Department { get; set; }
        public string Category { get; set; }
        public string Designation { get; set; }
        public DateTime? DOJ { get; set; }
        public decimal PercentageAcheived { get; set; }
        public string TestPassedStatus { get; set; }
        public List<TrainingPreTestReport_Report> PreTestReport { get; set; }

        public List<TrainingPreTestReport> TrainingPreTestReportList { get; set; }
      
    }
    public class TrainingPreTestReport_Report
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
        public int active { get; set; }
        public int totalRecords { get; set; }


    }

}
