using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.Master
{
    public class JobPostingConfirmationViewModel
    {
        public int PendingCount { get; set; }
        public int ConfirmedCount { get; set; }

        public List<JobPostingConfirmation> Vacancies { get; set; }
            = new List<JobPostingConfirmation>();
    }

    public class JobPostingConfirmation
    {
        public int VacancySourceCode { get; set; }
        public int SourceTranactionCode { get; set; }

        public string BranchName { get; set; }
        public string DepartmentName { get; set; }
        public string SectionName { get; set; }
        public string GradeName { get; set; }
        public string VacancySource { get; set; }
        public string PriorityName { get; set; }
        public int PlanCount { get; set; }

        // Confirmed & posted tab
        public string PostedTo { get; set; }
        public string PostedBy { get; set; }
        public DateTime? PostedOn { get; set; }
    }

    public class JobPostingConfirmationDetails
    {
        public JobPostingConfirmation Vacancy { get; set; } = new JobPostingConfirmation();

        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public string BusinessType { get; set; }

        // Job master details panel (View)
        public string Qualification { get; set; }
        public string Experience { get; set; }
        public string LanguagesKnown { get; set; }
        public string SalaryRange { get; set; }
        public string SkillsRequired { get; set; }
        public string LastSavedBy { get; set; }
        public DateTime? LastSavedOn { get; set; }

        // Post To destinations from FIXED.mstJobPostingDestination
        public List<JobPostingConfirmationSource> Sources { get; set; }
            = new List<JobPostingConfirmationSource>();

        public string PostedBy { get; set; }
    }

    public class JobPostingConfirmationSource
    {
        public int JobSourceCode { get; set; }
        public string JobSourceName { get; set; }
        public bool Selected { get; set; }
    }

    public class JobPostingConfirmationRequest
    {
        public int VacancySourceCode { get; set; }
        public int SourceTranactionCode { get; set; }
        public int PostingDestinationCode { get; set; }
        public bool Naukri { get; set; }
        public bool LinkedIn { get; set; }
        public string PostedBy { get; set; }
    }
}
