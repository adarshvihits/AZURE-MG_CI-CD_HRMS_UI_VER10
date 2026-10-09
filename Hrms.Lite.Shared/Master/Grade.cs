using Hrms.Lite.Shared.General;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
namespace Hrms.Lite.Shared.Master
{
    public class Grade : MasterBase
    {
        public int ActiveCount { get; set; }
        public int InactiveCount { get; set; }
        public int PendingCount { get; set; }
        public int NoticePeriod { get; set; }

        public List<Grade> GradeList { get; set; }
        public SelectList DropdownData { get; set; }
    }
}
