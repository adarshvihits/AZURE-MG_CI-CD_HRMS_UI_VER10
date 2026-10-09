using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.Master
{
    public class HRPlanningMaster : MasterBase
    {
        public int AvgSalary { get; set; }
        public string planId { get; set; }
        public int PlanCount { get; set; }
        public int TotalSalary { get; set; }
        public int PendingCount { get; set; }
        public string Status { get; set; }
        public string EntryType { get; set; }
        public string BranchName { get; set; }
        public string DepartmentName { get; set; }
        public string SectionName { get; set; }
        public string GradeName { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public Dropdown Branch { get; set; }
        public Dropdown PlanningPeriod { get; set; }
        public Dropdown Department { get; set; }
        public Dropdown Grade { get; set; }
        public Dropdown Section { get; set; }
        public int Logcode { get; set; }
        public string Remarks { get; set; }
        public List<HRPlanningMaster> HRPlanningMasterList { get; set; }
    }
    public class Combination : MasterBase
    {

    }
}