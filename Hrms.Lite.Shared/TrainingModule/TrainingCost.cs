using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.TrainingModule
{
   public class TrainingCost :MasterBase
    {
        public int ActiveCount { get; set; }
        public int InactiveCount { get; set; }
        public int PendingCount { get; set; }
        public UserBase User { get; set; }
        public List<TrainingCost> GetTrainingCostList { get; set; }
        public int TrainingCostHeadCode { get; set; }
        public string TrainingCostName { get; set; }
        public int Trainingcost { get; set; }
        public int TrainingCostHeadTypeCode { get; set; }
        public string TrainingCostHeadTypeName { get; set; }
    }
}
