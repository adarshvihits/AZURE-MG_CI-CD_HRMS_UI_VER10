using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.Reports
{
    public class EmployeeTaskReport
    {
        public Dropdown Status { get; set; }
        public int EmployeeCode { get; set; }
        public string EmployeeID { get; set; }
        public string AssignedToEmployee { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Task { get; set; }
        public string GivenBy { get; set; }
        public string TaskStatus { get; set; }
        public string ExpectedCompletetionDate { get; set; }
        public int ActionCount { get; set; }
        public int TaskCode { get; set; }
        public int ActionCode { get; set; }
        public string EntryDate { get; set; }
        public string Description { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public Filter Filter { get; set; }

        public List<EmployeeTaskReport> EmployeeTaskReportList { get; set; }
    }
}
