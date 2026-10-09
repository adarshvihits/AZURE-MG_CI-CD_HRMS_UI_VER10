using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.PayRoll.Reports
{
    public class SalaryReconciliation
    {
        public MasterBase SelectedMonth { get; set; }
        public MasterBase CompareWith { get; set; }
        public MasterBase Status { get; set; }
        public MasterBase Employee { get; set; }
        public List<SalaryReconciliationDetails> SalaryReconciliationDetails { get; set; }
        public List<SalaryReconciliationEmployeeDetails> SalaryReconciliationEmployeeDetails { get; set; }
        public Filter Filter { get; set; }
        public int Code { get; set; }
        public string Type { get; set; }
        public int Month { get; set; }
        public int PayrollGroup { get; set; }
    }
    public class SalaryReconciliationDetails
    {
        public int Code { get; set; }
        public string Description { get; set; }
        public int PreviousEmpCount { get; set; }
        public decimal PreviousGross { get; set; }
        public decimal PreviousEarning { get; set; }
        public int CurrentEmpCount { get; set; }
        public decimal CurrentGross { get; set; }
        public decimal CurrentEarning { get; set; }
        public decimal Variance { get; set; }
        public int Remark { get; set; }
        public string Type { get; set; }
        public int EmpCount { get; set; }
        public int Salary { get; set; }
        public int BankCode { get; set; }
        public bool Link { get; set; }
    }
    public class SalaryReconciliationEmployeeDetails
    {
        public int totalRecords;
        public int active;

        public int EmployeeCode { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Location { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        public decimal PreviousEarning { get; set; }
        public decimal CurrentEarning { get; set; }
        public decimal Variance { get; set; }
        public decimal Salary { get; set; }
    }
}
