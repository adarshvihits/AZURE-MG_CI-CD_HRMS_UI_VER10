using Hrms.Lite.Shared.General;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.Master
{
    public class Category:MasterBase
    {
        public int ActiveCount { get; set; }
        public int PendingCount { get; set; }
        public int InActiveCount { get; set; }
        public List<Category> CategoryList { get; set; }
        public CategoryType CategoryType { get; set; }
        public int? ExpiryPeriod { get; set; }
        public SelectList DropdownData { get; set; }
        //public string ExpiryPeriodMonth { get; set; }

    }
}
