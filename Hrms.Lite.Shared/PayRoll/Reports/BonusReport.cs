using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Hrms.Lite.Shared.PayRoll.Reports
{
    public class BonusReport
    {
        public int ProcessMonth { get; set; }
        public int BonusGroup { get; set; }
        public int AmountType { get; set; }
        public DataSet ds { get; set; }
        public Filter Filter { get; set; }

    }
}
