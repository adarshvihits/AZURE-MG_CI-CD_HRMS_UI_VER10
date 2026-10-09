using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.TrainingModule
{
    public class TrainingCostReport
    {
        public int ScheduleCode { get; set; }
        public string Schedule { get; set; }
        public int TotalTrainees { get; set; }
        public int Duration { get; set; }
        public int TrainingCost { get; set; }
        public int Trainercost { get; set; }
        public int TraineeCost { get; set; }
        public int TotalCost { get; set; }
        public int CostPerHead { get; set; }
        public int FYCode { get; set; }
        public int CategoryCode { get; set; }
        public int ManHours { get; set; }

        public List<TrainingCostReport> TrainingCostReportList { get; set; }

    }
}
