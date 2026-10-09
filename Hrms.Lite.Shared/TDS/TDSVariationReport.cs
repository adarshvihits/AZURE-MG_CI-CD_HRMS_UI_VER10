using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.TDS
{
  public  class TDSVariationReport
    {
      
        public Filter Filter { get; set; }
        public int AssessmentYear { get; set; }
        public int PayrollGroup { get; set; }
        public int PayrollMonth { get; set; }
        public int Location { get; set; }
        public int Department { get; set; }
        public int Designation { get; set; }
        public int Category { get; set; }
        public int Grade { get; set; }
        public Url Url { get; set; }
        public string ReportUrl { get; set; }
        public Guid CompanyGI { get; set; }
        public Guid UserGI { get; set; }
        public Guid GroupCompanyGI { get; set; }
        public int GroupCompanyCode { get; set; }



    }

}
