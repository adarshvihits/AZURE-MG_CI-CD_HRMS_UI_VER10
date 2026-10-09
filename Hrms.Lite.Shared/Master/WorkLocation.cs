using Hrms.Lite.Shared.General;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
namespace Hrms.Lite.Shared.Master
{
    public class WorkLocation : MasterBase
    {
        public int ActiveCount { get; set; }
        public int InactiveCount { get; set; }
        public int PendingCount { get; set; }
        public string EntryType { get; set; }
        public List<WorkLocation> WorkLocationList { get; set; }
        public SelectList DropdownData { get; set; }
    }
}
