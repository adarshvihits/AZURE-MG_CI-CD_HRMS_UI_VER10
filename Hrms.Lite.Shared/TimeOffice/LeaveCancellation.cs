using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using Hrms.Lite.Shared.TimeOffice.Generic;
using Hrms.Lite.Shared.TimeOffice.Generic.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.TimeOffice
{
    public class LeaveCancellation
    {             
        public int EmployeeCode { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public Guid EmployeeGI { get; set; }
        public Designation Designation { get; set; }
        public Department Department { get; set; }
        public Location Location { get; set; }
        public DateTime LeaveDate { get; set; }
        public Leave Leave { get; set; }
        public int LeaveTypeCode { get; set; }
        public AttnDayStatus DayStatus { get; set; }
        public Month Month { get; set; }
        public PayrollGroup Payroll { get; set; }
        public string Remarks { get; set; }
        public Filter Filter { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public string EntryUser { get; set; }
        public List<LeaveCancellation> LeaveCancellationList { get; set; }
    }
}
