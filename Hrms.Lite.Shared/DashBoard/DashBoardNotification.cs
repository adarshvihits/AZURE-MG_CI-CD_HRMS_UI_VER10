using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DashBoard
{
    public class DashBoardNotification
    {
        public int Code { get; set; }
        public string Type { get; set; }
        public int count { get; set; }
        public string Action { get; set; }
        public string colour { get; set; }
        public string lightcolour { get; set; }
        public string lightcolourclass { get; set; }
        public string Priority { get; set; }
    }
}
