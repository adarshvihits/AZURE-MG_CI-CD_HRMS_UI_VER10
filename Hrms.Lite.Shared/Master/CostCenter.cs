using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.Master
{
    public class CostCenter : MasterBase
    {
        public Guid EmployeeGI { get; set; }
        public string EmployeeID { get; set; }
        public int ActiveCount { get; set; }
        public int InactiveCount { get; set; }
        public int PendingCount { get; set; }
        public int CostCenterCode { get; set; }
        public List<CostCenter> CostCenterList { get; set; }
        public List<CostCenterTablist> CostCenterTablist { get; set; }
        public string AssignOrUnassign { get; set; }
        public string CostCenterUserCode { get; set; }
        public EmployeeHeader EmployeeHeader { get; set; }
        public Filter Filter { get; set; }
        public File Excel { get; set; }
        public string Message { get; set; }
        public string Remarks { get; set; }
        public string CostCenterName { get; set; }

    }
    public class CostCenterTablist
    {
        public string TabName { get; set; }
        public Guid EmployeeGuid { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public int CostCenter { get; set; }
        public string CostCenterUserCode { get; set; }
        public string CostCenterName { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public string action { get; set; }

    }
}
