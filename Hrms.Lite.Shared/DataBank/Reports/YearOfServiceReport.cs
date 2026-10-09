using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.Reports
{
    public  class YearOfServiceReport
    {
        public Filter filter { get; set; }
        public string option { get; set; }
        public Dropdown Type { get; set; }
        public Guid EmployeeGI { get; set; }
        public int EmployeeCode { get; set; }
        public string AbsoluteUri { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string GroupByName { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public string Category { get; set; }
        public DateTime? DOJ { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public string YOS { get; set; }
        public string Total_YOS { get; set; }
        public string DateOfEntryIntoService { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public List<YearOfServiceReport> YearOfServiceReportList { get; set; }

        public string Age { get; set; }

    }
}
