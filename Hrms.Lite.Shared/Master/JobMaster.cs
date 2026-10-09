using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.Master
{
    public class JobMaster : MasterBase
    {
        public string JobCode { get; set; }
        public Dropdown Department { get; set; }
        public Dropdown Section { get; set; }
        public Dropdown BusinessType { get; set; }
        public Dropdown WorkMode { get; set; }
        public Dropdown EmploymentType { get; set; }
        public Dropdown ExperienceLevel { get; set; }
        public Dropdown Industry { get; set; }
        public string SalaryRange { get; set; }
        public string Skill { get; set; }
        public int?[] Grades { get; set; }
        public string GradeName { get; set; }
        public int?[] Qualifications { get; set; }
        public string ApprovalStatus { get; set; }
        public int ActiveCount { get; set; }
        public int InactiveCount { get; set; }
        public int PendingCount { get; set; }
        public List<JobMaster> JobMasterList { get; set; }
    }
}
