using Hrms.Lite.Shared.General;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
namespace Hrms.Lite.Shared.Master
{
    public class ApproverRole : MasterBase
    {
        public int ActiveCount { get; set; }
        public int InactiveCount { get; set; }
        public string EntryType { get; set; }
        public List<ApproverRole> ApproverRoleList { get; set; }
        public MasterBase Applicability { get; set; }
        public MasterBase Country { get; set; }
        public MasterBase Division { get; set; }
        public MasterBase Location { get; set; }
        public MasterBase Department { get; set; }
        public int?[] Locations { get; set; }
        public int?[] Countrys { get; set; }
        public int?[] Departments { get; set; }
        public int?[] Sections { get; set; }
        public MasterBase Employee{ get; set; }
        public string CountryName { get; set; }
        public string SectionName { get; set; }
        public string LocationName { get; set; }
        public string DepartmentName { get; set; }
        public MasterBase Role { get; set; }
        public List<Dropdown> DropdownData { get; set; }
        public int ApplicabilityCode { get; set; }
        public string ApplicabilityName { get; set; }
    }
}
