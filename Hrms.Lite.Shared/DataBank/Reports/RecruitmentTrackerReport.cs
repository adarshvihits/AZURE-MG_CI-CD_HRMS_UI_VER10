
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice.Generic;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.Reports
{
    public class RecruitmentTrackerReport
    {
        public List<RecruitmentTrackerReport> TrackingReporttList { get; set; }

        public int ApplicantCode { get; set; }
        public Guid ApplicantGI { get; set; }
        public Guid EmployeeGI { get; set; }
        public Filter Filter { get; set; }
        public string ApplicantId { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public string FunctionalDesignation { get; set; }
        public string Location { get; set; }
        public decimal? Salary { get; set; }
        public string Reason { get; set; }
        public string ReplacementEmployee { get; set; }
        public string Source { get; set; }
        public string Consultancy { get; set; }
        public string ReferenceEmployee { get; set; }
        public string SourceDetails { get; set; }
        public string Status { get; set; }
        public string Enterby { get; set; }
        public DateTime EntryDate { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public CandidateHeader ApplicantHeader { get; set; }
        public EmployeeHeader EmployeeHeader { get; set; }
        public string TabIndex { get; set; }

        public List<Tracker> Tracker { get; set; }

        public List<Candidate_Education> Education { get; set; }
        public List<Candidate_Certifications> Certifications { get; set; }
        public List<Candidate_PreviousEmployment> PreviousEmployment { get; set; }
        public CandidateBasicInfo CandidateBasicInfo { get; set; }
        public File Photo { get; set; }
        public File Aadhar { get; set; }
        public File Pan { get; set; }
        public File Resume { get; set; }
        public bool Varify { get; set; }
        public string RefereeName { get; set; }
        public string ContactInformation { get; set; }
        public string TitleAndCompanyName { get; set; }
        public MasterBase RelationshipWithCandidate { get; set; }
        public string OtherDetails { get; set; }
        public MasterBase HowManyYears { get; set; }
        public string DurationOFworkingTogether { get; set; }
        public bool Fresher { get; set; }
        public MasterBase DurationYears { get; set; }
        public MasterBase DurationMonths { get; set; }
        public string TypeOfReferenceName { get; set; }
        public string BGVCompanyName { get; set; }
        public string BGVCandidatePosition { get; set; }
        public string BGVRehire { get; set; }
        public string BGVReasonForExit { get; set; }
        public decimal ApprovedGross { get; set; }
        public decimal ApprovedTakeHome { get; set; }
        public decimal ApprovedCTC { get; set; }
        public MasterBase FuncDesignation { get; set; }
        public MasterBase WageGrade { get; set; }
        public decimal MinCTC { get; set; }
        public decimal Equity { get; set; }
        public decimal revenuePerDlr { get; set; }
        public decimal revenuePerEmployee { get; set; }
        public decimal OnlineBrokerage { get; set; }
        public decimal OfflineBrokerage { get; set; }
        public int AvgTrdClient { get; set; }
        public decimal Commoditybrokerage { get; set; }
        public List<RecruitmentStaffDetails> RecruitmentStaffDetails { get; set; }
        
        public int ClarificationCount { get; set; }
        public List<RecruitmentClarificationDetails> RecruitmentClarificationDetailsList { get; set; }
        public Dropdown NextLevel { get; set; }
        public string ApproverComments { get; set; }
        public JobDetails JobDetails { get; set; }
        public Dropdown DesignationDD { get; set; }
        public CandidateAddress PermanentAddress { get; set; }

        public CandidateAddress PresentAddress { get; set; }
        public List<Documents> Documents { get; set; }

    }
    public class JobDetails
    {
        public MasterBase Designation { get; set; }
        public MasterBase FunctionalDesignation { get; set; }
        public MasterBase Location { get; set; }
        public MasterBase WageGrade { get; set; }
        public string InterviewEvaluation { get; set; }
        public string ReasonForRecruitment { get; set; }
        public string SourceofRecruitment { get; set; }
        public MasterBase RecruitmentSource { get; set; }
        public MasterBase RefEmployee { get; set; }
        public MasterBase RecruitmentReason { get; set; }
        public MasterBase ReplaceEmployee { get; set; }
        public string ReplaceEmployeeName { get; set; }
        public MasterBase Industry { get; set; }
        public string OtherIndustry { get; set; }
        public MasterBase Consultancy { get; set; }
        public decimal Salary { get; set; }
        public DateTime? TestDate { get; set; }
        public int TestScore { get; set; }        
        public MasterBase GroupCompany { get; set; }
        public MasterBase ReferralScheme { get; set; }

    }
    public class CandidateAddress
    {
        public bool SameAsAddressCheck { get; set; }
        public string House { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string PinCode { get; set; }
        public MasterBase District { get; set; }
        public MasterBase State { get; set; }
        public MasterBase Country { get; set; }
        public string MobileNo { get; set; }
        public string MailID { get; set; }
    }
    public class Documents
    {
        public string Type { get; set; }
        public string FileName { get; set; }
        public int RowId { get; set; }
    }
}
