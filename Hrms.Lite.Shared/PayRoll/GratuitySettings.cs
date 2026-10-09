using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.PayRoll
{
    public class GratuitySettings
    {
        public int GratuityEligibleService { get; set; }
        public int NoOfWorkingDays { get; set; }
        public int GratuityDays { get; set; }
        public int MinimumAssured { get; set; }
        public int MaximumSealingAmount { get; set; }
        public int EmployeeStatus { get; set; }
        public List<GratuitySettingsList> GratuitySettingsList { get; set; }
        public List<GratuityProjection> GratuityProjection { get; set; }
        public Filter Filter { get; set; }
    }
    public class GratuitySettingsList : MasterBase
    {
        public int Percentage { get; set; }
    }
    public class GratuityProjection
    {
        public int EmployeeCode { get; set; }
        public Guid EmployeeGI { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public DateTime DOJ { get; set; }
        public DateTime? LWD { get; set; }
        public DateTime? DOC { get; set; }
        public string YOS { get; set; }
        public int LDS { get; set; }
        public int GratuityApllicableSalary { get; set; }
        public int EligibleGratuityAmount { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
    }
}
