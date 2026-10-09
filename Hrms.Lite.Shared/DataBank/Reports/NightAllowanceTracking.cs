using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice.Generic;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.Reports
{
    public class NightAllowanceTracking
    {
        public Filter Filter { get; set; }
        public Dropdown MonthDD { get; set; }
        public int HDCode { get; set; }
        public string RequestID { get; set; }
        public DateTime ApplyDate { get; set; }
        public Guid EmployeeGI { get; set; }
        public int EmployeeCode { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string FuncDesignation { get; set; }
        public string Authority { get; set; }
        public string Status { get; set; }
        public decimal Amount { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public List<NightAllowanceTracking> NightAllowanceTrackingList { get; set; }
        public EmployeeHeader EmployeeHeader { get; set; }
        public decimal TotalHours { get; set; }
        public string Purpose { get; set; }
        public List<RequestDetails> RequestDetailsList { get; set; }
        public List<AuthorityDetails> AuthorityDetailsList { get; set; }
    }
    public class RequestDetails
    {
        public int SlNo { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string TotalTime { get; set; }
        public decimal Amount { get; set; }
    }
    public class AuthorityDetails
    {
        public int SlNo { get; set; }
        public string EmployeeID { get; set; }
        public string Authority { get; set; }
        public string Designation { get; set; }
        public string Level { get; set; }
        public string Action { get; set; }
        public DateTime? ActionTime { get; set; }
        public string Remarks { get; set; }
    }
}
