using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.Master
{
    public class SalaryPeriod : MasterBase
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int TotalDays { get; set; }
        public int AvgLOP { get; set; }
        public int AvgDayOT { get; set; }
        public List<SalaryPeriod> Month { get; set; }
        public DateTime AttendanceCloseDate { get; set; }
        public LeavePeriod LeavePeriod { get; set; }
        public decimal AvgPrDaysOffday { get; set; }
        public int AvgDaysLJP { get; set; }
        public int AvgDaysLTP { get; set; }
        public int AvgDaysESI { get; set; }
    }
}
