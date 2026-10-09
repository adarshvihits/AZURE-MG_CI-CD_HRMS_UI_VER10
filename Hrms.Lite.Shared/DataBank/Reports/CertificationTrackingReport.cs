using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;


namespace Hrms.Lite.Shared.DataBank.Reports
{
    public class CertificationTrackingReport
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
        public DateTime ApplyDate { get; set; }
        public DateTime ExamDate { get; set; }
        public string Exam { get; set; }
        public string ExamStatus { get; set; }
        public string IsCpeProgram { get; set; }
        public string Level { get; set; }
        public string Type { get; set; }
        public Decimal ExamFee { get; set; }
        public string Status { get; set; }
        public string ClarityPending { get; set; }
        public string Authority { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public List<CertificationTrackingReport> CertificationTrackingReportList { get; set; }
        public int HDCode { get; set; }
        public string RequestID { get; set; }
        public string RegisterNo { get; set; }
        public EmployeeHeader EmployeeHeader { get; set; }
        public List<EmpCertificationDetails> EmpCertificationDetailsList { get; set; }
    }
    public class EmpCertificationDetails
    {
        public string EmployeeID { get; set; }
        public string Authority { get; set; }
        public string Designation { get; set; }
        public string Level { get; set; }
        public string Action { get; set; }
        public string Comments { get; set; }
        public DateTime? ActionTime { get; set; }
    }
}
