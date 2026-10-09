using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.PayRoll
{
  public  class SalaryReleaseInitialize
    {
        public int Month { get; set; }
        public PayrollGroup PayrollGroup { get; set; }
        public bool Applicability { get; set; }
        public List<SalaryReleaseInitialize> SalaryReleaseInitializeList { get; set; }
    }
}
