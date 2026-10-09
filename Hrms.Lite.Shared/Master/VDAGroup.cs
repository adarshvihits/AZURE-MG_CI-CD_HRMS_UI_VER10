using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.Master
{
    public class VDAGroup: MasterBase
    {
        public Guid EmployeeGI { get; set; }
        public string EmployeeID { get; set; }
        public int ActiveCount { get; set; }
        public int InactiveCount { get; set; }
        public int PendingCount { get; set; }
        public int VDAGroupCode { get; set; }
        public List<VDAGroup> VDAGroupList { get; set; }
        public List<EmployeeVsVDAGroup> EmployeeVsVDAGroupList { get; set; }
        public Filter Filter { get; set; }
        public File Excel { get; set; }
        public string TabName { get; set; }
        public string AssignOrUnassign { get; set; }
        public string VDAGroupName { get; set; }
        public string Message { get; set; }
        public string Remarks { get; set; }
        public SalaryPeriod Month { get; set; }
        public List<MonthlyVDASettings> MonthlySetting { get; set; }
    }
    public class EmployeeVsVDAGroup
    {
        public string TabName { get; set; }
        public Guid EmployeeGuid { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public int VDAGroupCode { get; set; }
        public string VDAGroupName { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
    }
    public class MonthlyVDASettings
    {
        public int VDAGroupCode { get; set; }
        public string VDAGroupName { get; set; }
        public decimal VDAPoint { get; set; }
        public decimal StandardDeduction { get; set; }
        public decimal DAIndex { get; set; }
        public decimal VDAAmount { get; set; }
      
    }
}
