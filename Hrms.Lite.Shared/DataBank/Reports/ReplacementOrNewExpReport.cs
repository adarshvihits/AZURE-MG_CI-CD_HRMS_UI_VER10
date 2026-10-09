using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.Reports
{
    public class ReplacementOrNewExpReport
    {
        public Filter Filter { get; set; }
        public int EmployeeCode { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string GroupCompany { get; set; }
        public DateTime? DOJ { get; set; }
        public string FuncState { get; set; }
        public string Region { get; set; }
        public string Location { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        public string FuncDesignation { get; set; }
        public decimal MonthlyCTC { get; set; }
        public string ReplacementNewExpDetails { get;set;}
        public DateTime? LWD { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public List <ReplacementOrNewExpReport> ReplacementOrNewExpReportList { get; set; }
    }
}
