using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.TimeOffice
{
    public class EmployeeVsShiftAllowance
    {
     
        public int Shift { get; set; }
        public int EmployeeCode { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public Guid EmployeeGI { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public decimal? Amount { get; set; }

        public DateTime DOJ { get; set; }
        public General.Filter Filter { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public List<EmployeeVsShiftAllowance> EmployeeVsShiftAllowanceList { get; set; }
    }
}
