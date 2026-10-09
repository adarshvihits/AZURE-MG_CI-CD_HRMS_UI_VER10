using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.Admin
{
    public class RenewalsCategory: MasterBase
    {
        public int RenewalsCategoryCode { get; set; }
        public int ActiveCount { get; set; }
        public int InactiveCount { get; set; }
        public int PendingCount { get; set; }
        public List<RenewalsCategory> RenewalsCategoryList { get; set; }
        public UserBase User { get; set; }
        public string RenewalCategory { get; set; }
    }
}
