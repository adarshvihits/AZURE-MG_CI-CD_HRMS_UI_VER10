using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.TrainingModule
{
   public class TrainingReport:MasterBase
    {
        public int TrainingCode { get; set; }
        public string TrainingName { get; set; }
        public string Schedule { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Count { get; set; }
      
        public int Duration { get; set; }
        public int FYCode { get; set; }
        public int FYName { get; set; }
        public int TrainerCost { get; set; }
        
        public int TrainingCost { get; set; }
        public int TraineeCost { get; set; }
        public int TotalCost { get; set; }

        public List<TrainingReport> TrainingReportList { get; set; }

    }
}
