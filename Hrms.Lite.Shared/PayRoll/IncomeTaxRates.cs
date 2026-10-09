using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.PayRoll
{
    public class IncomeTaxRates
    {        
        public MasterBase AssessmentYear { get; set; }
        public MasterBase ITCategory { get; set; }
        public int BasedOn { get; set; }
        public List<IncomeTaxRates_Details> Details { get; set; }
    }
    public class IncomeTaxRates_Details
    {
        public int RowNumber { get; set; }
        public int TrnCode { get; set; }
        public int? SalaryFrom { get; set; }
        public int? SalaryTo { get; set; }
        public decimal? Rate { get; set; }
      
    }
}
