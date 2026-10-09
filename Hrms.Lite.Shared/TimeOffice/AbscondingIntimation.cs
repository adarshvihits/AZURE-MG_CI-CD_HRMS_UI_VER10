using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.TimeOffice
{
    public class AbscondingIntimation
    {
        public Filter Filter { get; set; }
        public List<IntimationList> IntimationList { get; set; }
        public EmployeeHeader EmployeeHeader { get; set; }
        public Guid EmployeeGI { get; set; }
        public List<History> HistoryList { get; set; }
        public DateTime? DatagDate { get; set; }
        public string DetagRemarks { get; set; }
    }
    public class History
    {
        public string EmployeeName { get; set; }
        public string Remarks { get; set; }
        public string CommunicatedMethod { get; set; }
        public int EntryEmployeeCode { get; set; }
        public int? CommunicatedMethodCode { get; set; }
        public Guid EmployeeGI { get; set; }
        public DateTime AbscondingDate { get; set; }
    }
    public class IntimationList
    {
        public string EmployeeName { get; set; }
        public string EntryEmployeeName { get; set; }
        public string Remarks { get; set; }
        public string EmployeeID { get; set; }
        public string Designation { get; set; }
        public string FunctionalDesignation { get; set; }
        public string CommunicatedMethod { get; set; }
        public int EntryEmployeeCode { get; set; }
        public int EmployeeCode { get; set; }
        public string Status { get; set; }
        public int? CommunicatedMethodCode { get; set; }
        public Guid EmployeeGI { get; set; }
        public DateTime AbscondingDate { get; set; }
    }
}
