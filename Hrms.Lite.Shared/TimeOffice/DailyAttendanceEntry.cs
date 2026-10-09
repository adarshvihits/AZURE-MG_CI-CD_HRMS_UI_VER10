
using Hrms.Lite.Shared.TimeOffice.Generic.Master;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using Hrms.Lite.Shared.TimeOffice.Generic;

namespace Hrms.Lite.Shared.TimeOffice
{
    public class DailyAttendanceEntry
    {
        public int EmpId { get; set; }
        public Guid EmployeeGI { get; set; }
        public DateTime? ProcesingDate { get; set; }
        public EmployeeHeader EmployeeHeader { get; set; }
        public decimal AllCount { get; set; }
        public decimal AbsenteesamCount { get; set; }
        public decimal LeaveCount { get; set; }
        public decimal Percentage { get; set; }
        public int TotalRows { get; set; }
        public Month Month { get; set; }
        public int[] Attendancestatus { get; set; }
        public int AttendanceType { get; set; }
        public bool ShowAbsentDays { get; set; }
        public PayrollGroup PayrollGroup { get; set; }
        public  int   TabIndex { get; set; }
        public DateTime SelectedDate { get; set; }
        public MainList MainList { get; set; }
        public AttnDayStatus  AttnDay { get; set; }
        public List<dynamic> Data { get; set; }
        public List<DailyAttendanceEditList> DailyAttendanceEditList { get; set; }
        public DailyAttendanceEditDetails DailyAttendanceEditDetails { get; set; }
        public List<Employee_DailyAttendanceEditDetails> Employee_DailyAttendanceEditDetails { get; set; }
        public List<AttendanceSummary> AttendanceSummary { get; set; }
        public List<LeaveBalance> LeaveBalance { get; set; }
        public List<CoffDetails> CoffDetails { get; set; }
        public List<EmployeeWiseAttendanceList> EmployeeWiseAttendanceList { get; set; }
        public BatchUpDate BatchUpDate { get; set; }
        public Employee_DailyAttendanceBatchUpdate Employee_DailyAttendanceBatchUpdate { get; set; }
        public Filter Filter { get; set; }
        public List<DailyAttendanceProcessing> DailyAttendanceProcessing { get; set; }
        public List<DailyAttendanceProcessing_Status> status { get; set; }
        public DateTime? From { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public DateTime? To { get; set; }
        public List<DailtAttendance_popup> DailtAttendance_popup { get; set; }

    }
    public class MainList
    {
        public List<Header> Header { get; set; }
        public List<EmployeePrimaryInfo> EmployeePrimaryInfo { get; set; }
        public List<AttendanceDayDetailsDTO> AttendanceDayDetailsDTO { get; set; }

    }
    public class Header
    {
        public string Date { get; set; }
        public string HeaderName { get; set; }
        public string FooterName { get; set; }
        public string Color { get; set; }
        public string FieldName { get; set; }
    }
   
    public class EmployeePrimaryInfo
    {
        public int EmployeeCode { get; set; }
        public Guid EmployeeGI { get; set; }
        public string EmployeeID { get; set; }
        public string FullName { get; set; }
    }
    public class AttendanceDayDetailsDTO
    {
        public int StatusCode { get; set; }
        public string StatusName { get; set; }
        public string Color { get; set; }

    }
    public class DailyAttendanceEditList
    {
        public int EmployeeCode { get; set; }
        public Guid EmployeeGI { get; set; }
        public string EmployeeID { get; set; }
        public string FullName { get; set; }
        public Department Department { get; set; }
        public Location Location { get; set; }
        public Shift Shift { get; set; }
        public string IN { get; set; }
        public string OUT { get; set; }
        public string TotalTime { get; set; }
        public decimal ESI { get; set; }
        public decimal PaidDays { get; set; }
        public AttnDayStatus FirstHalf { get; set; }
        public AttnDayStatus SecondHalf { get; set; }
        public string Remarks { get; set; }
        public int active { get; set; }
        public int totalRecords { get; set; }
    }
    public class DailyAttendanceEditDetails
    {
        public DateTime SelectedDate { get; set; }
        public int EmployeeCode { get; set; }
        public Guid EmployeeGI { get; set; }
        public string EmployeeID { get; set; }
        public string FullName { get; set; }
        public Department Department { get; set; }
        public Location Location { get; set; }
        public Shift Shift { get; set; }
        public string ShiftTime { get; set; }
        public string Day { get; set; }
        public string IN { get; set; }
        public string OUT { get; set; }
        public string TotalTime { get; set; }
        public string LateIn { get; set; }

        public string EarlyOut { get; set; }
        public string OverTime { get; set; }
        public AttnDayStatus FirstHalf { get; set; }
        public AttnDayStatus SecondHalf { get; set; }
        public string CoffEntitleDate { get; set; }
        public string CoffEntitleDateValue { get; set; }
        public decimal CoffEntitleDateBalance { get; set; }
        public string Remarks { get; set; }
        public AttendanceStatusType FH_AttendStatusType { get; set; }
        public AttendanceStatusType SH_AttendStatusType { get; set; }
    }
    public class BatchUpDate
    {
        public DateTime Date { get; set; }
        public AttnDayStatus FirstHalfOld { get; set; }
        public AttnDayStatus FirstHalfNew { get; set; }
        public AttnDayStatus SecondHalfOld { get; set; }
        public AttnDayStatus SecondHalfNew { get; set; }
        public string Remarks { get; set; }

    }
    public class Employee_DailyAttendanceEditDetails
    {
       
        public DateTime Date { get; set; }
        public string Day { get; set; }
        public Shift Shift { get; set; }
        public string IN { get; set; }
        public string OUT { get; set; }
        public string TotalTime { get; set; }
        public AttnDayStatus FirstHalf { get; set; }
        public AttnDayStatus SecondHalf { get; set; }
        public Dropdown FHType { get; set; }
        public Dropdown SHType { get; set; }
        public string Remarks { get; set; }
        public DateTime? EntitleDate { get; set; }
    }
    public class AttendanceSummary
    {
        public int StatusCode { get; set; }
        public string Status { get; set; }
        public decimal Count { get; set; }
    }
    public class LeaveBalance
    {
        public int leaveCode { get; set; }
        public string Leave { get; set; }
        public decimal Balance { get; set; }
    }
    public class CoffDetails
    {
        public DateTime EDate { get; set; }
        public DateTime ExpireOn { get; set; }

    }
    public class Employee_DailyAttendanceBatchUpdate
    {
        public Guid EmployeeGI { get; set; }
        public Month Month { get; set; }
        public PayrollGroup PayrollGroup { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public AttnDayStatus FirstHalf { get; set; }
        public AttnDayStatus SecondHalf { get; set; }
        public string Remarks { get; set; }
    }
    public class EmployeeWiseAttendanceList
    {
        public int EmployeeCode { get; set; }
        public Guid EmployeeGI { get; set; }
        public string EmployeeID { get; set; }
        public string FullName { get; set; }
        public decimal Absent { get; set; }
        public decimal Leave { get; set; }
        public decimal Lop { get; set; }
        public decimal OffDay { get; set; }
        public decimal HoliDay { get; set; }
        public decimal ESI { get; set; }
        public decimal PaidDays { get; set; }
        public decimal OffdayPresent { get; set; }
        public decimal HolidayPresent { get; set; }
        public decimal Coff { get; set; }
        public decimal PresentDays { get; set; }
        public decimal OnDuty { get; set; }
        public decimal Ra { get; set; }
        public decimal Wfh { get; set; }
        public decimal Ljp { get; set; }
        public decimal Ltp { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
    }
    public class DailyAttendanceProcessing
    {
        public int EmployeeCode { get; set; }
        public Guid EmployeeGI { get; set; }
        public string EmployeeID { get; set; }
        public string FullName { get; set; }
        public Department Department { get; set; }
        public Designation Designation { get; set; }
        public Location Location { get; set; }
        public Shift Shift { get; set; }
        public string IN { get; set; }
        public string LateIn { get; set; }
        public string EarlyIn { get; set; }
        public string PunchingDate { get; set; }

        public string OUT { get; set; }
        public string EarlyOut { get; set; }
        public string OverTime { get; set; }
        public string TotalTime { get; set; }
        public string InShiftTime { get; set; }
        public string Session { get; set; }
        public string Reason { get; set; }
     
        public int totalRecords { get; set; }
        public int active { get; set; }

        public string LateOutEarlyIn { get; set; }

    }

    public class DailyAttendanceProcessing_Status
    {
        public int Code { get; set; }
        public string Description { get; set; }
        public int Count { get; set; }
    }
    public class DailtAttendance_popup
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }

        public string PunchingID { get; set; }
        public string Remarks { get; set; }

    }
}
