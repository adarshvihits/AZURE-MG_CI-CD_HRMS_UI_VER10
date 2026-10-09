using Hrms.Lite.Shared.DataBank.Generic;
using Hrms.Lite.Shared.DataBank.Generic.Master;

using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;


namespace Hrms.Lite.Shared.DataBank.DTO
{
    public class ResumeDataBank
    {
        public int TabIndex { get; set; }
        public int FresherOrExperienced { get; set; }
        public int ResumeDataBankCode { get; set; }
        public Guid ResumeDataBankGI { get; set; }
        public int AllTabCount { get; set; }
        public int Tab1Count { get; set; }
        public int Tab2Count { get; set; }
        public int Tab3Count { get; set; }
        public int Tab4Count { get; set; }
        public int Tab5Count { get; set; }
        public string Mode { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public List<ResumeDataBank_List> ResumeDataBank_List { get; set; }
        public ResumeDataBank_Details Details { get; set; }
        public List<ResumeDataBank_Experience> Experience { get; set; }
        public List<ResumeDataBank_Education> Education { get; set; }
        public List<ResumeDataBank_Certifications> Certifications { get; set; }
        public List<ResumeDataBank_Skill> Skill { get; set; }
        public List<ResumeDataBankListTabs> ResumeDataBankListTabs { get; set; }
        public ResumeFilter Filter { get; set; }
        public string SelectedTab { get; set; }
        public Breadscrumb Breadscrumb { get; set; }
    }
    public class ResumeDataBank_List
    {
        public int ResumeDataBankCode { get; set; }
        public Guid ResumeDataBankGI { get; set; }
        public string RefNo { get; set; }
        public string CandidateName { get; set; }
        public string MobileNo { get; set; }
        public int Age { get; set; }
        public string WorkLocation { get; set; }
        public string Designation { get; set; }
        public string Email { get; set; }
        public string YOS { get; set; }
        public string Qualification { get; set; }
        public string Stage { get; set; }
        public string SuitedLocation { get; set; }
        public string SuitableDesignation { get; set; }
        public string MRFNo { get; set; }
        public string JobTitle { get; set; }
        public string HeldAt { get; set; }
        public string RejectedAt { get; set; }
        public string RejectReason { get; set; }
        public string Resumes { get; set; }
        public string Remarks { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public int AllTabCount { get; set; }
        public int Tab1Count { get; set; }
        public int Tab2Count { get; set; }
        public int Tab3Count { get; set; }
        public int Tab4Count { get; set; }
        public int Tab5Count { get; set; }


    }
    public class ResumeDataBankListTabs
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }

    }
    public class ResumeDataBank_Details
    {
        public string RefNo { get; set; }
        public string FullName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string House { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Pin { get; set; }
        public Country Country { get; set; }
        public State State { get; set; }
        public District District { get; set; }
        public DateTime? DOB { get; set; }
        public Gender Gender { get; set; }
        public MaritalStatus MaritalStatus { get; set; }
        public int?[] SuitedDesignation { get; set; }
        public string[] SuitedDesignationName { get; set; }
        public Designation Designation { get; set; }
        public ResumeSource ResumeSource { get; set; }
        public File CandidatesPhoto { get; set; }
        public string CandidatePhotoFileName { get; set; }
        public File Resume { get; set; }
        public string Description { get; set; }
        public int?[] SuitableLocation { get; set; }
        public string[] SuitedLocationName { get; set; }
        public Location Location { get; set; }
        public string Adhar { get; set; }
        public string PAN { get; set; }
        public string DrivingLicenseNo { get; set; }
        public File PassPort { get; set; }
        public File PANDoc { get; set; }
        public File DrivingLicence { get; set; }//Dri
        public File aadhar { get; set; }
        public OtherDocList OtherDoc { get; set; }
        public int DocType { get; set; }

    }
    public class ResumeDataBank_Experience
    {

        public string CompanyName { get; set; }
        public string Location { get; set; }
        public string Designation { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public decimal Experience { get; set; }
        public string Area { get; set; }
    }
    public class ResumeDataBank_Education
    {
        public QualificationType QualificationType { get; set; }
        public List<Dropdown> QualifnDropdown { get; set; }
        public List<Dropdown> speznDropdown { get; set; }
        public Qualification Qualification { get; set; }
        public Specialisation SpecializationOfBranch { get; set; }
        public int? YearOfCompletion { get; set; }
    }
    public class ResumeDataBank_Certifications
    {
        public string Certificate { get; set; }
        public CertificateType CertificateType { get; set; }
        public string IssuedBy { get; set; }
        public DateTime? IssuedDate { get; set; }
        public CertificateMode CertificateMode { get; set; }
        public CertificateValidity CertificateValidity { get; set; }
        public DateTime? RenewalDate { get; set; }
    }
    public class ResumeDataBank_Skill
    {
        public List<Dropdown> SkillDropdown { get; set; }
        public Skill Skill { get; set; }
        public SkillType SkillType { get; set; }
        public SkillProficiency SkillProficiency { get; set; }
    }
    public class ResumeSource : MasterBase
    { }
}
