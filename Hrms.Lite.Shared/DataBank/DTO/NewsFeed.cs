using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.DTO
{
    public class NewsFeed
    {
        public Guid? NewsFeedGI { get; set; }
        public DateTime? VisibleFrom { get; set; }
        public DateTime? VisibleTo { get; set; }
        public int NewsFeedCode { get; set; }
        public string NewsFeedname { get; set; }
        public int Multiple { get; set; }
        public int MultipleCombination1 { get; set; }
        public int MultipleCombination2 { get; set; }
        public int AcceptanceRequired { get; set; }
        public string AccepRequiredTxt { get; set; }
        public int Priority { get; set; }
        public string PriorityTxt { get; set; }
        public int ViewOrDownloadable { get; set; }
        public int EmpCount { get; set; }
        //public EmployeeHeader Employee { get; set; }
        public File Attachment { get; set; }
        public List<NewsFeed> NewsFeedList { get; set; }
        public List<NewsFeedVsCombinations1> CombinationsList { get; set; }
        public List<NewsFeedVsCombinations1> Combinations1 { get; set; }
        public List<NewsFeedVsCombinations1> Combinations2 { get; set; }
        public string Description { get; set; }
        public string Mode { get; set; }
    }

    public class NewsFeedVsCombinations1 
    {
        public string EmployeeID { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }

        public bool Applicability { get; set; }
    }
}


