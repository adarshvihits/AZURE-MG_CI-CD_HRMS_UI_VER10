using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.PayRoll.Reports
{
   public class LOPReport
    {
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Location { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        public string LOPAbsent { get; set; }
        public string ESILeave { get; set; }
        public string LJP { get; set; }
        public string LTP { get; set; }
        public int Month { get; set; }
        public string PrevLOP { get; set; }
        public string TotCutDays { get; set; }
        public string Suspension { get; set; }
        public string Layoff { get; set; }
        public string Latehrs { get; set; }
        public string TotCutAmt { get; set; }
        public int Type { get; set; }
        public string LOPAbsentAmount { get; set; }
        public string ESILeaveAmount { get; set; }
        public string LJPAmount { get; set; }
        public string LTPAmount { get; set; }
        public string PreviosLOPAmount { get; set; }
        public string TotDaysToCutAmount { get; set; }
        public string SuspensionAmount { get; set; }
        public string LayOffAmount { get; set; }
        public string LessHoursAmount { get; set; }

        public Filter Filter { get; set; }
        public int active { get; set; }
        public int totalRecords { get; set; }
        public List<LOPReport> LOPReportList { get; set; }

    }
}
