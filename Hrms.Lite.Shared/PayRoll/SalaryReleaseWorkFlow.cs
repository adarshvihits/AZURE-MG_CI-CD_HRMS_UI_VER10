using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.PayRoll
{
    public class SalaryReleaseWorkFlow
    {
        public Filter Filter { get; set; }
        public int TabIndex { get; set; }
        public int PendingCount { get; set; }
        public int ConfirmedCount { get; set; }
        public int EmployeeCode { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Location { get; set; }
        public string Designation { get; set; }
        public string FirstApprover { get; set; }
        public string SecondApprover { get; set; }
        public decimal Amount { get; set; }
        public List<SalaryReleaseWorkFlow> SalaryReleaseWorkFlowList { get; set; }
        public Dropdown PayrollMonth { get; set; }
    }
}
