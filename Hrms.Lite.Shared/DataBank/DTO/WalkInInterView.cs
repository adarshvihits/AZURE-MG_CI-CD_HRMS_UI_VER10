using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.DTO
{
    public class WalkInInterView
    {
        public UserBase User { get; set; }
        public int VacancyManagementCode { get; set; }
        public Guid VacancyManagementGI { get; set; }
        public List<VacancyManagement_List> VacancyList { get; set; }
        public VacancyManagement_Details VacancyDetails { get; set; }
        public WalkInInterView_CandidateDetails CandidateDetails { get; set; }

    }
    public class WalkInInterView_CandidateDetails
    {
        public File CandidatePhoto { get; set; }
        public string RefNo { get; set; }
        public Salutation Salutation { get; set; }
        public string FullName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public DateTime? DOB { get; set; }
        public Gender Gender { get; set; }
        public string Adhar { get; set; }
        public string PAN { get; set; }
        public string DrivingLicenseNo { get; set; }
        public Qualification Qualification { get; set; }
        public int FresherOrExperienced { get; set; }
        public string CurrentEmployer { get; set; }
        public decimal Experience { get; set; }
        public string CurrentEmployementComments { get; set; }
        public DateTime? InterviewDate { get; set; }
        public EmployeeHeader Interviewer { get; set; }
        public string InterViewFeedBack { get; set; }
        public InterviewSelection InterviewSelection { get; set; }
        public File Resume { get; set; }
        public File KYCDoc { get; set; }
        public File File { get; set; }
    }
}
