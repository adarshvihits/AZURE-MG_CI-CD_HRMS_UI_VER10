using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.PayRoll.Reports
{
    public class IncentivePostingReport
    {
        public Filter filter { get; set; }
        public Dropdown MonthDD { get; set; }
        public string PeriodID { get; set; }
        public string Month { get; set; }
        public string EmployeeID { get; set; }
        public string EmpLocation { get; set; }
        public string EmpLocName { get; set; }
        public string Department { get; set; }
        public string DepartmentShortName { get; set; }
        public string DepartmentCostCentre { get; set; }
        public string FuncDesignation { get; set; }
        public decimal IncentiveAmount { get; set; }
        public int IncAmountRound { get; set; }
        public string IncentiveSource { get; set; }
        public string IncentiveComponent { get; set; }
        public string Earned_Direct { get; set; }
        public string IncentiveType { get; set; }
        public string Company { get; set; }
        public string FinalEntryMapping { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public List<IncentivePostingReport> IncentivePostingReportList { get; set; }
    }
}
