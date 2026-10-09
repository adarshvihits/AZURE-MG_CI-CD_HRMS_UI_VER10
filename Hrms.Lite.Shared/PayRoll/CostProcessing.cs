using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.PayRoll
{
  public  class CostProcessing
    {
        public Dropdown Month { get; set; }
        public Dropdown PayrollGroup { get; set; }
        public int PendingCount { get; set; }
        public List<EmployeeList> EmployeePendingList { get; set; }
    }

    public class EmployeeList
    {
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public DateTime DOJ { get; set; }
    }

}


