using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.Master
{
    public class EmployeeVsMobileRestriction
    {

        public int AllCount { get; set; }
        public int RestrictedEmpCount { get; set; }
        public int EmployeeCode { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public DateTime? DOJ { get; set; }
        public bool Applicable { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public List<EmployeeVsMobileRestriction> RestrictedEmpList { get; set; }
        public Filter filter { get; set; }
        public int Tabindex { get; set; }
    }
}
