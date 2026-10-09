using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.TimeOffice.Generic
{
    public class Tracker
    {
        public string Stage { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public string ApprovalStatus { get; set; }
        public string Comments { get; set; }

        public string Approver { get; set; }

    }
}
