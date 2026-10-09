using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.PayRoll.Reports
{
  public  class ESIGroupSummaryReport
    {
        public int Month { get; set; }
        public int ESIGroup { get; set; }
        public List<ESIGroupSummary> ESIGroupSummary { get; set; }
        public Filter SliderFilter { get; set; }
    }
    public class ESIGroupSummary
    {
        public int SlNo { get; set; }
        public string Name { get; set; }
        public int EmpCount { get; set; }
        public decimal ESISalary { get; set; }
        public decimal ESI { get; set; }
        public decimal ESIERCONT { get; set; }
        public decimal Total { get; set; }

        public int totalRecords { get; set; }
        public int active { get; set; }
    }
}
