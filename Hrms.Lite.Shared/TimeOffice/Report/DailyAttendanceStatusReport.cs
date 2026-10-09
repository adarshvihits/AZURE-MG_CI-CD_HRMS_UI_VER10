using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.TimeOffice.Report
{
    public class DailyAttendanceStatusReport
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public Designation Designation { get; set; }
        public int?[] Designations { get; set; }
        public Department Department { get; set; }
        public int?[] Departments { get; set; }
        public Location Location { get; set; }
        public int?[] Locations { get; set; }
        public Category Category { get; set; }
        public int?[] Categories { get; set; }
        public Grade Grade { get; set; }
        public int?[] Grades { get; set; }
        public PayrollGroup PayrollGroup { get; set; }
        public int?[] PayrollGroups { get; set; }
        public Division Division { get; set; }
        public int?[] Divisions { get; set; }
        public Section Section { get; set; }
        public int?[] Sections { get; set; }
        public EmploymentType EmploymentType { get; set; }
        public int?[] EmploymentTypes { get; set; }
        public WorkLocation WorkLocation { get; set; }
        public int?[] WorkLocations { get; set; }
        public WageGradeMaster WageGradeMaster { get; set; }
        public int?[] WageGrades { get; set; }
        public AttendanceStatus AttendanceStatus { get; set; }
        public int?[] AttendanceStatuses { get; set; }
        public List<dynamic> Data { get; set; }
        public DailyAttendanceStatusReport_MainList MainList { get; set; }
        public Region Region { get; set; }
        public int?[] Regions { get; set; }
        public State State { get; set; }
        public int?[] States { get; set; }
        public ShiftType Shift { get; set; }
        public int?[] Shifts { get; set; }

    }
    public class DailyAttendanceStatusReport_MainList
    {
        public List<DailyAttendanceStatusReport_Header> Header { get; set; }
        public List<DailyAttendanceStatusReport_EmployeePrimaryInfo> EmployeePrimaryInfo { get; set; }
        public List<DailyAttendanceStatusReport_Details> DailyAttendanceStatusReport_Details { get; set; }


    }
    public class DailyAttendanceStatusReport_Header
    {
        public string HeaderName { get; set; }
        public string FieldName { get; set; }
    }
    public class DailyAttendanceStatusReport_EmployeePrimaryInfo
    {
        public int EmployeeCode { get; set; }
        public string EmployeeID { get; set; }
        public string FullName { get; set; }

    }
    public class DailyAttendanceStatusReport_Details
    {
        public string Description { get; set; }

    }
}
