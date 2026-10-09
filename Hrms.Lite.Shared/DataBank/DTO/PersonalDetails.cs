using Hrms.Lite.Shared.CandidateLink;
using Hrms.Lite.Shared.DataBank.Generic;
using Hrms.Lite.Shared.DataBank.Generic.PersonalDetails;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.DTO
{
    public class PersonalDetails
    {
        public Guid EmployeeGI { get; set; }
        public Enrollment enrollment { get; set; }
        public EmployeeAddress Address { get; set; }
        public EmployeeContactDetails ContactDetails { get; set; }
        public PersonalInfo PersonalInfo { get; set; }
        public List<FamilyDetails> FamilyDetails { get; set; }
        public List<Education> Education { get; set; }
        public List<PreviousEmployment> PreviousEmployment { get; set; }
        public Education GetEducation { get; set; }
        public FamilyDetails GetFamilyDetails { get; set; }
        
        public List<Skills> Skills { get; set; }
        public Skills GetSkills { get; set; }
        public List<Certifications> Certifications { get; set; }
        public Certifications GetCertifications { get; set; }
        public PreviousEmployment GetPreviousEmployment { get; set; }
        public List<PersonalDetailsList> PersonalDetailsApprovalList { get; set; }
        public List<PreEnrollCandidateLink> PreEnrollCandidateLinkApprovalList { get; set; }
        public List<Classifications> Languagess { get; set; }
        public List<EmployeeVsNominee> EmployeeVsNomineeList { get; set; }
        public EmployeeVsNominee GetEmployeeVsNominee { get; set; }
        public Filter Filter { get; set; }
        public string TabIndex { get; set; }
        public Guid AddressLogGI { get; set; }
        public Guid ContactDetailsLogGI { get; set; }
        public Guid PersonalInfoLogGI { get; set; }
        public Guid?[] LanguageLogGI { get; set; }
        public Guid?[] HobbiesLogGI { get; set; }
        public Guid SkillsLogGI { get; set; }
        public Guid CertificationsLogGI { get; set; }
        public Guid FamilyLogGI { get; set; }
        public Guid EducationLogGI { get; set; }
        public Guid PrevEmploymentLogGI { get; set; }
        public Guid ProfilePhotoLogGI { get; set; }
        public Guid PoliceVerificationLogGI { get; set; }
        public Guid MedicalCheckupLogGI { get; set; }
        public String ApproveMode { get; set; }
        public String ApproveComments { get; set; }
        public string SliderName { get; set; }
        public string Languages { get; set; }
        public string Hobbies { get; set; }
        public bool AddressApprovalPending { get; set; }
        public bool ContactDetailsApprovalPending { get; set; }
        public bool PersonalInfoApprovalPending { get; set; }
        public bool FamilyDetailsApprovalPending { get; set; }
        public bool EducationApprovalPending { get; set; }
        public bool SkillsApprovalPending { get; set; }
        public bool CertificationApprovalPending { get; set; }
        public bool PreviousEmploymentApprovalPending { get; set; }
        public bool EmployeeVsNomineeApprovalPending { get; set; }

        public bool ProfilePicApprovalPending { get; set; }
        public bool BankDetailsApprovalPending { get; set; }
        public bool DocumentDetailsApprovalPending { get; set; }
        public bool PoliceVerificationApprovalPending { get; set; }
        public bool MedicalCheckupApprovalPending { get; set; }
        public string Profile_AbsolutURI_ApprovalPending { get; set; }
        public string Profile_FileName_ApprovalPending { get; set; }
        public EmployeeVsNominee EmployeeVsNominee { get; set; }
        public List<CandidateDocuments> CandidateDocumentUpload { get; set; }
        public PoliceVerification PoliceVerification { get; set; }
        public MedicalCheckups MedicalCheckup { get; set; }
        public Guid EmployeeVsNomineeLogGI { get; set; }
        public Guid BankDetailsLogGI { get; set; }
        public int radiomode { get; set; }

        public Guid PreEnrollGI { get; set; }
        public File File { get; set; }
      
        public string Type { get; set; }
        public string AbsoluteUri { get; set; }
        public string Mode { get; set; }
        public int EmployeeCode { get; set; }
        public Url Url { get; set; }
        public string EmployeeID { get; set; }
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
        public List<PersonalDetails> PersonalDetailsList { get; set; }
        public StatutoryClassification StatutoryClassification { get; set; }
        public bool Applicable { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public List<CertificationDocList> CertificationDocList { get; set; }

    }
    public class CandidateDocuments
    { 
        public Guid LogGI { get; set; }
        public int DocumentCode { get; set; }
        public string DocumentName { get; set; }
        public string NameInDocument { get; set; }
        public string DocumentNumber { get; set; }
        public string ValidFrom { get; set; }
        public string ValidTo { get; set; }
        public File Document { get; set; }
        public int flag { get; set; }
    }
    public class MedicalCheckups
    {

        public Guid EmployeeGI { get; set; }
        public string TabIndex { get; set; }
        public File Attachment { get; set; }
        public MasterBase CheckupType { get; set; }
        public DateTime? CheckupDate { get; set; }
        public string HospitalDetails { get; set; }
        public DateTime? CheckupValidity { get; set; }
        public string HealthCardNumber { get; set; }
        public string CheckupDetails { get; set; }
        public string CheckupBy { get; set; }
        public string Remarks { get; set; }
        public bool ApprovedStatus { get; set; }
    }
    public class CertificationDocList
    {
        public string DocName { get; set; }
        public int CertificateDocCode { get; set; }
    }
}
