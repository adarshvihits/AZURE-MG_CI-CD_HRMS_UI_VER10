using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.Reports
{
    public class CanteenDateWiseReport
    {
        public int MealTime { get; set; }
        public int MealType { get; set; }
        public DateTime? Date { get; set; }
        public int Total { get; set; }
        public Filter filter { get; set; }
        public List<CanteenDateEmployeeList> CanteenDateEmployeeList { get; set; }
        public List<MealTypeSummaryList> MealTypeSummaryList { get; set; }
    }
    public class CanteenDateEmployeeList
    {
        public EmployeeHeader EmployeeHeader { get; set; }
        public int EmployeeCode { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public Guid EmployeeGI { get; set; }
        public Designation Designation { get; set; }
        public Department Department { get; set; }
        public Location Location { get; set; }
        public Category Category { get; set; }
        public string MealType { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
    }

    public class MealTypeSummaryList
    {
        public int MealTypeCode { get; set; }
        public string Name { get; set; }
        public int TotalCount { get; set; }
    }
}
