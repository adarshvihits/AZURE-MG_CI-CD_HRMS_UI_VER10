using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.Master
{
   public class AssessmentYear :MasterBase
    {



        public int ActiveCount { get; set; }
        public int InactiveCount { get; set; }
        public int PendingCount { get; set; }
        public List<AssessmentYear> AssessmentYearList { get; set; }
     

        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public FinancialYear FinancialYear { get; set; }
    }
}
