using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.Reports
{
    public class SourceDetailsReport
    {
        public Filter Filter { get; set; }
        public int EmployeeCode { get; set; }
        public string EmployeeID { get; set; }
        public string ApplicantID { get; set; }
        public string EmployeeName { get; set; }
        public DateTime? DOJ { get; set; }
        public string FuncState { get; set; }
        public string Region { get; set; }
        public string Location { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        public string FuncDesignation { get; set; }
        public string Industry { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public List<SourceDetailsReport> SourceDetailsReportList { get; set; }
        public string ResignationStatus { get; set; }
        public string SourceDetails { get; set; }
        public string Source { get; set; }
        public string GeojitState { get; set; }
    }
}
