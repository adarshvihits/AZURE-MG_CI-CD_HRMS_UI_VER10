using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.Master
{
    public class AttendanceStatus : MasterBase
    {
       
      

        public int ActiveCount { get; set; }
        public int InactiveCount { get; set; }
        public int PendingCount { get; set; }

        public string EntryType { get; set; }
        public List<AttendanceStatus> AttendanceStatusList { get; set; }

        public AttendanceStatusType AttendanceStatusType { get; set; }
        public AttendanceStatusType FH { get; set; }
        public AttendanceStatusType SH { get; set; }
        public string ForeColor { get; set; }
        public string BackColor { get; set; }
      
    }

    public class AttendanceStatusType : MasterBase
    {

    }
}

