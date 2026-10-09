using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.Recruitment
{
    public class BackgroundVerification
    {
        public EmployeeHeader EmployeeHeader { get; set; }
        public int PreEnrollCode { get; set; }
        public Guid PreEnrollGI { get; set; }
        public Guid ResumeDataBankGI { get; set; }
        public string CandidateName { get; set; }
        public Department Department { get; set; }
        public Designation Designation { get; set; }
        public Location Location { get; set; }
        public DateTime ExpectedDOJ { get; set; }
        public int DueDays { get; set; }
        public int CategoryTypeCode { get; set; }
        public string CheckList { get; set; }
        public string PendingStatus { get; set; }
        public int AllCount { get; set; }

        public int PreenrollmentAppCount { get; set; }
        public int EnrollmentCount { get; set; }
        public int CheckListCount { get; set; }
        public int JoiningConfCount { get; set; }
        public int OverDueCount { get; set; }
        public int PreenrollmentPendingCount { get; set; }
        public int OfferProcessingCount { get; set; }
        public int CheckListTotal { get; set; }
        public int CreditCheckingCount { get; set; }
        public int EmploymentCheckingCount { get; set; }
        public int OfferInitiationCount { get; set; }
        public int ActiveVacancyCount { get; set; }
        public string JobCode { get; set; }
        public string JobTitle { get; set; }
        public DateTime NeedByDate { get; set; }
        public string MobileNumber { get; set; }
        public string FileNo { get; set; }
        public List<OnBoarding> BackgroundVerificationList { get; set; }        
        public List<CreditCheckListTabs> CreditCheckListTabs { get; set; }
        public List<JoiningCheckDetails> JoiningCheckList { get; set; }
        public string DocVerified { get; set; }
        public int OfferInitiationPendingCount { get; set; }
        public int OfferAcceptancePendingCount { get; set; }
        public int Offer_AcceptedCount { get; set; }
        public int OfferRejectedCount { get; set; }
        public int ALLOfferCount { get; set; }
        public string RefNo { get; set; }
        public string Stage { get; set; }
        public bool Aadhar { get; set; }
        public bool PAN { get; set; }
        public bool DrivingLicense { get; set; }
        public bool Passport { get; set; }
        public bool Others { get; set; }
        public string OfferLetterAbsoluteUri { get; set; }
        public byte[]? OfferLetterData { get; set; }
        public List<ActiveVacancy> ActiveVacancyList { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public int ApplicantTrackingCode { get; set; }
        public CreditEmployeementChecking CreditChecking { get; set; }

        public string TabName { get; set; }
        public int SelectionStatus { get; set; }
        public string Remarks { get; set; }
        public int? CreditScore { get; set; }
        public string Address { get; set; }
        public string AdharNo { get; set; }
        public string Email { get; set; }
        public string Mode { get; set; }
        public int BasicDetailsStatus { get; set; }
        public string BasicDetailsStatusReason { get; set; }
        public int EducationDetailsStatus { get; set; }
        public string EducationDetailsStatusReason { get; set; }
        public int DocumentDetailsStatus { get; set; }
        public string DocumentDetailsStatusReason { get; set; }
        public int PreEmployerDetailsStatus { get; set; }
        public string PreEmployerDetailsStatusReason { get; set; }
        public List<EmployeeDoccument> EmployeeDocumentList { get; set; }
        public List<EducationalDetails> EducationalDetailsList { get; set; }
        public List<ExperienceDetails> ExperienceDetailsList { get; set; }

        public string RelievingLetterAbsoluteUri { get; set; }
    }
    public class EmployeeDoccument
    {
        public int DocumentCode { get; set; }
        public int ApplicantCode { get; set; }
        public string DocumentName { get; set; }
        public string FileName { get; set; }
        public string AbsoluteUri { get; set; }
        public string DocRemarks { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Experience { get; set; }
    }
    public class EducationalDetails
    {
        public string College { get; set; }
        public string Aggregate { get; set; }
        public string Qualification { get; set; }
        public string Specialization { get; set; }
        public string YearOfCompletion { get; set; }
        public string AbsoluteURI { get; set; }
        public string Filename { get; set; }
    }
    public class ExperienceDetails
    {
        public string CompanyName { get; set; }
        public string Location { get; set; }
        public string Designation { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Experience { get; set; }
        public string ContactPerson { get; set; }
        public string FileName { get; set; }
        public string AbsoluteURI { get; set; }
    }
    public class CreditCheckListTabs
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }

    }
}
