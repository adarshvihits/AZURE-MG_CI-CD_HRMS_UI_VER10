using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.Master
{
    //public class QuickLinks:MasterBase
    //{
    //    public int ActiveCount { get; set; }
    //    public int InactiveCount { get; set; }
    //    public int PendingCount { get; set; }
    //    public List<QuickLinks> QuickLinksList { get; set; }
    //    public string UrlDescriptions { get; set; }
    //    public string Url { get; set; }
    //}




    public class QuickLinks : MasterBase
    {
        public List<QuickLinks> QuickLinksList { get; set; }
        public UserBase User { get; set; }
        public string UrlDescriptions { get; set; }
        public string Url { get; set; }
        public int Multiple { get; set; }
        public int MultipleCombination1 { get; set; }
        public int MultipleCombination2 { get; set; }
        public List<QuickLinksVsCombinations1> CombinationsList { get; set; }
        public List<QuickLinksVsCombinations1> Combinations1 { get; set; }
        public List<QuickLinksVsCombinations1> Combinations2 { get; set; }
        public int QuickLinksCode { get; set; }
    }
    public class QuickLinksVsCombinations1 : MasterBase
    {
        public string EmployeeID { get; set; }
        public int TypeCode { get; set; }
        public int CombyType { get; set; } // 1-Single 2- multiple
        public int ApplCode1 { get; set; }
        public bool Applicable { get; set; }

    }
    public class QuickLinksVsCombinations2 : MasterBase
    {
        public string EmployeeID { get; set; }
        public int TypeCode { get; set; }
        public int CombyType { get; set; } // 1-Single 2- multiple
        public int ApplCode2 { get; set; }
        public bool Applicable { get; set; }
    }






}
