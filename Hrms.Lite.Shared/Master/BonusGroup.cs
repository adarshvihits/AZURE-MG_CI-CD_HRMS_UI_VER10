using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.Master
{
    public class BonusGroup : MasterBase
    {       
        public decimal? SatuatoryPerc { get; set; }
        public decimal? NonSatuatoryPerc { get; set; }
        public decimal? Limit { get; set; }
        public decimal? EligibilityLimit { get; set; }
        public List<BonusGroup> BonusGroupList { get; set; }
        public List<BonusGroup_Allowance> Allowance { get; set; }
    }
    public class BonusGroup_Allowance
    {
        public int AllowanceCode { get; set; }
        public Guid AllowanceGI { get; set; }
        public string Allowance { get; set; }
        public decimal Percentage { get; set; }
        public bool Applicability { get; set; }
    }
}
