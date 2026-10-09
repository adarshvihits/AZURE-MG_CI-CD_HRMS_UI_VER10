using Hrms.Lite.Shared.DataBank.Generic;
using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.DTO
{
    public class PreEnrollment
    {
        public int PreEnrollCode { get; set; }
        public string Tab { get; set; }
        public Url Url { get; set; }
        public Guid? PreEnrollGI { get; set; }
        public int ResumeDataBankCode { get; set; }
        public Guid? ResumeDataBankGI { get; set; }
        public int OfferProcessingCode { get; set; }
        public Guid? OfferProcessingGI { get; set; }
        public EmployeeHeader EmployeeHeader { get; set; }
        public EmployeeBasicInfo BasicInfo { get; set; }
        public EmployeeClassification JobDetails { get; set; }
        public CompensationStructure Compensation { get; set; }
        public ContactDetails PermanentAddress { get; set; }
        public string Mode { get; set; }
        public string FormName { get; set; }
        public List<AlreadyExists_OnBoarding> AlreadyExists_OnBoarding { get; set; }
        public CreditEmployeementChecking CreditChecking { get; set; }
        public CreditEmployeementChecking EmployeementChecking { get; set; }
        public string JobCode { get; set; }
        public string JobTitle { get; set; }
        public DateTime? NeedByDate { get; set; }
        public string Description { get; set; }
        public int MinimumSalary { get; set; }
        public int MaximumSalary { get; set; }
        public int PFMaxValue { get; set; }
        public string selectedTab {get;set;}
        public int  VacancyManagementCode { get; set; }
        public int WFTrnCode { get; set; }
        public List<FiledValidations> FiledValidations { get; set; }
        public string Reason { get; set; }
        public int ApplicantTrackingCode { get; set; }
        public string OfferEntryFrom { get; set; }
        public int LogCode { get; set; }
        public string EntryUser { get; set; }
        public DateTime EntryDate { get; set; }
    }
    public class CreditEmployeementChecking
    {
       
        public int ResumeDataBankCode { get; set; }
        public Guid? ResumeDataBankGI { get; set; }
        public int PreEnrollCode { get; set; }
        public Guid? PreEnrollGI { get; set; }
        public int SelectionStatus { get; set; }
        public int? CreditScore { get; set; }
        public File Attachment { get; set; }
        public string Remarks { get; set; }
    }
    public class FiledValidations
    {
        public int FiledCode { get; set; }
        public string FiledName { get; set; }
        public string Type { get; set; }
        public string Tab { get; set; }
        public bool Applicability { get; set; }

    }

}
