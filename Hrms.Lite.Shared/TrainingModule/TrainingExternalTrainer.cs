using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.TrainingModule
{
   public class TrainingExternalTrainer : MasterBase
    {
        public int ActiveCount { get; set; }
        public int InactiveCount { get; set; }
        public int PendingCount { get; set; }
        public List<TrainingExternalTrainer> TrainingList { get; set; }
        public UserBase User { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public string ContactAddress { get; set; }
        public string ApproximateAmount { get; set; }

    }
}
