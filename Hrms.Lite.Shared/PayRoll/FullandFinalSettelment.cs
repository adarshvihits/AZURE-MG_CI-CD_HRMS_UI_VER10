using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TDS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.PayRoll
{
    public class FullandFinalSettelment
    {
        public EmployeeHeader EmployeeHeader { get; set; }
        public Guid EmployeeGI { get; set; }
        public Guid FinalSettlementGI { get; set; }
        public string mode { get; set; }
        public Url Url { get; set; }
        public List<FullandFinalSettelment_EmployeeList> EmployeeList { get; set; }
        public FullandFinalSettelment_Details Details { get; set; }
        public List<FullandFinalSettelment_HeldAmountDetaisl> HeldAmountDetaisl { get; set; }
        public Filter filter { get; set; }
        public string Remarks { get; set; }
        public Dropdown PayrollGroup { get; set; }
        public Dropdown Month { get; set; }
        public Dropdown FromMonth { get; set; }
        public Dropdown ToMonth { get; set; }
        public DateTime? SettlementFrom { get; set; }
        public DateTime? SettlementTo { get; set; }
        public Filter Filter { get; set; }
        public List<string> Head { get; set; }
        public List<Row> Rows { get; set; }
        public List<string> Footer { get; set; }
        public List<Row> FooterRows { get; set; }
        public TDSEmployeeDetails TDSEmployeeDetails { get; set; }
    }
    public class FullandFinalSettelment_EmployeeList
    {
        public int EmployeeCode { get; set; }
        public Guid EmployeeGI { get; set; }
        public Guid FinalSettlementGI { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public DateTime ResignationDate { get; set; }
        public DateTime LWD { get; set; }
        public DateTime DOJ { get; set; }
        public DateTime SettlementDate { get; set; }
        public int HeldAmount { get; set; }
        public int RecoverableAmount { get; set; }
        public int NetPayable { get; set; }
        public string LastProcessingMonth { get; set; }
        public string Aging { get; set; }
        public int SalProcessingValidation { get; set; }
        public int AttnProcessingValidation { get; set; }
        public int totalRecords { get; set; }
        public int Active { get; set; }
        public string stage { get; set; }
        public string Remarks { get; set; }
        public string Permission { get; set; }
        public int MonthCode { get; set; }
    }
    public class FullandFinalSettelment_Details
    {
        public Guid EmployeeGI { get; set; }
        public DateTime ResignationDate { get; set; }
        public DateTime LWD { get; set; }
        public int NoticePeriod { get; set; }
        public int LopOrAbInNotice { get; set; }
        public decimal ExtraLeave { get; set; }
        public int EmployeeCode { get; set; }
        public int CompanyCode { get; set; }
        public int UserCode { get; set; }
        public int NoticeServedDays { get; set; }
        public decimal ShortfallDays { get; set; }
        public int RecoverableAmount { get; set; }
        public DateTime SettlementDate { get; set; }
        public int SettlementAging { get; set; }
        public decimal TotalEncashmentLeaves { get; set; }
        public int AdjustedShortfallDays { get; set; }
        public int NoticePeriodDeductionAfterAdjustment { get; set; }
        public string SalaryForTheMonth { get; set; }
        public int EarnedGross { get; set; }
        public int TotalDeduction { get; set; }
        public int NetPayable { get; set; }
        public int HeldAmount { get; set; }
 
        public int LeaveEncashment { get; set; }
        public int Gratuity { get; set; }
        public int NoticePeriodRefund { get; set; }
        public int NoticePeriodDeduction { get; set; }
        public decimal FinalSettlementAmountPayable { get; set; }
        public decimal TDSAmount { get; set; }
        public int LoanBalance { get; set; }
        public string Remarks { get; set; }
        public List<FullandFinalSettelment_LeaveEncashment> LeaveDetails { get; set; }
        public List<FullandFinalSettelment_OtherAmounts> OtherAdditions { get; set; }
        public List<FullandFinalSettelment_OtherAmounts> OtherDeduction { get; set; }
        public List<FullandFinalSettelment_SalaryDetails> Allowance { get; set; }
        public List<FullandFinalSettelment_SalaryDetails> Deduction { get; set; }
        public List<FullandFinalSettelment_LoanAmounts> Loan { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public string EntryComments { get; set; }

        public string VarifieddBy { get; set; }
        public string VarifiedOn { get; set; }
        public string VarificationComments { get; set; }


        public string ApproveddBy { get; set; }
        public string ApprovedOn { get; set; }
        public string ApproverComments { get; set; }
        public string NetPayableInWords { get; set; }
    }
    public class FullandFinalSettelment_LeaveEncashment
    {
        public string LeaveName { get; set; }
        public decimal LeaveCount { get; set; }
    }
    public class FullandFinalSettelment_OtherAmounts
    {
        public int TrnCode { get; set; }
        public int Code { get; set; }
        public string Narration { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
    }
    public class FullandFinalSettelment_SalaryDetails
    {

        public int Code { get; set; }
        public string Description { get; set; }
        public decimal ActualAmount { get; set; }
        public decimal Earning { get; set; }
    }
    public class FullandFinalSettelment_HeldAmountDetaisl
    {
        public string Month { get; set; }
        public int Amount { get; set; }
        public string Remarks { get; set; }
    }
    public class FullandFinalSettelment_LoanAmounts
    {

        public int TrnCode { get; set; }
        public int Code { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
    }
}
