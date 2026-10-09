using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.Master
{
    public class KeyPosition:MasterBase
    {
        public int ActiveCount { get; set; }
        public int InactiveCount { get; set; }
        public int PendingCount { get; set; }
        public List<KeyPosition> KeyPositionList { get; set; }        
        public Dropdown KeyPositionRisk { get; set; }
        public MasterBase Designation { get; set; }
        public string KeyPositionCodeDesc { get; set; }
        public List<KeyPositionClassifications> Skills { get; set; }
        public List<KeyPositionClassifications> SkillProficiency { get; set; }
        public List<KeyPositionClassifications> Education { get; set; }
        public List<KeyPositionClassifications> Languages { get; set; }
        public Gender Gender { get; set; }
        public int?[] Genders { get; set; }
        public Operators AgeAboveOrBelow { get; set; }
        public int? AgeFrom { get; set; }
        public int? AgeTo { get; set; }
        public Operators YSAboveOrBelow { get; set; }
        public int? YOSFrom { get; set; }
        public int? YOSTo { get; set; }
    }
    public class KeyPositionClassifications
    {
        public int CODE { get; set; }
        public string NAME { get; set; }
        public bool APPLICABILITY { get; set; }
        public bool Read { get; set; }
        public bool Write { get; set; }
        public bool Speak { get; set; }
    }
}

