using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.PayRoll.Reports
{
    public class PF15GReport
    {
        public UserBase User { get; set; }
        public int Month { get; set; }
        public int PFGroup { get; set; }
        public Filter Filter { get; set; }
        public int EmployeeCode { get; set; }
        public List<PF15GSummary> PF15GSummary { get; set; }
        public Url Url { get; set; }
        public string Place { get; set; }
        public string Date { get; set; }
    }
    public class PF15GSummary
    {
        public int SlNo { get; set; }
        public int EmployeeCode { get; set; }
        public string EmployeeID { get; set; }
        public string FullName { get; set; }
        public string dsgName { get; set; }
        public string dptName { get; set; }
        public string locName { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }


    }
}
