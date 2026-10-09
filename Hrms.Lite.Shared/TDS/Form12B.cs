using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.TDS
{
 public   class Form12B
    {
        public MasterBase AssessmentYear { get; set; }
    
        public List<FORMB_EmployeeList> EmployeeList { get; set; }
        public Url Url { get; set; }
        public int CompanyCode { get; set; }
     
        public int? CurrentSalaryPeriodCode { get; set; }
        public Filter filter { get; set; }
    }
    public class FORMB_EmployeeList
    {

        public int EmployeeCode { get; set; }
        public Guid EmployeeGI { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public int ACTIVE { get; set; }
        public int TotalCount { get; set; }
    }
}
