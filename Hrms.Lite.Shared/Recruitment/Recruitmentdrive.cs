using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace Hrms.Lite.Shared.Recruitment
{
    public class Recruitmentdrive : MasterBase
    {
        public string LocationName { get; set; }
        public Dropdown Venue { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public decimal? Cost { get; set; }
        public string Remarks { get; set; }
        public int DisplayOrder { get; set; }
        public int ActiveCount { get; set; }
        public int InactiveCount { get; set; }
        public int PendingCount { get; set; }

        public string ApprovalStatus { get; set; }
        public bool InUse { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public string EntryComments { get; set; }
        public Recruitmentdrive OldData { get; set; }

        public List<RecruitmentDriveCost> CostSplitUpList { get; set; }
        public List<Recruitmentdrive> List { get; set; }

        public SelectList DropdownData { get; set; }
    }

    public class RecruitmentDriveCost : MasterBase
    {
        public Guid? RecruitmentDriveGI { get; set; }
        public Dropdown CostHead { get; set; }
        public decimal? Amount { get; set; }
        public string Remarks { get; set; }
    }
}