using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.Master
{
    public class TrainingMaster : MasterBase
    {
        public int ActiveCount { get; set; }
        public int InactiveCount { get; set; }
        public int PendingCount { get; set; }        
        public string Status { get; set; }
        public string EntryType { get; set; }
        public TrainingDropDwn TrainingType { get; set; }
        public TrainingDropDwn AssesmentMode { get; set; }
        public TrainingDropDwn TrainingCategory { get; set; }
        public List<TrainingMaster> TrainingMasterList { get; set; }
    }
    public class TrainingDropDwn : MasterBase
    {

    }
}
