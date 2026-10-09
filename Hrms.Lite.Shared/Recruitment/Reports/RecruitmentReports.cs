using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.Recruitment.Reports
{
    public class RecruitmentReports
    {
        public List<VacancyStatusReport> VacancyStatusReport { get; set; }
        public List<ApplicantTrackingReport> ApplicantTrackingReport { get; set; }
        public List<RecruitmentTrnHistory> RecruitmentTrnHistory { get; set; }
        public string ApplicantName { get; set; }
        public string RefNumber { get; set; }
        public string ApplDesignation { get; set; }
        public string ApplLocation { get; set; }
        public string PhotoFilename { get; set; }
        public int BasedOn { get; set; }
        public int UserDropdown { get; set; }
        public int MenuDropdown { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string ResumeByCandidate { get; set; }
        public string ResumeFileName { get; set; }
        public string IAndEDocument { get; set; }
        public string DocName { get; set; }
        public Guid ResumeDataBankGI { get; set; }
    }
    public class VacancyStatusReport
    {
        public int VacancyManagementCode { get; set; }
        public Guid VacancyManagementGI { get; set; }
        public string JobCode { get; set; }
        public string JobTitle { get; set; }
        public DateTime NeedByDate { get; set; }
        public int NoOfRequirements { get; set; }
        public string LocationName { get; set; }
        public int DaysLeft { get; set; }
        public int MAP { get; set; }
        public int SL { get; set; }
        public int IE { get; set; }
        public int RAA { get; set; }
        public int SALARY { get; set; }
        public int CC { get; set; }
        public int EC { get; set; }
        public int OFFER { get; set; }
        public string EDC { get; set; }
        public DateTime CancelledDate { get; set; }
        public string CancelledBy { get; set; }
        public string RequestedBy { get; set; }
        public string Designation { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public string Reason { get; set; }
        public int EmployeeActivation { get; set; }
        public int JoinConfirm { get; set; }        
             public string Status { get; set; }
    }
    public class ApplicantTrackingReport
    {
        public Guid ResumeDataBankGI { get; set; }
        public int ResumeDataBankCode { get; set; }
        public string RefNumber { get; set; }
        public string Candidate { get; set; }
        public int VacancyManagementCode { get; set; }
        public Guid VacancyManagementGI { get; set; }
        public string JobCode { get; set; }
        public string JobTitle { get; set; }
        public string LocationName { get; set; }
        public bool SL { get; set; }
        public bool IE { get; set; }
        public bool RAA { get; set; }
        public bool SALARY { get; set; }
        public bool CC { get; set; }
        public bool EC { get; set; }
        public bool OFFER { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public string ResumeSource { get; set; }
    }
    public class RecruitmentTrnHistory {
        public string RefNo { get; set; }
        public string UserID { get; set; }
        public string UserName { get; set; }

        public string CandidateName { get; set; }
        public string Transaction { get; set; }
        public string Actions { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string JobCode { get; set; }
        public string JobTitle { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
    }
}
