using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.DTO
{
    public class Suspension
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string Remarks { get; set; }
        public Filter Filter { get; set; }
        public int PendingCount { get; set; }
        public int CompletedCount { get; set; }
        public string Tab { get; set; }
        public List<SuspensionDetails> SuspensionList { get; set; }
        public List<SuspensionDetails> TransactionHistoryList { get; set; }
        public SuspensionDetails Details { get; set; }
        public List<SuspensionSalarySplit> SuspensionSalarySplit { get; set; }
    }
    public class SuspensionDetails
    {
        public Guid EmployeeGI { get; set; }
        public Guid IncidentGI { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        public string Location { get; set; }
        public string DOJ { get; set; }
        public string Incident { get; set; }
        public string Category { get; set; }
        public string OccuredDate { get; set; }
        public string ReportedBy { get; set; }
        public string ReportedDate { get; set; }
        public string FileName { get; set; }
        public string AbsoluteUri { get; set; }
        public string ActionTaken { get; set; }
        public string Narration { get; set; }
        public string EntryUser { get; set; }
        public string EntryDate { get; set; }
    }
    public class SuspensionSalarySplit
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public decimal CuttingPercentage { get; set; }
        public int AttendanceStatus { get; set; }
    }
}
