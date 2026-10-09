using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.Admin
{
    public class DocumentCategory : MasterBase
    {
        public List<DocumentCategory> DocumentCategoryList { get; set; }
        public List<ParameterList> ParameterList { get; set; }
        public bool RenewalDateApplicable { get; set; }
    }

    public class ParameterList
    {
        public int ParameterCode { get; set; }
        public string ParameterName { get; set; }
        public bool Applicable { get; set; }
        
    }
}
