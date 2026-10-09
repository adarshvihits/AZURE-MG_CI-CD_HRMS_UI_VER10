using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;

namespace Hrms.Lite.Shared.DataBank.DTO
{
    public class PositionManager
    {
        public UserBase User { get; set; }
        public Filter Filter { get; set; }
        public Guid PositionGI { get; set; }
        public int PositionCode { get; set; }
        public string PositionName { get; set; }
        public string BranchName { get; set; }
        public string DepartmentName { get; set; }
        public string SectionName { get; set; }
        public string GradeName { get; set; }
        public int PositionNo { get; set; }
        public string ReportsTo { get; set; }
        public string Status { get; set; }
        public int TotalPositions { get; set; }
        public int FilledPositions { get; set; }
        public int VacantPositions { get; set; }
        public int FrozenPositions { get; set; }
        public int BranchCount { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public List<PositionManager> PositionManagerList { get; set; }
        public List<PositionManagerBranchSummary> BranchSummaryList { get; set; }
        public List<PositionEmployeeHistory> EmployeeHistoryList { get; set; }
        public List<PositionReportsToHistory> ReportsToHistoryList { get; set; }
    }

    public class PositionManagerBranchSummary
    {
        public string BranchName { get; set; }
        public int Filled { get; set; }
        public int Vacant { get; set; }
        public int Frozen { get; set; }
        public int Total { get; set; }
    }

    public class PositionEmployeeHistory
    {
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public bool IsCurrent { get; set; }
    }

    public class PositionReportsToHistory
    {
        public string ReportsTo { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public bool IsCurrent { get; set; }
        public string HeldBy { get; set; }
    }
}
