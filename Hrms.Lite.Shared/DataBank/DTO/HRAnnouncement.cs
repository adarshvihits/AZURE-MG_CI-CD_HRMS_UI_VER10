using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.DTO
{
    public class HRAnnouncement
    {
        public Guid? AnnouncementGI { get; set; }
        public DateTime? VisibleFrom { get; set; }
        public DateTime? VisibleTo { get; set; }
        public int AnnouncementCode { get; set; }
        public string Announcementname { get; set; }
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
        public List<HRAnnouncement> AnnouncementList { get; set; }
        public List<AnnouncementVsCombinations1> CombinationsList { get; set; }
        public List<AnnouncementVsCombinations1> Combinations1 { get; set; }
        public List<AnnouncementVsCombinations1> Combinations2 { get; set; }
        public string Description { get; set; }
        public string Mode { get; set; }
    }

    public class AnnouncementVsCombinations1 : MasterBase
    {
        public string EmployeeID { get; set; }
        public int TypeCode { get; set; }
        public int CombyType { get; set; }
        public int ApplCode1 { get; set; }

    }
}
