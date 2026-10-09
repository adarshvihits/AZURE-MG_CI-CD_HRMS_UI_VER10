using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Shared.Master
{
    public class JobTransitionReason : MasterBase
     {
        public int ActiveCount { get; set; }
        public int InactiveCount { get; set; }
        public int PendingCount { get; set; }
       public int?[] JobTransitionsTypes { get; set; }
        public string EntryType { get; set; }
        public List<JobTransitionReason> JobTransitionReasonList { get; set; }
        public JobTransitionsType JobTransitionsType { get; set; }
    }

}
