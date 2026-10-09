using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.Reports
{
    public class SalaryPerDayReport
    {
        public Filter filter { get; set; }
        public int[] Allowance { get; set; }
        public int SalaryDays { get; set; }
        public int DividingFactor { get; set; }
        public Guid EmployeeGI { get; set; }
        public int EmployeeCode { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public int GrossPay { get; set; }
        public int CTC { get; set; }
        public int SalaryAsPerDay { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public List<SalaryPerDayReport> EmployeeSalaryPerDayList { get; set; }

    }
}
