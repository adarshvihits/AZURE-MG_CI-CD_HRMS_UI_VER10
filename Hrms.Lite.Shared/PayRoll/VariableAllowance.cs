using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using Hrms.Lite.Shared.PayRoll.Generic.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.PayRoll
{
   public class VariableAllowance
    {

  
        public int EmployeeCode { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public Guid EmployeeGI { get; set; }
        public Allowance Allowance { get; set; }
        public PayrollMonth AllowanceMonth { get; set; }
        public Designation Designation { get; set; }
        public Department Department { get; set; }
        public Location Location { get; set; }
        public decimal? Amount { get; set; }
        public string AmountStr { get; set; }
        public string Mode { get; set; }
        public string Message { get; set; }
        public VariableAllowanceBatchUpDate VariableAllowanceBatchUpDate { get; set; }
        public List<VariableAllowance> VariableAllowanceList { get; set; }
        public File Excel { get; set; }
        public Filter Filter { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public int EmployeeswithAmount { get; set; }
        public int NoofEmployees { get; set; }
        public decimal TotalTDSAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public string Remark { get; set; }
        public bool Append { get; set; }
        public decimal? TDSAmount { get; set; }
    
    }
    public class VariableAllowanceBatchUpDate
    {

        public decimal Amount { get; set; }
        public Allowance Allowance { get; set; }
        public PayrollMonth AllowanceMonth { get; set; }
        public decimal AmountBatchUpdate { get; set; }
    }
}
