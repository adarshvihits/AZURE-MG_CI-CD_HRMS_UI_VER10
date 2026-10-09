using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.Generic
{
    public class AttendanceSettings
    {
        public ShiftType ShiftType { get; set; }
        public string ShiftDetails { get; set; }
        public int[] ShiftDetailsIds { get; set; }
        public int? ShiftDetailsId { get; set; }
        public string PunchingMethods { get; set; }
        public int[] PunchingMethodIDs { get; set; }
        public MasterBase ShiftCircle { get; set; }
        public LeaveGroup LeaveGroup { get; set; }
        public HolidayGroup HolidayGroup { get; set; }
        public OffdaySettings OffdaySettings { get; set; }
        public MasterBase PunchingApplicability { get; set; }
    }
}
