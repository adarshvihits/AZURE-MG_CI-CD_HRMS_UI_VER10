using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.DTO
{
    public class SuccessionPlanning
    {

        public int AllCount { get; set; }
        public int PendingCount { get; set; }
        public Guid EmployeeGI { get; set; }
        public Guid KeyPositionGI { get; set; }
        public string KeyPositionDesc { get; set; }
        public Guid SuccessionPlanningGI { get; set; }
        public string KeyPosition { get; set; }
        public string Gender { get; set; }
        public string YOS { get; set; }
        public string Age { get; set; }
        public string Qualification { get; set; }
        public string Skill { get; set; }
        public string Language { get; set; }
        public int Matching { get; set; }
        public int EmployeesType { get; set; }//Radio button --1) Matching Employees 2)All Active Employees
        public Filter Filter { get; set; }
        public string TabIndex { get; set; }
        public List<SuccessorsList> SuccessorsList { get; set; }
        public List<SuccessionPlanningList> SuccessionPlanningList { get; set; }
        public List<SuccessorEmployeeList> SuccessorEmployeeList { get; set; }
        public EmployeeHeader EmployeeHeader { get; set; }
    }
    public class SuccessionPlanningList
    {
        public int SlNo { get; set; }
        public string KeyPositionDesc { get; set; }
        public string KeyPosition { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeID { get; set; }
        public Guid EmployeeGI { get; set; }
        public Guid KeyPositionGI { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public int Successor { get; set; }
        public string SuccessorEmployeeName { get; set; }
        public string SuccessorEmployeeID { get; set; }

    }
    public class SuccessorEmployeeList
    {
        public Guid EmployeeGI { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public int Matching { get; set; }
        public bool Applicable { get; set; }
        public int KeyPositionCode { get; set; }
        public int EmployeeCode { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
    }
    public class SuccessorsList
    {
        public Guid EmployeeGI { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public bool Gender { get; set; }
        public Guid SuccessionPlanningGI { get; set; }
        public bool YOS { get; set; }
        public bool Age { get; set; }
        public bool Qualification { get; set; }
        public bool Skill { get; set; }
        public bool Language { get; set; }
        public int Matching { get; set; }
    }
}

