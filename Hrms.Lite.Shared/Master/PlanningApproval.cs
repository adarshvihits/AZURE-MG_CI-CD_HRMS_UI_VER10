using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;

namespace Hrms.Lite.Shared.Master
{
    public class PlanningApproval : MasterBase
    {
        public int Logcode { get; set; }
        public string Status { get; set; }
        public string planId { get; set; }
        public string BranchName { get; set; }
        public string DepartmentName { get; set; }
        public string SectionName { get; set; }
        public string GradeName { get; set; }
        public int PlanCount { get; set; }
        public int AvgSalary { get; set; }
        public int TotalSalary { get; set; }
        public int? PreviousPlanCount { get; set; }
        public int? PreviousAvgSalary { get; set; }
        public int? PreviousTotalSalary { get; set; }
        public string EntryType { get; set; }
        public string SubmittedBy { get; set; }
        public DateTime? SubmittedOn { get; set; }
        public string ApproverRole { get; set; }
        public string ApproverName { get; set; }
        public int StepNumber { get; set; }
        public DateTime? DecisionDate { get; set; }
        public string DecidedBy { get; set; }
        public string Remarks { get; set; }
        public Dropdown PlanningPeriod { get; set; }
        public int PendingCount { get; set; }
        public int ApprovedCount { get; set; }
        public int RejectedCount { get; set; }
        public List<PlanningApproval> PlanningApprovalList { get; set; }
    }
}
