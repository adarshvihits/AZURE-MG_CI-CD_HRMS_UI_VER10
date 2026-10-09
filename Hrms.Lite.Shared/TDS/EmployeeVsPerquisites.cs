using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.TDS
{
  
    public class EmployeeVsPerquisites
    {
        public Filter Filter { get; set; }
        public Dropdown AssessmentYear { get; set; }
        public Dropdown PayrollMonth { get; set; }
        public Dropdown Perquisite { get; set; }
        public  File Excel { get; set; }
        public List<EmployeeVsPerqList> EmployeeVsPerquisitesList { get; set; }

    }
    public class EmployeeVsPerqList

    {
        public EmployeeHeader Employee { get; set; }
        public string LocationName { get; set; }
        public string DepartmentName { get; set; }
        public string DesignationName { get; set; }
        public decimal PerqAmount { get; set; }
        public string PerqAmountheading { get; set; }
        public string TDSAmountheading { get; set; }
        public decimal TDSAmount { get; set; }
        public int Count { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public string Remarks { get; set; }
    }
    public class EmployeeVsPerquisitesUpdate
    {
        public Filter filter { get; set; }
        public MasterBase AssessmentYear { get; set; }
        public Dropdown RegimeType { get; set; }

    }
}
