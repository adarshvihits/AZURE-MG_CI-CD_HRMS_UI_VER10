using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
namespace Hrms.Lite.Shared.PayRoll
{
    public class MonthlyEmployeeVsCostCenter
    {
        public EmployeeHeader EmployeeHeader { get; set; }
        //public int MonthCode { get; set; }
        //public string MonthName { get; set; }
        public Dropdown Month { get; set; }
        public Dropdown MonthTo { get; set; }
        public int EmployeeCode { get; set; }
        public string Mode { get; set; }
        public DataSet ds { get; set; }
        public Filter Filter { get; set; }
        public List<CostCenters> CostCenterList { get; set; }
    }

public class CostCenters
{
    public int CostCenterCode { get; set; }
    public Guid CostCenterGI { get; set; }
    public string CostCenter { get; set; }
    public decimal Percentage { get; set; }
    public bool Applicability { get; set; }
}

}
