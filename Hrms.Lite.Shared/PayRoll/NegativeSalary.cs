using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.PayRoll
{
    public class NegativeSalary
    {
        public Designation Designation { get; set; }
        public Department Department { get; set; }
        public Location Location { get; set; }
        public int PayrollGrpCode { get; set; }
        public int MonthCode { get; set; }
        public Filter Filter { get; set; }
        public int EmployeeCode { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public Guid EmployeeGI { get; set; }
        public decimal GrossAlw { get; set; }
        public decimal GrossDed { get; set; }
        public decimal Advance { get; set; }
        public decimal Netsalary { get; set; }
        public string Remarks { get; set; }
        public List<NegativeSalary> NegativeSalaryList { get; set; }
    }
}
