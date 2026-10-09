using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.TimeOffice.Report
{


   public class LivePunchReport
    {


        public int empCode { get; set; }
        public Filter filter { get; set; }
        public string Date { get; set; }
        public String EmployeeID { get; set; }
        public String FullName { get; set; }
        public String DEPARTMENT { get; set; }
        public String DIVISION { get; set; }
        public String SECTION { get; set; }
        public String LOCATION { get; set; }
        public String PAYROLLGROUP { get; set; }
        public String SHIFT { get; set; }
        public String Time { get; set; }
        public String PunchStatus { get; set; }
        public List<LivePunchReport> LivePunchReportList { get; set; }


    }
}
