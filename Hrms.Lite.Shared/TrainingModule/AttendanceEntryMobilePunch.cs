using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.TrainingModule
{
  public  class AttendanceEntryMobilePunch
    {
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public int EmployeeCode { get; set; }
        public Guid EmployeeGI { get; set; }
        public string Location { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        public DateTime DOJ { get; set; }
        public string TrnStatus { get; set; }
        public string Action { get; set; }
        public int ScheduleCode { get; set; }
        public Filter Filter { get; set; }
        public int active { get; set; }
        public int totalRecords { get; set; }
        public bool applicable { get; set; }
        public int radiomode { get; set; }
        public int radio1 { get; set; }
        public int TotalTopics { get; set; }
        public int TotalPunch { get; set; }
        public string completionmode { get; set; }
        public List<AttendanceEntryMobilePunch> AttendanceEntryMobilePunchList { get; set; }
        public List<TopicDetails> TopicDetails { get; set; }
    }
    public class TopicDetails
    {
        public int EmployeeCode { get; set; }
        public int ScheduleCode { get; set; }
        public string Punchtime { get; set; }
        public string Topic { get; set; }
        public string Addr { get; set; }
    }
}
