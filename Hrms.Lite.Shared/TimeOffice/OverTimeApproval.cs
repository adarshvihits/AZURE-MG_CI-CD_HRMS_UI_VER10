using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.TimeOffice
{
    public class OverTimeApproval
    {        
        public DateTime Date { get; set; }
        public List<OverTimeApproval_Details> Details { get; set; }        
        public Filter Filter { get; set; }
    }
    public class OverTimeApproval_Details
    {
        public Guid EmployeeGI { get; set; }
        public int EmployeeCode { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public string Shift { get; set; }
        public int OTHours { get; set; }
        public int OTMin { get; set; }
        public string Remarks { get; set; }
        public bool YN { get; set; }
        public string PunchIn { get; set; }
        public string PunchOutTime { get; set; }
        public string TotalPunchTime { get; set; }
        public int ActualOTHours { get; set; }
        public int ActualOTMin { get; set; }
        public string InTime { get; set; }
        public string OutTime { get; set; }
        public DateTime OtDate { get; set; }
        public string EditOTHours { get; set; }
        public string EditOTMin { get; set; }

        public int totalRecords { get; set; }
        public int active { get; set; }

    }
}
