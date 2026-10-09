using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.TDS
{
  public  class TDSProcessing
    {
      
          public Filter Filter { get; set; }
        public MasterBase AssessmentYear { get; set; }
        public MasterBase PayrollGroup { get; set; }
        public MasterBase PayrollMonth { get; set; }
        public bool Approved { get; set; }
        public List<TDSConfirmationDetails> TDSConfirmationDetails { get; set; }

    }
    public class TDSConfirmationDetails
    {
        public EmployeeHeader EmployeeHeader { get; set; }
        public string Regime { get; set; }
        public decimal ProcessedSalary { get; set; }
        public decimal ProjectedSalary { get; set; }
        public decimal GrossSalary { get; set; }
        public decimal TotExemption { get; set; }
        public decimal ProfessionalTax { get; set; }
        public decimal OtherIncome { get; set; }
        public decimal TaxableIncome { get; set; }
        public decimal Tax { get; set; }
        public decimal EightyC { get; set; }
        public decimal SixA { get; set; }
        public decimal Rebate { get; set; }
        public decimal Surcharge { get; set; }
        public decimal EduCess { get; set; }
        public decimal TotalTax { get; set; }
        public decimal TaxPaid { get; set; }
        public decimal BalTax { get; set; }
        public int RemainingMonths { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public decimal CurrentMonthTax { get; set; }

    }
    public class TDSEmployeeDetails
    {
        public EmployeeHeader EmployeeHeader { get; set; }
        public List<TDSComponentSplitUp> TDSComponentSplitUp_PopUp { get; set; }
        public List<Tax> TaxDetails { get; set; }
        public decimal TaxtoPay { get; set; }
        public decimal TaxDeductd { get; set; }
        public decimal BalTaxPayable { get; set; }
        public decimal TaxableIncme { get; set; }
        public decimal Tax { get; set; }
        public decimal EduCess { get; set; }
        public decimal TotTax { get; set; }

    }
    public class TDSComponentSplitUp
    {
        public int SlNo { get; set; }
        public string Head { get; set; }
        public decimal ProcessedSalary { get; set; }
        public decimal ProjectedSalary { get; set; }
        public decimal Total { get; set; }

    }
    public class Tax
    {
        public decimal Amount { get; set; }
        public decimal ITRate { get; set; }
        public decimal TDS { get; set; }
        public decimal EduCess { get; set; }
        public decimal Total { get; set; }

    }
}
