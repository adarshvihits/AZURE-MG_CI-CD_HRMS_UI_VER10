using Hrms.Lite.Shared.MIS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Shared.DashBoard
{
    public class HRPlanningDashBoard
    {
        public int PlanningPeriod { get; set; }
        public string Todate { get; set; }
        public int YTDnumberofLeavers { get; set; }
        public int HeadCount { get; set; }
        public int PreviousHeadCount { get; set; }
        public decimal HeadCountRate { get; set; }
        public decimal CurrentLeaversRate { get; set; }
        public int CurrentLeavers { get; set; }
        public int NewJoineeCount { get; set; }
        public int PreviousMonthLeavers { get; set; }
        public decimal AnnualFluctuationRate { get; set; }
        public List<MonthWiseTerminationList> MonthWiseTerminationList { get; set; }
        public List<SepJoiningClassifnDiagram> SepJoiningClassifnDiagram { get; set; }
    }
        public class SepJoiningClassifnDiagram
        {
            public string Type { get; set; }
            public string TypeFullName { get; set; }
            public int HeadCount { get; set; }
            public int TargetCount { get; set; }
          


        }
    
}
