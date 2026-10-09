using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.Master
{
    public class RoundOffSettings
    {
        public UserBase User { get; set; }
        public string TabName { get; set; }
        public List<RoundOffSettingsDetails> Details { get; set; }
    }
    public class RoundOffSettingsDetails
    {
        public int ParameterCode { get; set; }
        public string ParameterName { get; set; }
        public string Type { get; set; }
        public int RoundOffCode { get; set; }
        public string RoundOffName { get; set; }
    }
}

