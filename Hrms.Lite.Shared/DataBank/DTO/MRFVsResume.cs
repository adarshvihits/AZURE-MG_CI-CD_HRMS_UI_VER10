using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.DTO
{
    public class MRFVsResume
    {
        public int VacancyManagementCode { get; set; }
        public Guid VacancyManagementGI { get; set; }
        public string JobCode { get; set; }
        public string JobTitle { get; set; }
        public DateTime NeedByDate { get; set; }
        public DateTime? RequestedDate { get; set; }

        public int NoOfRequirements { get; set; }
        public string Location { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public int LocationCode { get; set; }
        public string Gender { get; set; }
        public int DaysLeft { get; set; }
        public int Aging { get; set; }
        public int Shortlisted { get; set; }
        public int InterviewEvaluation { get; set; }
        public int FinalApprove { get; set; }
        public string RecruitmentComments { get; set; }
        public int MappedResumes { get; set; }
        public List<MRFVsResume> VacancyManagement_List { get; set; }
        public List<ResumeList> ResumeList_List { get; set; }
        public ResumeFilter Filter { get; set; }
    }
    public class ResumeList : ResumeDataBank_List
    {
        public int ComboType { get; set; } //1-ALL,2.Matching
        public bool Applicable { get; set; }
    }
}
