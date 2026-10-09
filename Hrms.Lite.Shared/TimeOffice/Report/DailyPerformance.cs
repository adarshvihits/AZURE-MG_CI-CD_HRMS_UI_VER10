
using System;
using System.Collections.Generic;
using System.Text;
using Hrms.Lite.Shared.General;


namespace Hrms.Lite.Shared.TimeOffice.Report
{
    public class DailyPerformance
    {
        public DateTime Date { get; set; }
        public string EmployeeID { get; set; }
        public int EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public Guid EmployeeGI { get; set; }
        public UserBase User { get; set; }
        public string DesignationName { get; set; }
        public string DepartmentName { get; set; }
        public string LocationName { get; set; }
        public string Division { get; set; }
        public string RDDATE { get; set; }
        public string NoOfPunch { get; set; }
        public Dropdown NoOfPunchCode { get; set; }
        public string CountPunch { get; set; }
        public string PINTIME { get; set; }
        public string POUTTIME { get; set; }
        public string Firstin { get; set; }
        public string Lastout { get; set; }
        public int InCount { get; set; }
        public int OutCount { get; set; }
        public Dropdown Filter { get; set; }
        public List<DailyPerformance> DailyPerformanceList { get; set; }
        public List<DailyPerformance> DailyPerformancePopUpList { get; set; }
        public int SlNo { get; set; }
        public string PunchTime { get; set; }
        public string punchStatus { get; set; }
        public int Active { get; set; }
        public int TotalRecords { get; set; }
        public int PunchSlNo { get; set; }
        public string PunchDate { get; set; }
        public int ALORNOT { get; set; }
        public string PTOTALTIME { get; set; }
        public string TotInTime { get; set; }
        public string TotOUTTime { get; set; }
        public string FromTime { get; set; }
        public string ToTime { get; set; }
        public string Type { get; set; }
        public string Reader { get; set; }
        public string TotalTime { get; set; }

        public string IN { get; set; }
        public string OUT { get; set; }
        public string TotalPunchTime { get; set; }
        public int DisplayOrder { get; set; }
    }

}
