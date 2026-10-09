using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.TrainingModule
{
   public class TrainingReplyGroup : MasterBase
    {
        public int ActiveCount { get; set; }
        public int InactiveCount { get; set; }
        public int PendingCount { get; set; }
        public List<TrainingReplyGroup> TrainingReplaygroupList { get; set; }
        public int radiomode { get; set; }
        public int Type { get; set; }
        public string typetext { get; set; }
    }
}
