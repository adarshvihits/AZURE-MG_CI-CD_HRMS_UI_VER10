using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.DTO
{
    public class JobPosting
    { 
        public int Tab1Count { get; set; }
        public int Experience { get; set; }
        public int Tab2Count { get; set; }
        public int JobPostingCode { get; set; }
        public Guid MRFGI { get; set; }

        public List<PostedIn> PostedIn { get; set; }
        public List<JobPostingList> JobPostingList { get; set; }
        public JobPosting_Details Details { get; set; }
    }
    public class PostedIn
    {
        public int JobSourceCode { get; set; }
        public string JobSourceName { get; set; }
        public bool Applicability { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
    }
    public class JobPostingList
    {
        public int VacancyManagementCode { get; set; }
        public Guid MRFGI { get; set; }
        public string JobCode { get; set; }
        public string JobTitle { get; set; }
        public string JobpostedIn { get; set; }
        public DateTime NeedByDate { get; set; }
        public int NoOfRequirements { get; set; }
        public string RequestedBy { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public string Gender { get; set; }
        public Decimal Experience { get; set; }
        public int Aging { get; set; }
        public string RecruitmentComments { get; set; }
        public int MappedResumes { get; set; }
        public DateTime RequestedDate { get; set; }
        public bool IsJobDescriptionNotPresent { get; set; }


    }
    public class JobPosting_Details
    {
        public int VacancyManagementCode { get; set; }
        public Guid MRFGI { get; set; }
        public string FullName { get; set; }
        public int EmployeeCode { get; set; }
        public string JobCode { get; set; }

        public string JobTitle { get; set; }

        public string Description { get; set; }
        public DateTime NeedByDate { get; set; }
        public int NoOfRequirements { get; set; }
        public decimal ExGrossPay { get; set; }
        public Department Department { get; set; }
        public Designation Designation { get; set; }
        public Location Location { get; set; }
        public Gender Gender { get; set; }
    }
}
