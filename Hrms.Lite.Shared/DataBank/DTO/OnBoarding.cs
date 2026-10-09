using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.DTO
{
  public  class OnBoarding
    {
        public Guid PreEnrollGI { get; set; }
        public Guid ResumeDataBankGI { get; set; }
        public string OfferLetterAbsoluteUri { get; set; }
        public string OfferFileName { get; set; }
        public EmployeeHeader EmployeeHeader { get; set; }
        public string ApplicantID { get; set; }
        public string CandidateName { get; set; }
        public Department Department { get; set; }
        public Designation Designation { get; set; }
        public Location Location { get; set; }
        public DateTime ExpectedDOJ { get; set; }
        public int DueDays { get; set; }
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
        public int CreditCheckingCount { get; set; }
        public int EmploymentCheckingCount { get; set; }
        public int OfferInitiationCount { get; set; }      

       public int ActiveVacancyCount { get; set; }
        public string JobCode { get; set; }
        public string JobTitle { get; set; }
        public DateTime NeedByDate { get; set; }
        public string MobileNumber { get; set; }
        public List<OnBoarding> OnBoardingList { get; set; }
        public FilterSlabComboFill FilterSlabComboFill { get; set; }
        public List<OnBoardingListTabs> OnBoardingListTabs { get; set; }
        public List<JoiningCheckDetails> JoiningCheckList { get; set; }
        public List<ActiveVacancy> ActiveVacancyList { get; set; }
        public string DocVerified { get; set; }
        public int TabIndex { get; set; }
        public Filter Filter { get; set; }
        public int TabCode { get; set; }
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
        public string TabName { get; set; }
        public byte[] OfferLetterData { get; set; }
        public List<FormPermissionMenuTab> UserVsMenuTab { get; set; }
        public string FirstTab { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public int ApplicantTrackingCode { get; set; }
        public string Resume { get; set; }
        public int LogCode { get; set; }
        public string ResumeFileName { get; set; }
    }



    public class OfferLetter
    {
        public Filter Filter { get; set; }
        public Guid PreEnrollGI { get; set; }
        public File File { get; set; }
        public string Type { get; set; }
        public string FileName { get; set; }
        public string AbsoluteUri { get; set; }
        public string Mode { get; set; }
        public int EmployeeCode { get; set; }
        public Url Url { get; set; }
        public Guid EmployeeGI { get; set; }
        public string EmployeeID { get; set; }
        public string ApplicantID { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public DateTime DOJ { get; set; }
        public int CompanyCode { get; set; }
        public int PreEnrollCode { get; set; }
        public int LetterFormatCode { get; set; }
        public int HistoryCode { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public List<OfferLetter> OfferLetterList { get; set; }
        public bool Applicable { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public int radiomode { get; set; }
        public string ApprovedStatus { get; set; }

    }









    public class OnBoardingListTabs
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }

    }
    public class JoiningCheckDetails
    {
        public Guid PreEnrollGI { get; set; }        
        public int CheckTypeCode { get; set; }
        public string CheckType { get; set; }
        public int CheckItemCode { get; set; }
        public string CheckItem { get; set; }
        public bool IsMandatory { get; set; }
        public JoiningCheckStatus JoiningCheckStatus { get; set; }
        public string Remarks { get; set; }
    }
    public class JoiningCheckStatus : MasterBase
    {

    }

    public class ActiveVacancy
    {
        public int VacancyManagementCode { get; set; }
        public Guid VacancyManagementGI { get; set; }
        public string JobCode { get; set; }
        public string JobTitle { get; set; }
        public DateTime NeedByDate { get; set; }
        public DateTime RequestedDate { get; set; }
        public int NoOfRequirements { get; set; }
        public string RequestedBy { get; set; }
        public Designation Designation { get; set; }
        public Department Department { get; set; }
        public Location Location { get; set; }
        public WorkLocation WorkLocation { get; set; }
        public string Region { get; set; }
        public int Aging { get; set; }
        public string RecruitmentComments { get; set; }
        public int MappedResumes { get; set; }
        public string VacancyType { get; set; }


    }
}
