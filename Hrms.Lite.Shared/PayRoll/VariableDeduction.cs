using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using Hrms.Lite.Shared.PayRoll.Generic.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.PayRoll
{
   public class VariableDeduction : MasterBase
    {
        
        public int EmployeeCode { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public Guid EmployeeGI { get; set; }
        public Deduction Deduction { get; set; }
        public PayrollMonth DeductionMonth { get; set; }
        public Designation Designation { get; set; }
        public Department Department { get; set; }
        public Location Location { get; set; }
        public decimal? Amount { get; set; }
        public string AmountStr { get; set; }
        public VariableDeductionBatchUpDate VariableDeductionBatchUpDate { get; set; }
        public List<VariableDeduction> VariableDeductionList { get; set; }
        public Filter Filter { get; set; }
        public int totalRecords { get; set; }
        public int activeRow { get; set; }
        public int EmployeeswithAmount { get; set; }
        public int NoofEmployees { get; set; }
        public decimal TotalAmount { get; set; }
        public File Excel { get; set; }
        public string Message { get; set; }
        public string Remark { get; set; }
        public bool Append { get; set; }
    }
    public class VariableDeductionBatchUpDate
    {

        public decimal Amount { get; set; }
        public Deduction Deduction { get; set; }
        public PayrollMonth DeductionMonth { get; set; }
        public decimal AmountBatchUpdate { get; set; }
    }

}

