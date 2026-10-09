using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.PayRoll
{
    public class SalaryRecovery
    {
        public Guid EmployeeGI { get; set; }
        public string EmployeeID { get; set; }
        public int EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public Allowance Allowance { get; set; }
        public Filter filter { get; set; }
        public int Month { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Division { get; set; }
        public string WageGrade { get; set; }
        public string Location { get; set; }
        public decimal Amount { get; set; }
        public string AmountStr { get; set; }
        public decimal TotalAmount { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public List<SalaryRecovery> SalaryRecoveryList { get; set; }
        public File Excel { get; set; }
        public string Message { get; set; }
    }



}
