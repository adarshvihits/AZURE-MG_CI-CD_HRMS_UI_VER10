using System;
using System.Collections.Generic;
using System.Text;
using Hrms.Lite.Shared.General;

namespace Hrms.Lite.Shared.Master
{
    public class FinancialYear : MasterBase
    {

        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int Code { get; set; }
        public int ActiveCount { get; set; }
        //public int InactiveCount { get; set; }
        public int PendingCount { get; set; }
        public string EntryType { get; set; }
        public SalaryPeriod salaryPeriod { get; set; }
    
        public List<FinancialYear> FinancialYearList { get; set; }
    }
}
