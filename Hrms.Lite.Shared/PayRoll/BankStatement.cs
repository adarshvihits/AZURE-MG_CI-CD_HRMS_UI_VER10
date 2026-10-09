using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.PayRoll
{
    public class BankStatement
    {

        public MasterBase PayrollMonth { get; set; }

        public int Designation { get; set; }

        public int Department { get; set; }

        public int Location { get; set; }
        public int PayrollGroup { get; set; }

        public int Division { get; set; }

        public int Section { get; set; }

        public int BasedOn { get; set; }
        public Bank Bank { get; set; }
        public int?[] Banks { get; set; }
        public int ReportFormat { get; set; }
        public DateTime ExecutionDate { get; set; }

    }
 
}