using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.MIS
{
    public class MISUserSettings:MasterBase
    {
        public int ActiveCount { get; set; }
        public int InactiveCount { get; set; }
        public List<MISUserSettings> MISUserDetailsList { get; set; }

        public int EmployeeCode { get; set; }
        public string EmployeeGI { get; set; }
        public string EmployeeID { get; set; }
        public string FullName { get; set; }
        public string DEPARTMENT { get; set; }
        public string DESIGNATION { get; set; }
        public string LOCATION { get; set; }
        public string CATEGORY { get; set; }
        public string GENDER { get; set; }
        public int Status { get; set; }
        public int TotalCount { get; set; }
    }
}
