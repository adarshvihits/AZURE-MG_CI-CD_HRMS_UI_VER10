using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll.Generic.Master;
using Hrms.Lite.Shared.DataBank.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.TDS
{
    public class InvestmentDeclaration
    {
        public EmployeeHeader EmployeeHeader { get; set; }
        public string TabIndex { get; set; }
        public string SliderName { get; set; }
        public Filter filter { get; set; }
        public MasterBase AssessmentYear { get; set; }
        public Guid EmployeeGuid { get; set; }
        public string Type { get; set; }
        public string TrnRef { get; set; }
        public int EmployeeCode { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public bool RentalCount { get; set; }
        public bool DedCount { get; set; }
        public bool DonCount { get; set; }
        public bool ChapterCount { get; set; }
        public bool RepayCount { get; set; }
        public bool LetoutCount { get; set; }
        public bool OtherCount { get; set; }
        public string RentalStatus { get; set; }
        public string DedStatus { get; set; }
        public string DonStatus { get; set; }
        public string ChapterStatus { get; set; }
        public string RepayStatus { get; set; }
        public string LetoutStatus { get; set; }
        public string OtherStatus { get; set; }
        public string LICStatus { get; set; }
        public string EntryFrom { get; set; }
        public InvestmentDeclaration()
        {
            EntryFrom = "HRMS";
        }
        public List<InvestmentDeclaration> TDSEmployeeList { get; set; }
        public List<HouseRentalData> HouseRentalData { get; set; }
        public List<LandlordDetails> LandlordList { get; set; }
        public LandlordDetails LandlordDetails { get; set; }
        public List<LICDetails> LICActualList { get; set; }
        public List<LICDetails> LICList { get; set; }
        public LICDetails LICDetails { get; set; }
        public ChapterVIADetails ChapterVIADetails { get; set; }
        public List<DeductionDetails> EightyCDeductionList { get; set; }

        public List<DonationDetails> DonationList { get; set; }
        public List<HLRepayDetails> HLRepayActualList { get; set; }
        public List<HLRepayDetails> HLRepayList { get; set; }
        public HLRepayDetails HLRepayDetails { get; set; }
        public List<LetOutDetails> LetOutActualList { get; set; }
        public List<LetOutDetails> LetOutList { get; set; }
        public LetOutDetails LetOutDetails { get; set; }
        public List<OtherIncomeDetails> OtherIncomeActualList { get; set; }
        public List<OtherIncomeDetails> OtherIncomeList { get; set; }
        public OtherIncomeDetails OtherIncomeDetails { get; set; }
        public PANDetails PANDetails { get; set; }
        public List<DocumentList> DocumentList { get; set; }
        public string ApproveReject { get; set; }
        public int Rowid { get; set; }
        public string ApproverComments { get; set; }

    }
    public class DocumentList
    {
        public int TrnCode { get; set; }
        public Guid DeclarationDocumentGI { get; set; }
        public string FileName { get; set; }
        public string ActualFileName { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Type { get; set; }
        public string TrnRef { get; set; }
        public File Attachment { get; set; }
        public string AbsoluteUri { get; set; }

    }
    public class PANDetails
    {
        public string PANNumber { get; set; }
    }
        public class HouseRentalData
    {
        public int TrnCode { get; set; }
        public int AssessmentYearCode { get; set; }
        public int EmployeeCode { get; set; }
        public int SalaryPeriodCode { get; set; }
        public string Month { get; set; }
        public int Amount { get; set; }
        public Dropdown MetroCity { get; set; }
        public int CityCode { get; set; }
        public string MetroNonMetro { get; set; }
        public string OtherCityName { get; set; }
        public int ApprvdAmount { get; set; }


    }
    public class LandlordDetails
    {
        public int TrnCode { get; set; }
        public int Rowid { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public PayrollMonth From { get; set; }
        public PayrollMonth To { get; set; }
        public string Pan { get; set; }
        public int RentalDocCount { get; set; }
        public string ApprovalStatus { get; set; }
        public string StatusColor { get; set; }


    }
    public class LICDetails
    {
        public int TrnCode { get; set; }
        public string Name { get; set; }
        public string RelationShipName { get; set; }
        public Dropdown RelationShip { get; set; }
        public DateTime PolicyTakenDate { get; set; }
        public string PolicyNo { get; set; }
        public decimal PremiumAmount { get; set; }
        public decimal SumAssured { get; set; }
        public decimal PremiumAmountApp { get; set; }
        public decimal SumAssuredApp { get; set; }
        public string Remarks { get; set; }
        public int LICDocCount { get; set; }
        public int flag { get; set; }
        public string ApprovalStatus { get; set; }
        public string StatusColor { get; set; }

    }
    public class DeductionDetails
    {
        public int TrnCode { get; set; }
        public int EmployeeCode { get; set; }
        public int InvestmentCode { get; set; }
        public string InvestmentName { get; set; }
        public decimal Amount { get; set; }
        public decimal ApprovedAmount { get; set; }
        public int DedDocCount { get; set; }
        public string ApprovalStatus { get; set; }
        public string StatusColor { get; set; }
        public int flag { get; set; }
    }
    public class OtherIncomeDetails
    {
        
        public int TrnCode { get; set; }
        public string NatureofIncome { get; set; }
        public decimal TdsorTcs { get; set; }
        public decimal Amount { get; set; }
        public int OIDocCount { get; set; }
        public string ApprovalStatus { get; set; }
        public string StatusColor { get; set; }
        public int flag { get; set; }
    }

}
