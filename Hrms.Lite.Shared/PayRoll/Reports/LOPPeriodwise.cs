using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.PayRoll.Reports
{
   public class LOPPeriodwise
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
        public int fMonth { get; set; }
        public int tMonth { get; set; }
        public string PrevLOP { get; set; }
        public string PrevArrear { get; set; }
        public string TotCutDays { get; set; }
        public string Latehrs { get; set; }
        public string TotCutAmt { get; set; }
        public DateTime? fromdate { get; set; }
        public DateTime? todate { get; set; }
        public Filter Filter { get; set; }
        public int active { get; set; }
        public int totalRecords { get; set; }
        public List<LOPPeriodwise> LOPPeriodwiseList { get; set; }
    }
}
