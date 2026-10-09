using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.Generic
{
    public class EmployeeSettings
    {
        public ReportingManager ReportingManager { get; set; }
        public string EmployeeID { get; set; }
        public string PunchingID { get; set; }
        public Dropdown RequestType { get; set; }
        public List<RequestVsAuthoritySettings> RequestVsAuthoritySettings { get; set; } 
    }
    public class RequestVsAuthoritySettings { 
        public int RequestTypeCode { get; set; }
        public string RequestTypeName { get; set; }
        public int LevelCode { get; set; }
        public string LevelName { get; set; }
        public int AuthorityCode { get; set; }
        public string AuthorityName { get; set; }
    }
}
