using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.Master
{
    public class Faq: MasterBase

    {
        public int ActiveCount { get; set; }
        public int InactiveCount { get; set; }
        public int PendingCount { get; set; }
        public List<Faq> FaqList { get; set; }
        public string Question { get; set; }
        public int FaqCategoryCode { get; set;}
        public int SoftwareCode { get; set; }
        public string Answer { get; set; }
        public string Software { get; set; }
        public string FaqCategory { get; set; }
    }
}
