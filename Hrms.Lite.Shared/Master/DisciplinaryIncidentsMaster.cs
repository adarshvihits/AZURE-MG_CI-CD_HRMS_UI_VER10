using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.Master
{
   public class DisciplinaryIncidentsMaster:MasterBase
    {
        public int ActiveCount { get; set; }
        public int InactiveCount { get; set; }
        public int PendingCount { get; set; }
        public string Status { get; set; }
        public string EntryType { get; set; }
        public ResponseEntity ResponseEntity { get; set; }
        public List<DisciplinaryIncidentsMaster> DisciplinaryIncidentsList { get; set; }
       
        public int TabCode { get; set; }
    }
   
}
