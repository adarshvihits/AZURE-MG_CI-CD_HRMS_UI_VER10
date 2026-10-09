using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.Master
{
    public class AppraisalTypeMaster : MasterBase
    {
        public int ActiveCount { get; set; }
        public int InactiveCount { get; set; }
        public int PendingCount { get; set; }       
        public string Status { get; set; }
        public string EntryType { get; set; }
        public AppraisalTypeDropDwn AssesmentMode { get; set; }
        public List<AppraisalTypeMaster> AppraisalTypeMasterList { get; set; }
    }
    public class AppraisalTypeDropDwn : MasterBase
    {

    }
}
