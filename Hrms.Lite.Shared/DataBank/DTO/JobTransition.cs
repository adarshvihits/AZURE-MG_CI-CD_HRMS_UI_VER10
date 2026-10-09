using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.DTO
{
    public class JobTransition
    {
        public string JobTransitionType { get; set; }
        public int Empcode { get; set; }
        public int HistoryCode { get; set; }
        public Guid HistoryGI { get; set; }
        public Guid EmployeeGI { get; set; }
        public EmployeeHeader EmployeeHeader { get; set; }
        public UserBase User { get; set; }
        public int Tabindex { get; set; }
        public int SearchId { get; set; }
        public int AllTabCount { get; set; }
        public int Tab1Count { get; set; }
        public int Tab2Count { get; set; }
        public int Tab3Count { get; set; }
        public int Tab4Count { get; set; }
        public int MoreCount { get; set; }
        public File Confidential { get; set; }
        public FilterSlabComboFill Filter { get; set; }
        public Filter SliderFilter { get; set; }
        public List<JobTransitionList> JobTransitionList { get; set; }
        public List<JobTransitionList> JobTransitionConfirmList { get; set; }
        public List<JobTransitionList> JobTransitionDueList { get; set; }
        public List<JobTransitionList> JobTransitionLogList { get; set; }
        public List<JobTransition_Tabs> JobTransition_Tabs { get; set; }
        public List<JobTransition_Tabs> JobTransition_Due_List_Tabs { get; set; }
        public List<JobTransition_GrowthHistory> GrowthHistory { get; set; }
        public List<Allowance> JobTransition_FixedAllowance { get; set; }
        public List<Allowance> JobTransition_VaiableAllowance { get; set; }
        public List<Deduction> JobTransition_FixedDeduction { get; set; }
        public List<Deduction> JobTransition_VariableDeduction { get; set; }
        public List<Provisions> JobTransition_FixedProvision { get; set; }
        public List<Provisions> JobTransition_VariableProvision { get; set; }
        public List<JobTransition_StatutoryDeductions> JobTransition_StatutoryDeductions { get; set; }

        public JobTransition_JobDetail JobTransition_JobDetail { get; set; }
        public JobTransition_Approve JobTransition_Approve { get; set; }
        public int TabCode { get; set; }        
        public decimal? OfferedSalary { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public int BasedOn { get; set; }
        public string Type { get; set; }

        public List<JobTransition_Authority> JobTransition_Authority { get; set; }
        public List<JobTransition_Subordinates> Subordinates_C { get; set; }
        public List<JobTransition_Subordinates> Subordinates_N { get; set; }
        public List<JobTransition_Summary> Summary { get; set; }
        public JobTransition_Summary JobTransition_Summary { get; set; }
    }
    public class JobTransitionList
    {
        public int HistoryCode { get; set; }
        public Guid HistoryGI { get; set; }
        public int EmployeeCode { get; set; }
        public Guid EmployeeGI { get; set; }

        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
      
        public string TransitionType { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime? RenewalDate { get; set; }
        public DateTime DOJ { get; set; }
        public int? Ageing { get; set; }
        public string ApprovalStatus { get; set; }
        public string PRApplicable { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public string ODesignation { get; set; }
        public string ODepartment { get; set; }
        public string OLocation { get; set; }
        public Decimal OldGP { get; set; }
        public Decimal NewGp { get; set; }
        public Decimal GP_DIF { get; set; }
    }
    public class JobTransition_Tabs
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }

    }
    public class JobTransition_Approve
    {
        public Guid EmployeeGI { get; set; }
        public Guid HistoryGI { get; set; }
        public string TransitionType { get; set; }
        public string Reason { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public DateTime? OrderDate { get; set; }
        public string PRApplicable { get; set; }
        public string EntryUser { get; set; }
        public  string  EntryDate { get; set; }

        public string ReasonForDelete { get; set; }
        public string ApproverComments { get; set; }
        public string ApproveMode { get; set; }

        public DateTime? NextAppraisalDate { get; set; }
    }
    public class JobTransition_GrowthHistory
    {
        public Guid HistoryGI { get; set; }
        public Guid EmployeeGI { get; set; }
        public string TransitionType { get; set; }
        public string ReasonsName { get; set; }
        public string OrderDate { get; set; }
        public string EntryDate { get; set; }
        public string PRApplicable { get; set; }
        public DateTime EffectiveDate { get; set; }
        public string EffectiveDate_ { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public string Category { get; set; }
        public string Grade { get; set; }
        public decimal? GrossPay { get; set; }
        public decimal? CTC { get; set; }
        public int IncPercentage { get; set; }
        public string FileName { get; set; }
        public string AbsoluteUri { get; set; }
    }

    public class JobTransition_JobDetail
    {
        public Guid EmployeeGI { get; set; }
        public JobTransitionsType JobTransitionsType { get; set; }
        public JobTransitionReason JobTransitionReason { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public DateTime? OrderDate { get; set; }
        public PRApplicability PRApplicability { get; set; }
        public Location CurrentLocation { get; set; }
        public Location NewLocation { get; set; }
        public Department CurrentDepartment { get; set; }
        public Department NewDepartment { get; set; }
        public Designation CurrentDesignation { get; set; }
        public Designation NewDesignation { get; set; }
        public Category CurrentCategory { get; set; }
        public Category NewCategory { get; set; }
        public Grade CurrentGrade { get; set; }
        public Grade NewGrade { get; set; }
        public PayrollGroup CurrentPayrollGroup { get; set; }
        public PayrollGroup NewPayrollGroup { get; set; }
        public VDAGroup CurrentVDAGroup { get; set; }
        public VDAGroup NewVDAGroup { get; set; }
        public DateTime? CurrentRenewalDate { get; set; }
        public DateTime? NewRenewalDate { get; set; }
        public int CurrentNoticePeriod { get; set; }
        public int NewNoticePeriod { get; set; }
        public decimal? CurrentGrossSalary { get; set; }
        public decimal? NewGrossSalary { get; set; }
        public decimal? CurrentCTC { get; set; }
        public decimal? NewCTC { get; set; }
        public bool PF { get; set; }
        public bool ESI { get; set; }
        public string Remarks { get; set; }
        public SalaryStructure CurrentSalaryStructure { get; set; }
        public SalaryStructure NewSalaryStructure { get; set; }

        public PFGroup CurrentPFGroup { get; set; }
        public PFGroup NewPFGroup { get; set; }
        public ESIGroup CurrentESIGroup { get; set; }
        public ESIGroup NewESIGroup { get; set; }
        public PTGroup CurrentPTGroup { get; set; }
        public PTGroup NewPTGroup { get; set; }

        public Division CurrentDivision { get; set; }
        public Division NewDivision { get; set; }

        public Section CurrentSection { get; set; }
        public Section NewSection { get; set; }

        public EmploymentType CurrentEmploymentType { get; set; }
        public EmploymentType NewEmploymentType { get; set; }
        public WorkLocation CurrentWorkLocation { get; set; }
        public WorkLocation NewWorkLocation { get; set; }
        public GroupCompany CurrentGroupCompany { get; set; }
        public GroupCompany NewGroupCompany { get; set; }
        public WageGradeMaster CurrentWageGrade { get; set; }
        public WageGradeMaster NewWageGrade { get; set; }
        public DateTime? NextAppraisalDate { get; set; }
        public SubDepartment NewSubDepartment { get; set; }
        public SubDepartment CurrentSubDepartment { get; set; }
        public Function NewFunction { get; set; }
        public Function CurrentFunction { get; set; }
        public MasterBase NewIncApplicability { get; set; }
        public MasterBase CurrentIncApplicability { get; set; }
        public IncentiveCostCenter NewIncentiveCostCenter { get; set; }
        public IncentiveCostCenter CurrentIncentiveCostCenter { get; set; }
        public bool PositionReplacementOrnot { get; set; }
        public PositionManagement CurrentPosition { get; set; }
        public PositionManagement NewPosition { get; set; }
        public decimal GrossOldAmt { get; set; }
        public decimal oldCTCValue { get; set; }
        public decimal NewCTCValue { get; set; }
        public MasterBase GrossCalculator { get; set; }
        public decimal GrossChangeValue { get; set; }
        public decimal GrossNewValue { get; set; }

        public decimal BasicOldAmt { get; set; }
        public MasterBase BasicCalculator { get; set; }
        public decimal BasicChangeValue { get; set; }
        public decimal BasicNewValue { get; set; }

        public decimal ABOldAmt { get; set; }
        public MasterBase ABCalculator { get; set; }
        public decimal ABChangeValue { get; set; }
        public decimal ABNewValue { get; set; }

        public MasterBase Allowance { get; set; }
        public decimal AlwAmtOld { get; set; }
        public decimal AlwAmtNew { get; set; }
    }
    public class JobTransition_Allowance
    {
        public int AllowanceCode { get; set; }
        public string AllowanceName { get; set; }
        public string AllowanceType { get; set; }
        public decimal? CurrentAmount { get; set; }
        public decimal? NewAmount { get; set; }
    }
    public class JobTransition_Deduction
    {
        public int DeductionCode { get; set; }
        public string DeductionName { get; set; }
        public decimal? CurrentAmount { get; set; }
        public decimal? NewAmount { get; set; }
    }
    public class JobTransition_Provision
    {
        public int ProvisionCode { get; set; }
        public string ProvisionName { get; set; }
        public decimal? CurrentAmount { get; set; }
        public decimal? NewAmount { get; set; }
    }
    public class JobTransition_StatutoryDeductions
    {
        public int DeductionCode { get; set; }
        public string DeductionName { get; set; }
        public bool Current { get; set; }
        public bool New { get; set; }
    }
    public class JobTransition_Authority
    {
        public int RequestTypeCode { get; set; }
        public string RequestTypeName { get; set; }
        public int LevelCode { get; set; }
        public string LevelName { get; set; }
        public int AuthorityCode_Old { get; set; }
        public string AuthorityName_Old { get; set; }
        public int AuthorityCode_New { get; set; }
        public string AuthorityName_New { get; set; }
    }
    public class JobTransition_Subordinates
    {
        public string SubordinatesName { get; set; }
    }
    public class JobTransition_Summary
    {
        public int SLNo { get; set; }
        public int TypeCode { get; set; }
        public int ReasonCode { get; set; }
        public string Type { get; set; }
        public bool Active { get; set; }
    }
}
