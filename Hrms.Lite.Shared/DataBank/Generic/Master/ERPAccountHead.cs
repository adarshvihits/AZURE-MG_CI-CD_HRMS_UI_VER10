using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.Generic.Master
{
    public class ERPAccountHead : MasterBase
    {
        public int ActiveCount { get; set; }
        public int InactiveCount { get; set; }
        public int PendingCount { get; set; }
        public string ERPAccountHeadGI { get; set; }
        public List<ERPAccountHead> ERPAccountHeadList { get; set; }
        public int ACCUserCode { get; set; }

    }
}
