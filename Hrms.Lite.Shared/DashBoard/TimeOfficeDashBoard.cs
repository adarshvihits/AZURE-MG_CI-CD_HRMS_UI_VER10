using Hrms.Lite.Shared.DataBank.Generic.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DashBoard
{
    public class TimeOfficeDashBoard
    {
        public int ApprovalPending { get; set; }
        public int LowAttendanceRate { get; set; }
        public int AttendanceSettingsMissing { get; set; }
        public int UpcomingLeaves { get; set; }
        public int PreviousMonthAdjustments { get; set; }
        public int LessHours { get; set; }
        public int ContinousAbsenteesim { get; set; }
        public int DutyRosterVariance { get; set; }
        public int AuthorisedLOP { get; set; }
        public int UnauthorizedLOPs { get; set; }
       public string Popup_Type { get; set; }
        public List<TimeOfficeDashBoardList> TimeOfficeDashBoardList { get; set; }
        public List<TimeOfficeDashBoardList> Absenteeism_by_Day { get; set; }
        public List<TimeOfficeDashBoardList> Today_Check_In_Time_Status { get; set; }
        public List<TimeOfficeDashBoardDetailsList> TimeOfficeDashBoardDetailsList { get; set; }
        public ShiftDetails ShiftDetails { get; set; }
    }
    public class TimeOfficeDashBoardList
    {
        public string Date { get; set; }
        public int count { get; set; }
        public string Type { get; set; }
        public decimal CheckInTimeStatus { get; set; }
    }
    public class TimeOfficeDashBoardDetailsList
    {
        public string Type { get; set; }
        public int EmployeeCode { get; set; }
        public string FullName { get; set; }
        public string EmployeeID { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public string YearOfService { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string Reason { get; set; }
        public DateTime DateofApplication { get; set; }
        public decimal NoOfDays { get; set; }
        public string ApplicationStatus { get; set; }
        public DateTime LessHourDate { get; set; }
        public string IN { get; set; }
        public string OUT { get; set; }
        public string LessHours { get; set; }
        public string AssignedShift { get; set; }
        public string WorkedShift { get; set; }
    }
    public class LowAttendanceRate
    {
        public List<string> Head { get; set; }
        public List<Rows> Rows { get; set; }
    }
    public class Rows
    {
        public List<string> Columns { get; set; }
    }
}
