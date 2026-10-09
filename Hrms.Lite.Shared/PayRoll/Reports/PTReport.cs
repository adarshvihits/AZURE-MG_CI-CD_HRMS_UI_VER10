using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.PayRoll.Reports
{
    public class PTReport
    {
        

        public int PTGroupMonth { get; set; }
        public int PTGroupCode { get; set; }
        public FilterMultiSelect Filter { get; set; }



        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public DateTime DOJ { get; set; }
        public DateTime? DOT { get; set; }
        public string PTGroup { get; set; }
        public int Duration { get; set; }
        public string SalaryFrom { get; set; }
        public string SalaryTo { get; set; }
        public int RemainingMonth { get; set; }
        public decimal ProcessedMonth { get; set; }
        public decimal CurrentMonthSalary { get; set; }
        public decimal ActualSalary { get; set; }
        public decimal ProjectedSalary { get; set; }
        public decimal TotalSalary { get; set; }
        public decimal PTAmount { get; set; }
        public decimal PTPaid { get; set; }
        public decimal BalancePT { get; set; }
        public decimal CurrentMonthPt { get; set; }
        public string DurationName { get; set; }
        public int active { get; set; }
        public int totalRecords;
        public List<PTReport> GetPTReportList { get; set; }

    }
}
