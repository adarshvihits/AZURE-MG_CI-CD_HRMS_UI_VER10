using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.Master
{
   public class EmployeeDocumentType:MasterBase
    {
        public int ActiveCount { get; set; }
        public int InactiveCount { get; set; }
        public int PendingCount { get; set; }
        public MasterBase DocumentType { get; set; }
        public List<EmployeeDocumentType> EmployeeDocumentTypeList { get; set; }
    }
}
