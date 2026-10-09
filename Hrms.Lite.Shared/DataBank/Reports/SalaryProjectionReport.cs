using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.Reports
{
    public class SalaryProjectionReport
    {
      public Filter filter { get; set; }
      public int? AmountType { get; set; }
      public int? Type { get; set; }
      public Decimal Amount { get; set; }
      public int[] Allowance { get; set; }



        public List<SalaryProjectionReport> SalaryProjectionList { get; set; }

        public int[] Designation { get; set; }
        public int[] Department { get; set; }
        public int[] Location { get; set; }

        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }

        public int OldGP { get; set; }
        public int NewGP { get; set; }

        public DataSet ds { get; set; }
    }
}
