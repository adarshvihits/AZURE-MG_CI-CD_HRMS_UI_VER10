using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.Master
{
   public class JoiningCheckList:MasterBase
    {
        public int ActiveCount { get; set; }
        public int InactiveCount { get; set; }
        public int PendingCount { get; set; }
        public MasterBase CheckListType { get; set; }
        public List<JoiningCheckList> JoiningChecklist { get; set; }
    }
}
