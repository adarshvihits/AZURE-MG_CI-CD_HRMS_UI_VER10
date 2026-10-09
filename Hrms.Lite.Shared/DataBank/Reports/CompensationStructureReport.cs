using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.Reports
{
    public class CompensationStructureReport
    {
     
        public List<CompensationStructure_List> CompensationStructure_List { get; set; }
        public List<CompensationStructure_Details> CompensationStructure_Details { get; set; }
    }
    public class CompensationStructure_List
    {
        public Guid EmployeeGI { get; set; }
        public string Employeeid { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public string Category { get; set; }
        public string YOS { get; set; }
        public Decimal GrossPay { get; set; }
    }
    public class CompensationStructure_Details
    {
        public string Description { get; set; }
        public decimal MonthlyAmount { get; set; }
        public decimal AnualAmount { get; set; }
        public string Type { get; set; }
    }
}
