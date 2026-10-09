using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.Master
{
    public class Reader : MasterBase
    {
        public int ActiveCount { get; set; }
        public int InactiveCount { get; set; }
        public int PendingCount { get; set; }        
        public List<Reader> ReaderList { get; set; }
        public string UserCode { get; set; }
        public string ReaderName { get; set; }
        public string IPAddress { get; set; }
        public string Port { get; set; }
        public MasterBase ReaderType { get; set; }
        public MasterBase Location { get; set; }
        public string BiometricIDInAttendanceDB { get; set; }
        public string Remarks { get; set; }


    }
}
