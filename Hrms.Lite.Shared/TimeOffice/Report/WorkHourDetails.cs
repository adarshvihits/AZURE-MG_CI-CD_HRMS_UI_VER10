using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.TimeOffice.Report
{
    public class WorkHourDetails
    {
        public Filter Filter { get; set; }
        public Dropdown Month { get; set; }
       
            public int EmployeeCode { get; set; }
            public string EmployeeID { get; set; }
            public Guid EmployeeGI { get; set; }
            public string EmployeeName { get; set; }
            public string Designation { get; set; }
            public string Department { get; set; }
            public string Location { get; set; }
            public decimal? ScheduledNoDuty { get; set; }
            public string ScheduledHours { get; set; }
            public decimal? ActualNoDuty { get; set; }
            public string ActualHours { get; set; }
            public string ExtraHours { get; set; }
            public string LessHours { get; set; }
            public string ConfirmedOTHours { get; set; }
            public string ConfirmedLessHour { get; set; }
            public string STATUS { get; set; }
            public int active { get; set; }
            public int totalRecords { get; set; }
            public List<WorkHourDetails> WorkingHoursList { get; set; }
    

    }
}
