using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.PayRoll
{
    public class ParentsMediclaimEMI
    {
        public Filter Filter { get; set; }
        public Dropdown FinancialYear { get; set; }
        public Dropdown SalaryPeriod { get; set; }
        public Guid EmployeeGi { get; set; }
        public int EmployeeCode { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public decimal PremiumAmount { get; set; }
        public int Tenure { get; set; }
        public decimal EMIAmount { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public List<ParentsMediclaimEMI> ParentsMediclaimEMIList { get; set; }
    }
}
