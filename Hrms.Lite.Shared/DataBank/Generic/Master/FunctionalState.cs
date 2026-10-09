using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.Generic.Master
{
    public class FunctionalState : MasterBase
    {
        public int ActiveCount { get; set; }
        public int InactiveCount { get; set; }
        public int PendingCount { get; set; }
        public string FunctionalStateGI { get; set; }
        public List<FunctionalState> FunctionalStateList { get; set; }
       
    }
}
