using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.Master
{
    public class EventGroup
    {
        public Filter filter { get; set; }
        public int ActiveCount { get; set; }
        public int InactiveCount { get; set; }
        public int TabIndex { get; set; }
        public int EventGroupCode { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string EventName { get; set; }
        public Dropdown Event { get; set; }
        public string Remarks { get; set; }
        public int Years { get; set; }
        public int Months { get; set; }
        public bool IsYOEApplicable { get; set; }
        public bool Active { get; set; }
        public string Mode { get; set; }
        public int IsMultiCombination { get; set; }
        public Dropdown MultipleCombination1 { get; set; }
        public Dropdown MultipleCombination2 { get; set; }
        public List<Combinations> Combinations1 { get; set; }
        public List<Combinations> Combinations2 { get; set; }
        public List<EventGroup> EventGroupList { get; set; }
        public List<EmpList> EmpList { get; set; }
    }
    public class Combinations : MasterBase
    {
        public string EmployeeID { get; set; }
        public int TypeCode { get; set; }
        public int CombyType { get; set; } // 1-Single 2- multiple
        public int ApplCode1 { get; set; }

    }
    public class EmpList
    { 
        public int EmployeeCode { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public bool Applicable { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }

    }
}
