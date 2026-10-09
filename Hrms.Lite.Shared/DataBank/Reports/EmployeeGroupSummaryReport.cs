using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.Reports
{
   public class EmployeeGroupSummaryReport
    {

        public List<string> Head { get; set; }
        public List<Row> Rows { get; set; }
        public List<string> Footer { get; set; }
        public int GroupByX { get; set; }
        public int GroupByY { get; set; }
        public Filter filter { get; set; }
        
        public List<EmployeeGroupSummarySelectList> EmployeeGroupSummarySelectList { get; set; }
        public Filter SliderFilter { get; set; }

    }
    public class Row
    {
        public List<string> Columns { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
    }
    public class EmployeeGroupSummarySelectList
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public bool Applicability { get; set; }
    }

}

