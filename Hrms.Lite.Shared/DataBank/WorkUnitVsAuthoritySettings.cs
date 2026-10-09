using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank
{
    public class WorkUnitVsAuthoritySettings
    {
        public int SlNo { get; set; }
        public int WorkUnitCode { get; set; }
        public string WorkUnit { get; set; }
        public string ReportingManager { get; set; }
        public string LeaveAuthority { get; set; }
        public string Shift { get; set; }
        public List<WorkUnitVsAuthoritySettings> WorkUnitVsAuthList { get; set; }
        public List<WorkUnitVsAuthority> WorkUnitVsAuthority { get; set; }
        public List<WorkUnitVsAuthority> WorkUnitAuth { get; set; }
        public Dropdown RequestType { get; set; }
        public int flag { get; set; }
        public List<WorkUnitVsSift> WorkUnitVsSift { get; set; }
    }
    public class WorkUnitVsAuthority
    {
        public int RequestTypeCode { get; set; }
        public string RequestTypeName { get; set; }
        public int LevelCode { get; set; }
        public string LevelName { get; set; }
        public int AuthorityCode { get; set; }
        public string AuthorityName { get; set; }
    }
    public class WorkUnitVsSift
    {
        public int SlNo { get; set; }
        public int ShiftCode { get; set; }
        public string Shift { get; set; }
        public bool Applicability { get; set; }
    }
}
