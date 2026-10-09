using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.PayRoll.Reports
{
    public class PTReportStatutory
    {
        public int PTGroupMonth { get; set; }
        public Filter Filter { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public int BasicPay { get; set; }
        public int SpecialPay { get; set; }
        public int DA { get; set; }
        public int Bonus { get; set; }
        public int Earnedleave { get; set; }
        public int Washing { get; set; }
        public int Total { get; set; }
        public int Half { get; set; }
        public List<PTReportStatutory> PTReportStatutoryList { get; set; }
        public int Month { get; set; }
        public Url Url { get; set; }
    }
}
