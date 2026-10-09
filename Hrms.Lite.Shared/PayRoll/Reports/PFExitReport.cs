using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.PayRoll.Reports
{
    public class PFExitReport
    {
     
        public int Month { get; set; }
        public int PFGroup { get; set; }
        public Filter Filter { get; set; }
        public List<PFExitReportSummary> PFExitReportSummary { get; set; }
    }
    public class PFExitReportSummary
    {
        public int SlNo { get; set; }
        public int EmployeeCode { get; set; }
        public string EmployeeID { get; set; }
        public string FullName { get; set; }
        public string UAN { get; set; }
        public string DATE { get; set; }
        public string REASON { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }


    }
}
