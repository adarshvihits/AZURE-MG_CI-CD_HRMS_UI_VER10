using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.Generic
{
    public class SalaryIncrementTool
    {
        public Dropdown TransitionType { get; set; }
        public Dropdown Reason { get; set; }
        public List<FixedList> FixedList { get; set; }
        public List<SettingsList> ClassificationList { get; set; }
        public List<SettingsList> AllowanceList { get; set; }
        public List<SettingsList> SettingsList { get; set; }
        public File Excel { get; set; }
        public string Message { get; set; }
        public List<SalaryIncrementTool> SalaryIncrementToolList { get; set; }
        public DataTable ExcelHead { get; set; }
        public DataTable ExcelTable { get; set; }
        public DataTable ExcelRows { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Remark { get; set; }
        public List<AllowanceResult> AllowanceResultList { get; set; }
        public List<SettingsTypeList> ClassificationResultList { get; set; }
        public List<EmployeeDetails> EmployeeDetailsList { get; set; }
    }

    public class FixedList
    {
        public string Heading { get; set; }
        public int Code { get; set; }
        public bool Applicable { get; set; }

    }
    public class ClassificationList
    {
        public int AllowanceCode { get; set; }
        public int ClassificationCode { get; set; }
        public bool Applicable { get; set; }
        public int Index { get; set; }
        public string Columns { get; set; }
        public string Heading { get; set; }


    }
    public class AllowanceList
    {
        public string Heading { get; set; }
        public int AllowanceCode { get; set; }
        public bool Applicable { get; set; }

    }
    public class SettingsList
    {

        public int Order { get; set; }
        public int AllowanceCode { get; set; }
        public int ClassificationCode { get; set; }
        public bool Applicable { get; set; }
        public int Index { get; set; }
        public string Columns { get; set; }
        public string Heading { get; set; }
        public int Type { get; set; }
    }

    public class AllowanceResult
    {
        public string ID { get; set; }
        public decimal Amount { get; set; }
        public int AllowanceCode { get; set; }
        public string AllowanceName { get; set; }

    }
    public class SettingsTypeList
    {
        public string EmployeeID { get; set; }
        public string ClassificationType { get; set; }
        public string ClassificationValue { get; set; }
    }
    public class EmployeeDetails
    {
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public DateTime EFFECTIVEDATE { get; set; }
        public DateTime ORDERDATE { get; set; }
    }
}
