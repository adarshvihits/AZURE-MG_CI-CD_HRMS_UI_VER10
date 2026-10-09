using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.TrainingModule
{
  public class TraineeCost
    {
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public int EmployeeCode { get; set; }

        public EmployeeHeader EmployeeHeader { get; set; }
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
        public int Traineecost { get; set; }
        public int radiomode { get; set; }
        public int FinancialYearCode { get; set; }
        public string FinancialYearName { get; set; }
        public Decimal AllotedAmount { get; set; }
        public Decimal Varience { get; set; }
        public List<TraineeCost> TraineeCostList { get; set; }
        public List<TraineeCost_details> Details { get; set; }
    }
    public class TraineeCost_details
    {
        public int EmployeeCode { get; set; }
        public string CostHead { get; set; }
        public int CostHeadCode { get; set; }
        public int Cost { get; set; }

    }
}
