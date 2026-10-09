using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.Master
{
    public class Circle : MasterBase
    {
        public int ActiveCount { get; set; }
        public int InactiveCount { get; set; }
        public int PendingCount { get; set; }
        public UserBase User { get; set; }
        public List<Circle> CircleList { get; set; }
        public List<CircleAuthority> CircleAuthorities { get; set; }
    }
    public class Authority : MasterBase
    {

    }
    public class Permission : MasterBase
    {

    }
    public class CircleAuthority
    {
        public int LevelCode { get; set; }
        public Authority Authority { get; set; }
        public Permission Permission { get; set; }
    }
}
