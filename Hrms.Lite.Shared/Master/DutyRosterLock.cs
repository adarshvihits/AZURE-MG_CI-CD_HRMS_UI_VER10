using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.Master
{
    public class DutyRosterLock
    {        
        public SalaryPeriod Month { get; set; }
        public DateTime? LockDate { get; set; }
        public List<DutyRosterLockDetails> Details { get; set; }
    }
    public class DutyRosterLockDetails
    {
        public Circle Circle { get; set; }
        public DateTime? LockDate { get; set; }
        public string Remarks { get; set; }
        public bool LockStatus { get; set; }
    }
}
