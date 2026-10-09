using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.Reports
{
    public class EConsoleSubmissionReport
    {
        public Filter Filter { get; set; }
        public int EmployeeCode { get; set; }
        public string ID { get; set; }
        public string EmployeeName { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public string PDF { get; set; }
        //public int EmployeeCode { get; set; }

        public int ConsoleCode { get; set; }

        public DateTime SubmittedDate { get; set; }
        
        public Dropdown Console { get; set; }

        public List<EConsoleSubmissionReport> EConsoleSubmissionReportList { get; set; }



    }
}
