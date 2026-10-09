using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using Hrms.Lite.Shared.PayRoll.Generic.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.PayRoll
{
    public class LoanConfirmation
    {
        public UserBase User { get; set; }
        public int PendingCount { get; set; }
        public int ConfirmedCount { get; set; }
        public int EmployeeCode { get; set; }
        public int LoanIssueCode { get; set; }
        public int BalanceTenure { get; set; }
        public decimal LoanBalance { get; set; }
        public decimal MthlyEMI { get; set; }
        public decimal ConfAmount { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string LoanName { get; set; }
        public int LoanSettingsCode { get; set; }
        public string Remarks { get; set; }
        public Guid EmployeeGI { get; set; }
        public Guid LoanIssueGI { get; set; }
        public LoanIssueRequest LoanIssueRequest { get; set; }
       // public Deduction Deduction { get; set; }
        public PayrollMonth LoanMonth { get; set; }
        public Designation Designation { get; set; }
        public Department Department { get; set; }
        public Location Location { get; set; }
        public LoanSettings LoanType { get; set; }
        public decimal Amount { get; set; }
        public List<LoanConfirmation> LoanConfirmationList { get; set; }
        public EmployeeHeader EmployeeHeader { get; set; }
        public int TabValues { get; set; }
        public int LoanTypeCode { get; set; }
        public int MonthCode { get; set; }
        public string MODE { get; set; }
        public Filter Filter { get; set; }
    }
}
