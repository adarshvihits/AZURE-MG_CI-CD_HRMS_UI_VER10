using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Hrms.Lite.Shared.PayRoll.Reports
{
    public class PeriodWiseDeductionReport
    {
        public int From { get; set; }
        public int To { get; set; }
        public int DeductionCode { get; set; }
        public DataSet ds { get; set; }
        public Filter Filter { get; set; }
    }
}
