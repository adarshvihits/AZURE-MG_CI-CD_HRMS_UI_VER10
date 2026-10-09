using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank
{
    public class DepartmentWiseProfitCenter
    {
        public int SlNo { get; set; }
        public string DepartmentName { get; set; }
        public int DepartmentCode { get; set; }
        public string ProfitCenter { get; set; }
        public int ProfitCenterCode { get; set; }
        public int GroupCompanyCode { get; set; }
        public Dropdown ProfitDropdown { get; set; }
        public Dropdown GroupCompany { get; set; }
        public decimal Percentage { get; set; }
        public bool Applicability { get; set; }
        public List<DepartmentWiseProfitCenter> DepartmentWiseProfitCenterList { get; set; }
    }
}
