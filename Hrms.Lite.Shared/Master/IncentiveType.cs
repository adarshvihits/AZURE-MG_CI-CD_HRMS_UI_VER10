using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.Master
{
    public class IncentiveType : MasterBase
    {
        public int ActiveCount { get; set; }
        public int InactiveCount { get; set; }
        public int PendingCount { get; set; }
        public Dropdown Product { get; set; }
        public Dropdown SubProduct { get; set; }
        public bool ProfitBased { get; set; }
        public List<IncentiveType> IncentiveTypeList { get; set; }
    }
}
