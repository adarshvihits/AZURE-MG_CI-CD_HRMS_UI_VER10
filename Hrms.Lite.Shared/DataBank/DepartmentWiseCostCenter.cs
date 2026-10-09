using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank
{
   public class DepartmentWiseCostCenter
    {
        public int SlNo { get; set; }
        public string DepartmentName { get; set; }
        public int DepartmentCode { get; set; }
        public string CostCenter { get; set; }
        public int CostCenterCode { get; set; }
        public int GroupCompanyCode { get; set; }
        public Dropdown CostDropdown { get; set; }
        public Dropdown GroupCompany { get; set; }
        public List<DepartmentWiseCostCenter> DepartmentWiseCostCenterList { get; set; }
    }
}
