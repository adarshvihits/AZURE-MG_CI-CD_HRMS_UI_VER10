using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.General
{
    public class Breadscrumb
    {
        public string DisplayName { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Area { get; set; }
    }
}
