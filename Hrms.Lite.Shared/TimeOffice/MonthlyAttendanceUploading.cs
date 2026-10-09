using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.TimeOffice
{
    public class MonthlyAttendanceUploading
    {
        public int MonthCode { get; set; }
        public int PayrollGroup { get; set; }
        public string EmployeeID { get; set; }
        public string FullName { get; set; }
        public decimal Lop { get; set; }
        public Filter Filter { get; set; }
        public File Excel { get; set; }
        public List<MonthlyAttendanceUploading_Data> MonthlyAttendanceUploading_Data { get; set; }
    }
    public class MonthlyAttendanceUploading_Data
    {
        public int EmployeeCode { get; set; }
        public Guid EmployeeGI { get; set; }
        public string EmployeeID { get; set; }
        public string FullName { get; set; }
        public decimal PresentDays { get; set; }
        public decimal Ra { get; set; }
        public decimal Absent { get; set; }
        public decimal Lop { get; set; }
        public string LopString { get; set; }
        public decimal ESILeave { get; set; }

        public decimal Leave { get; set; }
        public decimal Coff { get; set; }
        public decimal OnDuty { get; set; }
        public decimal Wfh { get; set; }
        public decimal HoliDay { get; set; }
        public decimal HolidayPresent { get; set; }
        public string HpString { get; set; }
        public decimal OffDay { get; set; }
        public decimal OffdayPresent { get; set; }
        public string OpString { get; set; }
        public decimal OTHrs { get; set; }
        public string OTHrsString { get; set; }
        public decimal LateHrs { get; set; }
        public string LateHrsString { get; set; }
        public decimal Ljp { get; set; }
        public decimal Ltp { get; set; }
        public decimal PreviousLOP { get; set; }
        public decimal PreviousArrear { get; set; }
        public decimal Suspension { get; set; }
        public decimal TotalLOP { get; set; }
        public decimal PaidDays { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public string Remarks { get; set; }

    }
}

