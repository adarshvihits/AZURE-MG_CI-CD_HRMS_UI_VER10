using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.Reports
{
    public class Reportees
    {
      
        public Filter Filter { get; set; }
        public int SlNo { get; set; }
        public Dropdown Authority { get; set; }
        public Dropdown ChangeAuthority { get; set; }
        public Dropdown RequestType { get; set; }
    
        public Dropdown RequestLevel { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        public string Location { get; set; }
        public DateTime DOJ { get; set; }
        public EmployeeHeader EmployeeHeader { get; set; }
        public string Type { get; set; }
        public string Level { get; set; }
        public string AuthorityName { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public List<Reportees> ReporteesList { get; set; }

    }
}
