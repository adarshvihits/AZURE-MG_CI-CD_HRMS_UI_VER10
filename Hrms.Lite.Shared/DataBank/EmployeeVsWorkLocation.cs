using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank
{
    public class EmployeeVsWorkLocation
    {
        public Filter Filter { get; set; }
        public int WorkLocationCode { get; set; }
        public Guid EmployeeGI { get; set; }
        public int EmpCode { get; set; }
        public List<EmployeeVsWorkLocationList> EmployeeVsWorkLocationList { get; set; }
    }

    public class EmployeeVsWorkLocationList
    {
        public Guid EmployeeGuid { get; set; }
        public int EmployeeCode { get; set; }
        public string EmployeeID { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public Dropdown WorkLocation { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }

    }
}
