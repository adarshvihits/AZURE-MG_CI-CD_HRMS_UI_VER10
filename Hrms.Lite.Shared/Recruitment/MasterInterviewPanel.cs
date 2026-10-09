using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;

namespace Hrms.Lite.Shared.Recruitment
{
    public class MasterInterviewPanel
    {
        public UserBase User { get; set; }
        public Guid PanelGI { get; set; }
        public int PanelCode { get; set; }
        public string PanelID { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public string Mode { get; set; }
        public int LocationCode { get; set; } // branch
        public string LocationName { get; set; }
        public int DepartmentCode { get; set; }
        public string DepartmentName { get; set; }
        public int SectionCode { get; set; }
        public string SectionName { get; set; }
        public int GradeCode { get; set; }
        public string GradeName { get; set; }
        public string Description { get; set; }
        public int MemberCount { get; set; }
        public List<MasterInterviewPanelMember> MemberList { get; set; }
        public List<MasterInterviewPanel> PanelList { get; set; }
        public List<Dropdown> LocationList { get; set; }
        public List<Dropdown> DepartmentList { get; set; }
        public List<Dropdown> SectionList { get; set; }
        public List<Dropdown> GradeList { get; set; }
    }

    public class MasterInterviewPanelMember
    {
        public int MemberCode { get; set; }
        public string InterviewerName { get; set; }
        public string Role { get; set; } // Chairperson / Department Head / Technical Panelist / HR Panelist / External Expert
        public string Designation { get; set; }
        public string Email { get; set; }
        public int DisplayOrder { get; set; }
    }
}
