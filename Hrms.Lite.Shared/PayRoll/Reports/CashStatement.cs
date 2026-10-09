using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.PayRoll.Reports
{
    public class CashStatement
    {        
        public MasterBase PayrollMonth { get; set; }
        public List<CashStatement_Details> Details { get; set; }
        public FilterMultiSelect filter { get; set; }
        public int Total_NetAmount { get; set; }
    }
    public class CashStatement_Details
    {
        public Guid EmployeeGI { get; set; }
        public int EmployeeCode { get; set; }
        public int SlNo { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public string Designation { get; set; }
        public string DOJ { get; set; }
        public int NetAmount { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }

    }
}
