using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.PayRoll.Reports
{
    public class PFGroupSummaryReport
    {
        public int Month { get; set; }
        public int PFGroup { get; set; }
        public List<PFGroupSummary> PFGroupSummary { get; set; }
        public Filter SliderFilter { get; set; }
    
    }
    public class PFGroupSummary
    {
        public int SlNo { get; set; }
        public string Name { get; set; }
        public int EmpCount { get; set; }
        public decimal PFSalary { get; set; }
        public decimal EmployeeContribution { get; set; }
        public decimal PensionFund { get; set; }
        public decimal ERPF { get; set; }
        public decimal PFERCont { get; set; }
        public decimal PFAdmincharges { get; set; }
        public decimal EDLI { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
    }


}
