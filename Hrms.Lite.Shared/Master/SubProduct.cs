using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.Master
{
   public  class SubProduct
    {
        public int ActiveCount { get; set; }
        public int InactiveCount { get; set; }
        public int PendingCount { get; set; }
        public List<SubProduct> SubProductList { get; set; }
        public int? Code { get; set; }
        public Guid? LogGI { get; set; }
        public Guid? GI { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string UserCode { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public string Mode { get; set; }
        public string ApproveMode { get; set; }
        public bool Applicable { get; set; }
        public bool MakerChecker { get; set; }
        public bool EditActive { get; set; }
        public bool IsMandatory { get; set; }
        public decimal Duration { get; set; }
    }
}
