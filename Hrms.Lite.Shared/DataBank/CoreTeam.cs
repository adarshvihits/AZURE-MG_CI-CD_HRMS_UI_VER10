using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank
{
    public class CoreTeam
    {
        public int AllCount { get; set; }
        public int CoreTeamCount { get; set; }
        public int EmployeeCode { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public string Category { get; set; }
        public DateTime? DOJ { get; set; }
        public string YOS { get; set; }
        public bool Applicable { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public List<CoreTeam> TeamList { get; set; }
        public Filter filter { get; set; }
        public int Tabindex { get; set; }

    }

}
