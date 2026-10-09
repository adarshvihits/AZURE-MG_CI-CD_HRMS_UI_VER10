using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.Reports
{
    public class SeparationReport
    {
        
        public List<SeparationReportList> SeparationReportList { get; set; }
        public List<EmployeeProfileSummary> EmployeeProfileSummary { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public GroupByComboFill GroupBy { get; set; }
        public string GroupByName { get; set; }
        public Filter Filter { get; set; }

        public string ProfileFilename { get; set; }
        public string Base64string { get; set; }
    }
    public class SeparationReportList
    {
        public Guid EmployeeGI { get; set; }
        public string AbsoluteUri { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public string Category { get; set; }
        public DateTime? DOJ { get; set; }
        public DateTime? ResignationDate { get; set; }
        public DateTime? LWD { get; set; }
        public Decimal GrossPay { get; set; }
        public string YOS { get; set; }
        public string SeparationType { get; set; }
        public string Reason { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public string Remarks { get; set; }
        public string AcceptanceRemarks { get; set; }
        public string RelievingRemarks { get; set; }
        public string WorkHandoverDetails { get; set; }

        public string ProfileFilename { get; set; }
        public string Base64string { get; set; }
        public string Filename { get; set; }
    }
}