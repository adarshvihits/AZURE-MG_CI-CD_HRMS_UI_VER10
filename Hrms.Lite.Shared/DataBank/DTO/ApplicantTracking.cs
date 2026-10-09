using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.DTO
{
    public class ApplicantTracking
    {
        public UserBase User { get; set; }
        public int ApplicantTrackingCode { get; set; }
        public int ResumeDataBankCode { get; set; }
        public int RejectReasonCode { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Resume { get; set; }
        public int Tab1Count { get; set; }
        public int Tab2Count { get; set; }
        public int Tab3Count { get; set; }
        public int Tab6Count { get; set; }
        public int Tab7Count { get; set; }
        public int BranchGradingSalary { get; set; }
        public int MinimumSalary { get; set; }
        public int MaximumSalary { get; set; }
        public ApplicantTracking_Details ApplicantTrackingDT { get; set; }
        public List<ApplicantTracking_Details> ApplicantTrackingDTList { get; set; }
        public List<ApplicantTrackingListTabs> ApplicantTrackingListTabs { get; set; }
        public List<ApplicantTrackingList> ApplicantTrackingList { get; set; }
        public List<ApplicantTracking_Details> ApplicantTracking_Details { get; set; }
        public ApplicantTracking_ShortListed ApplicantTracking_ShortListed { get; set; }

        public ApplicantTracking_ScheduledInterview ApplicantTracking_ScheduledInterview { get; set; }
        public ApplicantTracking_Evaluation ApplicantTracking_Evaluation { get; set; }

        public List<ApplicantTracking_ScheduledInterview> InterviewScheduledDetails { get; set; }
        public List<ApplicantTracking_Evaluation> InterviewEvaluationDetails { get; set; }
        public InterviewSelection InterviewSelection { get; set; }
        public InterviewLevel InterviewLevel { get; set; }
        public string Remarks { get; set; }
        public string ApplicantName { get; set; }
        public string ApplicantLocation { get; set; }
        public string ApplicantDesignation { get; set; }
        public string ApplicantRefNo { get; set; }
        public Guid ResumeDataBankGI { get; set; }
        public string CandidatesPhoto { get; set; }
        public string CandidatesPhotoFileName { get; set; }
        public ApplicantTracking_FinalApproval ApplicantTracking_FinalApproval { get; set; }
        public string HoldOrReject { get; set; }
        public List<ApplicantTracking_Tracker> Applicant_Tracker { get; set; }
        public int Type { get; set; }
    }
    public class ApplicantTrackingListTabs
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }

    }
    public class ApplicantTrackingList
    {
        public int ApplicantTrackingCode { get; set; }
        public int ResumeDataBankCode { get; set; }
        public Guid ApplicantTrackingGI { get; set; }
        public Guid VacancyManagementGI { get; set; }
        public Guid ResumeDataBankGI { get; set; }
        public Guid PreEnrollGI { get; set; }
        public string JobCode { get; set; }
        public string JobTitle { get; set; }
        public string ApplicantName { get; set; }
        public int Age { get; set; }
        public DateTime NeededByDate { get; set; }
        public string Stage { get; set; }
        public int DaysLeft { get; set; }
        public string Location { get; set; }
        public string YOS { get; set; }
        public string InterviewStatus { get; set; }
        public string InterviewStatusType { get; set; }
        public string RefNo { get; set; }
        public string DocVerified { get; set; }
        public string Aging { get; set; }
        public string Designation { get; set; }
        public string Resume { get; set; }
        public string ResumeFile { get; set; }
        public string Offerletter { get; set; }
        public string offerLetterAbsoluteuri { get; set; }
        public int  active { get; set; }
        public int totalRecords { get; set; }
        
    }
    public class ApplicantTracking_Details : EmployeeHeader
    {
        public int ResumeDataBankCode { get; set; }
        public int ApplicantTrackingCode { get; set; }
        public bool Status { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public int InterviewSelectionCode { get; set; }
        public int InterviewLevels { get; set; }
        public string InterviewStatusType { get; set; }
        public string Remarks { get; set; }
        public InterviewType InterviewType { get; set; }
        public DateTime? InterviewDate { get; set; }
        public string InterviewTime { get; set; }
        public InterviewMode InterviewMode { get; set; }
        public EmployeeHeader Interviewer { get; set; }
        public string Resume { get; set; }
        public  int NoInterviewLevel { get; set; }
        public File File { get; set; }
        public int InterviewLevel { get; set; }
       public MasterBase Level { get; set; }
    }
    public class ApplicantTracking_ShortListed
    {
        public int ResumeDataBankCode { get; set; }
        public InterviewSelection InterviewSelection { get; set; }
        public string Remarks { get; set; }
    }
    public class ApplicantTracking_ScheduledInterview
    {
        public int ResumeDataBankCode { get; set; }
        public InterviewLevel InterviewLevel { get; set; }
        public InterviewType InterviewType { get; set; }
        public DateTime InterviewDate { get; set; }
        public DateTime InterviewTime { get; set; }
        public InterviewMode InterviewMode { get; set; }
        public EmployeeHeader Interviewer { get; set; }
        public string Remarks { get; set; }
       
    }
    public class ApplicantTracking_Evaluation
    {
        public int ResumeDataBankCode { get; set; }
        public EvaluationStatus EvaluationStatus { get; set; }
        public decimal Score { get; set; }
        public EvaluationNextLevel EvaluationNextLevel { get; set; }
        public string Remarks { get; set; }

    }
    public class ApplicantTracking_Tracker
    {
        public string InterviewLevel { get; set; }
        public DateTime? InterviewDate { get; set; }
        public DateTime EvaluationDate { get; set; }
        public string EmpIdName { get; set; }
    }
    public class ApplicantTracking_FinalApproval
    {
        public int ResumeDataBankCode { get; set; }
        public int ExpectedSalary { get; set; }
        public int OfferedSalary { get; set; }
        public int Intervi { get; set; }
        public string Remarks { get; set; }
    }
    //public class ApplicantTracking_BackgroundVarification
    //{
    //    public int ResumeDataBankCode { get; set; }
    //    public BackgroundVarificationStatus BackgroundVarificationStatus { get; set; }
    //    public BackgroundVarificationType BackgroundVarificationType { get; set; }
    //    public string Satisfactory { get; set; }
    //    public ApplicantTrackingYesorNo PoliceCase { get; set; }
    //    public ApplicantTrackingYesorNo Compliants { get; set; }
    //    public string FeedBack { get; set; }
    //}
    //public class ApplicantTracking_SalaryCommunication
    //{
    //    public int ResumeDataBankCode { get; set; }
    //    public int ExpectedSalary { get; set; }
    //    public int OfferedSalary { get; set; }
    //    public AcceptorReject AcceptorReject { get; set; }
    //    public string Remarks { get; set; }
    //}
    public class InterviewSelection : MasterBase
    {
        public int RejectReasonCode { get; set; }
    }

    //public class InterviewType: MasterBase
    //{ }
    public class InterviewMode : MasterBase
    { }
    public class InterviewLevel : MasterBase
    { }
    public class EvaluationStatus : MasterBase
    { }
    public class EvaluationNextLevel : MasterBase
    { }
    //public class BackgroundVarificationStatus : MasterBase
    //{ }
    //public class BackgroundVarificationType : MasterBase
    //{ }
    public class ApplicantTrackingYesorNo : MasterBase
    { }
    public class AcceptorReject : MasterBase
    { }

}

