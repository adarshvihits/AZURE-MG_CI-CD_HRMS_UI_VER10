using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.Master
{
   public class Rewards:MasterBase
    {
        public int ActiveCount { get; set; }
        public int InactiveCount { get; set; }
        public int PendingCount { get; set; }
        public string EntryType { get; set; }
        public RewardVsDepartment RewardVsDepartment { get; set; }
        public RewardVsPointCriteria RewardVsPointCriteria { get; set; }
        public List<Rewards> RewardsList { get; set; }
        public List<RewardVsPointCriteria> RewardVsPointCriteriaList { get; set; }
        public List<RewardVsDepartment> RewardsGridFill { get; set; }
    }

    public class RewardVsDepartment
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public bool Applicable { get; set; }
    }
    public class RewardVsPointCriteria
    {
        public int Code { get; set; }
        public int LogCode { get; set; }
        public string Name { get; set; }
        public int? Point { get; set; }
    }
}
