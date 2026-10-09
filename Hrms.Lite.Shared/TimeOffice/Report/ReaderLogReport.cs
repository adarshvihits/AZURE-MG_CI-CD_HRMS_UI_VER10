using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.TimeOffice.Report
{
    public class ReaderLogReport
    {
        public List<string> Head { get; set; }
        public List<ReaderLogReportList> ReaderLogReportList { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public int GroupBy { get; set; }
        public int Classification { get; set; }
        public Filter filter { get; set; }
    }
    public class ReaderLogReportList
    {

        public int EmployeeCode { get; set; }
        public string EmployeeID { get; set; }
        public Guid EmployeeGI { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public string PUNCHID { get; set; }
        public string DATE { get; set; }
        public string TIME { get; set; }
        public string STATUS { get; set; }
        public string ReaderName { get; set; }
        public string ReaderType { get; set; }
        public int active { get; set; }
        public int totalRecords { get; set; }

        public string GeoAddress { get; set; }
    }
}
