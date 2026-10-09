using Hrms.Lite.Shared.General;
using System;

namespace Hrms.Lite.Shared.DataBank.Reports
{
    public class CandidateBasicInfo
    {
        public Guid ApplicantGI { get; set; }
        public int CompanyCode { get; set; }
        public string ApplicantID { get; set; }
        public MasterBase Salutation { get; set; }
        public string ApplicantName { get; set; }
        public string FirstName { get; set; }
        public string MiddileName { get; set; }
        public string LastName { get; set; }
        public DateTime? DOB { get; set; }

        public string Mobile { get; set; }
        public string WhatsappNo { get; set; }
        public string Email { get; set; }
        public MasterBase Gender { get; set; }
        public string AdharNo { get; set; }
        public string NameInAdhar { get; set; }
        public string PanNo { get; set; }
        public string Photo { get; set; }
        public File EmployeeThumbNail { get; set; }
        public File File { get; set; }
        public bool IsPoliceRecord { get; set; }
        public string PoliceRecordDetails { get; set; }

        public bool IsMedicalAllergies { get; set; }
        public string MedicalAllergiesDetails { get; set; }
        public bool IsTravelRestrictions { get; set; }
        public string TravelRestrictionDetails { get; set; }
        public bool IsHealthIssue { get; set; }
        public string HealthIssueDetails { get; set; }
        public bool IsAnyRelativesEmployed { get; set; }
        public string EmployedRelativeName { get; set; }
        public string EmployedPosition { get; set; }
        public string EmployedDepartment { get; set; }
        public string EmployedRelation { get; set; }
        public bool IsRelativesAssociated { get; set; }
        public string AssociatedRelativeName { get; set; }
        public string AssociatedRelativePosition { get; set; }
        public string AssociatedRelativeDepartment { get; set; }
        public string AssociatedRelativeRelation { get; set; }
        public bool IspreviouslyEmployed { get; set; }
        public Nullable<DateTime> PreviouslyEmployedDOJ { get; set; }
        public Nullable<DateTime> PreviouslyEmployedDOR { get; set; }
        public string PreviouslyEmployedDesignation { get; set; }
        public decimal PreviouslyEmployedCTC { get; set; }

    }
}