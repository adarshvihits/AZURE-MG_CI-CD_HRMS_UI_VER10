using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.PayRoll.Reports
{
   public class SalaryReleaseReport
    {
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public MasterBase PayrollGroup { get; set; }
        public string Location { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        public string HoldingAmt { get; set; }
        public int Month { get; set; }
        public string DOJ { get; set; }
        public string ReleaseAmount { get; set; }
        public string RecoveryAmt { get; set; }
        public Filter Filter { get; set; }
        public int active { get; set; }
        public int totalRecords { get; set; }
        public List<SalaryReleaseReport> SalaryReleaseReportList { get; set; }
    }
}
