using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.Reports
{
   public class EmployeeMood
    {
        public Guid EmployeeGI { get; set; }

        public int MoodCode { get; set; }
        public DateTime? Date { get; set; }
        public Filter filter { get; set; }
        

        public int EmployeeCode { get; set; }

        public string EmployeeID { get; set; }

        public string Employee { get; set; }

        public string Designation { get; set; }

        public string Department { get; set; }

        public string Location { get; set; }

        public string Mood { get; set; }

        public int totalRecords { get; set; }

        public int active { get; set; }

        public List<EmployeeMood> EmployeeMoodDetails { get; set; }

    }
}
