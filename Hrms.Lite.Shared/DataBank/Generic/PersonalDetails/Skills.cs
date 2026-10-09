using Hrms.Lite.Shared.DataBank.Generic.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.Generic.PersonalDetails
{
   public class Skills
    {
        public Guid SkillLogGI { get; set; }
        public Skill Skill { get; set; }
        public SkillType SkillType { get; set; }
        public SkillProficiency SkillProficiency { get; set; }
        public bool ApprovedStatus { get; set; }
        public int flag { get; set; }
    }
}
