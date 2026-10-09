using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.Reports
{
    public class ConfirmationTrackingReport
    {
        public Filter Filter { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public Guid EmployeeGI { get; set; }
        public int EmployeeCode { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string FuncDesignation { get; set; }
        public string Region { get; set; }
        public string RMStatus { get; set; }
        public string RmExtReason { get; set; }
        public decimal CurrSalary { get; set; }
        public string RMAuthority { get; set; }
        public string HrExeStatus { get; set; }
        public string HrExeExtReason { get; set; }
        public string HrExeAuthority { get; set; }
        public string Authority { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public List<ConfirmationTrackingReport> ConfirmationTrackingReportList { get; set; }
        public int HDCode { get; set; }
        public EmployeeHeader EmployeeHeader { get; set; }
        public List<EmpConfirmationDetails> EmpConfirmationDetailsList { get; set; }
    }
    public class EmpConfirmationDetails
    {
        public string EmployeeID { get; set; }
        public string Authority { get; set; }
        public string Designation { get; set; }
        public string Level { get; set; }
        public string Action { get; set; }
        public string Remarks { get; set; }
        public DateTime? ActionTime { get; set; }
    }
}

