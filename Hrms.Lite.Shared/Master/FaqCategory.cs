using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.Master
{
   public class FaqCategory : MasterBase
    {
        public int PendingCount { get; set; }
        public int ActiveCount { get; set; }
        public int InactiveCount { get; set; }
        public List<FaqCategory> FaqCategoryList { get; set; }
    }
}
