using System;
using System.Collections.Generic;
using System.Text;
using Hrms.Lite.Shared.General;

namespace Hrms.Lite.Shared.TDS
{
   public class Form16
    {
     
        public MasterBase AssessmentYear { get; set; }

        public List<FORM16_EmployeeList> EmployeeList { get; set; }
        public Url Url { get; set; }
        public int CompanyCode { get; set; }
        public Filter filter { get; set; }
        public int AssessmentYearCode { get; set; }

    }
    public class FORM16_EmployeeList
    {

        public int EmployeeCode { get; set; }
        public Guid EmployeeGI { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public int ACTIVE { get; set; }
        public int TotalCount { get; set; }

    }
}
