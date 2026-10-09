using Hrms.Lite.Shared.General;
using System;

namespace Hrms.Lite.Shared.DataBank.Reports
{
    public class CandidateHeader
    {
        public Guid ApplicantGI { get; set; }
        public int CompanyCode { get; set; }
        public string ApplicantID { get; set; }
        public MasterBase Salutation { get; set; }
        public string ApplicantName { get; set; }
        public string FirstName { get; set; }
        public string MiddileName { get; set; }
        public string LastName { get; set; }
        public string CandidateAbsoluteUri { get; set; }
        public DateTime? DOB { get; set; }
        public string DateOfBirth { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public MasterBase Gender { get; set; }
        public string FileName { get; set; }
        public string Designation { get; set; }
        public string FunDesignation { get; set; }
        public string Location { get; set; }
        public string WageGrade { get; set; }
        public decimal Salary { get; set; }
        public File ProfilePhoto { get; set; }
    }
}