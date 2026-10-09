using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.DTO
{
    public class SkillRequisition
    {
        public Guid GI { get; set; }
        public int SkillCode { get; set; }
        public int SkillTypeCode { get; set; }
        public int SkillProficiencyCode { get; set; }
        public string SkillTypeName { get; set; }
        public string SkillName { get; set; }
        public int RequirmentCount { get; set; }
        public int AvailableCount { get; set; }
        public List<SkillRequisition> SkillRequisitionList { get; set; }
    }
}
