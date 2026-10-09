using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.TrainingModule
{
  public  class TrainingQuestions:MasterBase
    {
        public int ActiveCount { get; set; }
        public int InactiveCount { get; set; }
        public int PendingCount { get; set; }
        public Dropdown ReplygroupCode { get; set; }
        public string ReplyGroupName { get; set; }
        public string TestLevel { get; set; }
        public int TestLevelCode { get; set; }
        public Dropdown TrainingCode { get; set; }
        public Dropdown ReplyCode { get; set; }
        public string Training { get; set; }

        public string Reply { get; set; }

        public List<TrainingQuestions> TrainingQuestionsList { get; set; }
    }
}
