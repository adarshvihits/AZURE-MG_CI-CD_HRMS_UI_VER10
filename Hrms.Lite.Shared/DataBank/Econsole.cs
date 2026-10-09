using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank
{
    public class Econsole
    {
        public Filter filter { get; set; }
        public int ActiveCount { get; set; }
        public int InactiveCount { get; set; }
        public int TabIndex { get; set; }
        public int consoleCode { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Category { get; set; }
        public Dropdown ConsoleType { get; set; }
        public Dropdown ConsoleCategory { get; set; }
        public int ViewOrDownloadable { get; set; }
        public DateTime? AssignmentDate { get; set; }
        public DateTime? FirstCompletionDate { get; set; }
        public DateTime? SecondCompletionDate { get; set; }
        public decimal AssignmentDay { get; set; }
        public decimal FirstCompletionDay { get; set; }
        public decimal SecondCompletionDay { get; set; }
        public bool Active { get; set; }
        public string Mode { get; set; }
        public File File { get; set; }
        public string Description { get; set; }
        public bool ForNewJoinee { get; set; }
        public int IsMultiCombination { get; set; }
        public Dropdown MultipleCombination1 { get; set; }
        public Dropdown MultipleCombination2 { get; set; }
        public List<Combinations> Combinations1 { get; set; }
        public List<Combinations> Combinations2 { get; set; }
        public List<Econsole> ConsoleList { get; set; }
        public bool DojAppli { get; set; }
        public int BeforeAfterButton { get; set; }
        public DateTime? Date { get; set; }
    }
    public class Combinations : MasterBase
    {
        public string EmployeeID { get; set; }
        public int TypeCode { get; set; }
        public int CombyType { get; set; } // 1-Single 2- multiple
        public int ApplCode1 { get; set; }

    }
}
