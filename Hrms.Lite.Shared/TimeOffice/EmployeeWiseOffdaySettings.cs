using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.TimeOffice
{
    public class EmployeeWiseOffdaySettings
    {
        public Guid EmployeeGI { get; set; }
        public EmployeeHeader EmployeeHeader { get; set; }
        public Filter filter { get; set; }
        public CalendarYear Year { get; set; }
        public ShiftType ShiftType { get; set; }
        public OffdaySettingsName PreviousCombination { get; set; }
        public OffdaySettingsName NewCombination { get; set; }
        public DateTime? EffectiveFrom { get; set; }
        public string EmployeeName { get; set; }
        public string Description { get; set; }
        public List<EmployeeWiseOffdaySettingsList> List { get; set; }
        public List<EmployeeWiseOffdaySettings_Dates> Dates { get; set; }
    }
    public class EmployeeWiseOffdaySettingsList
    {
        public Guid EmployeeGI { get; set; }
        public string EmployeeID { get; set; }
        public string FullName { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        public string Location { get; set; }
        public string PreviousCombination { get; set; }
        public string NewCombination { get; set; }
        public string EffectiveFrom { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public int order { get; set; }

    }
    public class EmployeeWiseOffdaySettings_Dates
    {
        public string Date { get; set; }
        public string Day { get; set; }

    }
    public class OffdaySettingsName : MasterBase
    { }
}

