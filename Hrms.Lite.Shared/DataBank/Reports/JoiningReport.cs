using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.Reports
{
   public class JoiningReport
    {
        public List<JoiningReport_List> JoiningReportList { get; set; }
        public List<EmployeeProfileSummary> EmployeeProfileSummary { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public GroupByComboFill GroupByFill { get; set; }
        public string groupbyname { get; set; }
        public Filter SliderFilter { get; set; }

    }
        public class JoiningReport_List
        {
            public EmployeeHeader EmployeeHeader { get; set; }
            public Guid EmployeeGI { get; set; }
            public string Employeeid { get; set; }
            public string EmployeeName { get; set; }
            public string Designation { get; set; }
        public string FunctionalState { get; set; }
        public string Department { get; set; }
            public string Location { get; set; }
            public string Category { get; set; }
            public string DOJ { get; set; }
            public string DOB { get; set; }
            public Decimal GrossPay { get; set; }
            public string AbsoluteUri { get; set; }
            public string ProfileFilename { get; set; }
            public string Base64string { get; set; }
            public int totalRecords { get; set; }
            public int active { get; set; }
       

    }
    }




