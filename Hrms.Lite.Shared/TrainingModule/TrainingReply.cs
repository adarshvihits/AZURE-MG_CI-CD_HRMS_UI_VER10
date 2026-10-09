using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.TrainingModule
{
  public  class TrainingReply :MasterBase
    {
        public int ActiveCount { get; set; }
        public int InactiveCount { get; set; }
        public int PendingCount { get; set; }

        public Dropdown TrainingReplyGroup { get; set; }

        public List<TrainingReply> TrainingReplyList { get; set; }
        public UserBase User { get; set; }


    }
}
