using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.PayRoll
{
  public  class SalaryEdit
    {
        public EmployeeHeader EmployeeHeader { get; set; }
        public int PayrollGroup { get; set; }
        public Filter filter { get; set; }
        public int Month { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public string Designation { get; set; }
        public string Grade { get; set; }
        public decimal GrossAllowance { get; set; }
        public decimal GrossDeduction { get; set; }
        public decimal Advance { get; set; }
        public decimal Loan { get; set; }
        public decimal NetAmount { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public decimal TotalAmount { get; set; }
        public List<SalaryEdit> SalaryEditList { get; set; }

        public List<SalaryEditDetails> SalaryEditDetailsList { get; set; }
    }
    public class SalaryEditDetails
    {
        public int DescriptionCode { get; set; }
        public string Description { get; set; }
        public string RowType { get; set; }
        public decimal Total { get; set; }
        public int Editable { get; set; }
        public string Colour { get; set; }

    }
}
