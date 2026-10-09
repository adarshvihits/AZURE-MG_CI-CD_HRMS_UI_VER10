using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.Master
{
   public class Asset:MasterBase
    {
        public int ActiveCount { get; set; }
        public int InactiveCount { get; set; }
        public int PendingCount { get; set; }
        public string Status { get; set; }
        public string EntryType { get; set; }
        public Condition Condition { get; set; }
        public Category AssetCategory { get; set; }
        public string AssetName { get; set; }
        public string AssetCode { get; set; }
        public DateTime? DateOfPurchase { get; set; }
        public string Brand { get; set; }
        public string ModelNo { get; set; }
        public int? Cost { get; set; }
        public Department IssuingDepartment { get; set; }
        public string Specification { get; set; }
        public List<Asset> AssetList { get; set; }
    }
    public class AssetDropdwn : MasterBase
    {
    }
}
