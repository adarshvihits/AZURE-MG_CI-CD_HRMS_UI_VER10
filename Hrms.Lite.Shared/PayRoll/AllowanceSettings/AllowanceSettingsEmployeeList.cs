using Hrms.Lite.Shared.DataBank.Generic;

using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice;

namespace Hrms.Lite.Shared.PayRoll.AllowanceSettings
{
    public class AllowanceSettingsEmployeeList
    {
        public EmployeePrimaryInfo Employee { get; set; }
        public string LocationName { get; set; }
        public string DepartmentName { get; set; }
        public string DesignationName { get; set; }
        public decimal Rate { get; set; }
        public Dropdown CalcType { get; set; }
        public int Count { get; set; }
        public List<AllowanceSettingsEmployeeList> SettingsEmployeeList { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public string FileName { get; set; }
    }
}
