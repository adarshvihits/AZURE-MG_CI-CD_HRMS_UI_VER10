using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.Reports
{
	public class ExitCheckListReport
	{
        public Guid EmployeeGI { get; set; }
        public int EmployeeCode { get; set; }
        public string ID { get; set; }
        public string VerifiedUser { get; set; }
        public DateTime EntryDate { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public string Category { get; set; }
        public string LWD { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }

        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public Filter filter { get; set; }
        public List<ExitCheckListDetails> ExitCheckListDetails { get; set; }
        public List<ExitCheckListReport> ExitCheckListReports { get; set; }
    }
    public class ExitCheckListDetails
    {
        public int SlNo { get; set; }
        public string Item { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
    }
}
