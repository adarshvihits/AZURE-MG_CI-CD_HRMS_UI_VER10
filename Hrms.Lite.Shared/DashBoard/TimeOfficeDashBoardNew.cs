using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DashBoard
{
    public class TimeOfficeDashBoardNew
    {
        public string Popup_Type { get; set; }
        public int ContinousAbsenteesim { get; set; }
        public int UnauthorisedAbsence { get; set; }
        public int PendingReg { get; set; }
        public int UpcomingLeaves { get; set; }
        public int DutyRosterVariance { get; set; }
        public int LessHours { get; set; }
        public int WFHLeaves { get; set; }
        public int LOP { get; set; }
        public string CurrentMonth { get; set; }
        public Filter filter { get; set; }
        public List<TimeOfficeDashBoardWorkStatusList> TimeOfficeDashBoardWorkStatusList { get; set; }
        public List<DashBoardNotification> DashBoardNotificationList { get; set; }
        public List<TimeOfficeDashBoardNewList> TimeOfficeDashBoardList { get; set; }
        public List<TimeOfficeDashBoardNewDetailsList> TimeOfficeDashBoardDetailsList { get; set; }
        public List<TimeOfficeDashBoardDiagram> TimeOfficeDashBoardDiagram { get; set; }
        public List<TimeOfficeDashBoardNotification_popup> Notification_PopUp { get; set; }
    }
    public class TimeOfficeDashBoardWorkStatusList
    {
        public string Type { get; set; }
        public int WFH { get; set; }
        public int Leave { get; set; }
        public int Onduty { get; set; }
        public int AB { get; set; }
        public int RA { get; set; }
        public int E_OUT { get; set; }
        public int SL { get; set; }
        public int L_HRS { get; set; }
        public int LATE_IN { get; set; }
    }
    public class TimeOfficeDashBoardNewList
    {
        public string Date { get; set; }
        public int count { get; set; }
        public string Type { get; set; }
        public decimal CheckInTimeStatus { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
    }
    public class TimeOfficeDashBoardNewDetailsList
    {
        public Guid EmployeeGI { get; set; }
        public string Type { get; set; }
        public int EmployeeCode { get; set; }
        public string FullName { get; set; }
        public string EmployeeID { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public string YearOfService { get; set; }
        public DateTime From { get; set; }
        public string To { get; set; }
        public string Reason { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string DateofApplication { get; set; }
        public decimal NoOfDays { get; set; }
        public string ApplicationStatus { get; set; }
        public DateTime LessHourDate { get; set; }
        public string IN { get; set; }
        public string OUT { get; set; }
        public string LessHours { get; set; }
        public string AssignedShift { get; set; }
        public string EmployeeAbsoluteUri { get; set; }
        public string FileName { get; set; }
        public string WorkedShift { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public DateTime EndDate { get; set; }
    }
    public class LowAttendanceRateNew
    {
        public List<string> Head { get; set; }
        public List<Row> Rows { get; set; }
    }
    public class Row
    {
        public List<string> Columns { get; set; }
    }
    public class TimeOfficeDashBoardDiagram
    {
        public int SalaryPeriodCode { get; set; }
        public string SalaryPeriodName { get; set; }
        public int Date { get; set; }
        public string FullDate { get; set; }
        public int COUNT { get; set; }
    }
    public class TimeOfficeDashBoardNotification_popup
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public DateTime DOJ { get; set; }
        public string Remarks { get; set; }
        public string PunchingID { get; set; }

    }
}
