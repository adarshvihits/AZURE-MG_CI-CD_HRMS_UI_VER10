
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.PayRoll
{
    public class VPFApproval
    {
        public int pendingCount { get; set; }
        public int approvedCount { get; set; }
        public int EmployeeCode { get; set; }
        public Guid EmployeeGI { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Function { get; set; }
        public string Location { get; set; }
        public DateTime ModifiedDate { get; set; }
        public decimal oldVpfAmount { get; set; }
        public decimal newVpfAmount { get; set; }
        public List<VPFApproval> VPFApprovalList { get; set; }
        public string Tab { get;set; }
        public Dropdown FinancialYear { get; set; }
        public bool Applicability { get; set; }
    }
}
