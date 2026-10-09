using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.Master
{
    public class NightAllowanceAmountSettings
    {
        public Dropdown Department { get; set; }
        public Dropdown Purpose { get; set; }
        public Dropdown Grade { get; set; }
        public int Code { get; set; }
        public Guid GI { get; set; }
        public decimal TimeFrom { get; set; }
        public decimal TimeTo { get; set; }
        public decimal Amount { get; set; }
        public string Mode { get; set; }
        public List<NightAllowanceAmountSettings> NightAllowanceAmountSettingsList { get; set; }
    }
}
