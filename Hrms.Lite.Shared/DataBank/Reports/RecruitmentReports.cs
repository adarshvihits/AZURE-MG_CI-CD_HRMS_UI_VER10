using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.Reports
{
    public class RecruitmentReports
    {
        public List<VacancyStatusReport> VacancyStatusReport { get; set; }
        public List<VacancyStatusReportDetails> VacancyStatusReportDetailsList { get; set; }
        public List<ApplicantTrackingReport> ApplicantTrackingReport { get; set; }
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


        public int OfferInitiated { get; set; }
        public string Department { get; set; }
        public DateTime RequestedDate { get; set; }
        public string Section { get; set; }
        public string Grade { get; set; }
        public string VacancyType { get; set; }
        public string Priority { get; set; }
        public string Manager { get; set; }
    }
    public class ApplicantTrackingReport
    {
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
    public class VacancyStatusReportDetails
    {
        public int VacancyManagementCode { get; set; }
        public Guid VacancyManagementGI { get; set; }
        public string CandidateName { get; set; }

        public string JobCode { get; set; }
        public string JobTitle { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        public string Location { get; set; }


    }
}
