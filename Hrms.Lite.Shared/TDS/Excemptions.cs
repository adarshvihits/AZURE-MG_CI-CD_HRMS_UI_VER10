using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.TDS
{
 public class Excemptions
    {
        public int ExCode { get; set; }
        public string Excemption { get; set; }
        public string ExcemptionType { get; set; }
        public string Section { get; set; }
        public string ComponentType { get; set; }
        public string ExAmount { get; set; }
        public Allowance ExcemptionAllowance { get; set; }
        public List<Excemptions> ExcemptionsList { get; set; }
    }
}
