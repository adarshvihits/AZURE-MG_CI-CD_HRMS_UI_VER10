using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.PayRoll
{
   
    public class EmployeeRequestLoanIssue
    {

        public int PayrollGroupCode { get; set; }
        //public int LoanTypeCode { get; set; }
        //public int MonthCode { get; set; }
        public int RequestCode { get; set; }
        public Guid RequestGI { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public Guid EmployeeGI { get; set; }
        public int DesignationCode { get; set; }
        public string Designation { get; set; }
        public int DepartmentCode { get; set; }
        public string Department { get; set; }
        public int LocationCode { get; set; }
        public string Location { get; set; }
        public DateTime RequestedDate { get; set; }
        public string LoanType { get; set; }
        public string Purpose { get; set; }
        public int LoanTypeCode { get; set; }
        public int MonthCode { get; set; }
        public decimal LoanAmount { get; set; }
        public int GrossSalary { get; set; }
        public int Tenure { get; set; }
        public decimal? MonthlyEMI { get; set; }
        public int RepaymentCode { get; set; }
        public string RepaymentMethod { get; set; }
        public Filter Filter { get; set; }
        public string Remarks { get; set; }
        public string AbsoluteUri { get; set; }
        public List<EmployeeRequestLoanIssue> LoanIssueList { get; set; }
        public string IssuedBy { get; set; }
        public int ConfirmedAmnt { get; set; }
        public List<LoanIssueTracker> LoanIssueTracker { get; set; }
        public File File { get; set; }
        public bool Applicable { get; set; }
        public string ApproveStatus { get; set; }
        public EmployeeHeader EmployeeHeader { get; set; }
    }

    public class LoanIssueTracker
    {
        public int RequestCode { get; set; }
        public Guid RequestGI { get; set; }
        public string Stage { get; set; }
        public string Authority { get; set; }
        public string IssueStatus { get; set; }
        public DateTime? EntryDate { get; set; }
    }

}
