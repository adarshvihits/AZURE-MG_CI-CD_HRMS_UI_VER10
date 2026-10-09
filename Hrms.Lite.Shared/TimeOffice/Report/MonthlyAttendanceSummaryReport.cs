using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice.Generic.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.TimeOffice.Report
{
    public class MonthlyAttendanceSummaryReport
    {
        public Month Month { get; set; }
        public List<MonthlyAttendanceProcessing_Data> MonthlyAttendanceProcessing_Data { get; set; }
        public Filter Filter { get; set; }

    }
}
