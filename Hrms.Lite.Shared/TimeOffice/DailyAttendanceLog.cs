using Hrms.Lite.Shared.TimeOffice.Generic.Master;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using Hrms.Lite.Shared.TimeOffice.Generic;

namespace Hrms.Lite.Shared.TimeOffice
{
    public class DailyAttendanceLog
    {
        public UserBase User { get; set; }
        public Filter Filter { get; set; }
        public DateTime? SelectedDate { get; set; }
        public string InTime { get; set; }
        public string OutTime { get; set; }
        public int EmployeeCode { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public Guid EmployeeGI { get; set; }
        public Designation Designation { get; set; }
        public Department Department { get; set; }
        public Location Location { get; set; }
        public List<DailyAttendanceLog> LogList { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
    }
}
