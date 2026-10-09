using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.TimeOffice
{
    public class AttendanceSetting
    {
        public int GroupBy { get; set; }
        public int Classification { get; set; }
        public Filter filter { get; set; }
        public EmployeeHeader EmployeeHeader { get; set; }
        public List<AttendanceSettingList> AttendanceSettingList { get; set; }
        public AttendanceSettingList AttendanceSettingDetails { get; set; }
        public int?[] AttendanceSettingPunchMethods { get; set; }
        public int?[] AttendanceSettingShtSelection { get; set; }
        public string PunchID { get; set; }
        public int?[] ShiftSelection { get; set; }
        public int? ShiftSelect { get; set; }
        public int? ShiftType { get; set; }
        public int?[] PunchingMethod { get; set; }
        public int? PunchingApplicability { get; set; }
        public int? HolidayGroup { get; set; }
        public int? Offday { get; set; }
        public int? LeaveGroup { get; set; }
        public int?[] ApplicableLeave { get; set; }
        public int COFF { get; set; }
        public int? Circle { get; set; }
        public int? SinglePunch { get; set; }
        public int? NightOff { get; set; }
        public string Type { get; set; }
        public int? ODAR { get; set; }
    }
    public class AttendanceSettingList
    {
        public int EmployeeCode { get; set; }
        public string EmpId { get; set; }
        public Guid EmployeeGuid { get; set; }
        public string EmployeeName { get; set; }
        public string DESIGNATION { get; set; }
        public string PUNCHINGID { get; set; }
        public string PUNCHINGMETHOD { get; set; }
        public string SHIFTTYPE { get; set; }
        public string HOLIDAYGROUP { get; set; }
        public string OFFDAY { get; set; }
        public string LEAVEGROUP { get; set; }
        public bool COFFAPPLICABILTY { get; set; }
        public bool LESSHOUR { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public bool SINGLEPUNCHAPPLICABILTY { get; set; }
        public bool NIGHTOFFAPPLICABILTY { get; set; }
        public bool ODAR { get; set; }
    }
}
