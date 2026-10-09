using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.Reports
{
    public class CelebrityList
    {
        public string ID { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string EmployeeName { get; set; }
        public string Type { get; set; }
        public DateTime Date { get; set; }
        public string Designation { get; set; }
        public string AbsoluteUri { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public string Category { get; set; }
        public string Mobile { get; set; }
        public string EmailID { get; set; }
        public string OfficeEmail { get; set; }
        public string Remarks { get; set; }
        public Guid EmployeeGI { get; set; }
        public int EmployeeCode { get; set; }
        public List<CelebrityList> List { get; set; }
        public int AllCount { get; set; }
        public int BirthdayCount { get; set; }
        public int WeddingCount { get; set; }
        public int JoiningCount { get; set; }
        public Filter Filter { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public string Breadscrumb { get; set; }
        public string FirstTab { get; set; }


        public string ProfileFilename { get; set; }
        public string Base64string { get; set; }
    }
}
