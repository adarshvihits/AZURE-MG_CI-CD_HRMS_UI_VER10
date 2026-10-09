using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using Hrms.Lite.Shared.TimeOffice;
using System;
using System.Collections.Generic;
using System.Text;


namespace Hrms.Lite.Shared.TDS
{
  public  class Regime
    {
        public Filter Filter { get; set; }
        public MasterBase AssessmentYear { get; set; }
        public List<RegimeSelectionEmployeeList> RegimeEmployeeList { get; set; }
       
    }
    public class RegimeSelectionEmployeeList

    {
        public EmployeeHeader Employee { get; set; }
        public string LocationName { get; set; }
        public string DepartmentName { get; set; }
        public string DesignationName { get; set; }
        public Dropdown RegimeType { get; set; }
        public int Count { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
    }
    public class RegimeBatchUpdate
    {
        public Filter filter { get; set; }
        public MasterBase AssessmentYear { get; set; }
        public Dropdown RegimeType { get; set; }

    }

}
