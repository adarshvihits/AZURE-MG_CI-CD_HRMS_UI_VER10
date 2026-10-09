using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank
{
   public class EmpVsAppliAllowanceOrDeduction
    {
        public Dropdown Type { get; set; }
        public Filter Filter { get; set; }
        public int radioSelection { get; set; }
        public File Excel { get; set; }
        public Dropdown AllowanceOrDeduction { get; set; }
        public List<ApplAllowOrDedList> ApplAllowOrDedList { get; set; }
        public string Message { get; set; }
    }
    public class ApplAllowOrDedList
        {
        public int EmployeeCode { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public Guid EmployeeGI { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public DateTime DOJ { get; set; }
        public bool Applicable { get; set; }
        public string Applicability { get; set; }
        public string Remark { get; set; }
    }
}
