using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.TimeOffice.Report
{
    public class LeaveBalanceAsOnDate
    {
        public Filter filter { get; set; }

        public List<LeaveBalanceAsOnDate> LeaveBalanceAsOnDatelist { get; set; }

        public DateTime? Date { get; set; }

        public int totalRecords { get; set; }

        public int active { get; set; }

        public int EmployeeCode { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public Designation Designation { get; set; }
        public Department Department { get; set; }
        public Location Location { get; set; }
        public string LeaveGroup { get; set; }
        public decimal Opening { get; set; }
        public decimal Taken { get; set; }
        public decimal Balance { get; set; }

        public string Type { get; set; }

        public Leave Leave { get; set; }

    }
}
