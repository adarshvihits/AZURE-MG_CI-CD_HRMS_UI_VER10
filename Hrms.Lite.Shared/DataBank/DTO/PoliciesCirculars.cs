using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.DTO
{
    public class PoliciesCirculars : MasterBase
    {
        public string CategoryName { get; set; }
        public Guid? CategoryGI { get; set; }
        public int policyCode { get; set; }
        public string Policyname { get; set; }
        public Guid? PolicyGI { get; set; }
        public DateTime? VisibleFrom { get; set; }
        public DateTime? VisibleTo { get; set; }
        public int AcceptanceRequired { get; set; }
        public string AccepRequiredTxt { get; set; }
        public int Priority { get; set; }
        public int CategoryCode { get; set; }   
        public string PriorityTxt { get; set; }
        public int ViewOrDownloadable { get; set; }
        public int FileType { get;set; }
        public int Multiple { get; set; }
        public int MultipleCombination1 { get; set; }
        public int MultipleCombination2 { get; set; }
        public int EmpCount { get; set; }
        public string Mode { get; set; }
        public EmployeeHeader Employee { get; set; }
        public List<PolicyCategory> Category { get; set; }
        public File Attachment { get; set; }
        public List<PoliciesCirculars> PolicyList { get; set; }
        public List<PolicyVsCombinations1> CombinationsList { get; set; }
        public List<PolicyVsCombinations1> Combinations1 { get; set; }
        public List<PolicyVsCombinations1> Combinations2 { get; set; }
    }
    public class PolicyCategory : MasterBase
    {
        public int SlNo { get; set; }

    }
    public class PolicyVsCombinations : MasterBase
    {
        public string EmployeeID { get; set; }


    }
    public class PolicyVsCombinations1 : MasterBase
    {
        public string EmployeeID { get; set; }
        public int TypeCode { get; set; }
        public int CombyType { get; set; } // 1-Single 2- multiple
        public int ApplCode1 { get; set; }

    }
    public class PolicyVsCombinations2 : MasterBase
    {
        public string EmployeeID { get; set; }
        public int TypeCode { get; set; }
        public int CombyType { get; set; } // 1-Single 2- multiple
        public int ApplCode2 { get; set; }
    }
}
