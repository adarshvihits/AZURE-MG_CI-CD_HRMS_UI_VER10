using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.Reports
{
    public class SkillVariance
    {
        public int SkillCode { get; set; }
        public int SkillTypeCode { get; set; }
        public string SkillName { get; set; }
        public int BasicAvailable { get; set; }
        public int BasicRequirments { get; set; }
        public int BasicVariance { get; set; }
        public int IntermediateAvailable { get; set; }
        public int IntermediateRequirments { get; set; }
        public int IntermediateVariance { get; set; }
        public int BeingDevelopedAvailable { get; set; }
        public int BeingDevelopedRequirments { get; set; }
        public int BeingDevelopedVariance { get; set; }
        public int AdvancedAvailable { get; set; }
        public int AdvancedRequirments { get; set; }
        public int AdvancedVariance { get; set; }
        public int ExpertAvailable { get; set; }
        public int ExpertRequirments { get; set; }
        public int ExpertVariance { get; set; }
        public List<SkillVariance> SKillVarianceLists { get; set; }
    }
}
