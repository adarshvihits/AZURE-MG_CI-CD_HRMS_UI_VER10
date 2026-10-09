using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.PayRoll
{
    public class SalaryHold
    {
        public EmployeeHeader EmployeeHeader { get; set; }
        public int Month { get; set; }
        public int LogMonth { get; set; }
        public int ResignType { get; set; }
        public int BasedOn { get; set; }
        public int PayrollGroup { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public decimal GrossSalry { get; set; }
        public decimal GrossDeduction { get; set; }
        public decimal NetPay { get; set; }
        public decimal? HoldAmount { get; set; }
        public string AmountStr { get; set; }
        public string Remarks { get; set; }
        public List<SalaryHold> SalaryHoldList { get; set; }
        public List<SalaryUnHold_Log> SalaryUnHold_Log { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public bool showhold { get; set; }
        public Filter Filter { get; set; }
        public File Excel { get; set; }
        public string Message { get; set; }
        public string EmployeeId { get; set; }

        public string EmployeeName { get; set; }

        public string LastWorkingDate { get; set; }
    }
    public class SalaryUnHold_Log
    {
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public decimal Amount { get; set; }
        public string User { get; set; }
        public DateTime Date { get; set; }
        public string Reason { get; set; }

    }
}
