using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.Generic.Master
{
    public class ShiftDetails : MasterBase
    {
        public int ActiveCount { get; set; }
        public int InactiveCount { get; set; }
        public int PendingCount { get; set; }
        public string ShiftCode { get; set; }
        public ShiftType ShiftType { get; set; }
        public ShiftCategory ShiftCategory { get; set; }
        public DutyRosterColor DutyRosterFontColor { get; set; }
        public string ShiftStartTime { get; set; }
        public DutyRosterColor DutyRosterBackgroundColor { get; set; }
        public string ShiftEndTime { get; set; }
        public string BreakHourStartTime { get; set; }
        public string BreakHourEndTime { get; set; }
        public string FullDayHour { get; set; }
        public string HalfDayHour { get; set; }
        public string BreakHour { get; set; }
        public string ScheduledHour { get; set; }
        public bool ShiftAllowanceApplicable { get; set; }
        public List<ShiftDetails> ShiftList { get; set; }
        public string HPOPFullDayHour { get; set; }
        public string HPOPHalfDayHour { get; set; }
    }
    public class DutyRosterColor : MasterBase
    {

    }
}
