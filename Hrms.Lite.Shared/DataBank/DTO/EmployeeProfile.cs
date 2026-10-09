using Hrms.Lite.Shared.DataBank.Generic;
using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.DataBank.Generic.PersonalDetails;
using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.DTO
{
    public class EmployeeProfile
    {
  
        public UserBase User { get; set; }
        public FinancialYear FinancialYear { get; set; }
        public int ActiveCount { get; set; }
        public int ResignedCount { get; set; }
        public string DOJ { get; set; }
        public string YOS { get; set; }
        public string ReportingManeger { get; set; }
        public string ReportingManegerDesignation { get; set; }
        public string ReportingManagerAbsoluteUri { get; set; }
        public Guid ReportingManagerGI { get; set; }
        public string FirstLeaveAuthority { get; set; }
        public string FirstLeaveAuthorityDesignation { get; set; }
        public string FirstLeaveAuthorityAbsoluteUri { get; set; }
        public string Languages { get; set; }
        public string Hobbies { get; set; }
        public List<EmployeeProfile_List> EmployeeProfileList { get; set; }
        public EmployeeHeader EmployeeHeader { get; set; }
        public EmployeeAddress Address { get; set; }
        public EmployeeBasicInfo EmployeeBasicInfo { get; set; }
        public EmployeeContactDetails ContactDetails { get; set; }
        public List<Education> Education { get; set; }
        public List<Skills> Skills { get; set; }
        public List<PreviousEmployment> PreviousEmployment { get; set; }

        public List<Certifications> Certifications { get; set; }
        public CompensationStructure Compensation { get; set; }
        public List<JobTransition_GrowthHistory> GrowthHistory { get; set; }
        public DocumentUpload DocumentUpload { get; set; }
        public List<RequestVsAuthorityLogDetails> AuthorityLogDetails { get; set; }
        public StatutoryClassification Statutory_Payment_Details { get; set; }
        public EmployeeClassification Job_Details { get; set; }
        public List<AssetRequest> AssetRequest { get; set; }
        public List<AssetRequestList> AssetRequestList { get; set; }
        public List<RewardsRequest> RewardsRequest { get; set; }
        public List<IncidentsAction> IncidentsAction { get; set; }
        public List<TrainingLog> TrainingLog { get; set; }
        public List<AppraisalLog> AppraisalLog { get; set; }
        
        public List<FamilyDetails> FamilyDetails { get; set; }
        public PersonalInfo PersonalInfo { get; set; }
        public List<EmployeeProfile_Attendance> Attendance { get; set; }
        public List<EmployeeProfile_Salary> Salary { get; set; }
        public List<EmployeeProfileSummary> EmployeeProfileSummary { get; set; }
        public Filter Filter { get; set; }
        public GroupByComboFill GroupByFill { get; set; }
        public string groupbyname { get; set; }
        public string Breadcrumb { get; set; }
        public string BreadcrumbName { get; set; }
        public int BreadcrumbCPCode { get; set; }
        public string BreadcrumbCPName { get; set; }
        public string BreadcrumbCPType { get; set; }
         public CompensationStructureReport CompensationStructureReport { get; set; }
        public List<CompensationStructure_Details> CompensationStructure_Details { get; set; }
        public Url Url { get; set; }
        public JobDescriptionDetails JobDescriptionDetails { get; set; }
        public List<PPRlog> PPRlog { get; set; }
        public SkipLevelMGR SkipLevelMGR { get; set; }
        public SeparationDetails SeparationDetails { get; set; }
        public List<DocumentList> DocumentList { get; set; }
        

    }
    public class SkipLevelMGR
    {
        public string Name { get; set; }
        public string  FunDesignation { get; set; }
        public string EmployeeGI { get; set; }
    }
    public class PPRlog
    {
        public string ReviewPeriod { get; set; }
        public string AppraiserName { get; set; }
        public string ApproverName { get; set; }
        public string ReviewerName { get; set; }
        public Decimal SelfScore { get; set; }
        public Decimal AppraiserScore { get; set; }
        public Decimal ApproverScore { get; set; }
        public Decimal ReviewerScore { get; set; }
        public Decimal Rating { get; set; }

    }
    public class EmployeeProfile_List
    {
       
        public Guid EmployeeGI { get; set; }
        public string Employeeid { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public string Category { get; set; }
        public int CategoryCode { get; set; }
       
        public string Gender { get; set; }
        public string Age { get; set; }
        public string YOS { get; set; }
        public DateTime DOB { get; set; }
        public DateTime DOJ { get; set; }
        public DateTime LWD { get; set; }
        public Decimal GrossPay { get; set; }
        public Decimal CTC { get; set; }
        public string AbsoluteUrl { get; set; }
        public string Base64string { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public int resigned { get; set; }
        public string FileName { get; set; }
        public string Grade { get; set; }
    }
    public class EmployeeProfile_Attendance
    {

        public string Month { get; set; }
        public int TOTALDAYS { get; set; }
        public decimal PRESENTDAYS { get; set; }
        public decimal HOLIDAYS { get; set; }
        public decimal LOP { get; set; }
        public decimal LEAVE { get; set; }
        public decimal ATT_PERCENTAGE { get; set; }
        public string ATTENDANCE_CARD { get; set; }
        public decimal OnDuty { get; set; }
        public decimal WFH { get; set; }
        public decimal RA { get; set; }
        public decimal Offday { get; set; }
        public decimal OffdayPresent { get; set; }
        public decimal COF { get; set; }
       
        public decimal HolidayPresent { get; set; }
       


    }
    public class EmployeeProfile_Salary
    {
        public string SalaryPeriod { get; set; }
        public decimal? GrossPay { get; set; }
        public decimal? GrossDeduction { get; set; }
        public decimal? NetPay { get; set; }
        public decimal? LOPAmount { get; set; }
        public string PaySlip { get; set; }

    }
    public class EmployeeProfileSummary
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public decimal Count_Perc { get; set; }
        public decimal  CTC { get; set; }
        public decimal CTC_Perc { get; set; }
    }
    public class GroupByComboFill : MasterBase
    {

    }
    public class JobDescriptionDetails
    {
        public string KeyResponsibilities { get; set; }
    }
    public class AssetRequestList
    {
        public Asset Asset { get; set; }
        public AssetName AssetName { get; set; }
        public DateTime? RequiredDate { get; set; }
        public string Brand { get; set; }
        public string ModelNo { get; set; }
        public Decimal Cost { get; set; }
        public Department IssuingDepartment { get; set; }
        public Condition Condition { get; set; }
        public string Specification { get; set; }
        public string Status { get; set; }
        public string AssetCode { get; set; }
        public Category AssetCategory { get; set; }




    }
    public class SeparationDetails
    {
        public DateTime? ResignationDate { get; set; }
        public DateTime? LWD { get; set; }
        public string SeparationType { get; set; }
        public string Reason { get; set; }
        public DateTime? ReportingMangerLWD { get; set; }
        public DateTime? SkipLevelMangerLWD { get; set; }
    }
    public class DocumentList
    {
      
        public string DocName { get; set; }
        public int CertificateDocCode { get; set; }
     


    }
}
