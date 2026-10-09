using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.Master
{
   public class ProductCompany : MasterBase
    {


       
        public int ActiveCount { get; set; }
        public int InactiveCount { get; set; }
        public int PendingCount { get; set; }
        public List<ProductCompany> ProductCompanyList { get; set; }
        public ProductRequest ProductRequest { get; set; }
        public File File { get; set; }
        public string AbsoluteUri { get; set; }
    }
    public class ProductRequest
    {
        public Guid GI { get; set; }
        public Guid? LogGI { get; set; }
        public int ProductCode { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Remarks { get; set; }
        public bool Active { get; set; }
        public File File { get; set; }
        public string Mode { get; set; }
        public string AbsoluteUri { get; set; }
    }
}
