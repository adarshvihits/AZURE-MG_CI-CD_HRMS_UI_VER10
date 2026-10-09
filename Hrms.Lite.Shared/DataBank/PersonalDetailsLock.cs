using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank
{
    public class PersonalDetailsLock : EmployeeHeader
    {
        public UserBase User { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string TabIndex { get; set; }
        public int active { get; set; }
        public int totalRecords { get; set; }
        public Filter Filter { get; set; }
        public int UnlockedCount { get; set; }
        public int LockedCount { get; set; }
        public List<PersonalDetailsLock> PersonalDetailsLockList { get; set; }

    }
}
