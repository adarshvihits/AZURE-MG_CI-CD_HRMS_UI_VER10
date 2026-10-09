using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.Reports
{
    public class HRPolicyAcceptanceReport
    {
        public List<string> Head { get; set; }
        public List<HRPolicyAcceptanceReportList> PolicyacceptanceReportList { get; set; }
        public int Acceptance { get; set; }
        public int PolicyOrEmp { get; set; }
        public int Type { get; set; }
        public Filter filter { get; set; }
        public EmployeeHeader EmployeeHeader { get; set; }
        public List<HRPolicy_GroupSummary> GroupSummary { get; set; }
    }
    public class HRPolicyAcceptanceReportList
    {

        public int EmployeeCode { get; set; }
        public string EmployeeID { get; set; }
        public Guid EmployeeGI { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public string PolicyCategory { get; set; }
        public string Policy { get; set; }
        public string AccepStatus { get; set; }
        public string AccepDate { get; set; }
        public DateTime? VisibleFrom { get; set; }
        public DateTime? VisibleTo { get; set; }
        public int active { get; set; }
        public int totalRecords { get; set; }

    }
    public class HRPolicy_GroupSummary
    {
        public string Policy { get; set; }
        public string PolicyCategory { get; set; }
        public int TotEmpCount { get; set; }
        public int Accepted { get; set; }
        public int NotAccepted { get; set; }

    }
    //----------------------------------------------------------------
    public class HrPolicy_AcceptanceReport
    {
        public string PolicyCode { get; set; }
        public string PolicyName { get; set; }
        public string Category { get; set; }
        public string VisibleFrom { get; set; }
        public string VisibleTo { get; set; }
        public string AcceptedOn { get; set; }
        public string EmployeeID { get; set; }
        public string Employee { get; set; }
        public string Location { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        public string Time { get; set; }
        public int totalRecords { get; set; }
        public Filter filter { get; set; }
        public int active { get; set; }
        public List<HrPolicy_AcceptanceReport> HrpolicyAcceptanceDetails { get; set; }

    }
}
