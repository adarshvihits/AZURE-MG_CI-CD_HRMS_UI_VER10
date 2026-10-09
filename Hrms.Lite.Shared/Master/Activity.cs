using Hrms.Lite.Shared.General;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Hrms.Lite.Shared.Master
{
    public class Activity : MasterBase
    {
       

        public int ActiveCount { get; set; }
        public int InactiveCount { get; set; }
        public int PendingCount { get; set; }
        public List<Activity> ActivityList { get; set; }
        public SelectList DropdownData { get; set; }
    }
}
