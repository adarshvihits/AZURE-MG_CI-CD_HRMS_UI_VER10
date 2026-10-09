using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.TimeOffice.Report
{
   public class MaternityEsiLeaveReport
    {
        public MasterBase MaternityEsiLeaves { get; set; }
        public String EmployeeID { get; set; }
        public String EmployeeName { get; set; }
        public String Department { get; set; }
        public String Designation { get; set; }
        public String Location { get; set; }
        public int DaysLeft { get; set; }
       
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int active { get; set; }
        public int totalRecords { get; set; }
        public Filter Filter { get; set; }
        public List<MaternityEsiLeaveReport> MaternityEsiLeaveReportList { get; set; }
    }

}
