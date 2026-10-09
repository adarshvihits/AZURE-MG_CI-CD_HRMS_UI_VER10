using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.Generic.Master
{
    public class DepartmentCostCenter : MasterBase
    {
        public int ActiveCount { get; set; }
        public int InactiveCount { get; set; }
        public int PendingCount { get; set; }
        public string DepartmentCostCenterGI { get; set; }
        public List<DepartmentCostCenter> DepartmentCostCenterList { get; set; }

    }
}
