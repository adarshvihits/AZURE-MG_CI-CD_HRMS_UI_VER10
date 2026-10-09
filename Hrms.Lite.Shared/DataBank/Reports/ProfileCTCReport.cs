using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.Reports
{
    public class ProfileCTCReport
    {
        public int Month { get; set; }
        public Filter Filter { get; set; }
        public List<ProfileCTCReportList> List { get; set; }
    }
    public class ProfileCTCReportList
    {
        public Guid EmployeeGI { get; set; }
       
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public string Region { get; set; }
        public string Category { get; set; }
        public int Gross { get; set; }
        public int Perks { get; set; }
        public int PFMC { get; set; }
        public int ESIMC { get; set; }
        public int CTC { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }


    }
}
