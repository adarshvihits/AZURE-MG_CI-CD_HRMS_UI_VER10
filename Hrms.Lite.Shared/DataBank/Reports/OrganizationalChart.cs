using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.Reports
{
    public class OrganizationalChart
    {

        public List<OrganizationalChart_List> OrganizationalChartList { get; set; }

        public List<OrganizationalChart_EmployeeList> EmployeeList { get; set; }

       

    }

    public class OrganizationalChart_EmployeeList
    {
        public int employeeCode { get; set; }

        public string employeeId { get; set; }

        public string employeeName { get; set; }

    }

    public class OrganizationalChart_List
    {
        public int ID
        {
            get; set;
        }
        public int? ParentID
        {
            get; set;
        }
        public string Name
        {
            get; set;
        }
        public string Title
        {
            get; set;
        }
        public string Group
        {
            get; set;
        }
        public bool? Expanded
        {
            get; set;
        }
        public bool hasChildren
        {
            get; set;
        }
        public string Avatar
        {
            get; set;
        }

        public string ProfileFilename { get; set; }
        public string Base64string { get; set; }
    }
}
