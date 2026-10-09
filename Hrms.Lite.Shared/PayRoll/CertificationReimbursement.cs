using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.PayRoll
{
    public class CertificationReimbursement
    {
        public Filter Filter { get; set; }
        public Dropdown Month { get; set; }
        public Guid EmployeeGI { get; set; }
        public int EmployeeCode { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public Guid RequestGI { get; set; }
        public int RequestCode { get; set; }
        public string Location { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        public string Category { get; set; }
        public string CertificationName { get; set; }
        public decimal ExamFee { get; set; }
        public int allowance { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public List<CertificationReimbursement> CertificationReimbursementList { get; set; }
        public int PendingCount { get; set; }
        public int ConfirmedCount { get; set; }
        public int TabIndex { get; set; }
    }
}
