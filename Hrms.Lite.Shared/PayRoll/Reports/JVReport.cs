using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.PayRoll.Reports
{
  public  class JVReport
    {
        public MasterBase PayrollMonth { get; set; }

        public int Designation { get; set; }

        public int Department { get; set; }

        public int Location { get; set; }
        public int PayrollGroup { get; set; }

        public int Division { get; set; }

        public int Region { get; set; }


        public int BasedOn { get; set; }
  

   
     
    }
}
