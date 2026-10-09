using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.Master
{
    public class NightAllowancePurpose
    {
        public int PurposeCode { get; set; }
        public Guid PurposeGi { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Department { get; set; }
        public string Remarks { get; set; }

        public int[] DptCode { get; set; }
        public string[] DptName { get; set; }
        public List<NightAllowancePurpose> NightAllowancePurposeList { get; set; }
        public string Mode { get; set; }
    }
}
