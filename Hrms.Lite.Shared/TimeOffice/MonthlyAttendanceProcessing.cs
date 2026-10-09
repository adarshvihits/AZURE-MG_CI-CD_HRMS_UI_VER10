using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using Hrms.Lite.Shared.TimeOffice.Generic.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.TimeOffice
{
    public class MonthlyAttendanceProcessing
    {

        public Month Month { get; set; }
        public PayrollGroup PayrollGroup { get; set; }
        public string Period { get; set; }
        public int TabIndex { get; set; }
        public int TotalDays { get; set; }
        public int EmployeeStatus { get; set; }
        public MasterBase Employee { get; set; }
        public EmployeeHeader EmployeeHeader { get; set; }
        public List<MonthlyAttendanceProcessing_Validations> MonthlyAttendanceProcessing_Validations { get; set; }
        public List<MonthlyAttendanceProcessing_Data> MonthlyAttendanceProcessing_Data { get; set; }
        public List<MonthlyAttendanceProcessing_Abbreviation> Abbreviation { get; set; }
        public MonthlyAttendanceProcessing_Msg Validation_Msg { get; set; }
        public List<MonthlyAttendanceProcessing_Validations_popup> MonthlyAttendanceProcessing_Validations_popup { get; set; }

        public Filter Filter { get; set; }
        public List<AttendanceApprovalPending_popup> AttendanceApprovalPending_popup { get; set; }
    }
    public class MonthlyAttendanceProcessing_Msg
    {
        public string Description { get; set; }
        public int FLAG { get; set; }
    }
    public class MonthlyAttendanceProcessing_Validations_popup
    {
        public string EmployeeID { get; set; }
        public Guid EmployeeGI { get; set; }
        public string FullName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public int SlNo { get; set; }
        public DateTime PendingDates { get; set; }
        public decimal Count { get; set; }
        public DateTime Dates { get; set; }


    }

    public class MonthlyAttendanceProcessing_Abbreviation
    {
        public string ShortName { get; set; }
        public string Fullname { get; set; }
        public string Description { get; set; }
    }
    public class MonthlyAttendanceProcessing_Validations
    {
        public int SlNo { get; set; }
        public string Description { get; set; }
        public int Count { get; set; }
    }
    public class MonthlyAttendanceProcessing_Data
    {
        public int EmployeeCode { get; set; }
        public Guid EmployeeGI { get; set; }
        public string EmployeeID { get; set; }
        public string FullName { get; set; }
        public decimal PresentDays { get; set; }
        public decimal Ra { get; set; }
        public decimal Absent { get; set; }
        public decimal Lop { get; set; }
        public decimal ESILeave { get; set; }

        public decimal Leave { get; set; }
        public decimal Coff { get; set; }
        public decimal OnDuty { get; set; }
        public decimal Wfh { get; set; }
        public decimal HoliDay { get; set; }
        public decimal HolidayPresent { get; set; }
        public decimal OffDay { get; set; }
        public decimal OffdayPresent { get; set; }

        public decimal Ljp { get; set; }
        public decimal Ltp { get; set; }
        public decimal PreviousLOP { get; set; }
        public decimal PreviousArrear { get; set; }
        public decimal Suspension { get; set; }
        public decimal TotalLOP { get; set; }
        public decimal PaidDays { get; set; }
        public decimal LayOff { get; set; }
        public decimal MaternityLeave { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public decimal DoubleDuty { get; set; }
    }
    public class AttendanceApprovalPending_popup
    {
        public string EmployeeID { get; set; }
        public string FullName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public string Type { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Authority { get; set; }
        public int active { get; set; }
        public int totalRecords { get; set; }

    }
}
