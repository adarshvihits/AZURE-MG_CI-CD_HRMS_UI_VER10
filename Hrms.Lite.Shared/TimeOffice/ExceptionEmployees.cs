using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.TimeOffice
{
    public class ExceptionEmployees 
    {
        public Guid EmployeeGI { get; set; }
        public int EmployeeCode { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public string Designation { get; set; }
        public string Grade { get; set; }
        public string HoldStatus { get; set; }
        public bool Applicability { get; set; }
        public Filter filter { get; set; }
        public UserBase User { get; set; }
        public File Excel { get; set; }
        public Dropdown Month { get; set; }
        public Dropdown Month1 { get; set; }

        public Decimal Abdays { get; set; }
        public string MonthName { get; set; }
        public int active { get; set; }
        public int TotalRecords { get; set; }
        public int BasedOn { get; set; }
        public string Remarks { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public int TotalExceptionEmployeeCount { get; set; }
        public List<ExceptionEmployees> ExceptionEmployeesList { get; set; }

        public List<ExceptionEmployees> TransactionHistoryList { get; set; }

    }
}
