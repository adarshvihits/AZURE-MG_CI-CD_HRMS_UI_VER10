
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.Master
{

    public class NewBranchPositionCreationViewModel
    {
        public NewBranchPositionCreationSummary Summary { get; set; }

        public List<NewBranchPositionCreation> Positions { get; set; }
            = new List<NewBranchPositionCreation>();

        public Dropdown PlanningPeriod { get; set; }

        public int ActiveCount { get; set; }
        public int InactiveCount { get; set; }
        public int PendingCount { get; set; }
    }
    public class NewBranchPositionCreationSummary
    {
        public int ApprovedPlanLines { get; set; }
        public int NewBranches { get; set; }
        public int Created { get; set; }
        public int AwaitingJD { get; set; }
    }


    public class NewBranchPositionCreation : MasterBase
    {
        public int AvgSalary { get; set; }
        public int PlanCount { get; set; }
        public int TotalSalary { get; set; }
        public int PendingCount { get; set; }
        public string planId { get; set; }
        public string CurrencyName { get; set; }
        public string EntryType { get; set; }
        public string BranchName { get; set; }
        public string DepartmentName { get; set; }
        public string SectionName { get; set; }
        public string GradeName { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public Dropdown Branch { get; set; }
        //public Dropdown PlanningPeriod { get; set; }
        public Dropdown Department { get; set; }
        public Dropdown Grade { get; set; }
        public Dropdown Section { get; set; }
        public int PlanCode { get; set; }
        public string Remarks { get; set; }

        //public List<NewBranchPositionCreation> NewBranchPositionCreationList { get; set; }

      
    }
    public class PositionRequest
    {
        public int PlanCode { get; set; }
        public int PositionsRequested { get; set; }
        public string Priority { get; set; }
        public DateTime? TargetFillDate { get; set; }
        public int? ReportsTo { get; set; }
    }
    public class PositionJobDetails
    {
        public Dropdown Department { get; set; }
        public Dropdown Section { get; set; }
        public Dropdown Grade { get; set; }
        public Dropdown BusinessType { get; set; }

        public string JDescription { get; set; }

        public DateTime? OpeningDate { get; set; }

        public string OpeningDateText { get; set; }

        public List<Dropdown> ParentPositions { get; set; } = new List<Dropdown>();
    }


    
}