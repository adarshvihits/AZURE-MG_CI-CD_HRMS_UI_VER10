using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.TDS
{
    public  class EmployeeVsExcemption
    {
        public Filter Filter { get; set; }
        public MasterBase AssessmentYear { get; set; }
        public MasterBase PayrollMonth { get; set; }
        public Dropdown Excemption { get; set; }
        public List<EmployeeVsExecempList> EmployeeVsExcemptionList { get; set; }
    }
    public class EmployeeVsExecempList

    {
        public EmployeeHeader Employee { get; set; }
        public string LocationName { get; set; }
        public string DepartmentName { get; set; }
        public string DesignationName { get; set; }
        public decimal ExcemptionAmount { get; set; }
        public int Count { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
    }
  
}
