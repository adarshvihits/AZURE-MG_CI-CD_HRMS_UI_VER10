using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using Hrms.Lite.Shared.TimeOffice.Generic.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.TimeOffice
{
   public class CoffCredit
    {


        public Month Month { get; set; }
        public int PendingCount { get; set; }
        public int ConfirmedCount { get; set; }
        public DateAndMonth DateAndMonth { get; set; }
        public string TabValues { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public string Mode { get; set; }
        public CoffCreditDetails Details { get; set; }
        public List<CoffCreditDetails> ConfirmationList { get; set; }
    }
    public class CoffCreditDetails
    {
        public int EmployeeCode { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public Guid EmployeeGI { get; set; }
        public Designation Designation { get; set; }
        public Department Department { get; set; }
        public Location Location { get; set; }
        public DateTime? DateWorked { get; set; }
        public string IN { get; set; }
        public string OUT { get; set; }
        public string TotalTime { get; set; }
        public decimal Opening { get; set; }
        public string AttendanceStatus { get; set; }
        public bool ConfirmationStatus { get; set; }
        public string Status { get; set; }
        public Filter Filter { get; set; }
    }
}

