using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.Master
{
    public class WPSAccountHead : MasterBase
    {
        public int ActiveCount { get; set; }
        public int InactiveCount { get; set; }
        public int PendingCount { get; set; }
        public List<AccountsHead> AccountsHeadList { get; set; }
        public List<WPSAccountHeadTablist> WPSAccountHeadTablist { get; set; }
        public string AccountsHeadUserCode { get; set; }
        public int AccountsHeadCodeType { get; set; }
        public string AccountsHeadCodeTypeName { get; set; }
        public string TabName { get; set; }
    }
    public class WPSAccountHeadTablist
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public int WPSHeader { get; set; }
        public string WPSHeaderName { get; set; }
        public string AllowanceType { get; set; }
    }
}