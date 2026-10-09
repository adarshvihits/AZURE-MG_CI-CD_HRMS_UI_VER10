using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.Recruitment
{
    // One row of the Recruiter Screening vacancy list: a job at a branch / department / section / grade
    public class RecruiterScreeningVacancy
    {
        public int JobCode { get; set; }
        public string JobName { get; set; }
        public string BusinessTypeName { get; set; }
        public int LocationCode { get; set; }
        public string BranchName { get; set; }
        public int DepartmentCode { get; set; }
        public string DepartmentName { get; set; }
        public int SectionCode { get; set; }
        public string SectionName { get; set; }
        public int GradeCode { get; set; }
        public string GradeName { get; set; }
        public int Openings { get; set; }
        public int MappedCount { get; set; }
        public int PositionPlanCode { get; set; }
    }

    // Applicants for one vacancy group: already mapped (Linked) and unattached (Suggested)
    public class RecruiterScreeningCandidates
    {
        public List<RecruiterScreeningCandidate> Linked { get; set; } = new List<RecruiterScreeningCandidate>();
        public List<RecruiterScreeningCandidate> Suggested { get; set; } = new List<RecruiterScreeningCandidate>();
    }

    public class RecruiterScreeningCandidate
    {
        // JobApplicationCode for linked applicants, ApplicantCode for unattached ones
        public int Code { get; set; }
        public string ApplicantID { get; set; }
        public string ApplicantName { get; set; }
        public string QualificationLevel { get; set; }
        public string Experience { get; set; }
        // AI match score for the vacancy, in percent
        public int MatchPercent { get; set; }
    }

    public class RecruiterScreeningMapRequest
    {
        public int ApplicantCode { get; set; }
        public int LocationCode { get; set; }
        public int DepartmentCode { get; set; }
        public int SectionCode { get; set; }
        public int GradeCode { get; set; }
        public int PositionPlanCode { get; set; }
    }
}
