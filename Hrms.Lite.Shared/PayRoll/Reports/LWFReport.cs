
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.PayRoll.Reports
{
    public class LWFReport
    {
        public int Month { get; set; }
        public int Deduction { get; set; }
        public Filter Filter { get; set; }
        public Url Url { get; set; }
        public string ELWF { get; set; }
        public string MLWF { get; set; }
        public List<LWFReport> LWFReportList { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Location { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        public int EmployeeContribution { get; set; }
        public int EmployerContribution { get; set; }

    }



}
