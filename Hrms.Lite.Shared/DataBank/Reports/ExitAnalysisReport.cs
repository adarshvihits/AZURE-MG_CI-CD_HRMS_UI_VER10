using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.Reports
{
    public class ExitAnalysisReport
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public Filter filter { get; set; }

        public List<ExitAnalysisReport> ExitAnalysisReportList { get; set; }

        public int ClassCount { get; set;  }
        public string Classification { get; set; }

        public int EmployeeCode { get; set; }

        public Guid EmployeeGI { get; set; }
        public Guid DesignationGI { get; set; }
        public Guid DepartmentGI { get; set; }
        public Guid LocationGI { get; set; }
        public Guid ResignationReasonGI { get; set; }
        public Guid GI { get; set; }

        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public DateTime? DOJ { get; set; }
        public DateTime? LWD { get; set; }

        public string Type { get; set; }
    }
}
