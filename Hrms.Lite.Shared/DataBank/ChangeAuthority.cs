using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank
{
    public class ChangeAuthority
    {
        public Dropdown Workflow { get; set; }
        public int WorkflowCode { get; set; }
        public int EmployeeCode { get; set; }
        public Guid EmployeeGI { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public string FuncDesignation { get; set; }
        public string Status { get; set; }
        public List<ChangeAuthority> ChangeAuthorityList { get; set; }
        public int TrnCode { get; set; }
        public Filter filter { get; set; }
        public EmployeeHeader EmployeeHeader { get; set; }
        public List<LevelAuthorityList> LevelAuthorityList { get; set; }
    }
    public class LevelAuthorityList
    {
        public Dropdown Authority { get; set; }
        public int LevelCode { get; set; }
        public string Level { get; set; }
        public int TrnCode { get; set; }
    }
}
