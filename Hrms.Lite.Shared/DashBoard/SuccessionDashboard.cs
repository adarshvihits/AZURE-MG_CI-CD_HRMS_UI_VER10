using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DashBoard
{
    public class SuccessionDashboard
    {
        public int Classifn { get; set; }
        public List<Diagram> ClassifnDiagram { get; set; }
        public Diagram RiskDiagram { get; set; }
        public List<PlanningGrid> PlanningGrid { get; set; }
        public List<DataBank.DTO.SuccessorsList> SuccessorsList { get; set; }
    }

    public class Diagram
    {
        public string TypeFullName { get; set; }
        public string Type { get; set; }
        public int HighRiskCount { get; set; }
        public int LowRiskCount { get; set; }
        public int ModerateRiskCount { get; set; }
        public int TotalCount { get; set; }
    }
    public class PlanningGrid
    {
        public string KeyPositionCodeDesc { get; set; }
        public Guid KeyPositionGI { get; set; }
        public Guid EmployeeGI { get; set; }
        public string KeyPositionName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public int Strength { get; set; }
        public string EmployeeName { get; set; }
        public string Risk { get; set; }
        public int MappedCount { get; set; }

    }
}
