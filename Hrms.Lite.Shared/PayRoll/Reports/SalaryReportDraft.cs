using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.PayRoll.Reports
{
   public class SalaryReportDraft
    {
      
        public PayrollGroup PayrollGroup { get; set; }
        public MasterBase PayrollMonth { get; set; }

        public Filter Filter { get; set; }
        public NetPaySplitUp NetPaySplitUp { get; set; }
        public List<SalaryDraft> SalaryDraft { get; set; }
    }
    public class SalaryDraft
    {
        public EmployeeHeader EmployeeHeader { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public decimal LOPDays { get; set; }
        public decimal PaidDays { get; set; }
        public decimal ActualGross { get; set; }
        public decimal LOPAmount { get; set; }
        public decimal EarnedGross { get; set; }
        public decimal TotalDeduction { get; set; }
        public decimal HoldAmount { get; set; }
        public decimal NetPay { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
    }
    public class NetPaySplitUp
    {
        public EmployeeHeader EmployeeHeader { get; set; }
        public string Month { get; set; }
        public string PayrollGroup { get; set; }
        public string DOJ { get; set; }
        public string Category { get; set; }
        public string Section { get; set; }
        public string Grade { get; set; }
        public string LastWrkDay { get; set; }
        public decimal TotLeaveDay { get; set; }
        public decimal PayDay { get; set; }
        public decimal LOPDay { get; set; }
        public decimal PrevLOPDay { get; set; }
        public decimal LJP { get; set; }
        public decimal LTP { get; set; }
        public decimal PrevMonthArrear { get; set; }
        public decimal LessHRDay { get; set; }
        public decimal Others { get; set; }
        public decimal LessHRAmount { get; set; }
        public decimal OTAmount { get; set; }
        public decimal NetPay { get; set; }
        public decimal PaAmt { get; set; }
        public decimal PaOrgAmt { get; set; }
        public decimal DedAmt { get; set; }
        public List<AllowanceDetails> AllowanceDetails { get; set; }

    }
    public class AllowanceDetails
    {
        public string Earnings { get; set; }
        public decimal ActualAmount { get; set; }
        public decimal EarnedAmount { get; set; }
        public string Deduction { get; set; }
        public decimal Amount { get; set; }

    }
}
