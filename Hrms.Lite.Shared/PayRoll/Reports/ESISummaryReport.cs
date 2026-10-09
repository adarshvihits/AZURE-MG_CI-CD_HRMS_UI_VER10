using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.PayRoll.Reports
{
    public class ESISummaryReport
    {
        public int Month { get; set; }
        public int ESIGroup { get; set; }
        public List<ESISummary> ESISummary { get; set; }
        public Filter Filter { get; set; }
    }
    public class ESISummary
    {
        public Guid EmployeeGI { get; set; }
        public int EmployeeCode { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string ESINumber { get; set; }
        public decimal ActualESISalary { get; set; }
        public decimal ESISalary { get; set; }
        public decimal PaidDays { get; set; }
        public decimal EmployeeContribution { get; set; }
        public decimal EmployerContribution { get; set; }
        public decimal Total { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public int SlNo { get; set; }
    }
}
