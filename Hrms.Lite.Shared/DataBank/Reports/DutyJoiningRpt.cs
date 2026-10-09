using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.Reports
{
   public class DutyJoiningRpt
    {
        public Filter Filter { get; set; }
        public int EmployeeCode { get; set; }
        public string ID { get; set; }
        public string EmployeeName { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }


        public List<DutyJoiningRpt> DutyJoiningRptList { get; set; }
    }
}
