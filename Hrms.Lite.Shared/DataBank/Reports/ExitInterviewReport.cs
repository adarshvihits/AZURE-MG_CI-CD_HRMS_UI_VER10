using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.Reports
{
    public class ExitInterviewReport
    {
        public int ResignationCode { get; set; }
        public Guid ResignationGI { get; set; }
        public int EmployeeCode { get; set; }
        public Guid EmployeeGI { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public string Category { get; set; }
        public string LWD { get; set; }
        public string EntryFrom { get; set; }
        public string Stage { get; set; }
        public string EmployeeAbsoluteUri { get; set; }
        public List<ExitInterviewReport> ExitInterviewReportList { get; set; }
        public Filter filter { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public UserBase User { get; set; }
        public List<ExitInterviewReportPopUpDetails> ExitInterviewReportPopUpDetails { get; set; }

    }
    public class ExitInterviewReportPopUpDetails
    {
        public int SlNo { get; set; }
        public string Question { get; set; }
        public string Type { get; set; }
        public string Result { get; set; }
        public string Remarks { get; set; }
    }
}
