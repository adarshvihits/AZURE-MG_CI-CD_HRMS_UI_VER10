using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.Reports
{
    public class SkillMatrix
    {
        public List<SkillMatrix_List> SkillMatrixList { get; set; }
        public List<SkillMatrixEmployee_List> SkillMatrixEmpList { get; set; }
        public List<SkillsList> SkillsList { get; set; }
        public Filter Filter { get; set; }
    }
    public class SkillMatrixEmployee_List
    {
        public string EmployeeCode { get; set; }
        public Guid EmployeeGI { get; set; }
        public string Employeeid { get; set; }
        public string EmployeeName { get; set; }
        public string YOS { get; set; }
        public string DOJ { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public string Skill { get; set; }
        public string Rating { get; set; }

    }
    public class SkillsList
    {
        public int SkillCode { get; set; }
        public string SkillType { get; set; }
        public string Skill { get; set; }
        public string ShortName { get; set; }
    }

    public class SkillMatrix_List
    {
        public string EmployeeCode { get; set; }
        public Guid EmployeeGI { get; set; }
        public string Employeeid { get; set; }
        public string EmployeeName { get; set; }
        public string YOS { get; set; }
        public string DOJ { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public string WrittenAndOral { get; set; }
        public string RelationshipBuliding { get; set; }
        public string Design { get; set; }
        public string EgilityAndAdaptability { get; set; }
        public string RequirementGatheringAndAnalysis { get; set; }
        public string Innovation { get; set; }
        public string ConflictManagement { get; set; }
        public string DecisionMaking { get; set; }
        public string EmployeeMotivation { get; set; }
        public string CriticalThinking { get; set; }
        public string Negotiation { get; set; }
        public string Deployment { get; set; }
        public string ImplementationAndCoding { get; set; }
        public string Maintenance { get; set; }
        public string Testing { get; set; }
        public string Presentation { get; set; }
        public string Confidence { get; set; }
        public string Respect { get; set; }
        public string Clarity { get; set; }
        public string ActiveListening { get; set; }
        public string Feedback { get; set; }
        public string GatherKnowledge { get; set; }
        public string ListPossibleSolutions { get; set; }
        public string EvaluateProposedSolutions { get; set; }
        public string PracticeGeneratingIdeas { get; set; }
        public string ExperimentWithIdeas { get; set; }
        public string FormulateQuestions { get; set; }
        public string IdentifyProblems { get; set; }
        public string ImplementFindings { get; set; }
        public string TimeManagement { get; set; }
        public string AttentionToDetail { get; set; }
        public string Delegation { get; set; }
        public string StrategyPlanning { get; set; }
        public string CodingLanguages { get; set; }
        public string DatabaseKnowlege { get; set; }
        public string DataStructuresAndAlgorithms { get; set; }
        public string DebuggingKnowledge { get; set; }
        public string OperatingSystems { get; set; }
        public string SoftwareFrameworks { get; set; }
        public string SourceControl { get; set; }
        public string TestingProcedures { get; set; }
    }
}
