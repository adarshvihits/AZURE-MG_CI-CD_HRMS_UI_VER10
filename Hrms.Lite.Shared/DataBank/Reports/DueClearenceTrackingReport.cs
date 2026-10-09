using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.Reports
{
    public class DueClearenceTrackingReport
    {
        public Filter Filter { get; set; }
        public int UserCode { get; set; }
        public int GcCode { get; set; }
        public Url Url { get; set; }

        public string EmployeeID { get; set; }
        public Guid EmployeeGI { get; set; }
        public string EmployeeName { get; set; }
        public int EmployeeCode { get; set; }

        public int ResignationCode { get; set; }
        public Guid ResignationGI { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public DateTime? LastWorkingdate { get; set; }
        public DateTime? ResignationDate { get; set; }
        public int UserLevel { get; set; }
        public string FunctionalDesignation { get; set; }
        public int L1 { get; set; }
        public int L2 { get; set; }
        public int L3 { get; set; }
        public int L4 { get; set; }
        public int L5 { get; set; }
        public int L6 { get; set; }
        public int L7 { get; set; }
        public int L8 { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public string PDF { get; set; }

        public List<DueClearenceTrackingReport> DueClearenceList { get; set; }
    }
}

