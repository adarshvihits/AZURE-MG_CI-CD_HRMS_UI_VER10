using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;

namespace Hrms.Lite.Shared.Recruitment
{
    public class Applicants
    {
        public UserBase User { get; set; }
        public int TabIndex { get; set; } // 1-Active 2-Shortlisted 3-In Evaluation Process
        public int ActiveCount { get; set; }
        public int ShortlistedCount { get; set; }
        public int EvaluationCount { get; set; }
        public int TotalCount { get; set; }    // rows in the selected tab
        public int FilteredCount { get; set; } // rows after search
        public List<ApplicantListItem> ApplicantList { get; set; }
    }

    public class ApplicantListItem
    {
        public Guid ResumeDataBankGI { get; set; }
        public int ResumeDataBankCode { get; set; }
        public string ApplicantCode { get; set; }
        public DateTime ApplicationDate { get; set; }
        public string ApplicantName { get; set; }
        public DateTime DOB { get; set; }
        public string Gender { get; set; }
        public string Qualification { get; set; }
        public string Experience { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Nationality { get; set; }
        public string Residence { get; set; }
        public string LanguagesKnown { get; set; }
        public string Resume { get; set; } // uploaded resume url
    }

    // Applicant full profile (Recruitment/Applicants/Profile)
    public class ApplicantProfile
    {
        public Guid ApplicantGI { get; set; }
        public string ApplicantCode { get; set; }
        public string ApplicantName { get; set; }
        public string Status { get; set; }
        public string AppliedFor { get; set; }
        public string AppliedLocation { get; set; }
        public DateTime AppliedOn { get; set; }

        // personal
        public string Nationality { get; set; }
        public string Residence { get; set; }
        public string Gender { get; set; }
        public DateTime DOB { get; set; }
        public string MaritalStatus { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }

        // address
        public ApplicantAddress CommunicationAddress { get; set; }
        public ApplicantAddress PermanentAddress { get; set; }
        public string Reference1Name { get; set; }
        public string Reference1Contact { get; set; }
        public string Reference2Name { get; set; }
        public string Reference2Contact { get; set; }

        // education / professional
        public string QualificationLevel { get; set; }
        public bool IsFresher { get; set; }
        public int ExperienceYears { get; set; }
        public int ExperienceMonths { get; set; }

        // additional
        public string Source { get; set; }
        public string ReferringEmployeeID { get; set; }
        public string ReferringEmployeeName { get; set; }
        public string Landmark { get; set; }
        public bool IsPreviousEmployee { get; set; }
        public bool IsFamilyMemberEmployee { get; set; }
        public bool WillingToTravel { get; set; }
        public bool WillingToRelocate { get; set; }
        public bool HavePassport { get; set; }
        public string PassportNo { get; set; }
        public string VisaDetails { get; set; }
        public bool HaveLegalRightToWork { get; set; }

        public List<ApplicantQualification> QualificationList { get; set; }
        public List<ApplicantEmployment> EmploymentList { get; set; }
        public List<ApplicantLanguage> LanguageList { get; set; }
        public List<ApplicantVacancy> VacancyList { get; set; }
        public List<ApplicantEmergencyContact> EmergencyContactList { get; set; }
        public Url Url { get; set; } // API base url for the PDF report (ApplicantProfile.trdp)
    }

    public class ApplicantAddress
    {
        public string House { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string PostOffice { get; set; }
        public string Pin { get; set; }
        public string District { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
    }

    public class ApplicantQualification
    {
        public string Qualification { get; set; }
        public string Institute { get; set; }
        public string CourseType { get; set; }
        public bool Completed { get; set; }
        public bool Passed { get; set; }
        public string Duration { get; set; }
        public string Year { get; set; }
        public string Percentage { get; set; }
        public bool Highest { get; set; }
    }

    public class ApplicantEmployment
    {
        public string Employer { get; set; }
        public string Designation { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public bool CurrentlyWorking { get; set; }
        public string CTC { get; set; }
        public string Reference { get; set; }
        public string Roles { get; set; }
    }

    public class ApplicantLanguage
    {
        public string Language { get; set; }
        public bool Read { get; set; }
        public bool Write { get; set; }
        public bool Speak { get; set; }
    }

    public class ApplicantEmergencyContact
    {
        public string Name { get; set; }
        public string Relation { get; set; }
        public string Mobile { get; set; }
    }

    public class ApplicantVacancy
    {
        public string Vacancy { get; set; }
        public string Location { get; set; }
        public DateTime AppliedOn { get; set; }
        public string ApplicationID { get; set; }
        public string Status { get; set; }
    }
}
