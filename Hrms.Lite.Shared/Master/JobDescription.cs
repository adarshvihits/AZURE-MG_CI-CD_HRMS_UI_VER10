using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.Master
{
    public class JobDescription : MasterBase
    {
        public int MasterJobCode { get; set; }
        public Dropdown MasterJob { get; set; }
        public int BusinessTypeCode { get; set; }
        public Dropdown BusinessType { get; set; }
        public int DepartmentCode { get; set; }
        public Dropdown Department { get; set; }
        public int SectionCode { get; set; }
        public Dropdown Section { get; set; }
        public int GradeCode { get; set; }
        public Dropdown Grade { get; set; }
        public string JDescription { get; set; }
        public int ActiveCount { get; set; }
        public int InactiveCount { get; set; }
        public int PendingCount { get; set; }
        public List<JobDescription> JobDescriptionList { get; set; }
    }
}
